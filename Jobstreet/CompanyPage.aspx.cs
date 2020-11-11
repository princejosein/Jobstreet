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
    public partial class CompanyPage : System.Web.UI.Page
    {
        DataAccessLayer _dataAccess;
        UserProfileEntity _userProfileEntity;
        JobEntity _jobEntity;
        int CompanyID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadCompanyDetails();
        }
        private void LoadCompanyDetails()
        {
            if (Request.QueryString["id"] != null)
            {
                if ((this.Master as Site).ConnectToDB())
                {
                    _dataAccess = (this.Master as Site).dataAccess;
                    _userProfileEntity = new UserProfileEntity(_dataAccess.sqlConnection);
                    CompanyID = int.Parse(Request.QueryString["id"].Trim());
                    DataTable dt = new DataTable();
                    dt = _userProfileEntity.GetCompanyInfo(CompanyID);
                    CompanyFormView.DataSource = dt;
                    CompanyFormView.DataBind();
                    if(dt.Rows.Count != 0)
                    {
                        CompanyLogo.ImageUrl = dt.Rows[0]["CompanyLogo"].ToString().Trim();
                    }
                    LoadJobLists();                   
                    _dataAccess.Close();
                }

            }
            
        }
        public void LoadJobLists()
        {
            CompanyID = int.Parse(Request.QueryString["id"].Trim());
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                DataTable dtl = new DataTable();
                _jobEntity = new JobEntity(_dataAccess.sqlConnection);
                dtl = _jobEntity.GetJobList(CompanyID);
                ListJobs.DataSource = dtl;
                ListJobs.DataBind();
                _dataAccess.Close();
            }
        }
        protected void ListJobs_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            lvDataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            LoadJobLists();
        }

    }
}