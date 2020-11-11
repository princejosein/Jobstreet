using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Jobstreet.Models;

namespace Jobstreet
{
    public partial class Register : System.Web.UI.Page
    {
        DataAccessLayer _dataAccess;
        UserEntity _userEntity;
        public static int USER_ROLE_ADMIN = 1;
        public static int USER_ROLE_EMPLOYER = 2;
        public static int USER_ROLE_EMPLOYEE = 3;
        protected void Page_Load(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>showContent()</script>", false);
        }

        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            bool UserRes = false;
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                _userEntity = new UserEntity(_dataAccess.sqlConnection);
                UserRes = CreateUser();
                _dataAccess.Close();
            }
            if (UserRes == false)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>errorToastr('Registration Error','Server Error Try After Some Time!')</script>", false);
                //(this.Master as Site).scripToaster(false, "Registration Error", "Server Error Try After Some Time!");
            }
        }
        private bool CreateUser()
        {
            int UserRole = int.Parse(radioAccountType.SelectedValue);
            bool userExists = _userEntity.EmailRegistered(txtEmail.Text.Trim());
            if (userExists == true)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>errorToastr('Registration Error','Email already registered')</script>", false);
                //(this.Master as Site).scripToaster(false, "Registration Error", "Email already registered");
                return true;
            }
            else
            {
                int UserID = _userEntity.AddUser(
                txtFirstName.Text.Trim(),
                txtLastName.Text.Trim(),
                txtPassword.Text.Trim(),
                txtEmail.Text.Trim(),
                txtPhone.Text.Trim(),
                UserRole
                );

                if (UserID > 0)
                {
                    hidFldID.Value = UserID.ToString().Trim();
                    Response.Redirect("Login.aspx");
                }
            }
            return false;
        }
    }
}