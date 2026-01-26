using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for Services
/// </summary>
public class Services
{
    public DataTable Get_ImsartData(string v_sql)
    {
        var webAddr = "https://ady.express/WS_BORDERALL_DATA.asmx/Get_AllData";

        DataTable dt = new DataTable();
        using (var client = new WebClient())
        {
            var values = new NameValueCollection();
            values["v_sql"] = v_sql.ToString();
            var response = client.UploadValues(webAddr, values);
            var responseString = Encoding.UTF8.GetString(response);
            string JsonString = responseString.ToString();
            dt = JsonConvert.DeserializeObject<DataTable>(JsonString);
            return dt;
        }
    }
}