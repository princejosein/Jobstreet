<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Jobstreet.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
	 
		<div class="col-md">
            <asp:TextBox ID="txtSearch" placeholder="Enter Skill" runat="server" CssClass="form-control p-4 m-2"></asp:TextBox>
		</div>
		<div class="col-md">
		<%--<input type="text" class="form-control p-4 m-2" placeholder="Enter City">--%>
            <asp:DropDownList ID="ddlCities" class="form-control m-2 form-dropdown" runat="server"></asp:DropDownList>
		</div>
		<div class="col-md">
            <asp:Button ID="SearchBtn" runat="server" Text="Search" CssClass="btn btn-main m-2" OnClick="SearchBtn_Click" />
		</div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="HomeArea" runat="server">
        <asp:Label ID="txtLabel" runat="server" Text=""></asp:Label>
    </div>
<%--<div class="three-col-container" runat="server" id="HomeArea">
    <div class="flex-three-item">
        <span class="home-sub-head"><i class="far fa-thumbs-up icon-style"></i>Popular Jobs</span>
        
        <div class="home-list">
            <div class="home-list-name">
                .Net Developer
            </div>
            <div class="home-list-content">
                Absolute IT Limited
            </div>
        </div>

    </div>
    <div class="flex-three-item">
        <span class="home-sub-head"><i class="far fa-thumbs-up icon-style"></i>Latest Jobs</span>
        <div class="home-list">
            <div class="home-list-name">
                .Net Developer
            </div>
            <div class="home-list-content">
                Absolute IT Limited
            </div>
        </div>
    </div>
    <div class="flex-three-item no-border">
        <span class="home-sub-head"><i class="far fa-thumbs-up icon-style"></i>Popular Companies</span>
        <div class="home-list">
            <div class="home-list-name">
                .Net Developer
            </div>
            <div class="home-list-content">
                Absolute IT Limited
            </div>
        </div>
    </div>
</div>--%>
    <div class="" runat="server" id="SearchArea">
        <asp:Label ID="TotalCountID" runat="server" Text="" CssClass="count-class"></asp:Label>
        <asp:ListView ID="ListJobs" runat="server">
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
                             <td colspan="2" class="list-jobs-desc"><%# Eval("JDesc") %></td>
                         </tr>
                         <%--<tr>
                             <td class="list-jobs-save">
                                 <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn side-btn">
                                     <i class="fas fa-heart"></i> 
                                 Save</asp:LinkButton>
                             </td>
                             <td class="list-jobs-apply">
                                 <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn side-btn">
                                    <i class="fas fa-arrow-circle-right"></i> 
                                 Apply</asp:LinkButton>
                             </td>
                         </tr>--%>
                     </table>
                     <hr />
                 </div>
             </ItemTemplate>
        
         </asp:ListView>
     <asp:DataPager ID="lvDataPager1" runat="server" PagedControlID="ListJobs" PageSize="10">
            <Fields>
                <asp:NumericPagerField ButtonType="Link" />
            </Fields>
        </asp:DataPager>
    </div>
</asp:Content>
