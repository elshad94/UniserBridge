using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.Utils;
/// <summary>
/// Summary description for XtraReport1
/// </summary>
public class KontReport : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRLabel lbl_profoma_h;
    private XRPictureBox img_logo;
    private XRLabel lbl_proformadate_;
    private XRLabel lbl_proformano_;
    private XRControlStyle xrControlStyle1;
    private PageFooterBand PageFooter;
    private XRLabel lbl_date;
    private XRLabel txt_clientname;
    private XRLabel lbl_inv_no;
    private PageHeaderBand PageHeader;
    private XRTable xrTable1;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRLabel lbl_note;
    private XRLabel txt_iban;
    private XRLabel txt_swift;
    private XRLabel txt_bcode;
    private XRLabel txt_tin;
    private XRLabel lbl_iban;
    private XRLabel lbl_swift;
    private XRLabel lbl_bcode;
    private XRLabel lbl_tin;
    private XRLabel lbl_bank;
    private XRLabel txt_beneficiary;
    private XRLabel lbl_beneficiary;
    private DevExpress.Utils.ImageCollection ımageCollection1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private XRLabel lbl_grd;
    private XRLabel lbl_subtotal;
    private XRLabel lbl_edv;
    private XRLabel lbl_tax;
    private XRLabel lbl_grtotal_;
    private XRLabel lbl_total;
    private XRLabel lbl_trans_no;
    private XRLabel lbl_phone_;
    private XRLabel txt_phone;
    private XRLabel lbl_address_;
    private XRLabel txt_address;
    private XRLabel lbl_eaddress_;
    private XRLabel txt_tin_clc;
    private XRLabel lbl_tin_clc_;
    private XRLabel txt_eaddress;
    private XRLabel lbl_pay_terms_;
    private XRLabel lbl_price_word;
    private XRLabel lbl_to;
    private XRLabel txt_other_cond;
    private XRLabel txt_pay_terms;
    private XRLabel lbl_prc_date;
    private XRLabel txt_prc_date;
    static string lng_ = "";
    private XRLabel xrLabel3;
    private XRLabel txt_swift_;
    private XRLabel lbl_bank_det;
    private GroupFooterBand GroupFooter1;
    private XRLabel to_lab_az;
    private XRLabel bank_rek;
    private XRLabel lbl_other_items_;
    public string prc_curr="";
    private XRLabel lbl_valyuta;
    private XRLabel txt_valyuta;
    private XRLabel lbl_contractno_;
    private XRLabel lbl_ct_no;
    private XRPictureBox img_pecat;
    private XRPictureBox img_QRCODE;
    private XRLabel lbl_f_site_r;
    private XRLabel xrLabel4;
    private XRLabel lbl_f_address_r;
    private XRLabel lbl_f_phone_r;
    private XRLabel lbl_ca;
    private XRLabel txt_ca;
    private XRLabel lbl_swift_;
    string pay_type = "";
    public KontReport(string tableName, string lang, int invtype,int firmtype)
    {
        InitializeComponent();

        conn con = new conn();
        System.Data.SqlClient.SqlDataReader reader = null;
        lng_ = lang;
        reader = con.dbrun(@"Select INV_PTYPE, INV_FRM_RECNO,INV_TYPE  from dbo.TBL_INVOICE  where INV_ID=" + tableName);
       
        string lng = "";
        
        string llp_mmc = "";
        string ct = "";
        string frm_recno = "";
        string INV_TYPE = "";
        if (reader.Read())
        {
            INV_TYPE = reader["INV_TYPE"].ToString();

            pay_type =reader["INV_PTYPE"].ToString();
            if (reader["INV_FRM_RECNO"].ToString() == "2")
            {
                llp_mmc = "1";
                ct = "1";
            }
            else
            {
                llp_mmc = "2";
                ct = "2";
            }
        }

        txt_pay_terms.Visible = false;
      if (invtype == 3)
        {
            txt_prc_date.Visible = false;
            lbl_prc_date.Visible = false;
        }
      
        if (lang == "1")
        {
           
            //to_lab_az.Visible = true;
            
            lbl_tax.Visible = true;
            lbl_edv.Visible = true;
            lbl_grtotal_.Visible = true;
            lbl_grd.Visible = true;
            txt_beneficiary.Text = "\"Alliance Multimodal\" MMC";
            lbl_profoma_h.Text = "İNVOYS";
            if (INV_TYPE == "5")
            {
                lbl_profoma_h.Text = "KREDIT NOT";
            }
            lbl_address_.Text = "Ünvanı:";
            lbl_phone_.Text = "Telefon nömrəsi:";
            lbl_tin_clc_.Text = "VÖEN:";
            lbl_eaddress_.Text = "Elektron ünvanı:";
            lbl_bank_det.Visible = false;
            lbl_beneficiary.Text = "Beneficiary:";
            lbl_proformadate_.Text = "Tarix:";
            lbl_proformano_.Text = "İnvoysun nömrəsi:";
            //lbl_ord_no_.Text = "Sifarişin nömrəsi:";
            lbl_pay_terms_.Text = "Ödəniş şərtləri";
            lbl_other_items_.Text = "Digər şərtlər:";
            txt_other_cond.Text = "Hörmətli müştəri! Ödəniş sənədində INV nömrə və tarixi qeyd etməyinizi xahiş edirik.";
            lbl_to.Text = "Adı:";
            lbl_address_.Text = "Ünvan:";
            lbl_contractno_.Text = "Müqavilə nömrəsi:";
           // lbl_code_.Text = "Podkod:";
            lbl_pay_terms_.Text = "Ödəmə şərtləri";
            lbl_tin.Text = "VÖEN:";
            lbl_ca.Text = "H/h:";
            lbl_bcode.Text = "Müxbir Hesab:";
            lbl_valyuta.Text = "Valyuta:";
            lbl_swift.Text = "SWIFT:";
           
            lbl_iban.Text = "Kod";
            lbl_prc_date.Text = "Invoys qüvvədədir:";
            if(Convert.ToInt32(pay_type)==1)
            txt_pay_terms.Text = "1. Ödəniş Bank köçürmələri ilə.";
            else { txt_pay_terms.Text = "1. Əvvəlcədən köçürmə yolu ilə."; }
            lbl_subtotal.Text = "Cəmi:";
            lbl_tax.Text = "ƏDV:";
            lbl_grtotal_.Text = "Yekun:";
            xrTableCell7.Text = "Sıra";
            xrTableCell8.Text = "Xidmət adı";
            xrTableCell10.Text = "Miqdar";
            xrTableCell11.Text = "Qiymət";
            xrTableCell12.Text = "Məbləğ";
            lbl_swift_.Text = "VÖEN";
            bank_rek.Visible = true;
            lbl_note.Text = "1. Ödəniş Azərbaycan Respublikası Mərkəzi Bankının həmin gün üçün müəyyən etdiyi valyuta məzənnəsinə uyğun olaraq.";
        }
        if (lang == "2" )
        {
            //if (invtype == 1)
            //{
            //    lbl_profoma_h.Text = "PROFORMA";
            //}
            //else
            {
                lbl_profoma_h.Text = "INVOICE";

                if (INV_TYPE == "5")
                {
                    lbl_profoma_h.Text = "CREDIT NOTE";
                }
            }
            //lbl_note.Text = "NOTE: Payment should be made according to the exchange rate on that day determined by The Central Bank of the Republic of Azerbaijan.";


            this.lbl_subtotal.LocationFloat = new DevExpress.Utils.PointFloat(497.3234F, 0F);
            this.lbl_subtotal.SizeF = new System.Drawing.SizeF(113F, 23F);
            //this.lbl_price_word.LocationFloat = new DevExpress.Utils.PointFloat(497.3334F, 22.99995F);


            //this.lbl_price_word.SizeF = new System.Drawing.SizeF(249.67F, 23F);



        }
        if (lang == "3" )
        {
        
            lbl_profoma_h.Text = "ИНВОЙС";
            if (INV_TYPE == "5")
            {
                lbl_profoma_h.Text = "КРЕДИТНАЯ НОТА";
            }
            txt_pay_terms.Text = "1. Предоплата банковским переводом.";
            lbl_note.Text = "1. В соответствии с обменным курсом Центрального банка Азербайджанской Республики на этот день.";
            txt_other_cond.Text = "Уважаемый клиент! Пожалуйста, запишите номер и дату INV в вашем платежном поручении.:";
            lbl_other_items_.Text = "Другие условия:";
            lbl_proformadate_.Text = "Дата:";
            lbl_address_.Text = "Aдрес:";
            lbl_phone_.Text = "№ Телефона:";
            lbl_tin_clc_.Text = "VÖEN:";
            lbl_eaddress_.Text = "E-aдрес:";
            lbl_proformano_.Text = "№ Инвойса :";
            lbl_pay_terms_.Text = "Условия оплаты:";
            lbl_prc_date.Text = "Счет фактура действителен до :";
            //lbl_code_.Text = "Подкод:";
            lbl_to.Text = "Kому:";
            //lbl_ord_no_.Text = "№ Заказа:";
            xrLabel3.Text = "Банк";
            lbl_contractno_.Text = "№ Договора :";
            lbl_bank_det.Text = "Сведения о банке:";
            lbl_subtotal.Text = "Итого:";
            lbl_tax.Text = "НДС:";
            lbl_grtotal_.Text = "Конечный:";
            xrTableCell7.Text = "№ п/п";
            xrTableCell8.Text = "Описание услуг";
            xrTableCell10.Text = "Количество";
            xrTableCell11.Text = "Цена";
            xrTableCell12.Text = "ИТОГО";
            lbl_note.Text = "Примечание: Оплата должна быть произведена в соответствии с обменным курсом на тот день, установленный Центральным банком Азербайджанской Республики.";
            lbl_tin.Text = "ИНН:";
            lbl_bcode.Text = "Код:";
            lbl_ca.Text = "Корр.счёт:";
            lbl_swift.Text = "SWIFT:";
            lbl_iban.Text = "Номер счёта:";
            lbl_beneficiary.Text = "Банк-посредник:";
        }

        DataSet ds = new DataSet();
        DataTable dt = new DataTable(tableName);
        ds.Tables.Add(dt);

        string inv_no = "";
        string fadress = "";
        string ftelefon = "";
        string fvoen = "";
        string INV_BANK_ID = "";
        string clientcode = "";
        string cvoen = "";
        string CT_DATE = "";
        string CLC_RECNO = "";
        string note = "";

        string v_sql = @"Select
                                 INV_CT_NO NO1, ISNULL(INV_B_ID,0) INV_B_ID,BANK_SWIFT_2,BA_CORES_NO_ACCOUNT,BA_CORES_BANK,INV_TOTAL,BA_CURR_ID,CLC_NOTE,INV_FRM_RECNO,BC.BANK_VOEN BC_VOEN,B.BANK_VOEN BVOEN,CURR_CODE,BANK_ID,BANK_NAME,CLC_EMAIL
                                ,BANK_NAME1,BANK_NAME2,BA_NO,BA_IBAN,BA_CODE,BANK_CODE,BANK_SWIFT,BANK_LOC,ORD_PODCODE,ORD_FICHENO, INV_BANK_ID,INVL_ORD_RECNO, INV_NO,INV_DATE,FRM_ADRESS,CLC_PHONE
                                ,CLC_ADDRESS,FRM_TELEFON,FRM_VOEN,CLC_ALLNAME" + lng + @",CLC_EXPCODE,CLC_VOEN,CLC_RECNO,isnull(INV_NOTE,'') INV_NOTE
                        FROM dbo.TBL_INVOICE
                                        LEFT JOIN TBL_INVOICELINE ON  INVL_INV_ID=INV_ID 
                                        LEFT JOIN TBL_TRANSORDERS ON  ORD_RECNO =INV_ORD_RECNO        
                                        LEFT JOIN TBL_FIRMS on FRM_RECNO=INV_FRM_RECNO
                                        INNER JOIN TBL_BANKACC BC on BA_ID=INV_BANK_ID
                                        INNER JOIN TBL_BANK B ON BANK_ID=BA_BANK_ID
                                        INNER JOIN TBL_CURRENCY ON INV_CURR_ID=CURR_ID
                                        LEFT JOIN TBL_CLCARDS on INV_CLC_RECNO=CLC_RECNO
                                        --LEFT JOIN TBL_CONTRACT on CT_CLC_RENO=CLC_RECNO
                        WHERE INV_ID=" + tableName + @"
                        GROUP BY 
		                       BANK_SWIFT_2,BA_CORES_NO_ACCOUNT,BA_CORES_BANK, ORD_FICHENO,ISNULL(INV_B_ID,0),BANK_ID,INV_TOTAL,BA_CURR_ID,CLC_NOTE,INV_CURR_ID,CURR_ID,CURR_CODE,BANK_NAME,BANK_NAME1,BANK_NAME2,BA_NO
	                           ,BANK_CODE,BANK_SWIFT,BANK_LOC,INV_BANK_ID,ORD_PODCODE,INVL_ORD_RECNO,INV_NO,INV_DATE,FRM_ADRESS,FRM_TELEFON,FRM_VOEN,CLC_ALLNAME" + lng + @",CLC_EXPCODE,
	                           INV_CT_NO,CLC_RECNO,INV_NOTE,BC.BANK_VOEN ,B.BANK_VOEN,INV_FRM_RECNO,CLC_PHONE,CLC_EMAIL,CLC_ADDRESS,CLC_VOEN,BA_IBAN,BA_CODE";
        string bankcurr = "";
        string ordacctype = "";
        System.Data.SqlClient.SqlDataReader v_reader = conn.fnc_dbrun(v_sql);
        String urgent = "";
        string iban = "";
 
        string branch = "";
        if (v_reader.Read())
        {
            string ctno = v_reader["INV_B_ID"].ToString();
            lbl_ct_no.Text = v_reader["NO1"].ToString();
            //CT_DATE = v_reader["CT_DATE"].ToString();
            branch = v_reader["INV_B_ID"].ToString();
            //ordacctype = v_reader["ORD_ACTTYPE"].ToString();
            bankcurr = v_reader["BA_CURR_ID"].ToString();
            frm_recno = v_reader["INV_FRM_RECNO"].ToString();
            lbl_inv_no.Text = v_reader["INV_NO"].ToString();
            lbl_date.Text = DateTime.Parse(v_reader["INV_DATE"].ToString()).ToString("dd-MM-yyyy");
            fadress = v_reader["FRM_ADRESS"].ToString();
            ftelefon = v_reader["FRM_TELEFON"].ToString();
            fvoen = v_reader["FRM_VOEN"].ToString();
            inv_no = v_reader["INV_NO"].ToString();
            INV_BANK_ID = v_reader["INV_BANK_ID"].ToString();
            CLC_RECNO = v_reader["CLC_RECNO"].ToString();
            note = v_reader["INV_NOTE"].ToString();
            txt_clientname.Text = v_reader["CLC_ALLNAME" + lng + ""].ToString();
            clientcode = v_reader["CLC_EXPCODE"].ToString();
            txt_phone.Text = v_reader["CLC_PHONE"].ToString();
            txt_tin_clc.Text = v_reader["CLC_VOEN"].ToString();
            txt_eaddress.Text = v_reader["CLC_EMAIL"].ToString();
            txt_address.Text = v_reader["CLC_ADDRESS"].ToString();
            txt_valyuta.Text = v_reader["CURR_CODE"].ToString();
           // txt_ord_no.Text = v_reader["ORD_FICHENO"].ToString();
            int month = DateTime.Now.Month + 1;
            var year = DateTime.Now.Year;
            var date = DateTime.Now;
            if (month>12)
            {
                month = date.AddMonths(1).Month;
                year = date.AddYears(1).Year;
            }
           
            txt_prc_date.Text = "01." + month.ToString("00") + "." + year;
          
                    lbl_bank.Text = v_reader["BANK_NAME"].ToString() ;
                    txt_tin.Text = v_reader["BVOEN"].ToString();
                    txt_bcode.Text = v_reader["BA_CORES_NO_ACCOUNT"].ToString();
                    txt_ca.Text = v_reader["BA_IBAN"].ToString();
                    txt_swift.Text = v_reader["BANK_SWIFT"].ToString();
                    txt_iban.Text = v_reader["BA_CORES_BANK"].ToString();
            txt_swift_.Text = v_reader["BANK_SWIFT_2"].ToString();
            if (lang == "1")
            { txt_swift_.Text = v_reader["BC_VOEN"].ToString();
                txt_iban.Text = v_reader["BA_CODE"].ToString();
            }
            

         iban = v_reader["BA_IBAN"].ToString();
           
            prc_curr = v_reader["CURR_CODE"].ToString();
            string rtotal = v_reader["INV_TOTAL"].ToString();
            //MessagingToolkit.QRCode.Codec.QRCodeEncoder encoder = new MessagingToolkit.QRCode.Codec.QRCodeEncoder();
            //System.Drawing.Bitmap img = encoder.Encode(inv_no + " | " + lbl_date.Text + " | " + rtotal + "|" + iban + "|" + tableName);
            //using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            //{
            //    img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //    byte[] byteImage = ms.ToArray();
            //    Convert.ToBase64String(byteImage);
            //    ImageQr.Image = img;
            //}
        }
        v_reader.Close();

        //string sqlin = "";
        //if (invtype == 4) v_sql = @"select CT_NO,CONVERT(VARCHAR(10)
        //    ,CT_DATE, 103) CT_DATE from TBL_INVOICE inner join TBL_CONTRACT on INV_CLC_RECNO=CT_CLC_RENO where INV_ID='" + tableName + @"' and CT_FRM_ID=INV_FRM_RECNO and CT_STATUS not in (-1,99)";
        //else v_sql = @"select CT_NO ,CONVERT(VARCHAR(10)
        //    ,CT_DATE, 103) CT_DATE from TBL_INVOICE inner join TBL_CONTRACT on INV_CLC_RECNO=CT_CLC_RENO where INV_ID='" + tableName + @"' and CT_FRM_ID=INV_FRM_RECNO and CT_STATUS not in (-1,99)";
        //v_reader = con.dbrun(v_sql);
        //if (v_reader.Read())
        //{
        //    lbl_ct_no.Text = v_reader["CT_NO"].ToString() + "  " + v_reader["CT_DATE"].ToString();
        //}
        //v_reader.Close();


        string sql = @"SELECT* FROM TBL_ASSIGN WHERE AS_BR_ID = " + firmtype;
        string llp_assign = "";
        string mmc_assign = "";
        string llp_logo = "";
        string mmc_logo = "";
        //v_reader = conn.fnc_dbrun(sql);
        //if (v_reader.Read())
        //{
        //    llp_assign = v_reader["AS_STAMP"].ToString();
        //    mmc_assign = v_reader["AS_MMC"].ToString();
        //    llp_logo = v_reader["AS_LLP_LOGO"].ToString();
        //    mmc_logo = v_reader["AS_MMC_LOGO"].ToString();
        //    ////v_reader["BA_IBAN"].ToString();
        //}
        //v_reader.Close();
    
        //    img_logo.Image = Image.FromFile("C:\\Users\\Rashid\\Project (Web)\\Imsart_Cont\\Imsart_Cont\\images\\AdyKont.png");


        img_logo.Image = Image.FromFile(@"C:\Projects\Imsarts\Multimodal Main\images\Multimodal.png");
        img_pecat.Image = Image.FromFile(@"C:\Projects\Imsarts\Multimodal Main\images\imza_mohur.png");

        ////   lng = "_ENG";
        //// img_pecat.WidthF = 270F;
        //// img_pecat.HeightF = 270F;
        //// img_pecat.LocationFloat = new DevExpress.Utils.PointFloat(367.2083F, 56.00004F);


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

        string queryString = string.Format(@"Select INV_EDV,ORD_VGNTRNTYPE,
				case 
                when ORD_ACTTYPE=" + lang + @" then 'For '+bgpoint.PNT_NAME" + lang + @"+' - For '+t.PNT_NAME" + lang + @" 
                when ORD_ACTTYPE=2 then 'For '+f.PNT_NAME" + lang + @"+' - For '+edpoint.PNT_NAME" + lang + @"
                when ORD_ACTTYPE=4 then 'For '+bgpoint.PNT_NAME" + lang + @"+' - For '+edpoint.PNT_NAME" + lang + @" 
                when ORD_ACTTYPE=4 then 'For '+f.PNT_NAME" + lang + @"+' - For '+t.PNT_NAME" + lang + @"  end as ROUT,
				INVL_ITEMDESC1 ,isnull(INVL_QTY,0) INVL_QTY,ISNULL(INVL_EXPTYPEID, 0) INVL_EXPTYPEID,ISNULL(INVL_EXP_TYPE, 0) INVL_EXP_TYPE,ISNULL(INVL_AMOUNT, 0) ADY_EXPENSE,
                isnull(INVL_QTY,0)*ISNULL(INVL_AMOUNT, 0) INV_TOTAL1,INV_TOTAL ,INV_EDV
                from TBL_INVOICELINE
                inner join  TBL_INVOICE on INV_ID=INVL_INV_ID
                LEFT join TBL_SPECODES SPC on SC_REFID=INVL_EXPTYPEID and SPC.SC_TYPE='EXPENCETYPE'
                LEFT join TBL_SPECODES SPC_TYPE on SPC_TYPE.SC_REFID=INVL_EXP_TYPE and SPC_TYPE.SC_TYPE='EXP_TYPE'
                LEFT join TBL_TRANSORDERS on INVL_ORD_RECNO=ORD_RECNO
                left join TBL_POINTS bgpoint on  ORD_BEGPOINT=bgpoint.PNT_RECNO
                left join TBL_POINTS edpoint on  ORD_ENDPOINT=edpoint.PNT_RECNO
                left join TBL_POINTS f on  ORD_FPOINT=f.PNT_RECNO
                left join TBL_POINTS t on  ORD_TPOINT=t.PNT_RECNO
                where INV_ID={0}", tableName);

        int count = 0;
        v_reader = conn.fnc_dbrun(queryString);
        int rowCnt;
        // Current row count.
        int rowCtr;
        // Total number of cells per row (columns).
        int cellCtr;
        // Current cell counter
        int cellCnt;
        string col2 = "";
        string col4 = "";
        string col5 = "";

        rowCnt = int.Parse("2");
        cellCnt = int.Parse("5");
        double total = 0;
        double edv = 0;
        while (v_reader.Read())
        {
            count++;
            col4 = (double.Parse(v_reader["INV_TOTAL1"].ToString()) / double.Parse(v_reader["INVL_QTY"].ToString())).ToString();
            col5 = double.Parse(v_reader["INV_TOTAL1"].ToString()).ToString();

            if (v_reader["ORD_VGNTRNTYPE"].ToString() == "1")
            {
                if (v_reader["INVL_EXPTYPEID"].ToString() == "1")
                {
                    if (v_reader["INVL_EXP_TYPE"].ToString() == "1")
                    {
                        col2 = "MT";
                    }
                    else
                    {
                        if (lang == "1")
                        {

                            col2 = "Ədəd";
                        }
                        else
                        {
                            col2 = "Ea";
                        }
                    }

                }
                else
                {
                    if (lang == "1")
                    {

                        col2 = "Ədəd";
                    }
                    else
                    {
                        col2 = "Ea";
                    }
                }
            }
            else
            {
                if (lang == "1")
                {

                    col2 = "Ədəd";
                }
                else
                {
                    col2 = "Ea";
                }
            }
            DevExpress.XtraReports.UI.XRTableRow row = new XRTableRow();

            for (cellCtr = 1; cellCtr <= cellCnt; cellCtr++)
            {
                // Create a new cell and add it to the row.
                DevExpress.XtraReports.UI.XRTableCell cell = new XRTableCell();
                if (cellCtr == 1)
                {
                    cell.Text = count.ToString();
                    cell.WidthF = float.Parse("37");
                }
                else if (cellCtr == 2)
                {
                    cell.Text = v_reader["INVL_ITEMDESC1"].ToString();
                    cell.WidthF = float.Parse("330.21");
                }
                //if (cellCtr == 3)
                //{
                //    cell.Text = col2;
                //    cell.WidthF = float.Parse("131.13");
                //}
                if (cellCtr == 3)
                {
                    cell.Text = v_reader["INVL_QTY"].ToString();
                    cell.WidthF = float.Parse("131.13");
                }
                if (cellCtr == 4)
                {
                    cell.Text = v_reader["ADY_EXPENSE"].ToString().STRINGROUNDAS00();
                    cell.WidthF = float.Parse("112");


                }
                if (cellCtr == 5)
                {
                    cell.Text = double.Parse(v_reader["INV_TOTAL1"].ToString()).ROUNDAS00();
                    cell.WidthF = float.Parse("136.67");

                    }
                    cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
                row.Cells.Add(cell);
            }
            total = double.Parse(v_reader["INV_TOTAL"].ToString()).ROUND2();
              edv =   double.Parse(v_reader["INV_EDV"].ToString()).ROUND2(); 
            lbl_edv.Text = edv.ROUNDAS00();
            // ..and add it to the table.
            tbl.Rows.Add(row);
        }
         // lbl_total.Text = total.ROUND2().ToString();
        lbl_total.Text = total.ROUNDAS00();
        //lbl_grd.Text = lbl_currency.Text + " " + (double.Parse(total.ToString()) + double.Parse(total.ToString()) * double.Parse(edv) / 100);
           lbl_grd.Text = (total + edv).ROUNDAS00();
        double ttotal = Convert.ToDouble(lbl_grd.Text);
        string[] lines = ttotal.ROUNDAS00().Split('.');
        int b = Convert.ToInt32(lines[0].ToString()) ;
        string a = "00";
        try
        {
            a = lines[1].ToString();
        }
        catch (Exception)
        {
            a = "00";
        }
        string text1 = b + "=" + a + "";
        string text2 = "";
        string curr = "";
        if (prc_curr == "USD")
        {
            if (lang == "1")
            {
                curr = " ABŞ dolları , " + a + "  sent";
            }
            else
            {
                curr = " USD , " + a + "  US Cents";
            }
            }
            else
            {
            if (lang == "1")
            {
                curr = " AZN , " + a + "  qəpik";
            }
            else
            {
                curr = " USD , " + a + " cent";
            }
        }
        if (lang=="1")
        {
            lbl_price_word.Text = NumberToWords_AZ(b ).SentenceCase() + curr ;
        }
        else
        {
            lbl_price_word.Text = NumberToWords_ENG(b).SentenceCase() + curr ;
        }

     // lbl_text_amount.Text = text2;
        XRLabel label = new XRLabel();
        label.Width = 500;
        label.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Bold);

    }
    public static string NumberToWords_AZ(int number)
    {
        if (number == 0)
            return "sıfır";

        if (number < 0)
            return "mənfi " + NumberToWords_AZ(Math.Abs(number));

        string words = "";

        if ((number / 1000000) > 0)
        {
            words += NumberToWords_AZ(number / 1000000) + " miliyon ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWords_AZ(number / 1000) + " min ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords_AZ(number / 100) + " yüz ";
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

    public static string NumberToWords_ENG(int number)
    {
        if (number == 0)
            return "zero";

        if (number < 0)
            return "minus " + NumberToWords_ENG(Math.Abs(number));

        string words = "";

        if ((number / 1000000) > 0)
        {
            words += NumberToWords_ENG(number / 1000000) + " million ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWords_ENG(number / 1000) + " thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords_ENG(number / 100) + " hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "")
                words += ", ";

            var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
        }

        return words;
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
            string resourceFileName = "KontReport.resx";
            System.Resources.ResourceManager resources = global::Resources.KontReport.ResourceManager;
            this.components = new System.ComponentModel.Container();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.lbl_subtotal = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_total = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_price_word = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.img_logo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.lbl_profoma_h = new DevExpress.XtraReports.UI.XRLabel();
            this.to_lab_az = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.lbl_ct_no = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_contractno_ = new DevExpress.XtraReports.UI.XRLabel();
            this.txt_valyuta = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_valyuta = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_to = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_eaddress_ = new DevExpress.XtraReports.UI.XRLabel();
            this.txt_eaddress = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_phone_ = new DevExpress.XtraReports.UI.XRLabel();
            this.txt_phone = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_address_ = new DevExpress.XtraReports.UI.XRLabel();
            this.txt_address = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_date = new DevExpress.XtraReports.UI.XRLabel();
            this.txt_clientname = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_inv_no = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_proformadate_ = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_proformano_ = new DevExpress.XtraReports.UI.XRLabel();
            this.txt_tin_clc = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_tin_clc_ = new DevExpress.XtraReports.UI.XRLabel();
            this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.lbl_swift_ = new DevExpress.XtraReports.UI.XRLabel();
            this.txt_ca = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_ca = new DevExpress.XtraReports.UI.XRLabel();
            this.img_QRCODE = new DevExpress.XtraReports.UI.XRPictureBox();
            this.lbl_f_site_r = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_f_address_r = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_f_phone_r = new DevExpress.XtraReports.UI.XRLabel();
            this.img_pecat = new DevExpress.XtraReports.UI.XRPictureBox();
            this.lbl_other_items_ = new DevExpress.XtraReports.UI.XRLabel();
            this.txt_iban = new DevExpress.XtraReports.UI.XRLabel();
            this.txt_swift_ = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_bcode = new DevExpress.XtraReports.UI.XRLabel();
            this.txt_bcode = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_iban = new DevExpress.XtraReports.UI.XRLabel();
            this.txt_swift = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_swift = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_bank = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_bank_det = new DevExpress.XtraReports.UI.XRLabel();
            this.txt_tin = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_tin = new DevExpress.XtraReports.UI.XRLabel();
            this.txt_beneficiary = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_beneficiary = new DevExpress.XtraReports.UI.XRLabel();
            this.bank_rek = new DevExpress.XtraReports.UI.XRLabel();
            this.txt_other_cond = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_note = new DevExpress.XtraReports.UI.XRLabel();
            this.txt_pay_terms = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_pay_terms_ = new DevExpress.XtraReports.UI.XRLabel();
            this.txt_prc_date = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_prc_date = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_grd = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_edv = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_tax = new DevExpress.XtraReports.UI.XRLabel();
            this.lbl_grtotal_ = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ımageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ımageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lbl_subtotal
            // 
            this.lbl_subtotal.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.lbl_subtotal.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_subtotal.LocationFloat = new DevExpress.Utils.PointFloat(498.3334F, 0F);
            this.lbl_subtotal.Name = "lbl_subtotal";
            this.lbl_subtotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_subtotal.SizeF = new System.Drawing.SizeF(111.9999F, 19.99998F);
            this.lbl_subtotal.StylePriority.UseBorders = false;
            this.lbl_subtotal.StylePriority.UseFont = false;
            this.lbl_subtotal.StylePriority.UseTextAlignment = false;
            this.lbl_subtotal.Text = "Total (USD):";
            this.lbl_subtotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lbl_total
            // 
            this.lbl_total.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.lbl_total.CanGrow = false;
            this.lbl_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_total.LocationFloat = new DevExpress.Utils.PointFloat(610.3335F, 0F);
            this.lbl_total.Name = "lbl_total";
            this.lbl_total.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_total.SizeF = new System.Drawing.SizeF(136.6617F, 20F);
            this.lbl_total.StylePriority.UseBorders = false;
            this.lbl_total.StylePriority.UseFont = false;
            this.lbl_total.StylePriority.UseTextAlignment = false;
            this.lbl_total.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lbl_price_word
            // 
            this.lbl_price_word.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lbl_price_word.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_price_word.LocationFloat = new DevExpress.Utils.PointFloat(498.3334F, 60.00001F);
            this.lbl_price_word.Name = "lbl_price_word";
            this.lbl_price_word.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_price_word.SizeF = new System.Drawing.SizeF(248.6618F, 20F);
            this.lbl_price_word.StylePriority.UseBorders = false;
            this.lbl_price_word.StylePriority.UseFont = false;
            this.lbl_price_word.StylePriority.UseTextAlignment = false;
            this.lbl_price_word.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // img_logo
            // 
            this.img_logo.LocationFloat = new DevExpress.Utils.PointFloat(12.79015F, 27.29162F);
            this.img_logo.Name = "img_logo";
            this.img_logo.SizeF = new System.Drawing.SizeF(232.6245F, 86.25004F);
            this.img_logo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // lbl_profoma_h
            // 
            this.lbl_profoma_h.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
            this.lbl_profoma_h.LocationFloat = new DevExpress.Utils.PointFloat(498.3335F, 83.24999F);
            this.lbl_profoma_h.Name = "lbl_profoma_h";
            this.lbl_profoma_h.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_profoma_h.SizeF = new System.Drawing.SizeF(231.7917F, 30.29167F);
            this.lbl_profoma_h.StylePriority.UseFont = false;
            this.lbl_profoma_h.StylePriority.UseTextAlignment = false;
            this.lbl_profoma_h.Text = "INVOICE";
            this.lbl_profoma_h.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // to_lab_az
            // 
            this.to_lab_az.Font = new System.Drawing.Font("Calibri", 12F);
            this.to_lab_az.LocationFloat = new DevExpress.Utils.PointFloat(11.29613F, 131.0417F);
            this.to_lab_az.Name = "to_lab_az";
            this.to_lab_az.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.to_lab_az.SizeF = new System.Drawing.SizeF(48.04149F, 17.79167F);
            this.to_lab_az.StylePriority.UseFont = false;
            this.to_lab_az.StylePriority.UseTextAlignment = false;
            this.to_lab_az.Text = "Kimə:";
            this.to_lab_az.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.to_lab_az.Visible = false;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.StylePriority.UseFont = false;
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lbl_ct_no
            // 
            this.lbl_ct_no.Font = new System.Drawing.Font("Calibri", 12F);
            this.lbl_ct_no.LocationFloat = new DevExpress.Utils.PointFloat(542.7993F, 185.4166F);
            this.lbl_ct_no.Name = "lbl_ct_no";
            this.lbl_ct_no.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_ct_no.SizeF = new System.Drawing.SizeF(199.4996F, 17.79167F);
            this.lbl_ct_no.StylePriority.UseFont = false;
            this.lbl_ct_no.StylePriority.UseTextAlignment = false;
            this.lbl_ct_no.Text = " ";
            this.lbl_ct_no.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl_contractno_
            // 
            this.lbl_contractno_.Font = new System.Drawing.Font("Calibri", 12F);
            this.lbl_contractno_.LocationFloat = new DevExpress.Utils.PointFloat(416.0053F, 185.4167F);
            this.lbl_contractno_.Name = "lbl_contractno_";
            this.lbl_contractno_.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_contractno_.SizeF = new System.Drawing.SizeF(126.7941F, 17.79167F);
            this.lbl_contractno_.StylePriority.UseFont = false;
            this.lbl_contractno_.StylePriority.UseTextAlignment = false;
            this.lbl_contractno_.Text = "Contract number:";
            this.lbl_contractno_.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // txt_valyuta
            // 
            this.txt_valyuta.Font = new System.Drawing.Font("Calibri", 12F);
            this.txt_valyuta.LocationFloat = new DevExpress.Utils.PointFloat(133.8022F, 238.7122F);
            this.txt_valyuta.Name = "txt_valyuta";
            this.txt_valyuta.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txt_valyuta.SizeF = new System.Drawing.SizeF(267.8301F, 17.79167F);
            this.txt_valyuta.StylePriority.UseFont = false;
            // 
            // lbl_valyuta
            // 
            this.lbl_valyuta.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_valyuta.LocationFloat = new DevExpress.Utils.PointFloat(9.382483F, 238.7122F);
            this.lbl_valyuta.Name = "lbl_valyuta";
            this.lbl_valyuta.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_valyuta.SizeF = new System.Drawing.SizeF(124.4197F, 17.79167F);
            this.lbl_valyuta.StylePriority.UseFont = false;
            this.lbl_valyuta.Text = "Currency:";
            // 
            // lbl_to
            // 
            this.lbl_to.Font = new System.Drawing.Font("Calibri", 12F);
            this.lbl_to.LocationFloat = new DevExpress.Utils.PointFloat(9.213465F, 148.8334F);
            this.lbl_to.Name = "lbl_to";
            this.lbl_to.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_to.SizeF = new System.Drawing.SizeF(48.54703F, 17.79167F);
            this.lbl_to.StylePriority.UseFont = false;
            this.lbl_to.StylePriority.UsePadding = false;
            this.lbl_to.StylePriority.UseTextAlignment = false;
            this.lbl_to.Text = "To:";
            this.lbl_to.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lbl_eaddress_
            // 
            this.lbl_eaddress_.Font = new System.Drawing.Font("Calibri", 12F);
            this.lbl_eaddress_.LocationFloat = new DevExpress.Utils.PointFloat(9.213478F, 220F);
            this.lbl_eaddress_.Name = "lbl_eaddress_";
            this.lbl_eaddress_.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_eaddress_.SizeF = new System.Drawing.SizeF(124.5887F, 17.79167F);
            this.lbl_eaddress_.StylePriority.UseFont = false;
            this.lbl_eaddress_.StylePriority.UseTextAlignment = false;
            this.lbl_eaddress_.Text = "Email address:";
            this.lbl_eaddress_.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // txt_eaddress
            // 
            this.txt_eaddress.Font = new System.Drawing.Font("Calibri", 12F);
            this.txt_eaddress.LocationFloat = new DevExpress.Utils.PointFloat(133.8022F, 220F);
            this.txt_eaddress.Name = "txt_eaddress";
            this.txt_eaddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txt_eaddress.SizeF = new System.Drawing.SizeF(267.83F, 17.79167F);
            this.txt_eaddress.StylePriority.UseFont = false;
            // 
            // lbl_phone_
            // 
            this.lbl_phone_.Font = new System.Drawing.Font("Calibri", 12F);
            this.lbl_phone_.LocationFloat = new DevExpress.Utils.PointFloat(9.382483F, 202.2084F);
            this.lbl_phone_.Name = "lbl_phone_";
            this.lbl_phone_.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_phone_.SizeF = new System.Drawing.SizeF(124.4197F, 17.79167F);
            this.lbl_phone_.StylePriority.UseFont = false;
            this.lbl_phone_.StylePriority.UseTextAlignment = false;
            this.lbl_phone_.Text = "Phone number:";
            this.lbl_phone_.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // txt_phone
            // 
            this.txt_phone.Font = new System.Drawing.Font("Calibri", 12F);
            this.txt_phone.LocationFloat = new DevExpress.Utils.PointFloat(133.8022F, 202.2084F);
            this.txt_phone.Name = "txt_phone";
            this.txt_phone.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txt_phone.SizeF = new System.Drawing.SizeF(267.83F, 17.79167F);
            this.txt_phone.StylePriority.UseFont = false;
            this.txt_phone.StylePriority.UseTextAlignment = false;
            this.txt_phone.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl_address_
            // 
            this.lbl_address_.Font = new System.Drawing.Font("Calibri", 12F);
            this.lbl_address_.LocationFloat = new DevExpress.Utils.PointFloat(9.213617F, 184.4167F);
            this.lbl_address_.Name = "lbl_address_";
            this.lbl_address_.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_address_.SizeF = new System.Drawing.SizeF(68.33855F, 17.79166F);
            this.lbl_address_.StylePriority.UseFont = false;
            this.lbl_address_.StylePriority.UseTextAlignment = false;
            this.lbl_address_.Text = "Address:";
            this.lbl_address_.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // txt_address
            // 
            this.txt_address.Font = new System.Drawing.Font("Calibri", 12F);
            this.txt_address.LocationFloat = new DevExpress.Utils.PointFloat(77.55216F, 184.4166F);
            this.txt_address.Name = "txt_address";
            this.txt_address.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txt_address.SizeF = new System.Drawing.SizeF(324.0801F, 17.79167F);
            this.txt_address.StylePriority.UseFont = false;
            this.txt_address.StylePriority.UseTextAlignment = false;
            this.txt_address.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl_date
            // 
            this.lbl_date.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.lbl_date.LocationFloat = new DevExpress.Utils.PointFloat(542.7993F, 149.8333F);
            this.lbl_date.Name = "lbl_date";
            this.lbl_date.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_date.SizeF = new System.Drawing.SizeF(199.4996F, 17.79167F);
            this.lbl_date.StylePriority.UseFont = false;
            this.lbl_date.StylePriority.UseTextAlignment = false;
            this.lbl_date.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // txt_clientname
            // 
            this.txt_clientname.Font = new System.Drawing.Font("Calibri", 12F);
            this.txt_clientname.LocationFloat = new DevExpress.Utils.PointFloat(57.76056F, 148.8334F);
            this.txt_clientname.Name = "txt_clientname";
            this.txt_clientname.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txt_clientname.SizeF = new System.Drawing.SizeF(343.8717F, 17.79167F);
            this.txt_clientname.StylePriority.UseFont = false;
            this.txt_clientname.StylePriority.UseTextAlignment = false;
            this.txt_clientname.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl_inv_no
            // 
            this.lbl_inv_no.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.lbl_inv_no.LocationFloat = new DevExpress.Utils.PointFloat(542.7993F, 167.625F);
            this.lbl_inv_no.Name = "lbl_inv_no";
            this.lbl_inv_no.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_inv_no.SizeF = new System.Drawing.SizeF(199.4996F, 17.79167F);
            this.lbl_inv_no.StylePriority.UseFont = false;
            this.lbl_inv_no.StylePriority.UseTextAlignment = false;
            this.lbl_inv_no.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // lbl_proformadate_
            // 
            this.lbl_proformadate_.Font = new System.Drawing.Font("Calibri", 12F);
            this.lbl_proformadate_.LocationFloat = new DevExpress.Utils.PointFloat(416.0053F, 149.8334F);
            this.lbl_proformadate_.Name = "lbl_proformadate_";
            this.lbl_proformadate_.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_proformadate_.SizeF = new System.Drawing.SizeF(126.7941F, 17.79167F);
            this.lbl_proformadate_.StylePriority.UseFont = false;
            this.lbl_proformadate_.StylePriority.UseTextAlignment = false;
            this.lbl_proformadate_.Text = "Date:";
            this.lbl_proformadate_.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // lbl_proformano_
            // 
            this.lbl_proformano_.Font = new System.Drawing.Font("Calibri", 12F);
            this.lbl_proformano_.LocationFloat = new DevExpress.Utils.PointFloat(416.0053F, 167.625F);
            this.lbl_proformano_.Name = "lbl_proformano_";
            this.lbl_proformano_.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_proformano_.SizeF = new System.Drawing.SizeF(126.7941F, 17.79167F);
            this.lbl_proformano_.StylePriority.UseFont = false;
            this.lbl_proformano_.StylePriority.UseTextAlignment = false;
            this.lbl_proformano_.Text = "INV number:";
            this.lbl_proformano_.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // txt_tin_clc
            // 
            this.txt_tin_clc.Font = new System.Drawing.Font("Calibri", 12F);
            this.txt_tin_clc.LocationFloat = new DevExpress.Utils.PointFloat(77.55216F, 166.625F);
            this.txt_tin_clc.Name = "txt_tin_clc";
            this.txt_tin_clc.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txt_tin_clc.SizeF = new System.Drawing.SizeF(324.0801F, 17.79167F);
            this.txt_tin_clc.StylePriority.UseFont = false;
            this.txt_tin_clc.StylePriority.UseTextAlignment = false;
            this.txt_tin_clc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl_tin_clc_
            // 
            this.lbl_tin_clc_.Font = new System.Drawing.Font("Calibri", 12F);
            this.lbl_tin_clc_.LocationFloat = new DevExpress.Utils.PointFloat(9.213465F, 166.6251F);
            this.lbl_tin_clc_.Name = "lbl_tin_clc_";
            this.lbl_tin_clc_.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_tin_clc_.SizeF = new System.Drawing.SizeF(68.3387F, 17.79167F);
            this.lbl_tin_clc_.StylePriority.UseFont = false;
            this.lbl_tin_clc_.StylePriority.UseTextAlignment = false;
            this.lbl_tin_clc_.Text = "TIN:";
            this.lbl_tin_clc_.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrControlStyle1
            // 
            this.xrControlStyle1.Name = "xrControlStyle1";
            this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lbl_swift_,
            this.txt_ca,
            this.lbl_ca,
            this.img_QRCODE,
            this.lbl_f_site_r,
            this.xrLabel4,
            this.lbl_f_address_r,
            this.lbl_f_phone_r,
            this.img_pecat,
            this.lbl_other_items_,
            this.txt_iban,
            this.txt_swift_,
            this.lbl_bcode,
            this.txt_bcode,
            this.lbl_iban,
            this.txt_swift,
            this.lbl_swift,
            this.lbl_bank,
            this.xrLabel3,
            this.lbl_bank_det,
            this.txt_tin,
            this.lbl_tin,
            this.txt_beneficiary,
            this.lbl_beneficiary,
            this.bank_rek,
            this.txt_other_cond,
            this.lbl_note,
            this.txt_pay_terms,
            this.lbl_pay_terms_,
            this.txt_prc_date,
            this.lbl_prc_date});
            this.PageFooter.HeightF = 550F;
            this.PageFooter.Name = "PageFooter";
            // 
            // lbl_swift_
            // 
            this.lbl_swift_.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl_swift_.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbl_swift_.LocationFloat = new DevExpress.Utils.PointFloat(9.382477F, 386.6248F);
            this.lbl_swift_.Name = "lbl_swift_";
            this.lbl_swift_.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_swift_.SizeF = new System.Drawing.SizeF(217.418F, 23F);
            this.lbl_swift_.StylePriority.UseBorders = false;
            this.lbl_swift_.StylePriority.UseFont = false;
            this.lbl_swift_.StylePriority.UseTextAlignment = false;
            this.lbl_swift_.Text = "TIN No:";
            this.lbl_swift_.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txt_ca
            // 
            this.txt_ca.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txt_ca.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.txt_ca.LocationFloat = new DevExpress.Utils.PointFloat(227.2466F, 363.625F);
            this.txt_ca.Name = "txt_ca";
            this.txt_ca.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txt_ca.SizeF = new System.Drawing.SizeF(489.1311F, 23F);
            this.txt_ca.StylePriority.UseBorders = false;
            this.txt_ca.StylePriority.UseFont = false;
            this.txt_ca.StylePriority.UseTextAlignment = false;
            this.txt_ca.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl_ca
            // 
            this.lbl_ca.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl_ca.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbl_ca.LocationFloat = new DevExpress.Utils.PointFloat(9.828568F, 363.625F);
            this.lbl_ca.Name = "lbl_ca";
            this.lbl_ca.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_ca.SizeF = new System.Drawing.SizeF(217.418F, 23F);
            this.lbl_ca.StylePriority.UseBorders = false;
            this.lbl_ca.StylePriority.UseFont = false;
            this.lbl_ca.StylePriority.UseTextAlignment = false;
            this.lbl_ca.Text = "Acc.No.Of Beneficiary Customer:";
            this.lbl_ca.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // img_QRCODE
            // 
            this.img_QRCODE.LocationFloat = new DevExpress.Utils.PointFloat(595.4576F, 444.1667F);
            this.img_QRCODE.Name = "img_QRCODE";
            this.img_QRCODE.SizeF = new System.Drawing.SizeF(135.37F, 90F);
            this.img_QRCODE.Visible = false;
            // 
            // lbl_f_site_r
            // 
            this.lbl_f_site_r.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.lbl_f_site_r.LocationFloat = new DevExpress.Utils.PointFloat(232.3343F, 513.887F);
            this.lbl_f_site_r.Name = "lbl_f_site_r";
            this.lbl_f_site_r.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_f_site_r.SizeF = new System.Drawing.SizeF(265.9991F, 15F);
            this.lbl_f_site_r.StylePriority.UseFont = false;
            this.lbl_f_site_r.StylePriority.UseTextAlignment = false;
            this.lbl_f_site_r.Text = "www.alliancemultimodal.com | info@alliancemultimodal.com";
            this.lbl_f_site_r.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(272.6237F, 451.887F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(182.7526F, 17.00003F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "ALLIANCE MULTIMODAL";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lbl_f_address_r
            // 
            this.lbl_f_address_r.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.lbl_f_address_r.LocationFloat = new DevExpress.Utils.PointFloat(232.3343F, 468.8871F);
            this.lbl_f_address_r.Multiline = true;
            this.lbl_f_address_r.Name = "lbl_f_address_r";
            this.lbl_f_address_r.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_f_address_r.SizeF = new System.Drawing.SizeF(265.9991F, 30F);
            this.lbl_f_address_r.StylePriority.UseFont = false;
            this.lbl_f_address_r.StylePriority.UseTextAlignment = false;
            this.lbl_f_address_r.Text = "Port Baku Towers | 153, Nefchiler Avenue\nAz1010, Baku, Azerbaijan";
            this.lbl_f_address_r.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lbl_f_phone_r
            // 
            this.lbl_f_phone_r.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.lbl_f_phone_r.LocationFloat = new DevExpress.Utils.PointFloat(232.3343F, 498.8869F);
            this.lbl_f_phone_r.Multiline = true;
            this.lbl_f_phone_r.Name = "lbl_f_phone_r";
            this.lbl_f_phone_r.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_f_phone_r.SizeF = new System.Drawing.SizeF(265.9952F, 15F);
            this.lbl_f_phone_r.StylePriority.UseFont = false;
            this.lbl_f_phone_r.StylePriority.UseTextAlignment = false;
            this.lbl_f_phone_r.Text = "+994 12-599-11-39/40";
            this.lbl_f_phone_r.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // img_pecat
            // 
            this.img_pecat.LocationFloat = new DevExpress.Utils.PointFloat(525.1251F, 225.6251F);
            this.img_pecat.Name = "img_pecat";
            this.img_pecat.SizeF = new System.Drawing.SizeF(205F, 183.9997F);
            this.img_pecat.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // lbl_other_items_
            // 
            this.lbl_other_items_.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbl_other_items_.LocationFloat = new DevExpress.Utils.PointFloat(4.706116F, 113.9999F);
            this.lbl_other_items_.Name = "lbl_other_items_";
            this.lbl_other_items_.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_other_items_.SizeF = new System.Drawing.SizeF(206.5364F, 21.58335F);
            this.lbl_other_items_.StylePriority.UseFont = false;
            this.lbl_other_items_.StylePriority.UseTextAlignment = false;
            this.lbl_other_items_.Text = "Other conditions:";
            this.lbl_other_items_.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txt_iban
            // 
            this.txt_iban.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txt_iban.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.txt_iban.LocationFloat = new DevExpress.Utils.PointFloat(225.544F, 271.6252F);
            this.txt_iban.Name = "txt_iban";
            this.txt_iban.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txt_iban.SizeF = new System.Drawing.SizeF(490.8337F, 23F);
            this.txt_iban.StylePriority.UseBorders = false;
            this.txt_iban.StylePriority.UseFont = false;
            this.txt_iban.StylePriority.UseTextAlignment = false;
            this.txt_iban.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txt_swift_
            // 
            this.txt_swift_.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txt_swift_.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.txt_swift_.LocationFloat = new DevExpress.Utils.PointFloat(227.418F, 386.6248F);
            this.txt_swift_.Name = "txt_swift_";
            this.txt_swift_.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txt_swift_.SizeF = new System.Drawing.SizeF(488.9597F, 22.99997F);
            this.txt_swift_.StylePriority.UseBorders = false;
            this.txt_swift_.StylePriority.UseFont = false;
            this.txt_swift_.StylePriority.UseTextAlignment = false;
            this.txt_swift_.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl_bcode
            // 
            this.lbl_bcode.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl_bcode.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbl_bcode.LocationFloat = new DevExpress.Utils.PointFloat(9.6241F, 294.625F);
            this.lbl_bcode.Name = "lbl_bcode";
            this.lbl_bcode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_bcode.SizeF = new System.Drawing.SizeF(201.6186F, 22.99997F);
            this.lbl_bcode.StylePriority.UseBorders = false;
            this.lbl_bcode.StylePriority.UseFont = false;
            this.lbl_bcode.StylePriority.UseTextAlignment = false;
            this.lbl_bcode.Text = "Correspandent. No. account:";
            this.lbl_bcode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txt_bcode
            // 
            this.txt_bcode.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txt_bcode.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.txt_bcode.LocationFloat = new DevExpress.Utils.PointFloat(225.544F, 294.6251F);
            this.txt_bcode.Name = "txt_bcode";
            this.txt_bcode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txt_bcode.SizeF = new System.Drawing.SizeF(490.8337F, 23F);
            this.txt_bcode.StylePriority.UseBorders = false;
            this.txt_bcode.StylePriority.UseFont = false;
            this.txt_bcode.StylePriority.UseTextAlignment = false;
            this.txt_bcode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl_iban
            // 
            this.lbl_iban.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl_iban.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbl_iban.LocationFloat = new DevExpress.Utils.PointFloat(9.6241F, 271.6251F);
            this.lbl_iban.Name = "lbl_iban";
            this.lbl_iban.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_iban.SizeF = new System.Drawing.SizeF(156.5449F, 23.00003F);
            this.lbl_iban.StylePriority.UseBorders = false;
            this.lbl_iban.StylePriority.UseFont = false;
            this.lbl_iban.StylePriority.UseTextAlignment = false;
            this.lbl_iban.Text = "Correspandent bank:";
            this.lbl_iban.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txt_swift
            // 
            this.txt_swift.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txt_swift.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.txt_swift.LocationFloat = new DevExpress.Utils.PointFloat(225.544F, 248.6252F);
            this.txt_swift.Name = "txt_swift";
            this.txt_swift.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txt_swift.SizeF = new System.Drawing.SizeF(490.8337F, 22.99994F);
            this.txt_swift.StylePriority.UseBorders = false;
            this.txt_swift.StylePriority.UseFont = false;
            this.txt_swift.StylePriority.UseTextAlignment = false;
            this.txt_swift.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl_swift
            // 
            this.lbl_swift.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl_swift.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbl_swift.LocationFloat = new DevExpress.Utils.PointFloat(9.6241F, 248.6252F);
            this.lbl_swift.Name = "lbl_swift";
            this.lbl_swift.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_swift.SizeF = new System.Drawing.SizeF(156.5449F, 22.99994F);
            this.lbl_swift.StylePriority.UseBorders = false;
            this.lbl_swift.StylePriority.UseFont = false;
            this.lbl_swift.StylePriority.UseTextAlignment = false;
            this.lbl_swift.Text = "SWIFT address:";
            this.lbl_swift.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl_bank
            // 
            this.lbl_bank.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl_bank.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.lbl_bank.LocationFloat = new DevExpress.Utils.PointFloat(226.544F, 225.6251F);
            this.lbl_bank.Name = "lbl_bank";
            this.lbl_bank.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_bank.SizeF = new System.Drawing.SizeF(489.8337F, 23F);
            this.lbl_bank.StylePriority.UseBorders = false;
            this.lbl_bank.StylePriority.UseFont = false;
            this.lbl_bank.StylePriority.UseTextAlignment = false;
            this.lbl_bank.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(7.917341F, 225.6251F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(158.2517F, 23F);
            this.xrLabel3.StylePriority.UseBorders = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Bank:";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl_bank_det
            // 
            this.lbl_bank_det.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl_bank_det.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_bank_det.LocationFloat = new DevExpress.Utils.PointFloat(7.917341F, 202.6252F);
            this.lbl_bank_det.Name = "lbl_bank_det";
            this.lbl_bank_det.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_bank_det.SizeF = new System.Drawing.SizeF(158.2517F, 23F);
            this.lbl_bank_det.StylePriority.UseBorders = false;
            this.lbl_bank_det.StylePriority.UseFont = false;
            this.lbl_bank_det.StylePriority.UseTextAlignment = false;
            this.lbl_bank_det.Text = "Bank Details:";
            this.lbl_bank_det.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txt_tin
            // 
            this.txt_tin.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txt_tin.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.txt_tin.LocationFloat = new DevExpress.Utils.PointFloat(225.544F, 317.6251F);
            this.txt_tin.Name = "txt_tin";
            this.txt_tin.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txt_tin.SizeF = new System.Drawing.SizeF(490.8337F, 23F);
            this.txt_tin.StylePriority.UseBorders = false;
            this.txt_tin.StylePriority.UseFont = false;
            this.txt_tin.StylePriority.UseTextAlignment = false;
            this.txt_tin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl_tin
            // 
            this.lbl_tin.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl_tin.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbl_tin.LocationFloat = new DevExpress.Utils.PointFloat(8.872938F, 317.6251F);
            this.lbl_tin.Name = "lbl_tin";
            this.lbl_tin.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_tin.SizeF = new System.Drawing.SizeF(157.2961F, 22.99997F);
            this.lbl_tin.StylePriority.UseBorders = false;
            this.lbl_tin.StylePriority.UseFont = false;
            this.lbl_tin.StylePriority.UseTextAlignment = false;
            this.lbl_tin.Text = "TIN No:";
            this.lbl_tin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // txt_beneficiary
            // 
            this.txt_beneficiary.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.txt_beneficiary.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.txt_beneficiary.LocationFloat = new DevExpress.Utils.PointFloat(226.544F, 340.625F);
            this.txt_beneficiary.Name = "txt_beneficiary";
            this.txt_beneficiary.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txt_beneficiary.SizeF = new System.Drawing.SizeF(220.7517F, 23F);
            this.txt_beneficiary.StylePriority.UseBorders = false;
            this.txt_beneficiary.StylePriority.UseFont = false;
            this.txt_beneficiary.StylePriority.UseTextAlignment = false;
            this.txt_beneficiary.Text = "Alliance Multimodal LLC";
            this.txt_beneficiary.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl_beneficiary
            // 
            this.lbl_beneficiary.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lbl_beneficiary.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbl_beneficiary.LocationFloat = new DevExpress.Utils.PointFloat(9.828536F, 340.625F);
            this.lbl_beneficiary.Name = "lbl_beneficiary";
            this.lbl_beneficiary.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_beneficiary.SizeF = new System.Drawing.SizeF(215.7155F, 23F);
            this.lbl_beneficiary.StylePriority.UseBorders = false;
            this.lbl_beneficiary.StylePriority.UseFont = false;
            this.lbl_beneficiary.StylePriority.UseTextAlignment = false;
            this.lbl_beneficiary.Text = "Name of Beneficiary customer: ";
            this.lbl_beneficiary.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // bank_rek
            // 
            this.bank_rek.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.bank_rek.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.bank_rek.LocationFloat = new DevExpress.Utils.PointFloat(8.872938F, 179.6252F);
            this.bank_rek.Name = "bank_rek";
            this.bank_rek.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.bank_rek.SizeF = new System.Drawing.SizeF(236.5417F, 22.99997F);
            this.bank_rek.StylePriority.UseBorders = false;
            this.bank_rek.StylePriority.UseFont = false;
            this.bank_rek.StylePriority.UseTextAlignment = false;
            this.bank_rek.Text = "Bank Rekvizitləri";
            this.bank_rek.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.bank_rek.Visible = false;
            // 
            // txt_other_cond
            // 
            this.txt_other_cond.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.txt_other_cond.LocationFloat = new DevExpress.Utils.PointFloat(211.2427F, 113.9999F);
            this.txt_other_cond.Name = "txt_other_cond";
            this.txt_other_cond.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txt_other_cond.SizeF = new System.Drawing.SizeF(518.8826F, 21.58335F);
            this.txt_other_cond.StylePriority.UseFont = false;
            this.txt_other_cond.StylePriority.UseTextAlignment = false;
            this.txt_other_cond.Text = "Dear customer! Please record INV number and date on your payment order.";
            this.txt_other_cond.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl_note
            // 
            this.lbl_note.Font = new System.Drawing.Font("Calibri", 10.8F);
            this.lbl_note.LocationFloat = new DevExpress.Utils.PointFloat(211.2425F, 52.99998F);
            this.lbl_note.Name = "lbl_note";
            this.lbl_note.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_note.SizeF = new System.Drawing.SizeF(518.8826F, 37.99998F);
            this.lbl_note.StylePriority.UseFont = false;
            this.lbl_note.Text = "1.In accordance with exchange rate of the Central Bank of the Republic of Azerbai" +
    "jan for that day.";
            // 
            // txt_pay_terms
            // 
            this.txt_pay_terms.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.txt_pay_terms.LocationFloat = new DevExpress.Utils.PointFloat(211.2425F, 90.99996F);
            this.txt_pay_terms.Name = "txt_pay_terms";
            this.txt_pay_terms.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txt_pay_terms.SizeF = new System.Drawing.SizeF(518.8826F, 23F);
            this.txt_pay_terms.StylePriority.UseFont = false;
            this.txt_pay_terms.StylePriority.UseTextAlignment = false;
            this.txt_pay_terms.Text = "1.Pre-payment by bank transfer.";
            this.txt_pay_terms.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl_pay_terms_
            // 
            this.lbl_pay_terms_.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_pay_terms_.LocationFloat = new DevExpress.Utils.PointFloat(4.706129F, 52.99998F);
            this.lbl_pay_terms_.Name = "lbl_pay_terms_";
            this.lbl_pay_terms_.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_pay_terms_.SizeF = new System.Drawing.SizeF(206.5364F, 23.00001F);
            this.lbl_pay_terms_.StylePriority.UseFont = false;
            this.lbl_pay_terms_.StylePriority.UseTextAlignment = false;
            this.lbl_pay_terms_.Text = "Payment terms:";
            this.lbl_pay_terms_.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // txt_prc_date
            // 
            this.txt_prc_date.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txt_prc_date.LocationFloat = new DevExpress.Utils.PointFloat(211.2427F, 30F);
            this.txt_prc_date.Name = "txt_prc_date";
            this.txt_prc_date.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txt_prc_date.SizeF = new System.Drawing.SizeF(137.2517F, 22.99999F);
            this.txt_prc_date.StylePriority.UseFont = false;
            this.txt_prc_date.StylePriority.UseTextAlignment = false;
            this.txt_prc_date.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl_prc_date
            // 
            this.lbl_prc_date.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_prc_date.LocationFloat = new DevExpress.Utils.PointFloat(4.706306F, 30F);
            this.lbl_prc_date.Name = "lbl_prc_date";
            this.lbl_prc_date.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_prc_date.SizeF = new System.Drawing.SizeF(206.5364F, 23F);
            this.lbl_prc_date.StylePriority.UseFont = false;
            this.lbl_prc_date.StylePriority.UseTextAlignment = false;
            this.lbl_prc_date.Text = "Invoice is valid till:";
            this.lbl_prc_date.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lbl_grd
            // 
            this.lbl_grd.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.lbl_grd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_grd.LocationFloat = new DevExpress.Utils.PointFloat(610.3187F, 39.99999F);
            this.lbl_grd.Name = "lbl_grd";
            this.lbl_grd.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_grd.SizeF = new System.Drawing.SizeF(136.6617F, 20F);
            this.lbl_grd.StylePriority.UseBorders = false;
            this.lbl_grd.StylePriority.UseFont = false;
            this.lbl_grd.StylePriority.UseTextAlignment = false;
            this.lbl_grd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lbl_edv
            // 
            this.lbl_edv.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lbl_edv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_edv.LocationFloat = new DevExpress.Utils.PointFloat(610.3334F, 19.99998F);
            this.lbl_edv.Name = "lbl_edv";
            this.lbl_edv.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_edv.SizeF = new System.Drawing.SizeF(136.6618F, 20F);
            this.lbl_edv.StylePriority.UseBorderDashStyle = false;
            this.lbl_edv.StylePriority.UseBorders = false;
            this.lbl_edv.StylePriority.UseFont = false;
            this.lbl_edv.StylePriority.UseTextAlignment = false;
            this.lbl_edv.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // lbl_tax
            // 
            this.lbl_tax.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lbl_tax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_tax.LocationFloat = new DevExpress.Utils.PointFloat(498.3334F, 19.99998F);
            this.lbl_tax.Name = "lbl_tax";
            this.lbl_tax.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_tax.SizeF = new System.Drawing.SizeF(111.9853F, 20.00001F);
            this.lbl_tax.StylePriority.UseBorders = false;
            this.lbl_tax.StylePriority.UseFont = false;
            this.lbl_tax.StylePriority.UseTextAlignment = false;
            this.lbl_tax.Text = "VAT:";
            this.lbl_tax.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lbl_grtotal_
            // 
            this.lbl_grtotal_.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.lbl_grtotal_.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_grtotal_.LocationFloat = new DevExpress.Utils.PointFloat(498.3334F, 39.99999F);
            this.lbl_grtotal_.Name = "lbl_grtotal_";
            this.lbl_grtotal_.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbl_grtotal_.SizeF = new System.Drawing.SizeF(111.9853F, 20.00002F);
            this.lbl_grtotal_.StylePriority.UseBorders = false;
            this.lbl_grtotal_.StylePriority.UseFont = false;
            this.lbl_grtotal_.StylePriority.UseTextAlignment = false;
            this.lbl_grtotal_.Text = "Grand Total Due:";
            this.lbl_grtotal_.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1,
            this.lbl_to,
            this.txt_tin_clc,
            this.lbl_proformano_,
            this.lbl_proformadate_,
            this.lbl_inv_no,
            this.txt_clientname,
            this.lbl_date,
            this.txt_address,
            this.lbl_address_,
            this.txt_phone,
            this.lbl_phone_,
            this.txt_eaddress,
            this.lbl_eaddress_,
            this.lbl_tin_clc_,
            this.lbl_valyuta,
            this.txt_valyuta,
            this.lbl_contractno_,
            this.lbl_ct_no,
            this.to_lab_az,
            this.img_logo,
            this.lbl_profoma_h});
            this.PageHeader.HeightF = 315F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTable1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 292F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable1.SizeF = new System.Drawing.SizeF(746.9999F, 23F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.CanShrink = true;
            this.xrTableCell7.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "No";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell7.Weight = 0.34186294444788373D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell8.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "Service description";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell8.Weight = 3.0509730046360106D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell10.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseBorders = false;
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.Text = "Amount";
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell10.Weight = 1.2115351250597994D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseBorders = false;
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "Price";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell11.Weight = 1.0348283939649903D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "Total";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell12.Weight = 1.2627350662494969D;
            // 
            // ımageCollection1
            // 
            this.ımageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("ımageCollection1.ImageStream")));
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lbl_subtotal,
            this.lbl_total,
            this.lbl_price_word,
            this.lbl_tax,
            this.lbl_edv,
            this.lbl_grtotal_,
            this.lbl_grd});
            this.GroupFooter1.HeightF = 80.00002F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // KontReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageFooter,
            this.PageHeader,
            this.GroupFooter1});
            this.Margins = new System.Drawing.Printing.Margins(53, 49, 0, 0);
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1});
            this.Version = "13.2";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.XtraReport1_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
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
