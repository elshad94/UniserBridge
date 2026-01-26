using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities.Dtos.RequestOperations
{
    public class OrderResponseData
    {
        public string Nts { get; set; }
        public string Podcode { get; set; }
        public decimal TotalSale { get; set; }
        public string Currency { get; set; }
    }
}
