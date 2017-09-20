<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="Johnny.CMS.admin.access.profile" %>

<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Calendar" TagPrefix="cc1" %>
<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Button" TagPrefix="zr" %>
<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Literal" TagPrefix="zr" %>
<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.TextBox" TagPrefix="zr" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">
    <table class="table_field" cellSpacing="1" cellPadding="2" align="center">
        <tr class="tr_title">
			<td colSpan="3"><zr:Literal ID="litPageTitle" runat="server" /></td>
		</tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litAdminName" runat="server" Mandatory="True" /></td>
    	    <td class="td_field_input"><asp:Label ID="lblAdminName" runat="server" Width="186px"></asp:Label></td>
    	    <td class="td_field_info"></td>
        </tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litFullName" runat="server" /></td>
    	    <td><zr:TextBox id="txtFullName" CssClass="normal_input" runat="server" MaxLength="50"></zr:TextBox></td>
    	    <td><asp:Panel ID="txtFullNameTip" runat="server"></asp:Panel></td>
        </tr>
   		<tr class="tr_field">
			<td class="td_field_label"><zr:Literal ID="litGender" runat="server" /></td>
			<td>			
    			<zr:ValidationRadioButton runat="server" id="rdbGender0" name="Gender"/>
    			<zr:ValidationRadioButton runat="server" id="rdbGender1" name="Gender"/>
            </td>
			<td><asp:Panel ID="rdbGender0Tip" runat="server" class="msgNormal"><asp:Literal ID="litRdbTip" runat="server" /></asp:Panel></td>
		</tr>
        <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litTel" runat="server" /></td>
    	    <td><zr:TextBox id="txtTel" CssClass="long_input" runat="server" MaxLength="50"></zr:TextBox></td>
    	    <td><asp:Panel ID="txtTelTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litEmail" runat="server" /></td>
    	    <td><zr:TextBox id="txtEmail" CssClass="long_input" runat="server" MaxLength="50" ValidateEmail="true"></zr:TextBox></td>
    	    <td><asp:Panel ID="txtEmailTip" runat="server"></asp:Panel></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litValidFrom" runat="server" Mandatory="True" /></td>
    	    <td><asp:Label ID="lblValidFrom" runat="server"></asp:Label></td>
    	    <td></td>
        </tr>
        <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litValidTo" runat="server" Mandatory="True" /></td>
    	    <td><asp:Label ID="lblValidTo" runat="server"></asp:Label></td>
    	    <td></td>
        </tr>
   		<tr class="tr_field">
			<td class="td_field_label"><zr:Literal ID="litActivated" runat="server" Mandatory="True" /></td>
			<td>			
    			<zr:ValidationRadioButton runat="server" id="rdbActivated0" name="IsActivated" pattern="validate-one-required" />
    			<zr:ValidationRadioButton runat="server" id="rdbActivated1" name="IsActivated" pattern="validate-one-required" />
            </td>
			<td><asp:Panel ID="rdbActivated0Tip" runat="server" class="msgNormal"><asp:Literal ID="litRdbActivatedTip" runat="server" /></asp:Panel></td>
		</tr>
	   <tr class="tr_field">
			<td class="td_field_label"><zr:Literal ID="litLoginTimes" runat="server" /></td>
			<td><asp:Label ID="lblLoginTimes" runat="server" Width="186px"></asp:Label></td>
			<td></td>
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

