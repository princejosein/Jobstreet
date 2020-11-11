using Jobstreet.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jobstreet
{
    public partial class User : System.Web.UI.Page
    {

        DataAccessLayer _dataAccess;
        UserEntity _userEntity;
        UserProfileEntity _userProfileEntity;
        private int UserID = 0;
        private int UserSID = 0;
        private int UserRoleID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Verify();
            if (!Page.IsPostBack)
            {
                RefreshProfile();
            }
                
        }
        private void RefreshProfile()
        {
            if (Request.QueryString["id"] != null)
            {
                UserSID = int.Parse(Request.QueryString["id"]);
                hidFldID.Value = UserSID.ToString();
                BtnDiv.Visible = false;
                if ((this.Master as Site).ConnectToDB())
                {
                    _dataAccess = (this.Master as Site).dataAccess;
                    DataTable dt = new DataTable();
                    _userProfileEntity = new UserProfileEntity(_dataAccess.sqlConnection);
                    dt = _userProfileEntity.GetUserInfo(UserSID);
                    if (dt.Rows.Count != 0)
                    {
                        PageTitle.Text = "Update User";
                        CreateOrUpdateUser.Text = "Update User";
                        txtFirstName.Text = dt.Rows[0]["FirstName"].ToString().Trim();
                        txtLastName.Text = dt.Rows[0]["LastName"].ToString().Trim();
                        txtEmail.Text = dt.Rows[0]["Email"].ToString().Trim();
                        txtPhone.Text = dt.Rows[0]["PhoneNumber"].ToString().Trim();
                        txtPassword.Text = "";
                        txtConfirmPassword.Text = "";
                        setControlMode(true);
                    } else
                    {
                        PageTitle.Text = "User Not Found!!";
                    }
                    _dataAccess.Close();
                    _userProfileEntity = null;
                }

            } else
            {
                EditBtn.Visible = false;
            }
        }
        private void setControlMode(bool ReadOnly = true)
        {
            txtFirstName.ReadOnly = ReadOnly;
            txtLastName.ReadOnly = ReadOnly;
            txtEmail.ReadOnly = true;
            txtPassword.ReadOnly = ReadOnly;
            txtConfirmPassword.ReadOnly = ReadOnly;
            txtPhone.ReadOnly = ReadOnly;
            RequiredFieldValidator3.Enabled = false;
            RequiredFieldValidator4.Enabled = false;
            RegularExpressionValidator1.Enabled = false;
            radioAccountType.Enabled = false;

        }
        private void Verify()
        {
            try
            {
                UserID = (int)Session["UserID"];
                UserRoleID = (int)Session["UserRoleID"];
                if (UserID == 0 || UserRoleID != 1)
                {
                    Response.Redirect("Default.aspx");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.Redirect("Default.aspx");
            }
        }
        private bool CreateUser()
        {
            int UserRole = int.Parse(radioAccountType.SelectedValue);
            UserSID = int.Parse(hidFldID.Value.Trim());
            if (UserSID != 0)
            {
                int Update = _userEntity.AddOrUpdateUser(
                    txtFirstName.Text.Trim(),
                    txtLastName.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtPhone.Text.Trim(),
                    UserRoleID,
                    txtPassword.Text.Trim(),
                    UserSID
                    );
                if(Update != 0)
                {
                    return true;
                }
            } else
            {
                bool userExists = _userEntity.EmailRegistered(txtEmail.Text.Trim());
                if (userExists == true)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>errorToastr('Registration Error','Email already registered')</script>", false);
                    //(this.Master as Site).scripToaster(false, "Registration Error", "Email already registered");
                    return true;
                }
                else
                {
                    int UserID = _userEntity.AddOrUpdateUser(
                    txtFirstName.Text.Trim(),
                    txtLastName.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtPhone.Text.Trim(),
                    UserRole,
                    txtPassword.Text.Trim()
                    );

                    if (UserID > 0)
                    {
                        hidFldID.Value = UserID.ToString().Trim();
                        return true;
                    }
                }
            }           
            
            return false;
        }

        protected void CreateOrUpdateUser_Click(object sender, EventArgs e)
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
            } else
            {
                (this.Master as Site).scripToaster(true, "Success", "User Details Updated");
            }
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            setControlMode(false);
            BtnDiv.Visible = true;
            EditBtn.Visible = false;
        }

        protected void backBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Users.aspx");
        }
    }
}