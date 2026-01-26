using Project.Core.Entities.Models;
using Project.Core.Mapper;

namespace Project.CardOperations.API.Infrastructure.Entities.Dtos.TranslateDtos
{
    public class EditMenuNameRequest : IMapTo<MenuLang>
    {
        public int? MenuId { get; set; }
        public List<MenuLangList> Language { get; set; }
    }
}
