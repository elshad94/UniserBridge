using Project.Core.Settings;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Project.Core.DataAccess.Concrete.Ado.Net
{
    public static class AdoNetHelper
    {
        public static SqlDataReader RunQuery(string commandText, List<SqlParameter> parameters = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(commandText, OpenConnection());
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                }
                cmd.CommandTimeout = 1000;
                cmd.CommandType = commandType;
                SqlDataReader reader = cmd.ExecuteReader();
                CloseConnetion();
                cmd.Parameters.Clear();
                return reader;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public static DataTable GetData(string commandText, List<SqlParameter> parameters = null, CommandType commandType = CommandType.Text)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(commandText, OpenConnection());
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                }
                cmd.CommandTimeout = 1000;
                cmd.CommandType = commandType;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                adp.Dispose();
                cmd.Dispose();
                CloseConnetion();
                cmd.Parameters.Clear();
                return dt;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public static int Execute(string tableName, OperationType operation, string fieldName = "", int ID = 0, List<SqlParameter> parameters = null, bool transaction = false)
        {
            string query = "";
            string fields = "";
            string pfields = "";

            if (operation == OperationType.Insert)
            {
                foreach (var data in parameters)
                {
                    fields += data.ParameterName + ",";
                    pfields += "@" + data.ParameterName + ",";
                }
                fields = fields.Substring(0, fields.Length - 1);
                pfields = pfields.Substring(0, pfields.Length - 1);

                query = @"INSERT into " + tableName + "(" + fields + ")";
                query += @"VALUES (" + pfields + ");select SCOPE_IDENTITY() OID;";
            }
            else if (operation == OperationType.Update)
            {
                if (parameters != null)
                {
                    foreach (var data in parameters)
                    {
                        fields += data.ParameterName + "=@" + data.ParameterName + ",";
                    }
                    fields = fields.Substring(0, fields.Length - 1);

                    query = @"UPDATE  " + tableName +
                             " SET " + fields;
                    query += @" where (" + fieldName + "=" + ID + ");";
                }
                else
                {
                    return 0;
                }
            }


            if (operation == OperationType.Delete)
            {
                query = @"Delete FROM " + tableName + "  WHERE " + fieldName + "=" + ID;
            }

            using (SqlConnection con = new SqlConnection(AppSettings.Settings.ProjectAppDbConnectionModel.ToString()))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    try
                    {
                        if (transaction)
                        {
                            command.Transaction = con.BeginTransaction();

                            foreach (var data in parameters)
                            {
                                command.Parameters.AddWithValue(data.ParameterName, data.Value);
                            }

                            var sqlreader = command.ExecuteReader();

                            if (sqlreader.Read())
                            {
                                ID = int.Parse(sqlreader["OID"].ToString());
                            }

                            sqlreader.Close();
                            command.Transaction.Commit();
                            command.Connection.Close();
                            command.Dispose();
                        }
                        else
                        {
                            if (operation != OperationType.Delete)
                            {
                                foreach (var data in parameters)
                                {
                                    command.Parameters.AddWithValue(data.ParameterName, data.Value);
                                }
                            }

                            var sqlreader = command.ExecuteReader();

                            if (sqlreader.Read())
                            {
                                ID = int.Parse(sqlreader["OID"].ToString());
                            }
                            sqlreader.Close();
                            command.Connection.Close();
                            command.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (transaction)
                            command.Transaction.Rollback();
                    }
                }
                con.Close();
            }

            return ID;
        }


        public static SqlConnection OpenConnection()
        {
            SqlConnection con = new SqlConnection(AppSettings.Settings.ProjectAppDbConnectionModel.ToString());
            SqlConnection.ClearAllPools();
            con.Open();
            return con;
        }


        public static void CloseConnetion()
        {
            SqlConnection con = new SqlConnection(AppSettings.Settings.ProjectAppDbConnectionModel.ToString());
            con.Close();
        }
    }
}
