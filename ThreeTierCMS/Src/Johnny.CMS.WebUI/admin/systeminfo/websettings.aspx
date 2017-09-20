<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeBehind="websettings.aspx.cs" Inherits="Johnny.CMS.admin.systeminfo.websettings" %>

<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Button" TagPrefix="zr" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">
    <table class="table_field" cellSpacing="1" cellPadding="2" align="center">
        <tr class="tr_title">
			<td colSpan="3">网站参数配置</td>
		</tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>网站名称</td>
    	    <td class="td_field_input"><asp:TextBox id="txtWebsiteName" CssClass="normal_input" runat="server" MaxLength="40" pattern="required max-length-40" tip="网站名称"></asp:TextBox></td>
    	    <td class="td_field_info"><asp:Panel ID="txtWebsiteNameTip" runat="server"></asp:Panel></td>
        </tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>网站标题</td>
    	    <td><asp:TextBox id="txtWebsiteTitle" CssClass="long_input" runat="server" MaxLength="100" pattern="required max-length-100" tip="网站标题，显示在浏览器的标题栏"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtWebsiteTitleTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">网站简介</td>
    	    <td><asp:TextBox id="txtShortDescription" TextMode="MultiLine" CssClass="multiple_input" runat="server" MaxLength="500" pattern="max-length-500" tip="网站简介"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtShortDescriptionTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">联系电话</td>
    	    <td><asp:TextBox id="txtTel" CssClass="normal_input" runat="server" MaxLength="50" pattern="max-length-50" tip="联系电话"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtTelTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">传真</td>
    	    <td><asp:TextBox id="txtFax" CssClass="normal_input" runat="server" MaxLength="50" pattern="max-length-50" tip="传真"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtFaxTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">电子邮件</td>
    	    <td><asp:TextBox id="txtEmail" CssClass="long_input" runat="server" MaxLength="50" pattern="validate-email max-length-50" tip="电子邮件"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtEmailTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>网址</td>
    	    <td><asp:TextBox id="txtWebsiteAddress" CssClass="long_input" runat="server" MaxLength="200" pattern="required max-length-200" tip="网址"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtWebsiteAddressTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>安装路径</td>
    	    <td><asp:TextBox id="txtWebsitePath" CssClass="long_input" runat="server" MaxLength="50" pattern="required max-length-50" tip="安装路径"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtWebsitePathTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>上传文件大小</td>
    	    <td><asp:TextBox id="txtFileSize" CssClass="short_input" runat="server" MaxLength="50" pattern="required validate-int-range-0-5120" tip="上传文件大小(最大5MB)"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtFileSizeTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>LOGO地址</td>
    	    <td><asp:TextBox id="txtLogoPath" CssClass="long_input" runat="server" MaxLength="100" pattern="required max-length-100" tip="LOGO地址"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtLogoPathTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>Banner地址</td>
    	    <td><asp:TextBox id="txtBannerPath" CssClass="long_input" runat="server" MaxLength="100" pattern="required max-length-100" tip="Banner地址"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtBannerPathTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">版权信息</td>
    	    <td><asp:TextBox id="txtCopyright" CssClass="long_input" runat="server" MaxLength="500" pattern="max-length-500" tip="版权信息"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtCopyrightTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">网站关键词</td>
    	    <td><asp:TextBox id="txtMetaKeywords" CssClass="long_input" runat="server" MaxLength="100" pattern="max-length-100" tip="网站关键词"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtMetaKeywordsTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">网站描述</td>
    	    <td><asp:TextBox id="txtMetaDescription" CssClass="long_input" runat="server" MaxLength="400" pattern="max-length-400" tip="网站描述"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtMetaDescriptionTip" runat="server"></asp:Panel></td>
        </tr>
   		<tr class="tr_field">
			<td class="td_field_label"><span class="input_mandatory">*</span>是否关闭网站</td>
			<td>			
    			<zr:ValidationRadioButton runat="server" id="rdbIsClosed1" name="IsClosed" pattern="validate-one-required" Text="是" />
    			<zr:ValidationRadioButton runat="server" id="rdbIsClosed0" name="IsClosed" pattern="validate-one-required" Text="否" />
            </td>
			<td><asp:Panel ID="rdbIsClosed0Tip" runat="server" class="msgNormal" tip="是否关闭网站">是否关闭网站</asp:Panel></td>
		</tr>
        <tr class="tr_field">
    	    <td class="td_field_label">关闭网站描述</td>
    	    <td><asp:TextBox id="txtClosedInfo" CssClass="long_input" runat="server" MaxLength="1000" pattern="max-length-1000" tip="关闭网站描述"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtClosedInfoTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">用户协议</td>
    	    <td><asp:TextBox id="txtUserAgreement" CssClass="long_input" runat="server" MaxLength="50" pattern="max-length-50" tip="用户协议"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtUserAgreementTip" runat="server"></asp:Panel></td>
        </tr>
   		<tr class="tr_field">
			<td class="td_field_label"><span class="input_mandatory">*</span>后台登陆方式</td>
			<td>			
    			<zr:ValidationRadioButton runat="server" id="rdbLoginType0" name="LoginType" pattern="validate-one-required" Text="Session" />
    			<zr:ValidationRadioButton runat="server" id="rdbLoginType1" name="LoginType" pattern="validate-one-required" Text="Cookies" />
            </td>
			<td><asp:Panel ID="rdbLoginType0Tip" runat="server" class="msgNormal" tip="后台登陆方式（Session/Cookies ）">后台登陆方式（Session/Cookies ）</asp:Panel></td>
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

