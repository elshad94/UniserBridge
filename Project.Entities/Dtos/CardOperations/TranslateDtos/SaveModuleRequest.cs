using Project.Core.Entities.Models;
using Project.Core.Mapper;

namespace Project.CardOperations.API.Infrastructure.Entities.Dtos.TranslateDtos
{
    public class SaveModuleRequest: IMapTo<ModuleLang>
    {
        public int? ModulId { get; set; }
        public List<ModuleLangList> Language { get; set; }
    }
}
