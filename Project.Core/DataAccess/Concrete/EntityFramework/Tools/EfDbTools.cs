using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Core.DataAccess.Concrete.EntityFramework.Contexts;
using Project.Core.Entities.SPModels;
using Project.Core.Utilities.Exceptions;

public static class EfDbTools
{
    private enum DbObjectType
    {
        Procedure = 1,
        Function = 2
    }

    public static IList<T> ExecuteProcedure<T>(
        string procedureName, 
        params SqlParameter[] parameters)
        where T : class
    {
        if (parameters != null
            && parameters.Any(p => p.Value == null))
        {
            return new List<T>();
        }

        using var context = new ProjectAppDbContext();

        if (!HasObjectInDb(context, procedureName, DbObjectType.Procedure))
        {
            throw new InvalidRequestParameterException(
                $"Invalid procedure name: '{procedureName}'.");
        }

        var parametersStr = parameters != null
            ? string.Join(", ", parameters.Select(p => $"@{p.ParameterName} = @{p.ParameterName}"))
            : "";
        var query =
            $"exec {procedureName} {parametersStr}";
        var parametersList = new List<object>();

        if (parameters != null)
        {
            parametersList.AddRange(parameters);
        }

        return context
            .Set<T>()
            .FromSqlRaw(query, parametersList.ToArray())
            .ToList();
    }

    public static IList<T> ExecuteProcedure<T>(
        string procedureName,
        IList<SqlParameter> parameters)
        where T : class
    {
        return ExecuteProcedure<T>(procedureName, parameters.ToArray());
    }

    private static TableValuedFunctionResult<T> ExecuteTableValuedFunction<T>(
        string functionName, 
        int offset, 
        int next,
        bool defaultDesc,
        TableValuedFunctionFilter[] filters,
        params SqlParameter[] parameters)
        where T : class
    {
        using var context = new ProjectAppDbContext();

        if (!HasObjectInDb(context, functionName, DbObjectType.Function))
        {
            throw new InvalidRequestParameterException(
                $"Invalid function name: '{functionName}'.");
        }

        var parametersList = new List<object>();

        if (parameters != null)
        {
            parametersList.AddRange(parameters);
        }

        string filtersString = "", ordersString = "";

        if (filters is {Length: > 0} && IsFiltersValid<T>(filters))
        {
            filtersString = string.Join(" and ", filters.Where(f => f.OrderString == null).Select(f => f.Filter));
            ordersString = string.Join(", ", filters.Where(f => f.OrderString != null)
                .Select(f => f.OrderString));

            parametersList.AddRange(filters.Select(f => f.ASqlParameter));
        }

        var parametersStr = parameters != null ? string.Join(", ", parameters.Select(p => $"@{p.ParameterName}")) : "";
        var query = $"select * from {functionName}({parametersStr})" +
                    $"{(filtersString.Length > 0 ? $" where {filtersString}" : "")}";

        var dbSet = context.Set<T>();
        var parametersArr = parametersList.ToArray();
        var orderBySql =
            $" order by {(ordersString.Length > 0 ? $"{ordersString}" : $"1{(defaultDesc ? " desc" : "")}")}";
       
        query += orderBySql;

        context.Database.SetCommandTimeout((int)TimeSpan.FromMinutes(2).TotalSeconds);  //Some queries need more time to execute
        
        if (next == int.MaxValue)
        {
            //query += orderBySql;
            var data = dbSet.FromSqlRaw(query, parametersArr).AsNoTracking().ToList();
            return new TableValuedFunctionResult<T>
            {
                Result = data,
                Count = data.Count
            };
        }


        var allData = dbSet.FromSqlRaw(query, parametersArr).AsNoTracking().ToList();

        //+ $" offset {offset} rows fetch next {next} rows only";
        var result = new TableValuedFunctionResult<T> 
        {
            Count = allData.Count,
            Result = allData.Skip(offset).Take(next).ToList()
        };

        return result;
    }

    public static TableValuedFunctionResult<T> ExecuteTableValuedFunction<T>(
        string functionName,
        TableValuedFunctionFilter[] filters,
        IList<SqlParameter> parameters,
        bool defaultDesc = true
    )
        where T: class
    {
        return ExecuteTableValuedFunction<T>(
            functionName,
            0,
            int.MaxValue,
            defaultDesc,
            filters,
            parameters.ToArray());
    }

    public static TableValuedFunctionResult<T> ExecuteTableValuedFunction<T>(
        string functionName,
        TableValuedFunctionRequest request,
        bool defaultDesc,
        params SqlParameter[] parameters)
        where T: class
    {
        if (request == null)
        {
            throw new InvalidRequestParameterException
                <TableValuedFunctionRequest>(nameof(request), null);
        }

        return ExecuteTableValuedFunction<T>(
            functionName,
            request.Offset,
            request.Next,
            defaultDesc,
            request.Filters,
            parameters);
    }

    public static TableValuedFunctionResult<T> ExecuteTableValuedFunction<T>(
        string functionName,
        TableValuedFunctionRequest request,
        params SqlParameter[] parameters)
        where T : class
    {
        return ExecuteTableValuedFunction<T>(
            functionName,
            request,
            true,
            parameters);
    }

    public static TableValuedFunctionResult<T> ExecuteTableValuedFunction<T>(
        string functionName,
        TableValuedFunctionRequest request,
        IList<SqlParameter> parameters,
        bool defaultDesc = true)
        where T: class
    {
        return ExecuteTableValuedFunction<T>(
            functionName,
            request,
            defaultDesc,
            parameters.ToArray());
    }

    /// <summary>
    /// Add parameter to list
    /// </summary>
    public static void AddParam(this IList<SqlParameter> list, string paramName, object paramValue)
    {
        list.Add(new SqlParameter(paramName, paramValue));
    }

    /// <summary>
    /// Add UserId parameter to list
    /// </summary>
    public static void AddUserIdParam(this IList<SqlParameter> list)
    {
        list.AddParam("UserId", CurrentScopeDataContainer.Instance.UserId.ToString());
    }

    /// <summary>
    /// Add Language value to list
    /// </summary>
    public static void AddLanguageParam(this IList<SqlParameter> list)
    {
        list.AddParam("Language", CurrentScopeDataContainer.Instance.Language);
    }

    /// <summary>
    /// Adds UserId, Language values to list
    /// </summary>
    public static void AddSystemParams(this IList<SqlParameter> list)
    {
        list.AddUserIdParam();
        list.AddLanguageParam();
    }

    private static bool HasObjectInDb(
        ProjectAppDbContext context,
        string objectName,
        DbObjectType objectType)
    {
        var dbObjects = context
            .Set<SP_GetDbObjects>()
            .FromSqlRaw($"exec dbo.SP_GetDbObjects {(int)objectType}")
            .ToList();

        return dbObjects.FirstOrDefault(o => string.Equals(o.ObjectName, objectName, StringComparison.CurrentCultureIgnoreCase)) != null;
    }

    private static bool IsFiltersValid<T>(TableValuedFunctionFilter[] filters)
        where T: class
    {
        var properties = typeof(T).GetProperties();

        return filters
            .All(filter => properties
                .FirstOrDefault(prop => string
                    .Equals(prop.Name, filter.ColumnName, StringComparison.CurrentCultureIgnoreCase)) == null 
                ? throw new InvalidRequestParameterException<TableValuedFunctionFilter>(
                    nameof(filter.ColumnName), filter.ColumnName) 
                : true);
    }
}