using Project.Core.Entities.Models;
using Project.Core.Mapper;

namespace Project.Entities.Dtos.System.AuthDtos
{
    public class LanguageResponse : IMapTo<Language>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
