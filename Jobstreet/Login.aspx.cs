using Jobstreet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jobstreet
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        DataAccessLayer _dataAccess;
        UserEntity _userEntity;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            bool Loginresult = false;
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                _userEntity = new UserEntity(_dataAccess.sqlConnection);
                Loginresult = _userEntity.Login(
                    txtEmail.Text.Trim(), 
                    txtPassword.Text.Trim()
                    );
                _dataAccess.Close();
            }
            if (Loginresult == false)
            {
                (this.Master as Site).scripToaster(false, "Login Error", "Email or Password Incorrect");
            } else
            {
                string email = (string)Session["Email"];
                Response.Redirect("Default.aspx");
            }
        }
    }
}