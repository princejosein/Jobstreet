using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Security.Cryptography;

namespace Jobstreet.Models
{
    public class UserEntity
    {
        SqlConnection _sqlConnection;
        public static int USER_ROLE_ADMIN = 1;
        public static int USER_ROLE_EMPLOYER = 2;
        public static int USER_ROLE_EMPLOYEE = 3;
        public UserEntity(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }
        public int AddUser(string FirstName, string LastName,
            string Password, string Email, string PhoneNumber,int UserRoleID = 1)
        {
            string HashPwd = HashPassword(Password);
            string query = "INSERT INTO tblUsers VALUES(@first_name, @last_name, @email," +
                "@password, @phone_number, @user_role_id);SELECT CAST(scope_identity() AS int)";
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.Parameters.AddWithValue("@first_name", FirstName);
            cmd.Parameters.AddWithValue("@last_name", LastName);
            cmd.Parameters.AddWithValue("@email", Email);
            cmd.Parameters.AddWithValue("@password", HashPwd);
            cmd.Parameters.AddWithValue("@phone_number", PhoneNumber);
            cmd.Parameters.AddWithValue("@user_role_id", UserRoleID);

            int InsertID = 0;
            try
            {
                InsertID = (int)cmd.ExecuteScalar();
                if(UserRoleID == USER_ROLE_EMPLOYER)
                {
                    CreateEmployerItem(InsertID);
                }                
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return InsertID;
        }
        private void CreateEmployerItem(int UserID)
        {
            string query = "INSERT INTO tblEmloyers (UserID) VALUES(@user_id)";
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.Parameters.AddWithValue("@user_id", UserID);
            Console.WriteLine(query);
            int RowsCount = 0;
            try
            {
                RowsCount = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool EmailRegistered(string Email)
        {
            string query = "SELECT COUNT(*) FROM tblUsers WHERE Email = @email";
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.Parameters.AddWithValue("@email", Email);
            int UserExists = 0;
            try
            {
                UserExists = (int)cmd.ExecuteScalar();
                if (UserExists != 0)
                {
                    return true;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }  
            return false;
        }

        private string HashPassword(string Password)
        {
            byte[] salt;
            byte[] buffer2;
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(Password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
        public static bool VerifyHashedPassword(string HashPwd, string Password)
        {
            byte[] buffer4;
            byte[] src = Convert.FromBase64String(HashPwd);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(Password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return buffer4.SequenceEqual(buffer3);
        }
        public bool Login(string Email, string Password)
        {
            string HashPassword = "";
            int UserRoleID = 0;
            int UserID = 0;
            string FirstName="";
            string query = "SELECT TOP 1 * FROM tblUsers WHERE Email = @email";
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.Parameters.AddWithValue("@email", Email);
            SqlDataReader reader;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        HashPassword = reader.GetString(4);
                        UserRoleID = reader.GetInt32(6);
                        UserID = reader.GetInt32(0);
                        FirstName = reader.GetString(1);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            bool result = VerifyHashedPassword(HashPassword, Password);
            if (result == true)
            {
                HttpContext httpContext = HttpContext.Current;
                httpContext.Session["Email"] = Email;
                httpContext.Session["UserRoleID"] = UserRoleID;
                httpContext.Session["UserID"] = UserID;
                httpContext.Session["FirstName"] = FirstName;

            }
            return result;
        }

        public bool SaveCompanyProfile(string UserID, string CompanyName,
            string CompanyDesc, string CompanyLogo, string CompanyAddress
            )
        {
            string query = "UPDATE tblEmloyers SET CompanyName = @company_name," +
                "CompanyDesc = @company_desc, CompanyLogo = @company_logo, CompanyAddress = " +
                "@company_adddress WHERE UserID = @user_id";
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.Parameters.AddWithValue("@user_id", int.Parse(UserID));
            cmd.Parameters.AddWithValue("@company_name", CompanyName);
            cmd.Parameters.AddWithValue("@company_desc", CompanyDesc);
            cmd.Parameters.AddWithValue("@company_logo", CompanyLogo);            
            cmd.Parameters.AddWithValue("@company_adddress", CompanyAddress);
            int RowsCount = 0;
            try
            {
                RowsCount = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        public DataTable GetCompanyProfile(string UserID)
        {
            DataTable dt = new DataTable();
            string query = "SELECT TOP 1 * FROM tblEmloyers WHERE UserID = @user_id";
            SqlDataAdapter dap = new SqlDataAdapter(query, _sqlConnection);
            dap.SelectCommand.Parameters.AddWithValue("@user_id", int.Parse(UserID));
            dap.Fill(dt);
            return dt;
        }
        public DataTable ListUsers()
        {
            DataTable dt = new DataTable();
            string query = "SELECT UserID, FirstName, LastName, Email, PhoneNumber, UserRoleID, CASE WHEN UserRoleID = 1 THEN 'Admin' WHEN UserRoleID = 2 THEN 'Employer' ELSE 'Employee' END AS UserRole FROM tblUsers ORDER BY UserID ASC";
            SqlDataAdapter dap = new SqlDataAdapter(query, _sqlConnection);
            dap.Fill(dt);
            return dt;
        }
        public int DeleteUser(int UserID)
        {
            int Result = 0;
            DeleteTableDataByUserID(UserID, "tblJobApply");
            string query = "DELETE FROM tblUsers WHERE UserID = @user_id";
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.Parameters.AddWithValue("@user_id", UserID);
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
        public int DeleteTableDataByUserID(int UserID, string Table)
        {
            string query = string.Format("DELETE FROM {0} WHERE UserID = @user_id",Table);
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.Parameters.AddWithValue("@user_id", UserID);
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
        public int DeleteEdcationByUserID(int UserID)
        {
            string query = "DELETE FROM tblEducations WHERE UserID = @user_id";
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.Parameters.AddWithValue("@user_id", UserID);
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
        public int DeleteUserSkillsByUserID(int UserID)
        {
            string query = "DELETE FROM tblUserSkills WHERE UserID = @user_id";
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.Parameters.AddWithValue("@user_id", UserID);
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

        public int AddOrUpdateUser(string FirstName, string LastName,
            string Email, string PhoneNumber,int UserRoleID = 1, 
            string Password = null, int UserID = 0
            )
        {
            string HashPwd = null;
            string query;
            SqlCommand cmd;
            if (Password != "")
            {
                HashPwd = HashPassword(Password);
            }
            if(UserID == 0)
            {
                query = "INSERT INTO tblUsers VALUES(@first_name, @last_name, @email," +
                "@password, @phone_number, @user_role_id);SELECT CAST(scope_identity() AS int)";
                cmd = new SqlCommand(query, _sqlConnection);
                cmd.Parameters.AddWithValue("@email", Email);
                cmd.Parameters.AddWithValue("@user_role_id", UserRoleID);
                cmd.Parameters.AddWithValue("@password", HashPwd);
            } else {
                if(HashPwd != null)
                {
                    query = "UPDATE tblUsers SET FirstName = @first_name," +
                "LastName = @last_name, PhoneNumber = @phone_number, Password = @password " +
                "WHERE UserID = @user_id";
                cmd = new SqlCommand(query, _sqlConnection);
                cmd.Parameters.AddWithValue("@password", HashPwd);
                cmd.Parameters.AddWithValue("@user_id", UserID);
                } else
                {
                    query = "UPDATE tblUsers SET FirstName = @first_name," +
                "LastName = @last_name, PhoneNumber = @phone_number " +
                "WHERE UserID = @user_id";
                cmd = new SqlCommand(query, _sqlConnection);
                cmd.Parameters.AddWithValue("@user_id", UserID);
                }                
            }
            cmd.Parameters.AddWithValue("@first_name", FirstName);
            cmd.Parameters.AddWithValue("@last_name", LastName);
            cmd.Parameters.AddWithValue("@phone_number", PhoneNumber);

            int InsertID = 0;
            if(UserID == 0)
            {
                try
                {
                    InsertID = (int)cmd.ExecuteScalar();
                    if (UserRoleID == USER_ROLE_EMPLOYER)
                    {
                        CreateEmployerItem(InsertID);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } else
            {
                try
                {
                    InsertID = (int)cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            
            return InsertID;
        }
    }
}