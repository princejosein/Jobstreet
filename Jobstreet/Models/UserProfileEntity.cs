using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Jobstreet.Models
{
    public class UserProfileEntity
    {

        SqlConnection _sqlConnection;
        public UserProfileEntity(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }
        public DataTable GetLevelOfQualificationList()
        {
            string query = "SELECT * FROM tblQualificationLevels";
            SqlDataAdapter dap = new SqlDataAdapter(query, _sqlConnection);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            dap.Dispose();
            return dt;
        }
        public int CreateOrUpdateExperience(int UserID, string JobTitle, string CompanyName,
            string CompanyLocation, string JobDesc, string StartMonth, string StartYear, 
            string EndMonth, string EndYear, int CareerID = 0
            )
        {
        string query;
            if (CareerID == 0)
            {
                query = "INSERT INTO tblCareers VALUES(@user_id, @job_title," +
                "@company_name, @company_location, @job_desc, @start_month," +
                "@start_year, @end_month, @end_year,0);SELECT CAST(scope_identity() AS int)";
            }
            else
            {
                query = "UPDATE tblCareers SET JobTtle = @job_title, " +
                    "CompanyName = @company_name, CompanyLocation = @company_location, JobDesc = @job_desc, " +
                    " StartMonth = @start_month, StartYear=@start_year, " +
                    "EndMonth = @end_month, EndYear= @end_year WHERE CareerID = @career_id";
            }

            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            if (CareerID != 0)
            {
                cmd.Parameters.AddWithValue("@career_id", CareerID);
            }
            cmd.Parameters.AddWithValue("@user_id", UserID);
            cmd.Parameters.AddWithValue("@job_title", JobTitle);
            cmd.Parameters.AddWithValue("@company_name", CompanyName);
            cmd.Parameters.AddWithValue("@company_location", CompanyLocation);
            cmd.Parameters.AddWithValue("@job_desc", JobDesc);
            cmd.Parameters.AddWithValue("@start_month", StartMonth);
            cmd.Parameters.AddWithValue("@start_year", StartYear);
            cmd.Parameters.AddWithValue("@end_month", EndMonth);
            cmd.Parameters.AddWithValue("@end_year", EndYear);
            int ID = 0;
            if (CareerID == 0)
            {
                try
                {
                    ID = (int)cmd.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
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
        public int CreateOrUpdateEducation(int UserID, string InstitutionName, string InstituteLocation,
            string CourseName, string EducationDesc, int LevelOfQualification, string StartMonth,
            string StartYear, string EndMonth, string EndYear, int EducationID = 0
            )
        {
            string query;
            if(EducationID == 0)
            {
                query = "INSERT INTO tblEducations VALUES(@user_id, @institute_name," +
                "@institute_location, @cource_name, @education_desc, @level_of_qualification, @start_month," +
                "@start_year, @end_month, @end_year,1);SELECT CAST(scope_identity() AS int)";
            } else
            {
                query = "UPDATE tblEducations SET UserID = @user_id, InstitutionName = @institute_name, " +
                    "InstituteLocation = @institute_location, CourseName = @cource_name, EducationDesc = @education_desc, " +
                    "LevelOfQualification = @level_of_qualification, StartMonth = @start_month, StartYear=@start_year, " +
                    "EndMonth = @end_month, EndYear= @end_year WHERE EducationID = @education_id";
            }

        SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            if (EducationID != 0)
            {
                cmd.Parameters.AddWithValue("@education_id", EducationID);
            }
            cmd.Parameters.AddWithValue("@user_id", UserID);
            cmd.Parameters.AddWithValue("@institute_name", InstitutionName);
            cmd.Parameters.AddWithValue("@institute_location", InstituteLocation);
            cmd.Parameters.AddWithValue("@cource_name", CourseName);
            cmd.Parameters.AddWithValue("@education_desc", EducationDesc);
            cmd.Parameters.AddWithValue("@level_of_qualification", LevelOfQualification);
            cmd.Parameters.AddWithValue("@start_month", StartMonth);
            cmd.Parameters.AddWithValue("@start_year", StartYear);
            cmd.Parameters.AddWithValue("@end_month", EndMonth);
            cmd.Parameters.AddWithValue("@end_year", EndYear);
            int ID = 0;
            if(EducationID == 0)
            {
                try
                {
                    ID = (int) cmd.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } else
            {
                try
                {
                    ID = (int) cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            
            return ID;
        }
        public DataSet GetEducationByUserID(int UserID)
        {
            string query = "SELECT * FROM tblEducations WHERE UserID ='"+UserID+"'";
            SqlDataAdapter dap = new SqlDataAdapter(query, _sqlConnection);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            dap.Dispose();
            return ds;
        }
        public DataSet GetExperienceByUserID(int UserID)
        {
            string query = "SELECT * FROM tblCareers WHERE UserID ='" + UserID + "' ORDER BY CareerID DESC";
            SqlDataAdapter dap = new SqlDataAdapter(query, _sqlConnection);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            dap.Dispose();
            return ds;
        }
        public DataTable GetExperienceItem(int CareerID)
        {
            DataTable dt = new DataTable();
            string query = "SELECT TOP 1 * FROM tblCareers WHERE CareerID = @career_id";
            SqlDataAdapter dap = new SqlDataAdapter(query, _sqlConnection);
            dap.SelectCommand.Parameters.AddWithValue("@career_id", CareerID);
            dap.Fill(dt);
            return dt;
        }
        public DataTable GetEducationItem(int EducationID)
        {
            DataTable dt = new DataTable();
            string query = "SELECT TOP 1 * FROM tblEducations WHERE EducationID = @education_id";
            SqlDataAdapter dap = new SqlDataAdapter(query, _sqlConnection);
            dap.SelectCommand.Parameters.AddWithValue("@education_id", EducationID);
            dap.Fill(dt);
            return dt;
        }
        public DataTable GetUserInfo(int UserID)
        {
            DataTable dt = new DataTable();
            string query = "SELECT TOP 1 * FROM tblUsers WHERE UserID = @user_id";
            SqlDataAdapter dap = new SqlDataAdapter(query, _sqlConnection);
            dap.SelectCommand.Parameters.AddWithValue("@user_id", UserID);
            dap.Fill(dt);
            return dt;
        }
        public DataTable GetCompanyInfo(int EmployerID)
        {
            DataTable dt = new DataTable();
            string query = "SELECT TOP 1 * FROM tblEmloyers WHERE EmployerID = @employer_id";
            SqlDataAdapter dap = new SqlDataAdapter(query, _sqlConnection);
            dap.SelectCommand.Parameters.AddWithValue("@employer_id", EmployerID);
            dap.Fill(dt);
            return dt;
        }
        public DataTable GetProfile(int ProfileID)
        {
            DataTable dt = new DataTable();
            string query = "SELECT TOP 1 * FROM tblUsers " +
                "WHERE tblUsers.UserID = @user_id";
            SqlDataAdapter dap = new SqlDataAdapter(query, _sqlConnection);
            dap.SelectCommand.Parameters.AddWithValue("@user_id", ProfileID);
            dap.Fill(dt);
            return dt;
        }
        public int UpdateUserProfile(
            string FirstName, string LastName, string PhoneNumber, int UserID
            )
        {
            string query = "UPDATE tblUsers SET FirstName = @first_name, LastName = @last_name, " +
                    "PhoneNumber = @phone_number " +
                    "WHERE UserID = @user_id";

            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.Parameters.AddWithValue("@user_id", UserID);
            cmd.Parameters.AddWithValue("@first_name", FirstName);
            cmd.Parameters.AddWithValue("@last_name", LastName);
            cmd.Parameters.AddWithValue("@phone_number", PhoneNumber);
            int ID = 0;            
            try
            {
                ID = (int)cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ID;
        }
        public int DeleteEdcation(int EducationID)
        {
            string query = "DELETE FROM tblEducations WHERE EducationID = @education_id";
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.Parameters.AddWithValue("@education_id", EducationID);
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
        public int DeleteCareer(int CareerID)
        {
            string query = "DELETE FROM tblCareers WHERE CareerID = @career_id";
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.Parameters.AddWithValue("@career_id", CareerID);
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