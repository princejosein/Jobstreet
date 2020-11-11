using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Threading;

namespace Jobstreet.Models
{
    public class DataAccessLayer
    {
        SqlConnection conn = null;
        string connString = "";
        public DataAccessLayer(string connectionString)
        {
            connString = connectionString;
        }
        public bool IsConnected
        {
            get
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
        }

        public SqlConnection sqlConnection
        {
            get { return conn; }

        }

        public string ConnectionString
        {
            set
            {
                connString = value;
            }
        }


        public bool Connect()
        {
            connString = Jobstreet.Properties.Settings.Default["JobStreetDatabase"].ToString().Trim();
            conn = new SqlConnection(connString);
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        return true;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
            return false;
        }

        public void Close()
        {
            try
            {
                conn.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
        }
        public DataTable GetCities()
        {
            string query = "SELECT * FROM tblCities";
            SqlDataAdapter dap = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            dap.Dispose();
            return dt;
        }
    }
}