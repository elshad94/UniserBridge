using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Ospecode
/// </summary>
public class OBank
{
    private int BANK_ID;

    public int ID
    {
        get { return BANK_ID; }
        set { BANK_ID = value; }
    }
    private string BANK_NAME;

    public string NAME
    {
        get { return BANK_NAME; }
        set { BANK_NAME = value; }
    }
    private string BANK_VOEN;

    public string VOEN
    {
        get { return BANK_VOEN; }
        set { BANK_VOEN = value; }
    }
    private string BA_NO;

    public string NO
    {
        get { return BA_NO; }
        set { BA_NO = value; }
    }
    private string BANK_CODE;

    public string CODE
    {
        get { return BANK_CODE; }
        set { BANK_CODE = value; }
    }
    private string BANK_SWIFT;

    public string SWIFT
    {
        get { return BANK_SWIFT; }
        set { BANK_SWIFT = value; }
    }
    private string BA_IBAN;

    public string IBAN
    {
        get { return BA_IBAN; }
        set { BA_IBAN = value; }
    }

   private string BA_ACC;

    public string ACC
    {
        get { return BA_ACC; }
        set { BA_ACC = value; }
    }
}