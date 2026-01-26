using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Report
/// </summary>
public class Report
{
    conn con=new conn();
    public List<OFaktCust> Get_FaktCust( string begindate,string enddate, int client)
    {
        List<OFaktCust> List_data = new List<OFaktCust>();
        string query = "";
        int count = 0;
        query = @"Select  ORD_PODCODE,CONVERT(VARCHAR(10),ORD_FICHEDATE,103) ORD_FICHEDATE,b.PNT_NAME beginpoint,e.PNT_NAME endpoint,STC_NAME,STC_CODE,
        CONVERT(VARCHAR(10), FCT_DATE, 103) FCT_DATE,FCT_OVERHEAD,FCT_WAGONNO,isnull(FCT_WEIGHT,0) FCT_WEIGHT,isnull(FCT_FERRYS,0) FCT_FERRYS, isnull(FCT_ADYS,0) FCT_ADYS,isnull(FCT_SECURITYS,0) FCT_SECURITYS,isnull(FCT_CASPARS,0) FCT_CASPARS,isnull(FCT_BRIDGES,0) FCT_BRIDGES 
        from TBL_FACT
        right join [dbo].[TBL_TRANSORDERS] on [ORD_RECNO]=[FCT_ORDID]
        left join [dbo].[TBL_POINTS] b on b.PNT_RECNO=[ORD_BEGPOINT] 
        left join [dbo].[TBL_POINTS] e on b.PNT_RECNO= [ORD_ENDPOINT]
        left join [dbo].[TBL_STCARDS] on STC_ID=[ORD_LOADSTCARD1]
        where ORD_STATUS in (2,3,6) and ORD_FICHEDATE between CONVERT(datetime, '" + begindate + "', 104)  and CONVERT(datetime, '" + enddate + "', 104) and ORD_CLCRECNO=" + client;
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            count++;
            List_data.Add(new OFaktCust
            {
                Count=count,
                SUBCODE = reader["ORD_PODCODE"].ToString(),
                DATE = reader["ORD_FICHEDATE"].ToString(),
                Begin_point = reader["beginpoint"].ToString(),
                END_point = reader["endpoint"].ToString(),
                NAME1 = reader["STC_CODE"].ToString(),
                FAKTDATE = reader["FCT_DATE"].ToString(),
                OVERHEAD = reader["FCT_OVERHEAD"].ToString(),
                WAGONNO = reader["FCT_WAGONNO"].ToString(),
                WEIGHT = double.Parse(reader["FCT_WEIGHT"].ToString()),
                FERRYS = double.Parse(reader["FCT_FERRYS"].ToString()),
                ADYS = double.Parse(reader["FCT_ADYS"].ToString()),
                SECURITYS = double.Parse(reader["FCT_SECURITYS"].ToString()),
                CASPARS = double.Parse(reader["FCT_CASPARS"].ToString()),
                BRIDGES = double.Parse(reader["FCT_BRIDGES"].ToString())
            });
        }
        return List_data;
    }
}