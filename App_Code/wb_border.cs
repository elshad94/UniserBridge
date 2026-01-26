
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using Newtonsoft.Json.Linq;

/// <summary>
/// Summary description for wb_border
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class wb_border : System.Web.Services.WebService {

    public wb_border () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public void Get_Data(string bgrow,string endrow,string Filter,string sort)
    {

        string v_sql = @"Select * FROM (
        select 
        ROW_NUMBER() over (order by " + sort + @") as rownumber,FCT_ID,
        SPC1.SC_VALUE1,isnull(FRM_NAME,N'99-Təyin edilməyib') FRM_NAME,ISNULL(BR.PNT_NAME, '') PNT_NAME,
        FCT_SUBCODE,FCT_OVERHEAD,
        FCT_WAGONNO,FCT_CONTAINERNO,FCT_QNQ,FCT_WEIGHT,FCT_TRAINNO,
        CONVERT(VARCHAR,FCT_IMDATE,103)+' '+CONVERT(varchar,FCT_IMTIME,108) FCT_IMDATE,
        CONVERT(VARCHAR,FCT_EXDATE,103)+' '+CONVERT(varchar,FCT_EXTIME,108) FCT_EXDATE
        from TBL_FACT
        LEFT JOIN TBL_SPECODES SPC1 ON (FCT_type=SPC1.SC_REFID AND SPC1.SC_TYPE='ORD_TYPEACT')
        LEFT JOIN TBL_FIRMS FRM ON (FCT_FIRM=FRM_RECNO) 
        LEFT JOIN TBL_POINTS BR  ON (FCT_BORDER_ID=BR.PNT_RECNO AND BR.PNT_TYPE=1)
        WHERE FCT_TEMPID is null  AND FCT_STATUS<>-1 " + Filter;

        v_sql += @") dt
        where dt.rownumber Between " + bgrow + @" and " + endrow;
        conn con = new conn();
        System.Data.SqlClient.SqlDataReader reader = con.dbrun(v_sql);

        DataTable dt = new DataTable();
        JavaScriptSerializer js = new JavaScriptSerializer();
        dt.Load(reader);
        string data = JsonConvert.SerializeObject(dt);

        reader.Close();
        //  return data;
         Context.Response.Write(data);
    }

    [WebMethod]
    public void Get_Data_count(string Filter)
    {
        conn con = new conn();
        string v_sql = @"select 
        COUNT(*) _count
        from TBL_FACT
        LEFT JOIN TBL_SPECODES SPC1 ON (FCT_type=SPC1.SC_REFID AND SPC1.SC_TYPE='ORD_TYPEACT')
        LEFT JOIN TBL_FIRMS FRM ON (FCT_FIRM=FRM_RECNO) 
        LEFT JOIN TBL_POINTS BR  ON (FCT_BORDER_ID=BR.PNT_RECNO AND BR.PNT_TYPE=1)
        WHERE FCT_TEMPID is null  AND FCT_STATUS<>-1 " + Filter;
        System.Data.SqlClient.SqlDataReader reader = con.dbrun(v_sql);
        DataTable dt = new DataTable();
        JavaScriptSerializer js = new JavaScriptSerializer();
        dt.Load(reader);
        string data = JsonConvert.SerializeObject(dt);
        reader.Close();
        //  return data;
        Context.Response.Write(data);
    }




}
