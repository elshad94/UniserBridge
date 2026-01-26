using Project.Core.DataAccess.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.API.Controllers.Common
{
    [Route("Common/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class FilesController : ControllerBase
    {
        private readonly IFileUploadRepository _fileUploadRepository;


        public FilesController(IFileUploadRepository fileUploadRepository)
        {
            _fileUploadRepository = fileUploadRepository;
        }


        //[HttpGet]
        //public IActionResult Download(Guid key)
        //{
        //    var result = _fileUploadRepository.Download(key);

        //    return result == null ? NotFound() : this.AsFileResult(result);
        //}

        //[HttpDelete]
        //public IActionResult Delete(Guid key)
        //{
        //    var result = _fileUploadRepository.Delete(key);

        //    return result.AsObjectResult();
        //}

        //[HttpPost]
        //public IActionResult DownloadTempFile(string fileName)
        //{
        //    var result = _fileUploadRepository.DownloadTempFile(fileName);

        //    return result == null ? NotFound() : this.AsFileResult(result);
        //}


        //[HttpPost]
        //public IActionResult UploadTempFile(IFormFile formFile)
        //{
        //    //incomingFileDto.   Request.Form.Files[0];
        //    var result = _fileUploadRepository.UploadTempFile(formFile);
        //    return result.AsObjectResult();
        //}


        //[HttpPost]
        //public IActionResult UploadTestFile(IFormFile formFile) //test
        //{
        //    var result = _fileUploadRepository.Upload(new FileUploadDto { BasePath = "Test", TableId = 12, TableName = "Test", AutoFolderDivision = true, FormFile = formFile });
        //    return result.AsObjectResult();
        //}

        //[HttpPost]
        //public IActionResult MoveFile() //Test
        //{
        //    var result = _fileUploadRepository.MoveFile(new FileUploadDto { BasePath = "Contracts", TableId = 12, TableName = "Contracts", AutoFolderDivision = true}, "2ade34df-489a-427e-be49-c2e721e4c7fb.png");
        //    return result.AsObjectResult();
        //}
    }
}
