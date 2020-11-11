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
    public partial class Job : System.Web.UI.Page
    {
        DataAccessLayer _dataAccess;
        JobEntity _jobEntity;
        private int UserID = 0;
        private int UserRoleID = 0;
        private int JobID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Verify();
            if (Request.QueryString["id"] != null)
            {
                JobID = int.Parse(Request.QueryString["id"]);
                if ((this.Master as Site).ConnectToDB())
                {                    
                    _dataAccess = (this.Master as Site).dataAccess;
                    DataTable dt = new DataTable();
                    _jobEntity = new JobEntity(_dataAccess.sqlConnection);
                    dt = _jobEntity.GetJob(JobID);
                    if(dt.Rows.Count != 0)
                    {
                        PageTitle.Text = dt.Rows[0]["JobTitle"].ToString();
                        ViewJob.DataSource = dt;
                        ViewJob.DataBind();
                        
                        dt = null;
                        if(UserID != 0 && UserRoleID == 3)
                        {
                            int[] Res = new int[2];
                            Res = _jobEntity.GetJobStatusOfUser(JobID, UserID);
                            if (Res[0] == 1)
                            {
                                BtnSave.Disabled = true;
                                BtnSave.InnerText = "Saved";
                            }
                            if (Res[1] == 1)
                            {
                                BtnApply.Disabled = true;
                                BtnApply.InnerText = "Applied";
                            }
                        } else
                        {
                            BtnSave.Visible = false;
                            BtnApply.Visible = false;
                        }
                        
                    } else
                    {
                        PageTitle.Text = "Job Not Found!";
                    }
                    _dataAccess.Close();
                    _jobEntity = null;
                }

            }
        }
        private void Verify()
        {
            try
            {
                UserID = (int)Session["UserID"];
                UserRoleID = (int)Session["UserRoleID"];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        protected void Apply_Click(object sender, EventArgs e)
        {
            if (JobID != 0)
            {
                if ((this.Master as Site).ConnectToDB())
                {
                    _dataAccess = (this.Master as Site).dataAccess;
                    int ApplyID = 0;
                    _jobEntity = new JobEntity(_dataAccess.sqlConnection);
                    ApplyID = _jobEntity.ApplyJobByUser(UserID, JobID);
                    if(ApplyID == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>swalAlert('Job Apply Error','Job not applied. Please try after some time!','error',false,true)</script>", false);
                    } else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>swalAlert('Job Applied Successfully','You Successfully applied to this job. Please Wait for the response from the employer. Wish You Good luck!!!','success',false,false)</script>", false);
                        BtnApply.InnerText = "Applied";
                        BtnApply.Disabled = true;
                    }
                }
            }               
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            if (JobID != 0)
            {
                if ((this.Master as Site).ConnectToDB())
                {
                    _dataAccess = (this.Master as Site).dataAccess;
                    int SavedID = 0;
                    _jobEntity = new JobEntity(_dataAccess.sqlConnection);
                    SavedID = _jobEntity.SaveJobByUser(UserID, JobID);
                    if (SavedID == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>swalAlert('Job Save Error','Job is not saved. Please try after some time!','error',false,true)</script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>swalAlert('Job Saved','This Job added to the saved list','success',false,false)</script>", false);
                        BtnSave.Disabled = true;
                        BtnSave.InnerText = "Saved";
                    }
                }
            }
        }

    }
}