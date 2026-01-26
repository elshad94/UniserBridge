using Microsoft.AspNetCore.Http;

namespace Project.Core.Entities.Dtos.FileUploads;

public class SaveFormFileDto
{
    public string BaseFolder { get; set; }

    /// <summary>
    /// Create subfolders by current day. Example: Uploads/Contracts/2023/08/21/32edf5fc-dc5b-4031-a211-94ad47c17804.pdf 
    /// </summary>
    public bool AutoFolderDivision { get; set; } = true;

    public IFormFile FormFile { get; set; }
}