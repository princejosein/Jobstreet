<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Career.aspx.cs" Inherits="Jobstreet.Career" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <h2><asp:Label ID="PageTitle" runat="server" Text="" CssClass="inner-page-head"></asp:Label></h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="EduDiv" runat="server" visible="false">
    <div class="form-main">
        <div class="col-md-12" style="text-align:right;" runat="server">
            <asp:Button ID="BtnDeleteEducation" runat="server" Text="Delete Education" CssClass="btn side-btn"   OnClick="BtnDeleteEducation_Click" />
        </div>
        <div class="row form-group">
            <div class="col-md-12 ">
            <label for="txtInstitutionName">Institution:</label>
                 <asp:TextBox ID="txtInstitutionName" CssClass="form-control" runat="server" ></asp:TextBox>
            </div>
            </div>
            
              <div class="row form-group">
            <div class="col-md-12 ">
            <label for="txtLocation">Location:</label>
                 <asp:TextBox ID="txtLocation" CssClass="form-control" runat="server" ></asp:TextBox>
            </div>
            </div>
              <div class="row form-group">
            <div class="col-md-12 ">
            <label for="txtCourseName">Cource Or Qualification:</label>
                 <asp:TextBox ID="txtCourseName" CssClass="form-control" runat="server" ></asp:TextBox>
            </div>
            </div>
              <div class="row form-group">
            <div class="col-md-12 ">
            <label for="ddlLevelQualification">Level of Qualification:</label>
                 <asp:DropDownList ID="ddlLevelQualification" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            </div>
            <div class="row form-group">
            <div class="col-md-12">
                    <label for="textAreaCourceDesc">Course Highlights:</label>
                    <textarea id="textAreaCourceDesc" runat="server" rows="4" style="width:100%" CssClass="form-control"></textarea>
            </div>
           </div>

            <div class="row form-group">
            <div class="col-md-6 ">
            <label for="ddlStartMonth">Start Month</label>
            <asp:DropDownList ID="ddlStartMonth" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>   
            <div class="col-md-6 ">
            <label for="ddlStartYear">Start Year</label>
            <asp:DropDownList ID="ddlStartYear" runat="server" CssClass="form-control"></asp:DropDownList>
            </div> 
            </div>

             <div class="row form-group">
            <div class="col-md-6 ">
            <label for="ddlEndMonth">End Month</label>
            <asp:DropDownList ID="ddlEndMonth" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>   
            <div class="col-md-6 ">
            <label for="ddlEndYear">End Year</label>
            <asp:DropDownList ID="ddlEndYear" runat="server" CssClass="form-control"></asp:DropDownList>
            </div> 
            </div>
            <asp:Button CssClass="btn side-btn" OnClick="SaveEducation_Click" ID="SaveEduaction" runat="server" Text="Save" CausesValidation="false" />
             &nbsp;&nbsp;
            <asp:HyperLink ID="BackToList" CssClass="btn side-btn" runat="server" NavigateUrl="UserProfile.aspx">Cancel</asp:HyperLink>
    </div>
    </div>

    <div id="CarDiv" runat="server" visible="false">
        <div class="form-main">
            <div class="col-md-12" style="text-align:right;" runat="server">
            <asp:Button ID="BtnDeleteCareer" runat="server" Text="Delete Experience" CssClass="btn side-btn"   OnClick="BtnDeleteExperience_Click" />
        </div>
        <div class="row form-group">
            <div class="col-md-12 ">
            <label for="txtJobTitle">Job Ttle:</label>
                 <asp:TextBox ID="txtJobTitle" CssClass="form-control" runat="server" ></asp:TextBox>
            </div>
            </div>

            <div class="row form-group">
            <div class="col-md-6 ">
            <label for="txtCompanyName">Company Name:</label>
                 <asp:TextBox ID="txtCompanyName" CssClass="form-control" runat="server" ></asp:TextBox>
            </div>

            <div class="col-md-6 ">
            <label for="txtCompanyLocation">Company Location:</label>
                 <asp:TextBox ID="txtCompanyLocation" CssClass="form-control" runat="server" ></asp:TextBox>
            </div>

            </div>

            <div class="row form-group">
            <div class="col-md-12">
                    <label for="txtAreaJobDesc">Job Description:</label>
                    <textarea id="txtAreaJobDesc" runat="server" rows="4" style="width:100%" CssClass="form-control"></textarea>
            </div>
           </div>

            <div class="row form-group">
            <div class="col-md-6 ">
            <label for="ddlStartMonthCar">Start Month</label>
            <asp:DropDownList ID="ddlStartMonthCar" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>   
            <div class="col-md-6 ">
            <label for="ddlStartYearCar">Start Year</label>
            <asp:DropDownList ID="ddlStartYearCar" runat="server" CssClass="form-control"></asp:DropDownList>
            </div> 
            </div>

             <div class="row form-group">
            <div class="col-md-6 ">
            <label for="ddlEndMonthCar">End Month</label>
            <asp:DropDownList ID="ddlEndMonthCar" runat="server" CssClass="form-control"></asp:DropDownList>
            </div> 
            <div class="col-md-6 ">
            <label for="ddlEndYearCar">End Year</label>
            <asp:DropDownList ID="ddlEndYearCar" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            </div>
            <asp:Button CssClass="btn side-btn" OnClick="SaveCareer_Click" ID="Button1" runat="server" Text="Save" CausesValidation="false" />
             &nbsp;&nbsp;
            <asp:HyperLink ID="HyperLink1" CssClass="btn side-btn" runat="server" NavigateUrl="UserProfile.aspx">Cancel</asp:HyperLink>
    </div>
        </div>
    
            <asp:HiddenField ID="HidID" Value="0" runat="server" />
</asp:Content>
