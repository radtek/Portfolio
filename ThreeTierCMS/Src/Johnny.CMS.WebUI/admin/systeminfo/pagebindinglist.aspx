<%@ Page Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true" CodeBehind="pagebindinglist.aspx.cs" Inherits="Johnny.CMS.admin.pagebindinglist" %>

<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.ManageGridView" TagPrefix="zr" %>
<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.CheckBox" TagPrefix="zr" %>
<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Button" TagPrefix="zr" %>               

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr><td align="center">
        <zr:ManageGridView ID="myManageGridView" runat="server" PagingStyle="PrevNext" PageSize="15" OnPageIndexChanging="myManageGridView_PageIndexChanging" AutoGenerateColumns="False" SkinID="ManageList" AllowPaging="True" OnRowDeleting="myManageGridView_RowDeleting" OnRowCancelingEdit="myManageGridView_RowCancelingEdit" OnRowEditing="myManageGridView_RowEditing" OnRowCommand="myManageGridView_RowCommand" EnableModelValidation="True" >
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <zr:CheckBox id="chkSelectAll" runat="server" Group="chkGroup1" IsParent="True" BindedValue="" />
                    </HeaderTemplate>
                    <HeaderStyle width="50px" />
                    <itemtemplate>
                        <zr:CheckBox id="chkSelect" runat="server" Group="chkGroup1" BindedValue="" IsParent="False"></zr:CheckBox>
                    </itemtemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <headerstyle width="50px" />
                    <itemtemplate>
                        <asp:Label runat="server" Text='<%# Bind("PageBindingId") %>' id="lblId"></asp:Label>
                    </itemtemplate>
                </asp:TemplateField>
                <asp:TemplateField>                    
                    <itemstyle cssclass="gridview_lefttd" />
                    <itemtemplate>
                        <asp:Label id="lblMenuCategoryName" runat="server" Text='<%# Bind("MenuCategoryName") %>'></asp:Label> 
                    </itemtemplate>
                </asp:TemplateField>
                <asp:TemplateField>                    
                    <itemstyle cssclass="gridview_lefttd" />
                    <itemtemplate>
                        <asp:Label id="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label> 
                    </itemtemplate>
                </asp:TemplateField>
                <asp:TemplateField>                    
                    <itemstyle cssclass="gridview_lefttd" />
                    <itemtemplate>
                        <asp:HyperLink id="lblListMenu" runat="server" Text='<%# Bind("ListMenuName") %>' NavigateUrl='<%# Bind("ListPageLink") %>' CssClass="list_link"></asp:HyperLink>
                    </itemtemplate>
                </asp:TemplateField>
                <asp:TemplateField>                    
                    <itemstyle cssclass="gridview_lefttd" />
                    <itemtemplate>
                        <asp:HyperLink id="lblAddMenu" runat="server" Text='<%# Bind("AddMenuName") %>' NavigateUrl='<%# Bind("AddPageLink") %>' CssClass="list_link"></asp:HyperLink>
                    </itemtemplate>
                </asp:TemplateField>
            </Columns>                
        </zr:ManageGridView>
        </td></tr>
    </table>
    <table align="center" cellspacing="0" border="0" width="98%">
        <tr><td style="height:10px"></td></tr>
        <tr>
            <td>
                <zr:Button ID="btnDelete" runat="server" ButtonType="Delete" OnClick="btnDelete_Click" ApplyOnClickEvent="True" />
            </td>
        </tr>
    </table>
</asp:Content>

