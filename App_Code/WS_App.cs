using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.IO;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for WS_App
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WS_App : System.Web.Services.WebService
{

    public WS_App()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Main_Data(string search,string dt1,string dt2)
    {
        conn con = new conn();
          string query = @"declare @date_str1 as nvarchar(10)
declare @date_str2 as nvarchar(10)
set @date_str1 = '"+dt1+@"'
set @date_str2 = '"+dt2+@"'
SELECT  C_NAME,C_CODE,
        SUM(IMPORT) as IMPORT,
		SUM(EXPORT) as EXPORT,
		SUM(TRANSIT) as TRANSIT,
		SUM(_LOCAL) as _LOCAL,
	    SUM(SALEPRC) as SALEPRC,
		SUM(PROFIT) as PROFIT FROM (
        SELECT 
        CLC_ALLNAME as C_NAME,ORD_CLCRECNO  as C_CODE,SUM(case when ORD_ACTTYPE=  1	 then 1 else 0 end) IMPORT,
        SUM(case when ORD_ACTTYPE=	2	 then 1 else 0 end) EXPORT,
        SUM(case when ORD_ACTTYPE=	3	 then 1 else 0 end) TRANSIT,
        SUM(case when ORD_ACTTYPE=	4	 then 1 else 0 end) _LOCAL,
        ROUND(SUM(ORD_STOTAL),2) SALEPRC,
        ROUND(SUM(ORD_PROFIT),2) PROFIT
        FROM TBL_TRANSORDERS
        LEFT JOIN TBL_CLCARDS on ORD_CLCRECNO=CLC_RECNO 
        LEFT JOIN TBL_INVOICE on ORD_RECNO=INV_ORD_RECNO
        WHERE  ORD_STATUS=17  AND INV_CURR_ID=3 AND CAST(ORD_CREATEDATE as date) BETWEEN @date_str1 AND @date_str2
        group by CLC_ALLNAME,ORD_CLCRECNO
        UNION ALL 
        SELECT 
        CLC_ALLNAME as C_NAME,ORD_CLCRECNO as C_CODE,SUM(case when ORD_ACTTYPE=  1	 then 1 else 0 end) IMPORT,
        SUM(case when ORD_ACTTYPE=	2	 then 1 else 0 end) EXPORT,
        SUM(case when ORD_ACTTYPE=	3	 then 1 else 0 end) TRANSIT,
        SUM(case when ORD_ACTTYPE=	4	 then 1 else 0 end) _LOCAL,
        ROUND(SUM(ORD_STOTAL/1.70),2) SALEPRC,
        ROUND(SUM(ORD_PROFIT/1.70),2) PROFIT
        FROM TBL_TRANSORDERS
        LEFT JOIN TBL_CLCARDS on ORD_CLCRECNO=CLC_RECNO 
        LEFT JOIN TBL_INVOICE on ORD_RECNO=INV_ORD_RECNO
        WHERE  ORD_STATUS=17  AND INV_CURR_ID=1 AND CAST(ORD_CREATEDATE as date) BETWEEN @date_str1 AND @date_str2
        group by CLC_ALLNAME,ORD_CLCRECNO )D ";

        if (search == "sdkj54884sds")
        {
            query += " group by C_NAME,C_CODE";
        }
        else { query += " WHERE C_NAME like '%" + search + "%'  group by C_NAME,C_CODE"; }
        string jsonstr = "";
        DataTable dt = new DataTable();
        dt = con.dbRunDt(query);
        jsonstr = JsonConvert.SerializeObject(dt);
        Context.Response.Write(jsonstr);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void INVM_Data(string search,string dt1,string dt2)
    {
        conn con = new conn();
        string query = @"declare @date_str1 as nvarchar(10)
declare @date_str2 as nvarchar(10)
set @date_str1 = '"+dt1+@"'
set @date_str2 = '"+dt2+@"'
SELECT INV_ORD_RECNO,CLC_ALLNAME as C_NAME,INV_NO,INV_TOTAL,CURR_CODE FROM TBL_INVOICE 
                                LEFT JOIN TBL_CLCARDS on INV_CLC_RECNO=CLC_RECNO
                                LEFT JOIN TBL_CURRENCY on INV_CURR_ID=CURR_ID
                                WHERE INV_TYPE=1 AND INV_CLC_RECNO<>9105 AND CAST(inv_date as date) BETWEEN @date_str1 AND @date_str2 ";
if(search=="sdkj54884sds"){
    
 }
else {  query +="AND  CLC_ALLNAME like '%" + search + "%' "; }


        string jsonstr = "";
        DataTable dt = new DataTable();
        dt = con.dbRunDt(query);
        jsonstr = JsonConvert.SerializeObject(dt);
        Context.Response.Write(jsonstr);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Client_Data(int ordclcrecno, string dt1, string dt2)
    {
        conn con = new conn();
        string query = @"  BEGIN
        DECLARE @ORDCLCRECNO  int 
        DECLARE @date_str1 as nvarchar(10)
        DECLARE @date_str2 as nvarchar(10)
        set @date_str1 = '" + dt1 + @"'
        set @date_str2 = '" + dt2 + @"'
        SET @ORDCLCRECNO = " + ordclcrecno + @"
        END
        SELECT ACT_TYPE,C_NAME,ACT_COUNT,SUM(ISNULL(BUY_PRC,0)) as BUY_PRC,SUM(ISNULL(SALE_PRC,0)) SALE_PRC,ROUND(SUM(ISNULL(SALE_PRC,0))-SUM(ISNULL(BUY_PRC,0)),2) as PROFIT FROM (
        SELECT SC_VALUE1 ACT_TYPE,CLC_ALLNAME C_NAME,COUNT(ISNULL(ORD_ACTTYPE,0)) ACT_COUNT,
        ROUND(SUM( ISNULL(ORD_PTOTAL ,0)),2) BUY_PRC ,
        ROUND(SUM( ISNULL(ORD_STOTAL ,0)),2) SALE_PRC  FROM TBL_TRANSORDERS
        LEFT JOIN TBL_INVOICE on ORD_RECNO=INV_ORD_RECNO
        LEFT JOIN TBL_SPECODES on ORD_ACTTYPE=SC_REFID AND SC_TYPE='ORD_TYPEACT'
        LEFT JOIN TBL_CLCARDS on ORD_CLCRECNO=CLC_RECNO 
        WHERE ORD_CLCRECNO=@ORDCLCRECNO AND  ORD_STATUS=17  AND INV_CURR_ID=3 AND CAST(ORD_CREATEDATE as date) BETWEEN @date_str1 AND @date_str2
        GROUP BY SC_VALUE1,CLC_ALLNAME,ORD_ACTTYPE
        UNION ALL
        SELECT SC_VALUE1 ACT_TYPE,CLC_ALLNAME C_NAME,COUNT(ISNULL(ORD_ACTTYPE,2)) ACT_COUNT,
        ROUND(SUM( ISNULL(ORD_PTOTAL,0))/1.70,2)  BUY_PRC ,
        ROUND(SUM( ISNULL(ORD_STOTAL,0))/1.70,2)  SALE_PRC
        FROM TBL_TRANSORDERS
        LEFT JOIN TBL_INVOICE on ORD_RECNO=INV_ORD_RECNO
        LEFT JOIN TBL_SPECODES on ORD_ACTTYPE=SC_REFID AND SC_TYPE='ORD_TYPEACT'
        LEFT JOIN TBL_CLCARDS on ORD_CLCRECNO=CLC_RECNO 
        WHERE ORD_CLCRECNO=@ORDCLCRECNO AND  ORD_STATUS=17  AND INV_CURR_ID=1 AND CAST(ORD_CREATEDATE as date) BETWEEN @date_str1 AND @date_str2
        GROUP BY SC_VALUE1,CLC_ALLNAME,ORD_ACTTYPE) D GROUP BY ACT_TYPE,C_NAME,ACT_COUNT
        UNION ALL
        SELECT 'Yekun' as ACT_TYPE,'' as C_NAME,SUM(ISNULL(ACT_COUNT,0)) ACT_COUNT,SUM(ISNULL(BUY_PRC,0)) as BUY_PRC,SUM(ISNULL(SALE_PRC,0)) SALE_PRC,ROUND(SUM(ISNULL(SALE_PRC,0))-SUM(ISNULL(BUY_PRC,0)),2) as PROFIT 
        FROM (
        SELECT COUNT(ISNULL(ORD_ACTTYPE,0)) ACT_COUNT,
        ROUND(SUM(ISNULL( ORD_PTOTAL ,0)),2) BUY_PRC ,
        ROUND(SUM(ISNULL( ORD_STOTAL,0)),2) SALE_PRC  FROM TBL_TRANSORDERS
        LEFT JOIN TBL_INVOICE on ORD_RECNO=INV_ORD_RECNO
        WHERE ORD_CLCRECNO=@ORDCLCRECNO AND  ORD_STATUS=17  AND INV_CURR_ID=3 AND CAST(ORD_CREATEDATE as date) BETWEEN @date_str1 AND @date_str2
        UNION ALL
        SELECT COUNT(ORD_ACTTYPE) ACT_COUNT,
        ROUND(SUM( ISNULL(ORD_PTOTAL /1.70,0)),2) BUY_PRC ,
        ROUND(SUM( ISNULL(ORD_STOTAL /1.70,0)),2) SALE_PRC  FROM TBL_TRANSORDERS
        LEFT JOIN TBL_INVOICE on ORD_RECNO=INV_ORD_RECNO
        WHERE ORD_CLCRECNO=@ORDCLCRECNO AND  ORD_STATUS=17  AND INV_CURR_ID=1 AND CAST(ORD_CREATEDATE as date) BETWEEN @date_str1 AND @date_str2
        ) D";
        string jsonstr = "";
        DataTable dt = new DataTable();
        dt = con.dbRunDt(query);
        jsonstr = JsonConvert.SerializeObject(dt);
        Context.Response.Write(jsonstr);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Payment_Data(int INV_CLCRECNO)
    {
        conn con = new conn();
        string query = @" BEGIN
        DECLARE @INVCLCRECNO  int 
        SET @INVCLCRECNO = "+ INV_CLCRECNO + @"
        END
        SELECT  1 P_TYPE,ROUND(SUM(INV_TOTAL),2) VAL,ROUND(SUM(INV_TOTAL_AZN),2) AZN FROM(
        SELECT SUM(INV_TOTAL/1.70) INV_TOTAL,SUM(INV_TOTAL_AZN) INV_TOTAL_AZN FROM TBL_INVOICE
        LEFT JOIN TBL_TRANSORDERS on INV_ORD_RECNO=ORD_RECNO
        WHERE INV_TYPE=1 AND INV_CURR_ID=1 AND ORD_STATUS=17 AND ORD_CLCRECNO=@INVCLCRECNO
        UNION ALL
        SELECT SUM(INV_TOTAL) INV_TOTAL,SUM(INV_TOTAL_AZN*1.70) INV_TOTAL_AZN FROM TBL_INVOICE
        LEFT JOIN TBL_TRANSORDERS on INV_ORD_RECNO=ORD_RECNO
        WHERE INV_TYPE=1 AND INV_CURR_ID=3 AND ORD_STATUS=17 AND ORD_CLCRECNO=@INVCLCRECNO) D  --Borc
        UNION ALL
        SELECT 2 as INV_TYPE, ROUND(SUM(isnull(BI_AMOUNT,0)+isnull(CI_AMOUNT,0)),2) VAL,ROUND(SUM(ISNULL(BI_AZNAMOUNT,0)+ISNULL(CI_AZNAMOUNT,0)),2) AZN
        FROM TBL_INVOICE 
        LEFT JOIN TBL_TRANSORDERS on INV_ORD_RECNO=ORD_RECNO 
        LEFT JOIN TBL_BANKINV on INV_ID=BI_INV_ID  
        LEFT JOIN TBL_CASHINV on INV_ID=CI_INV_ID  
        LEFT JOIN TBL_CURRENCY on INV_CURR_ID=CURR_ID
        WHERE INV_CLC_RECNO=@INVCLCRECNO AND INV_TYPE=1  AND ORD_STATUS=17
        GROUP BY INV_CLC_RECNO
        UNION ALL
        SELECT 3 as INV_TYPE,SUM(VAL) VAL,SUM(AZN) AZN FROM (
        SELECT SUM(ROUND(INV_TOTAL,2)) VAL,SUM(ROUND(INV_TOTAL_AZN,2)) AZN 
        FROM(
        SELECT SUM(INV_TOTAL/1.70) INV_TOTAL,SUM(INV_TOTAL_AZN) INV_TOTAL_AZN FROM TBL_INVOICE
        LEFT JOIN TBL_TRANSORDERS on INV_ORD_RECNO=ORD_RECNO
        WHERE INV_TYPE=1 AND INV_CURR_ID=1 AND ORD_STATUS=17 AND ORD_CLCRECNO=@INVCLCRECNO
        UNION ALL
        SELECT SUM(INV_TOTAL) INV_TOTAL,SUM(INV_TOTAL_AZN*1.70) INV_TOTAL_AZN FROM TBL_INVOICE
        LEFT JOIN TBL_TRANSORDERS on INV_ORD_RECNO=ORD_RECNO
        WHERE INV_TYPE=1 AND INV_CURR_ID=3 AND ORD_STATUS=17 AND ORD_CLCRECNO=@INVCLCRECNO
        ) A  --Borc
        UNION ALL
        SELECT -SUM(VAL),-SUM(AZN) FROM (
        SELECT  SUM(ROUND(isnull(BI_AMOUNT,0)+isnull(CI_AMOUNT,0),2)) VAL,SUM(ROUND(ISNULL(BI_AZNAMOUNT,0)+ISNULL(CI_AZNAMOUNT,0),2)) AZN
        FROM TBL_INVOICE 
        LEFT JOIN TBL_TRANSORDERS on INV_ORD_RECNO=ORD_RECNO 
        LEFT JOIN TBL_BANKINV on INV_ID=BI_INV_ID  
        LEFT JOIN TBL_CASHINV on INV_ID=CI_INV_ID  
        LEFT JOIN TBL_CURRENCY on INV_CURR_ID=CURR_ID
        WHERE INV_CLC_RECNO=@INVCLCRECNO AND INV_TYPE=1  AND ORD_STATUS=17
        GROUP BY INV_CLC_RECNO)A )D";
        string jsonstr = "";
        DataTable dt = new DataTable();
        dt = con.dbRunDt(query);
        jsonstr = JsonConvert.SerializeObject(dt);
        Context.Response.Write(jsonstr);
    }

[WebMethod]
    public void GetFile(int id)
    {
       conn con = new conn();
        string query = "";
        string filename = "http://31.171.73.66/IMSART_CONT";
        //string ff = "";
        query = @"SELECT 'http://31.171.73.66/IMSART_CONT'+''+Replace(UF_FILE,'~/','') UF_FILE FROM TBL_UPFILES WHERE UF_APP='ORD' and UF_DOCID=" + id;
        DataTable dt = new DataTable();
        dt = con.dbRunDt(query);
        //BinaryReader binReader = new BinaryReader(File.Open(Server.MapPath(filename), FileMode.Open, FileAccess.Read));
        //binReader.BaseStream.Position = 0;
        //byte[] binFile = binReader.ReadBytes(Convert.ToInt32(binReader.BaseStream.Length));
        //binReader.Close();
        string jsonstr = "";
        jsonstr = JsonConvert.SerializeObject(dt);
        Context.Response.Write(jsonstr);
    }

    [WebMethod]
    public void Get_LOGIN(string email,string password)
    {
        conn con = new conn();
        login lg = new login();
        global glb = new global();
        string query = "";
      query = @"Select U_TYPE from T_SYS_USER WHERE U_STATUS=0 and (U_EMAIL='" + email + @"' or U_USERNAME='" + email + @"') and
       ((case when isnull(U_CHANGEPASS,0)=1 then U_PASS else U_PASS2 end)='" + password + @"'
        OR
        (case when isnull(U_CHANGEPASS,0)=1 then U_PASS else U_PASS2 end)='" + glb.sha256_hash(password) + @"')";
        DataTable dt = new DataTable();
        dt = con.dbRunDt(query);
        string jsonstr = "";
        jsonstr = JsonConvert.SerializeObject(dt);
        Context.Response.Write(jsonstr);
    }

    [WebMethod]
    public void  Check_user(string username, string email)
    {
        conn con = new conn();
        Mail mm = new Mail();
        system sys = new system();
        System.Data.SqlClient.SqlDataReader reader = null;
        int status = 0;
        string sql_check = @"SELECT U_EMAIL,U_ID FROM T_SYS_USER
						     WHERE U_USERNAME = '" + username + "'  AND U_EMAIL = '" + email + "'";
        reader = con.dbrun(sql_check);
        if (reader.Read())
        { 
            status = 1;
            //string userId = reader["U_ID"].ToString() + "_" + reader["U_MAIL_STATUS"].ToString() + "_" + DateTime.Now.ToString("yyyy-MM-dd");
            string encryptedUser = system.Encrypt(reader["U_ID"].ToString()  + "_" + DateTime.Now.ToString("yyyy-MM-dd"));
            string body = "Şifrəni <a href=\"https://mm.program.az/Frm_EditUserInfo.aspx?user=" + encryptedUser + "\">buradan</a> dəyişin!";
            mm.send_mailRate(reader["U_EMAIL"].ToString(),"","Şifrə yeniləmə", body);

            con.dbrun("UPDATE T_SYS_USER SET U_MAIL_STATUS = 0 WHERE U_ID = " + reader["U_ID"].ToString());
        }
        else
        {
            status = 0;
        }
        Context.Response.Write(status);
    }

    [WebMethod]
    public void GetCtFiles(string ID, string _app)
    {
        System.Data.SqlClient.SqlDataReader reader = null;
        List<ContractFile> cfList = new List<ContractFile>();
        int count = 0;
        string v_sql = @"SELECT COUNT(UF_ID) CNT FROM [TBL_UPFILES]
WHERE UF_DOCID=" + ID + @" AND UF_APP='" + _app + "' AND UF_STATUS=1";

        System.Data.SqlClient.SqlDataReader v_reader = conn.fnc_dbrun(v_sql);
        while (v_reader.Read())
        {

            count = int.Parse(v_reader["CNT"].ToString());

        }
        v_reader.Close();
        string src = "";
        if (_app == "ORD")
        {
            v_sql = @"SELECT UF_UFILE,UF_FILE FROM [TBL_UPFILES]
                    WHERE UF_DOCID=(SELECT DISTINCT INVL_ORD_RECNO FROM TBL_INVOICELINE WHERE INVL_INV_ID=" + ID + @") AND UF_APP='" + _app + "' AND UF_STATUS=1";
        }
        else
        {
            v_sql = @"SELECT UF_UFILE, UF_FILE FROM [TBL_UPFILES]
            WHERE UF_DOCID=" + ID + @" AND UF_APP='" + _app + "' AND UF_STATUS=1";
        }

        reader = conn.fnc_dbrun(v_sql);
        while(reader.Read())
        {
            ContractFile cfModel = new ContractFile();
            cfModel.UF_UFILE = reader["UF_UFILE"].ToString();
            cfModel.UF_FILE = reader["UF_FILE"].ToString();
            cfList.Add(cfModel);
        }

        conn c = new conn();

        JavaScriptSerializer jss = new JavaScriptSerializer();

        //return clsList;
        Context.Response.Write(jss.Serialize(cfList));
    }

    public class ContractFile
    {
        public string UF_FILE { get; set; }
        public string UF_UFILE { get; set; }
    }


}
