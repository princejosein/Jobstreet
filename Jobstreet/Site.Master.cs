using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Jobstreet.Models;

namespace Jobstreet
{
    public partial class Site : System.Web.UI.MasterPage
    {
        public DataAccessLayer dataAccess = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateMenu();
        }
        public bool ConnectToDB()
        {

            bool bDbOK = false;
            if (dataAccess == null)
            {
                dataAccess = new DataAccessLayer(Jobstreet.Properties.Settings.Default.JobStreetDatabase.Trim());
            }
            else
            {
                bDbOK = dataAccess.IsConnected;
            }
            if (!bDbOK)
            {
                bDbOK = dataAccess.Connect();
            }
            return bDbOK;
        }

        public void scripToaster(bool success, string main, string msg)
        {
            if (success == false)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>errorToastr('" + main + "','" + msg + "')</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>successToastr('" + main + "','" + msg + "')</script>", false);
            }
        }
        private void UpdateMenu()
        {
            int UserRoleID = 0;
            try
            {
                UserRoleID = (int)Session["UserRoleID"];                

                if(UserRoleID == 1)
                {
                    //Admin
                    RemoveMenu("Job Applications");
                    RemoveMenu("Applied Jobs");
                    RemoveMenu("Saved Jobs");
                    RemoveMenu("Profile");
                    RemoveMenu("Jobs");
                    RemoveMenu("Company Profile");
                }
                if(UserRoleID == 2)
                {
                    //Employer
                    RemoveMenu("Management");
                    RemoveMenu("Saved Jobs");
                    RemoveMenu("Profile");
                    RemoveMenu("Applied Jobs");
                }
                if (UserRoleID == 3)
                {
                    //Employee
                    RemoveMenu("Jobs");
                    RemoveMenu("Company Profile");
                    RemoveMenu("Management");
                    RemoveMenu("Job Applications");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Login
            if (UserRoleID == 0)
            {
                BtnLogOut.Visible = false;
                RemoveMenu("Job Applications");
                RemoveMenu("Saved Jobs");
                RemoveMenu("Profile");
                RemoveMenu("Jobs");
                RemoveMenu("Company Profile");
                RemoveMenu("Management");
                RemoveMenu("Applied Jobs");

            }
            else
            {
                RemoveMenu("Login");
                BtnLogOut.Visible = true;
            }

        }

        protected void BtnLogOut_Click(object sender, EventArgs e)
        {
            Session.Contents.RemoveAll();
            Response.Redirect("Default.aspx");
        }
        private void RemoveMenu(string name)
        {
            MenuItem item = MainMenu.FindItem(name);
            if(item != null)
            {
                MainMenu.Items.Remove(item);
            }
        }
    }
    
}