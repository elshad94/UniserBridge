using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

/// <summary>
/// Summary description for Integration
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Integration : System.Web.Services.WebService
{

    public Integration()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void ORDERDATA(string JSONN, string CLIENT)//3371-azrus,3658-container
    {
        Order ord = new Order();
        conn con = new conn();
        string data = "";
        JavaScriptSerializer js = new JavaScriptSerializer();
        DataTable dt = (DataTable)JsonConvert.DeserializeObject(JSONN, (typeof(DataTable)));
        foreach (DataRow dtr in dt.Rows)
        {
            string ORD_ACTTYPE = dtr["ORD_ACTTYPE"].ToString();
            string ORD_ACTTYPEINFO = dtr["ORD_ACTTYPEINFO"].ToString();
            string ORD_BEGPOINT = dtr["ORD_BEGPOINT"].ToString();
            string ORD_ENDPOINT = dtr["ORD_ENDPOINT"].ToString();
            string ORD_EBEGPOINT = dtr["ORD_EBEGPOINT"].ToString();
            string ORD_EENDPOINT = dtr["ORD_EENDPOINT"].ToString();
            string ORD_CLCRECNO = CLIENT;
            string ORD_U_ID = "";

            string ORD_FIRM = "22";
            string ORD_B_ID = "22";

            if (CLIENT == "15078") { ORD_U_ID = "4541"; }
            string ORD_FCLIENT = dtr["ORD_FCLIENT"].ToString();
            string ORD_PODCODE = dtr["ORD_PODCODE"].ToString();
            string ORD_FPOINT = dtr["ORD_FPOINT"].ToString();
            string ORD_FPOINTCODE = dtr["ORD_FPOINTCODE"].ToString();
            string ORD_TCLIENT = dtr["ORD_TCLIENT"].ToString();
            string ORD_FICHEDATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string ORD_EXPRESS_FICHENO = dtr["ORD_EXPRESS_FICHENO"].ToString();
            string ORD_TPOINT = dtr["ORD_TPOINT"].ToString();
            string ORD_TPOINTCODE = dtr["ORD_TPOINTCODE"].ToString();
            string ORD_LOADSTCARD1 = dtr["ORD_LOADSTCARD1"].ToString();
            string ORD_LOADSTCARDCODE1 = dtr["ORD_LOADSTCARDCODE1"].ToString();
            string ORD_LOADSTCARD2 = dtr["ORD_LOADSTCARD2"].ToString();
            string ORD_LOADSTCARDCODE2 = dtr["ORD_LOADSTCARDCODE2"].ToString();
            string ORD_LOADDESC = dtr["ORD_LOADDESC"].ToString();
            string ORD_LOADAMOUNT = dtr["ORD_LOADAMOUNT"].ToString();
            string ORD_LOADNOTE = dtr["ORD_LOADNOTE"].ToString();
            string ORD_VGNTRNTYPE = dtr["ORD_VGNTRNTYPE"].ToString();
            string ORD_VAGONOWNER = dtr["ORD_VAGONOWNER"].ToString();
            string ORD_VAGONOWNERSTN = dtr["ORD_VAGONOWNERSTN"].ToString();
            string ORD_VAGONTYPE = dtr["ORD_VAGONTYPE"].ToString();
            string ORD_VAGONTONNAJ = dtr["ORD_VAGONTONNAJ"].ToString();
            string ORD_VGALLTONNAJ = dtr["ORD_VGALLTONNAJ"].ToString();
            string ORD_VAGONCOUNT = dtr["ORD_VAGONCOUNT"].ToString();
            string ORD_VGALLCOUNT = dtr["ORD_VGALLCOUNT"].ToString();
            string ORD_CONTAINTYPE = dtr["ORD_CONTAINTYPE"].ToString();
            string ORD_VAGONNOTE = dtr["ORD_VAGONNOTE"].ToString();
            string ORD_STATUS = dtr["ORD_STATUS"].ToString();
            DateTime STATUSDATE = Convert.ToDateTime(dtr["ORD_STATUSDATE"].ToString());
            string ORD_STATUSDATE = STATUSDATE.ToString("yyyy-MM-dd HH:mm:ss");
            string ORD_SIGNNOTE = dtr["ORD_SIGNNOTE"].ToString();
            string ORD_SIGNSEND = dtr["ORD_SIGNSEND"].ToString();
            DateTime SENDDATE = Convert.ToDateTime(dtr["ORD_SENDDATE"].ToString());
            string ORD_SENDDATE = SENDDATE.ToString("yyyy-MM-dd HH:mm:ss");
            string ORD_DISTANCE = dtr["ORD_DISTANCE"].ToString();
            string ORD_EXPEDITOR = dtr["ORD_EXPEDITOR"].ToString();
            string ORD_EXPCLIENT = dtr["ORD_EXPCLIENT"].ToString();
            string ORD_CONTAINAMOUNT3 = dtr["ORD_CONTAINAMOUNT3"].ToString();
            string ORD_CONTAINAMOUNT5 = dtr["ORD_CONTAINAMOUNT5"].ToString();
            string ORD_CONTAINAMOUNT10 = dtr["ORD_CONTAINAMOUNT10"].ToString();
            string ORD_CONTAINAMOUNT20 = dtr["ORD_CONTAINAMOUNT20"].ToString();
            string ORD_CONTAINAMOUNT30 = dtr["ORD_CONTAINAMOUNT30"].ToString();
            string ORD_CONTAINAMOUNT40 = dtr["ORD_CONTAINAMOUNT40"].ToString();
            string ORD_CONTAINAMOUNT45 = dtr["ORD_CONTAINAMOUNT45"].ToString();
            string ORD_ECONTAINAMOUNT3 = dtr["ORD_ECONTAINAMOUNT3"].ToString();
            string ORD_ECONTAINAMOUNT5 = dtr["ORD_ECONTAINAMOUNT5"].ToString();
            string ORD_ECONTAINAMOUNT10 = dtr["ORD_ECONTAINAMOUNT10"].ToString();
            string ORD_ECONTAINAMOUNT20 = dtr["ORD_ECONTAINAMOUNT20"].ToString();
            string ORD_ECONTAINAMOUNT30 = dtr["ORD_ECONTAINAMOUNT30"].ToString();
            string ORD_ECONTAINAMOUNT40 = dtr["ORD_ECONTAINAMOUNT40"].ToString();
            string ORD_ECONTAINAMOUNT45 = dtr["ORD_ECONTAINAMOUNT45"].ToString();
            string ORD_CONTAINAMOUNTEXTRA = dtr["ORD_CONTAINAMOUNTEXTRA"].ToString();
            string ORD_ECONTAINAMOUNTEXTRA = dtr["ORD_ECONTAINAMOUNTEXTRA"].ToString();
            string ORD_CLC_PTYPE = dtr["ORD_CLC_PTYPE"].ToString();
            string ORD_ISQT = dtr["ORD_ISQT"].ToString();
            string ORD_EXP_TEMPLET = dtr["ORD_EXP_TEMPLET"].ToString();
            string ORD_CVAGONTONNAJ = dtr["ORD_CVAGONTONNAJ"].ToString();
            string ORD_PLATOWNER = dtr["ORD_PLATOWNER"].ToString();
            string ORD_PLATTYPE = dtr["ORD_PLATTYPE"].ToString();
            string ORD_PLATCOUNT = dtr["ORD_PLATCOUNT"].ToString();
            string ORD_C_ID = dtr["ORD_C_ID"].ToString();
            string ORD_READ = dtr["ORD_READ"].ToString();
            string ORD_KASPAR = dtr["ORD_KASPAR"].ToString();
            string ORD_O_ID = dtr["ORD_O_ID"].ToString();
            string ORD_PRICEAREA = dtr["ORD_PRICEAREA"].ToString();

            string ORD_PRC_ST1 = dtr["ORD_PRC_ST1"].ToString();
            string ORD_PRC_ST1CODE = dtr["ORD_PRC_ST1CODE"].ToString();
            string ORD_PRC_ST2 = dtr["ORD_PRC_ST2"].ToString();
            string ORD_PRC_ST2CODE = dtr["ORD_PRC_ST2CODE"].ToString();


            //string ORD_CLNAME = dtr["CLC_ALLNAME"].ToString();
            string vsql = "Select ORD_RECNO,ORD_STATUS from TBL_TRANSORDERS where ORD_FICHENO='" + ORD_EXPRESS_FICHENO + "'";
            System.Data.SqlClient.SqlDataReader reader1 = con.dbrun(vsql);
            DataTable dtttbl = new DataTable();
            dtttbl.Clear();
            if (!reader1.Read())
            {
                string V_sql = @"insert into TBL_TRANSORDERS (ORD_FICHENO,ORD_FICHEDATE,ORD_ACTTYPE ,ORD_ACTTYPEINFO ,ORD_BEGPOINT ,ORD_ENDPOINT ,ORD_EBEGPOINT ,
                            ORD_EENDPOINT,ORD_CLCRECNO ,ORD_FIRM ,ORD_FCLIENT ,ORD_PODCODE
                            ,ORD_FPOINT ,ORD_FPOINTCODE ,ORD_TCLIENT ,ORD_TPOINT ,ORD_TPOINTCODE ,
--ORD_PRC_ST1,
--ORD_PRC_ST1CODE,
--ORD_PRC_ST2,
--ORD_PRC_ST2CODE ,
                            ORD_LOADSTCARD1,ORD_LOADSTCARDCODE1 ,ORD_LOADSTCARD2 ,ORD_LOADSTCARDCODE2 ,ORD_LOADDESC ,ORD_LOADAMOUNT ,ORD_LOADNOTE ,ORD_VGNTRNTYPE
                            ,ORD_VAGONOWNER ,ORD_VAGONOWNERSTN ,ORD_VAGONTYPE ,ORD_VAGONTONNAJ ,ORD_VGALLTONNAJ ,ORD_VAGONCOUNT ,ORD_VGALLCOUNT
                            ,ORD_CONTAINTYPE ,ORD_VAGONNOTE,ORD_STATUS ,ORD_STATUSDATE ,ORD_SIGNNOTE ,ORD_SIGNSEND ,ORD_SENDDATE  ,ORD_DISTANCE
                            ,ORD_EXPEDITOR ,ORD_EXPCLIENT ,ORD_CONTAINAMOUNT3 ,ORD_CONTAINAMOUNT5 ,ORD_CONTAINAMOUNT10 ,ORD_CONTAINAMOUNT20 ,ORD_CONTAINAMOUNT30
                            ,ORD_CONTAINAMOUNT40,ORD_CONTAINAMOUNT45 ,ORD_ECONTAINAMOUNT3 ,ORD_ECONTAINAMOUNT5 ,ORD_ECONTAINAMOUNT10 ,ORD_ECONTAINAMOUNT20 ,ORD_ECONTAINAMOUNT30
                            ,ORD_ECONTAINAMOUNT40 ,ORD_ECONTAINAMOUNT45,ORD_CONTAINAMOUNTEXTRA ,ORD_ECONTAINAMOUNTEXTRA ,ORD_CLC_PTYPE ,ORD_ISQT ,ORD_EXP_TEMPLET 
                            ,ORD_CVAGONTONNAJ ,ORD_PLATOWNER ,ORD_PLATTYPE ,ORD_PLATCOUNT ,ORD_C_ID ,ORD_READ ,ORD_KASPAR ,ORD_B_ID,ORD_U_ID,ORD_PRICEAREA)
                            values
                            ('" + ord.Get_OrderNo() + "','" + ORD_FICHEDATE + "','" + ORD_ACTTYPE + "','" + ORD_ACTTYPEINFO + "','" + ORD_BEGPOINT + @"'
                            ,'" + ORD_ENDPOINT + "','" + ORD_EBEGPOINT + "','" + ORD_EENDPOINT + "','" + ORD_CLCRECNO + @"',
                             '" + ORD_FIRM + "',N'" + ORD_FCLIENT + "','" + ORD_PODCODE + "','" + ORD_FPOINT + "','" + ORD_FPOINTCODE + @"',
                            N'" + ORD_TCLIENT + "','" + ORD_TPOINT + "','" + ORD_TPOINTCODE + @"',
--'" + ORD_PRC_ST1 + @"',
--'" + ORD_PRC_ST1CODE + @"',
--'" + ORD_PRC_ST2 + @"',
--'" + ORD_PRC_ST2CODE + @"',
                            '" + ORD_LOADSTCARD1 + "','" + ORD_LOADSTCARDCODE1 + @"',
                            '" + ORD_LOADSTCARD2 + "','" + ORD_LOADSTCARDCODE2 + "','" + ORD_LOADDESC + "','" + ORD_LOADAMOUNT + "',N'" + ORD_LOADNOTE + "','" + ORD_VGNTRNTYPE + @"',
                            '" + ORD_VAGONOWNER + "','" + ORD_VAGONOWNERSTN + "','" + ORD_VAGONTYPE + "','" + ORD_VAGONTONNAJ + "','" + ORD_VGALLTONNAJ + "','" + ORD_VAGONCOUNT + "','" + ORD_VGALLCOUNT + @"',
                            '" + ORD_CONTAINTYPE + "',N'" + ORD_VAGONNOTE + "','" + ORD_STATUS + "','" + ORD_STATUSDATE + "',N'" + ORD_SIGNNOTE + @"',
                            '" + ORD_SIGNSEND + "','" + ORD_SENDDATE + "','" + ORD_DISTANCE + "','" + ORD_EXPEDITOR + "','" + ORD_EXPCLIENT + "','" + ORD_CONTAINAMOUNT3 + @"',
                            '" + ORD_CONTAINAMOUNT5 + "','" + ORD_CONTAINAMOUNT10 + "','" + ORD_CONTAINAMOUNT20 + "','" + ORD_CONTAINAMOUNT30 + @"',
                            '" + ORD_CONTAINAMOUNT40 + "','" + ORD_CONTAINAMOUNT45 + "','" + ORD_ECONTAINAMOUNT3 + "','" + ORD_ECONTAINAMOUNT5 + @"',
                            '" + ORD_ECONTAINAMOUNT10 + "','" + ORD_ECONTAINAMOUNT20 + "','" + ORD_ECONTAINAMOUNT30 + "','" + ORD_ECONTAINAMOUNT40 + "','" + ORD_ECONTAINAMOUNT45 + "','" + ORD_CONTAINAMOUNTEXTRA + "','" + ORD_ECONTAINAMOUNTEXTRA + @"',
                            '" + ORD_CLC_PTYPE + "','" + ORD_ISQT + "','" + ORD_EXP_TEMPLET + "','" + ORD_CVAGONTONNAJ + "','" + ORD_PLATOWNER + "','" + ORD_PLATTYPE + "','" + ORD_PLATCOUNT + "','" + ORD_C_ID + @"',
                            '" + ORD_READ + "','" + ORD_KASPAR + "','" + ORD_B_ID + "','" + ORD_U_ID + "','" + ORD_PRICEAREA + "');SELECT SCOPE_IDENTITY() as _RECNO";
                
                
               
                
                System.Data.SqlClient.SqlDataReader reader = con.dbrun(V_sql);
                int RECNO = 0;
                if (reader.Read())
                {
                    RECNO = int.Parse(reader["_RECNO"].ToString());


                    string requery = @"insert into TBL_TRANSPORTLIST([TRN_ORDID], [TRN_PREFIX], [TRN_NO], [TRN_OWNER], [TRN_TRTYPE], [TRN_TRCAT], [TRN_TYPE], [TRN_FULLEMPTY], [TRN_TNID], [TRN_COUNT], [TRN_PLATOWNER], [TRN_PLATTYPE], [TRN_PLATCOUNT], [TRN_ORDFICHENO], [TRN_STATUS], [TRN_CRTDATE])
                                        select "+ RECNO + @", [TRN_PREFIX], [TRN_NO], [TRN_OWNER], [TRN_TRTYPE], [TRN_TRCAT], [TRN_TYPE], [TRN_FULLEMPTY], [TRN_TNID], [TRN_COUNT], [TRN_PLATOWNER], [TRN_PLATTYPE], [TRN_PLATCOUNT], [TRN_ORDFICHENO], [TRN_STATUS], [TRN_CRTDATE]
                                        from IMSART_MM_GEO.dbo.TBL_TRANSPORTLIST
                                        where TRN_ORDID = " + dtr["ORD_RECNO"].ToString() + ";";

                    requery += @"SELECT ORD_FICHENO FROM TBL_TRANSORDERS WHERE ORD_RECNO=" + RECNO;
                    dtttbl.Load(con.dbrun(requery));
                    data = JsonConvert.SerializeObject(dtttbl);
                }
                reader.Close();

            }
            else
            {
                if (reader1["ORD_STATUS"].ToString() != "1")
                {
                    data = "-1";
                }
                else
                {
                    string V_sql = @"UPDATE TBL_TRANSORDERS SET
                                        ORD_ACTTYPE='" + ORD_ACTTYPE + @"',
                                        ORD_ACTTYPEINFO='" + ORD_ACTTYPEINFO + @"',
                                        ORD_BEGPOINT='" + ORD_BEGPOINT + @"',
                                        ORD_ENDPOINT='" + ORD_ENDPOINT + @"',
                                        ORD_EBEGPOINT='" + ORD_EBEGPOINT + @"',
                                        ORD_EENDPOINT='" + ORD_EENDPOINT + @"',
                                        ORD_CLCRECNO ='" + ORD_CLCRECNO + @"',
                                        ORD_FIRM ='" + ORD_FIRM + @"',
                                        ORD_FCLIENT ='" + ORD_FCLIENT + @"',
                                        ORD_PODCODE='" + ORD_PODCODE + @"',
                                        ORD_FPOINT ='" + ORD_FPOINT + @"',
                                        ORD_FPOINTCODE='" + ORD_FPOINTCODE + @"',
                                        ORD_TCLIENT=N'" + ORD_TCLIENT + @"',
                                        ORD_TPOINT='" + ORD_TPOINT + @"',
                                        ORD_TPOINTCODE='" + ORD_TPOINTCODE + @"',
                                        --ORD_PRC_ST1='" + ORD_PRC_ST1 + @"',
                                        --ORD_PRC_ST1CODE ='" + ORD_PRC_ST1CODE + @"',
                                        --ORD_PRC_ST2 ='" + ORD_LOADSTCARD2 + @"',
                                        --ORD_PRC_ST2CODE ='" + ORD_PRC_ST2CODE + @"',
                                        ORD_LOADSTCARD1='" + ORD_LOADSTCARD1 + @"',
                                        ORD_LOADSTCARDCODE1 ='" + ORD_LOADSTCARDCODE1 + @"',
                                        ORD_LOADSTCARD2 ='" + ORD_LOADSTCARD2 + @"',
                                        ORD_LOADSTCARDCODE2 ='" + ORD_LOADSTCARDCODE2 + @"',
                                        ORD_LOADDESC='" + ORD_LOADDESC + @"',
                                        ORD_LOADAMOUNT='" + ORD_LOADAMOUNT + @"',
                                        ORD_LOADNOTE=N'" + ORD_LOADNOTE + @"',
                                        ORD_VGNTRNTYPE='" + ORD_VGNTRNTYPE + @"',
                                        ORD_VAGONOWNER='" + ORD_VAGONOWNER + @"',
                                        ORD_VAGONOWNERSTN ='" + ORD_VAGONOWNERSTN + @"',
                                        ORD_VAGONTYPE='" + ORD_VAGONTYPE + @"',
                                        ORD_VAGONTONNAJ ='" + ORD_VAGONTONNAJ + @"',
                                        ORD_VGALLTONNAJ ='" + ORD_VGALLTONNAJ + @"',
                                        ORD_VAGONCOUNT ='" + ORD_VAGONCOUNT + @"',
                                        ORD_VGALLCOUNT='" + ORD_VGALLCOUNT + @"',
                                        ORD_CONTAINTYPE ='" + ORD_CONTAINTYPE + @"',
                                        ORD_VAGONNOTE=N'" + ORD_VAGONNOTE + @"',
                                        ORD_STATUS ='" + ORD_STATUS + @"',
                                        ORD_STATUSDATE ='" + ORD_STATUSDATE + @"',
                                        ORD_SIGNNOTE =N'" + ORD_SIGNNOTE + @"',
                                        ORD_SIGNSEND ='" + ORD_SIGNSEND + @"',
                                        ORD_SENDDATE ='" + ORD_SENDDATE + @"',
                                        ORD_DISTANCE='" + ORD_DISTANCE + @"',
                                        ORD_EXPEDITOR='" + ORD_EXPEDITOR + @"',
                                        ORD_EXPCLIENT='" + ORD_EXPCLIENT + @"',
                                        ORD_CONTAINAMOUNT3='" + ORD_CONTAINAMOUNT3 + @"',
                                        ORD_CONTAINAMOUNT5='" + ORD_CONTAINAMOUNT5 + @"',
                                        ORD_CONTAINAMOUNT10='" + ORD_CONTAINAMOUNT10 + @"',
                                        ORD_CONTAINAMOUNT20='" + ORD_CONTAINAMOUNT20 + @"',
                                        ORD_CONTAINAMOUNT30='" + ORD_CONTAINAMOUNT30 + @"',
                                        ORD_CONTAINAMOUNT40 ='" + ORD_CONTAINAMOUNT40 + @"',
                                        ORD_CONTAINAMOUNT45 ='" + ORD_CONTAINAMOUNT45 + @"',
                                        ORD_ECONTAINAMOUNT3='" + ORD_ECONTAINAMOUNT3 + @"',
                                        ORD_ECONTAINAMOUNT5 ='" + ORD_ECONTAINAMOUNT5 + @"',
                                        ORD_ECONTAINAMOUNT10='" + ORD_ECONTAINAMOUNT10 + @"',
                                        ORD_ECONTAINAMOUNT20='" + ORD_ECONTAINAMOUNT20 + @"',
                                        ORD_ECONTAINAMOUNT30='" + ORD_ECONTAINAMOUNT30 + @"',
                                        ORD_ECONTAINAMOUNT40='" + ORD_ECONTAINAMOUNT40 + @"',
                                        ORD_ECONTAINAMOUNT45='" + ORD_ECONTAINAMOUNT45 + @"',
                                        ORD_CONTAINAMOUNTEXTRA='" + ORD_CONTAINAMOUNTEXTRA + @"',
                                        ORD_ECONTAINAMOUNTEXTRA='" + ORD_ECONTAINAMOUNTEXTRA + @"',
                                        ORD_CLC_PTYPE='" + ORD_CLC_PTYPE + @"',
                                        ORD_ISQT='" + ORD_ISQT + @"',
                                        ORD_EXP_TEMPLET='" + ORD_EXP_TEMPLET + @"',
                                        ORD_CVAGONTONNAJ='" + ORD_CVAGONTONNAJ + @"',
                                        ORD_PLATOWNER='" + ORD_PLATOWNER + @"',
                                        ORD_PLATTYPE='" + ORD_PLATTYPE + @"',
                                        ORD_PLATCOUNT='" + ORD_PLATCOUNT + @"',
                                        ORD_C_ID='" + ORD_C_ID + @"',
                                        ORD_READ ='" + ORD_READ + @"',
                                        ORD_KASPAR ='" + ORD_KASPAR + @"',
                                       
                                        ORD_B_ID='" + ORD_B_ID + @"'  where ORD_FICHENO=" + ORD_EXPRESS_FICHENO;
                    System.Data.SqlClient.SqlDataReader reader = con.dbrun(V_sql);
                    data = "0";
                }
            }
            Context.Response.Write(data);
        }
    }

    [WebMethod]
    public void GET_PodcodeNts(string JSONDATA)
    {
        conn con = new conn();
        JavaScriptSerializer js = new JavaScriptSerializer();
        DataTable dt = (DataTable)JsonConvert.DeserializeObject(JSONDATA, (typeof(DataTable)));
        string data = "";
        string fichenos = "";
        foreach (DataRow dtr in dt.Rows)
        {
            fichenos += "'" + dtr["ORD_EXPRESS_FICHENO"].ToString() + "',";
        }
        DataTable dtttbl = new DataTable();
        string NOES = fichenos.TrimEnd(fichenos[fichenos.Length - 1]);
        string v_sql = @"select ORD_PODCODE,ORD_NTS,ORD_STOTAL,ORD_FICHENO from TBL_TRANSORDERS 
        where ORD_FICHENO in (" + NOES + ") and ORD_STATUS>=15";
        System.Data.SqlClient.SqlDataReader reader = con.dbrun(v_sql);
        dtttbl.Load(reader);
        data = JsonConvert.SerializeObject(dtttbl);
        if (dtttbl.Rows.Count != 0)
        {
            Context.Response.Write(data);
        }
        else
        {
            Context.Response.Write("0");
        }
        reader.Close();
    }

}
