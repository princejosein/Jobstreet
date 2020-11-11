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
    public partial class SavedJobs : System.Web.UI.Page
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
                LoadSavedJobs();
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
        private void LoadSavedJobs()
        {
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                DataTable dt = new DataTable();
                _jobEntity = new JobEntity(_dataAccess.sqlConnection);
                dt = _jobEntity.GetSavedJobs(UserID);
                if (dt.Rows.Count != 0)
                {
                    TotalCountID.Text = dt.Rows.Count + " Jobs Saved";
                    SavedJobsView.DataSource = dt;
                    SavedJobsView.DataBind();
                    dt = null;
                }
                else
                {
                    PageTitle.Text = "No Jobs Saved!!!";
                    SavedJobsView.Visible = false;
                    TotalCountID.Text = "0 Job Saved";
                }
                _dataAccess.Close();
                _jobEntity = null;
            }
        }

        protected void BtnRemove_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int JobID = int.Parse(btn.CommandArgument.ToString());
            if (JobID != 0)
            {
                int DeleteJob = 0;
                DeleteJob = RemoveSavedJob(JobID);
                if (DeleteJob == 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>swalAlert('Job Remove Error','Job not removed. Please try after some time!','error',false,true)</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>swalAlert('Job Removed Successfully','You Successfully removed from the saved lists!!!','success',false,false)</script>", false);

                }
            }
            LoadSavedJobs();
        }
        private int RemoveSavedJob(int JobID)
        {
            int DeleteJob = 0;
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                DataTable dt = new DataTable();
                _jobEntity = new JobEntity(_dataAccess.sqlConnection);
                DeleteJob = _jobEntity.RemoveSavedJob(JobID, UserID);
                _dataAccess.Close();
            }
            return DeleteJob;
        }
            
    }
}