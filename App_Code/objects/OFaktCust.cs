using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OFaktCust
/// </summary>
public class OFaktCust
{
    private string FCT_SUBCODE;

    public string SUBCODE
    {
        get { return FCT_SUBCODE; }
        set { FCT_SUBCODE = value; }
    }
    private string ORD_DATE;

    public string DATE
    {
        get { return ORD_DATE; }
        set { ORD_DATE = value; }
    }
    private string FCT_DATE;

    public string FAKTDATE
    {
        get { return FCT_DATE; }
        set { FCT_DATE = value; }
    }
    private string FCT_OVERHEAD;

    public string OVERHEAD
    {
        get { return FCT_OVERHEAD; }
        set { FCT_OVERHEAD = value; }
    }
    private string FCT_WAGONNO;

    public string WAGONNO
    {
        get { return FCT_WAGONNO; }
        set { FCT_WAGONNO = value; }
    }
    private double FCT_WEIGHT;

    public double WEIGHT
    {
        get { return FCT_WEIGHT; }
        set { FCT_WEIGHT = value; }
    }
    private double FCT_FERRYS;

    public double FERRYS
    {
        get { return FCT_FERRYS; }
        set { FCT_FERRYS = value; }
    }
    private double FCT_ADYS;

    public double ADYS
    {
        get { return FCT_ADYS; }
        set { FCT_ADYS = value; }
    }
    private double FCT_SECURITYS;

    public double SECURITYS
    {
        get { return FCT_SECURITYS; }
        set { FCT_SECURITYS = value; }
    }
    private double FCT_CASPARS;

    public double CASPARS
    {
        get { return FCT_CASPARS; }
        set { FCT_CASPARS = value; }
    }
    private double FCT_BRIDGES;

    public double BRIDGES
    {
        get { return FCT_BRIDGES; }
        set { FCT_BRIDGES = value; }
    }

    private int count;

    public int Count
    {
        get { return count; }
        set { count = value; }
    }

    private string BEGINPOINT;

    public string Begin_point
    {
        get { return BEGINPOINT; }
        set { BEGINPOINT = value; }
    }

    private string ENDPOINT;

    public string END_point
    {
        get { return ENDPOINT; }
        set { ENDPOINT = value; }
    }

    private string ITEMNAME1;

    public string NAME1
    {
        get { return ITEMNAME1; }
        set { ITEMNAME1 = value; }
    }
}