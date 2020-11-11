<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompanyPage.aspx.cs" Inherits="Jobstreet.CompanyPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:Image ID="CompanyLogo" runat="server" Width="150" class="rounded mx-auto d-block"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
       
    <asp:FormView ID="CompanyFormView" runat="server" CssClass="form-view"  >
        <ItemTemplate>
            <table>
                <tr>
                    <td>
                        <h1><asp:Label ID="Label1" runat="server" Text='<%# Eval("CompanyName") %>'></asp:Label></h1>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="company-address"><asp:Label ID="Label2" runat="server" Text='<%# Eval("CompanyAddress") %>' ></asp:Label></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="company-desc"><asp:Label ID="Label3" runat="server" Text='<%# Eval("CompanyDesc") %>'></asp:Label></div>
                    </td>
                </tr>
                <tr>
                    <td><hr /></td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:FormView>
    <h2>Jobs Listed </h2>
    <asp:ListView ID="ListJobs" runat="server" OnPagePropertiesChanging="ListJobs_PagePropertiesChanging" >
             <ItemTemplate>
                 <div class="list-jobs">
                     <table class="list-jobs-table">
                         <tr>
                             <td colspan="2">
                                 <h3><a href="Job.aspx?id=<%# Eval("JobID") %>"><%# Eval("JobTitle") %></a></h3>
                             </td>

                         </tr>
                         <tr>
                             <td class="list-jobs-city"><%# Eval("CityName") %></td>
                             <td class="list-jobs-date"><%# Eval("Date") %> </td>
                         </tr>
                         <tr>
                             <td colspan="2" class="list-jobs-experience">Experience Required : <%# Eval("Experience") %></td>
                         </tr>
                         <tr>
                             <td colspan="2" class="list-jobs-experience">Job Status : <%# Eval("Status") %></td>
                         </tr>
                         <tr>
                             <td colspan="2" class="list-jobs-desc"><%# Eval("JDesc") %></td>
                         </tr>
                         
                     </table>
                     <hr />
                 </div>
             </ItemTemplate>
        <EmptyDataTemplate>
            No Jobs Listed
        </EmptyDataTemplate>
         </asp:ListView>
     <asp:DataPager ID="lvDataPager1" runat="server" PagedControlID="ListJobs" PageSize="10">
            <Fields>
                <asp:NumericPagerField ButtonType="Link" />
            </Fields>
        </asp:DataPager>
</asp:Content>
