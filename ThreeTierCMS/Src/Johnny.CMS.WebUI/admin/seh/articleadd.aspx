<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeBehind="articleadd.aspx.cs" Inherits="Johnny.CMS.admin.seh.articleadd" %>

<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Button" TagPrefix="zr" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Literal" TagPrefix="zr" %>
<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.TextBox" TagPrefix="zr" %>
<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.DropDownList" TagPrefix="zr" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">
    <table class="table_field" cellSpacing="1" cellPadding="2" align="center">
        <tr class="tr_title">
			<td colSpan="3"><zr:Literal ID="litPageTitle" runat="server" /></td>
		</tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litChannel" runat="server" Mandatory="True"/></td>
    	    <td class="td_field_input"><zr:DropDownList ID="ddlChannel" runat="server" class="normal_select" ValidateOneSelected="true"></zr:DropDownList></td>
    	    <td class="td_field_info"><asp:Panel ID="ddlChannelTip" runat="server"></asp:Panel></td>
        </tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litTitle" runat="server" Mandatory="True"/></td>
    	    <td><zr:TextBox id="txtTitle" CssClass="long_input" runat="server" MaxLength="100" Required="true"></zr:TextBox></td>
    	    <td><asp:Panel ID="txtTitleTip" runat="server"></asp:Panel></td>
        </tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litSubTitle" runat="server"/></td>
    	    <td><zr:TextBox id="txtSubTitle" CssClass="long_input" runat="server" MaxLength="200"></zr:TextBox></td>
    	    <td><asp:Panel ID="txtSubTitleTip" runat="server"></asp:Panel></td>
        </tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litKeyWord" runat="server"/></td>
    	    <td><zr:TextBox id="txtKeyWord" CssClass="normal_input" runat="server" MaxLength="100"></zr:TextBox></td>
    	    <td><asp:Panel ID="txtKeyWordTip" runat="server"></asp:Panel></td>
        </tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litSubContent" runat="server"/></td>
    	    <td><zr:TextBox id="txtSubContent" CssClass="long_input" runat="server" MaxLength="500"></zr:TextBox></td>
    	    <td><asp:Panel ID="txtSubContentTip" runat="server"></asp:Panel></td>
        </tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litContent" runat="server" Mandatory="True"/></td>
    	    <td colspan="2">
    	        <FCKeditorV2:FCKeditor ID="fckContent" runat="server" Height="400px">
                </FCKeditorV2:FCKeditor>
            </td>
        </tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litHits" runat="server" Mandatory="True" /></td>
    	    <td><zr:TextBox id="txtHits" CssClass="short_input" runat="server" MaxLength="10" Required="true" ValidateNumber="true" ValidateIntRange02147483647="true"></zr:TextBox></td>
    	    <td><asp:Panel ID="txtHitsTip" runat="server"></asp:Panel></td>
        </tr>
  		<tr class="tr_field">
			<td class="td_field_label"><zr:Literal ID="litFlags" runat="server" /></td>
			<td>			
    			<zr:ValidationCheckBox runat="server" id="chkIsTop" />
    			<zr:ValidationCheckBox runat="server" id="chkIsElite" />
    			<zr:ValidationCheckBox runat="server" id="chkIsPic" />
    			<zr:ValidationCheckBox runat="server" id="chkIsPageType" />
            </td>
			<td><asp:Panel ID="chkArticle" runat="server" class="msgNormal"><zr:Literal ID="litFlagsHint" runat="server" /></asp:Panel></td>
		</tr>
		<tr class="tr_field">
			<td class="td_field_label"><zr:Literal ID="litIsVerified" runat="server" Mandatory="True" /></td>
			<td>			
    			<zr:ValidationRadioButton runat="server" id="rdbVerified1" name="IsVerified" pattern="validate-one-required" />
    			<zr:ValidationRadioButton runat="server" id="rdbVerified0" name="IsVerified" pattern="validate-one-required" />
            </td>
			<td><asp:Panel ID="rdbVerified0Tip" runat="server" class="msgNormal"><asp:Literal ID="litRdbVerifiedTip" runat="server" /></asp:Panel></td>
		</tr>
	    <tr class="tr_field">
	        <td class="td_field_label"><zr:Literal ID="litCreatedTime" runat="server" /></td>
	        <td><asp:Label ID="lblCreatedTime" runat="server" Width="186px"></asp:Label></td>
	        <td></td>
        </tr>
        <tr class="tr_field">
	        <td class="td_field_label"><zr:Literal ID="litCreatedByName" runat="server" /></td>
	        <td><asp:Label ID="lblCreatedByName" runat="server" Width="186px"></asp:Label></td>
	        <td></td>
        </tr>
        <tr class="tr_field">
	        <td class="td_field_label"><zr:Literal ID="litUpdatedTime" runat="server" /></td>
	        <td><asp:Label ID="lblUpdatedTime" runat="server" Width="186px"></asp:Label></td>
	        <td></td>
        </tr>
        <tr class="tr_field">
	        <td class="td_field_label"><zr:Literal ID="litUpdatedByName" runat="server" /></td>
	        <td><asp:Label ID="lblUpdatedByName" runat="server" Width="186px"></asp:Label></td>
	        <td></td>
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