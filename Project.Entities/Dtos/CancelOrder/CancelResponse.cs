using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities.Dtos.CancelOrder
{
    public class CancelResponse
    {
        public D d { get; set; }
    }

    public class D
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
