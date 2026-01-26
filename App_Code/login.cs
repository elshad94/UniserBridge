using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for log
/// </summary>
public class login
{



    public bool log(string email, string password)
    {
        conn con = new conn();
        global glb = new global();
        string query = "";
        query = @"Select * from T_SYS_USER WHERE U_STATUS=0 and (U_EMAIL=@Username or U_USERNAME=@Username) and
       ((case when isnull(U_CHANGEPASS,0)=1 then U_PASS else U_PASS2 end)=@password
        OR
        (case when isnull(U_CHANGEPASS,0)=1 then U_PASS else U_PASS2 end)=@password2)";
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, con.connectionOpen());
        cmd.Parameters.AddWithValue("@Username", email);
        cmd.Parameters.AddWithValue("@password", password);
        cmd.Parameters.AddWithValue("@password2", glb.sha256_hash(password));
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            Set_Session(Convert.ToInt32(reader["U_ID"]));
            Set_USERNAME(reader["U_USERNAME"].ToString());
            Set_Usertype(Convert.ToInt32(reader["U_TYPE"]));
            return true;

        }
        return false;
        con.ConnetionClose();
    }

    public void Set_Session(int user_id)
    {

        System.Web.HttpContext.Current.Session["user_id"] = user_id;
    }
    public int Get_userid()
    {
        return Convert.ToInt32(System.Web.HttpContext.Current.Session["user_id"]);
    }
    public void Session_remove()
    {
        System.Web.HttpContext.Current.Session.RemoveAll(); ;
    }
    public void Set_USERNAME(string user_name)
    {

        System.Web.HttpContext.Current.Session["user_name"] = user_name;
    }
    public string GET_USERNAME()
    {
        try
        {
            return System.Web.HttpContext.Current.Session["user_name"].ToString();
        }
        catch
        {
            return "";
        }
    }


    public void Set_Usertype(int type)
    {
        System.Web.HttpContext.Current.Session["type"] = type;
    }
    public int Get_Usertype()
    {
        return Convert.ToInt32(System.Web.HttpContext.Current.Session["type"]);
    }

    public void Set_Lang(int lang)
    {
        System.Web.HttpContext.Current.Session["lg"] = lang;
    }
    public int Get_Lang()
    {
        try
        {
            return Convert.ToInt16(System.Web.HttpContext.Current.Session["lg"].ToString());

        }
        catch
        {
            return 1;
        }
    }

}