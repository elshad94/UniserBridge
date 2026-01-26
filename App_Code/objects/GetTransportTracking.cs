using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UploadStatusModel
/// </summary>
public class GetTransportTracking
{
    public string Operation { get; set; }
    public string OperationSt { get; set; }
    public DateTime OperationDate { get; set; }
    public DateTime? CargoAcceptanceDateTime { get; set; }
    public string DepartureStationCode { get; set; }
    public string DestinationStationCode { get; set; }
    public string DepartureStationCode_Az { get; set; }
    public string DestinationStationCode_Az { get; set; }
    public string QnqCargoCode { get; set; }
    public string EtsnqCargoCode { get; set; }
    public string CargoWeightKq { get; set; }
    public string TransportNumber { get; set; }
    public string WagonNo { get; set; }
    public string ContainerNo { get; set; }
    public string WaybillNo { get; set; }
    public string Expeditor { get; set; }
    public string LoadingSt { get; set; }
    public string DischargeSt { get; set; }
    public string BorderEntrySt { get; set; }
    public string BorderExitSt { get; set; }

}