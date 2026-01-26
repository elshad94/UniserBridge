using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using DevExpress.XtraPrinting.Drawing;

using System.Web.UI.WebControls;
//using Image = System.Web.UI.WebControls.Image;
using System.IO;
using MessagingToolkit.QRCode.Codec;
using System.Drawing.Imaging;
using System.Text;
/// <summary>
/// Summary description for XtraReport1
/// </summary>
public class XRKontDetail : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRControlStyle xrControlStyle1;
    private DevExpress.Utils.ImageCollection ımageCollection1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private XRLabel lbl_trans_no;
    static string lng_ = "";
    public string prc_curr="";
    private BackgroundWorker backgroundWorker1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRLabel xrLabel1;
    private PageHeaderBand PageHeader;
    private PageFooterBand PageFooter;
    private XRLabel xrLabel2;
    private XRLabel lbl_appendix;
    string pay_type = "";
    public XRKontDetail(string invId, string lang, int firmtype)
    {
        InitializeComponent();

        conn con = new conn();
        lng_ = lang;

        if (lang == "1")
        {
            xrLabel1.Text = "Konteynerlər";

            xrTableCell2.Text = "Transport №";
            xrTableCell3.Text = "Dolu/Boş";
            xrTableCell4.Text = "Konteyner";
            xrTableCell5.Text = "Mənsubiyyət";
        }

        if (lang == "3")
        {
            xrLabel1.Text = "Контейнеры";
            xrTableCell1.Text = "№ п/п";
            xrTableCell2.Text = "Транспорт №";
            xrTableCell3.Text = "Полный/пустой";
            xrTableCell4.Text = "Контейнер";
            xrTableCell5.Text = "Принадлежность";
        }

        //DataSet ds = new DataSet();
        //DataTable dt = new DataTable(invId);
        //ds.Tables.Add(dt);

        System.Data.SqlClient.SqlDataReader v_reader = null;

        v_reader = con.dbrun(@"Select INV_NO from TBL_INVOICE where inv_id =" + invId);
        if (v_reader.Read())
        {
            if (lang == "1")
            {
                lbl_appendix.Text = "Bu sənəd " + v_reader["INV_NO"].ToString() + " nömrəli fakturanın əlavəsidir";
            }
            else if (lang == "2")
            {
                lbl_appendix.Text = "This document is an appendix to the invoice number " + v_reader["INV_NO"].ToString();
            }
            else
            {
                lbl_appendix.Text = "Данный документ является приложением к счету-фактуре № " + v_reader["INV_NO"].ToString();
            }
        }
        v_reader.Close();

        string sql = @"SELECT* FROM TBL_ASSIGN WHERE AS_BR_ID = " + firmtype;
        string llp_assign = "";
        string mmc_assign = "";
        string llp_logo = "";
        string mmc_logo = "";
        v_reader = conn.fnc_dbrun(sql);
        if (v_reader.Read())
        {
            llp_assign = v_reader["AS_LLP"].ToString();
            mmc_assign = v_reader["AS_MMC"].ToString();
            llp_logo = v_reader["AS_LLP_LOGO"].ToString();
            mmc_logo = v_reader["AS_MMC_LOGO"].ToString();
        }
        v_reader.Close();


        DevExpress.XtraReports.UI.XRTable tbl = new XRTable();
        //     tbl.Location = new System.Drawing.Point(358, 17);
        tbl.Size = new System.Drawing.Size(747, 25);
        tbl.Borders = (DevExpress.XtraPrinting.BorderSide)
            (((DevExpress.XtraPrinting.BorderSide.Left
             | DevExpress.XtraPrinting.BorderSide.Top)
             | DevExpress.XtraPrinting.BorderSide.Right)
             | DevExpress.XtraPrinting.BorderSide.Bottom)
             ;
        Detail.Controls.Add(tbl);

        int count = 0;

        v_reader = con.dbrun(@"Select SPEC_F.SC_VALUE"+ lang +" TRN_Fullempty, TRN_TYPE.SC_VALUE" + lang + @" TrType, trn_PREFIX+ cast(TRN_NO as nvarchar(8)) No,  
            CASE TRN_TRTYPE
				WHEN  1 THEN isnull(V_CATEGORY.VC_NAME" + lang + @",'-')
				WHEN  2 THEN  G_TYPE.RT_NAME" + lang + @"
			END +' / '+
            CASE TRN_TRTYPE 
				WHEN 1 THEN V_TYPE.VT_NAME" + lang + @"  
				WHEN 2 then SP_CONTYPE.SC_VALUE" + lang + @" 
			END Container, OWNERTYPE.SC_VALUE" + lang + @" Owner
            from TBL_TRANSPORTLIST
		    left join TBL_SPECODES OWNERTYPE on OWNERTYPE.SC_REFID=TRN_OWNER and OWNERTYPE.SC_TYPE='VGN_OWNERTYPE' and isnull(OWNERTYPE.SC_STATUS,0) <>-1
            left join TBL_SPECODES TRN_TYPE on TRN_TYPE.SC_REFID=TRN_TRTYPE and TRN_TYPE.SC_TYPE='UOM' and isnull(TRN_TYPE.SC_STATUS,0) <>-1
            left join TBL_RTCTYPE G_TYPE on G_TYPE.RT_ID=TRN_TRCAT and G_TYPE.RT_TYPE=2
            left join TBL_VAGONTYPE V_TYPE on V_TYPE.VT_ID=TRN_TYPE and isnull(V_TYPE.VT_STATUS,0) <>-1
            left join TBL_VAGONCATEGORY V_CATEGORY on V_CATEGORY.VC_ID=V_TYPE.VT_C_ID and isnull(V_CATEGORY.VC_STATUS,0) <>-1
	        left join TBL_SPECODES SP_CONTYPE ON SP_CONTYPE.SC_REFID = TRN_TYPE AND SP_CONTYPE.SC_TYPE = 'CONT_TYPE'
			LEFT JOIN TBL_SPECODES SPEC_F ON SPEC_F.SC_REFID =  TRN_FULLEMPTY  and SPEC_F.SC_TYPE = 'EXP_TYPE' AND ISNULL(SPEC_F.SC_STATUS,0) <> -1
			left join TBL_TRANSPORTPARK on TRN_PREFIX = TN_PREFNO AND TRN_NO = TN_NO
            WHERE ISNULL(TRN_STATUS,0) <> 0 AND TRN_TRTYPE = 2 AND TRN_ORDID = (Select INV_ORD_RECNO from TBL_INVOICE where inv_id=" + invId + @")");


        while (v_reader.Read())
        {
            count++;

            DevExpress.XtraReports.UI.XRTableRow row = new XRTableRow();

            for (int cellCtr = 1; cellCtr <= 5; cellCtr++)
            {
                // Create a new cell and add it to the row.
                DevExpress.XtraReports.UI.XRTableCell cell = new XRTableCell();
                if (cellCtr == 1)
                {
                    cell.Text = count.ToString();
                    cell.WidthF = float.Parse("37.25");
                }
                else if (cellCtr == 2)
                {
                    cell.Text = v_reader["No"].ToString();
                    cell.WidthF = float.Parse("234.25");
                }

                if (cellCtr == 3)
                {
                    cell.Text = v_reader["TRN_Fullempty"].ToString();
                    cell.WidthF = float.Parse("113.25");
                }
                if (cellCtr == 4)
                {
                    cell.Text = v_reader["Container"].ToString();
                    cell.WidthF = float.Parse("251.25");
                }
                if (cellCtr == 5)
                {
                    cell.Text = v_reader["Owner"].ToString();
                    cell.WidthF = float.Parse("112.25");
                }

                cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
                row.Cells.Add(cell);
            }


            // ..and add it to the table.
            tbl.Rows.Add(row);
        }

    }








    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            string resourceFileName = "XRKontDetail.resx";
            System.Resources.ResourceManager resources = global::Resources.XRKontDetail.ResourceManager;
            this.components = new System.ComponentModel.Container();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.ımageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_appendix = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ımageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.HeightF = 55.83333F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(1.000099F, 151.6666F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable2.SizeF = new System.Drawing.SizeF(746.9999F, 25F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.CanShrink = true;
            this.xrTableCell1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "No";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell1.Weight = 0.34186294444788373D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "Transport №";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell2.Weight = 2.1655173680314306D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "Full/Empty";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell3.Weight = 1.0398654596838663D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "Container";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell4.Weight = 2.3152421801000536D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "Owner";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell5.Weight = 1.0394465820949463D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.StylePriority.UseFont = false;
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrControlStyle1
            // 
            this.xrControlStyle1.Name = "xrControlStyle1";
            this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // ımageCollection1
            // 
            this.ımageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("ımageCollection1.ImageStream")));
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(506.2083F, 83.04163F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(231.7917F, 30.29167F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Containers";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lbl_appendix,
            this.xrLabel1,
            this.xrTable2});
            this.PageHeader.HeightF = 176.6666F;
            this.PageHeader.Name = "PageHeader";
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2});
            this.PageFooter.HeightF = 88.16666F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 15F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(178.0417F, 10F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 120F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(354.1667F, 68.83331F);
            this.xrLabel2.StylePriority.UseFont = false;
            // 
            // lbl_appendix
            // 
            this.lbl_appendix.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_appendix.LocationFloat = new DevExpress.Utils.PointFloat(1.000099F, 83.04163F);
            this.lbl_appendix.Name = "lbl_appendix";
            this.lbl_appendix.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 2, 0, 0, 100F);
            this.lbl_appendix.SizeF = new System.Drawing.SizeF(492.1667F, 30.29168F);
            this.lbl_appendix.StylePriority.UseFont = false;
            this.lbl_appendix.StylePriority.UsePadding = false;
            this.lbl_appendix.StylePriority.UseTextAlignment = false;
            this.lbl_appendix.Text = "Appendix";
            this.lbl_appendix.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // XRKontDetail
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter});
            this.Margins = new System.Drawing.Printing.Margins(53, 49, 0, 0);
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1});
            this.Version = "13.2";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.XtraReport1_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ımageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void XtraReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
       
       // SetTextWatermark((XtraReport)sender);
        SetPictureWatermark((XtraReport)sender);
    }

    public void SetTextWatermark(XtraReport report)
    {
        // Adjust text watermark settings.
        report.Watermark.Text = "CUSTOM WATERMARK TEXT";
        report.Watermark.TextDirection = DirectionMode.ForwardDiagonal;
        report.Watermark.Font = new Font(report.Watermark.Font.FontFamily, 40);
        report.Watermark.ForeColor = Color.DodgerBlue;
        report.Watermark.TextTransparency = 150;
        report.Watermark.ShowBehind = false;
        report.Watermark.PageRange = "1,3-5";
    }

    public void SetPictureWatermark(XtraReport report)
    {
        // Adjust image watermark settings.
        if (lng_=="1")
        {
       //     report.Watermark.Image = Bitmap.FromFile(@"C:\Projects\IMSART_CONT\images\blank_az.png");
       
        }
        else if (lng_=="2")
        {
        //    report.Watermark.Image = Bitmap.FromFile(@"C:\Projects\IMSART_CONT\images\blank_en.png");
        
        }
        else if (lng_ == "3")
        {
            //report.Watermark.Image = Bitmap.FromFile(@"C:\Projects\IMSART_CONT\images\blank_ru.png");
       }
        report.Watermark.ImageAlign = ContentAlignment.TopCenter;
        report.Watermark.ImageTiling = false;
        report.Watermark.ImageViewMode = ImageViewMode.Stretch;
        report.Watermark.ImageTransparency = 100;
        report.Watermark.ShowBehind = true;
      //  report.Watermark.PageRange = "2,4";
    }

    private void lbl_other_items__BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }



}
