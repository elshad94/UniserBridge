using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Ocutomer
/// </summary>
public class Ocutomer
{
    private int C_ID;

    public int ID
    {
        get { return C_ID; }
        set { C_ID = value; }
    }
    private string C_NAME;

    public string NAME
    {
        get { return C_NAME; }
        set { C_NAME = value; }
    }
    private string C_Country;

    public string Country
    {
        get { return C_Country; }
        set { C_Country = value; }
    }
    private string C_CODE;

    public string CODE
    {
        get { return C_CODE; }
        set { C_CODE = value; }
    }
}