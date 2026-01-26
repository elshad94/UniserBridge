using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Project.Core.Entities.Dtos.CommonDtos
{
    public class SaveFileDto
    {
        [Required]
        public int ReferenceId { get; set; }

        [Required]
        public IFormFile FormFile { get; set; }
    }
}
