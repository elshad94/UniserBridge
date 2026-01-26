using Project.Core.Entities.Dtos.CommonDtos;
using Project.Core.Entities.Dtos.FileUploads;
using Project.Core.Entities.Models;
using Project.Core.Entities.SPModels;
using Project.Core.Utilities.Results;
using Microsoft.AspNetCore.Http;

namespace Project.Core.DataAccess.Abstract;

public interface IFileUploadRepository : IEntityRepositoryBase<FileUpload>
{
    IList<SP_GetUploadedFilesInfo> GetUploadedFilesInfo(string tableName, int id, short? documentType = null);

    /// <summary>
    /// Only uploads new file to project folder and returns FileUrl
    /// </summary>
    Result SaveFile(SaveFormFileDto fileDto);


    /// <summary>
    /// Uploads and saves new file to database
    /// </summary>
    ResultInfo Upload(FileUploadDto fileDto);

    /// <summary>
    /// Deletes old files, uploads and saves new file to database
    /// </summary>
    ResultInfo Upload(SaveFileDto model, string tableName, bool autoFolderDivision = true);

    Result UploadTempFile(IFormFile formFile);

    DownloadedFileResult Download(Guid key);
    DownloadedFileResult DownloadTempFile(string fileName);

    ResultInfo MoveFile(FileUploadDto fileDto, string oldFileName);

    ResultInfo Delete(Guid key);
    void DeleteFiles(string tableName, int referenceId);
}