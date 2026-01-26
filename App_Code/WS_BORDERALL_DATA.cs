using DevExpress.Web.ASPxUploadControl;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for WS_BORDERALL_DATA
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WS_BORDERALL_DATA : System.Web.Services.WebService {

    public WS_BORDERALL_DATA () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public void Get_Data(DateTime bgdaate, DateTime EndDate, int ALL, string WAGONNO,string FILTERFEALD,int Skip,int Take)
    {
        try
        {
            string v_sql = @"Select * from V_BORDER_ALLDATA2 where 
        (( cast(FCT_IMDATE as date) between '" + bgdaate.ToString("yyyy-MM-dd") + "' and '" + EndDate.ToString("yyyy-MM-dd") + "' and " + ALL + "=0) OR " + ALL + "=1 OR (" + ALL + "=2 and " + FILTERFEALD + " IN (" + WAGONNO + @")))
        Order by FCT_ID DESC
		OFFSET     " + Skip + @" ROWS       -- skip 10 rows
		FETCH NEXT " + Take + " ROWS ONLY; -- take 10 rows";
            conn con = new conn();
            System.Data.SqlClient.SqlDataReader reader = con.dbrun(v_sql);

            DataTable dt = new DataTable();
            JavaScriptSerializer js = new JavaScriptSerializer();
            dt.Load(reader);
            string data = JsonConvert.SerializeObject(dt);

            reader.Close();
            // return data;
            Context.Response.Write(data);
        }
        catch {  }
    }

    [WebMethod]
    public void Get_Data_Count(DateTime bgdaate, DateTime EndDate, int ALL, string WAGONNO, string FILTERFEALD)
    {
        try
        {
            string v_sql = @"Select COUNT(*) _count from V_BORDER_ALLDATA where 
        (( FCT_IMDATE between '" + bgdaate.ToString("yyyy-MM-dd") + "' and '" + EndDate.ToString("yyyy-MM-dd") + "' and " + ALL + "=0) OR " + ALL + "=1 OR (" + ALL + "=2 and " + FILTERFEALD + " IN (" + WAGONNO + @")))
        ";
            conn con = new conn();
            System.Data.SqlClient.SqlDataReader reader = con.dbrun(v_sql);

            DataTable dt = new DataTable();
            JavaScriptSerializer js = new JavaScriptSerializer();
            dt.Load(reader);
            string data = JsonConvert.SerializeObject(dt);

            reader.Close();
            // return data;
            Context.Response.Write(data);
        }
        catch { }
    }

    [WebMethod]
    public void Get_AllData(string v_sql)
    {
        try
        {
            if (!v_sql.Contains("del"))
            {
                conn con = new conn();
                System.Data.SqlClient.SqlDataReader reader = con.dbrun(v_sql);

                DataTable dt = new DataTable();
                JavaScriptSerializer js = new JavaScriptSerializer();
                dt.Load(reader);
                string data = JsonConvert.SerializeObject(dt);

                reader.Close();
                Context.Response.Write(data);
            }
        }
        catch { }
    }

  [WebMethod]
    public void Get_FactId(string Overheads, string WagonNo)
    {
        try
        {
            string v_sql = @"select FCT_ID from TBL_FACT where FCT_OVERHEAD=" + Overheads + " and FCT_WAGONNO=" + WagonNo;
            conn con = new conn();
            System.Data.SqlClient.SqlDataReader reader = con.dbrun(v_sql);

            DataTable dt = new DataTable();
            JavaScriptSerializer js = new JavaScriptSerializer();
            dt.Load(reader);
            string data = JsonConvert.SerializeObject(dt);

            reader.Close();
            // return data;
            Context.Response.Write(data);
        }
        catch { }
    }
    
    [WebMethod]
    public void FileUpload(string imgSrc, string fct_id, string u_id = "")
    {

        string UploadDirectory = "~/Uploads/";
        string FDir = "\\Uploads\\";
        string f = "";
        string fileName = "";


        //int COUNT_ = Convert.ToInt32(countFileMethod(wdId, overHeadType).Rows[0]["COUNT_"]);
        //int CONTEINERCOUNT = countContainer(wdId);

        
           // string pureBase64Encoded = imgSrc.Substring(imgSrc.IndexOf(",") + 1);

            Byte[] bitmapData = Convert.FromBase64String(imgSrc);
            System.IO.MemoryStream streamBitmap = new System.IO.MemoryStream(bitmapData);

            Bitmap bitImage = new Bitmap((Bitmap)System.Drawing.Image.FromStream(streamBitmap));

            Bitmap dstImg = new Bitmap(bitImage.Width, bitImage.Height);
            dstImg.SetResolution(72, 72);
            Graphics g = Graphics.FromImage(dstImg);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;


            // Resize the original
            g.DrawImage(bitImage, 0, 0, bitImage.Width, bitImage.Height);
            g.Dispose();

            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 100);          // 100% Percent Compression

            DateTime d;
            d = DateTime.Now;
            f = "FACT" + int.Parse("1").ToString("000") + "_" + d.Year.ToString() + d.Month.ToString() + d.Day.ToString() + d.Hour.ToString() + d.Minute.ToString() + d.Second.ToString() + d.Millisecond.ToString() + ".jpg";
            UploadDirectory = UploadDirectory + d.Year.ToString() + "/" + d.Month.ToString() + "/" + d.Day.ToString();
            FDir = FDir + d.Year.ToString() + "\\" + d.Month.ToString() + "\\" + d.Day.ToString() + "\\";
            String path = Server.MapPath(UploadDirectory);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            UploadDirectory = UploadDirectory + "/";
            fileName = Path.Combine(HttpContext.Current.Server.MapPath(UploadDirectory), f);
            
            dstImg.Save(fileName, ImageCodecInfo.GetImageEncoders()[1], encoderParameters);   // jpg format
            dstImg.Dispose();


            string drc = UploadDirectory + f;

            DataTable dt = new DataTable();

            dt.Columns.Add("UF_FILE", typeof(string));
            dt.Columns.Add("UF_UFILE", typeof(string));
            dt.Columns.Add("UF_ID", typeof(string));
        string v_sql = @"INSERT INTO TBL_UPFILES (UF_TYPE,UF_APP,UF_FILE,UF_DOCID,UF_UFILE,UF_DATE,UF_U_ID,UF_STATUS,UF_TYPE2)
            VALUES('1','FACT','~" + FDir + f + "'," + fct_id + ",'" + f + "','" + d + "','" + u_id + "','1','1') SELECT SCOPE_IDENTITY() as UF_ID ";
            conn con = new conn();
            System.Data.SqlClient.SqlDataReader reader = con.dbrun(v_sql);
            string UF_ID = "";
            if (reader.Read())
            {
                UF_ID = reader["UF_ID"].ToString();
            }
            dt.Rows.Add(drc, f,UF_ID);

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            Context.Response.Write(jsonstr);
    }
}
