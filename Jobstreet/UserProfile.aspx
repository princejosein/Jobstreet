<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="Jobstreet.UserProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
                            
    </script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <h2 class="inner-page-head" id="page-title">Update Profile</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="dvTab">

   
    <ul class="nav nav-tabs">
        <li class="nav-item">
          <a class="nav-link active" data-toggle="tab" href="#Profile">Profile</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" data-toggle="tab" href="#Education" runat="server" id="linkEducation">Education</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" data-toggle="tab" href="#Experience" runat="server" id="linkExperience">Experience</a>
        </li>
      </ul>

 <div class="tab-content">

     <div class="container form-main tab-pane active" id="Profile"  >
         
                <%-- <div class="form-group row">
        <div class="col-md-12 ">
            <asp:Button ID="EditBtn" runat="server" Text="Edit Profile" CssClass="btn side-btn float-right"  />
        </div> 
    </div>--%>

           
     
        <div class="form-group row">
        <div class="col-md-12 ">
             <label for="txtUserID">User ID:</label>
            <asp:TextBox ID="txtUserID" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
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

    <%-- <div class="row form-group">
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
    </div>--%>

     <div class="row form-group">
        <div class="col-md-6 ">
             <label for="txtEmail">Email:</label>
&nbsp;<asp:TextBox ID="txtEmail" TextMode="Email" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
        </div>   
        <div class="col-md-6 ">
             <label for="txtLastName">Phone Number:</label>
            <asp:TextBox ID="txtPhone" TextMode="Phone" CssClass="form-control" runat="server"></asp:TextBox>
        </div> 
    </div>

         <asp:Button ID="ProfileSave" Visible="true" runat="server" Text="Save" CssClass="btn side-btn" OnClick="ProfileSave_Click" />
        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn side-btn" NavigateUrl="Default.aspx" >Back</asp:HyperLink>
     </div>
     <div class="container form-main tab-pane" id="Education"  >
    <div class="col-md-12" id="btnSet" style="text-align:right;" runat="server">
                      
        <a href="Career.aspx?type=Education" class="btn side-btn"  >Add Education</a>    
    </div>

         <h2>Education</h2>
         <asp:ListView ID="ListEducation" runat="server" >
             <ItemTemplate>
                 <div class="list-cource">
                     <table class="list-cource-table">
                         <tr>
                             <td class="list-cource-name"><%# Eval("CourseName") %></td>                                 
                             <td>
                                 <a href="Career.aspx?id=<%# Eval("EducationID") %>" class="list-cource-edit">Edit</a>
                             </td>
                         </tr>
                         <tr>
                             <td class="list-cource-level">Level : <%# Eval("LevelOfQualification") %></td>
                         </tr>
                         <tr>
                             <td class="list-cource-institute"><%# Eval("InstitutionName") %></td>
                             <td class="list-cource-location"><%# Eval("InstituteLocation") %></td>
                         </tr>
                         <tr>
                             <td class="list-cource-graduated">Graduated  <%# Eval("EndYear") %></td>
                         </tr>
                     </table>
                     <hr />
                 </div>
             </ItemTemplate>
         </asp:ListView>
     </div>
     <div class="container form-main tab-pane" id="Experience"  >
             <div class="col-md-12" id="Div1" style="text-align:right;" runat="server">
         <a href="Career.aspx?type=Career" class="btn side-btn"  >Add Experience</a>    
    </div>

         <h2>Experiences</h2>
         <asp:ListView ID="ListExperience" runat="server" >
             <ItemTemplate>
                 <div class="list-cource">
                     <table class="list-cource-table">
                         <tr>
                             <td class="list-cource-name"><%# Eval("JobTtle") %></td>                                 
                             <td>
                                 <a href="Career.aspx?type=Career&id=<%# Eval("CareerID") %>" class="list-cource-edit">Edit</a>
                             </td>
                         </tr>
                         <tr>
                             <td class="list-cource-institute"><%# Eval("CompanyName") %></td>
                             <td class="list-cource-location"><%# Eval("CompanyLocation") %></td>
                         </tr>
                         <tr>
                             <td class="list-cource-graduated">Start :  <%# Eval("StartYear") %></td>
                             <td class="list-cource-graduated">End :  <%# Eval("EndYear") %></td>
                         </tr>
                     </table>
                     <hr />
                 </div>
             </ItemTemplate>
         </asp:ListView>
     </div>

     </div>
        </div>


    <div class="form-main">
        


    </div>

    <!-- Modal Education -->
<div class="modal fade" id="EducationModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Education</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
         <asp:UpdatePanel ID="UpdateModal" runat="server" >
          <ContentTemplate>
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
              <asp:HiddenField ID="EducationID" Value="0" runat="server" />
            </ContentTemplate>
           </asp:UpdatePanel>
         <div class="modal-footer">
             <asp:Button CssClass="btn side-btn" OnClick="SaveEducation_Click" ID="SaveEduaction" runat="server" Text="Save" CausesValidation="false" />
             &nbsp;&nbsp;
            <button type="button" class="btn side-btn" data-dismiss="modal">Cancel</button>
      </div>
      </div>
    </div>
  </div>
</div> 
    <asp:HiddenField ID="HidJobID" Value="0" runat="server" />
</asp:Content>
