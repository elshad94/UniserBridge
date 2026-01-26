using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.CardOperations.API.Infrastructure.Entities.Dtos.TranslateDtos;
using Project.DataAccess.Repositories.Abstract.CardOperations;
using Microsoft.AspNetCore.Authorization;

namespace Project.API.Controllers.CardOperations
{
    [Route("CardOperations/[controller]/[action]")]
    [ApiController]
    // [Authorize(Roles = "Admin")]
    [Authorize]
    public class TranslateController : ControllerBase
    {
        private readonly ITranslateRepository _translateRepository;
        private readonly IMapper _mapper;
        public TranslateController(IMapper mapper, ITranslateRepository translateRepository)
        {
            _translateRepository = translateRepository;
            _mapper = mapper;
        }
        //[HttpGet]
        //public IActionResult GetMenus()
        //{
        //    var data = _translateRepository.GetMenuNames();
        //    return data.AsObjectResult();
        //}
        //[HttpGet("{Id}")]
        //public IActionResult MenuById(int Id)
        //{
        //    var data = _translateRepository.GetMenuById(Id);
        //    return data.AsObjectResult();
        //}
        //[HttpPost]
        //public IActionResult Save(EditMenuNameRequest editMenu)
        //{
        //    var data = _translateRepository.Save(editMenu);
        //    return data.AsObjectResult();
        //}
        ////[HttpGet]
        ////public IActionResult Modules()
        ////{
        ////    var data = _translateRepository.GetModules();
        ////    return data.AsObjectResult();
        ////}
        //[HttpGet("{Id}")]
        //public IActionResult ModuleById(int Id)
        //{
        //    var data = _translateRepository.GetModuleById(Id);
        //    return data.AsObjectResult();
        //}
        //[HttpPost]
        //public IActionResult SaveModules(SaveModuleRequest saveModule)
        //{
        //    var data = _translateRepository.SaveModul(saveModule);
        //    return data.AsObjectResult();
        //}

        //[HttpGet]
        //public IActionResult Objects()
        //{
        //    var data = _translateRepository.GetObjectNames();
        //    return data.AsObjectResult();
        //}
        //[HttpGet("{Id}")]
        //public IActionResult ObjetcsById(int Id)
        //{
        //    var data = _translateRepository.GetObjectById(Id);
        //    return data.AsObjectResult();
        //}
        //[HttpPost]
        //public IActionResult SaveObject(ObjectRequest objectRequest)
        //{
        //    var data = _translateRepository.SaveObject(objectRequest);
        //    return data.AsObjectResult();
        //}
    }
}
