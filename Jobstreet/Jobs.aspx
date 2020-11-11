<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Jobs.aspx.cs" Inherits="Jobstreet.Jobs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <h2><asp:Label ID="PageTitle" runat="server" Text="Create A New Job" CssClass="inner-page-head"></asp:Label></h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="form-main">
        
        <div class="form-group row">
        <div class="col-md-12 ">
            <asp:Button ID="EditBtn" runat="server" Text="Edit Job" CssClass="btn side-btn float-right" OnClick="EditBtn_Click" />
        </div> 
    </div>

           
     
        <div class="form-group row">
        <div class="col-md-12 ">
             <label for="txtJobID">Job ID:</label>
            <asp:TextBox ID="txtJobID" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
        </div> 
    </div>

     <div class="form-group row">
        <div class="col-md-12 ">
             <label for="txtJobTitle">Job Title:</label>
            <asp:TextBox ID="txtJobTitle" CssClass="form-control" runat="server"></asp:TextBox>
        </div> 
    </div>

     <div class="form-group row">
        <div class="col-md-6 ">
             <label for="ddlStatus">Job Status:</label>
            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                <asp:ListItem Value="1" Text="Active" ></asp:ListItem>
                <asp:ListItem Value="2" Text="Not Active"></asp:ListItem>
            </asp:DropDownList>
        </div> 
         <div class="col-md-6 ">
             <label for="ddlExperience">Years Of Experience:</label>
            <asp:DropDownList ID="ddlExperience" runat="server" CssClass="form-control"></asp:DropDownList>
        </div> 
    </div>


    <div class="form-group row">
    <div class="col-md-12 ">
        <label for="txtAreaJobDesc">Job Description:</label>    
        <textarea id="txtAreaJobDesc" runat="server" rows="4" style="width:100%" CssClass="form-control"></textarea>
    </div> 
    </div>

    <div class="form-group row">
    <div class="col-md-12 ">
        <label for="txtAreaJobDuties">Job Duties:</label>    
        <textarea id="txtAreaJobDuties" runat="server" rows="4" style="width:100%" CssClass="form-control"></textarea>
    </div> 
    </div>

     <div class="form-group row">
    <div class="col-md-12 ">
        <label for="ddlCity">Job Location:</label>    
        <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>
    </div> 
    </div>

     
    
        <asp:Button ID="SaveBtn" Visible="true" runat="server" Text="Save" CssClass="btn side-btn" OnClick="SaveBtn_Click" 
              />
        <asp:HyperLink ID="BackLink" runat="server" CssClass="btn side-btn" NavigateUrl="JobLists.aspx" >Back</asp:HyperLink>

    </div>
    <asp:HiddenField ID="HidJobID" Value="0" runat="server" />
</asp:Content>
