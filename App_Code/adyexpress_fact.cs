using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

/// <summary>
/// Summary description for adyexpress_fact
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class adyexpress_fact : System.Web.Services.WebService
{

    public adyexpress_fact()
    {
    }


    [WebMethod]
    public void FACT_DATA(string JSONN)
    {
        conn con = new conn();
        JavaScriptSerializer js = new JavaScriptSerializer();
        DataTable dt = (DataTable)JsonConvert.DeserializeObject(JSONN, (typeof(DataTable)));
        foreach (DataRow dtr in dt.Rows)
        {


            int FCT_ID = int.Parse(dtr["_ID"].ToString());
            DateTime dateload = Convert.ToDateTime(dtr["FCT_LOADDATE"].ToString());
            string FCT_LOADDATE = dateload.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime imdate = Convert.ToDateTime(dtr["FCT_IMDATE"].ToString());
            string FCT_IMDATE = imdate.ToString("yyyy-MM-dd HH:mm:ss");
            //DateTime exdate = Convert.ToDateTime(dtr["FCT_EXDATE"].ToString());
            //string FCT_EXDATE = exdate.ToString("yyyy-MM-dd HH:mm:ss");
            ///////////////////////////////////////////////////////////////////////////////////////////////
            string FCT_NOTE = dtr["FCT_NOTE"].ToString();
            string FCT_CUSTOMP = dtr["FCT_CUSTOMP"].ToString();
            string FCT_CUSTOMS = dtr["FCT_CUSTOMS"].ToString();
            string FCT_SECURITYP = dtr["FCT_SECURITYP"].ToString();
            string FCT_SECURITYS = dtr["FCT_SECURITYS"].ToString();
            string FCT_FERRYS = dtr["FCT_FERRYS"].ToString();
            string FCT_FERRYP = dtr["FCT_FERRYP"].ToString();
            string FCT_ADYP = dtr["FCT_ADYP"].ToString();
            string FCT_ADYS = dtr["FCT_ADYS"].ToString();
            string FCT_NOTE2 = dtr["FCT_NOTE2"].ToString();
            string FCT_STNAME2 = dtr["FCT_STNAME2"].ToString();
            string FCT_PACKET = dtr["FCT_PACKET"].ToString();
            string FCT_ORDINAL = dtr["FCT_ORDINAL"].ToString();
            string FCT_NTS = dtr["FCT_NTS"].ToString();
            string FCT_TELEGRAM = dtr["FCT_TELEGRAM"].ToString();
            string FCT_FIRM = dtr["FCT_FIRM"].ToString();
            string FCT_CLIENTCODE = dtr["FCT_CLIENTCODE"].ToString();
            string FCT_CLIENTNAME = dtr["FCT_CLIENTNAME"].ToString();
            string FCT_ORDID = dtr["FCT_ORDID"].ToString();
            string FCT_DISTANCE = dtr["FCT_DISTANCE"].ToString();
            string FCT_CASPARP = dtr["FCT_CASPARP"].ToString();
            string FCT_CASPARS = dtr["FCT_CASPARS"].ToString();
            string FCT_CASPARE = dtr["FCT_CASPARE"].ToString();
            string FCT_CASPARPR = dtr["FCT_CASPARPR"].ToString();
            string FCT_BRIDGEP = dtr["FCT_BRIDGEP"].ToString();
            string FCT_BRIDGES = dtr["FCT_BRIDGES"].ToString();
            string FCT_BRIDGEE = dtr["FCT_BRIDGEE"].ToString();
            string FCT_BRIDGEPR = dtr["FCT_BRIDGEPR"].ToString();
            string FCT_FCLIENT = dtr["FCT_FCLIENT"].ToString();
            string FCT_TCLIENT = dtr["FCT_TCLIENT"].ToString();
            string FCT_STNAME1 = dtr["FCT_STNAME1"].ToString();
            string FCT_FERRYTOTAL = dtr["FCT_FERRYTOTAL"].ToString();
            string FCT_ADYTOTAL = dtr["FCT_ADYTOTAL"].ToString();
            string FCT_SECURITYTOTAL = dtr["FCT_SECURITYTOTAL"].ToString();
            string FCT_CASPARTOTAL = dtr["FCT_CASPARTOTAL"].ToString();
            string FCT_BRIDGETOTAL = dtr["FCT_BRIDGETOTAL"].ToString();
            string FCT_CWEIGHT = dtr["FCT_CWEIGHT"].ToString();
            string FCT_CEMPTY = dtr["FCT_CEMPTY"].ToString();
            string FCT_CLIENTID = dtr["FCT_CLIENTID"].ToString();
            string FCT_ORDER = dtr["FCT_ORDER"].ToString();
            string FCT_NOTE3 = dtr["FCT_NOTE3"].ToString();
            string FCT_SCALES = dtr["FCT_SCALES"].ToString();
            string FCT_ITEMDESC = dtr["FCT_ITEMDESC"].ToString();
            string FCT_TOTALP = dtr["FCT_TOTALP"].ToString();
            string FCT_TOTALS = dtr["FCT_TOTALS"].ToString();
            string FCT_CLC_RECNO = dtr["FCT_CLC_RECNO"].ToString();
            string FCT_EXP_TEMPLET = dtr["FCT_EXP_TEMPLET"].ToString();
            string FCT_TRAINNO = dtr["FCT_TRAINNO"].ToString();
            string FCT_CALCNOTE = dtr["FCT_CALCNOTE"].ToString();
            string FCT_C_ID = dtr["FCT_C_ID"].ToString();
            string FCT_WAGON_CC = dtr["FCT_WAGON_CC"].ToString();
            string FCT_CONTAINER_CC = dtr["FCT_CONTAINER_CC"].ToString();
            string FCT_ADYCODE = dtr["FCT_ADYCODE"].ToString();
            string FCT_EXPCODE = dtr["FCT_EXPCODE"].ToString();
            string FCT_PLOMB = dtr["FCT_PLOMB"].ToString();
            string FCT_DEGREE = dtr["FCT_DEGREE"].ToString();
            string FCT_SPECODE = dtr["FCT_SPECODE"].ToString();
            string FCT_QNQ_ID = dtr["FCT_QNQ_ID"].ToString();
            /////////////////////////////////////////////////////////////////////////////

            string FCT_OVERHEAD = dtr["FCT_OVERHEAD"].ToString();
            int FCT_WAGONNO = int.Parse(dtr["FCT_WAGONNO"].ToString());
            string FCT_CONTAINERNO = dtr["FCT_CONTAINERNO"].ToString();
            string FCT_CTYPE = dtr["FCT_CTYPE"].ToString();
            string FCT_OWNER = dtr["FCT_OWNER"].ToString();
            string FCT_WTYPE = dtr["FCT_WTYPE"].ToString();
            string FCT_SUBCODE = dtr["FCT_SUBCODE"].ToString();
            string fct_begpoint = dtr["fct_begpoint"].ToString();
            string fct_endpoint = dtr["fct_endpoint"].ToString();
            string fct_fpoint = dtr["fct_fpoint"].ToString();
            string fct_tpoint = dtr["fct_tpoint"].ToString();
            int fct_type = int.Parse(dtr["fct_type"].ToString());
            string FCT_WEIGHT = dtr["FCT_WEIGHT"].ToString();
            string FCT_QNQ = dtr["FCT_QNQ"].ToString();
            string FCT_ETSNG = dtr["FCT_ETSNG"].ToString();
            int FCT_MOUNTH = int.Parse(dtr["FCT_MOUNTH"].ToString());
            int FCT_YEAR = int.Parse(dtr["FCT_YEAR"].ToString());

            string V_sql = @"select FCT_EXPRESS_ID from TBL_FACT where FCT_EXPRESS_ID=" + FCT_ID;
            System.Data.SqlClient.SqlDataReader reader = con.dbrun(V_sql);
            if (!reader.Read())
            {
                string sql = @"insert into TBL_FACT (FCT_EXPRESS_ID,FCT_LOADDATE,FCT_OVERHEAD,
                        FCT_WAGONNO,FCT_CONTAINERNO,FCT_SUBCODE,
                        fct_begpoint,fct_endpoint,fct_fpoint,
                        fct_tpoint,fct_type,FCT_WEIGHT,FCT_QNQ,FCT_ETSNG,
                        FCT_MOUNTH,FCT_YEAR,FCT_IMDATE,FCT_CTYPE,FCT_OWNER,FCT_WTYPE,

FCT_NOTE,FCT_CUSTOMP,FCT_CUSTOMS,FCT_SECURITYP,FCT_SECURITYS,FCT_FERRYS,FCT_FERRYP,
FCT_ADYP,FCT_ADYS,FCT_NOTE2,FCT_STNAME2,FCT_PACKET,FCT_ORDINAL,FCT_NTS,FCT_TELEGRAM,
FCT_FIRM,FCT_CLIENTCODE,FCT_CLIENTNAME,FCT_DISTANCE,FCT_CASPARP,FCT_CASPARS,FCT_CASPARE,
FCT_CASPARPR,FCT_BRIDGEP,FCT_BRIDGES,FCT_BRIDGEE,FCT_BRIDGEPR,FCT_FCLIENT,FCT_TCLIENT,
FCT_STNAME1,FCT_FERRYTOTAL,FCT_ADYTOTAL,FCT_SECURITYTOTAL,FCT_CASPARTOTAL,FCT_BRIDGETOTAL,
FCT_CWEIGHT,FCT_CEMPTY,FCT_CLIENTID,FCT_NOTE3,FCT_SCALES,FCT_TOTALP,FCT_TOTALS,FCT_CLC_RECNO,
FCT_EXP_TEMPLET,FCT_TRAINNO,FCT_CALCNOTE,FCT_C_ID,FCT_WAGON_CC,FCT_CONTAINER_CC,FCT_ADYCODE,
FCT_EXPCODE,FCT_PLOMB,FCT_DEGREE,FCT_SPECODE,FCT_QNQ_ID)
                        values
                        (" + FCT_ID + ",'" + FCT_LOADDATE + "','" + FCT_OVERHEAD + @"',
                         '" + FCT_WAGONNO + "','" + FCT_CONTAINERNO + "','" + FCT_SUBCODE + @"',
                         '" + fct_begpoint + "','" + fct_endpoint + "','" + fct_fpoint + @"',
                         '" + fct_tpoint + "','" + fct_type + "','" + FCT_WEIGHT + @"',
                         '" + FCT_QNQ + "','" + FCT_ETSNG + "'," + FCT_MOUNTH + @",
                         '" + FCT_YEAR + "','" + FCT_IMDATE + "','" + FCT_CTYPE + @"',
                         '" + FCT_OWNER + "','" + FCT_WTYPE + @"',

'" + FCT_NOTE + "','" + FCT_CUSTOMP + "','" + FCT_CUSTOMS + @"','" + FCT_SECURITYP + "','" + FCT_SECURITYS + "','" + FCT_FERRYS + @"',
'" + FCT_FERRYP + "','" + FCT_ADYP + "','" + FCT_ADYS + @"','" + FCT_NOTE2 + "','" + FCT_STNAME2 + "','" + FCT_PACKET + @"',
'" + FCT_ORDINAL + "','" + FCT_NTS + "','" + FCT_TELEGRAM + @"','" + FCT_FIRM + "','" + FCT_CLIENTCODE + "','" + FCT_CLIENTNAME + @"',
'" + FCT_DISTANCE + "','" + FCT_CASPARP + "','" + FCT_CASPARS + "','" + FCT_CASPARE + @"','" + FCT_CASPARPR + "','" + FCT_BRIDGEP + "','" + FCT_BRIDGES + @"',
'" + FCT_BRIDGEE + "','" + FCT_BRIDGEPR + "','" + FCT_FCLIENT + @"','" + FCT_TCLIENT + "','" + FCT_STNAME1 + "','" + FCT_FERRYTOTAL + @"',
'" + FCT_ADYTOTAL + "','" + FCT_SECURITYTOTAL + "','" + FCT_CASPARTOTAL + @"','" + FCT_BRIDGETOTAL + "','" + FCT_CWEIGHT + "','" + FCT_CEMPTY + @"',
'" + FCT_CLIENTID + "','" + FCT_NOTE3 + "','" + FCT_SCALES + @"','" + FCT_TOTALP + "','" + FCT_TOTALS + "','" + FCT_CLC_RECNO + @"',
'" + FCT_EXP_TEMPLET + "','" + FCT_TRAINNO + "','" + FCT_CALCNOTE + @"','" + FCT_C_ID + "','" + FCT_WAGON_CC + "','" + FCT_CONTAINER_CC + @"',
'" + FCT_ADYCODE + "','" + FCT_EXPCODE + "','" + FCT_PLOMB + @"','" + FCT_DEGREE + "','" + FCT_SPECODE + "','" + FCT_QNQ_ID + @"' )";
                con.dbrun(sql);
            }
            else
            {
                string sql = @"UPDATE TBL_FACT  set 
                                FCT_LOADDATE='" + FCT_LOADDATE + @"',
                                FCT_OVERHEAD='" + FCT_OVERHEAD + @"',
                                FCT_WAGONNO='" + FCT_WAGONNO + @"',
                                FCT_CONTAINERNO='" + FCT_CONTAINERNO + @"',
                                FCT_SUBCODE='" + FCT_SUBCODE + @"',
                                fct_begpoint='" + fct_begpoint + @"',
                                fct_endpoint='" + fct_endpoint + @"',
                                fct_fpoint='" + fct_fpoint + @"',
                                fct_tpoint='" + fct_tpoint + @"',
                                fct_type='" + fct_type + @"',
                                FCT_WEIGHT='" + FCT_WEIGHT + @"',
                                FCT_QNQ='" + FCT_QNQ + @"',
                                FCT_ETSNG='" + FCT_ETSNG + @"',
                                FCT_MOUNTH=" + FCT_MOUNTH + @",
                                FCT_YEAR='" + FCT_YEAR + @"',
                                FCT_IMDATE='" + FCT_IMDATE + @"',
                                FCT_CTYPE='" + FCT_CTYPE + @"',
                                FCT_OWNER='" + FCT_OWNER + @"',
                                FCT_WTYPE='" + FCT_WTYPE + @"',

                                 FCT_NOTE='" + FCT_NOTE + @"',
                                 FCT_CUSTOMP='" + FCT_CUSTOMP + @"',
                                 FCT_CUSTOMS='" + FCT_CUSTOMS + @"',
                                 FCT_SECURITYP='" + FCT_SECURITYP + @"',
                                 FCT_SECURITYS='" + FCT_SECURITYS + @"',
                                 FCT_FERRYS='" + FCT_FERRYS + @"',
                                 FCT_FERRYP='" + FCT_FERRYP + @"',
                                 FCT_ADYP='" + FCT_ADYP + @"',
                                 FCT_ADYS='" + FCT_ADYS + @"',
                                 FCT_NOTE2='" + FCT_NOTE2 + @"',
                                 FCT_STNAME2='" + FCT_STNAME2 + @"',
                                 FCT_PACKET='" + FCT_PACKET + @"',
                                 FCT_ORDINAL='" + FCT_ORDINAL + @"',
                                 FCT_NTS='" + FCT_NTS + @"',
                                 FCT_TELEGRAM='" + FCT_TELEGRAM + @"',
                                 FCT_FIRM='" + FCT_FIRM + @"',
                                 FCT_CLIENTCODE='" + FCT_CLIENTCODE + @"',
                                 FCT_CLIENTNAME='" + FCT_CLIENTNAME + @"',
                                 FCT_DISTANCE='" + FCT_DISTANCE + @"',
                                 FCT_CASPARP='" + FCT_CASPARP + @"',
                                 FCT_CASPARS='" + FCT_CASPARS + @"',
                                 FCT_CASPARE='" + FCT_CASPARE + @"',
                                 FCT_CASPARPR='" + FCT_CASPARPR + @"',
                                 FCT_BRIDGEP='" + FCT_BRIDGEP + @"',
                                 FCT_BRIDGES='" + FCT_BRIDGES + @"',
                                 FCT_BRIDGEE='" + FCT_BRIDGEE + @"',
                                 FCT_BRIDGEPR='" + FCT_BRIDGEPR + @"',
                                 FCT_FCLIENT='" + FCT_FCLIENT + @"',
                                 FCT_TCLIENT='" + FCT_TCLIENT + @"',
                                 FCT_STNAME1='" + FCT_STNAME1 + @"',
                                 FCT_FERRYTOTAL='" + FCT_FERRYTOTAL + @"',
                                 FCT_ADYTOTAL='" + FCT_ADYTOTAL + @"',
                                 FCT_SECURITYTOTAL='" + FCT_SECURITYTOTAL + @"',
                                 FCT_CASPARTOTAL='" + FCT_CASPARTOTAL + @"',
                                 FCT_BRIDGETOTAL='" + FCT_BRIDGETOTAL + @"',
                                 FCT_CWEIGHT='" + FCT_CWEIGHT + @"',
                                 FCT_CEMPTY='" + FCT_CEMPTY + @"',
                                 FCT_CLIENTID='" + FCT_CLIENTID + @"',
                                 FCT_NOTE3='" + FCT_NOTE3 + @"',
                                 FCT_SCALES='" + FCT_SCALES + @"',
                                 FCT_TOTALP='" + FCT_TOTALP + @"',
                                 FCT_TOTALS='" + FCT_TOTALS + @"',
                                 FCT_CLC_RECNO='" + FCT_CLC_RECNO + @"',
                                 FCT_EXP_TEMPLET='" + FCT_EXP_TEMPLET + @"',
                                 FCT_TRAINNO='" + FCT_TRAINNO + @"',
                                 FCT_CALCNOTE='" + FCT_CALCNOTE + @"',
                                 FCT_C_ID='" + FCT_C_ID + @"',
                                 FCT_WAGON_CC='" + FCT_WAGON_CC + @"',
                                 FCT_CONTAINER_CC='" + FCT_CONTAINER_CC + @"',
                                 FCT_ADYCODE='" + FCT_ADYCODE + @"',
                                 FCT_EXPCODE='" + FCT_EXPCODE + @"',
                                 FCT_PLOMB='" + FCT_PLOMB + @"',
                                 FCT_DEGREE='" + FCT_DEGREE + @"',
                                 FCT_SPECODE='" + FCT_SPECODE + @"',
                                 FCT_QNQ_ID='" + FCT_QNQ_ID + @"' 
                                WHERE FCT_EXPRESS_ID=" + FCT_ID;
                con.dbrun(sql);
            }
            reader.Close();
        }
    }
    [WebMethod]
    public void FACT_EXPDATA(string JSONNEXPENCE)
    {
        conn con = new conn();
        JavaScriptSerializer js = new JavaScriptSerializer();
        DataTable expdt = (DataTable)JsonConvert.DeserializeObject(JSONNEXPENCE, (typeof(DataTable)));
        foreach (DataRow dtrexp in expdt.Rows)
        {
            int FEXP_ID = int.Parse(dtrexp["FEXP_ID"].ToString());
            int FEXP_FCT_ID = int.Parse(dtrexp["FEXP_FCT_ID"].ToString());
            string FEXP_QTY = dtrexp["FEXP_QTY"].ToString();
            int FEXP_EXPTYPEID = int.Parse(dtrexp["FEXP_EXPTYPEID"].ToString());
            string FEXP_EXPENSE = dtrexp["FEXP_EXPENSE"].ToString();
            string FEXP_AMOUNT = dtrexp["FEXP_AMOUNT"].ToString();

            string V_sql = "select FEXP_EXPRESS_ID from TBL_FACTEXP where FEXP_EXPRESS_ID=" + FEXP_ID;
            System.Data.SqlClient.SqlDataReader reader = con.dbrun(V_sql);
            if (!reader.Read())
            {
                string vsql = @"insert into TBL_FACTEXP (FEXP_EXPRESS_ID,FEXP_FCT_ID,FEXP_EXPTYPEID,FEXP_PEXPENSE,FEXP_PAMOUNT,FEXP_QTY) 
                                    values
                                    (" + FEXP_ID + "," + FEXP_FCT_ID + "," + FEXP_EXPTYPEID + ",'" + FEXP_EXPENSE + "','" + FEXP_AMOUNT + "','" + FEXP_QTY + "')";
                con.dbrun(vsql);
            }
            else
            {
                string vsql = @"UPDATE TBL_FACTEXP SET
                                FEXP_FCT_ID=" + FEXP_FCT_ID + @",
                                FEXP_EXPTYPEID=" + FEXP_EXPTYPEID + @",
                                FEXP_PEXPENSE='" + FEXP_EXPENSE + @"',
                                FEXP_PAMOUNT='" + FEXP_AMOUNT + @"',
                                FEXP_QTY='" + FEXP_QTY + @"'
                                WHERE FEXP_EXPRESS_ID=" + FEXP_ID;
                con.dbrun(vsql);
            }
            reader.Close();
        }
    }
}
