using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OInvoice
/// </summary>
public class OInvoice
{
    private int INV_ID;

    public int ID
    {
        get { return INV_ID; }
        set { INV_ID = value; }
    }
    private DateTime INV_DATE;

    public DateTime DATE
    {
        get { return INV_DATE; }
        set { INV_DATE = value; }
    }

    private string _INV_DATE;

    public string _DATE
    {
        get { return _INV_DATE; }
        set { _INV_DATE = value; }
    }

    private int INV_FRM_RECNO;

    public int FRM_RECNO
    {
        get { return INV_FRM_RECNO; }
        set { INV_FRM_RECNO = value; }
    }
    private int INV_CLC_RECNO;

    public int CLC_RECNO
    {
        get { return INV_CLC_RECNO; }
        set { INV_CLC_RECNO = value; }
    }
    private int INV_TYPE;

    public int TYPE
    {
        get { return INV_TYPE; }
        set { INV_TYPE = value; }
    }
    private string INV_NO;

    public string NO
    {
        get { return INV_NO; }
        set { INV_NO = value; }
    }
    private double INV_TOTAL;

    public double TOTAL
    {
        get { return INV_TOTAL; }
        set { INV_TOTAL = value; }
    }

    private string INV_FRM_NO;

    public string FRM_NO
    {
        get { return INV_FRM_NO; }
        set { INV_FRM_NO = value; }
    }

    private string INV_CLC_NO;

    public string CLC_NO
    {
        get { return INV_CLC_NO; }
        set { INV_CLC_NO = value; }
    }

    private string ORD_PODCODE;

    public string PODCODE
    {
        get { return ORD_PODCODE; }
        set { ORD_PODCODE = value; }
    }
    private string ORD_FICHENO;

    public string FICHENO
    {
        get { return ORD_FICHENO; }
        set { ORD_FICHENO = value; }
    }

    private string ORD_EXP;

    public string EXP
    {
        get { return ORD_EXP; }
        set { ORD_EXP = value; }
    }

    private string ADY_EXPENSE;

    public string EXPENSE
    {
        get { return ADY_EXPENSE; }
        set { ADY_EXPENSE = value; }
    }

    public string B_ID
    {
        get { return ADY_EXPENSE; }
        set { ADY_EXPENSE = value; }
    }

    private int SELECT_ROWCOUNT;

    public int ROWCOUNT
    {
        get { return SELECT_ROWCOUNT; }
        set { SELECT_ROWCOUNT = value; }
    }

    private string VAGON_STATUS;

    public string STATUS
    {
        get { return VAGON_STATUS; }
        set { VAGON_STATUS = value; }
    }

    private int SC_REFID;

    public int EXPID
    {
        get { return SC_REFID; }
        set { SC_REFID = value; }
    }

    private int INVL_ID;

    public int LINE_ID
    {
        get { return INVL_ID; }
        set { INVL_ID = value; }
    }

    private double INVL_QTY;

    public double QTY
    {
        get { return INVL_QTY; }
        set { INVL_QTY = value; }
    }

    private double INVL_EDV;

    public double L_EDV
    {
        get { return INVL_EDV; }
        set { INVL_EDV = value; }
    }

    private string INVL_ITEMDESC1;

    public string ITEMDESC1
    {
        get { return INVL_ITEMDESC1; }
        set { INVL_ITEMDESC1 = value; }
    }

    private int INV_CURR_ID;

    public int CURR_ID
    {
        get { return INV_CURR_ID; }
        set { INV_CURR_ID = value; }
    }
    

    private int INV_BANK_ID;

    public int BANK_ID
    {
        get { return INV_BANK_ID; }
        set { INV_BANK_ID = value; }
    }
    private int INV_PTYPE;

    public int PTYPE
    {
        get { return INV_PTYPE; }
        set { INV_PTYPE = value; }
    }
    private string INV_EDV;

    public string EDV
    {
        get { return INV_EDV; }
        set { INV_EDV = value; }
    }
    private string INV_NOTE;

    public string NOTE
    {
        get { return INV_NOTE; }
        set { INV_NOTE = value; }
    }

    private string INV_RATE;

    public string RATE
    {
        get { return INV_RATE; }
        set { INV_RATE = value; }
    }
    private string INV_FILE1;
    public string FILE1
    {
        get { return INV_FILE1; }
        set { INV_FILE1 = value; }
    }
    private string INV_FILE1_;
    public string FILE1_
    {
        get { return INV_FILE1_; }
        set { INV_FILE1_ = value; }
    }
    private string INV_ADD;
    public string ADD_
    {
        get { return INV_ADD; }
        set { INV_ADD = value; }
    }

    private string BCNO;
    public string BCNO_
    {
        get { return BCNO; }
        set { BCNO = value; }
    }

   private string ORDNO;
    public string ORDNO_
    {
        get { return ORDNO; }
        set { ORDNO = value; }
    }
 private string B_NAME;
    public string BRANCH
    {
        get { return B_NAME; }
        set { B_NAME = value; }
    }private string CURR_CODE_;
    public string CURR_CODE
    {
        get { return CURR_CODE_; }
        set { CURR_CODE_ = value; }
    }

    private string INVL_INV_NO_;
    public string INVL_INV_NO
    {
        get { return INVL_INV_NO_; }
        set { INVL_INV_NO_ = value; }
    }


    private string ORD_RECNO_;
    public string ORD_RECNO
    {
        get { return ORD_RECNO_; }
        set { ORD_RECNO_ = value; }
    }


}
