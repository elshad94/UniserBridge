using Project.Core.Enums;
using Microsoft.Data.SqlClient;
using Project.DataAccess.Repositories.Abstract.Common;
using Project.Core.Utilities.Results;
using Project.Core.Entities.SPModels;

namespace Project.DataAccess.Repositories.Concrete.Common;

public class ComboBoxRepository : IComboBoxRepository
{

    public Result GetCountries()
      => new()
      {
          Data = GetComboBoxValues<SP_KeyValueResult>(ComboBoxType.Countries)
      };


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