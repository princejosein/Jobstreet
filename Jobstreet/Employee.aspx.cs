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
    public partial class Employee : System.Web.UI.Page
    {
        DataAccessLayer _dataAccess;
        UserProfileEntity _userProfileEntity;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                if ((this.Master as Site).ConnectToDB())
                {
                    int ProfileID = int.Parse(Request.QueryString["id"]);
                    _dataAccess = (this.Master as Site).dataAccess;
                    DataTable dt = new DataTable();
                    _userProfileEntity = new UserProfileEntity(_dataAccess.sqlConnection);
                    dt = _userProfileEntity.GetProfile(ProfileID);
                    if (dt.Rows.Count != 0)
                    {
                        PageTitle.Text = dt.Rows[0]["FirstName"].ToString() + " "+ dt.Rows[0]["LastName"].ToString();
                        ProfileEmployee.DataSource = dt;
                        ProfileEmployee.DataBind();
                        dt = null;
                        DataSet ds = new DataSet();
                        ds = _userProfileEntity.GetExperienceByUserID(ProfileID);
                        if(ds.Tables.Count > 0)
                        {
                            ListExperience.DataSource = ds;
                            ListExperience.DataBind();
                        }
                        ds = null;
                        ds = _userProfileEntity.GetEducationByUserID(ProfileID);
                        if (ds.Tables.Count > 0)
                        {
                            ListEducations.DataSource = ds;
                            ListEducations.DataBind();
                        }
                        ds = null;
                    }
                    else
                    {
                        PageTitle.Text = "User Not Found!";
                    }
                    _dataAccess.Close();
                    _dataAccess = null;
                }

            }
        }
    }
}