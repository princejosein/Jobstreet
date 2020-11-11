<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="JobApplications.aspx.cs" Inherits="Jobstreet.JobApplications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <h2><asp:Label ID="PageTitle" runat="server" Text="Job Applications" CssClass="inner-page-head"></asp:Label></h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ListView ID="ListApplications" runat="server" >
            <ItemTemplate>
                 <div class="list-jobs">
                     <table class="list-jobs-table">
                         <tr>
                             <td class="list-jobs-city"><a href="Job.aspx?id=<%# Eval("JobID") %>"> <%# Eval("JobTitle") %></a></td>
                             <td class="list-jobs-date"><%# Eval("AppliedDate") %> </td>
                         </tr>
                         <tr>
                             <td colspan="2" class="list-jobs-city"><%# Eval("FirstName") %> <%# Eval("FirstName") %></td>
                         </tr>
                         <tr>
                             <td colspan="2" class="list-jobs-city"><%# Eval("Email") %></td>
                         </tr>
                         <tr>
                             <td colspan="2" class="list-jobs-city"><%# Eval("PhoneNumber") %> </td>
                         </tr>
                         <tr>
                             <td colspan="2" class="text-center">
                                 <a href="Employee.aspx?id=<%# Eval("UserID") %>"" class="btn side-btn">View Profile</a>
                                 
                             </td>
                         </tr>
                         <tr visible='<%# Convert.ToInt16(Eval("ApplicationStatus")) == 1?true:false %>' runat="server">
                             <td><asp:Button ID="BtnApprove" runat="server" Text="Accept Application" CommandArgument='<%# Eval("JobApplyID") %>' CssClass="btn btn-class btn-success " OnClick="BtnApprove_Click"  /></td>
                             <td class="text-right"><asp:Button ID="BtnReject" runat="server" Text="Reject Application" CommandArgument='<%# Eval("JobApplyID") %>' CssClass="btn btn-class btn-danger" OnClick="BtnReject_Click"  /></td>
                         </tr>
                         <tr visible='<%# Convert.ToInt16(Eval("ApplicationStatus")) != 1?true:false %>' runat="server">
                             <td colspan="2" class="text-center">
                                 <div class="btn side-btn">
                                     <%# Convert.ToInt16(Eval("ApplicationStatus")) == 2?"Application Accepted":"Application Rejected" %>
                                 </div>                                
                                 
                             </td>
                         </tr>
                         
                     </table>
                 </div>
    <hr />
             </ItemTemplate>
         </asp:ListView>
</asp:Content>
