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
    public partial class UserProfile : System.Web.UI.Page
    {
        private int UserID = 0;
        private int UserRoleID = 0;
        DataAccessLayer _dataAccess;
        UserProfileEntity _userProfileEntity;
        protected void Page_Load(object sender, EventArgs e)
        {
            Verify();
            if (!Page.IsPostBack)
            {
                if ((this.Master as Site).ConnectToDB())
                {

                    LevelOfQualificationList();
                    UpdateYear();
                    UpdateMonth();
                    LoadEducationList();
                    LoadCareerList();
                    LoadUserInfo();
                }

            }
            
        }
        private void LevelOfQualificationList()
        {
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                _userProfileEntity = new UserProfileEntity(_dataAccess.sqlConnection);
                DataTable dt = _userProfileEntity.GetLevelOfQualificationList();
                if (dt != null)
                {
                    ddlLevelQualification.DataSource = dt;
                    ddlLevelQualification.DataValueField = "QualiFicationLevelID";
                    ddlLevelQualification.DataTextField = "Name";
                    ddlLevelQualification.DataBind();
                }

            }
        }
        private void UpdateYear()
        {
            int Year = DateTime.Now.Year;
            for(int i = Year - 99; i<= Year; i++)
            {
                ListItem li = new ListItem(i.ToString());
                ddlStartYear.Items.Add(li);
                ddlEndYear.Items.Add(li);
            }
            ddlStartYear.Items.FindByText(Year.ToString()).Selected = true;
            ddlEndYear.Items.FindByText(Year.ToString()).Selected = true;
        }
        private void UpdateMonth()
        {
            int month = DateTime.Now.Month;
            for(int i = 1; i<= 12; i++)
            {
                ListItem li = new ListItem(i.ToString());
                ddlStartMonth.Items.Add(li);
                ddlEndMonth.Items.Add(li);
            }
            ddlStartMonth.Items.FindByValue(month.ToString()).Selected = true;
            ddlEndMonth.Items.FindByValue(month.ToString()).Selected = true;
        }
        protected void SaveEducation_Click(object sender, EventArgs e)
        {
            int InsertResult = 0;

            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                _userProfileEntity = new UserProfileEntity(_dataAccess.sqlConnection);
                InsertResult = _userProfileEntity.CreateOrUpdateEducation(
                   UserID,
                   txtInstitutionName.Text.Trim(),
                   txtLocation.Text.Trim(),
                   txtCourseName.Text.Trim(),
                   textAreaCourceDesc.InnerText.Trim(),
                   int.Parse(ddlLevelQualification.SelectedValue.Trim()),
                   ddlStartMonth.SelectedValue.Trim(),
                   ddlStartYear.SelectedValue.Trim(),
                   ddlEndMonth.SelectedValue.Trim(),
                   ddlEndYear.SelectedValue.Trim(),
                   int.Parse(EducationID.Value.Trim())
                   );
                _dataAccess.Close();
                if (InsertResult != 0)
                {
                    (this.Master as Site).scripToaster(true, "Success", "Education Information Updated");
                }
                else
                {
                    (this.Master as Site).scripToaster(false, "Error", "Update Error. Try After Some Time!");
                }
            }
        }
        private void LoadEducationList()
        {
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                _userProfileEntity = new UserProfileEntity(_dataAccess.sqlConnection);
                DataSet ds = new DataSet();
                ds = _userProfileEntity.GetEducationByUserID(UserID);
                ListEducation.DataSource = ds;
                ListEducation.DataBind();
            }
        }
        private void LoadCareerList()
        {
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                _userProfileEntity = new UserProfileEntity(_dataAccess.sqlConnection);
                DataSet ds = new DataSet();
                ds = _userProfileEntity.GetExperienceByUserID(UserID);
                ListExperience.DataSource = ds;
                ListExperience.DataBind();
            }
        }
        private void LoadUserInfo()
        {
            _dataAccess = (this.Master as Site).dataAccess;
            _userProfileEntity = new UserProfileEntity(_dataAccess.sqlConnection);
            DataTable dt = new DataTable();
            dt = _userProfileEntity.GetUserInfo(UserID);
            if(dt.Rows.Count > 0)
            {
                txtUserID.Text = dt.Rows[0]["UserID"].ToString().Trim();
                txtFirstName.Text = dt.Rows[0]["FirstName"].ToString().Trim();
                txtLastName.Text = dt.Rows[0]["LastName"].ToString().Trim();
                txtEmail.Text = dt.Rows[0]["Email"].ToString().Trim();
                txtPhone.Text = dt.Rows[0]["PhoneNumber"].ToString().Trim();
            }
        }
        protected void BtnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "Confirm();", true);
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

        protected void Button4_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenDialog();", true);
        }

        protected void ProfileSave_Click(object sender, EventArgs e)
        {
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                _userProfileEntity = new UserProfileEntity(_dataAccess.sqlConnection);
                int uID = 0;
                uID = _userProfileEntity.UpdateUserProfile(
                        txtFirstName.Text.Trim(),
                        txtLastName.Text.Trim(),
                        txtPhone.Text.Trim(),
                        UserID
                    );
                _dataAccess.Close();
            }
            
        }
    }
}