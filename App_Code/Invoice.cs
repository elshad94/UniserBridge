using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Invoice
/// </summary>
public class Invoice
{
    conn con = new conn();
    login lg = new login();
    System.Data.SqlClient.SqlDataReader v_reader = null;
    System.Data.SqlClient.SqlDataReader reader2 = null;
    //                inv_id = inv.Save_Invoice(txt_INV_DATE.Value.ToString(), Convert.ToInt32(sel_ORD_FIRM.Value), Convert.ToInt32(txt_clc_recno.Value), 1, txt_INV_NO.Value, total, total_AZN, Rate, 149,int.Parse(sel_Bank.Value),int.Parse(sel_Paytaype.Value),0,txt_INV_NOTE.Text, keyfield);//totali gonder burdan


    public string GetNewInvoiceNo(int type)
    {
        string invoiceNo = "";

        //string query = @"SELECT CONCAT(Right(YEAR(GETDATE()),2), FORMAT(MONTH(GETDATE()), '00' ))+'-'+ 
        //FORMAT(ISNULL(CAST((SELECT RIGHT(INV_NO,4)+1 FROM TBL_INVOICE WHERE INV_ID = (SELECT ISNULL(MAX(ISNULL(INV_ID,0)),0) ID FROM TBL_INVOICE 
        //WHERE YEAR(INV_DATE)=YEAR(GETDATE()) AND MONTH(INV_DATE)=MONTH(GETDATE()) AND INV_TYPE =" + type + ")) as int), '0001') ,'0000') AS NEW_NUMBER";

       string query = @"SELECT CONCAT(Right(YEAR(GETDATE()),2), FORMAT(MONTH(GETDATE()), '00' ))+'-'+ 
        FORMAT(ISNULL(CAST((SELECT RIGHT(INV_NO,4)+1 FROM TBL_INVOICE WHERE INV_ID = (SELECT ISNULL(MAX(ISNULL(INV_ID,0)),0) ID FROM TBL_INVOICE 
        WHERE INV_TYPE =" + type + ")) as int), '0001') ,'0000') AS NEW_NUMBER";

        v_reader = con.dbrun(query);

        if (v_reader.Read())
        {
            invoiceNo = v_reader["NEW_NUMBER"].ToString();
        }

        return invoiceNo;
    }


    public int Save_Invoice(string INV_DATE, int INV_FRM_RECNO, int INV_CLC_RECNO, int INV_TYPE, string INV_NO, double INV_TOTAL, double INV_TOTAL_AZN, double INV_RATE, int INV_CURR_ID, int BANK_ID, int PTYPE, double EDV, string Note, int ID, string CT_NO, string INV_PERIOD = "null", string INV_STATUS = "0")
    {//INV_ID, INV_DATE, INV_FRM_RECNO, INV_CLC_RECNO, INV_TYPE, INV_NO, INV_STATUS, INV_TOTAL, INV_TOTAL_AZN, INV_CURR_ID, INV_RATE, INV_PERIOD, 
        //try
        //{
        string ssql = @"SELECT CURR_CODE,RT_RATE FROM TBL_CURRENCY 
                        LEFT JOIN TBL_RATE ON RT_CURR_ID = CURR_ID
                        WHERE CURR_ID=" + INV_CURR_ID;
        reader2 = conn.fnc_dbrun(ssql);
        if (reader2.Read())
        {
            INV_TOTAL_AZN = INV_TOTAL * float.Parse(reader2["RT_RATE"].ToString());
        }

        string query = "";
        if (ID == -1)
        {
            query = @"Insert into TBL_INVOICE
                                            (
                                             INV_DATE
                                            , INV_FRM_RECNO
                                            , INV_STATUS
                                            , INV_CLC_RECNO
                                            , INV_TYPE
                                            , INV_NO
                                            , INV_TOTAL
                                            ,INV_TOTAL_AZN
                                            ,INV_RATE
                                            ,INV_CURR_ID
                                            ,INV_BANK_ID
                                            , INV_PTYPE
                                            , INV_EDV
                                            , INV_NOTE
                                            ,INV_PERIOD
                                            ,INV_CT_NO
                                            )
                                                                  values
                                            (
                                            @INV_DATE
                                           , @INV_FRM_RECNO
                                           , @INV_STATUS
                                            , @INV_CLC_RECNO
                                            , @INV_TYPE
                                            , @INV_NO
                                            ,  @INV_TOTAL
                                            ,@INV_TOTAL_AZN
                                            ,@INV_RATE
                                            ,@INV_CURR_ID
                                            ,@INV_BANK_ID
                                            , @INV_PTYPE
                                            , @INV_EDV
                                            , @INV_NOTE
                                            ," + INV_PERIOD + @"
                                            ,@CT_NO);
                      SELECT SCOPE_IDENTITY() as _newid ";
        }
        else
        {
            query = @"Update TBL_INVOICE set
                    INV_DATE=@INV_DATE, 
                    INV_FRM_RECNO=@INV_FRM_RECNO, 
                    INV_CLC_RECNO=@INV_CLC_RECNO, 
                    INV_TYPE=@INV_TYPE, 
                    INV_NO=@INV_NO, 
                    INV_TOTAL=@INV_TOTAL,
                    INV_TOTAL_AZN=@INV_TOTAL_AZN,
                    INV_RATE=@INV_RATE,
                    INV_CURR_ID=@INV_CURR_ID,
INV_CT_NO=@CT_NO,
INV_BANK_ID=@INV_BANK_ID, INV_PTYPE=@INV_PTYPE, INV_EDV=@INV_EDV, INV_NOTE=@INV_NOTE
                    where INV_ID=@ID";
        }
        DateTime date = Convert.ToDateTime(INV_DATE);
        conn con = new conn();
        using (SqlCommand command = new SqlCommand(query, con.connectionOpen()))
        {
            command.Parameters.AddWithValue("@INV_DATE", date.ToString("MM-dd-yyyy"));
            command.Parameters.AddWithValue("@INV_FRM_RECNO", INV_FRM_RECNO);
            if (ID == -1)
            {
                command.Parameters.AddWithValue("@INV_STATUS", INV_STATUS);
            }
            command.Parameters.AddWithValue("@INV_CLC_RECNO", INV_CLC_RECNO);
            command.Parameters.AddWithValue("@INV_TYPE", INV_TYPE);
            command.Parameters.AddWithValue("@INV_NO", INV_NO);
            command.Parameters.AddWithValue("@INV_TOTAL", INV_TOTAL);
            command.Parameters.AddWithValue("@INV_TOTAL_AZN", INV_TOTAL_AZN);
            command.Parameters.AddWithValue("@INV_RATE", INV_RATE);
            command.Parameters.AddWithValue("@INV_CURR_ID", INV_CURR_ID);
            command.Parameters.AddWithValue("@INV_BANK_ID", BANK_ID);
            command.Parameters.AddWithValue("@INV_PTYPE", PTYPE);
            command.Parameters.AddWithValue("@INV_EDV", EDV);
            command.Parameters.AddWithValue("@INV_NOTE", Note);
            command.Parameters.AddWithValue("@CT_NO", CT_NO);
            // command.Parameters.AddWithValue("@INV_PERIOD", INV_PERIOD);
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
    public bool Save_InvoiceLine(
                                  int INVL_INV_ID
                                , string INVL_ORD_RECNO
                                , string INVL_EXPTYPEID
                                , string TYPE
                                , double INVL_AMOUNT
                                , string INVL_ORD_FICHENO
                                , string INVL_ORD_PODCODE
                                , double QTY
                                , string INVL_ITEMDESC1
                                , int ID
                                , int branch
                                , double EDV
                                )
    {
        //try
        //{
        system sys = new system();
        string query = "";
        if (ID == -1)
        {
            query = @"Insert into TBL_INVOICELINE 
                                            (
                                             INVL_INV_ID
                                            ,INVL_ORD_RECNO
                                            ,INVL_EXPTYPEID
                                            ,INVL_AMOUNT
                                            ,INVL_ORD_FICHENO
                                            ,INVL_ORD_PODCODE
                                            ,INVL_EXP_TYPE
                                            ,INVL_QTY
                                            ,INVL_ITEMDESC1
                                            ,INVL_EDV
                                            ,INVL_B_ID
                                            )
                                      values 
                                    (
                                             @INVL_INV_ID
                                            ,@INVL_ORD_RECNO
                                            ,@INVL_EXPTYPEID
                                            ,@INVL_AMOUNT
                                            ,@INVL_ORD_FICHENO
                                            ,@INVL_ORD_PODCODE
                                            ,@INVL_EXP_TYPE
                                            ,@INVL_QTY
                                            ,@INVL_ITEMDESC1
                                            ,@INVL_EDV
                                            ," + branch + @"
                                            );";

            //sys.Set_Status(int.Parse(INVL_ORD_RECNO), 10, branch);

            //sys.Set_Status(int.Parse(INVL_ORD_RECNO), 15);
        }
        else
        {
            query = @"Update TBL_INVOICELINE set
                                                INVL_ORD_RECNO=@INVL_ORD_RECNO,
                                                INVL_EXPTYPEID=@INVL_EXPTYPEID,
                                                INVL_AMOUNT=@INVL_AMOUNT,
                                                INVL_ORD_FICHENO=@INVL_ORD_FICHENO,
                                                INVL_ORD_PODCODE=@INVL_ORD_PODCODE,
                                                INVL_EXP_TYPE=@INVL_EXP_TYPE,
                                                INVL_QTY=@INVL_QTY,
                                                INVL_ITEMDESC1=@INVL_ITEMDESC1,
                                                INVL_TEMP=0,
                                                INVL_EDV=@INVL_EDV,
                                                INVL_B_ID=" + branch + @"
                                                where INVL_ID=@ID; ";
        }
        query += "Update TBL_INVOICE set INV_ORD_RECNO=" + INVL_ORD_RECNO + ",INV_B_ID=" + branch + " where INV_ID=" + INVL_INV_ID;
        conn con = new conn();
        using (SqlCommand command = new SqlCommand(query, con.connectionOpen()))
        {
            command.Parameters.AddWithValue("@INVL_INV_ID", INVL_INV_ID);
            command.Parameters.AddWithValue("@INVL_ORD_RECNO", INVL_ORD_RECNO);
            command.Parameters.AddWithValue("@INVL_EXPTYPEID", INVL_EXPTYPEID);
            command.Parameters.AddWithValue("@INVL_EXP_TYPE", TYPE);
            command.Parameters.AddWithValue("@INVL_AMOUNT", INVL_AMOUNT);
            command.Parameters.AddWithValue("@INVL_ORD_FICHENO", INVL_ORD_FICHENO);
            command.Parameters.AddWithValue("@INVL_ORD_PODCODE", INVL_ORD_PODCODE);
            command.Parameters.AddWithValue("@INVL_QTY", QTY);
            command.Parameters.AddWithValue("@INVL_EDV", EDV);
            command.Parameters.AddWithValue("@INVL_ITEMDESC1", INVL_ITEMDESC1);
            command.Parameters.AddWithValue("@ID", ID);

            System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();
            con.ConnetionClose();
            return true;
        }
        //}
        //catch (Exception ex)
        //{

        //    //Erroru ya database yada mail gonder


        //    return false;
        //}
    }




    public bool Save_InvoiceLine_KreditNote(
                                  int INVL_INV_ID
                                , string INVL_ORD_RECNO
                                , string INVL_EXPTYPEID
                                , string TYPE
                                , double INVL_AMOUNT
                                , string INVL_ORD_FICHENO
                                , string INVL_ORD_PODCODE
                                , double QTY
                                , string INVL_ITEMDESC1
                                , int ID
                                , int branch
                                , double EDV
                                , string INVL_INV_NO
                                )
    {
        //try
        //{
        system sys = new system();
        string query = "";
        if (ID == -1)
        {
            query = @"Insert into TBL_INVOICELINE 
                                            (
                                             INVL_INV_ID
                                            ,INVL_ORD_RECNO
                                            ,INVL_EXPTYPEID
                                            ,INVL_AMOUNT
                                            ,INVL_ORD_FICHENO
                                            ,INVL_ORD_PODCODE
                                            ,INVL_EXP_TYPE
                                            ,INVL_QTY
                                            ,INVL_ITEMDESC1
                                            ,INVL_EDV
                                            ,INVL_B_ID
                                            ,INVL_INV_NO
                                            )
                                      values 
                                    (
                                             @INVL_INV_ID
                                            ,@INVL_ORD_RECNO
                                            ,@INVL_EXPTYPEID
                                            ,@INVL_AMOUNT
                                            ,@INVL_ORD_FICHENO
                                            ,@INVL_ORD_PODCODE
                                            ,@INVL_EXP_TYPE
                                            ,@INVL_QTY
                                            ,@INVL_ITEMDESC1
                                            ,@INVL_EDV
                                            ," + branch + @"
                                            ,@INVL_INV_NO
                                            );";

            sys.Set_Status(int.Parse(INVL_ORD_RECNO), 10, branch);
            //sys.Set_Status(int.Parse(INVL_ORD_RECNO), 15);
        }
        else
        {
            query = @"Update TBL_INVOICELINE set
                                                INVL_ORD_RECNO=@INVL_ORD_RECNO,
                                                INVL_EXPTYPEID=@INVL_EXPTYPEID,
                                                INVL_AMOUNT=@INVL_AMOUNT,
                                                INVL_ORD_FICHENO=@INVL_ORD_FICHENO,
                                                INVL_ORD_PODCODE=@INVL_ORD_PODCODE,
                                                INVL_EXP_TYPE=@INVL_EXP_TYPE,
                                                INVL_QTY=@INVL_QTY,
                                                INVL_ITEMDESC1=@INVL_ITEMDESC1,
                                                INVL_TEMP=0,
                                                INVL_EDV=@INVL_EDV,
                                                INVL_B_ID=" + branch + @",
                                                INVL_INV_NO = @INVL_INV_NO
                                                where INVL_ID=@ID; ";
        }
        query += "Update TBL_INVOICE set INV_ORD_RECNO=" + INVL_ORD_RECNO + ",INV_B_ID=" + branch + " where INV_ID=" + INVL_INV_ID;
        conn con = new conn();
        using (SqlCommand command = new SqlCommand(query, con.connectionOpen()))
        {
            command.Parameters.AddWithValue("@INVL_INV_ID", INVL_INV_ID);
            command.Parameters.AddWithValue("@INVL_ORD_RECNO", INVL_ORD_RECNO);
            command.Parameters.AddWithValue("@INVL_EXPTYPEID", INVL_EXPTYPEID);
            command.Parameters.AddWithValue("@INVL_EXP_TYPE", TYPE);
            command.Parameters.AddWithValue("@INVL_AMOUNT", INVL_AMOUNT);
            command.Parameters.AddWithValue("@INVL_ORD_FICHENO", INVL_ORD_FICHENO);
            command.Parameters.AddWithValue("@INVL_ORD_PODCODE", INVL_ORD_PODCODE);
            command.Parameters.AddWithValue("@INVL_QTY", QTY);
            command.Parameters.AddWithValue("@INVL_EDV", EDV);
            command.Parameters.AddWithValue("@INVL_ITEMDESC1", INVL_ITEMDESC1);
            command.Parameters.AddWithValue("@INVL_INV_NO", INVL_INV_NO);
            command.Parameters.AddWithValue("@ID", ID);

            System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();
            con.ConnetionClose();
            return true;
        }
        //}
        //catch (Exception ex)
        //{

        //    //Erroru ya database yada mail gonder


        //    return false;
        //}
    }





    public string Get_INVNO(int CTYPE = 1)
    {
        //string v_sql = @"Select isnull(substring(Max(INV_NO),6,8),0)+1 as _no from TBL_INVOICE where Substring(INV_NO,3,2)=RIGHT(YEAR(getDate()), 2) and INV_STATUS<>-1 and INV_TYPE in(1,2,5)";
        string v_sql = "";
        if (CTYPE == 1)
        {
            v_sql = @"Select isnull(substring(Max(INV_NO),9,4),0)+1 as _no from TBL_INVOICE 
            inner join [dbo].[TBL_CLCARDS]  on INV_CLC_RECNO=CLC_RECNO
            where Substring(INV_NO,4,2)=RIGHT(YEAR(getDate()), 2) and Substring(INV_NO,6,2)='" + DateTime.Now.Month.ToString("00") + @"'
			 and INV_STATUS<>-1 and INV_TYPE in(1) and isnull(CLC_TYPE,0)=2 and INV_ID>9034";
        }
        else
        {
            v_sql = @"Select isnull(substring(Max(INV_NO),13,4),0)+1 as _no from TBL_INVOICE 
            inner join [dbo].[TBL_CLCARDS]  on INV_CLC_RECNO=CLC_RECNO
            where Substring(INV_NO,8,2)=RIGHT(YEAR(getDate()), 2) and Substring(INV_NO,10,2)='" + DateTime.Now.Month.ToString("00") + @"' and INV_STATUS<>-1 and 
			INV_TYPE in(1) and isnull(CLC_TYPE,0)=2  and INV_ID>9034";
        }

        v_reader = con.dbrun(v_sql);
        int temp = 0;
        string no = "";
        if (v_reader.Read())
        {
            temp = int.Parse(v_reader["_no"].ToString());
        }
        no = temp.ToString("0000");
        string year = DateTime.Now.Year.ToString();
        string month = DateTime.Now.Month.ToString("00");
        if (CTYPE == 1)
        {
            return "PF " + year.Substring(2, 2) + month + "-" + no;
        }
        else
        {
            return "PF-AZE " + year.Substring(2, 2) + month + "-" + no;
        }

    }

    public string Get_INVPURCHASENO()
    {
        string v_sql = @"Select isnull(substring(Max(INV_NO),6,8),0)+1 as _no from TBL_INVOICE where Substring(INV_NO,3,2)=RIGHT(YEAR(getDate()), 2) and INV_STATUS<>-1 and INV_TYPE in(3,4)";
        v_reader = con.dbrun(v_sql);
        int temp = 0;
        string no = "";
        while (v_reader.Read())
        {
            temp = Convert.ToInt32(v_reader["_no"].ToString());
        }
        no = temp.ToString("00000");
        string year = DateTime.Now.Year.ToString();
        return "IN" + year.Substring(2, 2) + "-" + no;
    }

    public List<OInvoice> Get_INVList(int type = -1, string beginDate = null, string endDate = null, int invoiceId = -1)
    {
        List<OInvoice> List_data = new List<OInvoice>();

        string query = "", typeFilter="", dateFilter = "", idFilter = "";

        if (type != -1)
        {
            typeFilter += " and INV_TYPE = " + type;
        }

        if (beginDate != null && endDate != null)
        {
            dateFilter += " AND INV_DATE BETWEEN '" + beginDate + @"' AND '" + endDate + "'";
        }

        if (invoiceId != -1)
        {
            idFilter += " and INV_ID = " + invoiceId;
        }

        query = @"Select N'Əlavə Et'  ADD_,INVL_B_ID,B_ID,ORD_FICHENO,SUM(BI_AMOUNT) BD_NO,INV_ID, INV_DATE INV_DATE,INV_DATE _INV_DATE, INV_FRM_RECNO,
                  FRM_NAME as FRM_NAME,ORD_RECNO,
                  INV_CLC_RECNO,CLC_ALLNAME, INV_TYPE, INV_NO, INV_STATUS, INV_TOTAL,INV_CURR_ID,CURR_CODE,INV_NOTE,
                  INV_BANK_ID,SC_VALUE" + lg.Get_Lang() + @" EXP,INV_RATE,
                    CASE WHEN count(UF_UFILE)=0 THEN ''
                            WHEN count(UF_UFILE)<>0 THEN 'Bax' END FILE1,
                    CASE WHEN count(UF_UFILE)=0 THEN ''
                            WHEN count(UF_UFILE)<>0 THEN UF_FILE END FILE1_,B_NAME" + lg.Get_Lang() + @" B_NAME
                    from TBL_INVOICE
                    LEFT JOIN (SELECT BI_INV_ID,BI_AMOUNT FROM TBL_BANKINV
                    INNER JOIN TBL_BANKDOC ON BD_ID=BI_BD_ID
                    UNION ALL
                    SELECT CI_INV_ID,CI_AMOUNT FROM TBL_CASHINV
                    INNER JOIN TBL_CASHDOC ON CI_CD_ID=CD_ID) KK ON INV_ID=BI_INV_ID
                    inner join TBL_FIRMS on FRM_RECNO=INV_FRM_RECNO
                    inner join TBL_CLCARDS on CLC_RECNO=INV_CLC_RECNO
                    INNER JOIN (SELECT  INVL_ORD_RECNO ,INVL_INV_ID,isnull(INVL_B_ID,1) INVL_B_ID FROM tbl_invoiceline GROUP BY INVL_ORD_RECNO ,INVL_INV_ID,isnull(INVL_B_ID,1)  ) SS ON INVL_INV_ID=INV_ID
                    LEFT join (SELECT UF_DOCID,UF_UFILE,UF_FILE FROM  TBL_UPFILES WHERE  UF_APP='ORD' AND ISNULL(UF_STATUS,0)<>2 AND ISNULL(UF_STATUS,0)<>99
                    GROUP BY UF_DOCID,UF_UFILE,UF_FILE) ZZ ON UF_DOCID=INVL_ORD_RECNO 
                    LEFT JOIN TBL_TRANSORDERS ON ORD_RECNO=INVL_ORD_RECNO
                    left join TBL_SPECODES on  SC_REFID=INV_CLC_RECNO and SC_TYPE='EXPENCETYPE'
                    inner join T_SYS_USERBR on UB_B_ID=INVL_B_ID
                    left join T_SYS_BRANCH on B_ID=isnull(INV_B_ID,1)
                    left join TBL_CURRENCY on INV_CURR_ID=CURR_ID
                  WHERE INV_STATUS <> 99 " + typeFilter + @" and UB_U_ID=" + lg.Get_userid() + idFilter + dateFilter + @" 
                  group by CURR_CODE,B_ID,INV_ID, INV_DATE, INV_FRM_RECNO,ORD_FICHENO,
                  FRM_NAME ,UF_UFILE,UF_FILE,INVL_B_ID,INV_NOTE,ORD_RECNO,
                  INV_CLC_RECNO,CLC_ALLNAME, INV_TYPE, INV_NO, INV_STATUS, INV_TOTAL,INV_CURR_ID,
                  INV_BANK_ID,SC_VALUE" + lg.Get_Lang() + @" ,INV_RATE,B_NAME" + lg.Get_Lang() + @"
                  order by INV_ID desc";

        //   query = @"Select N'FILE'  ADD_,INVL_B_ID,B_ID,SSS.ORD_FICHENO ORD_FICHENO,SUM(BI_AMOUNT) BD_NO,INV_ID, INV_DATE INV_DATE,INV_DATE _INV_DATE, INV_FRM_RECNO,
        //             FRM_NAME as FRM_NAME,SSS.ORD_RECNO ORD_RECNO,
        //             INV_CLC_RECNO,CLC_ALLNAME, INV_TYPE, INV_NO, INV_STATUS, INV_TOTAL,INV_CURR_ID,CURR_CODE,INV_NOTE,
        //             INV_BANK_ID,SC_VALUE" + lg.Get_Lang() + @" EXP,INV_RATE,
        //               CASE WHEN count(UF_UFILE)=0 THEN ''
        //                       WHEN count(UF_UFILE)<>0 THEN 'Bax' END FILE1,

        //               B_NAME" + lg.Get_Lang() + @" B_NAME
        //               from TBL_INVOICE
        //               LEFT JOIN (SELECT BI_INV_ID,BI_AMOUNT FROM TBL_BANKINV
        //               INNER JOIN TBL_BANKDOC ON BD_ID=BI_BD_ID
        //               UNION ALL
        //               SELECT CI_INV_ID,CI_AMOUNT FROM TBL_CASHINV
        //               INNER JOIN TBL_CASHDOC ON CI_CD_ID=CD_ID) KK ON INV_ID=BI_INV_ID
        //               inner join TBL_FIRMS on FRM_RECNO=INV_FRM_RECNO
        //               inner join TBL_CLCARDS on CLC_RECNO=INV_CLC_RECNO
        //               LEFT JOIN (SELECT  INVL_ORD_RECNO ,INVL_INV_ID,isnull(INVL_B_ID,1) INVL_B_ID FROM tbl_invoiceline GROUP BY INVL_ORD_RECNO ,INVL_INV_ID,isnull(INVL_B_ID,1)  ) SS ON INVL_INV_ID=INV_ID
        //               LEFT join (SELECT UF_DOCID,UF_UFILE,UF_FILE FROM  TBL_UPFILES WHERE  UF_APP='ORD' AND ISNULL(UF_STATUS,0)<>2
        //               GROUP BY UF_DOCID,UF_UFILE,UF_FILE) ZZ ON UF_DOCID=INVL_ORD_RECNO 
        //               LEFT JOIN TBL_TRANSORDERS S ON S.ORD_RECNO=INVL_ORD_RECNO
        //LEFT JOIN TBL_TRANSORDERS SSS ON SSS.ORD_RECNO=INV_ORD_RECNO
        //               left join TBL_SPECODES on  SC_REFID=INV_CLC_RECNO and SC_TYPE='EXPENCETYPE'
        //               LEFT join T_SYS_USERBR on UB_B_ID=SSS.ORD_B_ID
        //               left join T_SYS_BRANCH on B_ID=isnull(INV_B_ID,1)
        //               left join TBL_CURRENCY on INV_CURR_ID=CURR_ID
        //             WHERE isnull(INV_STATUS,0)<>-1 and UB_U_ID=" + lg.Get_userid() + @" 
        //             group by CURR_CODE,B_ID,INV_ID, INV_DATE, INV_FRM_RECNO,SSS.ORD_FICHENO,
        //             FRM_NAME ,INVL_B_ID,INV_NOTE,SSS.ORD_RECNO,
        //             INV_CLC_RECNO,CLC_ALLNAME, INV_TYPE, INV_NO, INV_STATUS, INV_TOTAL,INV_CURR_ID,
        //             INV_BANK_ID,SC_VALUE" + lg.Get_Lang() + @" ,INV_RATE,B_NAME" + lg.Get_Lang() + @"
        //             order by INV_ID desc";

        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            List_data.Add(new OInvoice
            {
                ID = Convert.ToInt32(reader["INV_ID"].ToString()),
                DATE = Convert.ToDateTime(reader["INV_DATE"].ToString()),
                _DATE = reader["_INV_DATE"].ToString(),
                FRM_RECNO = Convert.ToInt32(reader["INV_FRM_RECNO"].ToString()),
                FRM_NO = reader["FRM_NAME"].ToString(),
                CLC_RECNO = Convert.ToInt32(reader["INV_CLC_RECNO"].ToString()),
                CLC_NO = reader["CLC_ALLNAME"].ToString(),
                TYPE = Convert.ToInt32(reader["INV_TYPE"].ToString()),
                NO = reader["INV_NO"].ToString(),
                TOTAL = Convert.ToDouble(reader["INV_TOTAL"].ToString()),
                CURR_ID = Convert.ToInt32(reader["INV_CURR_ID"].ToString()),
                EXP = reader["EXP"].ToString(),
                RATE = reader["INV_RATE"].ToString(),
                BANK_ID = int.Parse(reader["INV_BANK_ID"].ToString()),
                STATUS = reader["INV_STATUS"].ToString(),
                FILE1 = reader["FILE1"].ToString(),
                //FILE1_ = reader["FILE1_"].ToString(),
                ADD_ = reader["ADD_"].ToString(),
                BCNO_ = reader["BD_NO"].ToString(),
                ORDNO_ = reader["ORD_FICHENO"].ToString(),
                BRANCH = reader["B_NAME"].ToString(),
                B_ID = reader["B_ID"].ToString(),
                CURR_CODE = reader["CURR_CODE"].ToString(),
                NOTE = reader["INV_NOTE"].ToString(),
                ORD_RECNO = reader["ORD_RECNO"].ToString()
            });
        }
        return List_data;
    }
    public bool Delete_INV(int id)
    {
        string query = "";
        //  query = @"Delete from TBL_INVOICE where INV_ID=@id;Delete from TBL_INVOICELINE where INVL_INV_ID=@id";
        query = @"Update  [TBL_INVOICE] set  [INV_STATUS]=99 where [INV_ID]=@id
                  Update  [TBL_INVOICELINE] set  INVL_STATUS=99 where INVL_ID=@id";
        conn con = new conn();
        using (SqlCommand command = new SqlCommand(query, con.connectionOpen()))
        {
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            con.ConnetionClose();
            return true;
        }
    }

    public List<OInvoice> Get_INVline(int ORD_ID, int INV_ID)
    {
        List<OInvoice> List_data = new List<OInvoice>();
        string query = "";
        if (ORD_ID != -1)
        {

            query = @"select EX.PARENT EXPID,TBL_EXPENCE.EX_VALUE1,
case when EX_TYPE=1 and ORD_VGNTRNTYPE=1 then isnull(EXP_QTY,0) 
     when EX_TYPE=4 and ORD_VGNTRNTYPE=1 then isnull(EXP_QTY,0)  
     when EX_TYPE=1 and ORD_VGNTRNTYPE=2 then isnull(EXP_QTY,0)
     when EX_TYPE=4 and ORD_VGNTRNTYPE=2 then isnull(EXP_QTY,0) end as QTY,
    case --  when EX_TYPE=1 and ORD_VGNTRNTYPE=1 then ROUND(sum(EXP_AMOUNT)/(case when ORD_VGALLTONNAJ=0 then 1 else isnull(EXP_QTY,0) end) ,4) 
      --    when EX_TYPE=4 and ORD_VGNTRNTYPE=1 then ROUND(sum(EXP_AMOUNT)/(case when ORD_VAGONCOUNT=0 then 1 else ORD_VAGONCOUNT end),4)
        --  when EX_TYPE=1 and ORD_VGNTRNTYPE=2 then ROUND(sum(EXP_AMOUNT)/isnull(EXP_QTY,0),2) 
        --  when EX_TYPE=4 and ORD_VGNTRNTYPE=2 then ROUND(sum(EXP_AMOUNT)/isnull(EXP_QTY,0),2) 

--when EX_TYPE=1 and ORD_VGNTRNTYPE=1 then sum(EXP_AMOUNT)/isnull(EXP_QTY,0) 
--when EX_TYPE=4 and ORD_VGNTRNTYPE=1 then sum(EXP_AMOUNT)/isnull(EXP_QTY,0)
--when EX_TYPE=1 and ORD_VGNTRNTYPE=2 then sum(EXP_AMOUNT)/isnull(EXP_QTY,0)
--when EX_TYPE=4 and ORD_VGNTRNTYPE=2 then sum(EXP_AMOUNT)/isnull(EXP_QTY,0)

when EX_TYPE=1 and ORD_VGNTRNTYPE=1 then sum(EXP_EXPENSE)--/isnull(EXP_QTY,0),2) 
when EX_TYPE=4 and ORD_VGNTRNTYPE=1 then sum(EXP_EXPENSE)--/isnull(EXP_QTY,0),2)
when EX_TYPE=1 and ORD_VGNTRNTYPE=2 then sum(EXP_EXPENSE)--/isnull(EXP_QTY,0),2)
when EX_TYPE=4 and ORD_VGNTRNTYPE=2 then sum(EXP_EXPENSE)--/isnull(EXP_QTY,0),2)
     end as ADY_EXPENSE,

sum(EXP_AMOUNT) TOTAL,
     case 
     when isnull(CLC_INVLANG,1)=3 and ORD_ACTTYPE=1 then bgpoint.PNT_NAME3+' - '+Tpoint.PNT_NAME3  
    when isnull(CLC_INVLANG,1)=3 and ORD_ACTTYPE=2 then Fpoint.PNT_NAME3+ ' - '+enpoint.PNT_NAME3  
    when isnull(CLC_INVLANG,1)=3 and ORD_ACTTYPE=3 then bgpoint.PNT_NAME3+' - '+enpoint.PNT_NAME3  
    when isnull(CLC_INVLANG,1)=3 and ORD_ACTTYPE=4 then Fpoint.PNT_NAME3+ ' - '+Tpoint.PNT_NAME3 
   
    when isnull(CLC_INVLANG,1)=1 and ORD_ACTTYPE=1 then bgpoint.PNT_NAME1+' - '+Tpoint.PNT_NAME1  
	when isnull(CLC_INVLANG,1)=1 and ORD_ACTTYPE=2 then Fpoint.PNT_NAME1+ ' - '+enpoint.PNT_NAME1  
	when isnull(CLC_INVLANG,1)=1 and ORD_ACTTYPE=3 then bgpoint.PNT_NAME1+' - '+enpoint.PNT_NAME1  
	when isnull(CLC_INVLANG,1)=1 and ORD_ACTTYPE=4 then Fpoint.PNT_NAME1+ ' - '+Tpoint.PNT_NAME1  
	when isnull(CLC_INVLANG,1)=2 and ORD_ACTTYPE=1 then bgpoint.PNT_NAME2+' - '+Tpoint.PNT_NAME2  
	when isnull(CLC_INVLANG,1)=2 and ORD_ACTTYPE=2 then Fpoint.PNT_NAME2+ ' - '+enpoint.PNT_NAME2  
	when isnull(CLC_INVLANG,1)=2 and ORD_ACTTYPE=3 then bgpoint.PNT_NAME2+' - '+enpoint.PNT_NAME2  
	when isnull(CLC_INVLANG,1)=2 and ORD_ACTTYPE=4 then Fpoint.PNT_NAME2+ ' - '+Tpoint.PNT_NAME2  
	end as ITEMDESC1,'-1' INVL_ID, EXP_ORDRECNO ORD_RECNO,EX_ID EXPID,1 EXP_TYPE,
                            ORD_PODCODE,ORD_FICHENO,EX_VALUE1 SC_VALUE1,  'Dolu' _STATUS,0 as INVL_EDV
from(
Select 
case when isnull(EX_PARENTID,0)=0 then EX_ID else EX_PARENTID end as PARENT,EXP_QTY,EXP_AMOUNT,EXP_EXPENSE,EXP_ORDRECNO from TBL_TRANSORDEREXP
inner join TBL_EXPENCE on EXP_EXPTYPEID=EX_ID and EXP_TYPE=1) EX
left join TBL_EXPENCE on TBL_EXPENCE.EX_ID=EX.PARENT
left join TBL_TRANSORDERS on ORD_RECNO=EXP_ORDRECNO
left join TBL_CLCARDS on CLC_RECNO=ORD_CLCRECNO
left join TBL_POINTS Fpoint on Fpoint.PNT_RECNO=ORD_FPOINT
left join TBL_POINTS Tpoint on Tpoint.PNT_RECNO=ORD_TPOINT
left join TBL_POINTS bgpoint on bgpoint.PNT_RECNO=ORD_BEGPOINT
left join TBL_POINTS enpoint on enpoint.PNT_RECNO=ORD_ENDPOINT
where ORD_RECNO=@id 
group by  EX.PARENT,TBL_EXPENCE.EX_VALUE1,ORD_VAGONTONNAJ,CLC_INVLANG,ORD_ACTTYPE,bgpoint.PNT_NAME1,Tpoint.PNT_NAME1 ,EXP_QTY,
Fpoint.PNT_NAME1,enpoint.PNT_NAME1,+bgpoint.PNT_NAME2,Tpoint.PNT_NAME2,Fpoint.PNT_NAME2,enpoint.PNT_NAME2  ,EXP_ORDRECNO ,
EX_ID,ORD_PODCODE,ORD_FICHENO,EX_VALUE1,ORD_VGALLTONNAJ,ORD_VAGONCOUNT,EX_TYPE, ORD_VGNTRNTYPE,
bgpoint.PNT_NAME3,Tpoint.PNT_NAME3,
Fpoint.PNT_NAME3,enpoint.PNT_NAME3
having sum(EXP_QTY*EXP_AMOUNT)<>0

UNION ALL

select EX.PARENT EXPID,TBL_EXPENCE.EX_VALUE1,
    
isnull(EXP_QTY,0) QTY,
sum(EXP_AMOUNT)/isnull(EXP_QTY,0)    ADY_EXPENSE,
 sum(EXP_AMOUNT) TOTAL,
case 
    when isnull(CLC_INVLANG,1)=3 and ORD_ACTTYPE=1 then bgpoint.PNT_NAME3+' - '+Tpoint.PNT_NAME3  
    when isnull(CLC_INVLANG,1)=3 and ORD_ACTTYPE=2 then Fpoint.PNT_NAME3+ ' - '+enpoint.PNT_NAME3  
    when isnull(CLC_INVLANG,1)=3 and ORD_ACTTYPE=3 then bgpoint.PNT_NAME3+' - '+enpoint.PNT_NAME3  
    when isnull(CLC_INVLANG,1)=3 and ORD_ACTTYPE=4 then Fpoint.PNT_NAME3+ ' - '+Tpoint.PNT_NAME3 

    when isnull(CLC_INVLANG,1)=1 and ORD_ACTTYPE=1 then Tpoint.PNT_NAME1  +' - '+ bgpoint.PNT_NAME1
    when isnull(CLC_INVLANG,1)=1 and ORD_ACTTYPE=2 then enpoint.PNT_NAME1 +' - '+ Fpoint.PNT_NAME1  
    when isnull(CLC_INVLANG,1)=1 and ORD_ACTTYPE=3 then enpoint.PNT_NAME1 +' - '+ bgpoint.PNT_NAME1 
    when isnull(CLC_INVLANG,1)=1 and ORD_ACTTYPE=4 then Tpoint.PNT_NAME1  +' - '+ Fpoint.PNT_NAME1 
														 				  		    
    when isnull(CLC_INVLANG,1)=2 and ORD_ACTTYPE=1 then Tpoint.PNT_NAME2  +' - '+ bgpoint.PNT_NAME2
    when isnull(CLC_INVLANG,1)=2 and ORD_ACTTYPE=2 then enpoint.PNT_NAME2 +' - '+ Fpoint.PNT_NAME2  
    when isnull(CLC_INVLANG,1)=2 and ORD_ACTTYPE=3 then enpoint.PNT_NAME2 +' - '+ bgpoint.PNT_NAME2 
    when isnull(CLC_INVLANG,1)=2 and ORD_ACTTYPE=4 then Tpoint.PNT_NAME2  +' - '+ Fpoint.PNT_NAME2 
    end as ITEMDESC1,'-1' INVL_ID, EXP_ORDRECNO ORD_RECNO,EX_ID EXPID,2 EXP_TYPE, 
    ORD_PODCODE,ORD_FICHENO,EX_VALUE1 SC_VALUE1,  'Boş' _STATUS ,0 as INVL_EDV
from(
Select 
case when isnull(EX_PARENTID,0)=0 then EX_ID else EX_PARENTID end as PARENT,EXP_QTY,EXP_AMOUNT,EXP_ORDRECNO from TBL_TRANSORDEREXP
inner join TBL_EXPENCE on EXP_EXPTYPEID=EX_ID and EXP_TYPE=2) EX
left join TBL_EXPENCE on TBL_EXPENCE.EX_ID=EX.PARENT
left join TBL_TRANSORDERS on ORD_RECNO=EXP_ORDRECNO
left join TBL_CLCARDS on CLC_RECNO=ORD_CLCRECNO
left join TBL_POINTS Fpoint on Fpoint.PNT_RECNO=ORD_FPOINT
left join TBL_POINTS Tpoint on Tpoint.PNT_RECNO=ORD_TPOINT
left join TBL_POINTS bgpoint on bgpoint.PNT_RECNO=ORD_BEGPOINT
left join TBL_POINTS enpoint on enpoint.PNT_RECNO=ORD_ENDPOINT
where ORD_RECNO=@id   
group by  EX.PARENT,TBL_EXPENCE.EX_VALUE1,ORD_VAGONTONNAJ,CLC_INVLANG,ORD_ACTTYPE,bgpoint.PNT_NAME1,Tpoint.PNT_NAME1 ,
Fpoint.PNT_NAME1,enpoint.PNT_NAME1,+bgpoint.PNT_NAME2,Tpoint.PNT_NAME2,Fpoint.PNT_NAME2,enpoint.PNT_NAME2  ,EXP_ORDRECNO ,
EX_ID,ORD_PODCODE,ORD_FICHENO,EX_VALUE1,ORD_VGALLTONNAJ,ORD_VAGONCOUNT,EX_TYPE,ORD_VGALLCOUNT,EXP_QTY,
bgpoint.PNT_NAME3,Tpoint.PNT_NAME3,
Fpoint.PNT_NAME3,enpoint.PNT_NAME3
having sum(EXP_QTY*EXP_AMOUNT)<>0";

        }
        else if (INV_ID != -1)
        {
            query = @"Select INVL_ITEMDESC1 ITEMDESC1,INVL_ID,isnull(ORD_RECNO,0) ORD_RECNO,isnull(SPC.SC_REFID,0) EXPID,ISNULL(INVL_EXP_TYPE, 0) EXP_TYPE,ORD_PODCODE,ORD_FICHENO,
SPC.SC_VALUE1,ISNULL(INVL_AMOUNT, 0) ADY_EXPENSE,isnull(SPC_TYPE.SC_VALUE1,0) _STATUS,isnull(INVL_QTY,0) QTY,ISNULL(INVL_AMOUNT, 0)*isnull(INVL_QTY,0) TOTAL,INVL_EDV,INV_NOTE from TBL_INVOICELINE
inner join  TBL_INVOICE on INV_ID=INVL_INV_ID
LEFT join TBL_SPECODES SPC on SC_REFID=INVL_EXPTYPEID and SPC.SC_TYPE='EXPENCETYPE'
LEFT join TBL_SPECODES SPC_TYPE on SPC_TYPE.SC_REFID=INVL_EXP_TYPE and SPC_TYPE.SC_TYPE='EXP_TYPE'
LEFT join TBL_TRANSORDERS on INVL_ORD_RECNO=ORD_RECNO
where INVL_INV_ID=@INV_ID;
Update  TBL_INVOICELINE  set INVL_TEMP=1   where  INVL_INV_ID=@INV_ID";
        }
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        cmd.Parameters.AddWithValue("@id", ORD_ID);
        cmd.Parameters.AddWithValue("@INV_ID", INV_ID);
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        int count = 0;
        while (reader.Read())
        {
            count++;
            List_data.Add(new OInvoice
            {
                //  ROWCOUNT=count,
                ITEMDESC1 = reader["ITEMDESC1"].ToString(),
                LINE_ID = Convert.ToInt32(reader["INVL_ID"].ToString()),
                ID = Convert.ToInt32(reader["ORD_RECNO"].ToString()),
                EXPID = Convert.ToInt32(reader["EXPID"].ToString()),
                PODCODE = reader["ORD_PODCODE"].ToString(),
                FICHENO = reader["ORD_FICHENO"].ToString(),
                EXP = reader["SC_VALUE1"].ToString(),
                TYPE = Convert.ToInt32(reader["EXP_TYPE"].ToString()),
                STATUS = reader["_STATUS"].ToString(),
                EXPENSE = reader["ADY_EXPENSE"].ToString(),
                QTY = Convert.ToDouble(reader["QTY"].ToString()),
                TOTAL = double.Parse(reader["TOTAL"].ToString()),
                L_EDV = double.Parse(reader["INVL_EDV"].ToString()),


            });
        }
        return List_data;
    }



    public List<OInvoice> Get_INVline_KreditNote(int ORD_ID, int INV_ID)
    {
        List<OInvoice> List_data = new List<OInvoice>();
        string query = "";

        query = @"Select INVL_ITEMDESC1 ITEMDESC1,INVL_ID,isnull(ORD_RECNO,0) ORD_RECNO,isnull(SPC.SC_REFID,0) EXPID,ISNULL(INVL_EXP_TYPE, 0) EXP_TYPE,ORD_PODCODE,ORD_FICHENO,
                        SPC.SC_VALUE1,ISNULL(INVL_AMOUNT, 0) ADY_EXPENSE,isnull(SPC_TYPE.SC_VALUE1,0) _STATUS,isnull(INVL_QTY,0) QTY,ISNULL(INVL_AMOUNT, 0)*isnull(INVL_QTY,0) TOTAL,INVL_EDV,INV_NOTE, INVL_INV_NO from TBL_INVOICELINE
                        inner join  TBL_INVOICE on INV_ID=INVL_INV_ID
                        LEFT join TBL_SPECODES SPC on SC_REFID=INVL_EXPTYPEID and SPC.SC_TYPE='EXPENCETYPE'
                        LEFT join TBL_SPECODES SPC_TYPE on SPC_TYPE.SC_REFID=INVL_EXP_TYPE and SPC_TYPE.SC_TYPE='EXP_TYPE'
                        LEFT join TBL_TRANSORDERS on INVL_ORD_RECNO=ORD_RECNO
                        where INVL_INV_ID=@INV_ID;
                        Update  TBL_INVOICELINE  set INVL_TEMP=1   where  INVL_INV_ID=@INV_ID";

        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        cmd.Parameters.AddWithValue("@id", ORD_ID);
        cmd.Parameters.AddWithValue("@INV_ID", INV_ID);
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        int count = 0;
        while (reader.Read())
        {
            count++;
            List_data.Add(new OInvoice
            {
                //  ROWCOUNT=count,
                ITEMDESC1 = reader["ITEMDESC1"].ToString(),
                LINE_ID = Convert.ToInt32(reader["INVL_ID"].ToString()),
                ID = Convert.ToInt32(reader["ORD_RECNO"].ToString()),
                EXPID = Convert.ToInt32(reader["EXPID"].ToString()),
                PODCODE = reader["ORD_PODCODE"].ToString(),
                FICHENO = reader["ORD_FICHENO"].ToString(),
                EXP = reader["SC_VALUE1"].ToString(),
                TYPE = Convert.ToInt32(reader["EXP_TYPE"].ToString()),
                STATUS = reader["_STATUS"].ToString(),
                EXPENSE = reader["ADY_EXPENSE"].ToString(),
                QTY = Convert.ToDouble(reader["QTY"].ToString()),
                TOTAL = double.Parse(reader["TOTAL"].ToString()),
                L_EDV = double.Parse(reader["INVL_EDV"].ToString()),
                INVL_INV_NO = reader["INVL_INV_NO"].ToString(),

            });
        }
        return List_data;
    }

    public List<OInvoice> Get_FactINVline(string period, int ClCID, int INV_ID = -1)
    {
        List<OInvoice> List_data = new List<OInvoice>();
        string query = "";
        if (INV_ID == -1)
        {
            DateTime enddate = new DateTime(DateTime.Parse(period).Year, DateTime.Parse(period).Month, DateTime.DaysInMonth(DateTime.Parse(period).Year, DateTime.Parse(period).Month));

            query = @"
Declare @CLCRECNO int 
Declare @BeginPeriod datetime 
Declare @endPeriod datetime 

set @BeginPeriod='" + period + @"'
set @endPeriod='" + enddate + @"'

set @CLCRECNO=" + ClCID + @"

Select '' ITEMDESC1,'-1' INVL_ID,_EXP,EXPID,QTY,PRICE,ORD_RECNO,ORD_PODCODE,ORD_FICHENO ,FCT_EXDATE
from (
Select SC_VALUE1 _EXP,SC_REFID EXPID,FCT_WEIGHT QTY,[FCT_FERRY] PRICE,ORD_PODCODE,ORD_FICHENO,ORD_RECNO,ORD_CLCRECNO,FCT_EXDATE 
from TBL_FACT
inner join TBL_TRANSORDERS on FCT_ORDID=ORD_RECNO
inner join TBL_SPECODES on SC_REFID=4 and SC_TYPE='EXPENCETYPE'

UNiON ALL
Select SC_VALUE1,SC_REFID,FCT_WEIGHT,[FCT_ADYS],ORD_PODCODE,ORD_FICHENO,ORD_RECNO,ORD_CLCRECNO,FCT_EXDATE from TBL_FACT
inner join TBL_TRANSORDERS on FCT_ORDID=ORD_RECNO
inner join TBL_SPECODES on SC_REFID=1 and SC_TYPE='EXPENCETYPE'


UNiON ALL
Select SC_VALUE1,SC_REFID,FCT_WEIGHT,[FCT_SECURITYS],ORD_PODCODE,ORD_FICHENO,ORD_RECNO,ORD_CLCRECNO,FCT_EXDATE from TBL_FACT
inner join TBL_TRANSORDERS on FCT_ORDID=ORD_RECNO
inner join TBL_SPECODES on SC_REFID=3 and SC_TYPE='EXPENCETYPE'

UNiON ALL
Select SC_VALUE1,SC_REFID,FCT_WEIGHT,[FCT_CASPARS],ORD_PODCODE,ORD_FICHENO,ORD_RECNO,ORD_CLCRECNO,FCT_EXDATE from TBL_FACT
inner join TBL_TRANSORDERS on FCT_ORDID=ORD_RECNO
inner join TBL_SPECODES on SC_REFID=7 and SC_TYPE='EXPENCETYPE'

UNiON ALL
Select SC_VALUE1,SC_REFID,FCT_WEIGHT,[FCT_BRIDGES],ORD_PODCODE,ORD_FICHENO,ORD_RECNO,ORD_CLCRECNO,FCT_EXDATE from TBL_FACT
inner join TBL_TRANSORDERS on FCT_ORDID=ORD_RECNO
inner join TBL_SPECODES on SC_REFID=9 and SC_TYPE='EXPENCETYPE'
 )Temp 
where PRICE>0 and ORD_CLCRECNO=@CLCRECNO and (FCT_EXDATE between @BeginPeriod and @endPeriod)
order by ORD_PODCODE asc ,EXPID asc";
        }
        else if (INV_ID != -1)
        {
            query = @"Select INVL_ITEMDESC1 ITEMDESC1,INVL_ID,ORD_RECNO,ISNULL(SPC.SC_REFID,0) EXPID,ISNULL(INVL_EXP_TYPE, 0) EXP_TYPE,ORD_PODCODE,ORD_FICHENO,SPC.SC_VALUE1 _EXP,ISNULL(INVL_AMOUNT, 0) PRICE,SPC_TYPE.SC_VALUE1 _STATUS,isnull(INVL_QTY,0) QTY from TBL_INVOICELINE
inner join  TBL_INVOICE on INV_ID=INVL_INV_ID
LEFT join TBL_SPECODES SPC on SC_REFID=INVL_EXPTYPEID and SPC.SC_TYPE='EXPENCETYPE'
LEFT join TBL_SPECODES SPC_TYPE on SPC_TYPE.SC_REFID=INVL_EXP_TYPE and SPC_TYPE.SC_TYPE='EXP_TYPE'
inner join TBL_TRANSORDERS on INVL_ORD_RECNO=ORD_RECNO
where INVL_INV_ID=@INV_ID ;
Update  TBL_INVOICELINE  set INVL_TEMP=1   where  INVL_INV_ID=@INV_ID";
        }
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        cmd.Parameters.AddWithValue("@INV_ID", INV_ID);
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        int count = 0;
        while (reader.Read())
        {
            count++;
            List_data.Add(new OInvoice
            {
                //  ROWCOUNT=count,
                ITEMDESC1 = reader["ITEMDESC1"].ToString(),
                LINE_ID = Convert.ToInt32(reader["INVL_ID"].ToString()),
                ID = Convert.ToInt32(reader["ORD_RECNO"].ToString()),
                EXPID = Convert.ToInt32(reader["EXPID"].ToString()),
                PODCODE = reader["ORD_PODCODE"].ToString(),
                FICHENO = reader["ORD_FICHENO"].ToString(),
                EXP = reader["_EXP"].ToString(),
                EXPENSE = reader["PRICE"].ToString(),
                QTY = Convert.ToDouble(reader["QTY"].ToString()),
                TOTAL = double.Parse(reader["PRICE"].ToString()) * double.Parse(reader["QTY"].ToString())
            });
        }
        return List_data;
    }

    public List<OInvoice> Get_PURCHOSEINVline(string period, int VENDOR, int INV_ID = -1)
    {
        List<OInvoice> List_data = new List<OInvoice>();
        string query = "";
        if (INV_ID == -1)
        {
            DateTime enddate = new DateTime(DateTime.Parse(period).Year, DateTime.Parse(period).Month, DateTime.DaysInMonth(DateTime.Parse(period).Year, DateTime.Parse(period).Month));

            //            query = @"
            //Declare @BeginPeriod datetime 
            //Declare @endPeriod datetime 
            //Declare @EXP_TYPE int
            //
            //set @EXP_TYPE=" + VENDOR + @"
            //set @BeginPeriod='" + period + @"'
            //set @endPeriod='" + enddate + @"'
            //
            //
            //Select '-1' INVL_ID,'' ITEMDESC1,ORD_RECNO,ORD_PODCODE,ORD_NTS,ORD_FICHENO,
            //case 
            //when SC_REFID=@EXP_TYPE then isnull(FCT_ADYTOTAL,0)
            //when SC_REFID=@EXP_TYPE then isnull(FCT_SECURITYTOTAL,0)
            //when SC_REFID=@EXP_TYPE then isnull(FCT_CASPARTOTAL,0)
            //when SC_REFID=@EXP_TYPE then isnull(FCT_CASPARTOTAL,0)
            //when SC_REFID=@EXP_TYPE then isnull(FCT_BRIDGETOTAL,0) end as Expance
            // from TBL_FACT
            //inner join TBL_TRANSORDERS on FCT_ORDID=ORD_RECNO
            //inner join TBL_SPECODES on SC_REFID=1 and SC_TYPE='EXPENCETYPE'
            //where FCT_EXDATE between @BeginPeriod and @endPeriod";

            query = @"
Declare @BeginPeriod datetime 
Declare @endPeriod datetime 
Declare @EXP_TYPE int

set @EXP_TYPE=" + VENDOR + @"
set @BeginPeriod='" + period + @"'
set @endPeriod='" + enddate + @"'

SELECT '-1' INVL_ID,'' ITEMDESC1,ORD_RECNO,ORD_PODCODE,ORD_NTS,ORD_FICHENO,ISNULL(ADY.EXP_PAMOUNT, 0) Expance

 FROM TBL_SPECODES SPC
LEFT OUTER JOIN TBL_TRANSORDEREXP ADY ON (SPC.SC_REFID=ADY.EXP_EXPTYPEID AND ISNULL(ADY.EXP_TYPE, 0)=1 )
left join TBL_TRANSORDERS ORD on ORD.ORD_RECNO=ADY.EXP_ORDRECNO
WHERE SC_TYPE='EXPENCETYPE' AND  SC_REFID=@EXP_TYPE   and  ISNULL(ADY.EXP_PAMOUNT, 0)<>0 and ORD_FICHEDATE between @BeginPeriod and @endPeriod




UNION All
SELECT '-1' INVL_ID,'' ITEMDESC1,ORD_RECNO,ORD_PODCODE,ORD_NTS,ORD_FICHENO,ISNULL(ADY.EXP_PAMOUNT, 0) Expance

 FROM TBL_SPECODES SPC
LEFT OUTER JOIN TBL_TRANSORDEREXP ADY ON (SPC.SC_REFID=ADY.EXP_EXPTYPEID AND ISNULL(ADY.EXP_TYPE, 0)=2 )
left join TBL_TRANSORDERS ORD on ORD.ORD_RECNO=ADY.EXP_ORDRECNO
WHERE SC_TYPE='EXPENCETYPE' AND  SC_REFID=@EXP_TYPE   and  ISNULL(ADY.EXP_PAMOUNT, 0)<>0 and ORD_FICHEDATE between @BeginPeriod and @endPeriod";
        }
        else if (INV_ID != -1)
        {
            query = @"Select INVL_ITEMDESC1 ITEMDESC1,INVL_ID,ORD_RECNO,ISNULL(SPC.SC_REFID,0) EXPID,ISNULL(INVL_EXP_TYPE, 0) EXP_TYPE,ORD_PODCODE,ORD_FICHENO,SPC.SC_VALUE1 _EXP,ISNULL(INVL_AMOUNT, 0) Expance,SPC_TYPE.SC_VALUE1 _STATUS,isnull(INVL_QTY,0) QTY from TBL_INVOICELINE
inner join  TBL_INVOICE on INV_ID=INVL_INV_ID
LEFT join TBL_SPECODES SPC on SC_REFID=INVL_EXPTYPEID and SPC.SC_TYPE='EXPENCETYPE'
LEFT join TBL_SPECODES SPC_TYPE on SPC_TYPE.SC_REFID=INVL_EXP_TYPE and SPC_TYPE.SC_TYPE='EXP_TYPE'
inner join TBL_TRANSORDERS on INVL_ORD_RECNO=ORD_RECNO
where INVL_INV_ID=@INV_ID ;
Update  TBL_INVOICELINE  set INVL_TEMP=1   where  INVL_INV_ID=@INV_ID";
        }
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        cmd.Parameters.AddWithValue("@INV_ID", INV_ID);
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        int count = 0;
        while (reader.Read())
        {
            count++;
            List_data.Add(new OInvoice
            {
                //  ROWCOUNT=count,
                ITEMDESC1 = reader["ITEMDESC1"].ToString(),
                LINE_ID = Convert.ToInt32(reader["INVL_ID"].ToString()),
                ID = Convert.ToInt32(reader["ORD_RECNO"].ToString()),
                PODCODE = reader["ORD_PODCODE"].ToString(),
                FICHENO = reader["ORD_FICHENO"].ToString(),
                TOTAL = double.Parse(reader["Expance"].ToString())
            });
        }
        return List_data;
    }

}