using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for Extension
/// </summary>
public static class Extension
{
    public static double ROUND2(this double ins)
    {
        return Math.Round(ins, 2);
    }
    public static string  ROUNDAS00(this double ins)
    {
        return String.Format("{0:0.00}", ins); 
    }


    public static string SentenceCase(this string ins)
    {
        var sourcestring = ins;
        var lowerCase = sourcestring.ToLower();
        var r = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture);
        var result = r.Replace(lowerCase, s => s.Value.ToUpper());
        return result;
    }

    public static string STRINGROUNDAS00( this string ins)
    {
        double number = Convert.ToDouble(ins);
        return String.Format("{0:0.00}", number);
    }
}