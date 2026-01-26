using Project.Core.Utilities.Results;

namespace Project.DataAccess.Repositories.Abstract.Common;

public interface IAutoCompleteRepository
{
    Result GetCountries(string filter);

}
