using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Ocurrencycs
/// </summary>
public class Ocurrency
{
    private int CURR_ID;

    public int ID
    {
        get { return CURR_ID; }
        set { CURR_ID = value; }
    }
    private string CURR_CODE;

    public string CODE
    {
        get { return CURR_CODE; }
        set { CURR_CODE = value; }
    }
    private string CURR_NAME;

    public string NAME
    {
        get { return CURR_NAME; }
        set { CURR_NAME = value; }
    }

}