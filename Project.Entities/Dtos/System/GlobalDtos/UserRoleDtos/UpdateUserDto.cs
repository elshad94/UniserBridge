using System.ComponentModel.DataAnnotations;
using Project.Core.Entities.Models;
using Project.Core.Mapper;

namespace Project.Entities.Dtos.System.GlobalDtos.UserRoleDtos
{
    public class UpdateUserDto : IMapTo<User>
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public string FinCode { get; set; }
        public bool? Gender { get; set; }
        [Required]
        public string Username { get; set; }

        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        //public bool? Status { get; set; }


    }
}
