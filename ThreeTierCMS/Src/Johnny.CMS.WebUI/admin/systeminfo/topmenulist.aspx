<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeBehind="topmenulist.aspx.cs" Inherits="Johnny.CMS.admin.topmenulist" culture="auto" %>

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
                        <zr:CheckBox id="chkSelectAll" runat="server" Group="chkGroup" IsParent="True" BindedValue="" />
                    </HeaderTemplate>
                    <itemtemplate>
                        <zr:CheckBox id="chkSelect" runat="server" Group="chkGroup" BindedValue="" IsParent="False"></zr:CheckBox>
                    </itemtemplate>
                    <HeaderStyle width="50px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <itemtemplate>
                        <asp:Label runat="server" Text='<%# Bind("TopMenuId") %>' id="lblId"></asp:Label>
                        <asp:Label runat="server" Text='<%# Bind("Sequence") %>' id="lblSequence" Visible="False"></asp:Label>
                    </itemtemplate>
                    <headerstyle width="50px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <itemstyle cssclass="gridview_lefttd" />
                    <itemtemplate>                    
                        <asp:TextBox id="txtUptTopMenuName" runat="server" Text='<%# Bind("TopMenuName") %>' MaxLength="50" Width="100px"></asp:TextBox> 
                    </itemtemplate>                    
                </asp:TemplateField>
                <asp:TemplateField >
                    <itemstyle cssclass="gridview_lefttd" />
                    <itemtemplate>                    
                        <asp:TextBox id="txtUptTips" runat="server" Text='<%# Bind("Tooltip") %>' MaxLength="50" Width="100px"></asp:TextBox> 
                    </itemtemplate>                    
                </asp:TemplateField>
                <asp:TemplateField>
                    <itemstyle cssclass="gridview_lefttd" />
                    <itemtemplate>                    
                        <asp:TextBox id="txtUptPageLink" runat="server" Text='<%# Bind("PageLink") %>' MaxLength="100" Width="350px" ></asp:TextBox> 
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
                <zr:Button ID="btnSave" runat="server" ButtonType="Save" OnClick="btnSave_Click" ApplyOnClickEvent="True" />
                <zr:Button ID="btnDelete" runat="server" ButtonType="Delete" OnClick="btnDelete_Click" ApplyOnClickEvent="True" />
            </td>
        </tr>
    </table>
</asp:Content>
