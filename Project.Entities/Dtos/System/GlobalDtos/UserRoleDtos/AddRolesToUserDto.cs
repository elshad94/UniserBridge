using System.ComponentModel.DataAnnotations;

namespace Project.Entities.Dtos.System.GlobalDtos.UserRoleDtos
{
    public class AddRolesToUserDto
    {
        [Required]
        public int UserId { get; set; }
        public List<int> RoleIds { get; set; }
    }
}
