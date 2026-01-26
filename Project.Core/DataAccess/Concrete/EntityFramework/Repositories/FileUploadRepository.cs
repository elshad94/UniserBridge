using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Project.Core.DataAccess.Abstract;
using Project.Core.DataAccess.Concrete.EntityFramework.Contexts;
using Project.Core.Entities.Dtos.FileUploads;
using Project.Core.Entities.Models;
using Project.Core.Entities.SPModels;
using Project.Core.Utilities.Results;
using Microsoft.AspNetCore.StaticFiles;
using Project.Core.Entities.Dtos.CommonDtos;

namespace Project.Core.DataAccess.Concrete.EntityFramework.Repositories;

public class FileUploadRepository : EntityRepositoryBase<FileUpload, ProjectAppDbContext>, IFileUploadRepository
{

    public FileUploadRepository(IMapper mapper) : base(mapper)
    {
    }

    private static string BasePath => "..\\Project.Core\\Uploads";

    public IList<SP_GetUploadedFilesInfo> GetUploadedFilesInfo(
        string tableName,
        int id,
        short? documentType = null)
    {
        var parameters = new List<SqlParameter>
        {
            new("tableName", tableName),
            new("id", id)
        };


        if (documentType.HasValue)
        {
            parameters.Add(new SqlParameter("docType", documentType.Value));
        }

        var data = EfDbTools.ExecuteProcedure<SP_GetUploadedFilesInfo>("OPR.SP_GetUploadedFilesInfo", parameters);

        return data;
    }


    //public ResultInfo Upload(FileUploadDto fileDto)
    //{
    //    if (fileDto.FormFile == null
    //        || !IsCorrectFileFormat(fileDto.FormFile))
    //    {
    //        return ResultInfo.SaveFailure;
    //    }

    //    var directoryPath = Path.Combine(BasePath, fileDto.BasePath);

    //    if (!Directory.Exists(directoryPath))
    //    {
    //        Directory.CreateDirectory(directoryPath);
    //    }

    //    var fileName = Guid.NewGuid()
    //                   + Path.GetExtension(fileDto.FormFile.FileName);
    //    var filePath = Path.Combine(directoryPath, fileName);
    //    using var fs = new FileStream(filePath, FileMode.Create);
    //    fileDto.FormFile.CopyTo(fs);

    //    var fileUpload = new FileUpload
    //    {
    //        FileName = fileDto.FormFile.FileName,
    //        Url = filePath,
    //        CreateUserId = CurrentScopeDataContainer.Instance.UserId
    //    };

    //    Mapper.Map<FileUpload>(fileDto, fileUpload);

    //    return Add(fileUpload).ResultInfo;
    //}


    /// <summary>
    /// Only uploads new file to project folder and returns FileUrl
    /// </summary>
    public Result SaveFile(SaveFormFileDto fileDto)
    {
        if (fileDto.FormFile == null
            || !IsCorrectFileFormat(fileDto.FormFile))
        {
            return new(ResultInfo.SaveFailure);
        }

        var directoryPath = Path.Combine(BasePath, fileDto.BaseFolder);
        var currentDatePath = DateTime.Now.ToString("yyyy-MM-dd").Replace("-", @"\");

        if (fileDto.AutoFolderDivision)
            directoryPath = Path.Combine(directoryPath, currentDatePath);

        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        string extension = Path.GetExtension(fileDto.FormFile.FileName);
        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(directoryPath, fileName);

        if (!fileDto.AutoFolderDivision) currentDatePath = string.Empty;

        var fileUrl = Path.Combine("Uploads", fileDto.BaseFolder, currentDatePath, fileName);

        using var fs = new FileStream(filePath, FileMode.Create);
        fileDto.FormFile.CopyTo(fs);

        return new(fileUrl, ResultInfo.SaveSuccess);
    }


    /// <summary>
    /// Uploads and saves new file to database
    /// </summary>
    public ResultInfo Upload(FileUploadDto fileDto)
    {
        var saveFileResult = SaveFile(new SaveFormFileDto { BaseFolder = fileDto.BasePath, FormFile = fileDto.FormFile }); //Upload new file to project folder
       
        if (saveFileResult.ResultInfo == ResultInfo.SaveFailure)
            return ResultInfo.SaveFailure;

        string fileUrl = (string)saveFileResult.Data;

        //Add file to database
        var fileUpload = new FileUpload
        {
            FileName = fileDto.FormFile.FileName,
            TableId = fileDto.TableId,
            Url = fileUrl,
            DocType = fileDto.DocType,
            CreateUserId = CurrentScopeDataContainer.Instance.UserId
        };

        Mapper.Map<FileUpload>(fileDto, fileUpload);

        return Add(fileUpload).ResultInfo;
    }


    /// <summary>
    /// Deletes old files, uploads and saves new file to database
    /// </summary>
    public ResultInfo Upload(SaveFileDto model, string tableName, bool autoFolderDivision = true)
    {
        //find old files and delete them
        DeleteFiles(tableName, model.ReferenceId);

        //upload new file
        return Upload(new FileUploadDto { BasePath = tableName, TableId = model.ReferenceId, TableName = tableName, AutoFolderDivision = autoFolderDivision, FormFile = model.FormFile });
    }


    public Result UploadTempFile(IFormFile formFile)
    {
        if (formFile == null
            || !IsCorrectFileFormat(formFile))
        {
            return new Result { ResultInfo = ResultInfo.SaveFailure };
        }

        var directoryPath = Path.Combine(BasePath, "Temp");

        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}";
        var filePath = Path.Combine(directoryPath, newFileName);
        using var fs = new FileStream(filePath, FileMode.Create);
        formFile.CopyTo(fs);

        return new Result
        {
            ResultInfo = ResultInfo.Success,
            Data = new { FileName = newFileName }
        };
    }


    /// <summary>
    /// Moves old file from Temp folder and saves file into database
    /// </summary>
    public ResultInfo MoveFile(FileUploadDto fileDto, string oldFileName)
    {
        var directoryPath = Path.Combine(BasePath, fileDto.BasePath);

        var currentDatePath = DateTime.Now.ToString("yyyy-MM-dd").Replace("-", @"\");

        if (fileDto.AutoFolderDivision)
            directoryPath = Path.Combine(directoryPath, currentDatePath);

        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var filePath = Path.Combine(directoryPath, oldFileName);

        if (!fileDto.AutoFolderDivision) currentDatePath = string.Empty;

        var fileUrl = Path.Combine("Uploads", fileDto.BasePath, currentDatePath, oldFileName);

        try
        {
            File.Move(Path.Combine(BasePath, "Temp", oldFileName), filePath);

            var fileUpload = new FileUpload
            {
                FileName = oldFileName,
                TableId = fileDto.TableId,
                Url = fileUrl,
                CreateUserId = CurrentScopeDataContainer.Instance.UserId
            };

            Mapper.Map<FileUpload>(fileDto, fileUpload);

            return Add(fileUpload).ResultInfo;
        }
        catch (Exception)
        {
            return ResultInfo.NotFound;
        }
    }


    public DownloadedFileResult Download(Guid key)
    {
        var fileUpload = Get(f => f.DownloadKey == key
                                    && f.Status == true);

        if (fileUpload == null)
        {
            return null;
        }

        using var context = new ProjectAppDbContext();
        var uploadSetting = context.FileUploadSettings
            .FirstOrDefault(s =>
                s.Extension == Path.GetExtension(fileUpload.FileName)
                    && s.Status == true);

        if (uploadSetting == null)
        {
            return null;
        }

        var result = new DownloadedFileResult
        {
            Content = File.ReadAllBytes(fileUpload.Url),
            ContentType = uploadSetting.ContentType,
            FileName = fileUpload.FileName
        };

        return result;
    }


    public DownloadedFileResult DownloadTempFile(string fileName)
    {
        var directoryPath = Path.Combine(BasePath, "Temp");
        var filePath = Path.Combine(directoryPath, fileName);

        if (!File.Exists(filePath))
            return null;

        var result = new DownloadedFileResult
        {
            Content = File.ReadAllBytes(filePath),
            ContentType = GetMimeTypeForFileExtension(filePath),
            FileName = fileName
        };

        return result;
    }

    public ResultInfo Delete(Guid key)
    {
        var fileUpload = Get(f => f.DownloadKey == key
                                  && f.Status == true);

        if (fileUpload == null)
        {
            return ResultInfo.NotFound;
        }

        fileUpload.Status = false;

        return Update(fileUpload).ResultInfo;
    }


    public void DeleteFiles(string tableName, int referenceId)
    {
        var files = GetAll(f => f.TableId == referenceId && f.TableName == tableName && (bool)f.Status);

        foreach (var file in files)
        {
            Delete((Guid)file.DownloadKey);
        }
    }


    private static bool IsCorrectFileFormat(IFormFile formFile)
    {

        using var context = new ProjectAppDbContext();
        return context.FileUploadSettings.Any(
            s => s.Status == true
                && s.ContentType == formFile.ContentType
                 && s.Extension == Path.GetExtension(formFile.FileName)
                 && formFile.Length <= s.SizeInMegabyte * 1024 * 1024);
    }

    private string GetMimeTypeForFileExtension(string filePath)
    {
        const string DefaultContentType = "application/octet-stream";

        var provider = new FileExtensionContentTypeProvider();

        if (!provider.TryGetContentType(filePath, out string contentType))
        {
            contentType = DefaultContentType;
        }

        return contentType;
    }
}