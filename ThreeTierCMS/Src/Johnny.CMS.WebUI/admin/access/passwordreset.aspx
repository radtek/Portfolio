<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeBehind="passwordreset.aspx.cs" Inherits="Johnny.CMS.admin.access.passwordreset" %>

<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Button" TagPrefix="zr" %>
<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Literal" TagPrefix="zr" %>
<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.TextBox" TagPrefix="zr" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">
    <table class="table_field" cellSpacing="1" cellPadding="2" align="center">
        <tr class="tr_title">
			<td colSpan="3"><zr:Literal ID="litPageTitle" runat="server" /></td>
		</tr>
   	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litOriginalPassword" runat="server" Mandatory="True" /></td>
    	    <td><zr:TextBox id="txtOriginalPassword" CssClass="normal_input" runat="server" MaxLength="50" TextMode="Password" Required="true"></zr:TextBox></td>
    	    <td><asp:Panel ID="txtOriginalPasswordTip" runat="server"></asp:Panel></td>
        </tr>
   	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litNewPassword" runat="server" Mandatory="True" /></td>
    	    <td><zr:TextBox id="txtNewPassword" CssClass="normal_input" runat="server" MaxLength="50" TextMode="Password" Required="true"></zr:TextBox></td>
    	    <td><asp:Panel ID="txtNewPasswordTip" runat="server"></asp:Panel></td>
        </tr>
   	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litConfirmedPassword" runat="server" Mandatory="True" /></td>
    	    <td><zr:TextBox id="txtConfirmedPassword" CssClass="normal_input" runat="server" MaxLength="50" TextMode="Password" Required="true"></zr:TextBox></td>
    	    <td><asp:Panel ID="txtConfirmedPasswordTip" runat="server"></asp:Panel></td>
        </tr>
    </table>
    <table align="center" cellspacing="0" border="0" width="98%">
        <tr><td style="height:10px"></td></tr>
        <tr>
            <td>
                <zr:Button ID="btnAdd" runat="server" ButtonType="Save" OnClick="btnSave_Click" />
                <zr:ResetButton ID="btnReset" runat="server" />
            </td>
        </tr>
    </table>    
</asp:Content>