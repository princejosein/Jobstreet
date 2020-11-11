<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AppliedJobs.aspx.cs" Inherits="Jobstreet.AppliedJobs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <h2><asp:Label ID="PageTitle" runat="server" Text="Applied Jobs" CssClass="inner-page-head"></asp:Label></h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="TotalCountID" runat="server" Text="" CssClass="count-class"></asp:Label>
    <asp:ListView ID="AppliedJobsView" runat="server">
        <ItemTemplate>
                 <div class="list-jobs">
                     <table class="list-jobs-table">
                         <tr>
                             <td colspan="2" class="list-jobs-city"><a href="Job.aspx?id=<%# Eval("JobID") %>"> <%# Eval("JobTitle") %></a></td>
                             
                         </tr>
                         <tr>
                             <td colspan="2" class="list-jobs-city">Job Status : <%# Eval("Status") %></td>
                             
                         </tr>
                         <tr>
                             <td class="list-jobs-city"><%# Eval("CityName") %></td>
                             <td class="list-jobs-date">Job Created On : <%# Eval("Date") %> </td>
                         </tr>
                         <tr>
                             <td class="list-jobs-experience">Experience Required : <%# Eval("Experience") %> Years</td> 
                             <td class="list-jobs-date">Job Applied On : <%# Eval("appDate") %> </td>
                         </tr>
                         <tr>
                             <td colspan="2" class="text-center">
                                 <button disabled class="btn btn-danger"><%# Eval("AppStatus") %></button>
                             </td>
                         </tr>
                         
                     </table>
                     <hr />
                 </div>
             </ItemTemplate>
    </asp:ListView>
</asp:Content>
