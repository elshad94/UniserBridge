using System.Text.Json.Serialization;
using Microsoft.Data.SqlClient;
using Project.Core.Enums;

public enum FilteredColumnOrder
{
    Default = 0,
    Asc = 1,
    Desc = 2
}

public class TableValuedFunctionFilter
{
    private string _columnName;

    public TableValuedFunctionFilter()
    {
        Order = FilteredColumnOrder.Default;
    }

    public string ColumnName
    {
        get => _columnName;
        set => _columnName = value.Trim();
    }

    public string Value { get; set; }

    public FilteredColumnOrder Order { get; set; }

    /// <summary>
    /// test fdfgsdfs
    /// </summary>
    public ColumnFilterType ColumnFilterType { get; set; }

    [JsonIgnore] private string SqlParameterName => $"__col__{ColumnName}";

    [JsonIgnore]
    public string Filter
    {
        get
        {
            switch (ColumnFilterType)
            {
                case ColumnFilterType.Contains:
                    return $"[{ColumnName}] like N'%' + @{SqlParameterName} + N'%'";

                case ColumnFilterType.DoesNotContain:
                    return $"[{ColumnName}] not like N'%' + @{SqlParameterName} + N'%'";

                case ColumnFilterType.Equals:
                    return $"[{ColumnName}] = @{SqlParameterName} ";

                case ColumnFilterType.DoesNotEqual:
                    return $"[{ColumnName}] <> @{SqlParameterName} ";

                case ColumnFilterType.BeginsWith:
                    return $"[{ColumnName}] like N'' + @{SqlParameterName} + N'%'";

                case ColumnFilterType.EndsWith:
                    return $"[{ColumnName}] like N'%' + @{SqlParameterName} + N''";

                default:
                    goto case ColumnFilterType.Contains;
            }
        }
    }

    [JsonIgnore]
    public string OrderString => Order != FilteredColumnOrder.Default && Enum.IsDefined(Order)
        ? $"[{ColumnName}] {Order}" : null;

    [JsonIgnore] public SqlParameter ASqlParameter => new(SqlParameterName, Value ?? string.Empty);
}