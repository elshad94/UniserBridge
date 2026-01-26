using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for Order
/// </summary>
public class Order
{
    conn con = new conn();
    login lgn = new login();
    login lg = new login();
    public List<Oorder> Get_OrderList(string ACTTYPE, int id)
    {
        if (ACTTYPE == "5")
        {
            ACTTYPE = "1,2,3,4";
        }
        List<Oorder> List_data = new List<Oorder>();
        string query = "";

        query = @"SELECT DISTINCT ORD_FRAMELESS REF_FICHENO, ORD_ACTTYPE, TRO.ficheno REF_F_NO,
isnull(ORD_BEGPOINT,0)ORD_BEGPOINT,
isnull(ORD_ENDPOINT,0) ORD_ENDPOINT, 
isnull(STC1.STC_ID,-1) STC_ID1,
isnull(STC2.STC_ID,-1) STC_ID2, 
ORD_RECNO, ORD_CLCRECNO, ORD_FIRM,ORD_B_ID, ORD_FICHENO, ORD_FICHEDATE, ORD_PODCODE, ORD_NTS, ORD_TELEGRAM, 
ORD_FCLIENT, isnull(ORD_FPOINT,0) ORD_FPOINT, FPN.PNT_CODE FPNT_CODE, 
ISNULL(FPN.PNT_NAME, '') FPNT_NAME, 
ORD_TCLIENT, isnull(ORD_TPOINT,0) ORD_TPOINT, 
isnull( TPN.PNT_RECNO,0) TPNT_RECNO, 
TPN.PNT_CODE TPNT_CODE, 
ISNULL(TPN.PNT_NAME, '') TPNT_NAME, 
isnull(ORD_LOADSTCARD1,-1) ORD_LOADSTCARD1, 
isnull(STC1.STC_CODE,'') STC_CODE1, STC1.STC_NAME STC_NAME1, 
isnull(ORD_LOADSTCARD2,-1) ORD_LOADSTCARD2, 
isnull(STC2.STC_CODE,'') STC_CODE2, STC2.STC_NAME STC_NAME2, 
isnull(ORD_LOADAMOUNT,0) ORD_LOADAMOUNT, ORD_LOADNOTE, 
isnull(ORD_VGNTRNTYPE,0) ORD_VGNTRNTYPE, 
isnull(ORD_VAGONOWNER,0) ORD_VAGONOWNER, 
isnull(ORD_VAGONOWNERSTN,0) ORD_VAGONOWNERSTN, 
isnull(STN.STN_NAME1,0) STN_NAME, 
isnull(ORD_VAGONTYPE,0) ORD_VAGONTYPE, 
isnull(ORD_VAGONTONNAJ,0) ORD_VAGONTONNAJ, 
isnull(ORD_VGALLTONNAJ,0) ORD_VGALLTONNAJ, 
isnull(ORD_VAGONCOUNT,0) ORD_VAGONCOUNT, 
isnull(ORD_VGALLCOUNT,0) ORD_VGALLCOUNT,  
isnull(ORD_CLC_PTYPE,1) ORD_CLC_PTYPE,
isnull(ORD_DISTANCE,0) ORD_DISTANCE,
isnull(ORD_ISQT,0) ORD_ISQT,
isnull(ORD_CONTAINTYPE,0) ORD_CONTAINTYPE,   
isnull(ORD_CONTAINAMOUNT3,0) ORD_CONTAINAMOUNT3,
isnull(ORD_CONTAINAMOUNT5,0) ORD_CONTAINAMOUNT5,
isnull(ORD_CONTAINAMOUNT10,0) ORD_CONTAINAMOUNT10,
isnull(ORD_CONTAINAMOUNT20,0) ORD_CONTAINAMOUNT20,
isnull(ORD_CONTAINAMOUNT30,0) ORD_CONTAINAMOUNT30,
isnull(ORD_CONTAINAMOUNT40,0) ORD_CONTAINAMOUNT40, 
isnull(ORD_ECONTAINAMOUNT3,0) ORD_ECONTAINAMOUNT3,
isnull(ORD_ECONTAINAMOUNT5,0) ORD_ECONTAINAMOUNT5,
isnull(ORD_ECONTAINAMOUNT10,0) ORD_ECONTAINAMOUNT10,
isnull(ORD_ECONTAINAMOUNT20,0) ORD_ECONTAINAMOUNT20,
isnull(ORD_ECONTAINAMOUNT30,0) ORD_ECONTAINAMOUNT30,
isnull(ORD_ECONTAINAMOUNT40,0) ORD_ECONTAINAMOUNT40, ORD_VAGONNOTE, ORD_NOTE, ORD_STATUS, ORD_STATUSDATE, ORD_SIGNNOTE, ORD_SIGNSEND, ORD_SENDDATE, S_NAME1 ORD_STATUSN,ISNULL(FPN.PNT_NAME, '')+'-'+ISNULL(TPN.PNT_NAME, '') PNTROUTE,ORD_EXPEDITOR,ORD_EXPCLIENT,ORD_CONTWEIGHT,ORD_HEIGHT,ORD_WIDTH,ORD_LENGTH, ORD_ETA FROM TBL_TRANSORDERS
LEFT JOIN (SELECT ORD_FICHENO as ficheno,ORD_RECNO as recno FROM  TBL_TRANSORDERS) TRO on ORD_FRAMELESS=TRO.recno         
LEFT OUTER JOIN TBL_POINTS FPN ON (ORD_FPOINT=FPN.PNT_RECNO)
                LEFT OUTER JOIN TBL_POINTS TPN ON (ORD_TPOINT=TPN.PNT_RECNO)
                LEFT OUTER JOIN TBL_STATIONS STN ON (ORD_VAGONOWNERSTN=STN.STN_RECNO)
                LEFT OUTER JOIN TBL_STCARDS STC1 ON (ORD_LOADSTCARD1=STC1.STC_ID) 
                LEFT OUTER JOIN TBL_STCARDS STC2 ON (ORD_LOADSTCARD2=STC2.STC_ID) 
                --LEFT OUTER JOIN TBL_SPECODES SPC ON (ORD_STATUS=SPC.SC_REFID AND SC_TYPE='ORD_STATUS')
                LEFT join [dbo].[TBL_STATUS] on ORD_STATUS=S_ID
                left join TBL_STATUS_USER on S_ID=SU_S_ID
                WHERE ORD_RECNO=@ID  
                ORDER BY ORD_FICHEDATE";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        cmd.Parameters.AddWithValue("@ID", id);
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {

            List_data.Add(new Oorder
            {
                RECNO = reader["ORD_RECNO"].ToString(),
                REF_FICHENO = reader["REF_F_NO"].ToString(),
                PODCODE = reader["ORD_PODCODE"].ToString(),
                NTS = reader["ORD_NTS"].ToString(),
                FICHENO = reader["ORD_FICHENO"].ToString(),
                FICHEDATE = reader["ORD_FICHEDATE"].ToString(),
                //   FICHEDATE = Convert.ToDateTime(reader["ORD_FICHEDATE"].ToString()).ToString("MM/dd/yyyy HH:mm:ss"),
                PNTROUTE = reader["PNTROUTE"].ToString(),
                NAME = reader["STN_NAME"].ToString(),
                VAGONCOUNT = reader["ORD_VAGONCOUNT"].ToString(),
                NAME1 = reader["STC_NAME1"].ToString(),
                NAME2 = reader["STC_NAME2"].ToString(),
                LOADAMOUNT = reader["ORD_LOADAMOUNT"].ToString(),
                STATUS = int.Parse(reader["ORD_STATUS"].ToString()).ToString("0000"),
                STATUSN = reader["ORD_STATUSN"].ToString(),
                FIRM = reader["ORD_B_ID"].ToString(),
                TELEGRAM = reader["ORD_TELEGRAM"].ToString(),
                FCLIENT = reader["ORD_FCLIENT"].ToString(),
                F_CODE = reader["FPNT_CODE"].ToString(),
                F_NAME = reader["FPNT_NAME"].ToString(),
                TCLIENT = reader["ORD_TCLIENT"].ToString(),
                T_CODE = reader["TPNT_CODE"].ToString(),
                T_NAME = reader["TPNT_NAME"].ToString(),
                S_CODE1 = reader["STC_CODE1"].ToString(),
                S_CODE2 = reader["STC_CODE2"].ToString(),
                LOADNOTE = reader["ORD_LOADNOTE"].ToString(),
                VGNTRNTYPE = reader["ORD_VGNTRNTYPE"].ToString(),
                VAGONOWNER = reader["ORD_VAGONOWNER"].ToString(),
                VAGONOWNERSTN = reader["ORD_VAGONOWNERSTN"].ToString(),
                VAGONTYPE = reader["ORD_VAGONTYPE"].ToString(),
                VAGONTONNAJ = reader["ORD_VAGONTONNAJ"].ToString(),
                VGALLTONNAJ = reader["ORD_VGALLTONNAJ"].ToString(),
                CONTAINAMOUNT3 = reader["ORD_CONTAINAMOUNT3"].ToString(),
                CONTAINAMOUNT5 = reader["ORD_CONTAINAMOUNT5"].ToString(),
                CONTAINAMOUNT10 = reader["ORD_CONTAINAMOUNT10"].ToString(),
                CONTAINAMOUNT20 = reader["ORD_CONTAINAMOUNT20"].ToString(),
                CONTAINAMOUNT30 = reader["ORD_CONTAINAMOUNT30"].ToString(),
                CONTAINAMOUNT40 = reader["ORD_CONTAINAMOUNT40"].ToString(),
                ECONTAINAMOUNT3 = reader["ORD_ECONTAINAMOUNT3"].ToString(),
                ECONTAINAMOUNT5 = reader["ORD_ECONTAINAMOUNT5"].ToString(),
                ECONTAINAMOUNT10 = reader["ORD_ECONTAINAMOUNT10"].ToString(),
                ECONTAINAMOUNT20 = reader["ORD_ECONTAINAMOUNT20"].ToString(),
                ECONTAINAMOUNT30 = reader["ORD_ECONTAINAMOUNT30"].ToString(),
                ECONTAINAMOUNT40 = reader["ORD_ECONTAINAMOUNT40"].ToString(),
                VAGONNOTE = reader["ORD_VAGONNOTE"].ToString(),
                NOTE = reader["ORD_NOTE"].ToString(),
                FPOINT = Convert.ToInt32(reader["ORD_FPOINT"].ToString()),
                TPOINT = Convert.ToInt32(reader["ORD_TPOINT"].ToString()),
                T_RECNO = Convert.ToInt32(reader["TPNT_RECNO"].ToString()),
                S_ID1 = Convert.ToInt32(reader["STC_ID1"].ToString()),
                S_ID2 = Convert.ToInt32(reader["STC_ID2"].ToString()),
                BEGPOINT = reader["ORD_BEGPOINT"].ToString(),
                ENDPOINT = reader["ORD_ENDPOINT"].ToString(),
                VGALLCOUNT = reader["ORD_VGALLCOUNT"].ToString(),
                CONTAINTYPE = reader["ORD_CONTAINTYPE"].ToString(),
                EXPEDITOR = reader["ORD_EXPEDITOR"].ToString(),
                EXPCLIENT = reader["ORD_CLCRECNO"].ToString(),
                CLC_PTYPE = int.Parse(reader["ORD_CLC_PTYPE"].ToString()),
                DISTANCE = int.Parse(reader["ORD_DISTANCE"].ToString()),
                ISQT = bool.Parse(reader["ORD_ISQT"].ToString()),
                ACTTYPE = reader["ORD_ACTTYPE"].ToString(),
                WIDTH = reader["ORD_WIDTH"].ToString(),
                LENGTH = reader["ORD_LENGTH"].ToString(),
                HEIGHT = reader["ORD_HEIGHT"].ToString(),
                CONTWEIGHT = reader["ORD_CONTWEIGHT"].ToString(),
                ORD_ETA = reader["ORD_ETA"].ToString()
            });
        }
        return List_data;
    }
    public SqlDataReader Get_Order(string ACTTYPE)
    {
        string userbr = "";

        if (ACTTYPE == "5")
        {
            ACTTYPE = "1,2,3,4";
        }

        //if (lgn.Get_Usertype() == 18)
        //{
            //userbr += "inner join T_SYS_USERBR on UB_B_ID=B_ID and UB_U_ID=" + lg.Get_userid();
        //}


        List<Oorder> List_data = new List<Oorder>();
        string query = "";
        query = @"SELECT  
 CASE WHEN ORD_VGNTRNTYPE=1 THEN 
TRANSTYPE.SC_VALUE" + lg.Get_Lang() + @"+': '+CAST(ORD_VAGONCOUNT as varchar)+'/'+CAST(ORD_VGALLCOUNT as varchar)+'/'+CAST(ORD_VGALLTONNAJ as varchar)
ELSE
TRANSTYPE.SC_VALUE" + lg.Get_Lang() + @"+': 
        '+CAST((ISNULL(ORD_CONTAINAMOUNT3,0)
               +ISNULL(ORD_CONTAINAMOUNT5,0)
               +ISNULL(ORD_CONTAINAMOUNT10,0)
               +ISNULL(ORD_CONTAINAMOUNT20,0)
               +ISNULL(ORD_CONTAINAMOUNT30,0)
               +ISNULL(ORD_CONTAINAMOUNT40,0)
               +ISNULL(ORD_CONTAINAMOUNT45,0)) as varchar)+'/
        '+CAST((ISNULL(ORD_ECONTAINAMOUNT3,0)
               +ISNULL(ORD_ECONTAINAMOUNT5,0)
               +ISNULL(ORD_ECONTAINAMOUNT10,0)
               +ISNULL(ORD_ECONTAINAMOUNT20,0)
               +ISNULL(ORD_ECONTAINAMOUNT30,0)
               +ISNULL(ORD_ECONTAINAMOUNT40,0)
               +ISNULL(ORD_ECONTAINAMOUNT45,0)) as varchar)+'/
        '+CAST(ORD_VGALLTONNAJ as varchar)END AS VAGONCOUNT,

CASE
	    WHEN ORD_ACTTYPE=1 then  ISNULL(BPN.PNT_CODE+'-', '')+ISNULL(BPN.PNT_NAME, '')
		WHEN ORD_ACTTYPE=2 then  ISNULL(FPN.PNT_CODE+'-', '')+ISNULL(FPN.PNT_NAME, '')
	    WHEN ORD_ACTTYPE IN (3) then  ISNULL(BPN.PNT_CODE+'-', '')+ISNULL(BPN.PNT_NAME, '')
           WHEN ORD_ACTTYPE IN (4) then  ISNULL(FPN.PNT_CODE+'-', '') +ISNULL(FPN.PNT_NAME, '')  end BPOINT,
		 CASE
	    WHEN ORD_ACTTYPE=1 then  ISNULL(TPN.PNT_CODE+'-', '')+ISNULL(TPN.PNT_NAME, '')
		WHEN ORD_ACTTYPE=2 then  ISNULL(EPN.PNT_CODE+'-', '')+ISNULL(EPN.PNT_NAME, '')
	    WHEN ORD_ACTTYPE IN (3) then  ISNULL(EPN.PNT_CODE+'-', '')+ISNULL(EPN.PNT_NAME, '')
            WHEN ORD_ACTTYPE IN (4) then  ISNULL(TPN.PNT_CODE+'-', '') +ISNULL(TPN.PNT_NAME, '') end EPOINT,

B_NAME" + lg.Get_Lang() + @" B_NAME,CLC_ALLNAME  CLCRECNON,ORD_FIRM FIRM,ORD_RECNO RECNO, case when ORD_STATUS=99 then '' else ORD_PODCODE end as PODCODE,  ORD_NTS NTS, ORD_TELEGRAM TELEGRAM, ORD_FICHENO FICHENO, CONVERT(VARCHAR(10), ORD_FICHEDATE, 103) FICHEDATE, ORD_FPOINT, ORD_TPOINT, 
        ISNULL(FPN.PNT_CODE, '')+'-'+ISNULL(TPN.PNT_CODE, '') PNTROUTE, ORD_VAGONOWNERSTN, STN.STN_NAME" + lg.Get_Lang() + @" NAME, ORD_VAGONTYPE, 
        ORD_VAGONCOUNT, ORD_LOADSTCARD1, isnull(STC1.STC_CODE,0) NAME1,isnull(STC2.STC_CODE,0) NAME2, ORD_LOADSTCARD2,ISNULL(ORD_VGALLTONNAJ, 0) ORD_VGALLTONNAJ, ORD_NOTE, ORD_STATUS STATUS, 
        SPC.SC_VALUE" + lg.Get_Lang() + @" ACTTYPE,CASE WHEN ORD_STATUS<>'2' THEN isnull(SUM(EXP_AMOUNT),0) ELSE 0 END  TOTAL ,TRANSTYPE.SC_VALUE" + lg.Get_Lang() + @" TRANSTYPE,
                CASE WHEN ORD_VGNTRNTYPE=1 THEN ORD_VAGONCOUNT ELSE
        (ISNULL(ORD_CONTAINAMOUNT3,0)+ISNULL(ORD_CONTAINAMOUNT5,0)+ISNULL(ORD_CONTAINAMOUNT10,0)+ISNULL(ORD_CONTAINAMOUNT20,0)+
        ISNULL(ORD_CONTAINAMOUNT30,0)+ISNULL(ORD_CONTAINAMOUNT40,0)) END AS VAGONCOUNT,
        CASE WHEN ORD_VGNTRNTYPE=1 THEN ISNULL(ORD_VGALLCOUNT,0) ELSE
        (ISNULL(ORD_ECONTAINAMOUNT3,0)+ISNULL(ORD_ECONTAINAMOUNT5,0)+ISNULL(ORD_ECONTAINAMOUNT10,0)+ISNULL(ORD_ECONTAINAMOUNT20,0)+
        ISNULL(ORD_ECONTAINAMOUNT30,0)+ISNULL(ORD_ECONTAINAMOUNT40,0)) END AS ORD_VGALLCOUNT,
        CASE WHEN count(UF_UFILE)=0 THEN ''
        WHEN count(UF_UFILE)<>0 THEN 'View' END FILE1, 
        CASE WHEN count(UF_UFILE)=0 THEN ''
        WHEN count(UF_UFILE)<>0 THEN UF_FILE END FILE1_,
        S_CODE STATUSN,PA.SC_VALUE" + lg.Get_Lang() + @" ORD_PRICEAREA,S_COLOR,isnull(PAYMENT,0) INVPAYMENT, ORD_FCLIENT,ORD_TCLIENT FROM TBL_TRANSORDERS
                LEFT JOIN T_SYS_BRANCH on ORD_B_ID=B_ID
                LEFT OUTER JOIN TBL_POINTS FPN ON (ORD_FPOINT=FPN.PNT_RECNO)
                LEFT OUTER JOIN TBL_POINTS TPN ON (ORD_TPOINT=TPN.PNT_RECNO)
	            LEFT  JOIN TBL_POINTS BPN ON (ORD_BEGPOINT=BPN.PNT_RECNO)
                LEFT  JOIN TBL_POINTS EPN ON (ORD_ENDPOINT=EPN.PNT_RECNO)
                LEFT OUTER JOIN TBL_STATIONS STN ON (ORD_VAGONOWNERSTN=STN.STN_RECNO)
                LEFT OUTER JOIN TBL_STCARDS STC1 ON (ORD_LOADSTCARD1=STC1.STC_ID) 
                LEFT OUTER JOIN TBL_STCARDS STC2 ON (ORD_LOADSTCARD2=STC2.STC_ID) 
                LEFT OUTER JOIN TBL_SPECODES SPC ON (ORD_ACTTYPE=SPC.SC_REFID AND SC_TYPE='ORD_TYPEACT')
                LEFT  JOIN TBL_SPECODES TRANSTYPE ON (ORD_VGNTRNTYPE=TRANSTYPE.SC_REFID AND TRANSTYPE.SC_TYPE='ORD_TRANSTYPE')
                LEFT join [dbo].[TBL_STATUS] on ORD_STATUS=S_ID
                LEFT join V_INVFILE ZZ ON UF_DOCID=ORD_RECNO        
                INNER join T_SYS_USERCL on UC_CLC_ID=ORD_CLCRECNO
                LEFT  JOIN TBL_CLCARDS CLC ON (ORD_CLCRECNO=CLC.CLC_RECNO)
                LEFT JOIN TBL_TRANSORDEREXP on ORD_RECNO=EXP_ORDRECNO
                LEFT JOIN TBL_SPECODES PA ON isnull(ORD_PRICEAREA,1)=PA.SC_REFID AND PA.SC_TYPE='PRICE_AREA'
                left join V_INVPAYMENT on INVL_ORD_RECNO=ORD_RECNO
                " + userbr + @"
                WHERE ORD_STATUS<>-1 and UC_U_ID=" + lgn.Get_userid() + @" 

		and ORD_ACTTYPE IN (" + ACTTYPE + @")
          GROUP BY PAYMENT,ORD_FIRM,ORD_RECNO,ORD_PODCODE,ORD_NTS,ORD_TELEGRAM, ORD_FICHENO,ORD_FICHEDATE,B_NAME" + lg.Get_Lang() + @",
ORD_FPOINT, 
ORD_TPOINT, 
FPN.PNT_CODE,
TPN.PNT_CODE, 
ORD_VAGONOWNERSTN,
STN.STN_NAME" + lg.Get_Lang() + @" , 
TRANSTYPE.SC_VALUE" + lg.Get_Lang() + @",
ORD_VAGONTYPE, 
ORD_VAGONCOUNT, 
ORD_LOADSTCARD1, 
STC1.STC_CODE, 
ORD_LOADSTCARD2, 
STC2.STC_CODE,
ORD_VGALLTONNAJ, 
ORD_NOTE, CLC_ALLNAME,
ORD_STATUS, 
SPC.SC_VALUE" + lg.Get_Lang() + @" ,
S_CODE,ORD_VGALLCOUNT,
UF_FILE,
ORD_CONTAINAMOUNT3,ORD_CONTAINAMOUNT5,ORD_CONTAINAMOUNT10,ORD_CONTAINAMOUNT20,ORD_CONTAINAMOUNT30,ORD_CONTAINAMOUNT40,ORD_CONTAINAMOUNT45,
        ORD_ECONTAINAMOUNT3,ORD_ECONTAINAMOUNT5,ORD_ECONTAINAMOUNT10,ORD_ECONTAINAMOUNT20,ORD_ECONTAINAMOUNT30,
        ORD_ECONTAINAMOUNT40, ORD_ECONTAINAMOUNT45,ORD_VGNTRNTYPE,CASE
	    WHEN ORD_ACTTYPE=1 then  ISNULL(BPN.PNT_CODE+'-', '')+ISNULL(BPN.PNT_NAME, '')
		WHEN ORD_ACTTYPE=2 then  ISNULL(FPN.PNT_CODE+'-', '')+ISNULL(FPN.PNT_NAME, '')
	    WHEN ORD_ACTTYPE IN (3) then  ISNULL(BPN.PNT_CODE+'-', '')+ISNULL(BPN.PNT_NAME, '')
           WHEN ORD_ACTTYPE IN (4) then  ISNULL(FPN.PNT_CODE+'-', '') +ISNULL(FPN.PNT_NAME, '')  end ,
		 CASE
	    WHEN ORD_ACTTYPE=1 then  ISNULL(TPN.PNT_CODE+'-', '')+ISNULL(TPN.PNT_NAME, '')
		WHEN ORD_ACTTYPE=2 then  ISNULL(EPN.PNT_CODE+'-', '')+ISNULL(EPN.PNT_NAME, '')
	    WHEN ORD_ACTTYPE IN (3) then  ISNULL(EPN.PNT_CODE+'-', '')+ISNULL(EPN.PNT_NAME, '')
            WHEN ORD_ACTTYPE IN (4) then  ISNULL(TPN.PNT_CODE+'-', '') +ISNULL(TPN.PNT_NAME, '') end ,PA.SC_VALUE" + lg.Get_Lang() + @",S_COLOR, ORD_FCLIENT,ORD_TCLIENT
                ORDER BY ORD_RECNO  desc";

        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        return reader;
        //while (reader.Read())
        //{
        //    List_data.Add(new Oorder
        //    {
        //        CLCRECNON = reader["ORD_CLCRECNON"].ToString(),
        //        FILE1=reader["FILE1"].ToString(),
        //        FILE1_=reader["FILE1_"].ToString(),

        //        RECNO = reader["ORD_RECNO"].ToString(),
        //        PODCODE = reader["ORD_PODCODE"].ToString(),
        //        NTS = reader["ORD_NTS"].ToString(),
        //        TELEGRAM = reader["ORD_TELEGRAM"].ToString(),
        //        FICHENO = reader["ORD_FICHENO"].ToString(),
        //        FICHEDATE = reader["ORD_FICHEDATE"].ToString(),
        //        //   FICHEDATE = Convert.ToDateTime(reader["ORD_FICHEDATE"].ToString()).ToString("MM/dd/yyyy HH:mm:ss"),
        //        PNTROUTE = reader["PNTROUTE"].ToString(),
        //        NAME = reader["STN_NAME"].ToString(),
        //       // VAGONCOUNT = reader["ORD_VAGONCOUNT"].ToString() + "/" + reader["ORD_VGALLTONNAJ"].ToString(),
        //        VAGONCOUNT = reader["TRANSTYPE"].ToString() + ": " + reader["VAGONCOUNT"].ToString() + "/" + reader["ORD_VGALLCOUNT"] + "/" + reader["ORD_VGALLTONNAJ"].ToString(),
        //        NAME1 = reader["STC_NAME1"].ToString(),
        //        NAME2 = reader["STC_NAME2"].ToString(),
        //        STATUS = reader["ORD_STATUS"].ToString(),
        //        STATUSN = reader["ORD_STATUSN"].ToString(),
        //        ACTTYPE  = reader["ORD_ACTTYPE"].ToString(),
        //        TOTAL = double.Parse(reader["Total"].ToString())
        //    });
        //}
        //return List_data;
    }

    public SqlDataReader Get_RequestList(string ACTTYPE, string year, string mounth, bool All, string contFilter = "")
    {
        string date = "";
        if (ACTTYPE == "5")
        {
            ACTTYPE = "1,2,3,4";
        }
        if (All == false)
        {
            date = "and   YEar( ORD_FICHEDATE)='" + year + "' and  MONTH( ORD_FICHEDATE)='" + mounth + "' ";
        }
        List<Oorder> List_data = new List<Oorder>();

        string SU_U_TYPE = "";
        string UC_U_ID = "";
        if (lgn.Get_Usertype() == 4 || lgn.Get_Usertype() == 22 || lgn.Get_Usertype() == 3 || lgn.Get_Usertype() == 0 || lgn.Get_Usertype() == 13 || lgn.Get_Usertype() == 15 || lgn.Get_Usertype() == 18 || lgn.Get_Usertype() == 16 || lgn.Get_Usertype() == 21)
        {
            //   SU_U_TYPE += " Left JOIN TBL_STATUS_USER on S_ID=SU_S_ID  ";
            //   UC_U_ID = " Left join T_SYS_USERCL on UC_CLC_ID=ORD_CLCRECNO  ";
        }
        else if (lgn.Get_Usertype() == 5 || lgn.Get_Usertype() == 7 || lgn.Get_Usertype() == 9 || lgn.Get_userid() == 108 || lgn.Get_userid() == 94)
        {
            SU_U_TYPE += @" inner JOIN TBL_STATUS_USER on S_ID=SU_S_ID  and SU_U_TYPE=" + lgn.Get_Usertype();
            //   UC_U_ID = " Left join T_SYS_USERCL on UC_CLC_ID=ORD_CLCRECNO";
        }
        else
        {
            SU_U_TYPE += " inner JOIN TBL_STATUS_USER on S_ID=SU_S_ID  and SU_U_TYPE=" + lgn.Get_Usertype();
            UC_U_ID = " left join T_SYS_USERCL on UC_CLC_ID=ORD_CLCRECNO  and UC_U_ID=" + lgn.Get_userid();//bunu inner ele duzelenden sonra
        }
        string query = "";
        query = @"SELECT ORD_FRAMELESS,ORD_FRAMELESS ORD_REFERANS_NO, TRO.ficheno ORD_REF_FICHENO,  ORD_ETA ETA, ORD_EXPRESS_FICHENO CASORDNO,
'' ALLORDNO, B_NAME" + lg.Get_Lang() + @" B_NAME1,
 CASE WHEN ORD_VGNTRNTYPE=1 THEN 
TRANSTYPE.SC_VALUE" + lg.Get_Lang() + @"+': '+CAST(ORD_VAGONCOUNT as varchar)+'/'+CAST(ORD_VGALLCOUNT as varchar)+'/'+CAST(ORD_VGALLTONNAJ as varchar)
ELSE
TRANSTYPE.SC_VALUE" + lg.Get_Lang() + @"+': 
        '+CAST((ISNULL(ORD_CONTAINAMOUNT3,0)
               +ISNULL(ORD_CONTAINAMOUNT5,0)
               +ISNULL(ORD_CONTAINAMOUNT10,0)
               +ISNULL(ORD_CONTAINAMOUNT20,0)
               +ISNULL(ORD_CONTAINAMOUNT30,0)
               +ISNULL(ORD_CONTAINAMOUNT40,0)
               +ISNULL(ORD_CONTAINAMOUNT45,0) ) as varchar)+'/'+CAST((ISNULL(ORD_ECONTAINAMOUNT3,0)
               +ISNULL(ORD_ECONTAINAMOUNT5,0)
               +ISNULL(ORD_ECONTAINAMOUNT10,0)
               +ISNULL(ORD_ECONTAINAMOUNT20,0)
               +ISNULL(ORD_ECONTAINAMOUNT30,0)
               +ISNULL(ORD_ECONTAINAMOUNT40,0)
               +ISNULL(ORD_ECONTAINAMOUNT45,0)) as varchar)+'/'+CAST(ORD_PLATCOUNT as varchar) 
        END AS VAGONCOUNT,


(case when ch.CT_DOC_ID is not null then 'images/mail.gif' else '' end) VALUE,
'javascript:OnOderdebtListClick('+cast (ORD_CLCRECNO as varchar)+')' as Link,ORD_FIRM FIRM,CLC_RECNO EXPCLIENT,ORD_RECNO RECNO, ORD_CLCRECNO, CLC_ALLNAME CLCRECNON,isnull(ORD_DISTANCE,0) DISTANCE, ORD_PODCODE PODCODE, ORD_TELEGRAM TELEGRAM, ORD_FICHENO FICHENO, CONVERT(VARCHAR(10), ORD_FICHEDATE, 103)  FICHEDATE,ORD_FICHEDATE DATE, ORD_FPOINT, ORD_TPOINT, 
     
       ISNULL(FPN.PNT_CODE+'-', '')+ ISNULL(FPN.PNT_NAME, '') FPOINT1,
	   ISNULL(TPN.PNT_CODE+'-', '')+ ISNULL(TPN.PNT_NAME, '') TPOINT1,
	   --ISNULL(BPN.PNT_CODE+'-', '')+ ISNULL(BPN.PNT_NAME, '') BPOINT,
	   --ISNULL(EPN.PNT_CODE+'-', '')+ ISNULL(EPN.PNT_NAME, '') EPOINT,

 CASE
	    WHEN ORD_ACTTYPE=1 then  ISNULL(BPN.PNT_CODE, '')+'-'+ISNULL(TPN.PNT_CODE, '')
		WHEN ORD_ACTTYPE=2 then  ISNULL(FPN.PNT_CODE, '')+'-'+ISNULL(EPN.PNT_CODE, '')
	    WHEN ORD_ACTTYPE IN (3,4) then  ISNULL(BPN.PNT_CODE, '')+'-'+ISNULL(EPN.PNT_CODE, '') end PNTROUTE,


		 CASE
	    WHEN ORD_ACTTYPE=1 then  ISNULL(BPN.PNT_CODE+'-', '')+ISNULL(BPN.PNT_NAME, '')
		WHEN ORD_ACTTYPE=2 then  ISNULL(FPN.PNT_CODE+'-', '')+ISNULL(FPN.PNT_NAME, '')
	    WHEN ORD_ACTTYPE IN (3) then  ISNULL(BPN.PNT_CODE+'-', '')+ISNULL(BPN.PNT_NAME, '')
           WHEN ORD_ACTTYPE IN (4) then  ISNULL(FPN.PNT_CODE+'-', '') +ISNULL(FPN.PNT_NAME, '')  end BPOINT,
		 CASE
	    WHEN ORD_ACTTYPE=1 then  ISNULL(TPN.PNT_CODE+'-', '')+ISNULL(TPN.PNT_NAME, '')
		WHEN ORD_ACTTYPE=2 then  ISNULL(EPN.PNT_CODE+'-', '')+ISNULL(EPN.PNT_NAME, '')
	    WHEN ORD_ACTTYPE IN (3) then  ISNULL(EPN.PNT_CODE+'-', '')+ISNULL(EPN.PNT_NAME, '')
            WHEN ORD_ACTTYPE IN (4) then  ISNULL(TPN.PNT_CODE+'-', '') +ISNULL(TPN.PNT_NAME, '') end EPOINT,
--geri donush1
		case when ORD_VGALLCOUNT=0 then ''  when ORD_VGALLCOUNT<>0 then (  CASE
	    WHEN ORD_ACTTYPE=1 then  ISNULL(TPN.PNT_CODE+'-', '')+ISNULL(TPN.PNT_NAME, '')
		WHEN ORD_ACTTYPE=2 then  ISNULL(EPN.PNT_CODE+'-', '')+ISNULL(EPN.PNT_NAME, '')
	    WHEN ORD_ACTTYPE IN (3) then  ISNULL(EPN.PNT_CODE+'-', '')+ISNULL(EPN.PNT_NAME, '')
            WHEN ORD_ACTTYPE IN (4) then  ISNULL(TPN.PNT_CODE+'-', '') +ISNULL(TPN.PNT_NAME, '') end) end BPOINT2,
			--geri donush1 end
			--geri donush2
			case when ORD_VGALLCOUNT=0 then ''  when ORD_VGALLCOUNT<>0 then (CASE
	    WHEN ORD_ACTTYPE=1 then  ISNULL(BPN.PNT_CODE+'-', '')+ISNULL(BPN.PNT_NAME, '')
		WHEN ORD_ACTTYPE=2 then  ISNULL(FPN.PNT_CODE+'-', '')+ISNULL(FPN.PNT_NAME, '')
	    WHEN ORD_ACTTYPE IN (3) then  ISNULL(BPN.PNT_CODE+'-', '')+ISNULL(BPN.PNT_NAME, '')
           WHEN ORD_ACTTYPE IN (4) then  ISNULL(FPN.PNT_CODE+'-', '') +ISNULL(FPN.PNT_NAME, '')  end) end EPOINT2,
			--geri donush2 end
        ORD_VAGONOWNERSTN, STN.STN_NAME" + lg.Get_Lang() + @" NAME, ORD_VAGONTYPE, 
        ORD_VAGONCOUNT, ORD_LOADSTCARD1, isnull(STC1.STC_CODE,0) NAME1,isnull(STC1.STC_NAME,0) S_CODE1, 
        ORD_LOADSTCARD2, isnull(STC2.STC_CODE,0) NAME2, ISNULL(ORD_VGALLTONNAJ, 0) ORD_VGALLTONNAJ, 
        ORD_NOTE, ORD_STATUS STATUS, SPC.SC_VALUE" + lg.Get_Lang() + @" ACTTYPE,
        TRANSTYPE.SC_VALUE" + lg.Get_Lang() + @" TRANSTYPE ,  isnull(ORD_STOTAL,0) as TOTAL, S_CODE STATUSN,--_Desc._desc,
        CASE WHEN ORD_VGNTRNTYPE=1 THEN ORD_VAGONCOUNT ELSE
        (ISNULL(ORD_CONTAINAMOUNT3,0)+ISNULL(ORD_CONTAINAMOUNT5,0)+ISNULL(ORD_CONTAINAMOUNT10,0)+ISNULL(ORD_CONTAINAMOUNT20,0)+
        ISNULL(ORD_CONTAINAMOUNT30,0)+ISNULL(ORD_CONTAINAMOUNT40,0)) END AS VAGONCOUNT,
        CASE WHEN ORD_VGNTRNTYPE=1 THEN ISNULL(ORD_VGALLCOUNT,0) ELSE
        (ISNULL(ORD_ECONTAINAMOUNT3,0)+ISNULL(ORD_ECONTAINAMOUNT5,0)+ISNULL(ORD_ECONTAINAMOUNT10,0)+ISNULL(ORD_ECONTAINAMOUNT20,0)+
        ISNULL(ORD_ECONTAINAMOUNT30,0)+ISNULL(ORD_ECONTAINAMOUNT40,0)) END AS ORD_VGALLCOUNT,0  as NOTE,
        U_NAME+' '+U_SURNAME as USERNAME,   isnull([ORD_PTOTAL],0) PTOTAL,isnull(ORD_PROFIT,0) PROFIT,

        isnull(DeletedFile.UF_FILE, '') DeleteFilePath, isnull(DeletedFile.UF_DESC, '') DeleteFileDesc,

        CASE WHEN count(NT.UF_UFILE)=0  THEN ORD_NTS
        WHEN count(NT.UF_UFILE)<>0 THEN   ORD_NTS END NTS,
		CASE WHEN count(TL.UF_UFILE)=0 THEN ORD_TELEGRAM
        WHEN count(TL.UF_UFILE)<>0 THEN ORD_TELEGRAM END ORD_KSPR_NO, 
		CASE WHEN count(NY.UF_UFILE)=0 THEN ORD_NARYAD
        WHEN count(NY.UF_UFILE)<>0 THEN ORD_NARYAD END ORD_NARYAD,

         CASE WHEN count(ZZ.UF_UFILE)=0 THEN ''
        WHEN count(ZZ.UF_UFILE)<>0 THEN 'Invoice' END FILE1,
         CASE WHEN count(ZZ.UF_UFILE)=0 THEN ''
        WHEN count(ZZ.UF_UFILE)<>0 THEN ZZ.UF_FILE END FILE1_ ,
        CASE WHEN count(NT.UF_UFILE)=0 THEN ''
            WHEN count(NT.UF_UFILE)<>0 THEN NT.UF_FILE END FILE2_ ,
        CASE WHEN count(TL.UF_UFILE)=0 THEN ''
            WHEN count(TL.UF_UFILE)<>0 THEN TL.UF_FILE END FILE3_ ,
        CASE WHEN count(NY.UF_UFILE)=0 THEN ''
            WHEN count(NY.UF_UFILE)<>0 THEN NY.UF_FILE END FILENaryad_ ,


T_FAKTDESC FAKTDESC,0 as BORC_ORDER_MM01,
     isnull(T_FAKTTOTAL,0) FAKTTOTAL ,isnull(PAYMENT,0) INVPAYMENT, '0' BORC ,  '0' BORC_ORDER,   '' ORDERLINK,'0' BORC_ORDER_KR , '0' BORC_ORDER_MM ,
    '0' ORDER_FB,PA.SC_VALUE" + lg.Get_Lang() + @" ORD_PRICEAREA ,INV_NO,SPC_C.SC_VALUE1 CLC_PTYPE, ORD_B_ID,
 --  case
   --when  (ORD_BEGPOINT = 1362 or ORD_ENDPOINT = 1362) then 'javascript:OnGetNtsFileCaspianClick('''+cast (ORD_EXPRESS_FICHENO as nvarchar)+''')'
	--else 
'javascript:OnGetNtsFileByAllianceClick('''+cast (ORD_EXPRESS_FICHENO as nvarchar)+''')'
--end 
AllianceOrCaspianOrderNo,
    ORD_LOADNOTE, Cont.ContType, ORD_VAGONNOTE
        FROM TBL_TRANSORDERS
LEFT JOIN (SELECT ORD_FICHENO as ficheno,ORD_RECNO as recno FROM  TBL_TRANSORDERS) TRO on ORD_FRAMELESS=TRO.recno
	    left join V_Chat ch on ch.CT_DOC_ID=ORD_RECNO

        LEFT  JOIN TBL_POINTS FPN ON (ORD_FPOINT=FPN.PNT_RECNO)
        LEFT  JOIN TBL_POINTS TPN ON (ORD_TPOINT=TPN.PNT_RECNO)
	    LEFT  JOIN TBL_POINTS BPN ON (ORD_BEGPOINT=BPN.PNT_RECNO)
        LEFT  JOIN TBL_POINTS EPN ON (ORD_ENDPOINT=EPN.PNT_RECNO)
        LEFT  JOIN TBL_STATIONS STN ON (ORD_VAGONOWNERSTN=STN.STN_RECNO)
        LEFT  JOIN TBL_STCARDS STC1 ON (ORD_LOADSTCARD1=STC1.STC_ID AND STC1.STC_STTYPE=1) 
        LEFT  JOIN TBL_STCARDS STC2 ON (ORD_LOADSTCARD2=STC2.STC_ID AND STC2.STC_STTYPE=2) 
        LEFT  JOIN TBL_SPECODES SPC ON (ORD_ACTTYPE=SPC.SC_REFID AND SC_TYPE='ORD_TYPEACT')
        LEFT  JOIN TBL_SPECODES TRANSTYPE ON (ORD_VGNTRNTYPE=TRANSTYPE.SC_REFID AND TRANSTYPE.SC_TYPE='ORD_TRANSTYPE')
        LEFT JOIN  TBL_STATUS on ORD_STATUS=S_ID
        LEFT  JOIN TBL_CLCARDS CLC ON (ORD_CLCRECNO=CLC.CLC_RECNO)
        LEFT JOIN TBL_SPECODES SPC_C ON (SPC_C.SC_REFID = CLC.CLC_PTYPE AND SPC_C.SC_TYPE='CLC_PTYPE')

	    --LEFT join T_SYS_USER on ORD_U_ID=U_ID
	    LEFT join T_SYS_USER on 
		(CASE 
			WHEN ISNULL(ORD_UPD_U_ID,0) = 0 THEN ORD_U_ID
			ELSE ORD_UPD_U_ID
		END)
		
		=U_ID
        LEFT join TBL_TOTAL on T_ORDRECNO=ORD_RECNO 
		left join V_INVPAYMENT on INVL_ORD_RECNO=ORD_RECNO
        LEFT JOIN TBL_SPECODES PA ON isnull(ORD_PRICEAREA,1)=PA.SC_REFID AND PA.SC_TYPE='PRICE_AREA'
		inner join T_SYS_BRANCH on B_ID=ORD_B_ID 
		--inner join T_SYS_USERBR on UB_B_ID=B_ID and UB_U_ID=" + lg.Get_userid() + @"
        LEFT JOIN TBL_INVOICE ON  ORD_RECNO =INV_ORD_RECNO   and INV_TYPE = 1

        left join (
            Select *from(
                Select ROW_NUMBER() over(partition by TRN_ORDID order by TRN_ORDID) _rownum, TRN_ORDID, CONCAT(
                CASE TRN_TRTYPE WHEN  1 THEN isnull(V_CATEGORY.VC_NAME1,'-') WHEN  2 THEN  G_TYPE.RT_NAME1 END,
                ' / ',
                CASE TRN_TRTYPE WHEN 1 THEN V_TYPE.VT_NAME1 WHEN 2 then SP_CONTYPE.SC_VALUE1 END
                ) as ContType
                from TBL_TRANSPORTLIST
                left join TBL_RTCTYPE G_TYPE on G_TYPE.RT_ID=TRN_TRCAT and G_TYPE.RT_TYPE=2
                left join TBL_VAGONTYPE V_TYPE on V_TYPE.VT_ID=TRN_TYPE and isnull(V_TYPE.VT_STATUS,0) <>-1
                left join TBL_VAGONCATEGORY V_CATEGORY on V_CATEGORY.VC_ID=V_TYPE.VT_C_ID and isnull(V_CATEGORY.VC_STATUS,0) <>-1
                left join TBL_SPECODES SP_CONTYPE ON SP_CONTYPE.SC_REFID = TRN_TYPE AND SP_CONTYPE.SC_TYPE = 'CONT_TYPE'
                where ISNULL(TRN_STATUS,0) <> 0 AND TRN_TRTYPE = 2
                group by TRN_ORDID, CASE TRN_TRTYPE WHEN  1 THEN isnull(V_CATEGORY.VC_NAME1,'-') WHEN  2 THEN  G_TYPE.RT_NAME1 END,
                CASE TRN_TRTYPE WHEN 1 THEN V_TYPE.VT_NAME1 WHEN 2 then SP_CONTYPE.SC_VALUE1 END
			) tbl where _rownum=1
        ) Cont on Cont.TRN_ORDID = ORD_RECNO

        LEFT join V_INVFILE ZZ ON UF_DOCID=ORD_RECNO 
        LEFT join V_NTSFILE NT ON NT.UF_DOCID=ORD_RECNO   
	LEFT JOIN V_TELEGRAMFILE TL ON TL.UF_DOCID=ORD_RECNO  
        LEFT JOIN V_NARYADFILE NY ON NY.UF_DOCID=ORD_RECNO 
        LEFT JOIN TBL_UPFILES DeletedFile ON DeletedFile.UF_DOCID=ORD_RECNO AND DeletedFile.UF_APP='DeletedOrderFile' AND isnull(DeletedFile.UF_STATUS, 0)<>-1
        " + SU_U_TYPE + @"
        " + UC_U_ID + @"

        WHERE ORD_ACTTYPE IN (" + ACTTYPE + @") " + date + contFilter + @" 
        group by ORD_FRAMELESS, FICHENO, ORD_ETA, ORD_EXPRESS_FICHENO,B_NAME" + lg.Get_Lang() + @",(case when ch.CT_DOC_ID is not null then 'images/mail.gif' else '' end),BPN.PNT_CODE,EPN.PNT_CODE,BPN.PNT_NAME,EPN.PNT_NAME,TPN.PNT_NAME,FPN.PNT_NAME, ORD_ACTTYPE, ORD_FIRM,CLC_RECNO,ORD_RECNO, ORD_CLCRECNO, CLC_ALLNAME ,ORD_DISTANCE, ORD_PODCODE, ORD_NTS, ORD_TELEGRAM, ORD_NARYAD, ORD_FICHENO, ORD_FICHEDATE, ORD_FPOINT, ORD_TPOINT, 
        ORD_STOTAL,ORD_PTOTAL,ORD_PROFIT,FPN.PNT_CODE,TPN.PNT_CODE, ORD_VAGONOWNERSTN, STN.STN_NAME" + lg.Get_Lang() + @" , ORD_VAGONTYPE,-- _Desc._desc,
        ORD_VGALLCOUNT,ORD_VAGONCOUNT, ORD_LOADSTCARD1, STC1.STC_CODE , STC1.STC_NAME ,ORD_LOADSTCARD2, STC2.STC_CODE , ORD_VGALLTONNAJ, ORD_PLATCOUNT, ORD_NOTE, ORD_STATUS, TRANSTYPE.SC_VALUE" + lg.Get_Lang() + @" ,
        SPC.SC_VALUE" + lg.Get_Lang() + @" ,ORD_CONTAINAMOUNT3,ORD_BEGPOINT,ORD_ENDPOINT,ORD_CONTAINAMOUNT5,ORD_CONTAINAMOUNT10,ORD_CONTAINAMOUNT20,ORD_CONTAINAMOUNT30,ORD_CONTAINAMOUNT40,ORD_CONTAINAMOUNT45,
        ORD_ECONTAINAMOUNT3,ORD_ECONTAINAMOUNT5,ORD_ECONTAINAMOUNT10,ORD_ECONTAINAMOUNT20,ORD_ECONTAINAMOUNT30,ORD_ECONTAINAMOUNT40,ORD_ECONTAINAMOUNT45,ORD_VGNTRNTYPE,
        S_CODE,U_NAME,U_SURNAME,ZZ.UF_FILE, NT.UF_FILE, TL.UF_FILE,NY.UF_FILE, DeletedFile.UF_FILE, DeletedFile.UF_DESC, T_FAKTDESC,T_FAKTTOTAL,PAYMENT,PA.SC_VALUE" + lg.Get_Lang() + @",INV_NO,SPC_C.SC_VALUE1, ORD_B_ID, ORD_LOADNOTE, Cont.ContType, ORD_VAGONNOTE
        ORDER BY ORD_RECNO desc";

        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        return reader;

        //while (reader.Read())
        //{
        //    List_data.Add(new Oorder
        //    {
        //        EXPCLIENT = reader["CLC_RECNO"].ToString(),
        //        FIRM = reader["ORD_FIRM"].ToString(),
        //        RECNO = reader["ORD_RECNO"].ToString(),
        //        CLCRECNON = reader["ORD_CLCRECNON"].ToString(),
        //        FILE1 = reader["FILE1"].ToString(),
        //        FILE1_ = reader["FILE1_"].ToString(),
        //        PODCODE = reader["ORD_PODCODE"].ToString(),
        //        NTS = reader["ORD_NTS"].ToString(),
        //        TELEGRAM = reader["ORD_TELEGRAM"].ToString(),
        //        FICHENO = reader["ORD_FICHENO"].ToString(),
        //        FICHEDATE = reader["ORD_FICHEDATE"].ToString(),
        //        DATE = DateTime.Parse(reader["_DATE"].ToString()),
        //        PNTROUTE = reader["PNTROUTE"].ToString(),
        //        NAME = reader["STN_NAME"].ToString(),
        //        VAGONCOUNT = reader["TRANSTYPE"].ToString() + ": " + reader["VAGONCOUNT"].ToString() + "/" + reader["ORD_VGALLCOUNT"] + "/" + reader["ORD_VGALLTONNAJ"].ToString(),
        //        NAME1 = reader["STC_NAME1"].ToString(),
        //        S_CODE1 = reader["STC_CODE1"].ToString(),
        //        NAME2 = reader["STC_NAME2"].ToString(),
        //        STATUS = reader["ORD_STATUS"].ToString(),
        //        STATUSN = reader["ORD_STATUSN"].ToString(),
        //        ACTTYPE = reader["ORD_ACTTYPE"].ToString(),
        //        TOTAL = double.Parse(reader["Total"].ToString()),
        //        PTOTAL = double.Parse(reader["PTOTAL"].ToString()),
        //        PROFIT = double.Parse(reader["PROFIT"].ToString()),
        //        NOTE = reader["EXP_MANUAL"].ToString(),
        //        DISTANCE = int.Parse(reader["ORD_DISTANCE"].ToString()),
        //        USERNAME = reader["USERNAME"].ToString(),
        //        FPOINT1 = reader["FPOINT"].ToString(),
        //        TPOINT1 = reader["TPOINT"].ToString(),
        //        BPOINT = reader["PNTROUTE_BEG"].ToString(),
        //        EPOINT = reader["PNTROUTE_END"].ToString(),
        //        FAKTTOTAL = double.Parse(reader["T_FAKTTOTAL"].ToString()),
        //        FAKTDESC = reader["T_FAKTDESC"].ToString(),
        //        INVPAYMENT = double.Parse(reader["PAYMENT"].ToString()),
        //        VALUE = reader["_image"].ToString()
        //    });
        //}
        //return List_data;

    }

    public SqlDataReader Get_RequestList_AZAD(string ACTTYPE, string year, string mounth, bool All)
    {
        string date = "";
        if (ACTTYPE == "5")
        {
            ACTTYPE = "1,2,3,4";
        }
        if (All == false)
        {
            date = "and   YEar( ORD_FICHEDATE)='" + year + "' and  MONTH( ORD_FICHEDATE)='" + mounth + "' ";
        }
        List<Oorder> List_data = new List<Oorder>();

        string SU_U_TYPE = "";
        string UC_U_ID = "";
        if (lgn.Get_Usertype() == 4 || lgn.Get_Usertype() == 22 || lgn.Get_Usertype() == 3 || lgn.Get_Usertype() == 0 || lgn.Get_Usertype() == 13 || lgn.Get_Usertype() == 15 || lgn.Get_Usertype() == 18 || lgn.Get_Usertype() == 16)
        {
            //    SU_U_TYPE += " Left JOIN TBL_STATUS_USER on S_ID=SU_S_ID  ";
            //    UC_U_ID = " Left join T_SYS_USERCL on UC_CLC_ID=ORD_CLCRECNO  ";
        }
        else if (lgn.Get_Usertype() == 5 || lgn.Get_Usertype() == 7 || lgn.Get_Usertype() == 9 || lgn.Get_userid() == 108 || lgn.Get_userid() == 94)
        {
            SU_U_TYPE += @" inner JOIN TBL_STATUS_USER on S_ID=SU_S_ID  and SU_U_TYPE=" + lgn.Get_Usertype();
            //     UC_U_ID = " Left join T_SYS_USERCL on UC_CLC_ID=ORD_CLCRECNO";
        }
        else
        {
            SU_U_TYPE += " inner JOIN TBL_STATUS_USER on S_ID=SU_S_ID  and SU_U_TYPE=" + lgn.Get_Usertype();
            UC_U_ID = " inner join T_SYS_USERCL on UC_CLC_ID=ORD_CLCRECNO  and UC_U_ID=" + lgn.Get_userid();
        }

        string query = "";
        query = @"SELECT B_NAME1,(case when ch.CT_DOC_ID is not null then 'images/mail.gif' else '' end) VALUE,
 CASE WHEN ORD_VGNTRNTYPE=1 THEN 
TRANSTYPE.SC_VALUE" + lg.Get_Lang() + @"+': '+CAST(ORD_VAGONCOUNT as varchar)+'/'+CAST(ORD_VGALLCOUNT as varchar)+'/'+CAST(ORD_VGALLTONNAJ as varchar)
ELSE
TRANSTYPE.SC_VALUE" + lg.Get_Lang() + @"+': '+CAST((ISNULL(ORD_CONTAINAMOUNT3,0)+ISNULL(ORD_CONTAINAMOUNT5,0)+ISNULL(ORD_CONTAINAMOUNT10,0)+ISNULL(ORD_CONTAINAMOUNT20,0)+
        ISNULL(ORD_CONTAINAMOUNT30,0)+ISNULL(ORD_CONTAINAMOUNT40,0)) as varchar)+'/'+CAST((ISNULL(ORD_ECONTAINAMOUNT3,0)+ISNULL(ORD_ECONTAINAMOUNT5,0)+ISNULL(ORD_ECONTAINAMOUNT10,0)+ISNULL(ORD_ECONTAINAMOUNT20,0)+
        ISNULL(ORD_ECONTAINAMOUNT30,0)+ISNULL(ORD_ECONTAINAMOUNT40,0)) as varchar)+'/'+CAST(ORD_VGALLTONNAJ as varchar)




         END AS VAGONCOUNT,
'javascript:OnOderdebtListClick('+cast (ORD_CLCRECNO as varchar)+')' as ORDERLINK,ORD_FIRM FIRM,CLC_RECNO EXPCLIENT,ORD_RECNO RECNO, ORD_CLCRECNO ORD_CLCRECNON, CLC_ALLNAME CLCRECNON,isnull(ORD_DISTANCE,0) DISTANCE, ORD_PODCODE PODCODE, ORD_NTS NTS, ORD_TELEGRAM TELEGRAM, ORD_FICHENO FICHENO, CONVERT(VARCHAR(10), ORD_FICHEDATE, 103) FICHEDATE,ORD_FICHEDATE DATE, ORD_FPOINT, ORD_TPOINT, 
       
       ISNULL(FPN.PNT_CODE+'-', '')+ ISNULL(FPN.PNT_NAME, '') FPOINT1,
	   ISNULL(TPN.PNT_CODE+'-', '')+ ISNULL(TPN.PNT_NAME, '') TPOINT1,

 CASE
	    WHEN ORD_ACTTYPE=1 then  ISNULL(BPN.PNT_CODE, '')+'-'+ISNULL(TPN.PNT_CODE, '')
		WHEN ORD_ACTTYPE=2 then  ISNULL(FPN.PNT_CODE, '')+'-'+ISNULL(EPN.PNT_CODE, '')
	    WHEN ORD_ACTTYPE IN (3,4) then  ISNULL(BPN.PNT_CODE, '')+'-'+ISNULL(EPN.PNT_CODE, '') end PNTROUTE,

		 CASE
	    WHEN ORD_ACTTYPE=1 then  ISNULL(BPN.PNT_CODE+'-', '')+ISNULL(BPN.PNT_NAME, '')
		WHEN ORD_ACTTYPE=2 then  ISNULL(FPN.PNT_CODE+'-', '')+ISNULL(FPN.PNT_NAME, '')
	    WHEN ORD_ACTTYPE IN (3) then  ISNULL(BPN.PNT_CODE+'-', '')+ISNULL(BPN.PNT_NAME, '')
           WHEN ORD_ACTTYPE IN (4) then  ISNULL(FPN.PNT_CODE+'-', '') +ISNULL(FPN.PNT_NAME, '')  end BPOINT,
		 CASE
	    WHEN ORD_ACTTYPE=1 then  ISNULL(TPN.PNT_CODE+'-', '')+ISNULL(TPN.PNT_NAME, '')
		WHEN ORD_ACTTYPE=2 then  ISNULL(EPN.PNT_CODE+'-', '')+ISNULL(EPN.PNT_NAME, '')
	    WHEN ORD_ACTTYPE IN (3) then  ISNULL(EPN.PNT_CODE+'-', '')+ISNULL(EPN.PNT_NAME, '')
            WHEN ORD_ACTTYPE IN (4) then  ISNULL(TPN.PNT_CODE+'-', '') +ISNULL(TPN.PNT_NAME, '') end EPOINT,

        ORD_VAGONOWNERSTN, STN.STN_NAME" + lg.Get_Lang() + @" NAME, ORD_VAGONTYPE, 
        ORD_VAGONCOUNT, ORD_LOADSTCARD1, isnull(STC1.STC_CODE,0) NAME1,isnull(STC1.STC_NAME,0) S_CODE1, ORD_LOADSTCARD2, isnull(STC2.STC_CODE,0) NAME2, ISNULL(ORD_VGALLTONNAJ, 0) ORD_VGALLTONNAJ, 
        ORD_NOTE, ORD_STATUS STATUS, 
        SPC.SC_VALUE" + lg.Get_Lang() + @" ACTTYPE,TRANSTYPE.SC_VALUE" + lg.Get_Lang() + @" TRANSTYPE ,  isnull(ORD_STOTAL,0) as TOTAL, S_CODE STATUSN,--_Desc._desc,
        CASE WHEN ORD_VGNTRNTYPE=1 THEN ORD_VAGONCOUNT ELSE
        (ISNULL(ORD_CONTAINAMOUNT3,0)+ISNULL(ORD_CONTAINAMOUNT5,0)+ISNULL(ORD_CONTAINAMOUNT10,0)+ISNULL(ORD_CONTAINAMOUNT20,0)+
        ISNULL(ORD_CONTAINAMOUNT30,0)+ISNULL(ORD_CONTAINAMOUNT40,0)) END AS VAGONCOUNT,
        CASE WHEN ORD_VGNTRNTYPE=1 THEN ISNULL(ORD_VGALLCOUNT,0) ELSE
        (ISNULL(ORD_ECONTAINAMOUNT3,0)+ISNULL(ORD_ECONTAINAMOUNT5,0)+ISNULL(ORD_ECONTAINAMOUNT10,0)+ISNULL(ORD_ECONTAINAMOUNT20,0)+
        ISNULL(ORD_ECONTAINAMOUNT30,0)+ISNULL(ORD_ECONTAINAMOUNT40,0)) END AS ORD_VGALLCOUNT,0  as NOTE,
        U_NAME+' '+U_SURNAME as USERNAME,   isnull([ORD_PTOTAL],0) PTOTAL,isnull(ORD_PROFIT,0) PROFIT,
         CASE WHEN count(UF_UFILE)=0 THEN ''
                WHEN count(UF_UFILE)<>0 THEN 'Invoice' END FILE1,
         CASE WHEN count(UF_UFILE)=0 THEN ''
                WHEN count(UF_UFILE)<>0 THEN UF_FILE END FILE1_ ,Round(isnull(B.AMOUNT,0),2)   BORC ,Round(isnull(BA.AMOUNT ,0),2)   BORC_ORDER ,Round(isnull(KR.AMOUNT ,0),2)   BORC_ORDER_KR,Round(isnull(MM01.AMOUNT ,0),2)   BORC_ORDER_MM01,
Round(isnull(MM.AMOUNT ,0),2)   BORC_ORDER_MM,Round(isnull(FB.AMOUNT ,0),2) ORDER_FB,T_FAKTDESC FAKTDESC,
  isnull(T_FAKTTOTAL,0) FAKTTOTAL,isnull(PAYMENT,0) INVPAYMENT    
       -- ,_TYPE+':'+CAST(_FULL as varchar)+'/'+CAST(_EMPTY as varchar)+'/'+CAST(_WEIGHT as varchar) AS FAKT_DESC,isnull(FAKT_AMOUNT,0) FAKT_AMOUNT,isnull(PAYMENT,0) INVPAYMENT       
        FROM TBL_TRANSORDERS
       left join V_Chat ch on ch.CT_DOC_ID=ORD_RECNO
       LEFT join V_INVFILE ZZ ON UF_DOCID=ORD_RECNO             
        LEFT  JOIN TBL_POINTS FPN ON (ORD_FPOINT=FPN.PNT_RECNO)
        LEFT  JOIN TBL_POINTS TPN ON (ORD_TPOINT=TPN.PNT_RECNO)
	    LEFT  JOIN TBL_POINTS BPN ON (ORD_BEGPOINT=BPN.PNT_RECNO)
        LEFT  JOIN TBL_POINTS EPN ON (ORD_ENDPOINT=EPN.PNT_RECNO)
        LEFT  JOIN TBL_STATIONS STN ON (ORD_VAGONOWNERSTN=STN.STN_RECNO)
        LEFT  JOIN TBL_STCARDS STC1 ON (ORD_LOADSTCARD1=STC1.STC_ID AND STC1.STC_STTYPE=1) 
        LEFT  JOIN TBL_STCARDS STC2 ON (ORD_LOADSTCARD2=STC2.STC_ID AND STC2.STC_STTYPE=2) 
        LEFT  JOIN TBL_SPECODES SPC ON (ORD_ACTTYPE=SPC.SC_REFID AND SC_TYPE='ORD_TYPEACT')
        LEFT  JOIN TBL_SPECODES TRANSTYPE ON (ORD_VGNTRNTYPE=TRANSTYPE.SC_REFID AND TRANSTYPE.SC_TYPE='ORD_TRANSTYPE')
        LEFT JOIN  TBL_STATUS on ORD_STATUS=S_ID
        LEFT  JOIN TBL_CLCARDS CLC ON (ORD_CLCRECNO=CLC.CLC_RECNO)
        " + SU_U_TYPE + @"
        " + UC_U_ID + @"
	    LEFT join T_SYS_USER on ORD_U_ID=U_ID
  		left join V_BORC B ON ORD_CLCRECNO=B.FCT_CLC_RECNO
		left join V_BORC_ORDER BA ON ORD_CLCRECNO=BA.CLCRECNO
		left join V_BORC_ORDER_KR KR ON ORD_CLCRECNO=KR.CLCRECNO
		left join V_BORC_ORDER_MM MM ON ORD_CLCRECNO=MM.CLCRECNO 
        left join V_ORDER_AMOUNT_01 MM01 ON ORD_CLCRECNO=MM01.CLCRECNO
		left join V_BORC_ORDER_FAKT FB ON ORD_CLCRECNO=FB.CLCRECNO 
        LEFT join TBL_TOTAL on T_ORDRECNO=ORD_RECNO 
		left join V_INVPAYMENT on INVL_ORD_RECNO=ORD_RECNO
        inner join T_SYS_BRANCH on B_ID=ORD_B_ID 
		inner join T_SYS_USERBR on UB_B_ID=B_ID and UB_U_ID=" + lg.Get_userid() + @"
        WHERE ORD_ACTTYPE IN (" + ACTTYPE + @") " + date + @"
        group by B_NAME1,BPN.PNT_CODE,EPN.PNT_CODE,BPN.PNT_NAME,EPN.PNT_NAME,TPN.PNT_NAME,FPN.PNT_NAME, ORD_ACTTYPE, ORD_FIRM,CLC_RECNO,ORD_RECNO, ORD_CLCRECNO, CLC_ALLNAME ,ORD_DISTANCE, ORD_PODCODE, ORD_NTS, ORD_TELEGRAM, ORD_FICHENO, ORD_FICHEDATE, ORD_FPOINT, ORD_TPOINT, 
        ORD_STOTAL,ORD_PTOTAL,ORD_PROFIT,FPN.PNT_CODE,TPN.PNT_CODE, ORD_VAGONOWNERSTN, STN.STN_NAME" + lg.Get_Lang() + @" , ORD_VAGONTYPE,-- _Desc._desc,
        ORD_VGALLCOUNT,ORD_VAGONCOUNT, ORD_LOADSTCARD1, STC1.STC_CODE , STC1.STC_NAME ,ORD_LOADSTCARD2, STC2.STC_CODE , ORD_VGALLTONNAJ, ORD_NOTE, ORD_STATUS, TRANSTYPE.SC_VALUE" + lg.Get_Lang() + @" ,
        SPC.SC_VALUE" + lg.Get_Lang() + @" ,ORD_CONTAINAMOUNT3,ORD_CONTAINAMOUNT5,ORD_CONTAINAMOUNT10,ORD_CONTAINAMOUNT20,ORD_CONTAINAMOUNT30,ORD_CONTAINAMOUNT40,(case when ch.CT_DOC_ID is not null then 'images/mail.gif' else '' end),
        ORD_ECONTAINAMOUNT3,ORD_ECONTAINAMOUNT5,ORD_ECONTAINAMOUNT10,ORD_ECONTAINAMOUNT20,ORD_ECONTAINAMOUNT30,ORD_ECONTAINAMOUNT40,ORD_VGNTRNTYPE,S_CODE,U_NAME,U_SURNAME,UF_FILE,B.AMOUNT,BA.AMOUNT,KR.AMOUNT,MM.AMOUNT,FB.AMOUNT, T_FAKTDESC,T_FAKTTOTAL,PAYMENT,MM01.AMOUNT 
       -- ,_TYPE+':'+CAST(_FULL as varchar)+'/'+CAST(_EMPTY as varchar)+'/'+CAST(_WEIGHT as varchar),FAKT_AMOUNT,PAYMENT 
        ORDER BY ORD_RECNO desc";

        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        return reader;
        //while (reader.Read())
        //{
        //    List_data.Add(new Oorder
        //    {
        //    //    EXPCLIENT = reader["CLC_RECNO"].ToString(),
        //     //   FIRM = reader["ORD_FIRM"].ToString(),
        //     //   RECNO = reader["ORD_RECNO"].ToString(),
        //    //    CLCRECNON = reader["ORD_CLCRECNON"].ToString(),
        //    //    FILE1 = reader["FILE1"].ToString(),
        //    //    FILE1_ = reader["FILE1_"].ToString(),

        //    //    PODCODE = reader["ORD_PODCODE"].ToString(),
        //   //     NTS = reader["ORD_NTS"].ToString(),
        //   //     TELEGRAM = reader["ORD_TELEGRAM"].ToString(),
        //   //     FICHENO = reader["ORD_FICHENO"].ToString(),
        //   //     FICHEDATE = reader["ORD_FICHEDATE"].ToString(),
        //   //     DATE = DateTime.Parse(reader["_DATE"].ToString()),
        //   //     PNTROUTE = reader["PNTROUTE"].ToString(),
        //  //      NAME = reader["STN_NAME"].ToString(),
        //        VAGONCOUNT = reader["TRANSTYPE"].ToString() + ": " + reader["VAGONCOUNT"].ToString() + "/" + reader["ORD_VGALLCOUNT"] + "/" + reader["ORD_VGALLTONNAJ"].ToString(),
        //    //    NAME1 = reader["STC_NAME1"].ToString(),
        //    //    S_CODE1 = reader["STC_CODE1"].ToString(),

        //     //   NAME2 = reader["STC_NAME2"].ToString(),
        //    //    STATUS = reader["ORD_STATUS"].ToString(),
        //    //    STATUSN = reader["ORD_STATUSN"].ToString(),
        //    //    ACTTYPE = reader["ORD_ACTTYPE"].ToString(),
        //     //   TOTAL = double.Parse(reader["Total"].ToString()),
        //    //    PTOTAL = double.Parse(reader["PTOTAL"].ToString()),
        //    //    PROFIT = double.Parse(reader["PROFIT"].ToString()),
        //     //   NOTE = reader["EXP_MANUAL"].ToString(),
        //      //  DISTANCE = int.Parse(reader["ORD_DISTANCE"].ToString()),
        //      //  USERNAME = reader["USERNAME"].ToString(),
        //      //  BORC = double.Parse(reader["BORC"].ToString()),
        //    //    BORC_ORDER = double.Parse(reader["BORC_ORDER"].ToString()),
        //   //     ORDERLINK = reader["Link"].ToString(),
        //     //   BORC_ORDER_KR = double.Parse(reader["BORC_ORDER_KR"].ToString()),
        //     //   BORC_ORDER_MM = double.Parse(reader["BORC_ORDER_MM"].ToString()),
        //     //   ORDER_FB = double.Parse(reader["AMOUNT_FB"].ToString()),
        //    //	FPOINT1=reader["FPOINT"].ToString(),
        //    //    TPOINT1=reader["TPOINT"].ToString(),
        //    //    BPOINT=reader["PNTROUTE_BEG"].ToString(),
        //    //    EPOINT=reader["PNTROUTE_END"].ToString(),
        //        // FAKTTOTAL = 0,
        //        // FAKTDESC = "",
        //        // INVPAYMENT = 0
        //     //   FAKTTOTAL = double.Parse(reader["T_FAKTTOTAL"].ToString()),
        //     //   FAKTDESC = reader["T_FAKTDESC"].ToString(),
        //        INVPAYMENT = double.Parse(reader["PAYMENT"].ToString())

        //    });
        //}
        //return List_data;
    }


    public List<Oorder> Get_RequestFinansList(string ACTTYPE)
    {
        string mmcjoin = "";
        string mmc_filter = "SU_U_TYPE =" + lgn.Get_Usertype() + @"and ";
        if (lgn.Get_Usertype() == 18)
        {
            mmcjoin = @" left join TBL_CONTRACT on CLC_RECNO = CT_CLC_RENO ";
            mmc_filter = "CT_FRM_ID not in (12,13) and CLC_FIRM not in (12,13) and CT_STATUS = 1 and";
        }

        List<Oorder> List_data = new List<Oorder>();
        string query = "";
        query = @"SELECT ORD_FIRM,CLC_RECNO,ORD_RECNO, ORD_CLCRECNO, CLC_ALLNAME ORD_CLCRECNON, ORD_PODCODE, ORD_NTS, ORD_TELEGRAM, ORD_FICHENO, CONVERT(VARCHAR(10), ORD_FICHEDATE, 103) ORD_FICHEDATE, ORD_FPOINT, ORD_TPOINT, 
        ISNULL(FPN.PNT_CODE, '')+'-'+ISNULL(TPN.PNT_CODE, '') PNTROUTE, ORD_VAGONOWNERSTN, STN.STN_NAME" + lg.Get_Lang() + @" STN_NAME, ORD_VAGONTYPE, 
        ORD_VAGONCOUNT, ORD_LOADSTCARD1, STC1.STC_CODE STC_NAME1, ORD_LOADSTCARD2, STC2.STC_CODE STC_NAME2, ISNULL(ORD_VGALLTONNAJ, 0) ORD_VGALLTONNAJ, ORD_NOTE, ORD_STATUS, 
        SPC.SC_VALUE" + lg.Get_Lang() + @" ORD_ACTTYPE,  isnull(ORD_STOTAL,0) as Total, S_CODE ORD_STATUSN,
        CASE WHEN ORD_VGNTRNTYPE=1 THEN ORD_VAGONCOUNT ELSE
        (ISNULL(ORD_CONTAINAMOUNT3,0)+ISNULL(ORD_CONTAINAMOUNT5,0)+ISNULL(ORD_CONTAINAMOUNT10,0)+ISNULL(ORD_CONTAINAMOUNT20,0)+
        ISNULL(ORD_CONTAINAMOUNT30,0)+ISNULL(ORD_CONTAINAMOUNT40,0)+ISNULL(ORD_ECONTAINAMOUNT3,0)+ISNULL(ORD_ECONTAINAMOUNT5,0)+
        ISNULL(ORD_ECONTAINAMOUNT10,0)+ISNULL(ORD_ECONTAINAMOUNT20,0)+ISNULL(ORD_ECONTAINAMOUNT30,0)+ISNULL(ORD_ECONTAINAMOUNT40,0)) END AS VAGONCOUNT
        FROM TBL_TRANSORDERS
        LEFT  JOIN TBL_POINTS FPN ON (ORD_FPOINT=FPN.PNT_RECNO)
        LEFT  JOIN TBL_POINTS TPN ON (ORD_TPOINT=TPN.PNT_RECNO)
        LEFT  JOIN TBL_STATIONS STN ON (ORD_VAGONOWNERSTN=STN.STN_RECNO)
        LEFT  JOIN TBL_STCARDS STC1 ON (ORD_LOADSTCARD1=STC1.STC_ID AND STC1.STC_STTYPE=1) 
        LEFT  JOIN TBL_STCARDS STC2 ON (ORD_LOADSTCARD2=STC2.STC_ID AND STC2.STC_STTYPE=2) 
        LEFT  JOIN TBL_SPECODES SPC ON (ORD_ACTTYPE=SPC.SC_REFID AND SC_TYPE='ORD_TYPEACT')
        LEFT JOIN  TBL_STATUS on ORD_STATUS=S_ID
        LEFT  JOIN TBL_CLCARDS CLC ON (ORD_CLCRECNO=CLC.CLC_RECNO)
        LEFT JOIN TBL_STATUS_USER on S_ID=SU_S_ID
        LEFT join T_SYS_USERCL on UC_CLC_ID=ORD_CLCRECNO
        left join [dbo].[TBL_INVOICELINE] on INVL_ORD_RECNO=ORD_RECNO
	    left join [dbo].[TBL_INVOICE] on INVL_INV_ID=INV_ID 
        inner join T_SYS_BRANCH on B_ID=ORD_B_ID and B_STATUS = 0
		inner join T_SYS_USERBR on UB_B_ID=B_ID and UB_U_ID=" + lg.Get_userid() + @"
        " + mmcjoin + @"
        WHERE " + mmc_filter + " S_CODE>=4  and  ORD_ACTTYPE IN (1,2,3,4) ";
        if (ACTTYPE == "1")
        {
            query += "and INVL_ORD_RECNO is null ";
        }
        else
        {
            query += "and INVL_ORD_RECNO is not null ";

        }


        query += @"
        group by ORD_FIRM,CLC_RECNO,ORD_RECNO, ORD_CLCRECNO, CLC_ALLNAME , ORD_PODCODE, ORD_NTS, ORD_TELEGRAM, ORD_FICHENO, ORD_FICHEDATE, ORD_FPOINT, ORD_TPOINT, ORD_STOTAL,
        FPN.PNT_CODE,TPN.PNT_CODE, ORD_VAGONOWNERSTN, STN.STN_NAME" + lg.Get_Lang() + @" , ORD_VAGONTYPE, 
        ORD_VAGONCOUNT, ORD_LOADSTCARD1, STC1.STC_CODE , ORD_LOADSTCARD2, STC2.STC_CODE , ORD_VGALLTONNAJ, ORD_NOTE, ORD_STATUS, 
        SPC.SC_VALUE" + lg.Get_Lang() + @" ,ORD_CONTAINAMOUNT3,ORD_CONTAINAMOUNT5,ORD_CONTAINAMOUNT10,ORD_CONTAINAMOUNT20,ORD_CONTAINAMOUNT30,ORD_CONTAINAMOUNT40,
        ORD_ECONTAINAMOUNT3,ORD_ECONTAINAMOUNT5,ORD_ECONTAINAMOUNT10,ORD_ECONTAINAMOUNT20,ORD_ECONTAINAMOUNT30,ORD_ECONTAINAMOUNT40,ORD_VGNTRNTYPE,S_CODE
        ORDER BY ORD_RECNO desc";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_data.Add(new Oorder
            {
                EXPCLIENT = reader["CLC_RECNO"].ToString(),
                FIRM = reader["ORD_FIRM"].ToString(),
                RECNO = reader["ORD_RECNO"].ToString(),
                CLCRECNON = reader["ORD_CLCRECNON"].ToString(),
                PODCODE = reader["ORD_PODCODE"].ToString(),
                NTS = reader["ORD_NTS"].ToString(),
                TELEGRAM = reader["ORD_TELEGRAM"].ToString(),
                FICHENO = reader["ORD_FICHENO"].ToString(),
                FICHEDATE = reader["ORD_FICHEDATE"].ToString(),
                //   FICHEDATE = Convert.ToDateTime(reader["ORD_FICHEDATE"].ToString()).ToString("MM/dd/yyyy HH:mm:ss"),
                PNTROUTE = reader["PNTROUTE"].ToString(),
                NAME = reader["STN_NAME"].ToString(),
                VAGONCOUNT = reader["VAGONCOUNT"].ToString() + "/" + reader["ORD_VGALLTONNAJ"].ToString(),
                NAME1 = reader["STC_NAME1"].ToString(),
                NAME2 = reader["STC_NAME2"].ToString(),
                STATUS = reader["ORD_STATUS"].ToString(),
                STATUSN = reader["ORD_STATUSN"].ToString(),
                ACTTYPE = reader["ORD_ACTTYPE"].ToString(),
                TOTAL = double.Parse(reader["Total"].ToString())
            });
        }
        return List_data;
    }

    public List<Oorder> Get_OrderWagonList()
    {
        List<Oorder> List_data = new List<Oorder>();
        string query = "";
        query = @"SELECT ORD_RECNO, ORD_FICHENO, CONVERT(VARCHAR(10), ORD_FICHEDATE, 103) ORD_FICHEDATE, ORD_PERIODYEAR, ORD_PERIODMONTH, ORD_FPOINT, ORD_TPOINT, 
        ISNULL(FPN.PNT_NAME, '')+'-'+ISNULL(TPN.PNT_NAME, '') PNTROUTE, ORD_STATION, STN.STN_NAME" + lg.Get_Lang() + @" STN_NAME, ORD_VAGONTYPE, 
        ORD_VAGONCOUNT, ORD_LOADSTCARD1, STC1.STC_NAME STC_NAME1, ORD_LOADSTCARD2, STC2.STC_NAME STC_NAME2, ORD_LOADAMOUNT, ORD_NOTE, ORD_STATUS, 
        SPC.SC_VALUE" + lg.Get_Lang() + @" ORD_STATUSN FROM TBL_ORDERS
        LEFT OUTER JOIN TBL_POINTS FPN ON (ORD_FPOINT=FPN.PNT_RECNO)
        LEFT OUTER JOIN TBL_POINTS TPN ON (ORD_TPOINT=TPN.PNT_RECNO)
        LEFT OUTER JOIN TBL_STATIONS STN ON (ORD_STATION=STN.STN_RECNO)
        LEFT OUTER JOIN TBL_STCARDS STC1 ON (ORD_LOADSTCARD1=STC1.STC_ID) 
        LEFT OUTER JOIN TBL_STCARDS STC2 ON (ORD_LOADSTCARD2=STC2.STC_ID) 
        LEFT OUTER JOIN TBL_SPECODES SPC ON (ORD_STATUS=SPC.SC_REFID AND SC_TYPE='ORD_CLIENT')
        WHERE ORD_STATUS IN (1, 2, 3, 4, 5) 
        ORDER BY ORD_FICHEDATE ";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_data.Add(new Oorder
            {
                RECNO = reader["ORD_RECNO"].ToString(),
                FICHENO = reader["ORD_FICHENO"].ToString(),
                FICHEDATE = reader["ORD_FICHEDATE"].ToString(),
                PNTROUTE = reader["PNTROUTE"].ToString(),
                NAME = reader["STN_NAME"].ToString(),
                VAGONCOUNT = reader["ORD_VAGONCOUNT"].ToString(),
                NAME1 = reader["STC_NAME1"].ToString(),
                NAME2 = reader["STC_NAME2"].ToString(),
                LOADAMOUNT = reader["ORD_LOADAMOUNT"].ToString(),
                STATUS = reader["ORD_STATUS"].ToString(),
                STATUSN = reader["ORD_STATUSN"].ToString()
            });
        }

        return List_data;
    }

    public List<Oorder> Get_RequestWagonList()
    {
        List<Oorder> List_data = new List<Oorder>();
        string query = "";
        query = @"SELECT ORD_RECNO, ORD_FICHENO, CONVERT(VARCHAR(10), ORD_FICHEDATE, 103) ORD_FICHEDATE, ORD_PERIODYEAR, ORD_PERIODMONTH, ORD_FPOINT, ORD_TPOINT, 
        ISNULL(FPN.PNT_NAME, '')+'-'+ISNULL(TPN.PNT_NAME, '') PNTROUTE, ORD_STATION, STN.STN_NAME" + lg.Get_Lang() + @" STN_NAME, ORD_VAGONTYPE, 
        ORD_VAGONCOUNT, ORD_LOADSTCARD1, STC1.STC_NAME STC_NAME1, ORD_LOADSTCARD2, STC2.STC_NAME STC_NAME2, ORD_LOADAMOUNT, ORD_NOTE, ORD_STATUS, SPC.SC_VALUE1 ORD_STATUSN,
        ISNULL(ORD_SIGNSEND, 0) ORD_SIGNSEND FROM TBL_ORDERS
        LEFT OUTER JOIN TBL_POINTS FPN ON (ORD_FPOINT=FPN.PNT_RECNO)
        LEFT OUTER JOIN TBL_POINTS TPN ON (ORD_TPOINT=TPN.PNT_RECNO)
        LEFT OUTER JOIN TBL_STATIONS STN ON (ORD_STATION=STN.STN_RECNO)
        LEFT OUTER JOIN TBL_STCARDS STC1 ON (ORD_LOADSTCARD1=STC1.STC_RECNO) 
        LEFT OUTER JOIN TBL_STCARDS STC2 ON (ORD_LOADSTCARD2=STC2.STC_RECNO)
        LEFT OUTER JOIN TBL_SPECODES SPC ON (ORD_STATUS=SPC.SC_REFID AND SC_TYPE='ORD_CENTER')
        WHERE ORD_STATUS IN (3, 4, 5) 
        ORDER BY ORD_FICHEDATE ";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_data.Add(new Oorder
            {
                RECNO = reader["ORD_RECNO"].ToString(),
                FICHENO = reader["ORD_FICHENO"].ToString(),
                FICHEDATE = reader["ORD_FICHEDATE"].ToString(),
                PNTROUTE = reader["PNTROUTE"].ToString(),
                NAME = reader["STN_NAME"].ToString(),
                VAGONCOUNT = reader["ORD_VAGONCOUNT"].ToString(),
                NAME1 = reader["STC_NAME1"].ToString(),
                NAME2 = reader["STC_NAME2"].ToString(),
                LOADAMOUNT = reader["ORD_LOADAMOUNT"].ToString(),
                STATUS = reader["ORD_STATUS"].ToString(),
                STATUSN = reader["ORD_STATUSN"].ToString()
            });
        }
        return List_data;
    }

    public List<Oorder> Get_OrderPriceList(int Recno)
    {
        List<Oorder> List_data = new List<Oorder>();
        string query = "";
        query = @" SELECT SC_REFID REFID, SC_VALUE" + lg.Get_Lang() + @" SC_VALUE, ISNULL(FEX.EXP_EXPENSE, 0) FEXP_EXPENSE, ISNULL(EEX.EXP_EXPENSE, 0) EEXP_EXPENSE,isnull(FEX.EXP_AMOUNT,0) FEXP_AMOUNT,isnull(EEX.EXP_AMOUNT,0) EXP_AMOUNT FROM TBL_SPECODES SPC
        LEFT OUTER JOIN TBL_TRANSORDEREXP FEX ON (SPC.SC_REFID=FEX.EXP_EXPTYPEID AND ISNULL(FEX.EXP_TYPE, 0)=1 AND ISNULL(FEX.EXP_ORDRECNO, 0)=" + Recno.ToString() + @" AND ISNULL(FEX.EXP_FIRMTYPE, 0)=0)
        LEFT OUTER JOIN TBL_TRANSORDEREXP EEX ON (SPC.SC_REFID=EEX.EXP_EXPTYPEID AND ISNULL(EEX.EXP_TYPE, 0)=2 AND ISNULL(EEX.EXP_ORDRECNO, 0)=" + Recno.ToString() + @" AND ISNULL(EEX.EXP_FIRMTYPE, 0)=0)
        WHERE SC_TYPE='EXPENCETYPE' AND SC_STATUS=0 
        ORDER BY SC_ORDER";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_data.Add(new Oorder
            {
                REFID = reader["REFID"].ToString(),
                VALUE = reader["SC_VALUE"].ToString(),
                F_EXPENSE = reader["FEXP_AMOUNT"].ToString(),
                E_EXPENSE = reader["EXP_AMOUNT"].ToString()
            });
        }
        return List_data;
    }
    public List<Oorder> Get_RequestPriceList(int Recno, int type, int template)
    {
        List<Oorder> List_data = new List<Oorder>();
        string query = "";
        query = @"SELECT isnull(T_TYPE,0) TYPE,EX_ID REFID, EX_VALUE" + lg.Get_Lang() + @" SC_VALUE, 
        ISNULL(ADY.EXP_EXPENSE, 0) ADY_EXPENSE,isnull(EXP_PEXPENSE,0) P_EXPENSE,ISNULL(ADY.EXP_QTY,0) QTY,isnull(EXP_PAMOUNT,0) EXP_PAMOUNT,
        isnull(EXP_AMOUNT,0) EXP_AMOUNT,isnull(EXP_MANUAL,0) EXP_MANUAL,isnull(EXP_CLC_RECNO,0) EXP_CLC_RECNO,EXP_P_FORMULA,EXP_S_FORMULA,isnull(EXP_EXPENSE_H,0)  EXP_EXPENSE_H
        FROM TBL_EXPENCE SPC
        LEFT OUTER JOIN TBL_TRANSORDEREXP_TEMP ADY ON (SPC.EX_ID=ADY.EXP_EXPTYPEID AND ISNULL(ADY.EXP_TYPE, 0)=" + type + " AND ISNULL(ADY.EXP_ORDRECNO, 0)=" + Recno + @")
        inner join TBL_TEMPLET on EX_ID=T_EXPENCETYPE
        WHERE isnull(EX_STATUS,0)=0 and  T_EXP_TEMPLET=" + template + @" AND EXP_U_ID=" + lg.Get_userid() + @"
        ORDER BY EX_ID";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_data.Add(new Oorder
            {
                REFID = reader["REFID"].ToString(),
                VALUE = reader["SC_VALUE"].ToString(),
                A_EXPENSE = reader["ADY_EXPENSE"].ToString(),
                P_EXPENSE = reader["P_EXPENSE"].ToString(),
                QTY = reader["QTY"].ToString(),
                AMOUNT = float.Parse(reader["EXP_AMOUNT"].ToString()),
                P_AMOUNT = double.Parse(reader["EXP_PAMOUNT"].ToString()),
                ISQT = bool.Parse(reader["TYPE"].ToString()),
                ACTTYPE = reader["EXP_MANUAL"].ToString(),
                EXPEDITOR = reader["EXP_CLC_RECNO"].ToString(),
                P_FORMULA = reader["EXP_P_FORMULA"].ToString(),
                S_FORMULA = reader["EXP_S_FORMULA"].ToString(),
                EXPENSEH = float.Parse(reader["EXP_EXPENSE_H"].ToString())
            });

        }
        return List_data;
    }
    public int Save_Order(
                               string ORD_NTS
                           , string ORD_PODCODE
                           , string ORD_TELEGRAM
                           , int ORD_CLCRECNO
                           , int ORD_FIRM
                           , string ORD_FICHENO
                           , string ORD_FICHEDATE
                           , string ORD_FCLIENT
                           , int ORD_FPOINT
                           , string ORD_FPOINTCODE
                           , string ORD_TCLIENT
                           , int ORD_TPOINT
                           , string ORD_TPOINTCODE
                           , int ORD_LOADSTCARD1
                           , string ORD_LOADSTCARDCODE1
                           , int ORD_LOADSTCARD2
                           , string ORD_LOADSTCARDCODE2
                           , double ORD_LOADAMOUNT
                           , string ORD_LOADNOTE
                           , int ORD_VGNTRNTYPE
                           , int ORD_VAGONOWNER
                           , int ORD_VAGONTYPE
                           , double ORD_VAGONTONNAJ
                           , double ORD_VGALLTONNAJ
                           , int ORD_VAGONCOUNT
                           , int ORD_VGALLCOUNT
                           , int ORD_CONTAINTYPE
                           , double ORD_CONTAINAMOUNT3
                           , double ORD_CONTAINAMOUNT5
                           , double ORD_CONTAINAMOUNT10
                           , double ORD_CONTAINAMOUNT20
                           , double ORD_CONTAINAMOUNT30
                           , double ORD_CONTAINAMOUNT40
                           , double ORD_CONTAINAMOUNT45
                           , double ORD_ECONTAINAMOUNT3
                           , double ORD_ECONTAINAMOUNT5
                           , double ORD_ECONTAINAMOUNT10
                           , double ORD_ECONTAINAMOUNT20
                           , double ORD_ECONTAINAMOUNT30
                           , double ORD_ECONTAINAMOUNT40
                           , double ORD_ECONTAINAMOUNT45
                           , string ORD_VAGONNOTE
                           , string ORD_NOTE
                           , int ORD_STATUS
                           , string ORD_SIGNNOTE
                           , int ORD_SIGNSEND
                           , double ORD_DISTANCE
                           , int ORD_ACTTYPE
                           , string ORD_BEGPOINT
                           , string ORD_ENDPOINT
                           , int ORD_EXPCLIENT
                           , int ORD_CLC_PTYPE
                           , int ID
                           , double ORD_WIDTH
                           , double ORD_LENGTH
                           , double ORD_HEIGHT
                           , double ORD_CONTWEIGHT,
                           string ORD_ETA,
                           int ORD_ISQT,

                           string txt_extra,
                           string txt_eextra,
                           string ORD_PLATOWNER,
                           string ORD_PLATTYPE,
                           string ORD_PLATCOUNT,
                           string ORD_C_ID,
                           int ORD_KASPAR,
                           string ORD_B_ID,
                           string ORD_PRICEAREA,
                           string ORD_EXP_TEMPLET,
                           float ORD_PTOTAL,
                           float ORD_STOTAL,
                           float ORD_PROFIT,
                           int ORD_EBEGPOINT,
                           int ORD_EENDPOINT,
                           string ORD_REF_NO = ""

                           )
    {
        //try
        //{
        conn con = new conn();

        string query = "";
        if (ID == -1)
        {
            query = @"Insert into  TBL_TRANSORDERS 
                       (
                          ORD_NTS
                        , ORD_PODCODE
                        , ORD_TELEGRAM
                        , ORD_CLCRECNO
                        , ORD_B_ID
                        , ORD_FIRM
                        , ORD_FICHENO
                        , ORD_FICHEDATE
                        , ORD_FCLIENT
                        , ORD_FPOINT
                        , ORD_FPOINTCODE
                        , ORD_TCLIENT
                        , ORD_TPOINT
                        , ORD_TPOINTCODE
                        , ORD_LOADSTCARD1
                        , ORD_LOADSTCARDCODE1
                        , ORD_LOADSTCARD2
                        , ORD_LOADSTCARDCODE2
                        , ORD_LOADAMOUNT
                        , ORD_LOADNOTE
                        , ORD_VGNTRNTYPE
                        , ORD_VAGONOWNER
                        , ORD_VAGONTYPE
                        , ORD_VAGONTONNAJ
                        , ORD_VGALLTONNAJ
                        , ORD_VAGONCOUNT
                        , ORD_VGALLCOUNT
                        , ORD_CONTAINTYPE
                        , ORD_CONTAINAMOUNT3
                        , ORD_CONTAINAMOUNT5
                        , ORD_CONTAINAMOUNT10
                        , ORD_CONTAINAMOUNT20
                        , ORD_CONTAINAMOUNT30
                        , ORD_CONTAINAMOUNT40
                        , ORD_CONTAINAMOUNT45
                        , ORD_ECONTAINAMOUNT3
                        , ORD_ECONTAINAMOUNT5
                        , ORD_ECONTAINAMOUNT10
                        , ORD_ECONTAINAMOUNT20
                        , ORD_ECONTAINAMOUNT30
                        , ORD_ECONTAINAMOUNT40
                        , ORD_ECONTAINAMOUNT45
                        , ORD_VAGONNOTE
                        , ORD_NOTE
                        , ORD_STATUS
                        , ORD_SIGNNOTE
                        , ORD_SIGNSEND
                        , ORD_DISTANCE
                        , ORD_ACTTYPE
                        , ORD_ENDPOINT
                        , ORD_BEGPOINT
                        , ORD_EXPCLIENT
                        , ORD_CLC_PTYPE
                        , ORD_U_ID
                        , ORD_WIDTH
                        , ORD_LENGTH
                        , ORD_HEIGHT
                        , ORD_CONTWEIGHT,
                        ORD_ETA,
                        ORD_ISQT,
                        ORD_CONTAINAMOUNTEXTRA,
                        ORD_ECONTAINAMOUNTEXTRA,
                        ORD_PLATOWNER,
                        ORD_PLATTYPE,
                        ORD_PLATCOUNT,
                        ORD_C_ID,
                        ORD_KASPAR,
                        
                        ORD_PRICEAREA,
                        ORD_EXP_TEMPLET,
                        ORD_PTOTAL,
                        ORD_STOTAL,
                        ORD_PROFIT,
                        ORD_EBEGPOINT,
                        ORD_EENDPOINT,
                        ORD_FRAMELESS
                        )
                            values 
                        (
                          @ORD_NTS
                        , @ORD_PODCODE
                        , @ORD_TELEGRAM
                        , @ORD_CLCRECNO
                        , @ORD_B_ID
                        , @ORD_FIRM
                        , @ORD_FICHENO
                        , @ORD_FICHEDATE
                        , @ORD_FCLIENT
                        , @ORD_FPOINT
                        , @ORD_FPOINTCODE
                        , @ORD_TCLIENT
                        , @ORD_TPOINT
                        , @ORD_TPOINTCODE
                        , @ORD_LOADSTCARD1
                        , @ORD_LOADSTCARDCODE1
                        , @ORD_LOADSTCARD2
                        , @ORD_LOADSTCARDCODE2
                        , @ORD_LOADAMOUNT
                        , @ORD_LOADNOTE
                        , @ORD_VGNTRNTYPE
                        , @ORD_VAGONOWNER
                        , @ORD_VAGONTYPE
                        , @ORD_VAGONTONNAJ
                        , @ORD_VGALLTONNAJ
                        , @ORD_VAGONCOUNT
                        , @ORD_VGALLCOUNT
                        , @ORD_CONTAINTYPE
                        , @ORD_CONTAINAMOUNT3
                        , @ORD_CONTAINAMOUNT5
                        , @ORD_CONTAINAMOUNT10
                        , @ORD_CONTAINAMOUNT20
                        , @ORD_CONTAINAMOUNT30
                        , @ORD_CONTAINAMOUNT40
                        , @ORD_CONTAINAMOUNT45
                        , @ORD_ECONTAINAMOUNT3
                        , @ORD_ECONTAINAMOUNT5
                        , @ORD_ECONTAINAMOUNT10
                        , @ORD_ECONTAINAMOUNT20
                        , @ORD_ECONTAINAMOUNT30
                        , @ORD_ECONTAINAMOUNT40
                        , @ORD_ECONTAINAMOUNT45
                        , @ORD_VAGONNOTE
                        , @ORD_NOTE
                        , @ORD_STATUS
                        , @ORD_SIGNNOTE
                        , @ORD_SIGNSEND
                        , @ORD_DISTANCE
                        , @ORD_ACTTYPE
                        , @ORD_ENDPOINT
                        , @ORD_BEGPOINT
                        , @ORD_EXPCLIENT
                        , @ORD_CLC_PTYPE
                        , " + lgn.Get_userid() + @"
                        , @ORD_WIDTH
                        , @ORD_LENGTH 
                        , @ORD_HEIGHT
                        , @ORD_CONTWEIGHT
                        , @ORD_ETA

                        , @ORD_ISQT
                        , @txt_extra
                        , @txt_eextra
                        , @ORD_PLATOWNER
                        , @ORD_PLATTYPE
                        , @ORD_PLATCOUNT
                        , @ORD_C_ID
                        , @ORD_KASPAR
                        
                        , @ORD_PRICEAREA
                        , @ORD_EXP_TEMPLET
                        , @ORD_PTOTAL
                        , @ORD_STOTAL
                        , @ORD_PROFIT
                        , @ORD_EBEGPOINT
                        , @ORD_EENDPOINT
                        , @ORD_FRAMELESS
                        )
                        SELECT SCOPE_IDENTITY() as _newid ";
        }
        else
        {
            query = @"Update  TBL_TRANSORDERS set
                        ORD_CLCRECNO=@ORD_CLCRECNO, 
                        ORD_FIRM=@ORD_FIRM, 
                        ORD_B_ID=@ORD_B_ID,
                        ORD_FICHENO=@ORD_FICHENO, 
                        ORD_FICHEDATE=@ORD_FICHEDATE,
                        ORD_FCLIENT=@ORD_FCLIENT,
                        ORD_FPOINT=@ORD_FPOINT,
                        ORD_FPOINTCODE=@ORD_FPOINTCODE,
                        ORD_TCLIENT=@ORD_TCLIENT, 
                        ORD_TPOINT=@ORD_TPOINT, 
                        ORD_TPOINTCODE=@ORD_TPOINTCODE, 
                        ORD_LOADSTCARD1=@ORD_LOADSTCARD1, 
                        ORD_LOADSTCARDCODE1=@ORD_LOADSTCARDCODE1,
                        ORD_LOADSTCARDCODE2=@ORD_LOADSTCARDCODE2,
                        ORD_LOADSTCARD2=@ORD_LOADSTCARD2, 
                        ORD_LOADAMOUNT=@ORD_LOADAMOUNT, 
                        ORD_LOADNOTE=@ORD_LOADNOTE, 
                        ORD_VGNTRNTYPE=@ORD_VGNTRNTYPE, 
                        ORD_VAGONOWNER=@ORD_VAGONOWNER,
                        ORD_VAGONTYPE=@ORD_VAGONTYPE, 
                        ORD_VAGONTONNAJ=@ORD_VAGONTONNAJ, 
                        ORD_VGALLTONNAJ=@ORD_VGALLTONNAJ, 
                        ORD_VAGONCOUNT=@ORD_VAGONCOUNT, 
                        ORD_VGALLCOUNT=@ORD_VGALLCOUNT, 
                        ORD_CONTAINTYPE=@ORD_CONTAINTYPE,
                        ORD_CONTAINAMOUNT3=@ORD_CONTAINAMOUNT3,
                        ORD_CONTAINAMOUNT5=@ORD_CONTAINAMOUNT5,
                        ORD_CONTAINAMOUNT10=@ORD_CONTAINAMOUNT10,
                        ORD_CONTAINAMOUNT20=@ORD_CONTAINAMOUNT20,
                        ORD_CONTAINAMOUNT30=@ORD_CONTAINAMOUNT30,
                        ORD_CONTAINAMOUNT40=@ORD_CONTAINAMOUNT40,
                        ORD_CONTAINAMOUNT45=@ORD_CONTAINAMOUNT45,
                        ORD_ECONTAINAMOUNT3=@ORD_ECONTAINAMOUNT3,
                        ORD_ECONTAINAMOUNT5=@ORD_ECONTAINAMOUNT5,
                        ORD_ECONTAINAMOUNT10=@ORD_ECONTAINAMOUNT10,
                        ORD_ECONTAINAMOUNT20=@ORD_ECONTAINAMOUNT20,
                        ORD_ECONTAINAMOUNT30=@ORD_ECONTAINAMOUNT30,
                        ORD_ECONTAINAMOUNT40=@ORD_ECONTAINAMOUNT40,
                        ORD_ECONTAINAMOUNT45=@ORD_ECONTAINAMOUNT45,
                        ORD_VAGONNOTE=@ORD_VAGONNOTE, 
                        ORD_NOTE=@ORD_NOTE, 
                        ORD_STATUS=@ORD_STATUS,  
                        ORD_SIGNNOTE=@ORD_SIGNNOTE, 
                        ORD_SIGNSEND=@ORD_SIGNSEND, 
                        ORD_DISTANCE=@ORD_DISTANCE,
                        ORD_ACTTYPE=@ORD_ACTTYPE,
                        ORD_BEGPOINT=@ORD_BEGPOINT,
                        ORD_ENDPOINT=@ORD_ENDPOINT,
                        ORD_NTS=@ORD_NTS,
                        ORD_PODCODE=@ORD_PODCODE,
                        ORD_TELEGRAM=@ORD_TELEGRAM,
                        ORD_EXPCLIENT=@ORD_EXPCLIENT,
                        ORD_CLC_PTYPE=@ORD_CLC_PTYPE,
                        ORD_UPD_U_ID=" + lgn.Get_userid() + @",
                        ORD_CONTWEIGHT=@ORD_CONTWEIGHT,
                        ORD_WIDTH=@ORD_WIDTH,
                        ORD_LENGTH=@ORD_LENGTH,
                        ORD_HEIGHT=@ORD_HEIGHT,
                        ORD_ETA=@ORD_ETA,
                        ORD_ISQT=@ORD_ISQT,
                        ORD_CONTAINAMOUNTEXTRA=@txt_extra,
                        ORD_ECONTAINAMOUNTEXTRA=@txt_eextra,
                        ORD_PLATOWNER=@ORD_PLATOWNER,
                        ORD_PLATTYPE=@ORD_PLATTYPE,
                        ORD_PLATCOUNT=@ORD_PLATCOUNT,
                        ORD_C_ID=@ORD_C_ID,
                        ORD_KASPAR=@ORD_KASPAR,
                        ORD_PRICEAREA=@ORD_PRICEAREA,
                        ORD_EXP_TEMPLET=@ORD_EXP_TEMPLET,
                        ORD_PTOTAL=@ORD_PTOTAL,
                        ORD_STOTAL=@ORD_STOTAL,
                        ORD_PROFIT=@ORD_PROFIT,
                        ORD_EBEGPOINT=@ORD_EBEGPOINT,
                        ORD_EENDPOINT=@ORD_EENDPOINT
                        where ORD_RECNO=@ID";
        }

        using (SqlCommand command = new SqlCommand(query, con.connectionOpen()))
        {
            command.Parameters.AddWithValue("@ORD_NTS", ORD_NTS);
            command.Parameters.AddWithValue("@ORD_PODCODE", ORD_PODCODE);
            command.Parameters.AddWithValue("@ORD_TELEGRAM", ORD_TELEGRAM);
            int firm_id = ORD_FIRM;
            if (ORD_FIRM == 1) { firm_id = 2; }
            else if (ORD_FIRM == 2) { firm_id = 11; }
            else if (ORD_FIRM == 7) { firm_id = 12; }
            else if (ORD_FIRM == 8) { firm_id = 13; }

            command.Parameters.AddWithValue("@ORD_CLCRECNO", ORD_CLCRECNO);
            command.Parameters.AddWithValue("@ORD_FIRM", firm_id);
            command.Parameters.AddWithValue("@ORD_B_ID", ORD_FIRM);
            command.Parameters.AddWithValue("@ORD_FICHENO", ORD_FICHENO);


            DateTime FICHEDATE = Convert.ToDateTime(ORD_FICHEDATE);
            string ORD_FICHEDATEE = FICHEDATE.ToString("yyyy-MM-dd HH:mm:ss");

            command.Parameters.AddWithValue("@ORD_FICHEDATE", ORD_FICHEDATEE);
            command.Parameters.AddWithValue("@ORD_FCLIENT", ORD_FCLIENT);
            command.Parameters.AddWithValue("@ORD_FPOINT", ORD_FPOINT);
            command.Parameters.AddWithValue("@ORD_FPOINTCODE", ORD_FPOINTCODE);
            command.Parameters.AddWithValue("@ORD_TCLIENT", ORD_TCLIENT);
            command.Parameters.AddWithValue("@ORD_TPOINT", ORD_TPOINT);
            command.Parameters.AddWithValue("@ORD_TPOINTCODE", ORD_TPOINTCODE);
            command.Parameters.AddWithValue("@ORD_LOADSTCARD1", ORD_LOADSTCARD1);
            command.Parameters.AddWithValue("@ORD_LOADSTCARDCODE1", ORD_LOADSTCARDCODE1);
            command.Parameters.AddWithValue("@ORD_LOADSTCARDCODE2", ORD_LOADSTCARDCODE2);
            command.Parameters.AddWithValue("@ORD_LOADSTCARD2", ORD_LOADSTCARD2);
            command.Parameters.AddWithValue("@ORD_LOADAMOUNT", ORD_LOADAMOUNT);
            command.Parameters.AddWithValue("@ORD_LOADNOTE", ORD_LOADNOTE);
            command.Parameters.AddWithValue("@ORD_VGNTRNTYPE", ORD_VGNTRNTYPE);
            command.Parameters.AddWithValue("@ORD_VAGONOWNER", ORD_VAGONOWNER);
            command.Parameters.AddWithValue("@ORD_VAGONTYPE", ORD_VAGONTYPE);
            command.Parameters.AddWithValue("@ORD_VAGONTONNAJ", ORD_VAGONTONNAJ);
            command.Parameters.AddWithValue("@ORD_VGALLTONNAJ", ORD_VGALLTONNAJ);
            command.Parameters.AddWithValue("@ORD_VAGONCOUNT", ORD_VAGONCOUNT);
            command.Parameters.AddWithValue("@ORD_VGALLCOUNT", ORD_VGALLCOUNT);
            command.Parameters.AddWithValue("@ORD_CONTAINTYPE", ORD_CONTAINTYPE);

                command.Parameters.AddWithValue("@ORD_CONTAINAMOUNT3", ORD_CONTAINAMOUNT3);
                command.Parameters.AddWithValue("@ORD_CONTAINAMOUNT5", ORD_CONTAINAMOUNT5);
                command.Parameters.AddWithValue("@ORD_CONTAINAMOUNT10", ORD_CONTAINAMOUNT10);
                command.Parameters.AddWithValue("@ORD_CONTAINAMOUNT20", ORD_CONTAINAMOUNT20);
                command.Parameters.AddWithValue("@ORD_CONTAINAMOUNT30", ORD_CONTAINAMOUNT30);
                command.Parameters.AddWithValue("@ORD_CONTAINAMOUNT40", ORD_CONTAINAMOUNT40);
                command.Parameters.AddWithValue("@ORD_CONTAINAMOUNT45", ORD_CONTAINAMOUNT45);
                command.Parameters.AddWithValue("@ORD_ECONTAINAMOUNT3", ORD_ECONTAINAMOUNT3);
                command.Parameters.AddWithValue("@ORD_ECONTAINAMOUNT5", ORD_ECONTAINAMOUNT5);
                command.Parameters.AddWithValue("@ORD_ECONTAINAMOUNT10", ORD_ECONTAINAMOUNT10);
                command.Parameters.AddWithValue("@ORD_ECONTAINAMOUNT20", ORD_ECONTAINAMOUNT20);
                command.Parameters.AddWithValue("@ORD_ECONTAINAMOUNT30", ORD_ECONTAINAMOUNT30);
                command.Parameters.AddWithValue("@ORD_ECONTAINAMOUNT40", ORD_ECONTAINAMOUNT40);
                command.Parameters.AddWithValue("@ORD_ECONTAINAMOUNT45", ORD_ECONTAINAMOUNT45);
            
            command.Parameters.AddWithValue("@ORD_VAGONNOTE", ORD_VAGONNOTE);
            command.Parameters.AddWithValue("@ORD_NOTE", ORD_NOTE);
            command.Parameters.AddWithValue("@ORD_STATUS", ORD_STATUS);
            command.Parameters.AddWithValue("@ORD_SIGNNOTE", ORD_SIGNNOTE);
            command.Parameters.AddWithValue("@ORD_SIGNSEND", ORD_SIGNSEND);
            command.Parameters.AddWithValue("@ORD_DISTANCE", ORD_DISTANCE);
            command.Parameters.AddWithValue("@ORD_ACTTYPE", ORD_ACTTYPE);
            command.Parameters.AddWithValue("@ORD_BEGPOINT", ORD_BEGPOINT);
            command.Parameters.AddWithValue("@ORD_ENDPOINT", ORD_ENDPOINT);
            command.Parameters.AddWithValue("@ORD_EXPCLIENT", ORD_EXPCLIENT);
            command.Parameters.AddWithValue("@ORD_CLC_PTYPE", ORD_CLC_PTYPE);
            command.Parameters.AddWithValue("@ORD_HEIGHT", ORD_HEIGHT);
            command.Parameters.AddWithValue("@ORD_CONTWEIGHT", ORD_CONTWEIGHT);
            command.Parameters.AddWithValue("@ORD_ETA", ORD_ETA);

            command.Parameters.AddWithValue("@ORD_ISQT", ORD_ISQT);
            command.Parameters.AddWithValue("@txt_extra", txt_extra);
            command.Parameters.AddWithValue("@txt_eextra", txt_eextra);
            command.Parameters.AddWithValue("@ORD_PLATOWNER", ORD_PLATOWNER);
            command.Parameters.AddWithValue("@ORD_PLATTYPE", ORD_PLATTYPE);
            command.Parameters.AddWithValue("@ORD_PLATCOUNT", ORD_PLATCOUNT);
            command.Parameters.AddWithValue("@ORD_C_ID", ORD_C_ID);
            command.Parameters.AddWithValue("@ORD_KASPAR", ORD_KASPAR);
            command.Parameters.AddWithValue("@ORD_PRICEAREA", ORD_PRICEAREA);
            command.Parameters.AddWithValue("@ORD_EXP_TEMPLET", ORD_EXP_TEMPLET);
            command.Parameters.AddWithValue("@ORD_PTOTAL", ORD_PTOTAL);
            command.Parameters.AddWithValue("@ORD_STOTAL", ORD_STOTAL);
            command.Parameters.AddWithValue("@ORD_PROFIT", ORD_PROFIT);
            command.Parameters.AddWithValue("@ORD_EBEGPOINT", ORD_EBEGPOINT);
            command.Parameters.AddWithValue("@ORD_EENDPOINT", ORD_EENDPOINT);

            if (ID == -1)
            {
                command.Parameters.AddWithValue("@ORD_FRAMELESS", ORD_REF_NO);
            }

            command.Parameters.AddWithValue("@ORD_WIDTH", ORD_WIDTH);
            command.Parameters.AddWithValue("@ORD_LENGTH", ORD_LENGTH);
            command.Parameters.AddWithValue("@ID", ID);

            System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();
            int ORD_ID = 0;
            if (reader.Read())
            {
                ORD_ID = Convert.ToInt32(reader["_newid"]);
            }
            else
            {
                ORD_ID = ID;
            }
            con.ConnetionClose();
            return ORD_ID;
        }
        //}
        //catch (Exception ex)
        //{

        //    //Erroru ya database yada mail gonder


        //    return false;
        //}
    }





    public void SaveTransportList(string transportOpr, string platform, string ordid, string ficheClient="")
    {
        string sql = "";
        string sql_plat = "";

        System.Data.SqlClient.SqlDataReader reader = null;
        System.Data.SqlClient.SqlDataReader reader2 = null;

        List<TransportSaveModel> trpList = JsonConvert.DeserializeObject<List<TransportSaveModel>>(transportOpr);
        PlatformSaveModel platmodel = JsonConvert.DeserializeObject<PlatformSaveModel>(platform);

        List<ContCountModel> cList = new List<ContCountModel>();
        ListSaveResponseModel saverespmodel = new ListSaveResponseModel();

        try
        {
            foreach (var item in trpList)
            {
                TransportSaveModel trpModel = new TransportSaveModel();

                if (item.TRN_ID == 0)
                {
                    con.dbrun(@"INSERT INTO TBL_TRANSPORTLIST(TRN_ORDID,TRN_FULLEMPTY,TRN_PREFIX,TRN_NO,TRN_TRTYPE,TRN_OWNER,TRN_TRCAT,TRN_TYPE,TRN_TNID,TRN_COUNT,TRN_ORDFICHENO)
                            VALUES(
                            " + ordid + @",
                            " + item.TRN_FullemptyId + @",
                            '" + item.Prefix + @"',
                            '" + item.No + @"',
                            " + item.TrTypeId + @",
                            " + item.OwnerId + @",
                            " + item.CategoryId + @",
                            " + item.ConTypeId + @",
                            " + item.TNID + @",
                            " + item.ContCount + @",
                            '" + ficheClient + @"'
                            );");
                }
                else
                {
                    con.dbrun(@"UPDATE  TBL_TRANSPORTLIST
                            SET 
                               TRN_ORDID =  " + ordid + @",
                               TRN_FULLEMPTY =  " + item.TRN_FullemptyId + @",
                               TRN_PREFIX =  '" + item.Prefix + @"',
                               TRN_NO =  '" + item.No + @"',
                               TRN_TRTYPE = " + item.TrTypeId + @",
                               TRN_OWNER = " + item.OwnerId + @",
                               TRN_TRCAT =  " + item.CategoryId + @",
                               TRN_TYPE = " + item.ConTypeId + @",
                               TRN_COUNT = " + item.ContCount + @",
                               TRN_TNID = " + item.TNID + @",
                               TRN_ORDFICHENO = '" + ficheClient + @"'
                           WHERE TRN_ID = " + item.TRN_ID + ";");
                }

            }

            if (platform.Length > 0)
            {
                reader = con.dbrun("SELECT TRN_ID FROM TBL_TRANSPORTLIST WHERE TRN_TRTYPE = 1 AND TRN_ORDID = " + ordid);
                if (reader.Read())
                {
                    con.dbrun(@"UPDATE TBL_TRANSPORTLIST
                             SET TRN_PLATOWNER = " + platmodel.PlatOwner + @",
                                 TRN_PLATTYPE = " + platmodel.PlatType + @",
                                 TRN_PLATCOUNT = " + platmodel.PlatCount + @"
                             WHERE TRN_TRTYPE = 1 AND TRN_ORDID = " + ordid);
                }
                else
                {
                    con.dbrun(@"INSERT INTO TBL_TRANSPORTLIST(TRN_ORDID,TRN_TRTYPE,TRN_PLATOWNER,TRN_PLATTYPE,TRN_PLATCOUNT, TRN_ORDFICHENO)
                            VALUES(
                                " + ordid + @",
                                " + 1 + @",
                                " + platmodel.PlatOwner + @",
                                " + platmodel.PlatType + @",
                                " + platmodel.PlatCount + @",
                                '" + ficheClient + @"'
                                )");
                }
            }
        }
        catch (Exception ex)
        {
            
        }


    }



    public void UpdateTransportlist(int id, string ficheno, string clientId)
    {

        try
        {
            string sql = @"UPDATE TBL_TRANSPORTLIST
                        SET TRN_ORDID = " + id + @"
                        WHERE TRN_ORDFICHENO = '" + ficheno.Replace("-", "") + clientId + "'";
            con.dbrun(sql);
        }
        catch (Exception)
        {
            throw;
        }

    }
   
    public bool Delete_Order(int id)
    {
        string query = "";
        // query = @"Update   TBL_TRANSORDERS set ORD_STATUS=-1 WHERE ORD_RECNO=@ORD_RECNO and ORD_STATUS=1";
        query = @"Update   TBL_TRANSORDERS set ORD_STATUS=99 WHERE ORD_RECNO=@ORD_RECNO --and ORD_STATUS=1";
        conn con = new conn();
        using (SqlCommand command = new SqlCommand(query, con.connectionOpen()))
        {
            command.Parameters.AddWithValue("@ORD_RECNO", id);

            command.ExecuteNonQuery();
            con.ConnetionClose();
            return true;
        }
    }
    //    public string Get_PadCode(int firmid)
    //    {
    //        conn con = new conn();
    //     System.Data.SqlClient.SqlDataReader reader = null;
    //     string v_sql = @"Select case when 
    //     isnull(SUBSTRING( max(ORD_PODCODE), 0, 3 ),0)='' then '0' else
    //     isnull(SUBSTRING( max(ORD_PODCODE), 0, 3 ),0)end _mounth1, 
    //     case when 
    //     isnull(SUBSTRING( max(ORD_PODCODE), 7, 9 ),'')='' then '1' else
    //     isnull(SUBSTRING( max(ORD_PODCODE), 7, 9 ),0) end mounthcount,CLC_ALLNAME from [dbo].[TBL_TRANSORDERS] 
    //     inner join TBL_CLCARDS on [ORD_CLCRECNO]=CLC_RECNO
    //     where ORD_CLCRECNO=" + firmid + @"
    //     group by CLC_ALLNAME";
    //        string padcode = "";
    //        reader = con.dbrun(v_sql);
    //        if (reader.Read())
    //        {
    //            int mounth = Convert.ToInt32(reader["_mounth1"]);
    //            string mounthcount = reader["mounthcount"].ToString();
    //            if (mounth == Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00")) - 201540)//201452
    //            {
    //                mounth = Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00")) - 201540;
    //                mounthcount = (Convert.ToInt32(reader["mounthcount"].ToString())+1).ToString("000");
    //            }
    //            else//201540
    //            {
    //                mounth = Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00")) - 201540;
    //                mounthcount = 1.ToString("000");
    //            }
    //            string value = reader["CLC_ALLNAME"].ToString();
    //            Char delimiter = ' ';
    //            string[] substrings = value.Split(delimiter);
    //            value = substrings[0].ToString();

    //            padcode = (mounth).ToString() + Convert.ToInt32(value.ToString()).ToString("0000") + mounthcount;
    //        }
    //        return padcode;
    //    }


    public string Get_PadCode_old(int firmid, int acttype, int ord_recno)
    {
        conn con = new conn();
        System.Data.SqlClient.SqlDataReader reader = null;

        reader = con.dbrun(@"select CLC_EXPCODE from TBL_CLCARDS where CLC_RECNO='" + firmid + "'");
        string CLC_CODE = "";
        if (reader.Read())
        {
            CLC_CODE = reader["CLC_EXPCODE"].ToString();
        }

        int mounth = 0;
        mounth = Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00")) - 201576;

        string v_sql = @"select '" + CLC_CODE + @"'+'" + acttype + @"' +'" + mounth + @"'+RIGHT(REPLICATE('0', 2) + CAST(cast(isnull(max(right(ORD_PODCODE,3)),0)+1 as varchar) AS VARCHAR(3)), 3) PODKOD
                     FROM (SELECT ORD_PODCODE
                      from TBL_TRANSORDERS 
                      where left(ORD_PODCODE,7)  = '" + CLC_CODE + "'+'" + acttype + @"' +'" + mounth + @"'
                      UNION ALL
                      SELECT GSL_SUBCODE
                      from TBL_GRSUBCODE_L 
                      where left(GSL_SUBCODE,7)  = '" + CLC_CODE + "'+'" + acttype + @"' +'" + mounth + @"' AND ISNULL(GSL_STATUS,0)<>-1
          ) X";


        reader = con.dbrun(v_sql);
        if (reader.Read())
        {
            return reader["PODKOD"].ToString();

        }

        return "";
    }

    public string Get_PadCode(int firmid, int acttype, int ord_recno)
    {
        conn con = new conn();
        System.Data.SqlClient.SqlDataReader reader = null;
        string year = "";
        string sql_query = @"SELECT ORD_FIRM,CASE ORD_FIRM 
                                WHEN 2 THEN   '7847' 
                                WHEN 12 THEN  '7848'
							    WHEN 11 THEN  '7848' 
                                WHEN 13 THEN  '8035' END EXPCODE
                                FROM TBL_TRANSORDERS WHERE ORD_RECNO=" + ord_recno;

        reader = con.dbrun(sql_query);
        //reader = con.dbrun();
        string CLC_CODE = "";
        if (reader.Read())
        {
            CLC_CODE = reader["EXPCODE"].ToString();
        }
        reader.Close();
        reader = con.dbrun(@"SELECT FORMAT(MONTH(getdate()),'00') as pdcyear");//
        while (reader.Read())
        {
            year = reader["pdcyear"].ToString();//2018
        }
        reader.Close();

        int mounth = 0;
        mounth = Convert.ToInt32("2016" + DateTime.Now.Month.ToString("00")) + 12 - 201576;

        string v_sql = "";
        string podkod = "";
        // podkod = CLC_CODE + acttype + mounth;
        podkod = year + CLC_CODE + acttype;//1878782
                                           // v_sql = @"select SUBSTRING(ORD_PODCODE,0,8) _type,SUBSTRING(ORD_PODCODE,8,3) _row,ORD_PODCODE from (
                                           //               SELECT ORD_PODCODE
                                           //               from TBL_TRANSORDERS 
                                           //               where left(ORD_PODCODE,7)  = '" + podkod + @"'
                                           //               UNION ALL
                                           //               SELECT GSL_SUBCODE
                                           //               from TBL_GRSUBCODE_L 
                                           //               where left(GSL_SUBCODE,7)  =  '" + podkod + @"' AND ISNULL(GSL_STATUS,0)<>-1
                                           //) dt";

        v_sql = @"select SUBSTRING(ORD_PODCODE,0,8) _type,SUBSTRING(ORD_PODCODE,8,3) _row,ORD_PODCODE from (
                      SELECT ORD_PODCODE
                      from TBL_TRANSORDERS 
                      where left(ORD_PODCODE,7)  = '" + podkod + @"'
                      UNION ALL
                      SELECT GSL_SUBCODE
                      from TBL_GRSUBCODE_L 
                      where left(GSL_SUBCODE,7)  =  '" + podkod + @"' AND ISNULL(GSL_STATUS,0)<>-1
					  ) dt";

        List<string> podkod_rows = new List<string>();
        Random r = new Random();
        reader = con.dbrun(v_sql);
        while (reader.Read())
        {
            podkod_rows.Add(reader["_row"].ToString());
        }


        while (true)
        {
            int random_count = r.Next(1, 999);
            bool result = true;
            // return podkod + random_count.ToString("000");
            for (int i = 0; i < podkod_rows.Count; i++)
            {
                if (random_count.ToString("000") == podkod_rows[i].ToString())
                {
                    result = false;
                }
            }
            if (result == true)
            {
                return podkod + random_count.ToString("000"); ;
                break;
            }
        }

        return "";
    }
    public string Get_PadCode_Caspian(int firmid, int acttype, int ord_recno)
    {
        conn con = new conn();
        System.Data.SqlClient.SqlDataReader reader = null;
        string year = "";
        //string sql_query = @"SELECT ORD_FIRM,CASE ORD_FIRM 
        //                        WHEN 2 THEN '7847' 
        //                        WHEN 12 THEN  '7848' 
        //                        ELSE '7853' END EXPCODE
        //                        FROM TBL_TRANSORDERS WHERE ORD_RECNO=" + ord_recno;

        //reader = con.dbrun(sql_query);
        //reader = con.dbrun();
        string CLC_CODE = "";
        //if (reader.Read())
        //{
        CLC_CODE = "5012";
        //reader["EXPCODE"].ToString();
        //}
        // CLC_CODE = "2090";
        //reader.Close();
        reader = con.dbrun(@"select right( YEAR(getdate()),2) as pdcyear");//
        while (reader.Read())
        {
            year = reader["pdcyear"].ToString();//2018
        }
        reader.Close();

        int mounth = 0;
        mounth = Convert.ToInt32("2016" + DateTime.Now.Month.ToString("00")) + 12 - 201576;

        string v_sql = "";
        string podkod = "";
        // podkod = CLC_CODE + acttype + mounth;
        podkod = CLC_CODE + year + acttype;//1878782
                                           // v_sql = @"select SUBSTRING(ORD_PODCODE,0,8) _type,SUBSTRING(ORD_PODCODE,8,3) _row,ORD_PODCODE from (
                                           //               SELECT ORD_PODCODE
                                           //               from TBL_TRANSORDERS 
                                           //               where left(ORD_PODCODE,7)  = '" + podkod + @"'
                                           //               UNION ALL
                                           //               SELECT GSL_SUBCODE
                                           //               from TBL_GRSUBCODE_L 
                                           //               where left(GSL_SUBCODE,7)  =  '" + podkod + @"' AND ISNULL(GSL_STATUS,0)<>-1
                                           //) dt";

        v_sql = @"select SUBSTRING(ORD_PODCODE,0,8) _type,SUBSTRING(ORD_PODCODE,8,3) _row,ORD_PODCODE from (
                      SELECT ORD_PODCODE
                      from TBL_TRANSORDERS 
                      where left(ORD_PODCODE,7)  = '" + podkod + @"'
                      UNION ALL
                      SELECT GSL_SUBCODE
                      from TBL_GRSUBCODE_L 
                      where left(GSL_SUBCODE,7)  =  '" + podkod + @"' AND ISNULL(GSL_STATUS,0)<>-1
					  ) dt";

        List<string> podkod_rows = new List<string>();
        Random r = new Random();
        reader = con.dbrun(v_sql);
        while (reader.Read())
        {
            podkod_rows.Add(reader["_row"].ToString());
        }


        while (true)
        {
            int random_count = r.Next(1, 999);
            bool result = true;
            // return podkod + random_count.ToString("000");
            for (int i = 0; i < podkod_rows.Count; i++)
            {
                if (random_count.ToString("000") == podkod_rows[i].ToString())
                {
                    result = false;
                }
            }
            if (result == true)
            {
                return podkod + random_count.ToString("000"); ;
                break;
            }
        }

        return "";
    }
    public string Get_PadCode_temp(int firmid, int acttype, int ord_recno)
    {
        conn con = new conn();
        System.Data.SqlClient.SqlDataReader reader = null;

        reader = con.dbrun(@"select CLC_EXPCODE from TBL_CLCARDS where CLC_RECNO='" + firmid + "'");
        string CLC_CODE = "";
        if (reader.Read())
        {
            CLC_CODE = reader["CLC_EXPCODE"].ToString();
        }

        int mounth = 0;
        mounth = Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00")) - 201588;

        string v_sql = "";
        string podkod = "";
        podkod = CLC_CODE + acttype + mounth;
        podkod = CLC_CODE + acttype + mounth;
        v_sql = @"select SUBSTRING(ORD_PODCODE,0,8) _type,SUBSTRING(ORD_PODCODE,8,3) _row,ORD_PODCODE from (
                      SELECT ORD_PODCODE
                      from TBL_TRANSORDERS 
                      where left(ORD_PODCODE,7)  = '" + podkod + @"'
                      UNION ALL
                      SELECT GSL_SUBCODE
                      from TBL_GRSUBCODE_L 
                      where left(GSL_SUBCODE,7)  =  '" + podkod + @"' AND ISNULL(GSL_STATUS,0)<>-1
                      UNION ALL
                      SELECT GSLT_SUBCODE
                      from TBL_GRSUBCODE_L_temp 
                      where left(GSLT_SUBCODE,7)  =  '" + podkod + @"' AND ISNULL(GSLT_STATUS,0)<>-1
                                    ) dt";

        List<string> podkod_rows = new List<string>();
        Random r = new Random();
        reader = con.dbrun(v_sql);
        while (reader.Read())
        {
            podkod_rows.Add(reader["_row"].ToString());
        }


        while (true)
        {
            int random_count = r.Next(1, 999);
            bool result = true;
            // return podkod + random_count.ToString("000");
            for (int i = 0; i < podkod_rows.Count; i++)
            {
                if (random_count.ToString("000") == podkod_rows[i].ToString())
                {
                    result = false;
                }
            }
            if (result == true)
            {
                return podkod + random_count.ToString("000"); ;
                break;
            }
        }

        return "";

    }

    public string Get_PadCode_temp_old(int firmid, int acttype, int ord_recno)
    {
        conn con = new conn();
        System.Data.SqlClient.SqlDataReader reader = null;

        reader = con.dbrun(@"select CLC_EXPCODE from TBL_CLCARDS where CLC_RECNO='" + firmid + "'");
        string CLC_CODE = "";
        if (reader.Read())
        {
            CLC_CODE = reader["CLC_EXPCODE"].ToString();
        }

        int mounth = 0;
        mounth = Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00")) - 201588;

        string v_sql = @"select '" + CLC_CODE + @"'+'" + acttype + @"' +'" + mounth + @"'+RIGHT(REPLICATE('0', 2) + CAST(cast(isnull(max(right(ORD_PODCODE,3)),0)+1 as varchar) AS VARCHAR(3)), 3) PODKOD
                     FROM (SELECT ORD_PODCODE
                      from TBL_TRANSORDERS 
                      where left(ORD_PODCODE,7)  = '" + CLC_CODE + "'+'" + acttype + @"' +'" + mounth + @"'
                      UNION ALL
                      SELECT GSLT_SUBCODE
                      from TBL_GRSUBCODE_L_temp 
                      where left(GSLT_SUBCODE,7)  = '" + CLC_CODE + "'+'" + acttype + @"' +'" + mounth + @"' AND ISNULL(GSLT_STATUS,0)<>-1
          ) X";


        reader = con.dbrun(v_sql);
        if (reader.Read())
        {
            return reader["PODKOD"].ToString();

        }

        return "";
    }




    public string Get_OrderNo()
    {
        System.Data.SqlClient.SqlDataReader reader = null;
        string maxno = "";
        reader = con.dbrun("SELECT max(SUBSTRING(ORD_FICHENO,6,19)) ORD_FICHENO FROM TBL_TRANSORDERS");
        if (reader.Read())
        {
            maxno = reader["ORD_FICHENO"].ToString();
        }

        string v_OrdNo = "";
        v_OrdNo = DateTime.Now.Year.ToString("0000").Substring(2, 2) + DateTime.Now.Month.ToString("00");
        if (maxno != "")
        {
            v_OrdNo = v_OrdNo + "-" + (int.Parse(maxno.Substring(maxno.IndexOf("-") + 1, 5)) + 1).ToString("00000");
        }
        else
        {
            v_OrdNo = v_OrdNo + "-00001";
        }
        return v_OrdNo;
    }
    public bool Update_NTSCODE(int id, string NTS)
    {
        string query = "";
        query = @"Update TBL_TRANSORDERS set ORD_NTS=@NTS  where ORD_RECNO=@ORD_RECNO";

        conn con = new conn();
        using (SqlCommand command = new SqlCommand(query, con.connectionOpen()))
        {
            command.Parameters.AddWithValue("@ORD_RECNO", id);
            command.Parameters.AddWithValue("@NTS", NTS);
            command.ExecuteNonQuery();
            con.ConnetionClose();
            return true;
        }
    }
    public double Get_Balance(int CLC_RECNO)
    {
        conn con = new conn();
        System.Data.SqlClient.SqlDataReader reader = null;
        reader = con.dbrun(@"declare  @client int
set @client=" + CLC_RECNO + @"
Select sum(p.Amount) Amount from
(
SELECT isnull(DBO.Get_CurrAccRest(@client),0) Amount
union all
Select -isnull(SUM([EXP_AMOUNT]),0) from [dbo].[TBL_TRANSORDERS]
inner join [dbo].[TBL_TRANSORDEREXP] on [EXP_ORDRECNO]=ORD_RECNO
where ORD_STATUS<>16 and ORD_STATUS<>15  and ORD_CLCRECNO=@client
UNION all
Select isnull(CLC_LIMIT,0)-isnull(CLC_BLOCK,0) from [dbo].[TBL_CLCARDS]
)p");
        if (reader.Read())
        {
            return double.Parse(reader["Amount"].ToString());
        }
        return 0;
    }

    public bool Stop_control(int clc_reno, string qnqcode)
    {
        System.Data.SqlClient.SqlDataReader reader = null;

        string v_sql = @"Select * from TBL_STOP where S_CLC_RECNO=" + clc_reno + @" AND S_TYPE=3 AND S_STATUS=0
        UNION ALL
        Select * from TBL_STOP where S_STC_CODE='" + qnqcode + @"' AND S_TYPE=2 AND S_STATUS=0
        UNION ALL
        Select * from TBL_STOP where S_CLC_RECNO=" + clc_reno + " AND S_STC_CODE='" + qnqcode + "' AND S_TYPE=1 AND S_STATUS=0";
        reader = con.dbrun(v_sql);
        if (reader.Read())
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    public void Set_assign_onlineorder(int O_ID, int B_ID, int CLC_RECNO)
    {
        System.Data.SqlClient.SqlDataReader v_reader = null;
        System.Data.SqlClient.SqlDataReader v_reader2 = null;
        int U_ID = 0;
        string v_sql = "";
        if (CLC_RECNO == 3259)//eger online musteridise random atacaq
        {
            v_sql = @"declare @B_ID int
            SET @B_ID=" + B_ID + @"
            Select TOP(1) U_NAME,U_ID,COUNT(OU_U_ID)  _count from T_SYS_USER
            inner join T_SYS_USERBR on U_ID=UB_U_ID
            LEFT join TBL_ORDERUSER on OU_U_ID=U_ID AND OU_TYPE='ONLINE'
            LEFT join TBL_TRANSORDERS_ONLINE on O_ID=OU_DOC_ID and O_ID=3259 --ancaq online musterisini sayacaq
            where U_TYPE=6 AND UB_B_ID=@B_ID AND U_STATUS=0
            group by U_NAME,U_ID
            order by COUNT(OU_U_ID),U_NAME";

            v_reader = con.dbrun(v_sql);
            if (v_reader.Read())
            {
                U_ID = int.Parse(v_reader["U_ID"].ToString());
            }
            v_reader.Close();

        }
        else //Qeydiyyatli musteri
        {
            v_sql = @"Select U_ID,U_NAME,COUNT(*)  from TBL_TRANSORDERS_ONLINE
            inner join T_SYS_USERCL on UC_CLC_ID=O_CLC_RECNO 
            inner join T_SYS_USER on U_ID=UC_U_ID
	    inner join T_SYS_USERBR on U_ID=UB_U_ID
            where O_ID=" + O_ID + @" and  O_CLC_RECNO<>3259 AND U_TYPE=6 AND UB_B_ID=" + B_ID + @"
			group by  U_ID,U_NAME
			order by COUNT(*),U_NAME";

            v_reader = con.dbrun(v_sql);
            if (v_reader.Read())
            {
                U_ID = int.Parse(v_reader["U_ID"].ToString());
            }
            v_reader.Close();

        }

        //string v_sql2 = @"";////yoxla burda table  dushmeyini insert getsin yoxsa update

        //v_sql = @"INSERT into TBL_ORDERUSER (OU_DOC_ID,OU_U_ID,OU_TYPE)
        //    values (" + O_ID + @"," + U_ID + ",'ONLINE')";
        //con.dbrun(v_sql);
    }
}