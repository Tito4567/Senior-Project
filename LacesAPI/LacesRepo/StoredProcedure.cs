using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LacesRepo
{
    public class StoredProcedure
    {
        SqlConnection conn;
        SqlCommand cmd;

        public StoredProcedure(string connString, string procName)
        {
            conn = new SqlConnection(connString);

            cmd = new SqlCommand(procName, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 60;
        }

        public void AddInput(string name, object value, SqlDbType dataType)
        {
            SqlParameter para = new SqlParameter(name, dataType);

            para.Direction = ParameterDirection.Input;

            para.Value = value;

            cmd.Parameters.Add(para);
        }

        public SqlParameter AddOutput(string name, SqlDbType dataType)
        {
            SqlParameter para = new SqlParameter(name, dataType);

            para.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(para);

            return para;
        }

        public bool Execute()
        {
            bool result = true;

            conn.Open();

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }

                conn.Dispose();
            }

            return result;
        }
    }
}
