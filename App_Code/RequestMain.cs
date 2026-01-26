using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RequestMain
/// </summary>
public class RequestMain
{
   
       public int Type { get; set; }
        public int Transtype { get; set; }
        public string Subcode { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Loading_St { get; set; }
        public string Destination_St { get; set; }
        public string EntryBorder_St { get; set; }
        public string ExitBorder_St { get; set; }
        public bool Empty { get; set; }
        public bool Return { get; set; }
        public bool Guide { get; set; }
        public int Guide_Count { get; set; }
        public string QNQCODE { get; set; }
        public string ETSNQCODE { get; set; }
        public int ORIGIN { get; set; }
        public int Firm_Code { get; set; }
        public int ALLWEIGHT { get; set; }
        public string Note { get; set; }

        public string ORD_RECNO { get; set; }
}
