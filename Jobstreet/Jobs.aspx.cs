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
    public partial class Jobs : System.Web.UI.Page
    {
        protected int UserID = 0;
        protected int UserRoleID = 0;
        DataAccessLayer _dataAccess;
        JobEntity _jobEntity;
        protected void Page_Load(object sender, EventArgs e)
        {
            Verify();
            if (!Page.IsPostBack)
            {
                if ((this.Master as Site).ConnectToDB())
                {
                    refreshCityList();
                    refreshExpList();
                    if (Request.QueryString["id"] != null)
                    {
                        HidJobID.Value = Request.QueryString["id"].Trim();
                        RefreshJob();
                        EditBtn.Visible = true;
                        setControlMode(true);
                        PageTitle.Text = "Update Job";
                    }
                    else
                    {
                        setControlMode(false);
                        EditBtn.Visible = false;
                    }
                }
                    
            }
        }
        private void RefreshJob()
        {
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                _jobEntity = new JobEntity(_dataAccess.sqlConnection);
                DataTable dt = _jobEntity.GetJobByJobID(int.Parse(HidJobID.Value.Trim()));
                if (dt.Rows.Count > 0)
                {
                    txtJobID.Text = dt.Rows[0]["JobID"].ToString().Trim();
                    txtJobTitle.Text = dt.Rows[0]["JobTitle"].ToString().Trim();
                    txtAreaJobDesc.InnerText = dt.Rows[0]["JobDesc"].ToString().Trim();
                    txtAreaJobDuties.InnerText = dt.Rows[0]["JobDuties"].ToString().Trim();
                    ddlCity.SelectedValue = dt.Rows[0]["CityID"].ToString().Trim();
                    ddlExperience.SelectedValue = dt.Rows[0]["Experience"].ToString().Trim();
                    ddlStatus.SelectedValue = dt.Rows[0]["JobStatus"].ToString().Trim();
                }
            }
        }
        private void refreshCityList()
        {
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                DataTable dt = _dataAccess.GetCities();
                if(dt != null)
                {
                    ddlCity.DataSource = dt;
                    ddlCity.DataValueField = "CityID";
                    ddlCity.DataTextField = "CityName";
                    ddlCity.DataBind();
                }
                
            }

        }
        private void refreshExpList()
        {
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                _jobEntity = new JobEntity(_dataAccess.sqlConnection);
                DataTable dt = _jobEntity.GetExpList();
                if (dt != null)
                {
                    ddlExperience.DataSource = dt;
                    ddlExperience.DataValueField = "ExpeienceID";
                    ddlExperience.DataTextField = "Name";
                    ddlExperience.DataBind();
                }

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

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            int InsertResult = 0;
            
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                _jobEntity = new JobEntity(_dataAccess.sqlConnection);
                InsertResult = _jobEntity.CreateOrUpdateJob(
                   txtJobTitle.Text.Trim(),
                   txtAreaJobDesc.InnerText.Trim(),
                   txtAreaJobDuties.InnerText.Trim(),
                   int.Parse(ddlStatus.SelectedValue.Trim()),
                   int.Parse(ddlExperience.SelectedValue.Trim()),
                   int.Parse(ddlCity.SelectedValue.Trim()),
                   UserID,
                   int.Parse(HidJobID.Value.Trim())
                   );
                _dataAccess.Close();
                if (InsertResult != 0)
                {
                    (this.Master as Site).scripToaster(true, "Job Updated", "Job Saved");
                }
                else
                {
                    (this.Master as Site).scripToaster(false, "Job Updated Error", "Job Updated Error. Try After Some Time!");
                }
                //setControlMode(true);
            }
        }
        private void setControlMode(bool ReadOnly = true)
        {
            if (ReadOnly == true)
            {
                txtAreaJobDuties.Attributes.Add("readonly", "readonly");
                txtAreaJobDesc.Attributes.Add("readonly", "readonly");
                EditBtn.Visible = true;
                SaveBtn.Visible = false;
            }
            else
            {
                txtAreaJobDuties.Attributes.Remove("readonly");
                txtAreaJobDesc.Attributes.Remove("readonly");
                EditBtn.Visible = false;
                SaveBtn.Visible = true;
            }
            txtJobTitle.ReadOnly = ReadOnly;
            ddlCity.Enabled = !ReadOnly;
            ddlExperience.Enabled = !ReadOnly;
            ddlStatus.Enabled = !ReadOnly;
        }
        protected void EditBtn_Click(object sender, EventArgs e)
        {
            setControlMode(false);
        }
    }
}