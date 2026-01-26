using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using Project.Core.DataAccess.Concrete.EntityFramework.Contexts;
using Project.Core.Entities.SPModels;
using Project.Core.Entities.SPModels.System;
using Project.Core.Enums;

public static class CommonRepository
{
    public static IList<SP_KeyValueResult> GetSpeCodeValues(string type)
    {
        var parameters = new List<SqlParameter>();
        parameters.AddParam("type", type);
        parameters.AddLanguageParam();

        return EfDbTools.ExecuteProcedure<SP_KeyValueResult>(
            "OBJ.SP_GetSpeCodeValues", parameters);
    }

    //public static IList<SP_GetServices> GetServices()
    //{
    //    var parameters = new List<SqlParameter>();
    //    parameters.AddLanguageParam();

    //    return EfDbTools.ExecuteProcedure<SP_GetServices>(
    //        "CRD.SP_GetServices", parameters);
    //}

    #region Auto number generation

    public static string GenerateDocumentNumber<TEntity, TOrderKey>(
        string prefix,
        Expression<Func<TEntity, TOrderKey>> orderByExpression,
        Expression<Func<TEntity, string>> selectExpression,
        Expression<Func<TEntity, bool>> whereExpression = null)
        where TEntity : class
    {
        var yearSuffix = (DateTime.Now.Year % 100).ToString();

        using var context = new ProjectAppDbContext();
        var queryableEntity = whereExpression == null
            ? context.Set<TEntity>()
            : context.Set<TEntity>().Where(whereExpression);

        var lastNumberStr = queryableEntity
            .OrderByDescending(orderByExpression)
            .Take(1)
            .Select(selectExpression)
            .FirstOrDefault();

        if (lastNumberStr == null
            || yearSuffix != lastNumberStr.Substring(
                prefix.Length, yearSuffix.Length))
        {
            return $"{prefix}{yearSuffix}-00001";
        }

        var lastNumber = int.Parse(lastNumberStr.Split('-')[1]);

        return $"{prefix}{yearSuffix}-{lastNumber + 1:00000}";
    }


    public static string GetLastNumber<TEntity, TOrderKey>(
        Expression<Func<TEntity, TOrderKey>> orderByExpression,
        Expression<Func<TEntity, string>> selectExpression,
        Expression<Func<TEntity, bool>> whereExpression = null)
        where TEntity : class
    {
        using var context = new ProjectAppDbContext();
        var queryableEntity = whereExpression == null
            ? context.Set<TEntity>()
            : context.Set<TEntity>().Where(whereExpression);

        var lastNumberStr = queryableEntity
            .OrderByDescending(orderByExpression)
            .Take(1)
            .Select(selectExpression)
            .FirstOrDefault();

        return lastNumberStr;
    }

    #endregion

    public static string GetResultMessageValue(ResultInfo resultMessage)
    {
        var parameters = new List<SqlParameter>
        {
            new("MessageCode", ((int)resultMessage).ToString())
        };
        parameters.AddLanguageParam();

        var getMessageResult = EfDbTools.ExecuteProcedure<SP_GetMessage>(
            "OBJ.SP_GetMessage", parameters);



        if (getMessageResult == null
            || getMessageResult.Count == 0)
        {
            return resultMessage.ToString();
        }

        return getMessageResult.First().MessageValue;
    }

    public static IList<T> GetAutoCompletedValues<T>(
        string filter, 
        AutoCompleteType type,
        int filterType = 0
        )
        where T : class
    {
        var parameters = new List<SqlParameter>
        {
            new("filter", filter),
            new("type", (int)type),
            new("filterType", filterType)
        };
        parameters.AddLanguageParam();

        var result = EfDbTools.ExecuteProcedure<T>(
            "OPR.SP_GetAutoCompletes", parameters);

        return result;
    }

    public static IList<SP_KeyValueResult> GetAutoCompletedValues(
        string filter,
        AutoCompleteType type,
        int filterType = 0
        )
        => GetAutoCompletedValues<SP_KeyValueResult>(filter, type, filterType);

    public static IList<T> GetComboBoxValues<T>(ComboBoxType type, string filter = null) where T : class
    {
        var parameters = new List<SqlParameter>
        {
            new("type", (int)type),
            new("filter", filter ?? string.Empty)
        };

        parameters.AddLanguageParam();

        var result = EfDbTools.ExecuteProcedure<T>(
            "OPR.SP_GetComboBoxValues", parameters);

        return result;
    }
}