<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="Jobstreet.User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<h2>
    <asp:Label ID="PageTitle" runat="server" Text="Create New User" CssClass="inner-page-head"></asp:Label>
</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="form-main">
     <div class="form-group row">
        <div class="col-md-12 ">
            <asp:Button ID="EditBtn" runat="server" Text="Edit User" CssClass="btn side-btn float-right" OnClick="EditBtn_Click"  />
        </div> 
    </div>

      <div class="row">
        <div class="col-md-12 text-danger ">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        </div>
     </div>     
         

     <div class="row form-group">
        <div class="col-md-6 ">
             <label for="txtFirstName">First Name:</label>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="*" runat="server" CssClass="text-danger" ErrorMessage="First Name Required" ControlToValidate="txtFirstName"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtFirstName" CssClass="form-control" runat="server" ></asp:TextBox>
        </div>   
        <div class="col-md-6 ">
             <label for="txtLastName">Last Name:</label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Text="*" runat="server" CssClass="text-danger" ErrorMessage="Last Name Required" ControlToValidate="txtLastName"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtLastName" CssClass="form-control" runat="server"></asp:TextBox>
        </div> 
    </div>

     <div class="row form-group">
        <div class="col-md-6 ">
             <label for="txtPassword">Password:
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3"  Text="*" Display="Dynamic" runat="server" ControlToValidate="txtPassword" CssClass="text-danger" ErrorMessage="Password Required"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator Text="*" Display="Dynamic" ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPassword" CssClass="text-danger" ErrorMessage="Password Must Contain 9 Characters and One Upper Case" ValidationExpression="^[a-zA-Z0-9]{6,}$"></asp:RegularExpressionValidator>
             </label>
             
&nbsp;<asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
        </div>   
        <div class="col-md-6 ">
             <label for="txtConfirmPassword">Confirm Password:</label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4"  Text="*" Display="Dynamic" runat="server" ControlToValidate="txtConfirmPassword" CssClass="text-danger" ErrorMessage="Confirm Password Required"></asp:RequiredFieldValidator>
            <asp:CompareValidator Text="*" Display="Dynamic"  ID="CompareValidator1" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" CssClass="text-danger" ErrorMessage="Password and Confirm Password Must Be Same"></asp:CompareValidator>
&nbsp;<asp:TextBox ID="txtConfirmPassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
        </div> 
    </div>

     <div class="row form-group">
        <div class="col-md-6 ">
             <label for="txtEmail">Email:</label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5"  Text="*" Display="Dynamic" runat="server" ControlToValidate="txtEmail" CssClass="text-danger" ErrorMessage="Email Required"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Text="*" Display="Dynamic" runat="server" ControlToValidate="txtEmail" CssClass="text-danger" ErrorMessage="Email format is wrong" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
&nbsp;<asp:TextBox ID="txtEmail" TextMode="Email" CssClass="form-control" runat="server"></asp:TextBox>
        </div>   
        <div class="col-md-6 ">
             <label for="txtLastName">Phone Number:</label>
            <asp:TextBox ID="txtPhone" TextMode="Phone" CssClass="form-control" runat="server"></asp:TextBox>
        </div> 
    </div>

     

      <div class="row form-group">
        <div class="col-md-6 ">
             <label for="radioAccountType">Account Type:</label>
        </div>
          <div class="col-md-6">
            <asp:RadioButtonList ID="radioAccountType" runat="server" CssClass="form-check form-check-inline">
                <asp:ListItem Value="3" Selected="True">Job Seeker</asp:ListItem>
                <asp:ListItem Value="2">Employer</asp:ListItem>
            </asp:RadioButtonList>
        </div> 
    </div>
    <div id="BtnDiv" runat="server">
    <asp:Button ID="CreateOrUpdateUser" runat="server" Text="Create User" CssClass="btn side-btn" OnClick="CreateOrUpdateUser_Click" />
    <asp:Button ID="backBtn" runat="server" Text="Back" CssClass="btn side-btn" OnClick="backBtn_Click" />
    </div>
    </div>
    <asp:HiddenField ID="hidFldID" Value="0" runat="server" />
</asp:Content>
