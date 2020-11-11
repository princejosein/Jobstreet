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
    public partial class AppliedJobs : System.Web.UI.Page
    {
        DataAccessLayer _dataAccess;
        JobEntity _jobEntity;
        private int UserID = 0;
        private int UserRoleID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Verify();
            if (!Page.IsPostBack)
            {
                LoadAppliedJobs();
            }
        }
        private void Verify()
        {
            try
            {
                UserID = (int)Session["UserID"];
                UserRoleID = (int)Session["UserRoleID"];
                if (UserID == 0 || UserRoleID != 3)
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
        private void LoadAppliedJobs()
        {
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                DataTable dt = new DataTable();
                _jobEntity = new JobEntity(_dataAccess.sqlConnection);
                dt = _jobEntity.GetAppliedJobs(UserID);
                if (dt.Rows.Count != 0)
                {
                    TotalCountID.Text = dt.Rows.Count + " Jobs Applied";
                    AppliedJobsView.DataSource = dt;
                    AppliedJobsView.DataBind();
                    dt = null;
                }
                else
                {
                    PageTitle.Text = "No Jobs Applied!!!";
                }
                _dataAccess.Close();
                _jobEntity = null;
            }
        }
    }
}