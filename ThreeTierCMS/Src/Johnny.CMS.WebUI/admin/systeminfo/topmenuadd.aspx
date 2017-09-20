<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeBehind="topmenuadd.aspx.cs" Inherits="Johnny.CMS.admin.topmenuadd" %>

<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Button" TagPrefix="zr" %>
<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Literal" TagPrefix="zr" %>
<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.TextBox" TagPrefix="zr" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">
    <table class="table_field" cellSpacing="1" cellPadding="2" align="center">
        <tr class="tr_title">
			<td colSpan="3"><zr:Literal ID="litPageTitle" runat="server" /></td>
		</tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litTopMenuName" runat="server" Mandatory="True" /></td>
    	    <td><zr:TextBox id="txtTopMenuName" CssClass="normal_input" runat="server" MaxLength="50" Required="true"></zr:TextBox></td>
    	    <td><asp:Panel ID="txtTopMenuNameTip" runat="server"></asp:Panel></td>
        </tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litToolTip" runat="server" Mandatory="True" /></td>
    	    <td><zr:TextBox id="txtToolTip" CssClass="normal_input" runat="server" MaxLength="50" Required="true"></zr:TextBox></td>
    	    <td><asp:Panel ID="txtToolTipTip" runat="server"></asp:Panel></td>
        </tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litPageLink" runat="server" Mandatory="True" /></td>
    	    <td><zr:TextBox id="txtPageLink" CssClass="long_input" runat="server" MaxLength="100" Required="true"></zr:TextBox></td>
    	    <td><asp:Panel ID="txtPageLinkTip" runat="server"></asp:Panel></td>
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