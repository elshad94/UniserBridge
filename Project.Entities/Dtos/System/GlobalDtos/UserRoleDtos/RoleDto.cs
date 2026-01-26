using System.ComponentModel.DataAnnotations;
using Project.Core.Entities.Models;
using Project.Core.Mapper;

namespace Project.Entities.Dtos.System.GlobalDtos.UserRoleDtos
{
    public class RoleDto : IMapTo<Role>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
        public bool? Status { get; set; }
    }


}
