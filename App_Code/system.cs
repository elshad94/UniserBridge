using DevExpress.CodeParser;
using DevExpress.Utils.OAuth.Provider;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;



/// <summary>
/// Summary description for sql_load
/// </summary>
public class system
{
    NotificationHub notification = new NotificationHub();
    conn con = new conn();
    login lg = new login();
    Mail mn = new Mail();
    static string CURR_DATE;
    static float CURR_RATE;
    string resultvservice = "";

    public List<Omodul> Get_modul(int user_id)
    {
        List<Omodul> List_modul = new List<Omodul>();

        string query = "";
        query = @"select * from T_SYS_APP inner join T_SYS_USERAPP ON UA_U_ID=@userid AND UA_PERM=1 AND UA_APP_ID=APP_ID";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        cmd.Parameters.AddWithValue("@userid", user_id);
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_modul.Add(new Omodul
            {
                ID = Convert.ToInt32(reader["APP_ID"]),
                CODE = reader["APP_CODE"].ToString(),
                Header = reader["APP_NAMEaz-LATN"].ToString(),
                URL = reader["APP_URL"].ToString(),
                IMAGEURL = reader["APP_IMAGEURL"].ToString(),
                COLOR = reader["APP_COLOR"].ToString()
            });
        }
        return List_modul;
    }
    public List<Omenu> Get_menu(int user_type, int app_id)
    {
        List<Omenu> List_menu = new List<Omenu>();

        string query = "";
        query = @"SELECT MNU_ID ID, MNU_CAPTION" + lg.Get_Lang() + @" as Header, MNU_ORDERBY, MNU_LEVEL, MNU_PAGEURL, MNU_CSSURL from T_SYS_MENUS 
            inner join T_SYS_USERMENU on MNU_ID=UM_MNU_ID
			inner join T_SYS_MENUSBR on MNB_MNU_ID=UM_MNU_ID
			--inner join T_SYS_USERBR on MNB_B_ID=UB_B_ID  
            WHERE   MNU_STATUS=0   and isnull(MNB_STATUS,0)=0 and  UM_U_TYPE=" + lg.Get_Usertype() + @"
			GROUP BY MNU_ID , MNU_CAPTION" + lg.Get_Lang() + @" , MNU_ORDERBY, MNU_LEVEL, MNU_PAGEURL, MNU_CSSURL
            order by MNU_ORDERBY";

        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_menu.Add(new Omenu
            {
                ID = Convert.ToInt32(reader["ID"]),
                HEADER = reader["Header"].ToString(),
                LAVEL = reader["MNU_LEVEL"].ToString(),
                URL = reader["MNU_PAGEURL"].ToString(),
                CSS = reader["MNU_CSSURL"].ToString(),
                ORDER = reader["MNU_ORDERBY"].ToString()
            });
        }

        return List_menu;
    }
    public List<string> Get_country()
    {
        List<string> list_data = new List<string>();
        foreach (CultureInfo info in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
        {
            RegionInfo inforeg = new RegionInfo(info.LCID);
            if (!list_data.Contains(inforeg.EnglishName))
            {
                list_data.Add(inforeg.EnglishName);
                list_data.Sort();
            }
        }
        return list_data;
    }
    public string GetCurrentPageName()
    {
        string Path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo Info = new System.IO.FileInfo(Path);
        string pageName = Info.Name;
        return pageName;
    }
    public List<Ospecode> GET_Company(int user_id)
    {
        List<Ospecode> List_data = new List<Ospecode>();
        string query = "";
        query = @"SELECT C_NAME text, C_ID value FROM T_GL_COMPANY
        INNER JOIN T_SYS_USERCOMP ON C_ID=CU_C_ID AND CU_U_ID=@user_id";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        cmd.Parameters.AddWithValue("@user_id", user_id);
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_data.Add(new Ospecode
            {
                value = Convert.ToInt32(reader["value"].ToString()),
                text = reader["text"].ToString()
            });
        }

        return List_data;
    }
    public List<Ospecode> Get_Firms(int type)
    {
        List<Ospecode> List_data = new List<Ospecode>();
        string query = "";
        query = @"SELECT B_ID,B_NAME1 FROM T_SYS_BRANCH WHERE B_STATUS=0";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        cmd.Parameters.AddWithValue("@type", type);
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_data.Add(new Ospecode
            {
                value = Convert.ToInt32(reader["B_ID"].ToString()),
                text = reader["B_NAME1"].ToString()
            });
        }

        return List_data;
    }
    public List<Ospecode> Get_Specode(string type)
    {
        List<Ospecode> List_data = new List<Ospecode>();
        string query = "";
        
        query = @"SELECT SC_REFID value, SC_VALUE" + lg.Get_Lang().ToString() + @" text FROM TBL_SPECODES WHERE SC_TYPE=@type and isnull(SC_STATUS,0)=0 ";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        cmd.Parameters.AddWithValue("@type", type);
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_data.Add(new Ospecode
            {
                value = Convert.ToInt32(reader["value"].ToString()),
                text = reader["text"].ToString()
            });
        }

        return List_data;
    }
    public List<Ospecode> Get_Stations()
    {
        List<Ospecode> List_data = new List<Ospecode>();
        string query = "";
        query = @"SELECT STN_RECNO value, STN_CODE, STN_NAME" + lg.Get_Lang().ToString() + @" text FROM TBL_STATIONS ORDER BY STN_NAME" + lg.Get_Lang().ToString();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_data.Add(new Ospecode
            {
                value = Convert.ToInt32(reader["value"].ToString()),
                text = reader["text"].ToString()
            });
        }

        return List_data;
    }
    public List<Ospecode> Get_Borders()
    {
        List<Ospecode> List_data = new List<Ospecode>();
        string query = "";
        query = @"Select  [PNT_RECNO] value,ISNULL([PNT_CODE], '')+' '+ISNULL([PNT_NAME], '') text from TBL_POINTS where [PNT_TYPE]=1 and isnull([PNT_STATUS],0)=0";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_data.Add(new Ospecode
            {
                value = Convert.ToInt32(reader["value"].ToString()),
                text = reader["text"].ToString()
            });
        }

        return List_data;
    }
    public List<Ospecode> Get_Points()
    {
        List<Ospecode> List_data = new List<Ospecode>();
        string query = "";
        query = @"SELECT [PNT_RECNO] value, ISNULL([PNT_CODE], '')+' '+ISNULL([PNT_NAME], '') text FROM [TBL_POINTS] WHERE isnull(PNT_TYPE,0)=1  ORDER BY [PNT_NAME]";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_data.Add(new Ospecode
            {
                value = Convert.ToInt32(reader["value"].ToString()),
                text = reader["text"].ToString()
            });
        }

        return List_data;
    }
    public List<Ospecode> GET_EXP(int type, int firmid)//o-->General 1-->Expiditor
    {
        List<Ospecode> List_data = new List<Ospecode>();
        string query = "";
        if (type == 0)
        {
            query = @"Select FRM_RECNO value,FRM_NAME text from TBL_FIRMS";
        }
        else if (type == 1)
        {
            // query = @"Select CLC_RECNO value,CLC_ALLNAME  text from TBL_CLCARDS where [CLC_FIRM]='" + firmid + "' order by CLC_ALLNAME";
            query = @"Select CLC_RECNO value,CLC_ALLNAME  text from TBL_CLCARDS 
                        
                        where CLC_TYPE=2 AND ISNULL(CLC_STATUS,0)<>-1 order by CLC_ALLNAME";

        }
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_data.Add(new Ospecode
            {
                value = Convert.ToInt32(reader["value"].ToString()),
                text = reader["text"].ToString()
            });
        }

        return List_data;
    }
    public string SET_Login(string GLOBALIP, string SESSIONID, string LOCALIP, string SL_ID, string Username, string passport, int user_id, int type = 0)
    {
        //if (LOCALIP == "")
        //{
        string query = "";
        query = @"Insert into T_SYS_LOG 
            (SL_GLOBALIP,SL_LOCALIP,SL_SESSIONID,SL_USERNAME,SL_PASSWORD,SL_U_ID,SL_TYPE) 
            values 
            (@SL_GLOBALIP,@SL_LOCALIP,@SL_SESSIONID,@SL_USERNAME,@SL_PASSWORD,@SL_U_ID,@SL_TYPE) ";
        using (SqlCommand command = new SqlCommand(query, con.connectionOpen()))
        {
            command.Parameters.AddWithValue("@SL_GLOBALIP", GLOBALIP);
            command.Parameters.AddWithValue("@SL_LOCALIP", LOCALIP);
            command.Parameters.AddWithValue("@SL_SESSIONID", SESSIONID);
            command.Parameters.AddWithValue("@SL_USERNAME", Username);
            command.Parameters.AddWithValue("@SL_PASSWORD", passport);
            command.Parameters.AddWithValue("@SL_U_ID", user_id);
            command.Parameters.AddWithValue("@SL_TYPE", type);

            System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return reader["ID"].ToString();
            }
            return null;
        }
        //}
        //else
        //{
        //    string query = @"Update T_SYS_LOG  set  SL_LOCALIP=@SL_LOCALIP where SL_ID=@SL_ID";
        //    using (SqlCommand command = new SqlCommand(query, con.connectionOpen()))
        //    {
        //        command.Parameters.AddWithValue("@SL_LOCALIP", LOCALIP);
        //        command.Parameters.AddWithValue("@SL_ID", SL_ID);
        //        command.ExecuteNonQuery();
        //        return "";
        //    }
        //}
    }
    public float GetPrice(int roudtype, int wagontype, int rtype, float ton, float diss, float wagon, float ewagon, float kt, float ekt, string code, int owner)
    {
        string query = "";
        query = @"SELECT * FROM TBL_RTCTYPE WHERE RT_ID=" + wagontype + "";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        string function_ie = "";
        string function_t = "";
        float price = 0;
        try
        {
            while (reader.Read())
            {

                function_ie = reader["RT_PRICEF_IE"].ToString();
                function_t = reader["RT_PRICEF_T"].ToString();

            }
            reader.Close();
            //@RTYPE INT,@TON float,@DISS float,@WAGON FLOAT,@EWAGON FLOAT,@KT FLOAT,@EKT FLOAT,@ITEMCODE NVARCHAR(20),@OWNER INT
            if (roudtype == 1 || roudtype == 2)//im exp
            {
                query = "SELECT dbo." + function_ie + @"(" + rtype + "," + ton + "," + diss + "," + wagon + "," + ewagon + "," + kt + "," + ekt + ",'" + code + "'," + owner + ") AS PRICE";

            }
            else if (roudtype == 3)//tranzit
            {
                query = "SELECT dbo." + function_t + @"(" + rtype + "," + ton + "," + diss + "," + wagon + "," + ewagon + "," + kt + "," + ekt + ",'" + code + "'," + owner + ") AS PRICE";

            }


            cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                price = float.Parse(reader["PRICE"].ToString());
            }
            reader.Close();
        }
        catch
        {
            price = 0;
        }

        if (price.ToString() == "Infinity")
        {
            price = 0;
        }
        return price;
    }
    public float GetPriceE(int roudtype, int wagontype, int rtype, float ton, float diss, float wagon, float ewagon, float kt, float ekt, string code, int owner)
    {
        string query = "";
        query = @"SELECT * FROM TBL_RTCTYPE WHERE RT_ID=" + wagontype + "";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        string function = "";
        string arcount = "0";
        float price = 0;

        while (reader.Read())
        {

            function = reader["RT_EPRICE"].ToString();
            arcount = reader["RT_ARRCOUNT"].ToString();

        }
        reader.Close();
        //@RTYPE INT,@TON float,@DISS float,@WAGON FLOAT,@EWAGON FLOAT,@KT FLOAT,@EKT FLOAT,@ITEMCODE NVARCHAR(20),@OWNER INT

        query = "SELECT dbo." + function + @"(" + arcount + "," + diss + "," + owner + ") AS PRICE";


        try
        {

            cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                price = float.Parse(reader["PRICE"].ToString());


            }
            reader.Close();
        }
        catch
        { price = 0; }

        if (price.ToString() == "Infinity")
        {
            price = 0;
        }


        return price;
    }
    public List<Ospecode> Get_WagonType(int type)
    {
        List<Ospecode> List_data = new List<Ospecode>();
        string query = "";
        query = @" SELECT RT_ID value, RT_NAME" + lg.Get_Lang().ToString() + @" text FROM TBL_RTCTYPE where isnull(RT_STATUS,0)<>-1 and RT_TYPE=@type ORDER BY RT_ORDERBY";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        cmd.Parameters.AddWithValue("@type", type);
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            if (type == 1)
            {
                if (Convert.ToInt32(reader["value"].ToString()) == 58 || Convert.ToInt32(reader["value"].ToString()) == 53 || Convert.ToInt32(reader["value"].ToString()) == 2 || Convert.ToInt32(reader["value"].ToString()) == 3 || Convert.ToInt32(reader["value"].ToString()) == 18 ||
                    Convert.ToInt32(reader["value"].ToString()) == 18 || Convert.ToInt32(reader["value"].ToString()) == 43 || Convert.ToInt32(reader["value"].ToString()) == 27 || Convert.ToInt32(reader["value"].ToString()) == 28 || Convert.ToInt32(reader["value"].ToString()) == 29
                   )
                {
                    List_data.Add(new Ospecode
                    {
                        value = Convert.ToInt32(reader["value"].ToString()),
                        text = reader["text"].ToString()
                    });
                }
            }
            else
                List_data.Add(new Ospecode
                {
                    value = Convert.ToInt32(reader["value"].ToString()),
                    text = reader["text"].ToString()
                });
        }

        return List_data;
    }
    public List<OBank> Get_BankList()
    {
        List<OBank> List_data = new List<OBank>();
        string query = "";
        query = @"select SC_REFID BA_ID,SC_VALUE" + lg.Get_Lang() + @" BANK_NAME,'' BANK_VOEN,'' BA_NO,'' BANK_CODE,'' BANK_SWIFT,'' BA_IBAN,''  BA_ACC from dbo.TBL_SPECODES where SC_TYPE='EMPTY' 
        UNION ALL
        Select BA_ID,CASE BANK_ID WHEN 14 THEN  BANK_NAME+' LLC' ELSE BANK_NAME END ,TBL_BANK.BANK_VOEN ,BA_NO,BANK_CODE,BANK_SWIFT,BA_IBAN,BA_ACC
        from TBL_BANK
        inner join TBL_BANKACC on BANK_ID=BA_BANK_ID
        INNER JOIN T_SYS_USERBR ON UB_B_ID=BA_B_ID
        INNER JOIN T_SYS_USER ON U_ID=UB_U_ID
        INNER JOIN T_SYS_BRANCH ON B_ID=UB_B_ID
        WHERE U_ID=" + lg.Get_userid() + @" and BA_STATUS=0 and BA_ID<>99";

        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_data.Add(new OBank
            {
                ID = int.Parse(reader["BA_ID"].ToString()),
                NAME = reader["BANK_NAME"].ToString(),
                VOEN = reader["BANK_VOEN"].ToString(),
                NO = reader["BA_NO"].ToString(),
                CODE = reader["BANK_CODE"].ToString(),
                SWIFT = reader["BANK_SWIFT"].ToString(),
                IBAN = reader["BA_IBAN"].ToString(),
                ACC = reader["BA_ACC"].ToString()
            });
        }

        return List_data;
    }
    public string NumberToWords(int number)
    {
        if (number == 0)
            return "sıfır";

        if (number < 0)
            return "mənfi " + NumberToWords(Math.Abs(number));

        string words = "";

        if ((number / 1000000) > 0)
        {
            words += NumberToWords(number / 1000000) + " miliyon ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWords(number / 1000) + " min ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords(number / 100) + " yüz ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "")
                words += "";

            var unitsMap = new[] { "sıfır", "bir", "iki", "üç", "dörd", "beş", "altı", "yeddi", "səkkiz", "doqquz", "on", "on bir", "on iki", "on üç", "on dörd", "on beş", "on altı", "on yeddi", "on səkkiz", "on doqquz" };
            var tensMap = new[] { "sıfır", "on", "iyirmi", "otuz", "qırx", "əlli", "altımış", "yetmiş", "səksən", "doxsan" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) != 0)
                    words += " " + unitsMap[number % 10];
            }
        }
        return words;
    }
    public List<OSTATUS> Get_Satus()

    {
        System.Data.SqlClient.SqlDataReader v_reader = null;
        List<OSTATUS> List_data = new List<OSTATUS>();
        string query = "";
        if (lg.Get_userid() == 3358)
        {
            query = @"Select S_ID,S_CODE,S_NAME" + lg.Get_Lang().ToString() + @" Text, S_COLOR from TBL_STATUS 
            inner join TBL_STATUS_USER on S_ID=SU_S_ID
            where SU_U_TYPE=" + lg.Get_Usertype() + @" and isnull(S_STATUS,0)=0 and S_ID in (1,2,14,4)
	    GROUP BY S_ID,S_CODE,S_NAME" + lg.Get_Lang().ToString() + @" ,S_COLOR,S_ORDER
            order by S_ID asc";
        }
        else if (lg.Get_userid() == 3335)
        {
            query = @"Select S_ID,S_CODE,S_NAME1 Text, S_COLOR from TBL_STATUS 
                        inner join TBL_STATUS_USER on S_ID=SU_S_ID
                        where SU_U_TYPE=6 and isnull(S_STATUS,0)=0 and S_ID in (18,19,20)
                        GROUP BY S_ID,S_CODE,S_NAME1 ,S_COLOR,S_ORDER
                        order by S_ID asc";
        }
        else
        {
            query = @"Select S_ID,S_CODE,S_NAME" + lg.Get_Lang().ToString() + @" Text,S_COLOR from TBL_STATUS 
            inner join TBL_STATUS_USER on S_ID=SU_S_ID
            where SU_U_TYPE=" + lg.Get_Usertype() + @" and isnull(S_STATUS,0)=0
	    GROUP BY S_ID,S_CODE,S_NAME" + lg.Get_Lang().ToString() + @" ,S_COLOR,S_ORDER
            order by S_ID asc";
        }
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_data.Add(new OSTATUS
            {
                ID = int.Parse(reader["S_ID"].ToString()),
                NAME = reader["S_CODE"].ToString() + ". " + reader["Text"].ToString(),
                COLOR = reader["S_COLOR"].ToString()
            });
        }
        return List_data;
    }

    public void Set_Status(int id, int merhele, int branch, string userId = null)
    {
        System.Data.SqlClient.SqlDataReader v_reader = null;
        if (userId == null)
        {
            userId = lg.Get_userid().ToString();
        }
        string v_sql = @"DECLARE @RC int
                              DECLARE @P_ORDID int
                              DECLARE @P_MERHELE int
                              DECLARE @P_USRID int
                              DECLARE @P_BRANCH int
                              DECLARE @R_RESULTINT INT 
                              DECLARE @R_RESULTSTR NVARCHAR(500)

                              SET @P_ORDID =" + id + @"
                              SET @P_MERHELE =" + merhele + @"
                              SET @P_USRID =" + userId + @"
                              SET @P_BRANCH =" + branch + @"
                              EXECUTE @RC = [dbo].[FNC_SETPROCESS_tst] 
                                                           @P_ORDID
                                                          ,@P_MERHELE
                                                          ,@P_USRID
                                                          ,@P_BRANCH
                                                          ,@R_RESULTINT OUTPUT
                                                          ,@R_RESULTSTR OUTPUT

                              SELECT @R_RESULTINT RES_INT, @R_RESULTSTR RES_STR";
        v_reader = con.dbrun(v_sql);

        if (v_reader.Read())
        {
            System.Data.SqlClient.SqlDataReader sub_reader = null;
            sub_reader = con.dbrun(@"select U_EMAIL,SM_QUERY,SM_TYPE,isnull(U_ID,0) U_ID from T_SYS_MAILRULE  
            LEFT join T_SYS_USER on U_ID=SM_U_ID 
            where SM_S_ID=" + v_reader["RES_INT"].ToString() + @" and SM_B_ID=" + branch + " and SM_STATUS=0");
            while (sub_reader.Read())
            {
                System.Data.SqlClient.SqlDataReader sub_reader2 = null;
                string query = sub_reader["SM_QUERY"].ToString().Replace("<ORD_RECNO>", id.ToString());
                sub_reader2 = con.dbrun(query);
                while (sub_reader2.Read())
                {

                    notification.SendNotifications();
                    #region MyRegion
                    //int USER_ID = int.Parse(sub_reader["U_ID"].ToString());
                    //if (sub_reader["SM_TYPE"].ToString() == "1")
                    //{
                    //    if (USER_ID != 112)
                    //    {
                    //        //  mn.send_mail(sub_reader["U_EMAIL"].ToString(), "System", "New Order",
                    //        //     "Order No: " + sub_reader2["ORD_FICHENO"].ToString() + "</br>" + "CLIENT: " + sub_reader2["CLC_ALLNAME"].ToString());
                    //    }
                    //}
                    //else
                    //{
                    //    if (USER_ID != 112)
                    //    {
                    //        if (int.Parse(sub_reader2["U_ID"].ToString()) != lg.Get_userid())
                    //        {
                    //            //   mn.send_mail(sub_reader2["U_EMAIL"].ToString(), "System", "New Order",
                    //            //   "Order No: " + sub_reader2["ORD_FICHENO"].ToString() + "</br>" + "CLIENT: " + sub_reader2["CLC_ALLNAME"].ToString());

                    //        }
                    //    }
                    //}
                    #endregion

                }
            }
        }
    }

    public float GetPriceC(int roudtype, int wagontype, int rtype, float ton, float diss, float wagon, float ewagon, float kt, float ekt, string code, int owner, string fpoint, string tpoint)
    {
        string query = "";
        query = @"SELECT * FROM TBL_RTCTYPE WHERE RT_ID=" + wagontype + "";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        string function_ie = "";
        string function_t = "";
        float price = 0;
        try
        {
            while (reader.Read())
            {

                function_ie = reader["RT_PRICEF_IE"].ToString();
                function_t = reader["RT_PRICEF_T"].ToString();

            }
            reader.Close();
            //@RTYPE INT,@TON float,@DISS float,@WAGON FLOAT,@EWAGON FLOAT,@KT FLOAT,@EKT FLOAT,@ITEMCODE NVARCHAR(20),@OWNER INT
            if (roudtype == 1 || roudtype == 2)//im exp
            {
                query = "SELECT dbo." + function_ie + @"(" + rtype + "," + ton + "," + diss + "," + wagon + "," + ewagon + "," + kt + "," + ekt + ",'" + code + "'," + owner + "," + fpoint + "," + tpoint + ") AS PRICE";

            }
            else if (roudtype == 3)//tranzit
            {
                query = "SELECT dbo." + function_t + @"(" + rtype + "," + ton + "," + diss + "," + wagon + "," + ewagon + "," + kt + "," + ekt + ",'" + code + "'," + owner + "," + fpoint + "," + tpoint + ") AS PRICE";

            }


            cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                price = float.Parse(reader["PRICE"].ToString());


            }
            reader.Close();
        }
        catch
        {

            price = 0;
        }
        return price;
    }
    public string GetPriceMess(int roudtype, int wagontype, int rtype, float ton, float diss, float wagon, float ewagon, float kt, float ekt, string code, int owner, int bpoint, int epoint, string fpoint, string tpoint, int clc_type, int clc_id)
    {
        string query = @"declare @MSG Nvarchar(MAX) = ''

EXEC [dbo].GET_PRICE_MESS_PR '1','" + wagontype + @"','" + roudtype + @"','" + ton + @"','" + diss + @"','" + wagon + @"','" + ewagon + @"','" + kt + @"','" + ekt + @"','" + code + @"','" + owner + @"','" + bpoint + @"','" + epoint + @"','" + fpoint + @"','" + tpoint + @"','" + clc_type + @"','" + clc_id + @"',@MSG  = @MSG output    select @MSG PRICE
";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        string price = "";
        try
        {


            cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                price = reader["PRICE"].ToString();


            }
            reader.Close();
        }
        catch
        {

            price = "";
        }
        return price;
    }
    public List<Ospecode> Get_FirmCode(string CLC_RECNO)
    {
        List<Ospecode> List_data = new List<Ospecode>();
        string query = "";
        query = @"declare @type int
            declare @CLCRECNO int
            set @CLCRECNO=" + CLC_RECNO + @"
            set @type=(select Sum(distinct dt.CLCRECNO) from (
            select 0 CLCRECNO 
            UNION ALL
            select  isnull(FC_CLC_RECNO,0) from TBL_FIRMCODE where FC_CLC_RECNO=@CLCRECNO AND isnull(FC_STATUS,0)=0)dt)


            select * from TBL_FIRMCODE where FC_CLC_RECNO=@type AND isnull(FC_STATUS,0)=0";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_data.Add(new Ospecode
            {
                value = Convert.ToInt32(reader["FC_ID"].ToString()),
                text = reader["FC_CODE"].ToString()
            });
        }

        return List_data;
    }

    public float Get_WEIGHT(string QNQCODE, float WEIGHT)
    {
        float RESULT_WEIGHT = 0;
        System.Data.SqlClient.SqlDataReader v_reader = null;
        string v_sql = @"SELECT isnull(DBO.MIN_TONNAJ('" + QNQCODE + @"'),0) as TONNAJ";
        v_reader = con.dbrun(v_sql);
        if (v_reader.Read())
        {

            if (WEIGHT <= float.Parse(v_reader["TONNAJ"].ToString()))
            {
                return RESULT_WEIGHT = float.Parse(v_reader["TONNAJ"].ToString());
            }
        }


        v_sql = @"SELECT isnull(DBO.WEIGHT_CAT('" + WEIGHT + "'),0) TONNAJ";
        v_reader = con.dbrun(v_sql);
        if (v_reader.Read())
        {
            if (v_reader["TONNAJ"].ToString() != "null")
            {
                return RESULT_WEIGHT = float.Parse(v_reader["TONNAJ"].ToString());
            }
        }
        return RESULT_WEIGHT;
    }

    public List<OUSERGRID> Get_GridHeader(string GridName)
    {
        List<OUSERGRID> List_data = new List<OUSERGRID>();
        string query = "";
        query = @"select LNG_OBJECT,LNG_CAPTION" + lg.Get_Lang().ToString() + @" LNG_CAPTION,isnull(LNG_WIDTH,60) WIDTH,UG_STATUS VISIBLE, UG_FILTER FILTER,isnull(UG_ORDER,0) _ORDER,LNG_FORMAT _FORMAT
       from [dbo].[TBL_LANGUAGE] 
       inner join [dbo].[T_SYS_USERGRID] on LNG_ID=UG_LNG_ID
       where LNG_OBJTYPE='" + GridName + "' and  isnull(LNG_TYPE,0)=0 and [UG_U_ID]='" + lg.Get_userid() + "'    order by LNG_ORDER ";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_data.Add(new OUSERGRID
            {
                FIELD = reader["LNG_OBJECT"].ToString(),
                HEADER = reader["LNG_CAPTION"].ToString(),
                WIDTH = int.Parse(reader["WIDTH"].ToString()),
                ORDER = int.Parse(reader["_ORDER"].ToString()),
                FILTER = reader["FILTER"].ToString(),
                VISIBLE = bool.Parse(reader["VISIBLE"].ToString()),
                FORMAT = reader["_FORMAT"].ToString()
            });
        }


        return List_data;
    }

    public bool Set_GridHeader(int column_ID, int Usergrid_id, bool Visible)
    {
        login lg = new login();
        string query = "";
        if (Usergrid_id == 0) //Bu hallda colum teze acilib ve mu userin orda id-si yoxdur o column gormek istese insert edeceyik
        {
            query = @" Insert into T_SYS_USERGRID(UG_LNG_ID, UG_U_ID, UG_STATUS)
		    values (@UG_LNG_ID, @UG_U_ID, @UG_STATUS)";
        }
        else
        {
            query = @"Update  T_SYS_USERGRID set UG_STATUS=@UG_STATUS where UG_LNG_ID=@UG_LNG_ID and UG_U_ID=@UG_U_ID";
        }
        conn con = new conn();
        using (SqlCommand command = new SqlCommand(query, con.connectionOpen()))
        {
            command.Parameters.AddWithValue("@UG_LNG_ID", column_ID);
            command.Parameters.AddWithValue("@UG_U_ID", lg.Get_userid());
            command.Parameters.AddWithValue("@UG_STATUS", Visible);

            command.ExecuteNonQuery();
            con.ConnetionClose();
            return true;
        }

    }

    public int GET_DISTANCE(string POINTID1, string POINTID2)
    {
        try
        {
            System.Data.SqlClient.SqlDataReader v_reader = null;
            int distance = 0;
            string query = @"EXEC GET_DISTANCE @PNT_RECNO1='" + POINTID1 + "',@PNT_RECNO2='" + POINTID2 + "'";
            v_reader = con.dbrun(query);
            if (v_reader.Read())
            {
                distance = int.Parse(v_reader["RESULT"].ToString());
            }
            return distance;
        }
        catch { return 0; }
    }
    public float GetPriceNew(int RTCID, int TRTYPE, int ORDTYPE, float TON, float DISS, float WAGON, float EWAGON, float KT, float EKT, string ITEMCODE, int OWNER, int BPOINT, int EPOINT, string FPOINT, string TPOINT, int CLC_TYPE, int CLC_ID, int PRICETYPE, int EXPYPE)
    {
        float pricea = 0;
        float prices = 0;
        string query2 = @"
                     
declare @MSG_ALISH Nvarchar(MAX) = ''
declare @PR_SATISH Nvarchar(MAX) = ''
EXEC DBO.CALC_PRICE  '1','" + RTCID + @"','" + TRTYPE + @"','" + ORDTYPE + @"','" + TON + @"','" + DISS + @"','" + WAGON + @"','" + EWAGON + @"','" + KT + @"','" + EKT + @"','" + ITEMCODE + @"','" + OWNER + @"','" + BPOINT + @"','" + EPOINT + @"','" + FPOINT + @"','" + TPOINT + @"','" + CLC_TYPE + @"','" + CLC_ID + @"','" + EXPYPE + @"', @MSG_ALISH  = @MSG_ALISH output ,@PR_SATISH  = @PR_SATISH output    
EXEC('SELECT '+@MSG_ALISH +'PRICEA,' +@PR_SATISH+' PRICES')";
        System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand(query2, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader2 = cmd2.ExecuteReader();
        while (reader2.Read())
        {
            pricea = float.Parse(reader2["PRICEA"].ToString());
            //if (EXPYPE == 3)
            //{
            //    pricea = pricea / GetCurrency(DateTime.Now, "usd");
            //}
            prices = float.Parse(reader2["PRICES"].ToString());
        }


        if (PRICETYPE == 0)
        { return pricea; }
        else
        { return prices; }

    }
    public string GetPriceMessNew(int RTCID, int TRTYPE, int ORDTYPE, float TON, float DISS, float WAGON, float EWAGON, float KT, float EKT, string ITEMCODE, int OWNER, int BPOINT, int EPOINT, string FPOINT, string TPOINT, int CLC_TYPE, int CLC_ID, int PRICETYPE, int EXPYPE)
    {
        string pricea = "";
        string prices = "";
        string app_no = "";

        string query2 = @"
declare @APP_NO Nvarchar(MAX) = ''
declare @MSG_ALISH Nvarchar(MAX) = ''
declare @MSG_SATISH Nvarchar(MAX) = ''
EXEC DBO.CALC_PRICE  '1','" + RTCID + @"','" + TRTYPE + @"','" + ORDTYPE + @"','" + TON + @"','" + DISS + @"','" + WAGON + @"','" + EWAGON + @"','" + KT + @"','" + EKT + @"','" + ITEMCODE + @"','" + OWNER + @"','" + BPOINT + @"','" + EPOINT + @"','" + FPOINT + @"','" + TPOINT + @"','" + CLC_TYPE + @"','" + CLC_ID + @"','" + EXPYPE + @"', @MSG_ALISH  = @MSG_ALISH output ,@MSG_SATISH  = @MSG_SATISH output,@APP_NO  = @APP_NO output  select @MSG_ALISH PRICEA,@MSG_SATISH PRICES,@APP_NO APPENDIX 
EXEC('SELECT '+@MSG_ALISH +'PRICEA,' +@MSG_SATISH+'PRICES,N'''+@APP_NO+''' APPENDIX')";
        System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand(query2, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader2 = cmd2.ExecuteReader();
        while (reader2.Read())
        {

            pricea = reader2["PRICEA"].ToString();
            prices = reader2["PRICES"].ToString();
            app_no = reader2["APPENDIX"].ToString();

        }


        if (PRICETYPE == 0)
        { return pricea; }
        else if (PRICETYPE == 1)
        { return prices; }
        else if (PRICETYPE == 2)
        { return app_no; }
        else
        {
            return "";
        }
    }
    public List<Ospecode> Get_Country()
    {
        List<Ospecode> List_data = new List<Ospecode>();
        string query = "";
        query = @"Select C_ID,C_NAME" + lg.Get_Lang() + " C_NAME from TBL_COUNTRY";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_data.Add(new Ospecode
            {
                value = Convert.ToInt32(reader["C_ID"].ToString()),
                text = reader["C_NAME"].ToString()
            });
        }

        return List_data;
    }

    public string GetEtsnq(string qnq)
    {

        string v_sql = @"
DECLARE @E NVARCHAR(MAX)=''

EXEC DBO.QNQ '" + qnq + @"' ,@E=@E OUTPUT

EXEC('Select * from 
               (Select 0 _ID, STC_ID , STC_CODE GNG_CODE, STC_NAME GNG_NAME, STC_ID ETS_ID,STC_CODE ETS_CODE, STC_NAME ETS_NAME
                FROM TBL_STCARDS WHERE STC_STTYPE=3 AND STC_STATUS=0
                UNION ALL
			    SELECT ROW_NUMBER() 
                OVER (ORDER BY GNG.STC_CODE,ETS.STC_CODE) AS _ID, 
                isnull(GNG.STC_ID,-1) STC_ID, GNG.STC_CODE GNG_CODE, GNG.STC_NAME GNG_NAME,
                isnull(ETS.STC_ID,-1) ETS_ID, ETS.STC_CODE ETS_CODE, ETS.STC_NAME ETS_NAME
                FROM (SELECT * FROM TBL_STCARDS WHERE STC_STTYPE=1 AND STC_STATUS=0) GNG
                LEFT JOIN TBL_STJCARDS GE ON GE.STCJ_GNG_CODE=GNG.STC_CODE
                LEFT JOIN (SELECT * FROM TBL_STCARDS WHERE STC_STTYPE=2 AND STC_STATUS=0) ETS ON GE.STCJ_ETS_CODE=ETS.STC_CODE
				)emp
				WHERE ETS_CODE IN ('+@E+') AND GNG_CODE =''" + qnq + "''')";
        return v_sql;

    }
    public PricesMess GetPriceMessNew1(int RTCID, int TRTYPE, int ORDTYPE, float TON, float DISS, float WAGON, float EWAGON, float KT, float EKT, int MEXQTY, int EMEXQTY, int PTQTY, int PTOWNER, int PTRTCID, string ITEMCODE, string ITEMCODE_ETSNQ, int OWNER, int BPOINT, int EPOINT, string FPOINT, string TPOINT, int CLC_TYPE, int CLC_ID, int PRICETYPE, int EXPYPE, string datetime, int origin, out string pricea, out string prices, out string pricea_total, out string prices_total, out string app_no)
    {
        pricea = "";

        prices = "";

        pricea_total = "";

        prices_total = "";

        app_no = "";

        string query2 = @"
declare @APP_NO Nvarchar(MAX) = ''
declare @MSG_ALISH Nvarchar(MAX) = ''
declare @MSG_SATISH Nvarchar(MAX) = ''
declare @TOTAL_ALISH Nvarchar(MAX) = ''
declare @TOTAL_SATISH Nvarchar(MAX) = ''
EXEC DBO.CALC_PRICE11  '1','" + RTCID + @"','" + TRTYPE + @"','" + ORDTYPE + @"','" + TON + @"','" + DISS + @"','" + WAGON + @"','" + EWAGON + @"','" + KT + @"','" + EKT + @"','" + ITEMCODE + @"','" + ITEMCODE_ETSNQ + @"','" + OWNER + @"','" + BPOINT + @"','" + EPOINT + @"','" + FPOINT + @"','" + TPOINT + @"','" + CLC_TYPE + @"','" + CLC_ID + @"','" + EXPYPE + @"','" + datetime + @"','" + origin + @"','" + MEXQTY + @"','" + EMEXQTY + @"','" + PTQTY + @"','" + PTOWNER + @"','" + PTRTCID + @"',@MSG_ALISH  = @MSG_ALISH output ,@MSG_SATISH  = @MSG_SATISH output, @TOTAL_ALISH  = @TOTAL_ALISH output ,@TOTAL_SATISH  = @TOTAL_SATISH output,@APP_NO  = @APP_NO output  select @MSG_ALISH PRICEA,@MSG_SATISH PRICES,@TOTAL_ALISH PRICEAT,@TOTAL_SATISH PRICEST,@APP_NO APPENDIX 
EXEC('SELECT '+@MSG_ALISH +'PRICEA,' +@MSG_SATISH+'PRICES,'+@TOTAL_ALISH +'PRICEAT,' +@TOTAL_SATISH+'PRICEST,N'''+@APP_NO+''' APPENDIX')";
        System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand(query2, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader2 = cmd2.ExecuteReader();
        while (reader2.Read())
        {
            pricea = reader2["PRICEA"].ToString();

            prices = reader2["PRICES"].ToString();

            pricea_total = reader2["PRICEAT"].ToString();

            prices_total = reader2["PRICEST"].ToString();

            app_no = reader2["APPENDIX"].ToString();

        }

        PricesMess price = new PricesMess();

        price.appno = app_no;

        price.price_a = pricea;
        price.price_s = prices;

        price.price_a_total = pricea_total;
        price.price_s_total = prices_total;


        return price;
    }
    public Prices GetPriceNew1(int RTCID, int TRTYPE, int ORDTYPE, float TON, float DISS, float WAGON, float EWAGON, float KT, float EKT, int MEXQTY, int EMEXQTY, int PTQTY, int PTOWNER, int PTRTCID, string ITEMCODE, string ITEMCODE_ETSNQ, int OWNER, int BPOINT, int EPOINT, string FPOINT, string TPOINT, int CLC_TYPE, int CLC_ID, int PRICETYPE, int EXPYPE, string datetime, int origin, out float pricea, out float prices, out float pricea_total, out float prices_total, out int qty)
    {
        qty = 0;
        pricea = 0;

        prices = 0;

        pricea_total = 0;

        prices_total = 0;


        string query2 = @"
DECLARE @QTY INT=0                     
declare @MSG_ALISH Nvarchar(MAX) = ''
declare @PR_SATISH Nvarchar(MAX) = ''
declare @PRTOTAL_ALISH Nvarchar(MAX) = ''
declare @PRTOTAL_SATISH Nvarchar(MAX) = ''
EXEC DBO.CALC_PRICE11  '1','" + RTCID + @"','" + TRTYPE + @"','" + ORDTYPE + @"','" + TON + @"','" + DISS + @"','" + WAGON + @"','" + EWAGON + @"','" + KT + @"','" + EKT + @"','" + ITEMCODE + @"','" + ITEMCODE_ETSNQ + @"','" + OWNER + @"','" + BPOINT + @"','" + EPOINT + @"','" + FPOINT + @"','" + TPOINT + @"','" + CLC_TYPE + @"','" + CLC_ID + @"','" + EXPYPE + @"','" + datetime + @"','" + origin + @"','" + MEXQTY + @"','" + EMEXQTY + @"','" + PTQTY + @"','" + PTOWNER + @"','" + PTRTCID + @"', @MSG_ALISH  = @MSG_ALISH output ,@PR_SATISH  = @PR_SATISH output,@PRTOTAL_ALISH  = @PRTOTAL_ALISH output ,@PRTOTAL_SATISH  = @PRTOTAL_SATISH output , @QTY  = @QTY output  
EXEC('SELECT '+@QTY +' QTY, '+@MSG_ALISH +'PRICEA,' +@PR_SATISH+' PRICES,' +@PRTOTAL_ALISH+' PRICEAT,' +@PRTOTAL_SATISH+' PRICEST')";
        System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand(query2, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader2 = cmd2.ExecuteReader();
        while (reader2.Read())
        {
            pricea = float.Parse(reader2["PRICEA"].ToString());

            prices = float.Parse(reader2["PRICES"].ToString());

            pricea_total = float.Parse(reader2["PRICEAT"].ToString());

            prices_total = float.Parse(reader2["PRICEST"].ToString());

            qty = int.Parse(reader2["QTY"].ToString());
        }
        if (EXPYPE == 3)
        {
            try
            {
                pricea = pricea / global.GetCurrency(DateTime.Now, "USD");
                pricea_total = pricea_total / global.GetCurrency(DateTime.Now, "USD");
            }
            catch 
            {

                pricea = 0;
                pricea_total = 0;
          
            }
          
        }
        Prices price = new Prices();

        price.qty = qty;

        price.price_a = pricea;
        price.price_s = prices;

        price.price_a_total = pricea_total;
        price.price_s_total = prices_total;


        return price;
    }

    public Prices GetPriceNew_ONLINE(int RTCID, int TRTYPE, int ORDTYPE, float TON, float DISS, float WAGON, float EWAGON, float KT, float EKT, int MEXQTY, int EMEXQTY, int PTQTY, int PTOWNER, int PTRTCID, string ITEMCODE, string ITEMCODE_ETSNQ, int OWNER, int BPOINT, int EPOINT, string FPOINT, string TPOINT, int CLC_TYPE, int CLC_ID, int PRICETYPE, int EXPYPE, string datetime, int origin, out float pricea, out float prices, out float pricea_total, out float prices_total, out int qty)
    {
        qty = 0;
        pricea = 0;

        prices = 0;

        pricea_total = 0;

        prices_total = 0;


        string query2 = @"
DECLARE @QTY INT=0                     
declare @MSG_ALISH Nvarchar(MAX) = ''
declare @PR_SATISH Nvarchar(MAX) = ''
declare @PRTOTAL_ALISH Nvarchar(MAX) = ''
declare @PRTOTAL_SATISH Nvarchar(MAX) = ''
EXEC DBO.CALC_PRICE_ONLINE  '1','" + RTCID + @"','" + TRTYPE + @"','" + ORDTYPE + @"','" + TON + @"','" + DISS + @"','" + WAGON + @"','" + EWAGON + @"','" + KT + @"','" + EKT + @"','" + ITEMCODE + @"','" + ITEMCODE_ETSNQ + @"','" + OWNER + @"','" + BPOINT + @"','" + EPOINT + @"','" + FPOINT + @"','" + TPOINT + @"','" + CLC_TYPE + @"','" + CLC_ID + @"','" + EXPYPE + @"','" + datetime + @"','" + origin + @"','" + MEXQTY + @"','" + EMEXQTY + @"','" + PTQTY + @"','" + PTOWNER + @"','" + PTRTCID + @"', @MSG_ALISH  = @MSG_ALISH output ,@PR_SATISH  = @PR_SATISH output,@PRTOTAL_ALISH  = @PRTOTAL_ALISH output ,@PRTOTAL_SATISH  = @PRTOTAL_SATISH output , @QTY  = @QTY output  
EXEC('SELECT '+@QTY +' QTY, '+@MSG_ALISH +'PRICEA,' +@PR_SATISH+' PRICES,' +@PRTOTAL_ALISH+' PRICEAT,' +@PRTOTAL_SATISH+' PRICEST')";
        System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand(query2, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader2 = cmd2.ExecuteReader();
        while (reader2.Read())
        {
            pricea = float.Parse(reader2["PRICEA"].ToString());

            prices = float.Parse(reader2["PRICES"].ToString());

            pricea_total = float.Parse(reader2["PRICEAT"].ToString());

            prices_total = float.Parse(reader2["PRICEST"].ToString());

            qty = int.Parse(reader2["QTY"].ToString());
        }
        try
        {
            pricea = pricea / global.GetCurrency(DateTime.Now, "USD");
            pricea_total = pricea_total / global.GetCurrency(DateTime.Now, "USD");
        }
        catch
        {

            pricea = 0;
            pricea_total = 0;

        }
        Prices price = new Prices();

        price.qty = qty;

        price.price_a = pricea;
        price.price_s = prices;

        price.price_a_total = pricea_total;
        price.price_s_total = prices_total;


        return price;
    }
    public string CalcQuery(int RTCID, int TRTYPE, int ORDTYPE, float TON, float DISS, float WAGON, float EWAGON, float KT, float EKT, int MEXQTY, int EMEXQTY, int PTQTY, int PTOWNER, int PTRTCID, string ITEMCODE, string ITEMCODE_ETSNQ, int OWNER, int BPOINT, int EPOINT, string FPOINT, string TPOINT, int CLC_TYPE, int CLC_ID, int PRICETYPE, int EXPYPE, string datetime, int origin, int casparcheck)
    {

        string rtnquery = "";
        string query = @"
DECLARE	@return_value int,
		@RATE float

EXEC	@return_value = [dbo].[sp_centralbankGetXML33]
		@DD ='" + datetime + @"',
		@RATE = @RATE OUTPUT
declare @RSQL Nvarchar(MAX) = ''

EXEC DBO.GET_EXPTYPE33   '1','" + RTCID + @"','" + TRTYPE + @"','" + ORDTYPE + @"','" + TON + @"','" + DISS + @"','" + WAGON + @"','" + EWAGON + @"','" + KT + @"','" + EKT + @"','" + ITEMCODE + @"','" + ITEMCODE_ETSNQ + @"','" + OWNER + @"',0,0,'" + BPOINT + @"','" + EPOINT + @"','" + FPOINT + @"','" + TPOINT + @"','" + CLC_TYPE + @"','" + CLC_ID + @"','" + EXPYPE + @"','" + datetime + @"','" + origin + @"','" + MEXQTY + @"','" + EMEXQTY + @"','" + PTQTY + @"','" + PTOWNER + @"','" + PTRTCID + @"', '" + casparcheck + @"',@RATE,
@RSQL  = @RSQL output SELECT @RSQL RSQL";
        System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader2 = cmd2.ExecuteReader();
        while (reader2.Read())
        {
            rtnquery = reader2["RSQL"].ToString();


        }
        return rtnquery;
    }
    public Boolean CheckCasparItems(string itemcode)
    {
        try
        {
            int count = 0;
            string query2 = @" SELECT [dbo].[CheckCasparItems] ('" + itemcode + "') as C";
            System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand(query2, con.connectionOpen());
            System.Data.SqlClient.SqlDataReader reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                count = int.Parse(reader2["C"].ToString());
            }
            reader2.Close();

            if (count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch { return false; }
    }
     public Boolean GlobalPerm(string ip, string id)
    {
        int count = 0;
        string query2 = @"select isnull((SELECT count(*) C FROM TBL_GLUSER_1
INNER JOIN TBL_GLOBALIP ON GLIP_STYPE=GU_GLTYP
where glip_ip LIKE '%" + ip + @"%' and gu_u_id='" + id + "'),0) c";
        System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand(query2, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader2 = cmd2.ExecuteReader();
        while (reader2.Read())
        {
            count = int.Parse(reader2["C"].ToString());

        }
        reader2.Close();

        if (count == 0)
        { return false; }
        else
        {
            return true;
        }


    }

  public string CreatecontainerSQL(string sql)
     {
        string v_sql = @"DECLARE @P_FORMULA VARCHAR(8000) 
DECLARE @S_FORMULA VARCHAR(8000)
DECLARE @APPID VARCHAR(8000)
DECLARE @TRFID VARCHAR(8000) 
SELECT  @P_FORMULA = COALESCE(@P_FORMULA + '+', '') + P_FORMULA ,@S_FORMULA = COALESCE(@S_FORMULA + '+', '') + S_FORMULA,@APPID = COALESCE(@APPID + ',', '') + APPID,@TRFID = COALESCE(@TRFID + ',', '') + TRFID
FROM (
SELECT  DISTINCT P_FORMULA, S_FORMULA,APPID, TRFID
FROM
(
"+sql+@"
) X

) ZH

SELECT DELT,TYP, REFID,VALUE,QTY,P_EXPENSE,P_AMOUNT, A_EXPENSE,AMOUNT,PROFIT,ISQT,ACCTYPE,'Alis- '+@P_FORMULA+'; Satis- '+@S_FORMULA NOTE,EXPENSEH,@P_FORMULA P_FORMULA,@S_FORMULA S_FORMULA, VENDER,CASE WHEN @APPID=',' THEN '' END APPID,@TRFID TRFID
FROM (
SELECT DELT,  TYP, REFID,VALUE, SUM(QTY) QTY,  SUM(P_EXPENSE) P_EXPENSE, SUM(P_AMOUNT) P_AMOUNT, SUM(A_EXPENSE) A_EXPENSE, SUM(AMOUNT) AMOUNT, SUM(PROFIT) PROFIT,0 ISQT,0 ACCTYPE,  SUM(EXPENSEH) EXPENSEH,0 VENDER
FROM
(
" + sql + @"
) X
GROUP BY DELT, TYP,REFID,VALUE,ISQT,ACCTYPE,VENDER
)ZH";
        return v_sql;
     }

    public bool Control_block(string PointCodes)
    {
        try
        {
            string query2 = @"Select * from [dbo].[T_SYS_BLOCK] where B_POINT in (" + PointCodes + ")";
            System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand(query2, con.connectionOpen());
            System.Data.SqlClient.SqlDataReader reader2 = cmd2.ExecuteReader();
            if (reader2.Read())
            {
                return true;
            }
            return false;
        }
        catch { return false; }
    }




    public string GenerateTokenToday(string apiKey)
    {
        string step = "";
        string token = "";
        step = DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + apiKey.ToString();// day + YEAR + MONTH + 55555
        token = CreateMD5(step);
        return token;
    }



    public string CreateMD5(string input)
    {
        string result = "";
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
                
            }
            result = sb.ToString();
        }
        return result;
    }

    public string SET_DATA_smart(string keyfield, int acceptInteq)
    {
        System.Data.SqlClient.SqlDataReader reader2 = null;
        System.Data.SqlClient.SqlDataReader reader3 = null;

        var payload = "";
        string message = "";
        string apikey = "31211";
        string ORD_B_ID = "";

        MainRequestModel mainModel = new MainRequestModel();
        Container contModel = new Container();
        DataTable contDt = new DataTable();

        var token = GenerateTokenToday(apikey);

        try
        {
            string sql_inteq = @"
                    SELECT ORD_RECNO,ORD_VGNTRNTYPE,ORD_C_ID mensheyi,ORD_ACTTYPE rejim,
        CASE 
			WHEN ORD_ACTTYPE = 1 THEN 2
			WHEN ORD_ACTTYPE = 2 THEN 1
			else ORD_ACTTYPE
        END Ret_Rejim,
        ORD_VAGONOWNER ORIGIN,ORD_CONTAINTYPE, ORD_VAGONTYPE, ORD_NOTE,
        CASE
			WHEN ORD_CONTAINAMOUNT20 <> 0 OR ORD_ECONTAINAMOUNT20 <> 0 THEN 4
			WHEN ORD_CONTAINAMOUNT40 <> 0 OR ORD_ECONTAINAMOUNT40 <> 0 THEN 6
			WHEN ORD_CONTAINAMOUNT45 <> 0 OR ORD_ECONTAINAMOUNT45 <> 0 THEN 7
        END ContainerAMOUNT,
        ORD_PODCODE,ORD_FCLIENT,ORD_TCLIENT,ORD_FPOINTCODE loading_st,ORD_TPOINTCODE destination_st,BPN.PNT_CODE Entry_boder_st,EPN.PNT_CODE exit_border_st,
        ORD_LOADSTCARDCODE1 qnq,ORD_LOADSTCARDCODE2 ETSNQ,ISNULL(ORD_C_ID,0) ORIG,ORD_FIRMCODE Firm_Code, ORD_FICHENO,
    	ORD_ECONTAINAMOUNT20 + ORD_ECONTAINAMOUNT40 + ORD_ECONTAINAMOUNT45 Empty,
    	ORD_CONTAINAMOUNT20 + ORD_CONTAINAMOUNT40 + ORD_CONTAINAMOUNT45 dolu, ORD_VAGONCOUNT WagonDolu,ORD_VGALLCOUNT WagonBosh, 
      	CASE
			WHEN ISNULL(ORD_VAGONCOUNT,0) = 0 AND ISNULL(ORD_VGALLCOUNT,0) <> 0 THEN ORD_VGALLCOUNT
			WHEN ISNULL(ORD_VGALLCOUNT,0) = 0 AND ISNULL(ORD_VAGONCOUNT,0) <> 0 THEN ORD_VAGONCOUNT
			WHEN ISNULL(ORD_VGALLCOUNT,0) = 0 AND ISNULL(ORD_VAGONCOUNT,0) = 0  THEN ORD_VGALLCOUNT
			WHEN ISNULL(ORD_VGALLCOUNT,0) <> 0 AND ISNULL(ORD_VAGONCOUNT,0) <> 0  THEN ORD_VGALLCOUNT
		END WCOUNT,
                    --ORD_VAGONCOUNT + ORD_VGALLCOUNT WCOUNT, 
		ORD_VAGONTONNAJ, ORD_VAGONNOTE, ORD_VGALLTONNAJ,ISNULL(ORD_LOADNOTE,'') ORD_LOADNOTE,
        (SELECT FC_CCODE FROM TBL_FIRMCODE WHERE FC_STATUS <> -1) FIRMCODE, ORD_B_ID
        from TBL_TRANSORDERS

        LEFT  JOIN TBL_STCARDS STC1 ON (ORD_LOADSTCARD1=STC1.STC_ID AND STC1.STC_STTYPE=1) 
        LEFT  JOIN TBL_POINTS FPN ON (ORD_FPOINT=FPN.PNT_RECNO)
        LEFT  JOIN TBL_POINTS TPN ON (ORD_TPOINT=TPN.PNT_RECNO)
    	LEFT  JOIN TBL_POINTS BPN ON (ORD_BEGPOINT=BPN.PNT_RECNO)
        LEFT  JOIN TBL_POINTS EPN ON (ORD_ENDPOINT=EPN.PNT_RECNO)
        WHERE (ISNULL(ORD_FRAMELESS,0) = 0 OR ORD_FRAMELESS = '') and ORD_EXPRESS_ID is null and  ORD_RECNO in (" + keyfield + ")";

            //DataTable expdt = new DataTable();
            //expdt.Clear();
            //expdt.Load(conn.dbrun(sql_inteq));

            reader2 = con.dbrun(sql_inteq);
            if (reader2.Read())
            {
                string contList = @"SELECT 
                                    CASE
	                                    WHEN TRN_OWNER = 1 THEN 2
	                                    WHEN TRN_OWNER = 2 THEN 1
                                    END CONTAINER_OWER,
                                    TRN_TRCAT CONTAINER_TYPE,
                                    TRN_TYPE CONTAINER_WEIGHTTYPE,
                                    TRN_PREFIX + CAST(TRN_NO as nvarchar) CONTAINER_NUMBER,
                                    ISNULL(TRN_COUNT,0) TRN_COUNT
                                    FROM TBL_TRANSPORTLIST
                                    WHERE TRN_ORDID = " + keyfield + " and TRN_STATUS <> 0";
                contDt = con.dbRunDt(contList);
                if (reader2["dolu"].ToString() == "0" && reader2["Empty"].ToString() != "0")
                {
                    mainModel.Empty = true;
                }

                if (reader2["Empty"].ToString() != "0" && reader2["dolu"].ToString() != "0")
                {
                    mainModel.Return = true;
                }



                mainModel.Type = int.Parse(reader2["rejim"].ToString());
                mainModel.Transtype = 2;//konteyner

                mainModel.Subcode = reader2["ORD_PODCODE"].ToString();

                mainModel.Sender = reader2["ORD_FCLIENT"].ToString().Replace('"', ' ').Replace("'", " ");

                mainModel.Receiver = reader2["ORD_TCLIENT"].ToString().Replace('"', ' ').Replace("'", " ");
                mainModel.Loading_St = reader2["loading_st"].ToString().Trim();
                mainModel.Destination_St = reader2["destination_st"].ToString().Trim();
                if (reader2["Entry_boder_st"].ToString() != "")
                {
                    mainModel.EntryBorder_St = reader2["Entry_boder_st"].ToString().Trim();
                }
                if (reader2["exit_border_st"].ToString() != "")
                {
                    mainModel.ExitBorder_St = reader2["exit_border_st"].ToString().Trim();
                }
                mainModel.QNQCODE = reader2["qnq"].ToString();
                if (reader2["ETSNQ"].ToString() != "")
                {
                    mainModel.ETSNQCODE = reader2["ETSNQ"].ToString();
                }
                mainModel.ORIGIN = int.Parse(reader2["ORIGIN"].ToString());
                mainModel.FIRMCODE = reader2["FIRMCODE"].ToString();
                mainModel.ALLWEIGHT = 0;
                mainModel.Note = reader2["ORD_FICHENO"].ToString();

                if (reader2["dolu"].ToString() == reader2["Empty"].ToString() || (reader2["dolu"].ToString() != "0" && reader2["Empty"].ToString() == "0"))
                {
                    contModel.CONTAINER_CONTCOUNT = int.Parse(reader2["dolu"].ToString());
                }
                else
                {
                    contModel.CONTAINER_CONTCOUNT = int.Parse(reader2["Empty"].ToString());
                }

                //if (contDt.Rows.Count > 0)
                //{
                //    foreach (DataRow contitem in contDt.Rows)
                //    {
                //        contModel.CONTAINER_OWER = contitem["CONTAINER_OWER"].ToString();
                //        contModel.CONTAINER_TYPE = contitem["CONTAINER_TYPE"].ToString();
                //        contModel.CONTAINER_WEIGHTTYPE = contitem["CONTAINER_WEIGHTTYPE"].ToString();
                //    }
                //}


                mainModel.RType = int.Parse(reader2["rejim"].ToString());
                mainModel.RTranstype = 2;//konteyner
                mainModel.RSubcode = reader2["ORD_PODCODE"].ToString();
                mainModel.RSender = reader2["ORD_FCLIENT"].ToString().Replace('"', ' ').Replace("'", " ");
                mainModel.RReceiver = reader2["ORD_TCLIENT"].ToString().Replace('"', ' ').Replace("'", " ");


                mainModel.RLoading_St = reader2["loading_st"].ToString().Trim();
                mainModel.RDestination_St = reader2["destination_st"].ToString().Trim();
                if (reader2["Entry_boder_st"].ToString() != "")
                {
                    mainModel.REntryBorder_St = reader2["Entry_boder_st"].ToString().Trim();
                }
                if (reader2["exit_border_st"].ToString() != "")
                {
                    mainModel.RExitBorder_St = reader2["exit_border_st"].ToString().Trim();
                }

                mainModel.RQNQCODE = reader2["qnq"].ToString();
                mainModel.RETSNQCODE = reader2["ETSNQ"].ToString();
                mainModel.RORIGIN = int.Parse(reader2["ORIGIN"].ToString());
                mainModel.RFIRMCODE = reader2["FIRMCODE"].ToString();
                mainModel.RALLWEIGHT = 0;

                //if (contDt.Rows.Count > 0)
                //{
                //    foreach (DataRow contitem in contDt.Rows)
                //    {
                //        contModel.RCONTAINER_OWER = contitem["ORIGIN"].ToString();
                //        contModel.RCONTAINER_TYPE = contitem["ORD_CONTAINTYPE"].ToString();
                //        contModel.RCONTAINER_WEIGHTTYPE = contitem["ContainerAMOUNT"].ToString();
                //    }
                //}
                //RROWCOUNT = int.Parse(reader2["KONTCOUNT"].ToString());
                ORD_B_ID = reader2["ORD_B_ID"].ToString();


                payload = "{";
                payload += "\"Main\":[";

                payload += "{\"Type\": " + mainModel.Type + "," +
                            "\"Transtype\": " + mainModel.Transtype + "," +
                            "\"Subcode\": \"" + mainModel.Subcode + "\"," +
                            "\"Sender\": \"" + mainModel.Sender + "\"," +
                            "\"Receiver\": \"" + mainModel.Receiver + "\"," +
                            "\"Loading_St\": '" + mainModel.Loading_St + "'," +
                            "\"Destination_St\": '" + mainModel.Destination_St + "'," +
                            "\"EntryBorder_St\": '" + mainModel.EntryBorder_St + "'," +
                            "\"ExitBorder_St\": '" + mainModel.ExitBorder_St + "',";
                if (mainModel.Empty == false)
                {
                    payload += "\"Empty\":false,";
                }
                else
                {
                    payload += "\"Empty\":true,";
                }
                if (mainModel.Return == false)
                {
                    payload += "\"Return\":false,";
                }
                else
                {
                    payload += "\"Return\":true,";
                }

                payload +=


                            "\"Guide\": false," +
                            "\"Guide_Count\": 0," +
                            "\"QNQCODE\": \"" + mainModel.QNQCODE + "\"," +
                            "\"ETSNQCODE\": \"" + mainModel.ETSNQCODE + "\"," +
                            "\"ORIGIN\": " + mainModel.ORIGIN + "," +
                            "\"Firm_Code\": " + mainModel.FIRMCODE + "," +
                            "\"ALLWEIGHT\": " + mainModel.ALLWEIGHT + "," +
                            "\"Note\": \"" + mainModel.Note + "\"}";

                //payload += "],\"Container\":[";

                //payload += "{\"ROWCOUNT\": " + ROWCOUNT + "," +
                //                "\"CONTAINER_OWER\": \"" + CONTAINER_OWER + "\"," +
                //                "\"CONTAINER_TYPE\": \"" + CONTAINER_TYPE + "\"," +
                //                "\"CONTAINER_WEIGHTTYPE\": \"" + CONTAINER_WEIGHTTYPE + "\"," +
                //                "\"CONTAINER_CONTCOUNT\": " + CONTAINER_CONTCOUNT + "," +
                //                "\"CONTAINER_NUMBER\": \"" + CONTAINER_NUMBER + "\"}";


                ///////CONTAINER
                payload += "],\"Container\":[";

                if (contDt.Rows.Count >= 1)
                {
                    for (int i = 1; i <= contDt.Rows.Count; i++)
                    {
                        if (i != contDt.Rows.Count)
                        {
                            payload += "{\"ROWCOUNT\": " + i + "," +
                               "\"CONTAINER_OWER\": \"" + contDt.Rows[i - 1]["CONTAINER_OWER"].ToString() + "\"," +
                               "\"CONTAINER_TYPE\": \"" + contDt.Rows[i - 1]["CONTAINER_TYPE"].ToString() + "\"," +
                               "\"CONTAINER_WEIGHTTYPE\": \"" + contDt.Rows[i - 1]["CONTAINER_WEIGHTTYPE"].ToString() + "\"," +
                               "\"CONTAINER_CONTCOUNT\": \"" + contDt.Rows[i - 1]["TRN_COUNT"].ToString() + "\"," +
                               "\"CONTAINER_NUMBER\": \"" + contDt.Rows[i - 1]["CONTAINER_NUMBER"].ToString().Trim() + "\"},";
                        }
                        else
                        {
                            payload += "{\"ROWCOUNT\": " + i + "," +
                              "\"CONTAINER_OWER\": \"" + contDt.Rows[i - 1]["CONTAINER_OWER"].ToString() + "\"," +
                              "\"CONTAINER_TYPE\": \"" + contDt.Rows[i - 1]["CONTAINER_TYPE"].ToString() + "\"," +
                              "\"CONTAINER_WEIGHTTYPE\": \"" + contDt.Rows[i - 1]["CONTAINER_WEIGHTTYPE"].ToString() + "\"," +
                              "\"CONTAINER_CONTCOUNT\": \"" + contDt.Rows[i - 1]["TRN_COUNT"].ToString() + "\"," +
                              "\"CONTAINER_NUMBER\": \"" + contDt.Rows[i - 1]["CONTAINER_NUMBER"].ToString().Trim() + "\"}";
                        }

                    }
                }
                else
                {
                        payload += "{\"ROWCOUNT\": " + 1 + "," +
                           "\"CONTAINER_OWER\": \"" + 0 + "\"," +
                           "\"CONTAINER_TYPE\": \"\"," +
                           "\"CONTAINER_WEIGHTTYPE\": \"\"," +
                           "\"CONTAINER_CONTCOUNT\": \"" + 0 + "\"," +
                           "\"CONTAINER_NUMBER\": \"\"}";
                }

                ///////////RETURN

                payload += "],\"Return\":[";

                payload += "{\"Type\": " + mainModel.RType + "," +
                                "\"Transtype\": " + mainModel.RTranstype + "," +
                                "\"Subcode\": \"" + mainModel.RSubcode + "\"," +
                                "\"Sender\": \"" + mainModel.RSender + "\"," +
                                "\"Receiver\": \"" + mainModel.RReceiver + "\"," +
                                "\"Loading_St\": '" + mainModel.RLoading_St + "'," +
                                "\"Destination_St\": '" + mainModel.RDestination_St + "'," +
                                "\"EntryBorder_St\": '" + mainModel.REntryBorder_St + "'," +
                                "\"ExitBorder_St\": '" + mainModel.RExitBorder_St + "',";
                if (mainModel.Empty == false)
                {
                    payload += "\"Empty\":false,";
                }
                else
                {
                    payload += "\"Empty\":true,";
                }
                if (mainModel.Return == false)
                {
                    payload += "\"Return\":false,";
                }
                else
                {
                    payload += "\"Return\":true,";
                }


                payload +=

                                "\"Guide\": false," +
                                "\"Guide_Count\": 0," +
                                "\"QNQCODE\": \"" + mainModel.RQNQCODE + "\"," +
                                "\"ETSNQCODE\": \"" + mainModel.RETSNQCODE + "\"," +
                                "\"ORIGIN\": " + mainModel.RORIGIN + "," +
                                "\"Firm_Code\": " + mainModel.RFIRMCODE + "," +
                                "\"ALLWEIGHT\": " + mainModel.RALLWEIGHT + "}";

                //payload += "],\"Return_Container\":[";

                //payload += "{\"ROWCOUNT\": " + ROWCOUNT + "," +
                //    "\"CONTAINER_OWER\": \"" + RCONTAINER_OWER + "\"," +
                //    "\"CONTAINER_TYPE\": \"" + RCONTAINER_TYPE + "\"," +
                //    "\"CONTAINER_WEIGHTTYPE\": \"" + RCONTAINER_WEIGHTTYPE + "\"," +
                //    "\"CONTAINER_CONTCOUNT\": " + CONTAINER_CONTCOUNT + "," +
                //    "\"CONTAINER_NUMBER\": \"" + RCONTAINER_NUMBER + "\"}";

                ///////Return CONTAINER
                payload += "],\"Return_Container\":[";

                if (contDt.Rows.Count >= 1)
                {
                    for (int i = 1; i <= contDt.Rows.Count; i++)
                    {
                        if (i != contDt.Rows.Count)
                        {
                            payload += "{\"ROWCOUNT\": " + i + "," +
                               "\"CONTAINER_OWER\": \"" + contDt.Rows[i - 1]["CONTAINER_OWER"].ToString() + "\"," +
                               "\"CONTAINER_TYPE\": \"" + contDt.Rows[i - 1]["CONTAINER_TYPE"].ToString() + "\"," +
                               "\"CONTAINER_WEIGHTTYPE\": \"" + contDt.Rows[i - 1]["CONTAINER_WEIGHTTYPE"].ToString() + "\"," +
                               "\"CONTAINER_CONTCOUNT\": \"" + contDt.Rows[i - 1]["TRN_COUNT"].ToString() + "\"," +
                               "\"CONTAINER_NUMBER\": \"" + contDt.Rows[i - 1]["CONTAINER_NUMBER"].ToString().Trim() + "\"},";
                        }
                        else
                        {
                            payload += "{\"ROWCOUNT\": " + i + "," +
                              "\"CONTAINER_OWER\": \"" + contDt.Rows[i - 1]["CONTAINER_OWER"].ToString() + "\"," +
                              "\"CONTAINER_TYPE\": \"" + contDt.Rows[i - 1]["CONTAINER_TYPE"].ToString() + "\"," +
                              "\"CONTAINER_WEIGHTTYPE\": \"" + contDt.Rows[i - 1]["CONTAINER_WEIGHTTYPE"].ToString() + "\"," +
                              "\"CONTAINER_CONTCOUNT\": \"" + contDt.Rows[i - 1]["TRN_COUNT"].ToString() + "\"," +
                              "\"CONTAINER_NUMBER\": \"" + contDt.Rows[i - 1]["CONTAINER_NUMBER"].ToString().Trim() + "\"}";
                        }

                    }
                }
                else
                {

                    payload += "{\"ROWCOUNT\": " + 1 + "," +
                           "\"CONTAINER_OWER\": \"" + 0 + "\"," +
                           "\"CONTAINER_TYPE\": \"\"," +
                           "\"CONTAINER_WEIGHTTYPE\": \"\"," +
                           "\"CONTAINER_CONTCOUNT\": \"" + 0 + "\"," +
                           "\"CONTAINER_NUMBER\": \"\"}";
                }

                payload += "]}";

            }


            if (payload != "" && acceptInteq == 1)
            {
                string url = "https://smartint.ady.az/api/GExpeditor/SetOrder?token=" + token;

                String Username = "GEXP";
                String Password = "@DYSM@RAT!09";


                String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(Username + ":" + Password));

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.AllowAutoRedirect = true;
                req.Headers.Add("Authorization", "Basic " + encoded);
                req.Method = "POST";
                req.ContentType = "application/json";
                using (StreamWriter streamWriter = new StreamWriter(req.GetRequestStream()))
                {
                    streamWriter.Write(payload);
                    streamWriter.Flush();
                }
                string responseData = string.Empty;
                WebResponse resp = (HttpWebResponse)req.GetResponse();
                using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                {
                    responseData = reader.ReadToEnd();
                }

                ResponseTransID response = JsonConvert.DeserializeObject<ResponseTransID>(responseData);
                if (response.message == "Success")
                {
                    //message = response.transactionId;
                    Set_ORD_EXPRESS_ID(reader2["ORD_RECNO"].ToString(), response.transactionId);

                    //con.dbrun(@"UPDATE TBL_TRANSORDERS
                    //                SET ORD_STATUS = 16
                    //                where ORD_RECNO in (" + keyfield + ")" + @"
                    //Insert into [TBL_PROCESSLOGS] (PLG_LOGTYPE,PLG_ORDID,PLG_USRID,PLG_STATUS,PLG_NOTE)
                    //values ('ORD'," + keyfield + "," + lg.Get_userid() + "," + 16 + ",N'NTS GOZLEYEN')"
                    //  );

                    Set_Status(int.Parse(keyfield), 16, int.Parse(ORD_B_ID));


                }
                else
                {

                    //System.Web.HttpContext.Current.Response.Write("<script>alert('QNQ!');document.location='Frm_BrBglRequest.aspx'; </script>");

                    message = response.message;
                }

                File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"\inteqrasiyaApi.txt"), mainModel.Note + " - " + " Token: " + url + " - " + payload + Environment.NewLine + response.message + Environment.NewLine + Environment.NewLine);
            }
            else
            {
                //con.dbrun(@"UPDATE TBL_TRANSORDERS
                //SET ORD_STATUS = 16
                //where ORD_RECNO in (" + keyfield + ")" + @"
                //Insert into [TBL_PROCESSLOGS] (PLG_LOGTYPE,PLG_ORDID,PLG_USRID,PLG_STATUS,PLG_NOTE)
                //     values ('ORD'," + keyfield + "," + lg.Get_userid() + "," + 16 + ",N'NTS GOZLEYEN')"
                //);
                string sqll_b = @"SELECT ORD_B_ID from TBL_TRANSORDERS
                                    WHERE ORD_RECNO = " + keyfield;
                reader3 = con.dbrun(sqll_b);
                if (reader3.Read())
                {
                    ORD_B_ID = reader3["ORD_B_ID"].ToString();
                }

                Set_Status(int.Parse(keyfield), 16, int.Parse(ORD_B_ID));
            }

        }
        catch (Exception ex)
        {

            //var result = ex.StackTrace;
            message = "Xəta baş verdi!" + Environment.NewLine + ex.Message;
            //MessageBox.Show(result);
        }
        return message;
    }

    private void Set_ORD_EXPRESS_ID(string ORD_RECNO, string ORD_EXPRESS_ID)
    {
        string sql = @"UPDATE TBL_TRANSORDERS 
                        SET ORD_EXPRESS_ID = '" + ORD_EXPRESS_ID + "' " +
                        "WHERE ORD_RECNO = '" + ORD_RECNO + "'";
        con.dbrun(sql);
    }



    private Master Get_Data_transs(string keyfield)
    {
        System.Data.SqlClient.SqlDataReader reader2 = null;
        string sql_inteq = @"
                    select ORD_RECNO,ORD_C_ID mensheyi,ORD_ACTTYPE rejim,ORD_VAGONOWNER ORIGIN,ORD_CONTAINTYPE, ORD_NOTE, ORD_VAGONOWNER,ORD_CONTAINTYPE,
                    CASE
                    WHEN ORD_CONTAINAMOUNT20 <> 0 OR ORD_ECONTAINAMOUNT20 <> 0 THEN 4
                    WHEN ORD_CONTAINAMOUNT40 <> 0 OR ORD_ECONTAINAMOUNT40 <> 0 THEN 6
                    WHEN ORD_CONTAINAMOUNT45 <> 0 OR ORD_ECONTAINAMOUNT45 <> 0 THEN 7
                    END ContainerAMOUNT
                    ,ORD_PODCODE,ORD_FCLIENT,ORD_TCLIENT,ORD_FPOINTCODE loading_st,ORD_TPOINTCODE destination_st,BPN.PNT_CODE Entry_boder_st,EPN.PNT_CODE exit_border_st,
                    ORD_LOADSTCARDCODE1 qnq,ORD_LOADSTCARDCODE2 ETSNQ,ORD_C_ID ORIGIN,ORD_FIRMCODE Firm_Code

                    from TBL_TRANSORDERS

                        LEFT  JOIN TBL_STCARDS STC1 ON (ORD_LOADSTCARD1=STC1.STC_ID AND STC1.STC_STTYPE=1) 
                        LEFT  JOIN TBL_POINTS FPN ON (ORD_FPOINT=FPN.PNT_RECNO)
                            LEFT  JOIN TBL_POINTS TPN ON (ORD_TPOINT=TPN.PNT_RECNO)
    		                LEFT  JOIN TBL_POINTS BPN ON (ORD_BEGPOINT=BPN.PNT_RECNO)
                            LEFT  JOIN TBL_POINTS EPN ON (ORD_ENDPOINT=EPN.PNT_RECNO)
                    --where ORD_RECNO = 63688
                                                      WHERE ISNULL(ORD_EXPRESS_ID,0)='0' AND ORD_RECNO=" + keyfield;

        reader2 = con.dbrun(sql_inteq);
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Load(reader2);

        var JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(dt);
        byte[] bytes = Encoding.ASCII.GetBytes(JSONString);


        var responseString = Encoding.UTF8.GetString(bytes);
        string JsonString = responseString.ToString();
        Master master = JsonConvert.DeserializeObject<Master>(JsonString);



        return master;
    }





    public void AmountInsert(string transid)
    {
        System.Data.SqlClient.SqlDataReader reader3 = null;
        string sc_id = "";

        string sql = @"SELECT E_ORD_RECNO,ROUND(E_AMOUNT/ROUND(RT_RATE,2),2) E_AMOUNT,E_AMOUNT E_AZAMOUNT,INV_ID,INV_NO, CURR_CODE,CURR_ID,ROUND(RT_RATE,2) RT_RATE FROM TBL_EPAY " +
                        @" INNER JOIN TBL_INVOICE inv on inv.INV_ORD_RECNO = E_ORD_RECNO
                            INNER JOIN TBL_CURRENCY on CURR_ID = INV_CURR_ID 
                            INNER JOIN TBL_RATE ON RT_CURR_ID = CURR_ID 
                            WHERE E_PAYMENTKEY='" + transid + "'";
        reader3 = con.dbrun(sql);

        if (reader3.Read())
        {
            float amount = float.Parse(reader3["E_AMOUNT"].ToString());
            int curr_ID = int.Parse(reader3["CURR_ID"].ToString());
            float curr_rate = float.Parse(reader3["RT_RATE"].ToString());
            int INV_ID = int.Parse(reader3["INV_ID"].ToString());
            float amountAZ = float.Parse(reader3["E_AZAMOUNT"].ToString());
            //float az_amount = float.Parse(reader3["RT_RATE"].ToString());




            string ins_BANKDOC = @"INSERT INTO TBL_BANKDOC 
                                    (
                                   
                                    BD_DATE,	
                                    BD_TYPE	,
                                    BD_OTYPE,		
                                    BD_AMOUNT	,
                                    BD_CURR_ID	,
                                    BD_CURR_RATE,	
                                    BD_AZNAMOUNT
                                    --BD_USDAMOUNT,
                                    --BD_REFNO	,
                                    --BD_PRONO,	
                                    --BD_NOTE	,
                                    --BD_U_ID,
                                    --BD_CACC_ID,
                                    --BD_CACC_ID_M,
                                    --BD_EXP_ID,
                                    --BD_1CNO,
                                    --BD_B_ID
                                    ) 
                                       VALUES (GETDATE(),1,1," + amount + "," + curr_ID + "," + curr_rate + "," + amountAZ + "); select SCOPE_IDENTITY() OID";

            System.Data.SqlClient.SqlDataReader v_reader = conn.fnc_dbrun(ins_BANKDOC);

            if (v_reader.Read())
            {
                sc_id = v_reader["OID"].ToString();
            }

            string v_sql = @"INSERT INTO TBL_BANKINV (
                                BI_BD_TYPE,
                                BI_INV_ID,
                                --BI_VHFNO,
                                --BI_TEMP,
                                BI_BD_ID, 

                                --BI_NO,  
                                BI_AMOUNT,
                                BI_CURR_ID,
                                BI_CURR_RATE,
                                BI_AZNAMOUNT
                                --BI_VAT_AMOUNT,
                                --BI_VAT_AZNAMOUNT,
                                --BI_VAT
                                    )
                                                VALUES (1," + INV_ID + "," + sc_id + "," + amount + "," + curr_ID + "," + curr_rate + "," + amountAZ + ")";
            conn.fnc_dbrun(v_sql);
        }
    }




    //public TEntity SqlReaderToModel<TEntity>(TEntity entity, SqlDataReader reader)
    //{
    //    Type type = typeof(TEntity);

    //    for (int i = 0; i < reader.FieldCount; i++)
    //    {
    //        string fieldName = "";
    //        object fieldValue = null;
    //        try
    //        {
    //            fieldName = reader.GetName(i);
    //            fieldValue = reader.GetValue(i);
    //            type.GetProperty(fieldName).SetValue(entity, fieldValue);
    //        }
    //        catch (Exception)
    //        {
    //            type.GetProperty(ToTitleCase(fieldName)).SetValue(entity, fieldValue);
    //        }
    //    }

    //    return entity;
    //}

    public string ToTitleCase(string text)
    {
       return char.ToUpper(text.First()) + text.Substring(1).ToLower();
    }


    public static string GetRandomPassword(int length)
    {
        const string chars = "!@#$%^&*0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        StringBuilder sb = new StringBuilder();
        Random rnd = new Random();

        for (int i = 0; i < length; i++)
        {
            int index = rnd.Next(chars.Length);
            sb.Append(chars[index]);
        }

        return sb.ToString();
    }


    public static string Encrypt(string s)
    {
        if (String.IsNullOrEmpty(s))
        {
            return s;
        }
        else
        {
            var encoding = new UTF8Encoding();
            byte[] plain = encoding.GetBytes(s);
            return Convert.ToBase64String(plain);
        }
    }
    public static string Decrypt(string s)
    {
        if (String.IsNullOrEmpty(s))
        {
            return s;
        }
        else
        {
            byte[] secret = Convert.FromBase64String(s);
            var encoding = new UTF8Encoding();
            return encoding.GetString(secret);
        }
    }

    public string GetNewClientVenderCode(string type)
    {
        string code = "";

        string query = @"EXEC SP_GetNewClientVenderCode @type=" + type;
        var reader = con.dbrun(query);

        if (reader.Read())
        {
            code = Convert.ToString(reader["Code"]);
        }

        return code;
    }


    public void SendAllianceOrCaspian(int orderId,int type)
    {
        System.Data.SqlClient.SqlDataReader reader = null;
        string client="";
        //type  //1 - Alliance, 2 - Caspian

        if (type == 2)
        {
            client = "9290";
        }
        else if (type == 1)
        {
            client = "10530";
        }
        

        ORDER_OTHERCOMPANY(client, type, orderId); ///9290 - clietno; 2 - Həmişə Caspiana gedəcək
        if (resultvservice != "-1" && resultvservice != "0")
        {
            string sqlll = "update TBL_TRANSORDERS set ORD_EXPRESS_FICHENO='" + resultvservice + "' where ORD_RECNO=" + orderId;
            reader = con.dbrun(sqlll);
        }
       
    }

    public string ORDER_OTHERCOMPANY(string clientno, int type, int orderId)
    {
        DataTable dt = new DataTable();
        conn con = new conn();
        if (type == 2) // Caspian
        {
            //var webAddr = "http://localhost:50439/Integration.asmx/ORDERDATA";
            //var webAddr = "http://85.132.47.43:1001/Integration.asmx/OrderMultimodal";
            var webAddr = "https://ufs.caspianrail.az/Integration.asmx/OrderMultimodal";
            string expsql = @"
               select [ORD_RECNO]
              ,[ORD_RECNO_OLD]
              ,[ORD_ACTTYPE]
              ,[ORD_ACTTYPEINFO]
              ,[ORD_BEGPOINT]
              ,[ORD_ENDPOINT]
              ,[ORD_EBEGPOINT]
              ,[ORD_EENDPOINT]
              ,"+ clientno + @" as ORD_CLCRECNO
              ,[ORD_FIRM]
              ,[ORD_EXPRESS_FICHENO]
              ,[ORD_FICHENO]
              ,[ORD_FICHEDATE]
              ,Replace([ORD_FCLIENT],'''','') as [ORD_FCLIENT]
              ,[ORD_PODCODE]
              ,[ORD_PODCODE_SYS]
              ,[ORD_KARVANCODE]
              ,[ORD_NTS]
              ,[ORD_TELEGRAM]
              ,[ORD_FPOINT]
              ,[ORD_FPOINTCODE]
              ,Replace([ORD_TCLIENT],'''','') as [ORD_TCLIENT]
              ,[ORD_TPOINT]
              ,[ORD_TPOINTCODE]
              ,[ORD_LOADSTCARD1]
              ,[ORD_LOADSTCARDCODE1]
              ,[ORD_LOADSTCARD2]
              ,[ORD_LOADSTCARDCODE2]
              ,[ORD_LOADDESC]
              ,[ORD_LOADAMOUNT]
              ,Replace([ORD_LOADNOTE],'''','') as [ORD_LOADNOTE]
              ,[ORD_VGNTRNTYPE]
              ,[ORD_VAGONOWNER]
              ,[ORD_VAGONOWNERSTN]
              ,[ORD_VAGONTYPE]
              ,[ORD_VAGONTONNAJ]
              ,[ORD_VGALLTONNAJ]
              ,[ORD_VAGONCOUNT]
              ,[ORD_VGALLCOUNT]
              ,[ORD_CONTAINTYPE]
              --,[ORD_VAGONNOTE]
              ,[ORD_NOTE]
              ,2 [ORD_STATUS]
              ,[ORD_STATUSDATE]
              ,[ORD_SIGNNOTE]
              ,[ORD_SIGNSEND]
              ,[ORD_SENDDATE]
              ,[ORD_DISTANCE]
              ,[ORD_EXPEDITOR]
              ,[ORD_EXPCLIENT]
              ,[ORD_CONTAINAMOUNT3]
              ,[ORD_CONTAINAMOUNT5]
              ,[ORD_CONTAINAMOUNT10]
              ,[ORD_CONTAINAMOUNT20]
              ,[ORD_CONTAINAMOUNT30]
              ,[ORD_CONTAINAMOUNT40]
              ,[ORD_CONTAINAMOUNT45]
              ,[ORD_ECONTAINAMOUNT3]
              ,[ORD_ECONTAINAMOUNT5]
              ,[ORD_ECONTAINAMOUNT10]
              ,[ORD_ECONTAINAMOUNT20]
              ,[ORD_ECONTAINAMOUNT30]
              ,[ORD_ECONTAINAMOUNT40]
              ,[ORD_ECONTAINAMOUNT45]
              ,[ORD_CONTAINAMOUNTEXTRA]
              ,[ORD_ECONTAINAMOUNTEXTRA]
              ,1 [ORD_CLC_PTYPE]
              ,[ORD_ISQT]
              ,[ORD_EXP_TEMPLET]
              ,[ORD_U_ID]
              ,[ORD_UPD_U_ID]
              ,[ORD_CREATEDATE]
              ,[ORD_FIRMCODE]
              ,[ORD_CVAGONTONNAJ]
              ,[ORD_PLATOWNER]
              ,[ORD_PLATTYPE]
              ,[ORD_PLATCOUNT]
              ,[ORD_C_ID]
              ,[ORD_PTOTAL]
              ,[ORD_STOTAL]
              ,[ORD_PROFIT]
              ,[ORD_READ]
              ,[ORD_KASPAR]
              ,[ORD_O_ID]
              ,[ORD_B_ID]
              ,'1' [ORD_PRICEAREA]
             -- ,[ORD_FRAMELESS]
              --,[ORD_CONTWEIGHT]
             -- ,[ORD_WIDTH]
             -- ,[ORD_LENGTH]
              --,[ORD_HEIGHT]
              , TransportNo ORD_VAGONNOTE
              , PurchaseFull ORD_NOTEFULL
              , PurchaseEmpty ORD_NOTEEMPTY
              ,
	            CASE
	                WHEN ORD_ACTTYPE=1 then  BPN.PNT_RECNO
	                WHEN ORD_ACTTYPE=2 then  FPN.PNT_RECNO
	                WHEN ORD_ACTTYPE=3 then  BPN.PNT_RECNO
                    WHEN ORD_ACTTYPE=4 then  FPN.PNT_RECNO  end ORD_PRC_ST1,
	            CASE
	                WHEN ORD_ACTTYPE=1 then  BPN.PNT_CODE
	                WHEN ORD_ACTTYPE=2 then  FPN.PNT_CODE
	                WHEN ORD_ACTTYPE=3 then  BPN.PNT_CODE
                    WHEN ORD_ACTTYPE=4 then  FPN.PNT_CODE  end ORD_PRC_ST1CODE,
                CASE
	                WHEN ORD_ACTTYPE=1 then  TPN.PNT_RECNO
	                WHEN ORD_ACTTYPE=2 then  EPN.PNT_RECNO
	                WHEN ORD_ACTTYPE=3 then  EPN.PNT_RECNO
                    WHEN ORD_ACTTYPE=4 then  TPN.PNT_RECNO end ORD_PRC_ST2,
	            CASE
	                WHEN ORD_ACTTYPE=1 then  TPN.PNT_CODE
	                WHEN ORD_ACTTYPE=2 then  EPN.PNT_CODE
	                WHEN ORD_ACTTYPE=3 then  EPN.PNT_CODE
                    WHEN ORD_ACTTYPE=4 then  TPN.PNT_CODE  end ORD_PRC_ST2CODE
          FROM [TBL_TRANSORDERS]
            left join (select STRING_AGG(TRN_PREFIX + TRN_NO, ', ') As TransportNo, TRN_ORDID  from TBL_TRANSPORTLIST
            where TRN_TRTYPE = 2 
            group by TRN_ORDID) transport on TRN_ORDID = Ord_recno
            --Dolu
            left join (select  STRING_AGG(EX_VALUE1 + ' - ' + cast(EXP_PAMOUNT as varchar(max)), ', ') As PurchaseFull, EXP_ORDRECNO  from TBL_TRANSORDEREXP
            join TBL_EXPENCE on EX_ID = EXP_EXPTYPEID
            where  EXP_TYPE = 1
            group by EXP_ORDRECNO) purchaseFull on purchaseFull.EXP_ORDRECNO = ORD_RECNO
            --Bosh
            left join (select  STRING_AGG(EX_VALUE1 + ' - ' + cast(EXP_PAMOUNT as varchar(max)), ', ') As PurchaseEmpty, EXP_ORDRECNO  from TBL_TRANSORDEREXP
            join TBL_EXPENCE on EX_ID = EXP_EXPTYPEID
            where  EXP_TYPE = 2 
            group by EXP_ORDRECNO) purchaseEmpty on purchaseEmpty.EXP_ORDRECNO = ORD_RECNO
        	LEFT  JOIN TBL_POINTS FPN ON (ORD_FPOINT=FPN.PNT_RECNO)
            LEFT  JOIN TBL_POINTS TPN ON (ORD_TPOINT=TPN.PNT_RECNO)
            LEFT  JOIN TBL_POINTS BPN ON (ORD_BEGPOINT=BPN.PNT_RECNO)
            LEFT  JOIN TBL_POINTS EPN ON (ORD_ENDPOINT=EPN.PNT_RECNO)
         WHERE ORD_RECNO=" + orderId;
            DataTable expdt = new DataTable();
            expdt.Load(con.dbrun(expsql));
            string expdata = JsonConvert.SerializeObject(expdt);
            using (var client = new WebClient())
            {
                client.Headers.Add("X-WebService-Header", system.GetHeaderTokenCaspian());

                var values = new NameValueCollection();
                values["JSONN"] = expdata.ToString();
                values["CLIENT"] = clientno;
                var response = client.UploadValues(webAddr, values);

                var responseString = Encoding.UTF8.GetString(response);
                string JsonString = responseString.ToString();

                dt = JsonConvert.DeserializeObject<DataTable>(JsonString);
                if (JsonString != "0" && JsonString != "-1")
                {
                    foreach (DataRow dtrw in dt.Rows)
                    {
                        resultvservice = dtrw["ORD_FICHENO"].ToString();
                    }
                }
                else
                {
                    resultvservice = JsonString;
                }

            }
        }
        else
        {
            //var webAddr = "http://localhost:49393/Integration.asmx/OrderMultimodal";
            var webAddr = "https://ufs.alliance.net/Integration.asmx/OrderMultimodal";
            string expsql = @"
select [ORD_RECNO]
      ,[ORD_RECNO_OLD]
      ,[ORD_ACTTYPE]
      ,[ORD_ACTTYPEINFO]
      ,[ORD_BEGPOINT]
      ,[ORD_ENDPOINT]
      ,[ORD_EBEGPOINT]
      ,[ORD_EENDPOINT]
      ,"+ clientno + @" as ORD_CLCRECNO
      ,[ORD_FIRM]
      ,[ORD_EXPRESS_FICHENO]
      ,[ORD_FICHENO]
      ,[ORD_FICHEDATE]
      ,Replace([ORD_FCLIENT],'''','') as [ORD_FCLIENT]
      ,[ORD_PODCODE]
      ,[ORD_PODCODE_SYS]
      ,[ORD_KARVANCODE]
      ,[ORD_NTS]
      ,[ORD_TELEGRAM]
      ,[ORD_FPOINT]
      ,[ORD_FPOINTCODE]
      ,Replace([ORD_TCLIENT],'''','') as [ORD_TCLIENT]
      ,[ORD_TPOINT]
      ,[ORD_TPOINTCODE]
      ,[ORD_LOADSTCARD1]
      ,[ORD_LOADSTCARDCODE1]
      ,[ORD_LOADSTCARD2]
      ,[ORD_LOADSTCARDCODE2]
      ,[ORD_LOADDESC]
      ,[ORD_LOADAMOUNT]
      ,Replace([ORD_LOADNOTE],'''','') as [ORD_LOADNOTE]
      ,[ORD_VGNTRNTYPE]
      ,[ORD_VAGONOWNER]
      ,[ORD_VAGONOWNERSTN]
      ,[ORD_VAGONTYPE]
      ,[ORD_VAGONTONNAJ]
      ,[ORD_VGALLTONNAJ]
      ,[ORD_VAGONCOUNT]
      ,[ORD_VGALLCOUNT]
      ,[ORD_CONTAINTYPE]
      --,[ORD_VAGONNOTE]
      ,[ORD_NOTE]
      ,2 [ORD_STATUS]
      ,[ORD_STATUSDATE]
      ,[ORD_SIGNNOTE]
      ,[ORD_SIGNSEND]
      ,[ORD_SENDDATE]
      ,[ORD_DISTANCE]
      ,[ORD_EXPEDITOR]
      ,[ORD_EXPCLIENT]
      ,[ORD_CONTAINAMOUNT3]
      ,[ORD_CONTAINAMOUNT5]
      ,[ORD_CONTAINAMOUNT10]
      ,[ORD_CONTAINAMOUNT20]
      ,[ORD_CONTAINAMOUNT30]
      ,[ORD_CONTAINAMOUNT40]
      ,[ORD_CONTAINAMOUNT45]
      ,[ORD_ECONTAINAMOUNT3]
      ,[ORD_ECONTAINAMOUNT5]
      ,[ORD_ECONTAINAMOUNT10]
      ,[ORD_ECONTAINAMOUNT20]
      ,[ORD_ECONTAINAMOUNT30]
      ,[ORD_ECONTAINAMOUNT40]
      ,[ORD_ECONTAINAMOUNT45]
      ,[ORD_CONTAINAMOUNTEXTRA]
      ,[ORD_ECONTAINAMOUNTEXTRA]
      ,1 [ORD_CLC_PTYPE]
      ,[ORD_ISQT]
      ,[ORD_EXP_TEMPLET]
      ,[ORD_U_ID]
      ,[ORD_UPD_U_ID]
      ,[ORD_CREATEDATE]
      ,[ORD_FIRMCODE]
      ,[ORD_CVAGONTONNAJ]
      ,[ORD_PLATOWNER]
      ,[ORD_PLATTYPE]
      ,[ORD_PLATCOUNT]
      ,[ORD_C_ID]
      ,[ORD_PTOTAL]
      ,[ORD_STOTAL]
      ,[ORD_PROFIT]
      ,[ORD_READ]
      ,[ORD_KASPAR]
      ,[ORD_O_ID]
      ,[ORD_B_ID]
      ,'1' [ORD_PRICEAREA]
     -- ,[ORD_FRAMELESS]
      --,[ORD_CONTWEIGHT]
     -- ,[ORD_WIDTH]
     -- ,[ORD_LENGTH]
      --,[ORD_HEIGHT]
    , TransportNo ORD_VAGONNOTE
    , PurchaseFull ORD_NOTEFULL
    , PurchaseEmpty ORD_NOTEEMPTY
    ,
    CASE
	    WHEN ORD_ACTTYPE=1 then  BPN.PNT_RECNO
	    WHEN ORD_ACTTYPE=2 then  FPN.PNT_RECNO
	    WHEN ORD_ACTTYPE=3 then  BPN.PNT_RECNO
        WHEN ORD_ACTTYPE=4 then  FPN.PNT_RECNO  end ORD_PRC_ST1,
	CASE
	    WHEN ORD_ACTTYPE=1 then  BPN.PNT_CODE
	    WHEN ORD_ACTTYPE=2 then  FPN.PNT_CODE
	    WHEN ORD_ACTTYPE=3 then  BPN.PNT_CODE
        WHEN ORD_ACTTYPE=4 then  FPN.PNT_CODE  end ORD_PRC_ST1CODE,
    CASE
	    WHEN ORD_ACTTYPE=1 then  TPN.PNT_RECNO
	    WHEN ORD_ACTTYPE=2 then  EPN.PNT_RECNO
	    WHEN ORD_ACTTYPE=3 then  EPN.PNT_RECNO
        WHEN ORD_ACTTYPE=4 then  TPN.PNT_RECNO end ORD_PRC_ST2,
	CASE
	    WHEN ORD_ACTTYPE=1 then  TPN.PNT_CODE
	    WHEN ORD_ACTTYPE=2 then  EPN.PNT_CODE
	    WHEN ORD_ACTTYPE=3 then  EPN.PNT_CODE
        WHEN ORD_ACTTYPE=4 then  TPN.PNT_CODE  end ORD_PRC_ST2CODE
  FROM [TBL_TRANSORDERS]
    left join (select STRING_AGG(TRN_PREFIX + TRN_NO, ',') As TransportNo, TRN_ORDID  from TBL_TRANSPORTLIST
    where TRN_TRTYPE = 2 
    group by TRN_ORDID) transport on TRN_ORDID = Ord_recno
    --Dolu
    left join (select  STRING_AGG(EX_VALUE1 + ' - ' + cast(EXP_PAMOUNT as varchar(max)), ', ') As PurchaseFull, EXP_ORDRECNO  from TBL_TRANSORDEREXP
    join TBL_EXPENCE on EX_ID = EXP_EXPTYPEID
    where  EXP_TYPE = 1 and  EXP_CLC_RECNO = 15020
    group by EXP_ORDRECNO) purchaseFull on purchaseFull.EXP_ORDRECNO = ORD_RECNO
    --Bosh
    left join (select  STRING_AGG(EX_VALUE1 + ' - ' + cast(EXP_PAMOUNT as varchar(max)), ', ') As PurchaseEmpty, EXP_ORDRECNO  from TBL_TRANSORDEREXP
    join TBL_EXPENCE on EX_ID = EXP_EXPTYPEID
    where  EXP_TYPE = 2 and  EXP_CLC_RECNO = 15020
    group by EXP_ORDRECNO) purchaseEmpty on purchaseEmpty.EXP_ORDRECNO = ORD_RECNO
    LEFT  JOIN TBL_POINTS FPN ON (ORD_FPOINT=FPN.PNT_RECNO)
    LEFT  JOIN TBL_POINTS TPN ON (ORD_TPOINT=TPN.PNT_RECNO)
    LEFT  JOIN TBL_POINTS BPN ON (ORD_BEGPOINT=BPN.PNT_RECNO)
    LEFT  JOIN TBL_POINTS EPN ON (ORD_ENDPOINT=EPN.PNT_RECNO)
 WHERE ORD_RECNO=" + orderId;
            DataTable expdt = new DataTable();
            expdt.Load(con.dbrun(expsql));
            string expdata = JsonConvert.SerializeObject(expdt);
            using (var client = new WebClient())
            {
                client.Headers.Add("X-WebService-Header", system.GetHeaderToken());

                var values = new NameValueCollection();
                values["JSONN"] = expdata.ToString();
                values["CLIENT"] = clientno;
                var response = client.UploadValues(webAddr, values);

                var responseString = Encoding.UTF8.GetString(response);
                string JsonString = responseString.ToString();

                dt = JsonConvert.DeserializeObject<DataTable>(JsonString);
                if (JsonString != "0" && JsonString != "-1")
                {
                    foreach (DataRow dtrw in dt.Rows)
                    {
                        resultvservice = dtrw["ORD_FICHENO"].ToString();
                    }
                }
                else
                {
                    resultvservice = JsonString;
                }

            }
        }
        return resultvservice;

    }

    public static string GetHeaderToken()
    {
        string webServiceKey = "05bd29b524f0851d42e7902d6af59bf1";
        string day = DateTime.Now.Day.ToString();
        string month = DateTime.Now.Month.ToString();
        string year = DateTime.Now.Year.ToString();
        string token = day + year + month + webServiceKey;
        string tokenHash = system.Create_MD5(token);

        return tokenHash;
    }

    public static string GetHeaderTokenCaspian()
    {
        string webServiceKey = "fe9247e883afe7f8b2f3945dea5178d2";
        string day = DateTime.Now.Day.ToString();
        string month = DateTime.Now.Month.ToString();
        string year = DateTime.Now.Year.ToString();
        string token = day + year + month + webServiceKey;
        string tokenHash = system.Create_MD5(token);

        return tokenHash;
    }

    public static string Create_MD5(string input)
    {
        // Use input string to calculate MD5 hash
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }

    public string GetCompanyName(int userId)
    {
        System.Data.SqlClient.SqlDataReader reader = null;
        string companyName = "";

        string query = @"Select top 1 CLC_ALLNAME CompanyName from T_SYS_USER u
                        left join T_SYS_USERCL uc on uc.UC_U_ID = u.U_ID
                        left join TBL_CLCARDS c on c.CLC_RECNO = uc.UC_CLC_ID
                        where isnull(c.CLC_STATUS, 0) = 0 and u.U_ID = " + userId + @"
                        order by uc.UC_ID ";

        reader = con.dbrun(query);

        if (reader.Read())
        {
            companyName = reader["CompanyName"].ToString().Trim();
        }
        else
        {
            companyName = "";
        }

        return companyName;
    }

}
public class Prices
{
    public int qty
    { get; set; }
    public float price_a
    { get; set; }
    public float price_s
    { get; set; }
    public float price_a_total
    { get; set; }
    public float price_s_total
    { get; set; }
}
public class PricesMess
{
    public string appno
    { get; set; }
    public string price_a
    { get; set; }
    public string price_s
    { get; set; }
    public string price_a_total
    { get; set; }
    public string price_s_total
    { get; set; }
}




