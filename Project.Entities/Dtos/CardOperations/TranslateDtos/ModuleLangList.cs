using System.Text.Json.Serialization;
using Project.Core.Entities.Models;
using Project.Core.Mapper;

namespace Project.CardOperations.API.Infrastructure.Entities.Dtos.TranslateDtos
{
    public class ModuleLangList : IMapTo<ModuleLang>
    {
        public int? LangId { get; set; }

        public string Value { get; set; }
        [JsonIgnore]
        public bool? Status { get; set; }
    }
}
