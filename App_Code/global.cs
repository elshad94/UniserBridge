using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml;
using System.Windows.Forms.VisualStyles;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Text;
using System.Security.Cryptography;
using System.Net;
/// <summary>
/// Summary description for global
/// </summary>
public sealed class global
{

    public static string _webServiceKey = "be4fe9cdf6c107b7e8a74aeb165a19fe";
    public static int glb_language = 1;
    public static DataTable lcl_TableItems = null;
    public static DataTable lcl_TableGroups = null;

    public static DataTable lcl_TableRItems = null;
    public static DataTable lcl_TableRGroups = null;
    public static DevExpress.Web.ASPxGridView.GridViewDataDateColumn _AddColumnD(string _FieldName, string _Caption, string _format, bool _visible, int _width)
    {
        DevExpress.Web.ASPxGridView.GridViewDataDateColumn gc_ = new GridViewDataDateColumn();
        gc_ = new GridViewDataDateColumn();
        gc_.FieldName = _FieldName;
        gc_.Caption = _Caption;
        gc_.ReadOnly = true;
        gc_.Visible = _visible;

        gc_.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_.PropertiesDateEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_.ExportWidth = _width;
        if (_format != "")
        {
            gc_.PropertiesDateEdit.DisplayFormatString = _format;
            gc_.PropertiesDateEdit.EditFormatString = _format;
        }
        return gc_;
    }
    public static GridViewDataTextColumn _AddColumn(string _FieldName, string _Caption, string _format, bool _visible, int _width, bool ReadOnly = true)
    {
        GridViewDataTextColumn gc_ = new GridViewDataTextColumn();

        gc_.FieldName = _FieldName;
        gc_.Caption = _Caption;
        gc_.ReadOnly = ReadOnly;
        gc_.Visible = _visible;
        gc_.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
        gc_.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_.ExportWidth = _width;
        if (_format != "")
        {
            gc_.PropertiesTextEdit.DisplayFormatString = _format;
        }
        // grd_Browse1.Columns.Add(gc_);

        return gc_;
    }

    public static GridViewDataTextColumn _AddColumnNew(string _FieldName, string _Caption, string _format, bool _visible, int _width, bool ReadOnly)
    {
        GridViewDataTextColumn gc_ = new GridViewDataTextColumn();

        gc_.FieldName = _FieldName;
        gc_.Caption = _Caption;
        gc_.ReadOnly = ReadOnly;
        gc_.Visible = _visible;
        gc_.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
        gc_.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_.ExportWidth = _width;
        if (_format != "")
        {
            gc_.PropertiesTextEdit.DisplayFormatString = _format;
        }
        // grd_Browse1.Columns.Add(gc_);

        return gc_;
    }
    public static GridViewDataTextColumn _AddColumnL(string _FieldName, string _Caption, string _format, bool _visible, int _width, bool ReadOnly = true, bool Sort = false, int Sortindex = -1, DevExpress.Data.ColumnSortOrder x = DevExpress.Data.ColumnSortOrder.Ascending)
    {
        GridViewDataTextColumn gc_ = new GridViewDataTextColumn();

        gc_.FieldName = _FieldName;
        gc_.Caption = _Caption;
        gc_.ReadOnly = ReadOnly;
        gc_.Visible = _visible;
        gc_.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
        gc_.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_.ExportWidth = _width;
        if (Sortindex != -1)
        {
            gc_.SortIndex = Sortindex;
        }
        if (Sort==true)
        {
        gc_.SortOrder =x;
            }
        if (_format != "")
        {
            gc_.PropertiesTextEdit.DisplayFormatString = _format;
        }
        // grd_Browse1.Columns.Add(gc_);

        return gc_;
    }
  public static GridViewCommandColumn _AddColumnCommand(string _Caption, bool _visible, int _width, bool Delv,bool Editv,bool NewH )
    {
        GridViewCommandColumn gc_ = new GridViewCommandColumn();

         gc_.Caption = _Caption;
         gc_.ShowDeleteButton = Delv;
         gc_.ShowEditButton = Editv;
         gc_.ShowInCustomizationForm = _visible;
         gc_.ShowNewButtonInHeader = NewH;
         gc_.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
         gc_.ExportWidth = _width;
      
        // grd_Browse1.Columns.Add(gc_);

        return gc_;
    }
    public static GridViewBandColumn _AddColumnb3(string _FieldName1, string _FieldName2, string _FieldName3, string _Caption1, string _Caption2, string _Caption3, string _Caption, string _format, bool _visible, int _width)
    {

        GridViewBandColumn gc_ = new GridViewBandColumn();

        GridViewDataTextColumn gc_1 = new GridViewDataTextColumn();
        GridViewDataTextColumn gc_2 = new GridViewDataTextColumn();
        GridViewDataTextColumn gc_3 = new GridViewDataTextColumn();


        gc_1.FieldName = _FieldName1;
        gc_2.FieldName = _FieldName2;
        gc_3.FieldName = _FieldName3;


        gc_1.Caption = _Caption1;
        gc_2.Caption = _Caption2;
        gc_3.Caption = _Caption3;


        gc_.Columns.Add(gc_1);
        gc_.Columns.Add(gc_2);
        gc_.Columns.Add(gc_3);


        gc_.Caption = _Caption;
        gc_.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        //   gc_.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_.ExportWidth = _width;


        gc_1.Visible = _visible;
        gc_1.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_1.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_1.ExportWidth = _width;
        gc_2.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_2.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_2.ExportWidth = _width;


        gc_3.Visible = _visible;
        gc_3.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_3.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_3.ExportWidth = _width;



        if (_format != "")
        {
            gc_1.PropertiesTextEdit.DisplayFormatString = _format;
            gc_2.PropertiesTextEdit.DisplayFormatString = _format;
            gc_3.PropertiesTextEdit.DisplayFormatString = _format;

        }
        //  grd_Browse.Columns.Add(gc_);
        return gc_;
    }
    public static GridViewDataSpinEditColumn _AddColumnSpinEdit(string _FieldName, string _Caption, string _format, bool _visible, int _width, bool _readonly)
    {
        GridViewDataSpinEditColumn gc_ = new GridViewDataSpinEditColumn();
        gc_.FieldName = _FieldName;
        gc_.Caption = _Caption;
        gc_.ReadOnly = _readonly;
        //gc_.CellStyle.BackColor = System.Drawing.Color.DarkCyan;
        gc_.EditFormSettings.Visible = (_readonly == true ? DevExpress.Utils.DefaultBoolean.False : DevExpress.Utils.DefaultBoolean.True);
        gc_.Visible = _visible;
        gc_.Width = Unit.Percentage(_width);

        gc_.Settings.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;


        if (_format != "")
        {
            gc_.PropertiesEdit.DisplayFormatString = _format;
        }
        return gc_;

    }
    public static string Get_Gridcaption(string _page, int _lang, string _object, string _objtype)
    {
        conn con = new conn();
        string v_sql = @"SELECT LNG_ID,LNG_PAGE,LNG_OBJECT,LNG_LABEL,LNG_CAPTION" + _lang.ToString() + @" as LNG_CAPTION  ,LNG_OBJTYPE
        FROM TBL_LANGUAGE
        where LNG_PAGE='" + _page + @"' and ISNULL(LNG_STATUS,0)=0 AND LNG_OBJTYPE='" + _objtype + @"' and LNG_OBJECT='" + _object + @"'
        order by LNG_OBJECT";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(v_sql, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        string cpt = "";
        while (reader.Read())
        {
            cpt = reader["LNG_CAPTION"].ToString();
        }
        reader.Close();
        return cpt;
    }

    public static GridViewBandColumn _AddColumnb4(string _FieldName1, string _FieldName2, string _FieldName3, string _FieldName4, string _Caption1, string _Caption2, string _Caption3, string _Caption4, string _Caption, string _format, bool _visible, int _width)
    {

        GridViewBandColumn gc_ = new GridViewBandColumn();
        gc_.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;

        GridViewDataTextColumn gc_1 = new GridViewDataTextColumn();
        GridViewDataTextColumn gc_2 = new GridViewDataTextColumn();
        GridViewDataTextColumn gc_3 = new GridViewDataTextColumn();
        GridViewDataTextColumn gc_4 = new GridViewDataTextColumn();

        gc_1.FieldName = _FieldName1;
        gc_2.FieldName = _FieldName2;
        gc_3.FieldName = _FieldName3;
        gc_4.FieldName = _FieldName4;

        gc_1.Caption = _Caption1;
        gc_2.Caption = _Caption2;
        gc_3.Caption = _Caption3;
        gc_4.Caption = _Caption4;

        gc_.Columns.Add(gc_1);
        gc_.Columns.Add(gc_2);
        gc_.Columns.Add(gc_3);
        gc_.Columns.Add(gc_4);

        gc_.Caption = _Caption;
        gc_.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        //   gc_.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_.ExportWidth = _width;


        gc_1.Visible = _visible;
        gc_1.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_1.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_1.ExportWidth = _width;
        gc_2.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_2.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_2.ExportWidth = _width;


        gc_3.Visible = _visible;
        gc_3.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_3.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_3.ExportWidth = _width;
        gc_4.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_4.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_4.ExportWidth = _width;



        if (_format != "")
        {
            gc_1.PropertiesTextEdit.DisplayFormatString = _format;
            gc_2.PropertiesTextEdit.DisplayFormatString = _format;
            gc_3.PropertiesTextEdit.DisplayFormatString = _format;
            gc_4.PropertiesTextEdit.DisplayFormatString = _format;
        }
        //  grd_Browse.Columns.Add(gc_);
        return gc_;
    }
    public static GridViewBandColumn _AddColumnb2(string _FieldName1, string _FieldName2, string _Caption1, string _Caption2, string _Caption, string _format, bool _visible, int _width1, int _width2, int _width)
    {

        GridViewBandColumn gc_ = new GridViewBandColumn();
        gc_.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;

        GridViewDataTextColumn gc_1 = new GridViewDataTextColumn();
        GridViewDataTextColumn gc_2 = new GridViewDataTextColumn();

        gc_1.FieldName = _FieldName1;
        gc_2.FieldName = _FieldName2;

        gc_1.Caption = _Caption1;
        gc_2.Caption = _Caption2;

        gc_.Columns.Add(gc_1);
        gc_.Columns.Add(gc_2);


        gc_.Caption = _Caption;
        gc_.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        //   gc_.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_.ExportWidth = _width;


        gc_1.Visible = _visible;
        gc_1.Width = System.Web.UI.WebControls.Unit.Pixel(_width1);
        gc_1.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width1);
        gc_1.ExportWidth = _width1;
        gc_2.Width = System.Web.UI.WebControls.Unit.Pixel(_width2);
        gc_2.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width2);
        gc_2.ExportWidth = _width2;



        if (_format != "")
        {
            gc_1.PropertiesTextEdit.DisplayFormatString = _format;
            gc_2.PropertiesTextEdit.DisplayFormatString = _format;

        }
        //  grd_Browse.Columns.Add(gc_);
        return gc_;
    }


    public static GridViewBandColumn _AddColumnDb2(string _FieldName1, string _FieldName2, string _Caption1, string _Caption2, string _Caption, string _format, bool _visible, int _width)
    {

        GridViewBandColumn gc_ = new GridViewBandColumn();

        GridViewDataDateColumn gc_1 = new GridViewDataDateColumn();
        GridViewDataTextColumn gc_2 = new GridViewDataTextColumn();

        gc_1.FieldName = _FieldName1;
        gc_2.FieldName = _FieldName2;

        gc_1.Caption = _Caption1;
        gc_2.Caption = _Caption2;

        gc_.Columns.Add(gc_1);
        gc_.Columns.Add(gc_2);


        gc_.Caption = _Caption;
        gc_.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        //   gc_.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_.ExportWidth = _width;


        gc_1.Visible = _visible;
        gc_1.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_1.PropertiesDateEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_1.ExportWidth = _width;
        gc_2.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_2.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_2.ExportWidth = _width;



        if (_format != "")
        {
            gc_1.PropertiesDateEdit.DisplayFormatString = _format;
            gc_1.PropertiesDateEdit.EditFormatString = _format;
            // gc_2.PropertiesTextEdit.DisplayFormatString = _format;

        }
        //  grd_Browse.Columns.Add(gc_);
        return gc_;
    }
    public static float GetCurrency(DateTime date, string curr)
    {
        conn con = new conn();
        //string day = "";
        //string month = "";
        //string year = "";
        //day = date.Day.ToString("00");
        //month = date.Month.ToString("00");
        //year = date.Year.ToString();
        //string currdate = "";
        //currdate = day + "." + month + "." + year;
        //System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
        //doc.Load("https://cbar.az/currencies/" + currdate + ".xml");
        //System.Xml.XmlNodeList nodes = doc.SelectNodes("/ValCurs/ValType/Valute");
        //    foreach (XmlNode node in nodes)
        //{
        //    if (node.Attributes["Code"].InnerText.ToString() == curr)
        //    {
        //        rate = float.Parse(node["Value"].InnerText);
        //    }
        //}

        string query = "";
        float rate = 1;
        System.Data.SqlClient.SqlDataReader reader;

        try
        {           
            query = @"SELECT CURR_CODE,CR_CODE,CR_RATE FROM TBL_CURRRATE
                          INNER JOIN TBL_CURRENCY on CR_CODE=CURR_ID
                          WHERE (CR_DATE=CAST(GETDATE() as date) OR CR_DATE=CAST(DATEADD(day,-1,GETDATE()) as date)) and CURR_ID='" + curr + @"' Order by CR_DATE";
           
            reader = conn.fnc_dbrun(query);

            while (reader.Read())
            {
                rate = float.Parse(reader["CR_RATE"].ToString());
            }
        }
        catch (Exception ex)
        {
            query = @"SELECT CURR_CODE,CR_CODE,CR_RATE FROM TBL_CURRRATE
                          INNER JOIN TBL_CURRENCY on CR_CODE=CURR_ID
                          WHERE (CR_DATE=CAST(GETDATE() as date) OR CR_DATE=CAST(DATEADD(day,-1,GETDATE()) as date)) and CURR_CODE='" + curr + @"' Order by CR_DATE";

            reader = conn.fnc_dbrun(query);

            while (reader.Read())
            {
                rate = float.Parse(reader["CR_RATE"].ToString());
            }
        }       

        return rate;

    }
    public static GridViewDataHyperLinkColumn _AddColumnh(string jv, string _FieldName, string _Text, string _Caption, string _format, bool _visible, int _width)
    {

        GridViewDataHyperLinkColumn colItemName = new GridViewDataHyperLinkColumn();
        colItemName.ExportWidth = _width;
        colItemName.FieldName = _FieldName;
        colItemName.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        colItemName.Caption = _Caption;
        colItemName.PropertiesHyperLinkEdit.NavigateUrlFormatString = jv;
        colItemName.PropertiesHyperLinkEdit.TextFormatString = "{0}";
        colItemName.PropertiesHyperLinkEdit.TextField = _Text;
        return colItemName;
    }

    public static GridViewDataHyperLinkColumn _AddColumnh2(string jv, string _FieldName, string _Text, string _Caption, string _format, bool _visible, int _width)
    {

        GridViewDataHyperLinkColumn colItemName = new GridViewDataHyperLinkColumn();
        colItemName.ExportWidth = _width;
        colItemName.FieldName = _FieldName;
        colItemName.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        colItemName.Caption = _Caption;
        colItemName.PropertiesHyperLinkEdit.Target = "_blank";
        colItemName.PropertiesHyperLinkEdit.TextFormatString = "{0}";
        colItemName.PropertiesHyperLinkEdit.TextField = _Text;
        return colItemName;
    }

    public static GridViewDataHyperLinkColumn _AddColumnhLink(string jv, string _FieldName, string _Text, string _Caption, string _format, bool _visible, int _width)
    {

        GridViewDataHyperLinkColumn colItemName = new GridViewDataHyperLinkColumn();
        colItemName.ExportWidth = _width;
        colItemName.FieldName = _FieldName;
        colItemName.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        colItemName.Caption = _Caption;
        colItemName.PropertiesHyperLinkEdit.Target = "";
        colItemName.PropertiesHyperLinkEdit.TextFormatString = "{0}";
        colItemName.PropertiesHyperLinkEdit.TextField = _Text;
        return colItemName;
    }

    public static void CheckControls(System.Web.UI.Page x)
    {
        login lgn = new login();
        string v_pageName = x.Page.ToString().Substring(4, x.Page.ToString().Substring(4).Length - 5) + ".aspx";
        string v_ctrl_name = "";
        string v_ctrl_def = "";
        string v_ctrl_type = "";

        string v_sql = @"SELECT * FROM TBL_CONTROLS
                  INNER JOIN  TBL_CONTROLU ON CNT_ID=CNTU_CNT_ID AND CNTU_U_ID=" + lgn.Get_userid() + @"
        WHERE CNT_MNU='" + v_pageName + "'";
        System.Data.SqlClient.SqlDataReader v_reader = conn.fnc_dbrun(v_sql);
        while (v_reader.Read())
        {
            v_ctrl_name = v_reader["CNT_NAME"].ToString();
            v_ctrl_def = v_reader["CNT_DEFAULT"].ToString();
            foreach (Control c in x.Form.Controls)
            {
                foreach (Control childc in c.Controls)
                {
                    string a = childc.ID;
                    if (v_ctrl_name == childc.ID)
                    {
                        if (v_ctrl_def == "0")
                        {
                            childc.Visible = false;
                        }
                        else
                        {
                            childc.Visible = true;
                        }
                    }
                }
            }
        }
    }
    public static GridViewBandColumn _AddColumnb6(string _FieldName1, string _FieldName2, string _FieldName3, string _FieldName4, string _FieldName5, string _FieldName6, string _Caption1, string _Caption2, string _Caption3, string _Caption4, string _Caption5, string _Caption6, string _Caption, string _format, bool _visible, int _width)
    {

        GridViewBandColumn gc_ = new GridViewBandColumn();

        GridViewDataTextColumn gc_1 = new GridViewDataTextColumn();
        GridViewDataTextColumn gc_2 = new GridViewDataTextColumn();
        GridViewDataTextColumn gc_3 = new GridViewDataTextColumn();
        GridViewDataTextColumn gc_4 = new GridViewDataTextColumn();
        GridViewDataTextColumn gc_5 = new GridViewDataTextColumn();
        GridViewDataTextColumn gc_6 = new GridViewDataTextColumn();

        gc_1.FieldName = _FieldName1;
        gc_2.FieldName = _FieldName2;
        gc_3.FieldName = _FieldName3;
        gc_4.FieldName = _FieldName4;
        gc_5.FieldName = _FieldName5;
        gc_6.FieldName = _FieldName6;

        gc_1.Caption = _Caption1;
        gc_2.Caption = _Caption2;
        gc_3.Caption = _Caption3;
        gc_4.Caption = _Caption4;
        gc_5.Caption = _Caption5;
        gc_6.Caption = _Caption6;

        gc_.Columns.Add(gc_1);
        gc_.Columns.Add(gc_2);
        gc_.Columns.Add(gc_3);
        gc_.Columns.Add(gc_4);
        gc_.Columns.Add(gc_5);
        gc_.Columns.Add(gc_6);

        gc_.Caption = _Caption;
        gc_.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        //   gc_.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_.ExportWidth = _width;


        gc_1.Visible = _visible;
        gc_1.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_1.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_1.ExportWidth = _width;
        gc_2.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_2.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_2.ExportWidth = _width;


        gc_3.Visible = _visible;
        gc_3.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_3.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_3.ExportWidth = _width;
        gc_4.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_4.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_4.ExportWidth = _width;
        gc_5.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_5.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_5.ExportWidth = _width;
        gc_6.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_6.PropertiesTextEdit.Width = System.Web.UI.WebControls.Unit.Pixel(_width);
        gc_6.ExportWidth = _width;



        if (_format != "")
        {
            gc_1.PropertiesTextEdit.DisplayFormatString = _format;
            gc_2.PropertiesTextEdit.DisplayFormatString = _format;
            gc_3.PropertiesTextEdit.DisplayFormatString = _format;
            gc_4.PropertiesTextEdit.DisplayFormatString = _format;
            gc_5.PropertiesTextEdit.DisplayFormatString = _format;
            gc_6.PropertiesTextEdit.DisplayFormatString = _format;
        }
        //  grd_Browse.Columns.Add(gc_);
        return gc_;
    }
    public static GridViewDataComboBoxColumn _AddComboColumn(string _FieldName, string _Caption, bool _visible, int _width, bool ReadOnly = true)
    {
        GridViewDataComboBoxColumn comboColumn = new GridViewDataComboBoxColumn();
        comboColumn.Caption = _Caption;
        comboColumn.FieldName = _FieldName;
        comboColumn.PropertiesComboBox.TextField = "ID";
        comboColumn.PropertiesComboBox.ValueField = "TEXT";
        comboColumn.PropertiesComboBox.EnableIncrementalFiltering =true;
        comboColumn.PropertiesComboBox.IncrementalFilteringMode=DevExpress.Web.ASPxEditors.IncrementalFilteringMode.Contains ;

        comboColumn.PropertiesComboBox.EnableSynchronization=DevExpress.Utils.DefaultBoolean.True;
        comboColumn.Width = _width;
        comboColumn.Visible = _visible;
        comboColumn.ReadOnly = ReadOnly;
        return comboColumn;
    }
    public static string _getsessionparam(string pname)
    {
        try
        {
            return System.Web.HttpContext.Current.Session[pname].ToString();
        }
        catch
        {
            return "";
        }
    }
    public static void _setsessionparam(string pname, string pval)
    {
        try
        {
            System.Web.HttpContext.Current.Session[pname] = pval;
        }
        catch
        {

            System.Web.HttpContext.Current.Session.Add(pname, pval);
        }

    }
    public string ConvertHexStringToBase64(string hexString)
    {
        byte[] buffer = new byte[hexString.Length / 2];
        for (int i = 0; i < hexString.Length; i++)
        {
            buffer[i / 2] = Convert.ToByte(Convert.ToInt32(hexString.Substring(i, 2), 16));
            i += 1;
        }
        string res = sha256_hash(Convert.ToBase64String(buffer));
        return res;
    }

    public string sha256_hash(String value)
    {
        StringBuilder Sb = new StringBuilder();

        using (SHA256 hash = SHA256Managed.Create())
        {
            Encoding enc = Encoding.UTF8;
            Byte[] result = hash.ComputeHash(enc.GetBytes(value));

            foreach (Byte b in result)
                Sb.Append(b.ToString("x2"));
        }

        return Sb.ToString();
    }

    public string MD5Hash(string input)
    {
        StringBuilder hash = new StringBuilder();
        MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
        byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

        for (int i = 0; i < bytes.Length; i++)
        {
            hash.Append(bytes[i].ToString("x2"));
        }
        return hash.ToString();
    }

}