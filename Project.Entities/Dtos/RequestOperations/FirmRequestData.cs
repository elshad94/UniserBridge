using Project.Core.Entities.Models;
using Project.Core.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities.Dtos.RequestOperations
{
    public class FirmRequestData: IMapTo<RequestDetail>
    {
        public int UserId { get; set; }
        public OrderDetail OrderInfo { get; set; }
    }
}
