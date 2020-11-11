<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompanyProfile.aspx.cs" Inherits="Jobstreet.CompanyProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function ValidateInput() {
            if (document.getElementById("<%=uploadLogo.ClientID %>").value == "" &&
                document.getElementById("ImgExists").value == "") {
            swalAlert("Image Upload Error", "Please Upload Company Logo", "error");
            return false;
        }
        return true;      
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <h2 class="inner-page-head">Company Profile</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-main">
        
        <div class="form-group row">
        <div class="col-md-12 ">
            <asp:Button ID="EditBtn" runat="server" Text="Edit Profile" CssClass="btn side-btn float-right" OnClick="EditBtn_Click" />
        </div> 
    </div>

        <asp:Image ID="LogoImg" ImageUrl="Images/Company/placeholder_logo.png" Width="100" runat="server" class="rounded mx-auto d-block" />
      <div class="row">
        <div class="col-md-12 text-danger ">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        </div>
     </div>     
     
        <div class="form-group row">
        <div class="col-md-12 ">
             <label for="txtUserID">Company User ID:</label>
            <asp:TextBox ID="txtUserID" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
        </div> 
    </div>

     <div class="form-group row">
        <div class="col-md-12 ">
             <label for="txtCompany">Company Name:</label>
            <asp:TextBox ID="txtCompanyName" CssClass="form-control" runat="server"></asp:TextBox>
        </div> 
    </div>

    <div class="form-group row">
        <div class="col-md-12 ">
             <label for="txtCompany">Company Address:</label>
            <asp:TextBox ID="txtCompanyAddress" CssClass="form-control" runat="server"></asp:TextBox>
        </div> 
    </div>

    <div class="form-group row">
    <div class="col-md-12 ">
        <label for="txtAreaDescription">Company Description:</label>    
        <textarea id="txtAreaDescription" runat="server" rows="4" style="width:100%" CssClass="form-control"></textarea>
    </div> 
    </div>

    <div class="form-group row">
    <div class="col-md-12 ">
    <label for="uploadLogo">Upload Company Logo:</label>
     <asp:FileUpload ID="uploadLogo" runat="server" CssClass="form-control-file" />
    </div> 
    </div>
        <asp:Button ID="SaveBtn" Visible="false" runat="server" Text="Save" CssClass="btn side-btn" 
            OnClientClick="return ValidateInput()" OnClick="SaveBtn_Click"  />
        <asp:HyperLink ID="BackLink" runat="server" CssClass="btn side-btn" NavigateUrl="Default.aspx" >Back</asp:HyperLink>

    </div>
    <asp:HiddenField ID="ImgExists" Value="" runat="server"  />
</asp:Content>
