using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

/// <summary>
/// Summary description for StopProjects
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class StopProjects : System.Web.Services.WebService {

    public StopProjects () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public void ProjectActivity(string JSONN)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        string active = "1";
        Context.Response.Write(active);
    }

    [WebMethod]
    public void BOOMDATABASE(string JSONN)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        string active = "1";
        Context.Response.Write(active);
    }
}
