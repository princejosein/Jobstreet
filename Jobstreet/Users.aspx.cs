using Jobstreet.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jobstreet.Admin
{
    public partial class Users : System.Web.UI.Page
    {
        DataAccessLayer _dataAccess;
        UserEntity _userEntity;
        private int UserID = 0;
        private int UserRoleID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Verify();
            if (!Page.IsPostBack)
            {
                LoadTable();
            }
                
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
        private void LoadTable()
        {
            DataTable dt = new DataTable();
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                _userEntity = new UserEntity(_dataAccess.sqlConnection);
                dt = _userEntity.ListUsers();
            }
            GridUsers.DataSource = dt;
            GridUsers.DataBind();
            lblRowCnt.InnerText = dt.Rows.Count.ToString().Trim() + " Row(s)";
        }
        protected void GridUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridUsers.PageIndex = e.NewPageIndex;
            LoadTable();
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int UserID = int.Parse(btn.CommandArgument.ToString());
            if (UserID != 0)
            {
                if ((this.Master as Site).ConnectToDB())
                {
                    int UserDelete = 0;
                    _dataAccess = (this.Master as Site).dataAccess;
                    _userEntity = new UserEntity(_dataAccess.sqlConnection);
                    UserDelete = _userEntity.DeleteUser(UserID);
                    if (UserDelete == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>swalAlert('User Delete Error','User Not Deleted. Please try after some time!','error',false,true)</script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>swalAlert('User Deleted','Successfully Deleted the User','success',false,false)</script>", false);
                        LoadTable();
                    }
                }

            }
        }
    }
}