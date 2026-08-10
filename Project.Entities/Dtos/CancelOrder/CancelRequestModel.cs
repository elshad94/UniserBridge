using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities.Dtos.CancelOrder
{
    public class CancelRequestModel
    {
        public string ContractApiIntegrationCode { get; set; }
        public string Token { get; set; }
    }
}
