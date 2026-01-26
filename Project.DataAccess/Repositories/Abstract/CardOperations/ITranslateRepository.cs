using Project.CardOperations.API.Infrastructure.Entities.Dtos.TranslateDtos;
using Project.Core.DataAccess.Abstract;
using Project.Core.Entities.Models;
using Project.Core.Utilities.Results;

namespace Project.DataAccess.Repositories.Abstract.CardOperations
{
    public interface ITranslateRepository : IEntityRepositoryBase<MenuLang>
    {
        Result GetMenuNames();
        Result Save(EditMenuNameRequest edit);
        Result GetModules();
        Result SaveModul(SaveModuleRequest edit);
        Result GetModuleById(int id);
        Result GetMenuById(int id);
        Result GetObjectNames();
        Result GetObjectById(int id);
        Result SaveObject(ObjectRequest edit);
    }
}
