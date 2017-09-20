<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeBehind="breviarysettings.aspx.cs" Inherits="Johnny.CMS.admin.systeminfo.breviarysettings" %>

<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Button" TagPrefix="zr" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">
    <table class="table_field" cellSpacing="1" cellPadding="2" align="center">
        <tr class="tr_title">
			<td colSpan="3">缩略图参数配置</td>
		</tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>缩略图宽度</td>
    	    <td class="td_field_input"><asp:TextBox id="txtWidth" CssClass="normal_input" runat="server" MaxLength="4" pattern="required validate-int-range-1-1680" tip="缩略图宽度"></asp:TextBox></td>
    	    <td class="td_field_info"><asp:Panel ID="txtWidthTip" runat="server"></asp:Panel></td>
        </tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><span class="input_mandatory">*</span>缩略图高度</td>
    	    <td><asp:TextBox id="txtHeight" CssClass="normal_input" runat="server" MaxLength="4" pattern="required validate-int-range-1-1024" tip="缩略图高度"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtHeightTip" runat="server"></asp:Panel></td>
        </tr>
   		<tr class="tr_field">
			<td class="td_field_label"><span class="input_mandatory">*</span>添加水印</td>
			<td>			
    			<zr:ValidationRadioButton runat="server" id="rdbPlusWatermark1" name="PlusWatermark" pattern="validate-one-required" Text="是" />
    			<zr:ValidationRadioButton runat="server" id="rdbPlusWatermark0" name="PlusWatermark" pattern="validate-one-required" Text="否" />
            </td>
			<td><asp:Panel ID="rdbPlusWatermark0Tip" runat="server" class="msgNormal" tip="是否添加水印">是否添加水印</asp:Panel></td>
		</tr>
   		<tr class="tr_field">
			<td class="td_field_label"><span class="input_mandatory">*</span>水印类型</td>
			<td>			
    			<zr:ValidationRadioButton runat="server" id="rdbWatermarkType0" name="WatermarkType" pattern="validate-one-required" Text="图片水印" />
    			<zr:ValidationRadioButton runat="server" id="rdbWatermarkType1" name="WatermarkType" pattern="validate-one-required" Text="文字水印" />
            </td>
			<td><asp:Panel ID="rdbWatermarkType0Tip" runat="server" class="msgNormal" tip="是否添加水印">是否添加水印</asp:Panel></td>
		</tr>
        <tr class="tr_field">
    	    <td class="td_field_label">水印图片</td>
    	    <td><asp:TextBox id="txtWatermarkImage" CssClass="long_input" runat="server" MaxLength="800" pattern="max-length-800" tip="水印图片"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtWatermarkImageTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">图片水印透明度</td>
    	    <td><asp:TextBox id="txtImageTransparent" CssClass="long_input" runat="server" MaxLength="3" pattern="validate-int-range-0-100" tip="图片水印透明度，0：不透明，100：透明"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtImageTransparentTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">水印文字</td>
    	    <td><asp:TextBox id="txtWatermarkText" CssClass="long_input" runat="server" MaxLength="50" pattern="required max-length-50" tip="水印文字"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtWatermarkTextTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">文字水印透明度</td>
    	    <td><asp:TextBox id="txtTextTransparent" CssClass="long_input" runat="server" MaxLength="3" pattern="validate-int-range-0-100" tip="文字水印透明度，0：不透明，100：透明"></asp:TextBox></td>
    	    <td><asp:Panel ID="txtTextTransparentTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label">水印位置</td>
    	    <td class="td_field_input"><asp:DropDownList ID="ddlWatermarkPosition" runat="server" class="normal_select" tip="水印位置：左上，左中，左下，中上，正中，中下，右上，右中，右下"></asp:DropDownList></td>
    	    <td class="td_field_info"><asp:Panel ID="ddlWatermarkPositionTip" runat="server">水印位置：左上，左中，左下，中上，正中，中下，右上，右中，右下</asp:Panel></td>
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

