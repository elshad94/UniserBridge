using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities.Dtos.RequestOperations
{
    public class OrderDetail
    {
        public string ContractApiIntegrationCode { get; set; }
        public string TransportName { get; set; }
        public string TransportAreaName { get; set; }
        public string CountryName { get; set; }
        public string TransportMode { get; set; }

        public string LoadStationCode { get; set; }
        public string DestinationStationCode { get; set; }
        public string BorderInStationCode { get; set; }
        public string BorderOutStationCode { get; set; }
        public string PriceStartStationCode { get; set; }
        public string PriceEndStationCode { get; set; }

        public string QnqCode { get; set; }
        public string EtsnqCode { get; set; }

        public string Sender { get; set; }
        public string SenderVoen { get; set; }
        public string Receiver { get; set; }
        public string ReceiverVoen { get; set; }

        public string FirmCode { get; set; }
        public string KasparCode { get; set; }

        public string ContainerFutName { get; set; }

        public string WgType { get; set; }
        public string WgOwner { get; set; }
        public int WgCount { get; set; }
        public decimal WgTotal { get; set; }
        public int WgEmpty { get; set; }
        public int WgFull { get; set; }

        public string ContainerType { get; set; }
        public string ContainerOwner { get; set; }
        public string ContainerPlatformType { get; set; }
        public string ContainerPlatformOwner { get; set; }

        public int Ctplatform { get; set; }

        public List<TransportDetailModel> TransportDetail { get; set; } = new();
        public List<OrderTypeModel> OrderTypes { get; set; } = new();
        public List<TransportNumberModel> TransportNumber { get; set; } = new();
        public List<NoteModel> Notes { get; set; } = new();
    }

    public class OrderTypeModel
    {
        public string OrderType { get; set; }
    }

    public class TransportNumberModel
    {
        public int Type { get; set; }
        public string No { get; set; }
        public decimal Weight { get; set; }
        public string Overhead { get; set; }
    }

    public class TransportDetailModel
    {
        public string ContainerFutName { get; set; }
        public int Full { get; set; }
        public int Empty { get; set; }
    }


    public class NoteModel
    {
        public string Desc { get; set; }
        public string Overhead { get; set; }
    }

}
