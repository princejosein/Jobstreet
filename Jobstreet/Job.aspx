<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Job.aspx.cs" Inherits="Jobstreet.Job" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <h2><asp:Label ID="PageTitle" runat="server" Text="" CssClass="inner-page-head"></asp:Label></h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
    <asp:ListView ID="ViewJob" runat="server" >
            <ItemTemplate>
                 <div class="list-jobs">
                     <table class="list-jobs-table">
                         <tr>
                             <td class="list-jobs-city"><a href="CompanyPage.aspx?id=<%# Eval("EmployerID") %>"> <%# Eval("CompanyName") %></a></td>
                             <td class="list-jobs-date">
                                 <asp:Image ID="CompanyLogo" Width="150" runat="server" ImageUrl='<%# Eval("CompanyLogo") %> ' />
                             </td>
                         </tr>
                         <tr>
                             <td class="list-jobs-city"><%# Eval("CityName") %></td>
                             <td class="list-jobs-date"><%# Eval("Date") %> </td>
                         </tr>
                         <tr>
                             <td colspan="2" class="list-jobs-experience">Experience Required : <%# Eval("Experience") %> Years</td> 
                         </tr>
                        <tr>
                            <td colspan="2">Job Description</td>                            
                        </tr>
                         <tr>
                             <td colspan="2" class="list-jobs-desc"><%# Eval("JobDesc") %></td>
                         </tr>
                         <tr>
                            <td colspan="2">Job Duties</td>                            
                        </tr>
                         <tr>
                             <td colspan="2" class="list-jobs-desc"><%# Eval("JobDuties") %></td>
                         </tr>
                         
                     </table>
                 </div>
             </ItemTemplate>
        
         </asp:ListView>
        </asp:PlaceHolder>

    <table class="list-jobs-table">
        <tr>
                             <td class="list-jobs-save">
                                 <button runat="server" id="BtnSave" onserverclick="Save_Click" class="btn side-btn">
                                    <i class="fas fa-heart"></i> 
                                Save</button>
                                 <%--<asp:LinkButton ID="BtnSave" runat="server" CssClass="btn side-btn">
                                     <i class="fas fa-heart"></i> 
                                 Save</asp:LinkButton>--%>
                             </td>
                             <td class="list-jobs-apply">

                                 <button ID="BtnApply" runat="server" onserverclick="Apply_Click" class="btn side-btn">
                                    <i class="fas fa-arrow-circle-right"></i> 
                                Apply</button>
                                <%-- <asp:LinkButton ID="BtnApply" runat="server" CssClass="btn side-btn">
                                     <i class="fas fa-arrow-circle-right"></i> 
                                 Apply</asp:LinkButton>--%>
                             </td>
                         </tr>
    </table>
    
</asp:Content>
