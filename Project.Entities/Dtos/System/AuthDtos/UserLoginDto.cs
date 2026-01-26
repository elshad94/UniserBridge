using Project.Core.Mapper;
using Project.Core.Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Project.Entities.Dtos.System.AuthDtos
{
    public class UserLoginDto : IMapTo<User>
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
