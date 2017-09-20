<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeBehind="bestpracticeadd.aspx.cs" Inherits="Johnny.CMS.admin.seh.bestpracticeadd" %>

<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Button" TagPrefix="zr" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Literal" TagPrefix="zr" %>
<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.TextBox" TagPrefix="zr" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">
    <table class="table_field" cellSpacing="1" cellPadding="2" align="center">
        <tr class="tr_title">
			<td colSpan="3"><zr:Literal ID="litPageTitle" runat="server" /></td>
		</tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litBestPracticeName" runat="server" Mandatory="True" /></td>
    	    <td><zr:TextBox id="txtBestPracticeName" CssClass="normal_input" runat="server" MaxLength="50" Required="true"></zr:TextBox></td>
    	    <td><asp:Panel ID="txtBestPracticeNameTip" runat="server"></asp:Panel></td>
        </tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litShortDescription" runat="server" /></td>
    	    <td><zr:TextBox id="txtShortDescription" CssClass="long_input" runat="server" MaxLength="200"></zr:TextBox></td>
    	    <td><asp:Panel ID="txtShortDescriptionTip" runat="server"></asp:Panel></td>
        </tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litContent" runat="server" Mandatory="true" /></td>
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
			<td class="td_field_label"><zr:Literal ID="litIsDisplay" runat="server" Mandatory="True" /></td>
			<td>			
    			<zr:ValidationRadioButton runat="server" id="rdbDisplay0" name="IsDisplay" pattern="validate-one-required" />
    			<zr:ValidationRadioButton runat="server" id="rdbDisplay1" name="IsDisplay" pattern="validate-one-required" />
            </td>
			<td><asp:Panel ID="rdbDisplay0Tip" runat="server" class="msgNormal"><asp:Literal ID="litRdbDisplayTip" runat="server" /></asp:Panel></td>
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