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
    public partial class JobApplications : System.Web.UI.Page
    {
        DataAccessLayer _dataAccess;
        JobEntity _jobEntity;
        protected int EmployerID = 0;
        protected int UserID = 0;
        protected int UserRoleID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Verify();
            if (!Page.IsPostBack)
            {
                LoadJobApplications();
            }                
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
        private void LoadJobApplications()
        {
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                DataTable dt = new DataTable();
                _jobEntity = new JobEntity(_dataAccess.sqlConnection);
                EmployerID = _jobEntity.GetEmployerIDFromUserID(UserID);
                dt = _jobEntity.GetJobApplicationsByEmployerID(EmployerID);
                if (dt.Rows.Count != 0)
                {
                    ListApplications.DataSource = dt;
                    ListApplications.DataBind();
                }
                else
                {
                    PageTitle.Text = "No Job Applications";
                }
                dt = null;
                _jobEntity = null;
                _dataAccess = null;
            }
        }

        protected void BtnApprove_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int Aprove = 2;
            int JobApplyID = int.Parse(btn.CommandArgument.ToString());
            if (JobApplyID != 0)
            {
                if ((this.Master as Site).ConnectToDB())
                {
                    int AppStatus = 0;
                    _dataAccess = (this.Master as Site).dataAccess;
                    _jobEntity = new JobEntity(_dataAccess.sqlConnection);
                    AppStatus = _jobEntity.ApproveApplication(JobApplyID, Aprove);
                    if (AppStatus == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>swalAlert('Application Update Error','Application Approve Error. Please try after some time!','error',false,true)</script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>swalAlert('Application Accepted','Application Accepted','success',false,false)</script>", false);
                        LoadJobApplications();

                    }
                }

            }
        }

        protected void BtnReject_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int Aprove = 3;
            int JobApplyID = int.Parse(btn.CommandArgument.ToString());
            if (JobApplyID != 0)
            {
                if ((this.Master as Site).ConnectToDB())
                {
                    int AppStatus = 0;
                    _dataAccess = (this.Master as Site).dataAccess;
                    _jobEntity = new JobEntity(_dataAccess.sqlConnection);
                    AppStatus = _jobEntity.ApproveApplication(JobApplyID, Aprove);
                    if (AppStatus == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>swalAlert('Application Update Error','Application Reject Error. Please try after some time!','error',false,true)</script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>swalAlert('Application Rejected','Application Rejected','success',false,false)</script>", false);
                        LoadJobApplications();

                    }
                }
            }
        }
    }
}