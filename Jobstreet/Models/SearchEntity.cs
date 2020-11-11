using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Jobstreet.Models
{
    public class SearchEntity
    {
        SqlConnection _sqlConnection;
        public SearchEntity(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }
        public DataTable Search(string KeyWord, int CityID)
        {
            string query = "";
            SqlDataAdapter dap;
            if (CityID == 0)
            {
                query = "SELECT tblJobs.*, CONCAT(SUBSTRING(tblJobs.JobDesc, 1, 199), '...') AS JDesc, FORMAT(tblJobs.CreatedDate, 'dd-MM-yyyy') as date, " +
                    "tblCities.CityName as CityName FROM tblJobs INNER JOIN tblCities ON " +
                    "tblJobs.CityID = tblCities.CityID WHERE (tblJobs.JobTitle LIKE @keyword OR " +
                    "tblJobs.JobDesc LIKE @keyword OR tblJobs.JobDuties LIKE @keyword) AND JobStatus = 1 ORDER BY JobID DESC";
                dap = new SqlDataAdapter(query, _sqlConnection);
            } else
            {
                query = "SELECT tblJobs.*, CONCAT(SUBSTRING(tblJobs.JobDesc, 1, 199), '...') AS JDesc, FORMAT(tblJobs.CreatedDate, 'dd-MM-yyyy') as date, " +
                    "tblCities.CityName as CityName FROM tblJobs INNER JOIN tblCities ON " +
                    "tblJobs.CityID = tblCities.CityID WHERE (tblJobs.JobTitle LIKE @keyword OR " +
                    "tblJobs.JobDesc LIKE @keyword OR JobDuties LIKE @keyword) AND tblJobs.CityID = @city_id AND JobStatus = 1 ORDER BY JobID DESC";
                dap = new SqlDataAdapter(query, _sqlConnection);
                dap.SelectCommand.Parameters.AddWithValue("@city_id", CityID);
            }
            dap.SelectCommand.Parameters.AddWithValue("@keyword", "%"+KeyWord+"%");
            DataTable dt = new DataTable();
            dap.Fill(dt);            
            return dt;
        }
    }
}