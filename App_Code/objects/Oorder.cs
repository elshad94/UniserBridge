using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Oorder
/// </summary>
public class Oorder
{
    private string ORD_WIDTH;
    public string REF_FICHENO;

    public string ORD_ETA { get; set; }

    public string WIDTH
    {
        get { return ORD_WIDTH; }
        set { ORD_WIDTH = value; }
    }

    private string ORD_LENGTH;

    public string LENGTH
    {
        get { return ORD_LENGTH; }
        set { ORD_LENGTH = value; }
    }

    private string ORD_HEIGHT;

    public string HEIGHT
    {
        get { return ORD_HEIGHT; }
        set { ORD_HEIGHT = value; }
    }



    private string ORD_CONTWEIGHT;

    public string CONTWEIGHT
    {
        get { return ORD_CONTWEIGHT; }
        set { ORD_CONTWEIGHT = value; }
    }


    private string ORD_RECNO;

    public string RECNO
    {
        get { return ORD_RECNO; }
        set { ORD_RECNO = value; }
    }
    private string ORD_PODCODE;

    public string PODCODE
    {
        get { return ORD_PODCODE; }
        set { ORD_PODCODE = value; }
    }
    private string ORD_NTS;

    public string NTS
    {
        get { return ORD_NTS; }
        set { ORD_NTS = value; }
    }
    private string ORD_FICHENO;

    public string FICHENO
    {
        get { return ORD_FICHENO; }
        set { ORD_FICHENO = value; }
    }
    private string ORD_FICHEDATE;

    public string FICHEDATE
    {
        get { return ORD_FICHEDATE; }
        set { ORD_FICHEDATE = value; }
    }
    private string ORD_PNTROUTE;

    public string PNTROUTE
    {
        get { return ORD_PNTROUTE; }
        set { ORD_PNTROUTE = value; }
    }
    private string STN_NAME;

    public string NAME
    {
        get { return STN_NAME; }
        set { STN_NAME = value; }
    }
    private string ORD_VAGONCOUNT;

    public string VAGONCOUNT
    {
        get { return ORD_VAGONCOUNT; }
        set { ORD_VAGONCOUNT = value; }
    }
    private string STC_NAME1;

    public string NAME1
    {
        get { return STC_NAME1; }
        set { STC_NAME1 = value; }
    }
    private string STC_NAME2;

    public string NAME2
    {
        get { return STC_NAME2; }
        set { STC_NAME2 = value; }
    }
    private string ORD_LOADAMOUNT;

    public string LOADAMOUNT
    {
        get { return ORD_LOADAMOUNT; }
        set { ORD_LOADAMOUNT = value; }
    }
    private string ORD_STATUS;

    public string STATUS
    {
        get { return ORD_STATUS; }
        set { ORD_STATUS = value; }
    }
    private string ORD_STATUSN;

    public string STATUSN
    {
        get { return ORD_STATUSN; }
        set { ORD_STATUSN = value; }
    }

    private string ORD_CLCRECNON;

    public string CLCRECNON
    {
        get { return ORD_CLCRECNON; }
        set { ORD_CLCRECNON = value; }
    }

    private string ORD_TELEGRAM;

    public string TELEGRAM
    {
        get { return ORD_TELEGRAM; }
        set { ORD_TELEGRAM = value; }
    }

    private string ORD_VGALLTONNAJ;

    public string VGALLTONNAJ
    {
        get { return ORD_VGALLTONNAJ; }
        set { ORD_VGALLTONNAJ = value; }
    }

    private string SC_REFID;

    public string REFID
    {
        get { return SC_REFID; }
        set { SC_REFID = value; }
    }

    private string SC_VALUE;

    public string VALUE
    {
        get { return SC_VALUE; }
        set { SC_VALUE = value; }
    }

    private string FEXP_EXPENSE;

    public string F_EXPENSE
    {
        get { return FEXP_EXPENSE; }
        set { FEXP_EXPENSE = value; }
    }

    private string EEXP_EXPENSE;

    public string E_EXPENSE
    {
        get { return EEXP_EXPENSE; }
        set { EEXP_EXPENSE = value; }
    }




    private string ORD_FIRM;

    public string FIRM
    {
        get { return ORD_FIRM; }
        set { ORD_FIRM = value; }
    }
    private string ORD_FCLIENT;

    public string FCLIENT
    {
        get { return ORD_FCLIENT; }
        set { ORD_FCLIENT = value; }
    }
    private string FPNT_CODE;

    public string F_CODE
    {
        get { return FPNT_CODE; }
        set { FPNT_CODE = value; }
    }
    private string FPNT_NAME;

    public string F_NAME
    {
        get { return FPNT_NAME; }
        set { FPNT_NAME = value; }
    }
    private string ORD_TCLIENT;

    public string TCLIENT
    {
        get { return ORD_TCLIENT; }
        set { ORD_TCLIENT = value; }
    }
    private string TPNT_CODE;

    public string T_CODE
    {
        get { return TPNT_CODE; }
        set { TPNT_CODE = value; }
    }
    private string TPNT_NAME;

    public string T_NAME
    {
        get { return TPNT_NAME; }
        set { TPNT_NAME = value; }
    }
    private string STC_CODE1;

    public string S_CODE1
    {
        get { return STC_CODE1; }
        set { STC_CODE1 = value; }
    }
    private string STC_CODE2;

    public string S_CODE2
    {
        get { return STC_CODE2; }
        set { STC_CODE2 = value; }
    }
    private string ORD_LOADNOTE;

    public string LOADNOTE
    {
        get { return ORD_LOADNOTE; }
        set { ORD_LOADNOTE = value; }
    }
    private string ORD_VGNTRNTYPE;

    public string VGNTRNTYPE
    {
        get { return ORD_VGNTRNTYPE; }
        set { ORD_VGNTRNTYPE = value; }
    }
    private string ORD_VAGONOWNER;

    public string VAGONOWNER
    {
        get { return ORD_VAGONOWNER; }
        set { ORD_VAGONOWNER = value; }
    }
    private string ORD_VAGONOWNERSTN;

    public string VAGONOWNERSTN
    {
        get { return ORD_VAGONOWNERSTN; }
        set { ORD_VAGONOWNERSTN = value; }
    }
    private string ORD_VAGONTYPE;

    public string VAGONTYPE
    {
        get { return ORD_VAGONTYPE; }
        set { ORD_VAGONTYPE = value; }
    }
    private string ORD_VAGONTONNAJ;

    public string VAGONTONNAJ
    {
        get { return ORD_VAGONTONNAJ; }
        set { ORD_VAGONTONNAJ = value; }
    }

    private string ORD_VGALLCOUNT;

    public string VGALLCOUNT
    {
        get { return ORD_VGALLCOUNT; }
        set { ORD_VGALLCOUNT = value; }
    }
    private string ORD_CONTAINAMOUNT3;

    public string CONTAINAMOUNT3
    {
        get { return ORD_CONTAINAMOUNT3; }
        set { ORD_CONTAINAMOUNT3 = value; }
    }
    private string ORD_CONTAINAMOUNT5;

    public string CONTAINAMOUNT5
    {
        get { return ORD_CONTAINAMOUNT5; }
        set { ORD_CONTAINAMOUNT5 = value; }
    }
    private string ORD_CONTAINAMOUNT10;

    public string CONTAINAMOUNT10
    {
        get { return ORD_CONTAINAMOUNT10; }
        set { ORD_CONTAINAMOUNT10 = value; }
    }
    private string ORD_CONTAINAMOUNT20;

    public string CONTAINAMOUNT20
    {
        get { return ORD_CONTAINAMOUNT20; }
        set { ORD_CONTAINAMOUNT20 = value; }
    }
    private string ORD_CONTAINAMOUNT30;

    public string CONTAINAMOUNT30
    {
        get { return ORD_CONTAINAMOUNT30; }
        set { ORD_CONTAINAMOUNT30 = value; }
    }
    private string ORD_CONTAINAMOUNT40;

    public string CONTAINAMOUNT40
    {
        get { return ORD_CONTAINAMOUNT40; }
        set { ORD_CONTAINAMOUNT40 = value; }
    }
    private string ORD_ECONTAINAMOUNT3;

    public string ECONTAINAMOUNT3
    {
        get { return ORD_ECONTAINAMOUNT3; }
        set { ORD_ECONTAINAMOUNT3 = value; }
    }
    private string ORD_ECONTAINAMOUNT5;

    public string ECONTAINAMOUNT5
    {
        get { return ORD_ECONTAINAMOUNT5; }
        set { ORD_ECONTAINAMOUNT5 = value; }
    }
    private string ORD_ECONTAINAMOUNT10;

    public string ECONTAINAMOUNT10
    {
        get { return ORD_ECONTAINAMOUNT10; }
        set { ORD_ECONTAINAMOUNT10 = value; }
    }
    private string ORD_ECONTAINAMOUNT20;

    public string ECONTAINAMOUNT20
    {
        get { return ORD_ECONTAINAMOUNT20; }
        set { ORD_ECONTAINAMOUNT20 = value; }
    }
    private string ORD_ECONTAINAMOUNT30;

    public string ECONTAINAMOUNT30
    {
        get { return ORD_ECONTAINAMOUNT30; }
        set { ORD_ECONTAINAMOUNT30 = value; }
    }
    private string ORD_ECONTAINAMOUNT40;

    public string ECONTAINAMOUNT40
    {
        get { return ORD_ECONTAINAMOUNT40; }
        set { ORD_ECONTAINAMOUNT40 = value; }
    }
    private string ORD_CONTAINTYPE;

    public string CONTAINTYPE
    {
        get { return ORD_CONTAINTYPE; }
        set { ORD_CONTAINTYPE = value; }
    }
    private string ORD_VAGONNOTE;

    public string VAGONNOTE
    {
        get { return ORD_VAGONNOTE; }
        set { ORD_VAGONNOTE = value; }
    }
    private string ORD_NOTE;

    public string NOTE
    {
        get { return ORD_NOTE; }
        set { ORD_NOTE = value; }
    }

    private int ORD_FPOINT;

    public int FPOINT
    {
        get { return ORD_FPOINT; }
        set { ORD_FPOINT = value; }
    }
    private int ORD_TPOINT;

    public int TPOINT
    {
        get { return ORD_TPOINT; }
        set { ORD_TPOINT = value; }
    }
    private int TPNT_RECNO;

    public int T_RECNO
    {
        get { return TPNT_RECNO; }
        set { TPNT_RECNO = value; }
    }
    private int STC_ID1;

    public int S_ID1
    {
        get { return STC_ID1; }
        set { STC_ID1 = value; }
    }
    private int STC_ID2;

    public int S_ID2
    {
        get { return STC_ID2; }
        set { STC_ID2 = value; }
    }

    private string ORD_BEGPOINT;

    public string BEGPOINT
    {
        get { return ORD_BEGPOINT; }
        set { ORD_BEGPOINT = value; }
    }

    private string ORD_ENDPOINT;

    public string ENDPOINT
    {
        get { return ORD_ENDPOINT; }
        set { ORD_ENDPOINT = value; }
    }

    private string ADY_EXPENSE;

    public string A_EXPENSE
    {
        get { return ADY_EXPENSE; }
        set { ADY_EXPENSE = value; }
    }
    private string ADY_PEXPENSE;

    public string P_EXPENSE
    {
        get { return ADY_PEXPENSE; }
        set { ADY_PEXPENSE = value; }
    }
    private double _P_AMOUNT;

    public double P_AMOUNT
    {
        get { return _P_AMOUNT; }
        set { _P_AMOUNT = value; }
    }

    private string ORD_EXPEDITOR;

    public string EXPEDITOR
    {
        get { return ORD_EXPEDITOR; }
        set { ORD_EXPEDITOR = value; }
    }

    private string ORD_EXPCLIENT;

    public string EXPCLIENT
    {
        get { return ORD_EXPCLIENT; }
        set { ORD_EXPCLIENT = value; }
    }

    private string A_QTY;

    public string QTY
    {
        get { return A_QTY; }
        set { A_QTY = value; }
    }

    private float A_AMOUNT;

    public float AMOUNT
    {
        get { return A_AMOUNT; }
        set { A_AMOUNT = value; }
    }
    private int ORD_CLC_PTYPE;

    public int CLC_PTYPE
    {
        get { return ORD_CLC_PTYPE; }
        set { ORD_CLC_PTYPE = value; }
    }

    private int ORD_DISTANCE;

    public int DISTANCE
    {
        get { return ORD_DISTANCE; }
        set { ORD_DISTANCE = value; }
    }

    private bool ORD_ISQT;

    public bool ISQT
    {
        get { return ORD_ISQT; }
        set { ORD_ISQT = value; }
    }

    private string ORD_ACTTYPE;

    public string ACTTYPE
    {
        get { return ORD_ACTTYPE; }
        set { ORD_ACTTYPE = value; }
    }

    private double ORD_TOTAL;

    public double TOTAL
    {
        get { return ORD_TOTAL; }
        set { ORD_TOTAL = value; }
    }

    private string ORD_FILE1;

    public string FILE1
    {
        get { return ORD_FILE1; }
        set { ORD_FILE1 = value; }
    }

private string ORD_FILE1_;

    public string FILE1_
    {
        get { return ORD_FILE1_; }
        set { ORD_FILE1_ = value; }
    }

  private DateTime ORD_DATE;

    public DateTime DATE
    {
        get { return ORD_DATE; }
        set { ORD_DATE = value; }
    }
    private string ORD_USERNAME;

    public string USERNAME
    {
        get { return ORD_USERNAME; }
        set { ORD_USERNAME = value; }
    }

  private string EXP_P_FORMULA;

    public string P_FORMULA
    {
        get { return EXP_P_FORMULA; }
        set { EXP_P_FORMULA = value; }
    }

    private string EXP_S_FORMULA;

    public string S_FORMULA
    {
        get { return EXP_S_FORMULA; }
        set { EXP_S_FORMULA = value; }
    }


    private double ORD_PTOTAL;

    public double PTOTAL
    {
        get { return ORD_PTOTAL; }
        set { ORD_PTOTAL = value; }
    }

    private double ORD_PROFIT;

    public double PROFIT
    {
        get { return ORD_PROFIT; }
        set { ORD_PROFIT = value; }
    }

    private double ORD_BORC;

    public double BORC
    {
        get { return ORD_BORC; }
        set { ORD_BORC = value; }
    }

    private double ORD_BORC_ORDER;

    public double BORC_ORDER
    {
        get { return ORD_BORC_ORDER; }
        set { ORD_BORC_ORDER = value; }
    }

    private string ORD_ORDERLINK;

    public string ORDERLINK
    {
        get { return ORD_ORDERLINK; }
        set { ORD_ORDERLINK = value; }
    }

    private double ORD_BORC_ORDER_KR;

    public double BORC_ORDER_KR
    {
        get { return ORD_BORC_ORDER_KR; }
        set { ORD_BORC_ORDER_KR = value; }
    }

    private double ORD_BORC_ORDER_MM;

    public double BORC_ORDER_MM
    {
        get { return ORD_BORC_ORDER_MM; }
        set { ORD_BORC_ORDER_MM = value; }
    }

    private double FAKT_TOTAL;
    public double FAKTTOTAL
    {
        get { return FAKT_TOTAL; }
        set { FAKT_TOTAL = value; }
    }

    private string FAKT_DESC;
    public string FAKTDESC
    {
        get { return FAKT_DESC; }
        set { FAKT_DESC = value; }
    }

    private double INV_PAYMENT;
    public double INVPAYMENT
    {
        get { return INV_PAYMENT; }
        set { INV_PAYMENT = value; }
    }

    private double BORC_ORDER_FB;
    public double ORDER_FB
    {
        get { return BORC_ORDER_FB; }
        set { BORC_ORDER_FB = value; }
    }

    private float EXPENSE_H;
    public float EXPENSEH
    {
        get { return EXPENSE_H; }
        set { EXPENSE_H = value; }
    }

private string FPOINT_1;
    public string FPOINT1
    {
        get { return FPOINT_1; }
        set { FPOINT_1= value; }
    }

    private string TPOINT_1;
    public string TPOINT1
    {
        get { return TPOINT_1; }
        set { TPOINT_1 = value; }
    }

    private string BPOINT_;
    public string BPOINT
    {
        get { return BPOINT_; }
        set { BPOINT_ = value; }
    }

    private string EPOINT_;
    public string EPOINT
    {
        get { return EPOINT_; }
        set { EPOINT_ = value; }
    }
}