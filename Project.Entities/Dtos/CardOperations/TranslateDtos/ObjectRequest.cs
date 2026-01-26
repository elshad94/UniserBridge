using Project.Core.Entities.Models;
using Project.Core.Mapper;

namespace Project.CardOperations.API.Infrastructure.Entities.Dtos.TranslateDtos
{
    public class ObjectRequest :IMapTo<ObjectContentsLang>
    {
        public int? ObjectId { get; set; }
        public List<ObjectLangList> Language { get; set; }
    }
}
