<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="JobLists.aspx.cs" Inherits="Jobstreet.JobLists" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <h2 class="inner-page-head" id="page-title">Job Lists</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-group row">
        <div class="col-md-12 ">
            <asp:HyperLink ID="BackLink" runat="server" CssClass="btn side-btn" NavigateUrl="Jobs.aspx" >Create New Job</asp:HyperLink>
        </div> 
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="table-responsive bg-light">
                <asp:GridView ID="GridJobs" runat="server" AutoGenerateColumns="false"
                    DataKeyNames="JobID" EmptyDataRowStyle-HorizontalAlign="Center" ShowHeaderWhenEmpty="true"   AllowSorting="true" AllowPaging="true" PageSize="2"
                            EmptyDataText="--No Record(s) Found--" CssClass="table table-hover table-bordered" HeaderStyle-HorizontalAlign="Center" OnPageIndexChanging="GridJobs_PageIndexChanging"
                            
                    >
                    <Columns>
                        <asp:HyperLinkField HeaderText="JOB ID" DataNavigateUrlFormatString="Jobs.aspx?id={0}" DataNavigateUrlFields="JobID"   DataTextField="JobID" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="JobTitle" HeaderText="JOB TITLE"  />
                        <asp:BoundField DataField="CreatedDate" HeaderText="CREATED DATE"  />
                    </Columns>
                    <EmptyDataRowStyle  />
                </asp:GridView>
            </div>
            <div style="color:blue;margin-left:15px;text-align:left;font-weight:bold;">
                    <label class="control-label" runat="server" id="lblRowCnt">0 Row(s)</label>  
                </div>
        </ContentTemplate>       
        
    </asp:UpdatePanel>
</asp:Content>
