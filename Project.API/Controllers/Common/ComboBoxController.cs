using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.DataAccess.Repositories.Abstract.Common;

namespace Project.API.Controllers.Common
{
    //test
    [Route("Common/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ComboBoxController : ControllerBase
    {
        private readonly IComboBoxRepository _comboBoxRepository;

        public ComboBoxController(IComboBoxRepository comboBoxRepository)
        {
            _comboBoxRepository = comboBoxRepository;
        }


        //[HttpGet]
        //public IActionResult GetCountries()
        //{
        //    var result = _comboBoxRepository.GetCountries();

        //    return result.AsObjectResult();
        //}

    }
}