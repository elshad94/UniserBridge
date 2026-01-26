using Microsoft.AspNetCore.Http;
using Project.Core.Entities.Models;
using Project.Core.Mapper;

namespace Project.Core.Entities.Dtos.FileUploads;

public class FileUploadDto : IMapTo<FileUpload>
{
    /// <summary>
    /// [moduleName]\[controllerName]
    /// </summary>
    public string BasePath { get; set; }

    public string TableName { get; set; }

    public int? TableId { get; set; }

    public short? DocType { get; set; }

    /// <summary>
    /// Create subfolders by current day. Example: Uploads/Contracts/2023/08/21/32edf5fc-dc5b-4031-a211-94ad47c17804.pdf 
    /// </summary>
    public bool AutoFolderDivision { get; set; } = true;

    public IFormFile FormFile { get; set; }
}