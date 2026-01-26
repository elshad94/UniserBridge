using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UploadStatusModel
/// </summary>
public class BorderDataRequestModel
{
    public List<string> TransportNumbers { get; set; }
    public int TransportType { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }

}