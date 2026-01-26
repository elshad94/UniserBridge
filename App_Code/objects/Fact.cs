using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Fact
/// </summary>
public class Fact
{
    conn con = new conn();
    login lg = new login();
    public List<Ofact> Get_FactList(string year, string month)
    {
        List<Ofact> List_data = new List<Ofact>();
        string query = "";
        query = @"SELECT [FCT_ID]

      ,[FCT_PACKET]
CASE WHEN FCT_WNO IS NULL OR FCT_WNO='' THEN FCT_CNO ELSE FCT_WNO END FCT_NO
      ,[FCT_DATE]
      ,[FCT_CDATE]
      ,[FCT_CTRCODE]
      ,[FCT_STRCODE]
      ,[FCT_OWNER]
      ,[FCT_BEGPOINT]
      ,[FCT_ENDPOINT]
      ,[FCT_DISTANCE]
      ,[FCT_QTY]
      ,[FCT_CALCQTY]
      ,[FCT_ORDRECNO]
      ,[FCT_PRICE]
      ,[FCT_CURR_ID]
      ,[FCT_TOTAL]
      ,[FCT_SUBCODE]
      ,[FCT_NTCCODE]
      ,[FCT_EXP1]
      ,[FCT_EXP2]
      ,[FCT_TYPEID]
      ,[FCT_NOTE]
      ,[FCT_NAME]
      ,[FCT_CODE]
      ,[FCT_POINTID]
,B.PNT_NAME BEGPOINT
,E.PNT_NAME ENDPOINT
     ,SC_VALUE" + lg.Get_Lang() + @" OWNER FROM TBL_FACT
        INNER JOIN TBL_SPECODES SPC ON (FCT_OWNER=SPC.SC_REFID AND SC_TYPE='VGN_OWNERTYPE')
        INNER JOIN TBL_POINTS B ON B.PNT_RECNO=FCT_BEGPOINT
        INNER JOIN TBL_POINTS E ON E.PNT_RECNO=FCT_ENDPOINT
WHERE FCT_STATUS IN (1, 2, 3) and DATEPART(YEAR,FCT_DATE)=@YEAR AND DATEPART(MONTH,FCT_DATE)=@MONTH
   ";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        cmd.Parameters.AddWithValue("@year", year);
        cmd.Parameters.AddWithValue("@month", month);
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_data.Add(new Ofact
            {

                ID = reader["FCT_ID"].ToString(),
                PACKET = reader["FCT_PACKET"].ToString(),
                NO= reader["FCT_NO"].ToString(),
                DATE	= reader["FCT_DATE"].ToString(),
                CDATE = reader["FCT_CDATE"].ToString(),
                CTRCODE = reader["FCT_CTRCODE"].ToString(),
                STRCODE = reader["FCT_STRCODE"].ToString(),
                OWNER = reader["OWNER"].ToString(),
                BEGPOINT = reader["FCT_"].ToString(),
                ENDPOINT = reader["FCT_BEGPOINT"].ToString(),
                DISTANCE = reader["FCT_DISTANCE"].ToString(),
                QTY = reader["FCT_QTY"].ToString(),
                CALCQTY = reader["FCT_CALCQTY"].ToString(),
                ORDRECNO = reader["FCT_ORDRECNO"].ToString(),
                PRICE = reader["FCT_PRICE"].ToString(),
                CCODE = reader["FCT_CCODE"].ToString(),
                TOTAL = reader["FCT_TOTAL"].ToString(),
                SUBCODE = reader["FCT_SUBCODE"].ToString(),
                NTCCODE = reader["FCT_NTCCODE"].ToString(),
                EXP1 = reader["FCT_EXP1"].ToString(),
                EXP2 = reader["FCT_EXP2"].ToString(),
                TYPEID = reader["FCT_TYPEID"].ToString(),
                NOTE = reader["FCT_NOTE"].ToString(),
                NAME = reader["FCT_NAME"].ToString(),
                 CODE = reader["FCT_CODE"].ToString(),
                POINTID = reader["FCT_POINTID"].ToString()

             
            });
        }
        return List_data;
    }
    public int Save_Order(string FCT_PACKET,string FCT_WNO,string FCT_CNO,string FCT_DATE,string FCT_CDATE,string FCT_CTRCODE,string FCT_STRCODE,
                          string FCT_OWNER,string FCT_BEGPOINT,string FCT_ENDPOINT,float FCT_DISTANCE,
                          float FCT_QTY,float FTC_CALCQTY,string FCT_ORDRECNO,float FCT_PRICE, int CURR_ID,
                          float FCT_TOTAL,string FCT_SUBCODE,string FCT_NTCCODE, int STATUS,int FCT_ACTTYPE,int FCT_POINTID,int ID)
    {
       
        string query = "";
        if (ID == -1)
        {
            query = @"INSERT INTO [dbo].[TBL_FACT]
           ( [FCT_PACKET]
               ,FCT_WNO 
            ,FCT_CNO 
      ,[FCT_DATE]
      ,[FCT_CDATE]

           ,[FCT_CTRCODE]
           ,[FCT_STRCODE]
           ,[FCT_OWNER]
           ,[FCT_BEGPOINT]
           ,[FCT_ENDPOINT]
           ,[FCT_DISTANCE]
           ,[FCT_QTY]
           ,[FCT_CALCQTY]
           ,[FCT_ORDRECNO]
           ,[FCT_PRICE]
           ,[FCT_CURR_ID]
           ,[FCT_TOTAL]
           ,[FCT_SUBCODE]
           ,[FCT_NTCCODE]
           ,[FCT_STATUS]
           ,[FCT_ACTTYPE]
           ,[FCT_EXP1]
            ,[FCT_EXP2]
            ,[FCT_TYPEID]
           ,[FCT_NOTE]
           ,[FCT_NAME]
           ,[FCT_CODE]
,FCT_POINTID)
     VALUES
           (@FCT_PACKET
           ,@FCT_WNO 
            ,@FCT_CNO 
            ,@FCT_DATE
            ,@FCT_CDATE
           ,@FCT_CTRCODE
           ,@FCT_STRCODE
           ,@FCT_OWNER
           ,@FCT_BEGPOINT
           ,@FCT_ENDPOINT
           ,@FCT_DISTANCE
           ,@FCT_QTY
           ,@FCT_CALCQTY
           ,@FCT_ORDRECNO
           ,@FCT_PRICE
           ,@FCT_CURR_ID
           ,@FCT_TOTAL
           ,@FCT_SUBCODE
           ,@FCT_NTCCODE
           ,@FCT_STATUS
           ,@FCT_ACTTYPE
            ,@FCT_EXP1
            ,@FCT_EXP2
            ,@FCT_TYPEID
           ,@FCT_NOTE
           ,@FCT_NAME
           ,@FCT_CODE
,@FCT_POINTID)
                        SELECT SCOPE_IDENTITY() as _newid ";
        }
        else
        {
            query = @"UPDATE [dbo].[TBL_FACT]
   SET [FCT_PACKET]=@FCT_PACKET
      ,FCT_WNO=@FCT_WNO 
       ,FCT_CNO=@FCT_CNO
      ,[FCT_DATE]=@FCT_DATE
      ,[FCT_CDATE]=@FCT_CDATE
      ,[FCT_CTRCODE] = @FCT_CTRCODE
      ,[FCT_STRCODE] = @FCT_STRCODE
      ,[FCT_OWNER] = @FCT_OWNER
      ,[FCT_BEGPOINT] = @FCT_BEGPOINT
      ,[FCT_ENDPOINT] = @FCT_ENDPOINT
      ,[FCT_DISTANCE] = @FCT_DISTANCE
      ,[FCT_QTY] = @FCT_QTY
      ,[FCT_CALCQTY] = @FCT_CALCQTY
      ,[FCT_ORDRECNO] = @FCT_ORDRECNO
      ,[FCT_PRICE] = @FCT_PRICE
      ,[FCT_CURR_ID] = @FCT_CURR_ID
      ,[FCT_TOTAL] = @FCT_TOTAL
      ,[FCT_SUBCODE] = @FCT_SUBCODE
      ,[FCT_NTCCODE] = @FCT_NTCCODE
      ,[FCT_STATUS] = @FCT_STATUS
      ,[FCT_ACTTYPE] = @FCT_ACTTYPE
      ,FCT_EXP1=@FCT_EXP1
      ,FCT_EXP2=@FCT_EXP2
      ,FCT_TYPEID=@FCT_TYPEID
      ,FCT_NOTE=@FCT_NOTE
      ,FCT_NAME=@FCT_NAME
      ,FCT_CODE=@FCT_CODE
,FCT_POINTID=@FCT_POINTID
 WHERE FCT_ID=@FCT_ID";
        }
        conn con = new conn();
        using (SqlCommand command = new SqlCommand(query, con.connectionOpen()))
        {
            command.Parameters.AddWithValue("@FCT_ID", ID);
            command.Parameters.AddWithValue("@FCT_PACKET",FCT_PACKET);
            command.Parameters.AddWithValue("@FCT_WNO",FCT_WNO); 
            command.Parameters.AddWithValue("@FCT_CNO",FCT_CNO);
            command.Parameters.AddWithValue("@FCT_DATE",FCT_DATE);
            command.Parameters.AddWithValue("@FCT_CDATE",FCT_CDATE);

            command.Parameters.AddWithValue("@FCT_CTRCODE", FCT_CTRCODE);
            command.Parameters.AddWithValue("@FCT_STRCODE", FCT_STRCODE);
            command.Parameters.AddWithValue("@FCT_OWNER", FCT_OWNER);
            command.Parameters.AddWithValue("@FCT_BEGPOINT", FCT_BEGPOINT);
            command.Parameters.AddWithValue("@FCT_ENDPOINT", FCT_ENDPOINT);
            command.Parameters.AddWithValue("@FCT_DISTANCE", FCT_DISTANCE);
            command.Parameters.AddWithValue("@FCT_QTY", FCT_QTY);
            command.Parameters.AddWithValue("@FTC_CALCQTY", FTC_CALCQTY);
            command.Parameters.AddWithValue("@FCT_ORDRECNO", FCT_ORDRECNO);
            command.Parameters.AddWithValue("@FCT_PRICE", FCT_PRICE);
            command.Parameters.AddWithValue("@CURR_ID", CURR_ID);
            command.Parameters.AddWithValue("@FCT_TOTAL", FCT_TOTAL);
            command.Parameters.AddWithValue("@FCT_SUBCODE", FCT_SUBCODE);
            command.Parameters.AddWithValue("@FCT_NTCCODE", FCT_NTCCODE);
            command.Parameters.AddWithValue("@STATUS", STATUS);
            command.Parameters.AddWithValue("@FCT_ACTTYPE", FCT_ACTTYPE);
            command.Parameters.AddWithValue("@FCT_POINTID", FCT_ACTTYPE);


            System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();
            int FCT_ID = 0;
            if (reader.Read())
            {
                FCT_ID = Convert.ToInt32(reader["_newid"]);
            }
            else
            {
                FCT_ID = ID;
            }
            con.ConnetionClose();
            return FCT_ID;
        }
   
    }

}