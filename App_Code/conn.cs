using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for conn
/// </summary>
public class conn
{
     private string connection_string = @"Data Source=192.168.37.33;Initial Catalog=IMSART_CONT_V2;Persist Security Info=True;User ID=sa;Password=Uniser!@#";

    public string Connection_string
    {
        get { return connection_string; }

    }

    public SqlConnection connectionOpen()
    {
        SqlConnection con = new SqlConnection(Connection_string);
        SqlConnection.ClearAllPools();
        SqlDependency.Start(connection_string);
        con.Open();
        return con;
    }

    public void ConnetionClose()
    {
        SqlConnection con = new SqlConnection(Connection_string);
        con.Close();

    }

    public string connectionState()
    {
        SqlConnection con = new SqlConnection(Connection_string);
        return con.State.ToString();
    }

    public DataTable dbRunDt(string sql, string con_string = "")
    {
        //  Set_QueryLog(sql);
        if (con_string == "")
        {
            con_string = connection_string;
        }
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(connection_string))
        {
            var cmd = new SqlCommand(sql, con);
            cmd.Connection.Open();
            cmd.CommandTimeout = 180;
            var sqlReader = cmd.ExecuteReader();
            dt.Load(sqlReader);
            sqlReader.Close();
            cmd.Connection.Close();
            cmd.Dispose();
            con.Close();
        }
        return dt;
    }

    public System.Data.SqlClient.SqlDataReader dbrun(string sql)
    {
        try
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, connectionOpen());
            cmd.CommandTimeout = 1000;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            ConnetionClose();

            return reader;
        }
        catch(Exception ex)
        {
           //Mail mn = new Mail();
           //mn.send_mail("elshad@uniser.az", "sa", "error",ex.ToString()+"##" + sql);
           //mn.send_mail("shalala@uniser.az", "sa", "error", ex.ToString() + "##" + sql);
            return null;
        }
    }
    public static System.Data.SqlClient.SqlDataReader fnc_dbrun(string p_sql, SqlConnection p_conn)
    {
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(p_sql, p_conn);
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static System.Data.SqlClient.SqlDataReader fnc_dbrun(string p_sql)
    {
        conn v_con = new conn();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(p_sql, v_con.connectionOpen());
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        v_con.ConnetionClose();
        return reader;
    }

    public static int fnc_dbexecute(string p_sql, SqlConnection p_conn)
    {
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(p_sql, p_conn);
        int reader = cmd.ExecuteNonQuery();
        return reader;
    }

    public static int fnc_dbexecute(string p_sql)
    {
        conn v_con = new conn();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(p_sql, v_con.connectionOpen());
        int reader = cmd.ExecuteNonQuery();
        v_con.ConnetionClose();
        return reader;
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
}