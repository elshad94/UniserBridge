using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Business.Services.Abstract;
using Project.Entities.Dtos.CancelOrder;
using Project.Entities.Dtos.RequestOperations;

namespace Project.API.Controllers.RequestOperations
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost]
        public async Task<IActionResult> GetFirmRequest([FromBody] FirmRequestData model)
        {
            var data = await _requestService.GetFirmRequest(model);
            return data.AsObjectResult();

        }
        
        [HttpPost]
        public async Task<IActionResult> GetOrderResponse([FromBody] RequestTokenData model)
        {
            var data = await _requestService.GetOrderResponse(model);
            return data.AsObjectResult();

        }
        [HttpPost]
        public async Task<IActionResult> CancelIntegratedOrder([FromBody] List<CancelRequestModel> model)
        {
            var data = await _requestService.CancelIntegratedOrder(model);
            return data.AsObjectResult();

        }
    }
}
