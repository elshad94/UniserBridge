using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.DataAccess.Repositories.Abstract.Common;


namespace Project.API.Controllers.Common
{
    [Route("Common/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AutoCompleteController : ControllerBase
    {
        private readonly IAutoCompleteRepository _autocompleteRepository;

        public AutoCompleteController(IAutoCompleteRepository autocompleteRepository)
        {
            _autocompleteRepository = autocompleteRepository;
        }


        /// <summary>
        /// Filter by Country name
        /// </summary>
        //[HttpGet]
        //public IActionResult GetCountries(string filter)
        //{
        //    var result = _autocompleteRepository.GetCountries(filter);

        //    return result.AsObjectResult();
        //}


    }
}
