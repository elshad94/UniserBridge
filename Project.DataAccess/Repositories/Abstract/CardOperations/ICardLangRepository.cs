using Project.Core.DataAccess.Abstract;
using Project.Core.Entities.Models;
using Project.Core.Utilities.Results;

namespace Project.DataAccess.Repositories.Abstract.CardOperations
{
    public interface ICardLangRepository : IEntityRepositoryBase<CardLang>
    {
        Result GetCardLangByType(string type, int cardId);
    }
}
