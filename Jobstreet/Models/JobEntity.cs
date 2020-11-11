using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Jobstreet.Models
{
    public class JobEntity
    {
        SqlConnection _sqlConnection;
        public JobEntity(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }
        public DataTable GetExpList()
        {
            string query = "SELECT * FROM tblExperiences";
            SqlDataAdapter dap = new SqlDataAdapter(query, _sqlConnection);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            dap.Dispose();
            return dt;
        }
        public int CreateOrUpdateJob(
            string JobTitle, string JobDesc, string JobDuties, int Status,
            int Experience, int City, int UserID, int JobID = 0
            )
        {
            int EmployerID = GetEmployerIDFromUser(UserID);
            string query = "";
            if(JobID == 0)
            {
                query = "INSERT INTO tblJobs VALUES(@employer_id, @job_title," +
                "@experience, @job_desc, @job_duties, @job_status, @created_date," +
                "@city_id);SELECT CAST(scope_identity() AS int)";
            } else
            {
                query = "UPDATE tblJobs SET EmployerID = @employer_id, JobTitle = @job_title, " +
                    "Experience = @experience, JobDesc = @job_desc, JobDuties = @job_duties, " +
                    "JobStatus = @job_status, CityID = @city_id " +
                    "WHERE JobID = @job_id";
            }
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            if (JobID != 0)
            {
                cmd.Parameters.AddWithValue("@job_id", JobID);
            } else
            {
                cmd.Parameters.AddWithValue("@created_date", DateTime.Today);
            }
            cmd.Parameters.AddWithValue("@employer_id", EmployerID);
            cmd.Parameters.AddWithValue("@job_title", JobTitle);
            cmd.Parameters.AddWithValue("@experience", Experience);
            cmd.Parameters.AddWithValue("@job_desc", JobDesc);
            cmd.Parameters.AddWithValue("@job_duties", JobDuties);
            cmd.Parameters.AddWithValue("@job_status", Status);
            cmd.Parameters.AddWithValue("@city_id", City);
            int ID = 0;
            if(JobID == 0)
            {
                try
                {
                    ID = (int)cmd.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } else
            {
                try
                {
                    ID = (int)cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            
            return ID;
        }
        private int GetEmployerIDFromUser(int UserID)
        {
            string query = "SELECT EmployerID FROM tblEmloyers WHERE UserID = @user_id";
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.Parameters.AddWithValue("@user_id", UserID);
            int EmployerID = 0;
            try
            {
                EmployerID = (int)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return EmployerID;
        }
        public DataTable GetJobByJobID(int JobID)
        {
            DataTable dt = new DataTable();
            string query = "SELECT TOP 1 * FROM tblJobs WHERE JobID = @job_id";
            SqlDataAdapter dap = new SqlDataAdapter(query, _sqlConnection);
            dap.SelectCommand.Parameters.AddWithValue("@job_id", JobID);
            dap.Fill(dt);
            return dt;
        }
        public DataTable  GetJobsByUserID(int UserID)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM tblJobs WHERE EmployerID = @employer_id";
            SqlDataAdapter dap = new SqlDataAdapter(query, _sqlConnection);
            int EmployerID = GetEmployerIDFromUserID(UserID);
            dap.SelectCommand.Parameters.AddWithValue("@employer_id", EmployerID);
            dap.Fill(dt);
            return dt;
        }
        public int GetEmployerIDFromUserID(int UserID)
        {
            string query = "SELECT EmployerID FROM tblEmloyers WHERE UserID = @user_id";
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.Parameters.AddWithValue("@user_id", UserID);
            int EmployerID = 0;
            try
            {
                EmployerID = (int)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return EmployerID;
        }
        public DataTable GetJobList(int EmployerID)
        {
            DataTable dt = new DataTable();
            string query = "SELECT tblJobs.*, CONCAT(SUBSTRING (tblJobs.JobDesc, 1, 199),'...') AS JDesc, CASE WHEN tblJobs.JobStatus = 1 THEN 'Active' ELSE 'Expired' END AS Status  , FORMAT (tblJobs.CreatedDate, 'dd-MM-yyyy') as date, tblCities.CityName as CityName FROM tblJobs" +
                " INNER JOIN tblCities ON tblJobs.CityID = tblCities.CityID WHERE " +
                "tblJobs.EmployerID = @employer_id";
            SqlDataAdapter dap = new SqlDataAdapter(query, _sqlConnection);
            dap.SelectCommand.Parameters.AddWithValue("@employer_id", EmployerID);
            dap.Fill(dt);
            return dt;
        }
        public DataTable GetJob(int JobID)
        {
            DataTable dt = new DataTable();
            string query = "SELECT TOP 1 tblJobs.*, CASE WHEN tblJobs.JobStatus = 1 THEN 'Active' ELSE 'Expired' END AS Status  , FORMAT (tblJobs.CreatedDate, 'dd-MM-yyyy') as date, tblCities.CityName as CityName, " +
                "tblEmloyers.CompanyName as CompanyName, tblEmloyers.CompanyLogo as CompanyLogo FROM tblJobs" +
                " INNER JOIN tblCities ON tblJobs.CityID = tblCities.CityID " +
                " INNER JOIN tblEmloyers ON tblEmloyers.EmployerID = tblJobs.EmployerID WHERE " +
                "tblJobs.JobID = @job_id";
            SqlDataAdapter dap = new SqlDataAdapter(query, _sqlConnection);
            dap.SelectCommand.Parameters.AddWithValue("@job_id", JobID);
            dap.Fill(dt);
            return dt;
        }
        public int SaveJobByUser(int UserID, int JobID)
        {
            string query = "";
            query = "INSERT INTO tblSavedJobs VALUES(@job_id, @user_id, @saved_date);" +
                "SELECT CAST(scope_identity() AS int)";
            
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.Parameters.AddWithValue("@saved_date", DateTime.Today);
            cmd.Parameters.AddWithValue("@job_id", JobID);
            cmd.Parameters.AddWithValue("@user_id", UserID);
            int ID = 0;
            try
            {
                ID = (int)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ID;
        }
        public int ApplyJobByUser(int UserID, int JobID)
        {
            string query = "";
            query = "INSERT INTO tblJobApply VALUES(@job_id, @user_id, @application_status, @applied_date);" +
                "SELECT CAST(scope_identity() AS int)";

            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.Parameters.AddWithValue("@applied_date", DateTime.Today);
            cmd.Parameters.AddWithValue("@job_id", JobID);
            cmd.Parameters.AddWithValue("@user_id", UserID);
            cmd.Parameters.AddWithValue("@application_status", 1);
            int ID = 0;
            try
            {
                ID = (int)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ID;
        }
        public int[] GetJobStatusOfUser(int JobID, int UserID)
        {
            int[] Result = new int[2] { 0,0};
            DataTable dt = new DataTable();
            string query = "SELECT TOP 1 * FROM tblSavedJobs " +
                "WHERE tblSavedJobs.JobID = @job_id AND tblSavedJobs.UserID = @user_id";
            SqlDataAdapter dap = new SqlDataAdapter(query, _sqlConnection);
            dap.SelectCommand.Parameters.AddWithValue("@job_id", JobID);
            dap.SelectCommand.Parameters.AddWithValue("@user_id", UserID);
            dap.Fill(dt);
            if(dt.Rows.Count != 0)
            {
                Result[0] = 1;
            }
            DataTable dt1 = new DataTable();
            query = "SELECT TOP 1 * FROM tblJobApply " +
                "WHERE tblJobApply.JobID = @job_id AND tblJobApply.UserID = @user_id";
            SqlDataAdapter dap1 = new SqlDataAdapter(query, _sqlConnection);
            dap1.SelectCommand.Parameters.AddWithValue("@job_id", JobID);
            dap1.SelectCommand.Parameters.AddWithValue("@user_id", UserID);
            dap1.Fill(dt1);
            if (dt1.Rows.Count != 0)
            {
                Result[1] = 1;
            }
            return Result;
        }
        public DataTable GetAppliedJobs(int UserID)
        {
            DataTable dt = new DataTable();
            string query = "SELECT tblJobApply.*,CASE WHEN tblJobApply.ApplicationStatus = 1 THEN 'Application in Progress' WHEN " +
                "tblJobApply.ApplicationStatus = 2 THEN 'Application Accepted' ELSE 'Application Rejected' END AS AppStatus , CASE WHEN tblJobs.JobStatus = 1 THEN 'Active' ELSE 'Expired' END AS Status, tblJobs.*, FORMAT (tblJobs.CreatedDate, 'dd-MM-yyyy') as date, FORMAT (tblJobApply.AppliedDate, 'dd-MM-yyyy') as appDate,  tblCities.CityName as CityName FROM  tblJobApply " +
                "LEFT JOIN tblJobs ON tblJobs.JobID = tblJobApply.JobID " +
                "LEFT JOIN tblCities ON tblCities.CityID = tblJobs.CityID " +
                "WHERE UserID = @user_id";
            SqlDataAdapter dap = new SqlDataAdapter(query, _sqlConnection);
            dap.SelectCommand.Parameters.AddWithValue("@user_id", UserID);
            dap.Fill(dt);
            return dt;
        }
        public DataTable GetSavedJobs(int UserID)
        {
            DataTable dt = new DataTable();
            string query = "SELECT tblSavedJobs.*, CASE WHEN tblJobs.JobStatus = 1 THEN 'Active' ELSE 'Expired' END AS Status, tblJobs.*, FORMAT (tblJobs.CreatedDate, 'dd-MM-yyyy') as date, FORMAT (tblSavedJobs.SavedDate, 'dd-MM-yyyy') as SaveDate,  tblCities.CityName as CityName FROM  tblSavedJobs " +
                "LEFT JOIN tblJobs ON tblJobs.JobID = tblSavedJobs.JobID " +
                "LEFT JOIN tblCities ON tblCities.CityID = tblJobs.CityID " +
                "WHERE UserID = @user_id";
            SqlDataAdapter dap = new SqlDataAdapter(query, _sqlConnection);
            dap.SelectCommand.Parameters.AddWithValue("@user_id", UserID);
            dap.Fill(dt);
            return dt;
        }
        public int RemoveSavedJob(int JobID, int UserID)
        {
            string query = "DELETE FROM tblSavedJobs WHERE UserID = @user_id AND JobID = @job_id";
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.Parameters.AddWithValue("@user_id", UserID);
            cmd.Parameters.AddWithValue("@job_id", JobID);
            int Result = 0;
            try
            {
                Result = (int)cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Result;
        }
        public DataTable GetJobApplicationsByEmployerID(int EmployerID)
        {
            DataTable dt = new DataTable();
            string query = "SELECT tblJobApply.*,tblJobs.*, tblUsers.* FROM  tblJobApply " +
                "INNER JOIN tblJobs ON tblJobs.JobID = tblJobApply.JobID INNER JOIN tblUsers ON tblUsers.UserID = tblJobApply.UserID " +
                "WHERE tblJobs.EmployerID = @employer_id";
            SqlDataAdapter dap = new SqlDataAdapter(query, _sqlConnection);
            dap.SelectCommand.Parameters.AddWithValue("@employer_id", EmployerID);
            dap.Fill(dt);
            return dt;
        }
        public int ApproveApplication(int JobApplyID, int AppStatus)
        {
            string query = "UPDATE tblJobApply SET ApplicationStatus = @application_status " +
                    "WHERE JobApplyID = @job_apply_id";
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.Parameters.AddWithValue("@application_status", AppStatus);
            cmd.Parameters.AddWithValue("@job_apply_id", JobApplyID);
            int Result = 0;
            try
            {
                Result = (int)cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Result;
        }

    }
}