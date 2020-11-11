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
    public partial class JobLists : System.Web.UI.Page
    {
        protected int UserID = 0;
        protected int UserRoleID = 0;
        DataAccessLayer _dataAccess;
        JobEntity _jobEntity;
        protected void Page_Load(object sender, EventArgs e)
        {
            Verify();
            LoadTable();
        }
        private void Verify()
        {
            try
            {
                UserID = (int)Session["UserID"];
                UserRoleID = (int)Session["UserRoleID"];
                if (UserID == 0 || UserRoleID != 2)
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
        protected void GridJobs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridJobs.PageIndex = e.NewPageIndex;
            LoadTable();
        }

        private void LoadTable()
        {
            DataTable dt = new DataTable();
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                _jobEntity = new JobEntity(_dataAccess.sqlConnection);
                dt = _jobEntity.GetJobsByUserID(UserID);
            }
            GridJobs.DataSource = dt;
            GridJobs.DataBind();
            lblRowCnt.InnerText = dt.Rows.Count.ToString().Trim() + " Row(s)";
        }
    }
}