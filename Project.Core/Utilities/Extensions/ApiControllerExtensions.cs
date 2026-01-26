using Microsoft.AspNetCore.Mvc;
using Project.Core.Entities.Dtos.FileUploads;
using Project.Core.Utilities.Results;
using Project.Core.Utilities.Tools;

public static class ApiControllerExtensions
{
    public static IActionResult AsObjectResult(
        this Result result)
        => new ObjectResult(ResultDataGenerator.Generate(result));


    public static IActionResult AsObjectResult(
        this ResultInfo resultInfo)
        => new ObjectResult(ResultDataGenerator.Generate(resultInfo));


    public static IActionResult AsExcelFile(this ControllerBase controller,object data, string documentName=null)
    {
        
        string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        var byteArray = FileExporterTools.ExportDataToExcel(data, documentName);
        return controller.File(byteArray, contentType);

       // return  new FileContentResult((ResultDataGenerator.Generate(resultInfo));
    }


    public static IActionResult AsFileResult(
        this ControllerBase controller,
        DownloadedFileResult downloadedFile)
    {
        return controller.File(
            downloadedFile.Content, 
            downloadedFile.ContentType, 
            downloadedFile.FileName);
    }
}