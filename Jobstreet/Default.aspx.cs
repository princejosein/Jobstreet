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
    public partial class WebForm2 : System.Web.UI.Page
    {
        DataAccessLayer _dataAccess;
        SearchEntity _searchEntity;
        private string FirstName;
        protected void Page_Load(object sender, EventArgs e)
        {
            Verify();
            if (!Page.IsPostBack)
            {
                RefreshCities();
            }
        }
        private void Verify()
        {
            txtLabel.Text = "Don’t have an account? <a href='Register.aspx'>Register</a>";
            try
            {
                FirstName = (string)Session["FirstName"];
                if(FirstName != null)
                {
                    txtLabel.Text = "Welcome " + FirstName;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void RefreshCities()
        {
            if ((this.Master as Site).ConnectToDB())
            {
                _dataAccess = (this.Master as Site).dataAccess;
                DataTable dt = new DataTable();
                dt = _dataAccess.GetCities();
                if(dt != null)
                {
                    ddlCities.DataSource = dt;
                    ddlCities.DataValueField = "CityID";
                    ddlCities.DataTextField = "CityName";
                    ddlCities.DataBind();
                    ddlCities.Items.Insert(0, new ListItem("All Cities","0"));
                }
                _dataAccess.Close();
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            if ((this.Master as Site).ConnectToDB())
            {
                string KeyWord = txtSearch.Text.Trim();
                int CityID = int.Parse(ddlCities.SelectedValue.Trim());
                _dataAccess = (this.Master as Site).dataAccess;
                DataTable dt = new DataTable();
                _searchEntity = new SearchEntity(_dataAccess.sqlConnection);
                dt = _searchEntity.Search(KeyWord, CityID);
                ListJobs.DataSource = dt;
                ListJobs.DataBind();
                _dataAccess.Close();
                HomeArea.Visible = false;
                SearchArea.Visible = true;
                TotalCountID.Text = dt.Rows.Count.ToString()+" Jobs Found";
            }
        }
    }
}