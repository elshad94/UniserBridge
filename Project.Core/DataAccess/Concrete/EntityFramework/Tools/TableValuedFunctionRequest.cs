using System.Text.Json.Serialization;
using Project.Core.Utilities.Exceptions;

public class TableValuedFunctionRequest
{
    private TableValuedFunctionFilter[] _filters;
    private int _nextPageNumber;
    private int _visibleItemCount;

    public int NextPageNumber
    {
        get => _nextPageNumber;
        set => _nextPageNumber = value < 1 
            ? throw new InvalidRequestParameterException<TableValuedFunctionRequest>(
                nameof(NextPageNumber), value)
            : value;
    }

    public int VisibleItemCount
    {
        get => _visibleItemCount;
        set => _visibleItemCount = value < 1 
            ? throw new InvalidRequestParameterException<TableValuedFunctionRequest>(
                nameof(VisibleItemCount), value)
            : value;
    }

    public TableValuedFunctionFilter[] Filters
    {
        get => _filters;
        set => _filters = value ?? Array.Empty<TableValuedFunctionFilter>();
    }

    [JsonIgnore] public int Offset => (NextPageNumber - 1) * VisibleItemCount;

    [JsonIgnore] public int Next => VisibleItemCount;

    public void SetColumnOrder(string columnName, FilteredColumnOrder columnOrder)
    {
        columnName = columnName.Trim();
        var filters = new List<TableValuedFunctionFilter>(Filters ?? Array.Empty<TableValuedFunctionFilter>());
        var filter = filters.FirstOrDefault(
            f => string.Equals(f.ColumnName, columnName, StringComparison.CurrentCultureIgnoreCase));

        switch (filter)
        {
            case null:
                filters.Add(new TableValuedFunctionFilter
                {
                    ColumnName = columnName.ToLower(),
                    Order = columnOrder
                });
                break;
            case { OrderString: null }:
                filter.Order = columnOrder;
                break;
        }

        Filters = filters.ToArray();
    }
}