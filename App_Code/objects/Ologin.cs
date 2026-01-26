using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Ologin
/// </summary>
public class Ologin
{
    private int session_timeout = 525600;
   // private int session_timeout = 100000;
    public int Session_timeout
    {
        get { return session_timeout; }
    }
}