<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="Jobstreet.Employee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <h2><asp:Label ID="PageTitle" runat="server" Text="" CssClass="inner-page-head"></asp:Label></h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="profile-area">
    <div class="profile-section">
    <h2>Contact</h2>
    <asp:ListView ID="ProfileEmployee" runat="server">
            <ItemTemplate>
                 <div class="list-jobs">
                     <table class="list-jobs-table">
                         
                         <tr>
                             <td colspan="2"><i class="fas fa-at"></i> Email : <%# Eval("Email") %></td> 
                         </tr>
                        <tr>
                            <td colspan="2"><i class="fas fa-mobile-alt"></i> Phone Number : <%# Eval("PhoneNumber") %></td>                            
                        </tr>
                         
                     </table>
                 </div>
             </ItemTemplate>
         </asp:ListView>
        </div>

    <div class="profile-section">
    <h2>Experience History</h2>
    <asp:ListView ID="ListExperience" runat="server">
            <ItemTemplate>
                 <div class="list-jobs">
                     <table class="list-jobs-table">
                         <tr>
                             <td colspan="2"><strong><%# Eval("JobTtle") %> </strong></td>
                         </tr>
                         <tr>
                             <td colspan="2"><i class="far fa-building"></i> <%# Eval("CompanyName") %></td> 
                         </tr>
                        <tr>
                            <td colspan="2"><i class="fas fa-map-marker"></i> <%# Eval("CompanyLocation") %></td>                            
                        </tr>
                         <tr>
                            <td><i class="fas fa-calendar-alt"></i> <%# Eval("StartMonth") %> / <%# Eval("StartYear") %></td>        
                            <td><i class="fas fa-calendar-alt"></i> <%# Eval("EndMonth") %> / <%# Eval("EndYear") %></td>
                        </tr>
                         <tr>
                            <td colspan="2"><%# Eval("JobDesc") %></td>                            
                        </tr>
                     </table>
                 </div>
        <hr />
             </ItemTemplate>
         </asp:ListView>
        </div>

    <div class="profile-section">
    <h2>Education History</h2>
    <asp:ListView ID="ListEducations" runat="server">
            <ItemTemplate>
                 <div class="list-jobs">
                     <table class="list-jobs-table">
                         <tr>
                             <td colspan="2"><strong><%# Eval("CourseName") %> </strong></td>
                         </tr>
                         <tr>
                             <td colspan="2"><i class="fas fa-university"></i> <%# Eval("InstitutionName") %></td> 
                         </tr>
                        <tr>
                            <td colspan="2"><i class="fas fa-map-marker"></i> <%# Eval("InstituteLocation") %></td>                            
                        </tr>
                         <tr>
                            <td><i class="fas fa-calendar-alt"></i> <%# Eval("StartMonth") %> / <%# Eval("StartYear") %></td>        
                            <td><i class="fas fa-calendar-alt"></i> <%# Eval("EndMonth") %> / <%# Eval("EndYear") %></td>
                        </tr>
                         <tr>
                            <td colspan="2"><%# Eval("EducationDesc") %></td>                            
                        </tr>
                     </table>
                 </div>
        <hr />
             </ItemTemplate>
         </asp:ListView>
        </div>
</div>    
    <div  class="text-center">
        <a href="javascript:history.go(-1)" class="btn side-btn">Back</a>
                                 
    </div>
</asp:Content>
