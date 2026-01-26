using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MainRequestModel
/// </summary>
public class MainRequestModel
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
    public string FIRMCODE { get; set; }
    public int ALLWEIGHT { get; set; }
    public string Note { get; set; }



    public int RType { get; set; }
    public int RTranstype { get; set; }
    public string RSubcode { get; set; }
    public string RSender { get; set; }
    public string RReceiver { get; set; }
    public string RLoading_St { get; set; }
    public string RDestination_St { get; set; }
    public string REntryBorder_St { get; set; }
    public string RExitBorder_St { get; set; }
    public bool REmpty { get; set; }
    public bool RReturn { get; set; }
    public bool RGuide { get; set; }
    public int RGuide_Count { get; set; }
    public string RQNQCODE { get; set; }
    public string RETSNQCODE { get; set; }
    public int RORIGIN { get; set; }
    public string RFIRMCODE { get; set; }
    public int RALLWEIGHT { get; set; }
    public int CONTAINER_CONTCOUNT { get; set; }
}