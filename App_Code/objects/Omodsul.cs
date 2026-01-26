using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Omodul
/// </summary>
public class Omodul
{
    private int APP_ID;

    public int ID
    {
        get { return APP_ID; }
        set { APP_ID = value; }
    }
    private string APP_CODE;

    public string CODE
    {
        get { return APP_CODE; }
        set { APP_CODE = value; }
    }
    private string HEADER;

    public string Header
    {
        get { return HEADER; }
        set { HEADER = value; }
    }
    private string APP_URL;

    public string URL
    {
        get { return APP_URL; }
        set { APP_URL = value; }
    }
    private string APP_IMAGEURL;

    public string IMAGEURL
    {
        get { return APP_IMAGEURL; }
        set { APP_IMAGEURL = value; }
    }
    private string APP_COLOR;

    public string COLOR
    {
        get { return APP_COLOR; }
        set { APP_COLOR = value; }
    }
   
}