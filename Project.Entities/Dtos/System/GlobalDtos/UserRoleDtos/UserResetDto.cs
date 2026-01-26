using System.ComponentModel.DataAnnotations;

namespace Project.Entities.Dtos.System.GlobalDtos.UserRoleDtos
{
    public class UserResetDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
