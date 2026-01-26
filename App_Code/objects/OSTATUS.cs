using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Ospecode
/// </summary>
public class OSTATUS
{
    private int S_ID;

    public int ID
    {
        get { return S_ID; }
        set { S_ID = value; }
    }
    private string S_NAME;

    public string NAME
    {
        get { return S_NAME; }
        set { S_NAME = value; }
    }
    private string S_COLOR;

    public string COLOR
    {
        get { return S_COLOR; }
        set { S_COLOR = value; }
    }
}