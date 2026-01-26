using Project.Core.Enums;
using Project.Core.Utilities.Results;
using Project.DataAccess.Repositories.Abstract.Common;

namespace Project.DataAccess.Repositories.Concrete.Common;

public class AutoCompleteRepository : IAutoCompleteRepository
{

    public Result GetCountries(string filter)
    {
        return new Result
        {
            Data = CommonRepository.GetAutoCompletedValues(filter, AutoCompleteType.Countries)
        };
    }

}
