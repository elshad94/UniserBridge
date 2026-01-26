using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Omenu
/// </summary>
public class Omenu
{
	
    private int OBJ_ID;

    public int ID
    {
        get { return OBJ_ID; }
        set { OBJ_ID = value; }
    }
    private string OBJ_CODE;

    public string CODE
    {
        get { return OBJ_CODE; }
        set { OBJ_CODE = value; }
    }
    private string OBJ_HEADER;

    public string HEADER
    {
        get { return OBJ_HEADER; }
        set { OBJ_HEADER = value; }
    }
    private string OBJ_URL;

    public string URL
    {
        get { return OBJ_URL; }
        set { OBJ_URL = value; }
    }
    private string OBJ_LAVEL;

    public string LAVEL
    {
        get { return OBJ_LAVEL; }
        set { OBJ_LAVEL = value; }
    }
    private string OBJ_ORDER;

    public string ORDER
    {
        get { return OBJ_ORDER; }
        set { OBJ_ORDER = value; }
    }
    private string OBJ_APP_ID;

    public string APP_ID
    {
        get { return OBJ_APP_ID; }
        set { OBJ_APP_ID = value; }
    }
    private int OBJ_TYPE;

    public int TYPE
    {
        get { return OBJ_TYPE; }
        set { OBJ_TYPE = value; }
    }

    private string OBJ_CSS;

    public string CSS
    {
        get { return OBJ_CSS; }
        set { OBJ_CSS = value; }
    }
}