using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Entities
{
    public class ClientList
    {
        public static readonly ClientInfo AGT_Cargo = new ClientInfo
        {
            UserId = 4,
            ContractApiIntegrationCode = "f9c351667ca"
        };
    }

    public class ClientInfo
    {
        public int UserId { get; set; }
        public string ContractApiIntegrationCode { get; set; }
    }
}
