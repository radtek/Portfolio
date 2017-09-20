<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeBehind="websettings.aspx.cs" Inherits="Johnny.CMS.admin.systeminfo.websettings" %>

<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Button" TagPrefix="zr" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">
    <table class="table_field" cellSpacing="1" cellPadding="2" align="center">
        <tr class="tr_title">
			<td colSpan="3">��վ��������</td>
		</tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>��վ����</td>
    	    <td class="td_field_input"><asp:TextBox id="txtWebsiteName" CssClass="normal_input" runat="server" MaxLength="40" pattern="required max-length-40" tip="��վ����"></asp:TextBox></td>
    	    <td class="td_field_info"><asp:Panel ID="txtWebsiteNameTip" runat="server"></asp:Panel></td>
        </tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>��վ����</td>
    	    <td><asp:TextBox id="txtWebsiteTitle" CssClass="long_input" runat="server" MaxLength="100" pattern="required max-length-100" tip="��վ���⣬��ʾ��������ı�����"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtWebsiteTitleTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">��վ���</td>
    	    <td><asp:TextBox id="txtShortDescription" TextMode="MultiLine" CssClass="multiple_input" runat="server" MaxLength="500" pattern="max-length-500" tip="��վ���"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtShortDescriptionTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">��ϵ�绰</td>
    	    <td><asp:TextBox id="txtTel" CssClass="normal_input" runat="server" MaxLength="50" pattern="max-length-50" tip="��ϵ�绰"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtTelTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">����</td>
    	    <td><asp:TextBox id="txtFax" CssClass="normal_input" runat="server" MaxLength="50" pattern="max-length-50" tip="����"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtFaxTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">�����ʼ�</td>
    	    <td><asp:TextBox id="txtEmail" CssClass="long_input" runat="server" MaxLength="50" pattern="validate-email max-length-50" tip="�����ʼ�"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtEmailTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>��ַ</td>
    	    <td><asp:TextBox id="txtWebsiteAddress" CssClass="long_input" runat="server" MaxLength="200" pattern="required max-length-200" tip="��ַ"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtWebsiteAddressTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>��װ·��</td>
    	    <td><asp:TextBox id="txtWebsitePath" CssClass="long_input" runat="server" MaxLength="50" pattern="required max-length-50" tip="��װ·��"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtWebsitePathTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>�ϴ��ļ���С</td>
    	    <td><asp:TextBox id="txtFileSize" CssClass="short_input" runat="server" MaxLength="50" pattern="required validate-int-range-0-5120" tip="�ϴ��ļ���С(���5MB)"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtFileSizeTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>LOGO��ַ</td>
    	    <td><asp:TextBox id="txtLogoPath" CssClass="long_input" runat="server" MaxLength="100" pattern="required max-length-100" tip="LOGO��ַ"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtLogoPathTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>Banner��ַ</td>
    	    <td><asp:TextBox id="txtBannerPath" CssClass="long_input" runat="server" MaxLength="100" pattern="required max-length-100" tip="Banner��ַ"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtBannerPathTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">��Ȩ��Ϣ</td>
    	    <td><asp:TextBox id="txtCopyright" CssClass="long_input" runat="server" MaxLength="500" pattern="max-length-500" tip="��Ȩ��Ϣ"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtCopyrightTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">��վ�ؼ���</td>
    	    <td><asp:TextBox id="txtMetaKeywords" CssClass="long_input" runat="server" MaxLength="100" pattern="max-length-100" tip="��վ�ؼ���"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtMetaKeywordsTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">��վ����</td>
    	    <td><asp:TextBox id="txtMetaDescription" CssClass="long_input" runat="server" MaxLength="400" pattern="max-length-400" tip="��վ����"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtMetaDescriptionTip" runat="server"></asp:Panel></td>
        </tr>
   		<tr class="tr_field">
			<td class="td_field_label"><span class="input_mandatory">*</span>�Ƿ�ر���վ</td>
			<td>			
    			<zr:ValidationRadioButton runat="server" id="rdbIsClosed1" name="IsClosed" pattern="validate-one-required" Text="��" />
    			<zr:ValidationRadioButton runat="server" id="rdbIsClosed0" name="IsClosed" pattern="validate-one-required" Text="��" />
            </td>
			<td><asp:Panel ID="rdbIsClosed0Tip" runat="server" class="msgNormal" tip="�Ƿ�ر���վ">�Ƿ�ر���վ</asp:Panel></td>
		</tr>
        <tr class="tr_field">
    	    <td class="td_field_label">�ر���վ����</td>
    	    <td><asp:TextBox id="txtClosedInfo" CssClass="long_input" runat="server" MaxLength="1000" pattern="max-length-1000" tip="�ر���վ����"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtClosedInfoTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">�û�Э��</td>
    	    <td><asp:TextBox id="txtUserAgreement" CssClass="long_input" runat="server" MaxLength="50" pattern="max-length-50" tip="�û�Э��"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtUserAgreementTip" runat="server"></asp:Panel></td>
        </tr>
   		<tr class="tr_field">
			<td class="td_field_label"><span class="input_mandatory">*</span>��̨��½��ʽ</td>
			<td>			
    			<zr:ValidationRadioButton runat="server" id="rdbLoginType0" name="LoginType" pattern="validate-one-required" Text="Session" />
    			<zr:ValidationRadioButton runat="server" id="rdbLoginType1" name="LoginType" pattern="validate-one-required" Text="Cookies" />
            </td>
			<td><asp:Panel ID="rdbLoginType0Tip" runat="server" class="msgNormal" tip="��̨��½��ʽ��Session/Cookies ��">��̨��½��ʽ��Session/Cookies ��</asp:Panel></td>
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

