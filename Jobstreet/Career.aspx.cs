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
    public partial class Career : System.Web.UI.Page
    {
        private int UserID = 0;
        private int UserRoleID = 0;
        DataAccessLayer _dataAccess;
        UserProfileEntity _userProfileEntity;
        protected string PAGETYPE = "EDUCATION";
        protected string MODE = "CREATE";
        protected void Page_Load(object sender, EventArgs e)
        {
            Verify();
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["type"] != null)
                {
                    PAGETYPE = Request.QueryString["Type"].ToString().ToUpper();
                }
                UpdatePage(PAGETYPE);
                
                
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
        private void UpdatePage(string Type)
        {
            EduDiv.Visible = false;
            CarDiv.Visible = false;
            string modeText = "Create ";
            UpdateYear();
            UpdateMonth();
            if (Request.QueryString["id"] != null)
            {
                MODE = "EDIT";
                HidID.Value = Request.QueryString["id"].Trim();
                modeText = "Edit";
            }
            if (Type == "EDUCATION")
            {
                EduDiv.Visible = true;
                PageTitle.Text = modeText + " Education";
                LevelOfQualificationList();
                if (MODE == "EDIT")
                {
                    RefreshEducation();
                }
            } else
            {
                CarDiv.Visible = true;
                PageTitle.Text = modeText + " Experience";
                if (MODE == "EDIT")
                {
                    RefreshExperience();
                }
            }
        }
        protected void RefreshEducation()
        {
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                _userProfileEntity = new UserProfileEntity(_dataAccess.sqlConnection);
                DataTable dt = new DataTable();
                dt = _userProfileEntity.GetEducationItem(
                    int.Parse(HidID.Value.Trim())
                   );
                _dataAccess.Close();
                if (dt.Rows.Count > 0)
                {
                    txtInstitutionName.Text = dt.Rows[0]["InstitutionName"].ToString().Trim();
                    txtLocation.Text = dt.Rows[0]["InstituteLocation"].ToString().Trim();
                    txtCourseName.Text = dt.Rows[0]["CourseName"].ToString().Trim(); 
                    textAreaCourceDesc.InnerText = dt.Rows[0]["EducationDesc"].ToString().Trim();
                    ddlLevelQualification.SelectedValue = dt.Rows[0]["LevelOfQualification"].ToString().Trim();
                    ddlStartMonth.SelectedValue = dt.Rows[0]["StartMonth"].ToString().Trim();
                    ddlStartYear.SelectedValue = dt.Rows[0]["StartYear"].ToString().Trim();
                    ddlEndMonth.SelectedValue = dt.Rows[0]["EndMonth"].ToString().Trim();
                    ddlEndYear.SelectedValue = dt.Rows[0]["EndYear"].ToString().Trim();
                }
                
            }
        }
        protected void RefreshExperience()
        {
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                _userProfileEntity = new UserProfileEntity(_dataAccess.sqlConnection);
                DataTable dt = new DataTable();
                dt = _userProfileEntity.GetExperienceItem(
                    int.Parse(HidID.Value.Trim())
                   );
                _dataAccess.Close();
                if (dt.Rows.Count > 0)
                {
                    txtJobTitle.Text = dt.Rows[0]["JobTtle"].ToString().Trim();
                    txtCompanyName.Text = dt.Rows[0]["CompanyName"].ToString().Trim();
                    txtCompanyLocation.Text = dt.Rows[0]["CompanyLocation"].ToString().Trim();
                    txtAreaJobDesc.InnerText = dt.Rows[0]["JobDesc"].ToString().Trim();
                    ddlStartMonthCar.SelectedValue = dt.Rows[0]["StartMonth"].ToString().Trim();
                    ddlStartYearCar.SelectedValue = dt.Rows[0]["StartYear"].ToString().Trim();
                    ddlEndMonthCar.SelectedValue = dt.Rows[0]["EndMonth"].ToString().Trim();
                    ddlEndYearCar.SelectedValue = dt.Rows[0]["EndYear"].ToString().Trim();
                }

            }
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
                   int.Parse(HidID.Value.Trim())
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
        
        protected void SaveCareer_Click(object sender, EventArgs e)
        {
            int InsertResult = 0;

            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                _userProfileEntity = new UserProfileEntity(_dataAccess.sqlConnection);
                InsertResult = _userProfileEntity.CreateOrUpdateExperience(
                   UserID,
                   txtJobTitle.Text.Trim(),
                   txtCompanyName.Text.Trim(),
                   txtCompanyLocation.Text.Trim(),
                   txtAreaJobDesc.InnerText.Trim(),
                   ddlStartMonthCar.SelectedValue.Trim(),
                   ddlStartYearCar.SelectedValue.Trim(),
                   ddlEndMonthCar.SelectedValue.Trim(),
                   ddlEndYearCar.SelectedValue.Trim(),
                   int.Parse(HidID.Value.Trim())
                   );
                _dataAccess.Close();
                if (InsertResult != 0)
                {
                    (this.Master as Site).scripToaster(true, "Success", "Carrer Updated");
                }
                else
                {
                    (this.Master as Site).scripToaster(false, "Error", "Update Error. Try After Some Time!");
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
            for (int i = Year - 99; i <= Year; i++)
            {
                ListItem li = new ListItem(i.ToString());
                ddlStartYear.Items.Add(li);
                ListItem li1 = new ListItem(i.ToString());
                ddlEndYear.Items.Add(li1);
                ListItem liex1 = new ListItem(i.ToString());
                ddlStartYearCar.Items.Add(liex1);
                ListItem liex2 = new ListItem(i.ToString());
                ddlEndYearCar.Items.Add(liex2);
            }
            ddlStartYear.Items.FindByText(Year.ToString()).Selected = true;
            ddlEndYear.Items.FindByText(Year.ToString()).Selected = true;
            ddlStartYearCar.Items.FindByText(Year.ToString()).Selected = true;
            ddlEndYearCar.Items.FindByText(Year.ToString()).Selected = true;
        }
        private void UpdateMonth()
        {
            int month = DateTime.Now.Month;
            for (int i = 1; i <= 12; i++)
            {
                ListItem li = new ListItem(i.ToString());
                ddlStartMonth.Items.Add(li);
                ListItem lia = new ListItem(i.ToString());
                ddlEndMonth.Items.Add(lia);
                ListItem liex1 = new ListItem(i.ToString());
                ddlStartMonthCar.Items.Add(liex1);
                ListItem liex2 = new ListItem(i.ToString());
                ddlEndMonthCar.Items.Add(liex2);
            }
            ddlStartMonth.Items.FindByValue(month.ToString()).Selected = true;
            ddlEndMonth.Items.FindByValue(month.ToString()).Selected = true;
            ddlStartMonthCar.Items.FindByValue(month.ToString()).Selected = true;
            ddlEndMonthCar.Items.FindByValue(month.ToString()).Selected = true;
        }

        protected void BtnDeleteEducation_Click(object sender, EventArgs e)
        {
            if ((this.Master as Site).ConnectToDB())
            {
                int eID = int.Parse(HidID.Value.ToString()); 
                _dataAccess = (this.Master as Site).dataAccess;
                _userProfileEntity = new UserProfileEntity(_dataAccess.sqlConnection);
                int uID = _userProfileEntity.DeleteEdcation(eID);
                _dataAccess.Close();
                Response.Redirect("UserProfile.aspx");
            }
        }
        protected void BtnDeleteExperience_Click(object sender, EventArgs e)
        {
            if ((this.Master as Site).ConnectToDB())
            {
                int cID = int.Parse(HidID.Value.ToString());
                _dataAccess = (this.Master as Site).dataAccess;
                _userProfileEntity = new UserProfileEntity(_dataAccess.sqlConnection);
                int uID = _userProfileEntity.DeleteCareer(cID);
                _dataAccess.Close();
                Response.Redirect("UserProfile.aspx");
            }
        }
        
    }
}