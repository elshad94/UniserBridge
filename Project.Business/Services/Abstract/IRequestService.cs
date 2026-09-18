using Project.Core.Utilities.Results;
using Project.Entities.Dtos.CancelOrder;
using Project.Entities.Dtos.RequestOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Services.Abstract
{
    public interface IRequestService
    {
        Task<Result> GetFirmRequest(FirmRequestData model);
        Task<Result> GetOrderResponse(RequestTokenData model);
        Task<Result> CancelIntegratedOrder(List<CancelRequestModel> model);
    }
}
