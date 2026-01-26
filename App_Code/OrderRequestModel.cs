using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADYSMART.WebAPI.DBL.Entities.GExpeditor.Order
{
    public class OrderRequestModel
    {
        public int Type { get; set; }
        public int Transtype { get; set; }
        public string Subcode { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public int Loading_St { get; set; }
        public int Destination_St { get; set; }
        public int EntryBorder_St { get; set; }
        public int ExitBorder_St { get; set; }
        public bool Empty { get; set; }
        public bool Return1 { get; set; }
        public bool Guide { get; set; }
        public int Guide_Count { get; set; }
        public string QNQCODE { get; set; }
        public string ETSNQCODE { get; set; }
        public int ORIGIN { get; set; }
        public int Firm_Code { get; set; }
        public int ALLWEIGHT { get; set; }
        public string Note { get; set; }
      
    }
}
