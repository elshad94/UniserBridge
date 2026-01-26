using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Master
/// </summary>
public class Master
{
    public List<RequestMain> Main { get; set; }

    public List<RequestContainer> Container { get; set; }
    public List<RequestReturn> Return { get; set; }
    public List<RequestReturnContainer> Return_Container { get; set; }



}