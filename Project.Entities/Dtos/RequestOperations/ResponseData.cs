using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities.Dtos.RequestOperations
{
    public class ResponseData
    {
        public string Token { get; set; }
        public OrderResponseData OrderData { get; set; }
    }
}
