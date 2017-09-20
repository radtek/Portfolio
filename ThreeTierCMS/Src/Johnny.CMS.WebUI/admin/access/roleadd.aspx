<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeBehind="roleadd.aspx.cs" Inherits="Johnny.CMS.admin.access.roleadd" %>

<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Button" TagPrefix="zr" %>
<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Literal" TagPrefix="zr" %>
<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.TextBox" TagPrefix="zr" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">
    <table class="table_field" cellSpacing="1" cellPadding="2" align="center">
        <tr class="tr_title">
			<td colSpan="3"><zr:Literal ID="litPageTitle" runat="server" /></td>
		</tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litRoleName" runat="server" Mandatory="True" /></td>
    	    <td class="td_field_input"><zr:TextBox id="txtRoleName" CssClass="normal_input" runat="server" MaxLength="50" Required="true"></zr:TextBox></td>
    	    <td class="td_field_info"><asp:Panel ID="txtRoleNameTip" runat="server"></asp:Panel></td>
        </tr>	      
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litDescription" runat="server" /></td>
    	    <td class="td_field_input"><zr:TextBox id="txtDescription" CssClass="long_input" runat="server" MaxLength="200"></zr:TextBox></td>
    	    <td class="td_field_info"><asp:Panel ID="txtDescriptionTip" runat="server"></asp:Panel></td>
        </tr>
    </table>
    <table align="center" cellspacing="0" border="0" width="98%">
        <tr><td style="height:10px"></td></tr>
        <tr>
            <td>
                <zr:Button ID="btnAdd" runat="server" ButtonType="Add" OnClick="btnAdd_Click" />
                <zr:ResetButton ID="btnReset" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
