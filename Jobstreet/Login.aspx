<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Jobstreet.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<h2 class="inner-page-head">Login</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="form-main">

      <div class="row">
        <div class="col-md-12 text-danger ">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        </div>
     </div>     
         

     <div class="form-group row">
        <div class="col-md-12 ">
             <label for="txtEmail">Email:</label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5"  Text="*" Display="Dynamic" runat="server" ControlToValidate="txtEmail" CssClass="text-danger" ErrorMessage="Email Required"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtEmail" TextMode="Email" CssClass="form-control" runat="server"></asp:TextBox>
        </div> 
    </div>

     <div class="form-group row">
         <div class="col-md-12 ">
             <label for="txtPassword">Password:</label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  Text="*" Display="Dynamic" runat="server" ControlToValidate="txtEmail" CssClass="text-danger" ErrorMessage="Password Required"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
        </div> 
     </div>

     <asp:Button ID="LoginBtn" runat="server" Text="Login" CssClass="btn side-btn" OnClick="LoginBtn_Click" />
&nbsp;<div class="link-line">
         Don’t have an account? <a href="Register.aspx">Register</a>
     </div>

 </div>  
</asp:Content>
