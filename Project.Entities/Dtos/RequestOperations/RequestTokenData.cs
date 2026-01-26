using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities.Dtos.RequestOperations
{
    public class RequestTokenData
    {
        public int UserId { get; set; }
        public List<TokenModel> TokenList { get; set; }
    }
}
