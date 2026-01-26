using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

/// <summary>
/// Summary description for wb_Transport
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class wb_Transport : System.Web.Services.WebService
{
    login lg = new login();
    conn con = new conn();
    public wb_Transport()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }


    [WebMethod(EnableSession = true)]
    public void LoadCombo(int op)
    {
        System.Data.SqlClient.SqlDataReader reader = null;
        DataTable dt = new DataTable();

        List<ComboModel> clsList = new List<ComboModel>();
       
        reader = con.dbrun(GetQuery(op));



        try
        {
            while (reader.Read())
            {
                ComboModel cls = new ComboModel();
                cls.ID = reader["ID"].ToString();
                cls.NAME = reader["NAME"].ToString();
                clsList.Add(cls);
            }
        }
        catch (Exception)
        {

        }

        JavaScriptSerializer jss = new JavaScriptSerializer();

        //return clsList;
        Context.Response.Write(jss.Serialize(clsList));
    }



    private string GetQuery(int op)
    {
        switch (op)
        {
            case 1: //Dashima tipi
                return @"SELECT '' AS ID, SC_VALUE" + lg.Get_Lang() + @" NAME FROM TBL_SPECODES WHERE sc_type = 'EMPTY' and isnull(SC_STATUS,0) <>-1 
                            UNION ALL
                        Select SC_REFID ID, SC_VALUE" + lg.Get_Lang() + @" NAME FROM TBL_SPECODES WHERE sc_type = 'UOM' and isnull(SC_STATUS,0) <>-1 and SC_REFID<>0";
                

            case 2: //Mensubiyyet
                return @"
                        SELECT '' AS ID, SC_VALUE" + lg.Get_Lang() + @" NAME FROM TBL_SPECODES WHERE sc_type = 'EMPTY' and isnull(SC_STATUS,0) <>-1 
                             UNION ALL
                        SELECT SC_REFID ID, SC_VALUE" + lg.Get_Lang().ToString() + @" NAME FROM TBL_SPECODES WHERE SC_TYPE='VGN_OWNERTYPE' and isnull(SC_STATUS,0)=0 ";
                

            case 3:
                return @"
                        SELECT '' AS ID, SC_VALUE" + lg.Get_Lang() + @" NAME FROM TBL_SPECODES WHERE sc_type = 'EMPTY' and isnull(SC_STATUS,0) <>-1 
                            UNION ALL
                        SELECT VC_ID ID,VC_NAME" + lg.Get_Lang().ToString() + @" NAME FROM TBL_VAGONCATEGORY WHERE ISNULL(VC_STATUS, 0) <> -1";
                

            case 4:
                return @"
                        SELECT '' AS ID, SC_VALUE" + lg.Get_Lang() + @" NAME FROM TBL_SPECODES WHERE sc_type = 'EMPTY' and isnull(SC_STATUS,0) <>-1 
                            UNION ALL
                        SELECT VT_ID ID, VT_NAME" + lg.Get_Lang().ToString() + @" NAME FROM TBL_VAGONTYPE WHERE ISNULL(VT_STATUS, 0) <> -1";
                
            case 5:
                return @"
                        SELECT '' AS ID, SC_VALUE" + lg.Get_Lang() + @" NAME FROM TBL_SPECODES WHERE sc_type = 'EMPTY' and isnull(SC_STATUS,0) <>-1 
                            UNION ALL
                        SELECT RT_ID ID, RT_NAME" + lg.Get_Lang().ToString() + @" NAME FROM TBL_RTCTYPE WHERE RT_TYPE=2 AND ISNULL(RT_STATUS,0) <> -1 ";
                
            case 6:
                return @"
                        SELECT '' AS ID, SC_VALUE" + lg.Get_Lang() + @" NAME FROM TBL_SPECODES WHERE sc_type = 'EMPTY' and isnull(SC_STATUS,0) <>-1 
                            UNION ALL
                        SELECT SC_REFID ID, SC_VALUE" + lg.Get_Lang().ToString() + @" NAME FROM TBL_SPECODES WHERE SC_TYPE = 'CONT_TYPE' and ISNULL(SC_STATUS,0) <> -1";
                
            case 7:
                return @"SELECT '' AS ID, SC_VALUE" + lg.Get_Lang() + @" NAME FROM TBL_SPECODES WHERE sc_type = 'EMPTY' and isnull(SC_STATUS,0) <>-1 
                            UNION ALL
                        SELECT SC_REFID ID, SC_VALUE" + lg.Get_Lang().ToString() + @" NAME FROM TBL_SPECODES WHERE SC_CODE = 'EXP_TYPE' AND ISNULL(SC_STATUS,0) <> -1";
            case 8:
                return @"
                        SELECT '' AS ID, SC_VALUE" + lg.Get_Lang() + @" NAME FROM TBL_SPECODES WHERE sc_type = 'EMPTY' and isnull(SC_STATUS,0) <>-1 
                                        UNION ALL
                        SELECT RT_ID ID, RT_NAME" + lg.Get_Lang().ToString() + @" NAME FROM TBL_RTCTYPE WHERE RT_TYPE=1 AND ISNULL(RT_STATUS,0) <> -1 AND RT_ID in (58,53,2,3,18,43,27,28,29)";
            default:
                return "";
        }
    }

    [WebMethod(EnableSession = true)]
    public void LoadComboType(int trType,int category)
    {
        string sql = "";
        System.Data.SqlClient.SqlDataReader reader = null;
        DataTable dt = new DataTable();

        List<ComboModel> clsList = new List<ComboModel>();
        if (trType == 1)
        {
            sql = @"SELECT VT_ID ID, VT_NAME" + lg.Get_Lang().ToString() + @" NAME FROM TBL_VAGONTYPE WHERE VT_C_ID = "+ category +" AND ISNULL(VT_STATUS, 0) <> -1";
        }
        else
        {
            sql = @"SELECT SC_REFID ID, SC_VALUE1 NAME FROM TBL_SPECODES WHERE SC_TYPE = 'CONT_TYPE' AND ISNULL(SC_STATUS,0) <> -1";// AND  SC_PARENT = " + category;
        }
        reader = con.dbrun(sql);

        try
        {
            while (reader.Read())
            {
                ComboModel cls = new ComboModel();
                cls.ID = reader["ID"].ToString();
                cls.NAME = reader["NAME"].ToString();
                clsList.Add(cls);
            }
        }
        catch (Exception)
        {

        }

        JavaScriptSerializer jss = new JavaScriptSerializer();

        //return clsList;
        Context.Response.Write(jss.Serialize(clsList));
    }


    [WebMethod(EnableSession = true)]
    public void LoadTransport(string ORDID)
    {
        System.Data.SqlClient.SqlDataReader reader = null;

        LoadModel loadModel = new LoadModel();
        List<TransportLoadModel> trpLoadList = new List<TransportLoadModel>();
        PlatformLoadModel platformLoadModel = new PlatformLoadModel();

        try
        {
            if (int.Parse(ORDID) > 0)
            {
                string sql = @"
            Select  TRN_ID, 
            TRN_ID, SPEC_F.SC_REFID TRN_FullemptyId, TRN_TYPE.SC_REFID TrTypeId,
            ISNULL(OWNERTYPE.SC_REFID,0) OwnerId,
			--ISNULL(PLATOWNERTYPE.SC_REFID,0) PlatOwnerId,
			--ISNULL(PLAT_TYPE.RT_ID,0) PlatTypeId,
            CASE TRN_TRTYPE
				WHEN  1 THEN isnull(V_CATEGORY.VC_ID,'-')
				WHEN  2 THEN  G_TYPE.RT_ID
			END CategoryId,

		    CASE TRN_TRTYPE 
				WHEN 1 THEN V_TYPE.VT_ID
				WHEN 2 then SP_CONTYPE.SC_REFID
			END ConTypeId,
            SPEC_F.SC_VALUE1 TRN_Fullempty, TRN_TYPE.SC_VALUE" + lg.Get_Lang() + @" TrType, trn_PREFIX Prefix,TRN_NO No, 
            OWNERTYPE.SC_VALUE" + lg.Get_Lang() + @" Owner,
            CASE TRN_TRTYPE
				WHEN  1 THEN isnull(V_CATEGORY.VC_NAME" + lg.Get_Lang() + @",'-')
				WHEN  2 THEN  G_TYPE.RT_NAME" + lg.Get_Lang() + @"
			END Category,
            CASE TRN_TRTYPE 
				WHEN 1 THEN V_TYPE.VT_NAME" + lg.Get_Lang() + @"  
				WHEN 2 then SP_CONTYPE.SC_VALUE" + lg.Get_Lang() + @" 
			END ConType,
    		TRN_TNID TNID,
            ISNULL(TRN_COUNT,0) ContCount
            --ISNULL(PLATOWNERTYPE.SC_VALUE1,'') PlatOwner,
            --ISNULL(PLAT_TYPE.RT_NAME1,'') PlatType,
            --ISNULL(TRN_PLATCOUNT,0) PlatCount
            from TBL_TRANSPORTLIST
            left join TBL_SPECODES OWNERTYPE on OWNERTYPE.SC_REFID=TRN_OWNER and OWNERTYPE.SC_TYPE='VGN_OWNERTYPE' and isnull(OWNERTYPE.SC_STATUS,0) <>-1
            --left join TBL_SPECODES PLATOWNERTYPE on PLATOWNERTYPE.SC_REFID=TRN_PLATOWNER and PLATOWNERTYPE.SC_TYPE='VGN_OWNERTYPE' and isnull(PLATOWNERTYPE.SC_STATUS,0) <>-1
            left join TBL_SPECODES STYPE on STYPE.SC_REFID=TRN_STATUS and STYPE.SC_TYPE='NR_STATUS' and isnull(STYPE.SC_STATUS,0) <>-1
            left join TBL_SPECODES TRN_TYPE on TRN_TYPE.SC_REFID=TRN_TRTYPE and TRN_TYPE.SC_TYPE='UOM' and isnull(TRN_TYPE.SC_STATUS,0) <>-1
            left join TBL_RTCTYPE G_TYPE on G_TYPE.RT_ID=TRN_TRCAT and G_TYPE.RT_TYPE=2
            left join TBL_RTCTYPE PLAT_TYPE on PLAT_TYPE.RT_ID=TRN_PLATTYPE and PLAT_TYPE.RT_TYPE=1
            left join TBL_VAGONTYPE V_TYPE on V_TYPE.VT_ID=TRN_TYPE and isnull(V_TYPE.VT_STATUS,0) <>-1
            left join TBL_VAGONCATEGORY V_CATEGORY on V_CATEGORY.VC_ID=V_TYPE.VT_C_ID and isnull(V_CATEGORY.VC_STATUS,0) <>-1
	        left join TBL_SPECODES SP_CONTYPE ON SP_CONTYPE.SC_REFID = TRN_TYPE AND SP_CONTYPE.SC_TYPE = 'CONT_TYPE'
			LEFT JOIN TBL_SPECODES SPEC_F ON SPEC_F.SC_REFID =  TRN_FULLEMPTY  and SPEC_F.SC_TYPE = 'EXP_TYPE' AND ISNULL(SPEC_F.SC_STATUS,0) <> -1
			left join TBL_TRANSPORTPARK on TRN_PREFIX = TN_PREFNO AND TRN_NO = TN_NO
            WHERE ISNULL(TRN_STATUS,0) <> 0 AND TRN_ORDID = " + ORDID + " AND TRN_TRTYPE = 2";
                reader = con.dbrun(sql);

                while (reader.Read())
                {
                    TransportLoadModel trpLoadModel = new TransportLoadModel();
                    trpLoadModel.TRN_ID = int.Parse(reader["TRN_ID"].ToString());
                    trpLoadModel.TNID = int.Parse(reader["TNID"].ToString());
                    trpLoadModel.TRN_FullemptyId = reader["TRN_FullemptyId"].ToString();
                    trpLoadModel.TrTypeId = reader["TrTypeId"].ToString();
                    trpLoadModel.OwnerId = reader["OwnerId"].ToString();
                    trpLoadModel.CategoryId = reader["CategoryId"].ToString();
                    trpLoadModel.ConTypeId = reader["ConTypeId"].ToString();
                    trpLoadModel.TRN_Fullempty = reader["TRN_Fullempty"].ToString();
                    trpLoadModel.TrType = reader["TrType"].ToString();
                    trpLoadModel.Prefix = reader["Prefix"].ToString();
                    trpLoadModel.No = reader["No"].ToString();
                    trpLoadModel.Owner = reader["Owner"].ToString();
                    trpLoadModel.Category = reader["Category"].ToString();
                    trpLoadModel.ConType = reader["ConType"].ToString();
                    trpLoadModel.ContCount = reader["ContCount"].ToString();

                    trpLoadList.Add(trpLoadModel);
                }
                reader.Close();

                reader = con.dbrun(@" SELECT 
                        ISNULL(TRN_PLATOWNER,0) PlatOwner, 
                        ISNULL(TRN_PLATTYPE,0) PlatType, 
                        ISNULL(TRN_PLATCOUNT,0) PlatCount 
                        FROM TBL_TRANSPORTLIST
                        WHERE ISNULL(TRN_TRTYPE,0) = 1 AND TRN_ORDID = " + ORDID);
                if (reader.Read())
                {
                    platformLoadModel.PlatOwner = reader["PlatOwner"].ToString();
                    platformLoadModel.PlatType = reader["PlatType"].ToString();
                    platformLoadModel.PlatCount = reader["PlatCount"].ToString();
                }
                reader.Close();
            }

            loadModel.TransportLoadList = trpLoadList;
            loadModel.PlatformModel = platformLoadModel;
        }
        catch (Exception)
        {

            throw;
        }
        
        JavaScriptSerializer jss = new JavaScriptSerializer();

        //return clsList;
        Context.Response.Write(jss.Serialize(loadModel));

    }

    [WebMethod]
    public void GetCombos(string TRNID)
    {
        TransportLoadModel trpLoadModel = new TransportLoadModel();
        System.Data.SqlClient.SqlDataReader reader = null;
        try
        {
            reader = con.dbrun(@"SELECT  TRN_ID, TRN_FULLEMPTY, TRN_TRTYPE,TRN_PREFIX,TRN_NO,TRN_OWNER,TRN_TRCAT,TRN_TYPE FROM TBL_TRANSPORTLIST
                        WHERE TRN_ID = " + TRNID);

            while (reader.Read())
            {
                trpLoadModel.TRN_ID = int.Parse(reader["TRN_ID"].ToString());
                
                trpLoadModel.TrType = reader["TRN_TRTYPE"].ToString();
                trpLoadModel.TRN_Fullempty = reader["TRN_Fullempty"].ToString();
                trpLoadModel.Prefix = reader["TRN_PREFIX"].ToString();
                trpLoadModel.No = reader["TRN_NO"].ToString();
                trpLoadModel.Owner = reader["TRN_OWNER"].ToString();
                trpLoadModel.Category = reader["TRN_TRCAT"].ToString();
                trpLoadModel.ConType = reader["TRN_TYPE"].ToString();
            }
            reader.Close();
        }
        catch (Exception)
        {

            throw;
        }

        JavaScriptSerializer jss = new JavaScriptSerializer();

        //return clsList;
        Context.Response.Write(jss.Serialize(trpLoadModel));

    }

    [WebMethod]
    public void DeleteTransport(string TRNID)
    {
        con.dbrun(@"UPDATE TBL_TRANSPORTLIST
	                    SET TRN_STATUS = 0
	                    WHERE TRN_ID =" + TRNID);
    }

    [WebMethod]
    public void DeleteTransportAll(string ORDID)
    {
        con.dbrun(@"UPDATE TBL_TRANSPORTLIST
	                SET TRN_STATUS = 0
	                WHERE TRN_TRTYPE = 2 AND TRN_ORDID =" + ORDID);
    }


    [WebMethod]
    public void SaveOperation(string transportOpr, string platform, string ordid, string ficheClient)
    {
        System.Data.SqlClient.SqlDataReader reader = null;

        List<TransportSaveModel> trpList = JsonConvert.DeserializeObject<List<TransportSaveModel>>(transportOpr);
        List<ContCountModel> cList =  new List<ContCountModel>();
        ListSaveResponseModel saverespmodel = new ListSaveResponseModel();  

        try
        {
            reader = con.dbrun(@"Select Replace('txt_ORD_CONTAINAMOUNT' + SC_VALUE1,' FT','') InputId, SC_REFID ContType, 1 as FullEmpty from TBL_SPECODES where SC_TYPE='CONT_TYPE' and isnull(sc_status, 0)<>-1
                                 Union all
                                 Select Replace('txt_ORD_ECONTAINAMOUNT' + SC_VALUE1,' FT','') InputId, SC_REFID ContType, 2 as FullEmpty from TBL_SPECODES where SC_TYPE='CONT_TYPE' and isnull(sc_status, 0)<>-1");

            while (reader.Read())
            {
                ContCountModel contModel = new ContCountModel();
                string inputId = reader["InputId"].ToString();
                int contTypeId = Convert.ToInt32(reader["ContType"]);
                int fullEmpty = Convert.ToInt32(reader["FullEmpty"]);
                
                contModel.Id = inputId;
                contModel.ContCount = trpList.Count(q => q.TRN_FullemptyId == fullEmpty && q.ConTypeId == contTypeId);
               
                // Add full/empty
                contModel.ContCount += trpList.Count(q => q.TRN_FullemptyId == 3 && q.ConTypeId == contTypeId);
                cList.Add(contModel);
            }

            saverespmodel.ContCountInfo = cList;
            saverespmodel.platform = platform;
            saverespmodel.transportOpr = transportOpr;
        }
        catch (Exception ex)
        {  }

        JavaScriptSerializer jss = new JavaScriptSerializer();
        Context.Response.Write(jss.Serialize(saverespmodel));

    }

    [WebMethod]
    public void CheckWagonNo(string wagonno)
    {
        string no = "0";
        int no_1 = 0;
        int no_2 = 0;
        int no_3 = 0;
        int no_4 = 0;
        int no_5 = 0;
        int no_6 = 0;
        int no_7 = 0;
        int no_8 = 0;
        if (wagonno.Length == 8)
        {
            for (int i = 1; i <= wagonno.Length; i++)
            {
                no = wagonno.Substring(i - 1, 1);
                if (i == 1)
                {
                    no_1 = int.Parse(no) * 2;
                }
                else if (i == 2)
                {
                    no_2 = int.Parse(no) * 1;
                }
                else if (i == 3)
                {
                    no_3 = int.Parse(no) * 2;
                }
                else if (i == 4)
                {
                    no_4 = int.Parse(no) * 1;
                }
                else if (i == 5)
                {
                    no_5 = int.Parse(no) * 2;
                }
                else if (i == 6)
                {
                    no_6 = int.Parse(no) * 1;
                }
                else if (i == 7)
                {
                    no_7 = int.Parse(no) * 2;
                }
                else if (i == 8)
                {
                    no_8 = int.Parse(no) * 1;
                }
            }
            int no1 = 0;
            for (int i = 1; i <= no_1.ToString().Length; i++)
            {
                no1 = no1 + int.Parse(no_1.ToString().Substring(i - 1, 1));
            }
            int no2 = 0;
            for (int i = 1; i <= no_2.ToString().Length; i++)
            {
                no2 = no2 + int.Parse(no_2.ToString().Substring(i - 1, 1));
            }
            int no3 = 0;
            for (int i = 1; i <= no_3.ToString().Length; i++)
            {
                no3 = no3 + int.Parse(no_3.ToString().Substring(i - 1, 1));
            }
            int no4 = 0;
            for (int i = 1; i <= no_4.ToString().Length; i++)
            {
                no4 = no4 + int.Parse(no_4.ToString().Substring(i - 1, 1));
            }
            int no5 = 0;
            for (int i = 1; i <= no_5.ToString().Length; i++)
            {
                no5 = no5 + int.Parse(no_5.ToString().Substring(i - 1, 1));
            }
            int no6 = 0;
            for (int i = 1; i <= no_6.ToString().Length; i++)
            {
                no6 = no6 + int.Parse(no_6.ToString().Substring(i - 1, 1));
            }
            int no7 = 0;
            for (int i = 1; i <= no_7.ToString().Length; i++)
            {
                no7 = no7 + int.Parse(no_7.ToString().Substring(i - 1, 1));
            }
            int no8 = 0;
            for (int i = 1; i <= no_8.ToString().Length; i++)
            {
                no8 = no8 + int.Parse(no_8.ToString().Substring(i - 1, 1));
            }

            if ((no1 + no2 + no3 + no4 + no5 + no6 + no7 + no8).ToString().Substring(1, 1) == "0")
            {
                Context.Response.Write(1);
            }
            else
            {
                Context.Response.Write(0);
            }
        }
        else
        {

        }
        
    }

    [WebMethod]
    public void CheckPark(string prefix, string no)
    {
        System.Data.SqlClient.SqlDataReader reader = null;
        string sql = @"SELECT TN_ID FROM TBL_TRANSPORTPARK
                        WHERE TN_PREFNO = '" + prefix + "' AND TN_NO = '" + no + "'";
        reader = con.dbrun(sql);
        if (reader.Read())
        {
            Context.Response.Write(Convert.ToInt32(reader["TN_ID"]));
        }
        else
        {
            Context.Response.Write(0);
        }
    }


    [WebMethod]
    public void CheckParkArray(string kontList)
    {
        //DataModel dtModel = new DataModel();
         System.Data.SqlClient.SqlDataReader reader = null;
        List<string> trpList = JsonConvert.DeserializeObject<List<string>>(kontList);
        
        List<KontDetailModel>  kontDetails = new List<KontDetailModel>();   
       

        foreach (var item in trpList)
        {
            KontDetailModel kontDetail = new KontDetailModel();
            string prefix = item.Substring(0, 4);
            string kont = item.Substring(4);

            reader = con.dbrun(@"SELECT TN_ID FROM TBL_TRANSPORTPARK
                        WHERE TN_PREFNO = '" + prefix + "' AND TN_NO = '" + kont + "'");

            kontDetail.Prefix = prefix;
            kontDetail.No = kont;

            if (reader.Read())
            {
                //resultList.Add(1);
                kontDetail.TNID = Convert.ToInt32(reader["TN_ID"]);
            }
            else
            {
                //resultList.Add(2);
                kontDetail.TNID = 0;

            }
            kontDetails.Add(kontDetail);
        } 

        //dtModel.mainInfo = dataMain;
        //dtModel.KontStatus = resultList;


        JavaScriptSerializer jss = new JavaScriptSerializer();
        Context.Response.Write(jss.Serialize(kontDetails));


    }


    public class ComboModel
    {
        public string ID { get; set; }
        public string NAME { get; set; }
    }

    public class TransportSaveModel
    {
        public int TRN_ID { get; set; }
        public int ORDID { get; set; }
        public int TrTypeId { get; set; }
        public int TRN_FullemptyId { get; set; } 
        public string TRN_Fullempty { get; set; }
        public string TrType { get; set; }
        public string Prefix { get; set; }
        public string No { get; set; }
        public int OwnerId { get; set; }
        public string Owner { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int ConTypeId { get; set; }
        public string ConType { get; set; }

        public int ContCount { get; set; }
        public string TrpList { get; set; }
        public int TNID { get; set; }
    }

    public class PlatformSaveModel
    {
        public int PlatOwner { get; set; }
        public int PlatType { get; set; }
        public int PlatCount { get; set; }
    }

    public class LoadModel
    {
        public List<TransportLoadModel> TransportLoadList { get; set; }
        public PlatformLoadModel PlatformModel { get; set; }
    }
    public class TransportLoadModel
    {
        public int TRN_ID { get; set; }
        public int TNID { get; set; }
        public string TRN_FullemptyId { get; set; }
        public string TrTypeId { get; set; }
        public string OwnerId { get; set; }
        public string CategoryId { get; set; }
        public string ConTypeId { get; set; }
        public string PlatOwnerId { get; set; }
        public string PlatTypeId { get; set; }
        public string TRN_Fullempty { get; set; }
        public string TrType { get; set; }
        public string Prefix { get; set; }
        public string No { get; set; }
        public string Owner { get; set; }
        public string Category { get; set; }
        public string ConType { get; set; }
        public string PlatOwner { get; set; }
        public string PlatType { get; set; }
        public string PlatCount { get; set; }
        public string ContCount { get; set; }
    }

    public class PlatformLoadModel
    {
        public string PlatOwner { get; set; }
        public string PlatType { get; set; }
        public string PlatCount { get; set; }
    }

    public class ContType
    {
        public int con3 { get; set; }
        public int con5 { get; set; }
        public int con10 { get; set; }
        public int con20 { get; set; }
        public int con30 { get; set; }

        public int con40 { get; set; }
        public int con45 { get; set; }
        public int econ3 { get; set; }
        public int econ5 { get; set; }
        public int econ10 { get; set; }
        public int econ20 { get; set; }
        public int econ30 { get; set; }
        public int econ40 { get; set; }
        public int econ45 { get; set; }


    }

    public class DataModel
    {
        public List<int> KontStatus { get; set; }
        public TransportLoadModel mainInfo { get; set; }
    }

    public class KontDetailModel
    {
        public int TNID { get; set; }
        public string Prefix { get; set; }
        public string No { get; set; }
        
    }

    public class ContCountModel
    {
        public string Id { get; set; }
        public int ContCount { get; set; }
    }

    public class ListSaveResponseModel
    {
        public List<ContCountModel> ContCountInfo { get; set; }
        public int OwnerId { get; set; }
        public int CategoryId { get; set; }
        public int PlatOwner { get; set; }
        public int PlatType { get; set; }
        public int PlatCount { get; set; }

        public string transportOpr { get; set; }
        public string platform { get; set; }
    }

}
