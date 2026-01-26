using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OUSERGRID
/// </summary>
public class OUSERGRID
{
    private int ID;

    public int id
    {
        get { return ID; }
        set { ID = value; }
    }
    private string OBJECT;

    public string FIELD
    {
        get { return OBJECT; }
        set { OBJECT = value; }
    }
    private string CAPTION;

    public string HEADER
    {
        get { return CAPTION; }
        set { CAPTION = value; }
    }
    private int GRID_WIDTH;

    public int WIDTH
    {
        get { return GRID_WIDTH; }
        set { GRID_WIDTH = value; }
    }
    private bool STATUS;

    public bool VISIBLE
    {
        get { return STATUS; }
        set { STATUS = value; }
    }

    private string UG_FILTER;

    public string FILTER
    {
        get { return UG_FILTER; }
        set { UG_FILTER = value; }
    }

    private int UG_ORDER;

    public int ORDER
    {
        get { return UG_ORDER; }
        set { UG_ORDER = value; }
    }

    private string LNG_FORMAT;

    public string FORMAT
    {
        get { return LNG_FORMAT; }
        set { LNG_FORMAT = value; }
    }
    
   
}