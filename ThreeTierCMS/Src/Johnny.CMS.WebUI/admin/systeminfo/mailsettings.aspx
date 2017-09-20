<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeBehind="mailsettings.aspx.cs" Inherits="Johnny.CMS.admin.systeminfo.mailsettings" %>

<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Button" TagPrefix="zr" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">
    <table class="table_field" cellSpacing="1" cellPadding="2" align="center">
        <tr class="tr_title">
			<td colSpan="3">�ʼ���������</td>
		</tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>SMTP Server</td>
    	    <td class="td_field_input"><asp:TextBox id="txtSmtpServerIP" CssClass="long_input" runat="server" MaxLength="50" pattern="required max-length-50" tip="SMTP������"></asp:TextBox></td>
    	    <td class="td_field_info"><asp:Panel ID="txtSmtpServerIPTip" runat="server"></asp:Panel></td>
        </tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>�˿�</td>
    	    <td><asp:TextBox id="txtSmtpServerPort" CssClass="short_input" runat="server" MaxLength="10" pattern="required validate-int-range-0-20000" tip="�˿�"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtSmtpServerPortTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>�ʼ���ַ</td>
    	    <td><asp:TextBox id="txtMailId" CssClass="long_input" runat="server" MaxLength="100" pattern="required max-length-100 validate-email" tip="�ʼ���ַ"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtMailIdTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>����</td>
    	    <td><asp:TextBox id="txtMailPassword" CssClass="normal_input" runat="server" MaxLength="50" pattern="required max-length-50" tip="����"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtMailPasswordTip" runat="server"></asp:Panel></td>
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

