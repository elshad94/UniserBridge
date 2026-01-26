using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TransportListClasses
/// </summary>
public class TransportSaveModel
{
    public int TRN_ID { get; set; }
    public int ORDID { get; set; }
    public int TrTypeId { get; set; }
    public int TRN_FullemptyId { get; set; }
    public string TRN_Fullempty { get; set; }
    public string TrType { get; set; }
    public string Prefix { get; set; }
    public string No { get; set; }
    public int OwnerId { get; set; }
    public string Owner { get; set; }
    public int CategoryId { get; set; }
    public string Category { get; set; }
    public int ConTypeId { get; set; }
    public string ConType { get; set; }

    public int ContCount { get; set; }
    public string TrpList { get; set; }
    public int TNID { get; set; }
}


public class PlatformSaveModel
{
    public int PlatOwner { get; set; }
    public int PlatType { get; set; }
    public int PlatCount { get; set; }
}

public class ContCountModel
{
    public string Id { get; set; }
    public string ContCount { get; set; }
}

public class ListSaveResponseModel
{
    public List<ContCountModel> ContCountInfo { get; set; }
    public int OwnerId { get; set; }
    public int CategoryId { get; set; }
    public int PlatOwner { get; set; }
    public int PlatType { get; set; }
    public int PlatCount { get; set; }

    public string transportOpr { get; set; }
    public string platform { get; set; }
}