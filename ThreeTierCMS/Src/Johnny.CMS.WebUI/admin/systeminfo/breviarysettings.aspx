<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeBehind="breviarysettings.aspx.cs" Inherits="Johnny.CMS.admin.systeminfo.breviarysettings" %>

<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Button" TagPrefix="zr" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">
    <table class="table_field" cellSpacing="1" cellPadding="2" align="center">
        <tr class="tr_title">
			<td colSpan="3">����ͼ��������</td>
		</tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>����ͼ���</td>
    	    <td class="td_field_input"><asp:TextBox id="txtWidth" CssClass="normal_input" runat="server" MaxLength="4" pattern="required validate-int-range-1-1680" tip="����ͼ���"></asp:TextBox></td>
    	    <td class="td_field_info"><asp:Panel ID="txtWidthTip" runat="server"></asp:Panel></td>
        </tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>����ͼ�߶�</td>
    	    <td><asp:TextBox id="txtHeight" CssClass="normal_input" runat="server" MaxLength="4" pattern="required validate-int-range-1-1024" tip="����ͼ�߶�"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtHeightTip" runat="server"></asp:Panel></td>
        </tr>
   		<tr class="tr_field">
			<td class="td_field_label"><span class="input_mandatory">*</span>���ˮӡ</td>
			<td>			
    			<zr:ValidationRadioButton runat="server" id="rdbPlusWatermark1" name="PlusWatermark" pattern="validate-one-required" Text="��" />
    			<zr:ValidationRadioButton runat="server" id="rdbPlusWatermark0" name="PlusWatermark" pattern="validate-one-required" Text="��" />
            </td>
			<td><asp:Panel ID="rdbPlusWatermark0Tip" runat="server" class="msgNormal" tip="�Ƿ����ˮӡ">�Ƿ����ˮӡ</asp:Panel></td>
		</tr>
   		<tr class="tr_field">
			<td class="td_field_label"><span class="input_mandatory">*</span>ˮӡ����</td>
			<td>			
    			<zr:ValidationRadioButton runat="server" id="rdbWatermarkType0" name="WatermarkType" pattern="validate-one-required" Text="ͼƬˮӡ" />
    			<zr:ValidationRadioButton runat="server" id="rdbWatermarkType1" name="WatermarkType" pattern="validate-one-required" Text="����ˮӡ" />
            </td>
			<td><asp:Panel ID="rdbWatermarkType0Tip" runat="server" class="msgNormal" tip="�Ƿ����ˮӡ">�Ƿ����ˮӡ</asp:Panel></td>
		</tr>
        <tr class="tr_field">
    	    <td class="td_field_label">ˮӡͼƬ</td>
    	    <td><asp:TextBox id="txtWatermarkImage" CssClass="long_input" runat="server" MaxLength="800" pattern="max-length-800" tip="ˮӡͼƬ"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtWatermarkImageTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">ͼƬˮӡ͸����</td>
    	    <td><asp:TextBox id="txtImageTransparent" CssClass="long_input" runat="server" MaxLength="3" pattern="validate-int-range-0-100" tip="ͼƬˮӡ͸���ȣ�0����͸����100��͸��"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtImageTransparentTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">ˮӡ����</td>
    	    <td><asp:TextBox id="txtWatermarkText" CssClass="long_input" runat="server" MaxLength="50" pattern="required max-length-50" tip="ˮӡ����"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtWatermarkTextTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">����ˮӡ͸����</td>
    	    <td><asp:TextBox id="txtTextTransparent" CssClass="long_input" runat="server" MaxLength="3" pattern="validate-int-range-0-100" tip="����ˮӡ͸���ȣ�0����͸����100��͸��"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtTextTransparentTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">ˮӡλ��</td>
    	    <td class="td_field_input"><asp:DropDownList ID="ddlWatermarkPosition" runat="server" class="normal_select" tip="ˮӡλ�ã����ϣ����У����£����ϣ����У����£����ϣ����У�����"></asp:DropDownList></td>
    	    <td class="td_field_info"><asp:Panel ID="ddlWatermarkPositionTip" runat="server">ˮӡλ�ã����ϣ����У����£����ϣ����У����£����ϣ����У�����</asp:Panel></td>
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

