using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Jobstreet.Models;
using System.Data;

namespace Jobstreet
{
    public partial class CompanyProfile : System.Web.UI.Page
    {
        protected int UserID = 0;
        protected int UserRoleID = 0;
        DataAccessLayer _dataAccess;
        UserEntity _userEntity;
        protected void Page_Load(object sender, EventArgs e)
        {
            Verify();
            if (!Page.IsPostBack)
            {
                txtUserID.Text = UserID.ToString();
                refreshProfile();
                setControlMode(true);
            }
                
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            bool InsertResult = false;
            string dbImagePath = ImgExists.Value.ToString();
            if ((uploadLogo.PostedFile != null) && (uploadLogo.PostedFile.ContentLength > 0))
            {
                string fname = Path.GetFileName(uploadLogo.PostedFile.FileName);
                string fileName = Guid.NewGuid().ToString();
                var extention = Path.GetExtension(uploadLogo.PostedFile.FileName);
                string SaveLocation = Server.MapPath("Images/Company") + "\\" + fileName + extention;
                try
                {
                    uploadLogo.PostedFile.SaveAs(SaveLocation);
                    dbImagePath = "/Images/Company/" + fileName + extention;
                }
                catch (Exception ex)
                {
                    Console.Write("Error: " + ex.Message);
                }
            }
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                _userEntity = new UserEntity(_dataAccess.sqlConnection);
                InsertResult = _userEntity.SaveCompanyProfile(
                    txtUserID.Text.Trim(),
                    txtCompanyName.Text.Trim(),
                    txtAreaDescription.InnerText.Trim(),
                    dbImagePath,
                    txtCompanyAddress.Text.Trim()
                    );
                _dataAccess.Close();
                if(InsertResult == true)
                {
                    (this.Master as Site).scripToaster(true, "Company Profile Updated", "Update Company Profile Information Saved.");
                } else
                {
                    (this.Master as Site).scripToaster(false, "Company Profile Update Error", "Company Profile Update Eroor. Try After Some Time!");
                }
                setControlMode(true);
            }           

        }
        private void Verify()
        {
           try
            {
                UserID = (int)Session["UserID"];
                UserRoleID = (int)Session["UserRoleID"];
                if(UserID == 0 || UserRoleID != 2)
                {
                    Response.Redirect("Default.aspx");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Response.Redirect("Default.aspx");
            }
        }
        private void refreshProfile()
        {
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                _userEntity = new UserEntity(_dataAccess.sqlConnection);
                DataTable dt = _userEntity.GetCompanyProfile(txtUserID.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    txtCompanyName.Text = dt.Rows[0]["CompanyName"].ToString().Trim();
                    txtCompanyAddress.Text = dt.Rows[0]["CompanyAddress"].ToString().Trim();
                    txtAreaDescription.InnerText = dt.Rows[0]["CompanyDesc"].ToString().Trim();
                    txtCompanyName.Text = dt.Rows[0]["CompanyName"].ToString().Trim();
                    updateLogoImg(dt.Rows[0]["CompanyLogo"].ToString().Trim());
                }
            }
        }
        private void setControlMode(bool ReadOnly = true)
        {
            if(ReadOnly == true)
            {
                txtAreaDescription.Attributes.Add("readonly", "readonly");
                EditBtn.Visible = true;
                SaveBtn.Visible = false;
            } else
            {
                txtAreaDescription.Attributes.Remove("readonly");
                EditBtn.Visible = false;
                SaveBtn.Visible = true;
            }
            txtCompanyName.ReadOnly = ReadOnly;
            txtCompanyAddress.ReadOnly = ReadOnly;
            
        }
        private void updateLogoImg(string Path)
        {
            if(Path != null)
            {
                LogoImg.ImageUrl = Path;
                ImgExists.Value = Path;
            } else
            {
                LogoImg.ImageUrl = "Images/Company/placeholder_logo.png";
            }
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            setControlMode(false);
        }
    }
}