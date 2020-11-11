<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Jobstreet.Admin.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function DeleteUser() {
            swal({
              title: "Are you sure?",
              text: "All the datas associated with this user will be permanently deleted!",
              icon: "warning",
              buttons: true,
              dangerMode: true,
            })
            .then((willDelete) => {
              if (willDelete) {
                  return true;
              } else {
                  return false;
              }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <h2><asp:Label ID="PageTitle" runat="server" Text="Users" CssClass="inner-page-head"></asp:Label></h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-group row">
        <div class="col-md-12 ">
            <asp:HyperLink ID="BackLink" runat="server" CssClass="btn side-btn" NavigateUrl="User.aspx" >Create New User</asp:HyperLink>
        </div> 
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="table-responsive bg-light">
                <asp:GridView ID="GridUsers" runat="server" AutoGenerateColumns="false"
                    DataKeyNames="UserID" EmptyDataRowStyle-HorizontalAlign="Center" ShowHeaderWhenEmpty="true"   AllowSorting="true" AllowPaging="true" PageSize="2"
                            EmptyDataText="--No Record(s) Found--" CssClass="table table-hover table-bordered" HeaderStyle-HorizontalAlign="Center" OnPageIndexChanging="GridUsers_PageIndexChanging"
                            
                    >
                    <Columns>
                        <asp:HyperLinkField HeaderText="USER ID" DataNavigateUrlFormatString="User.aspx?id={0}" DataNavigateUrlFields="UserID"   DataTextField="UserID" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="FirstName" HeaderText="First Name"  />
                        <asp:BoundField DataField="Email" HeaderText="Email"  />
                        <asp:BoundField DataField="UserRole" HeaderText="Role"  />
                        <asp:TemplateField ShowHeader ="False">
                            <ItemTemplate>
                                <asp:Button ID="BtnDelete" runat="server"  CommandName="Delete_User" CssClass="btn btn-danger" Text="Delete" CommandArgument='<%# Bind("UserID") %>'  OnClientClick="return DeleteUser()" OnClick="BtnDelete_Click" />
                            </ItemTemplate>
                       </asp:TemplateField>
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

