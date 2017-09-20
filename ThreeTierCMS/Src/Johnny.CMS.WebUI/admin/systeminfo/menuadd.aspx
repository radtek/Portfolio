<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeBehind="menuadd.aspx.cs" Inherits="Johnny.CMS.admin.menuadd" %>

<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Button" TagPrefix="zr" %>
<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Literal" TagPrefix="zr" %>
<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.DropDownList" TagPrefix="zr" %>
<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.TextBox" TagPrefix="zr" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">
    <table class="table_field" cellSpacing="1" cellPadding="2" align="center">
        <tr class="tr_title">
			<td colSpan="3"><zr:Literal ID="litPageTitle" runat="server" /></td>
		</tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litMenuCategory" runat="server" Mandatory="True" /></td>
    	    <td class="td_field_input"><zr:DropDownList ID="ddlCategory" runat="server" class="normal_select" ValidateOneSelected="true"></zr:DropDownList></td>
    	    <td class="td_field_info"><asp:Panel ID="ddlCategoryTip" runat="server"></asp:Panel></td>
        </tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litMenuName" runat="server" Mandatory="True" /></td>
    	    <td class="td_field_input"><zr:TextBox id="txtMenuName" CssClass="normal_input" runat="server" MaxLength="50" Required="true"></zr:TextBox></td>
    	    <td class="td_field_info"><asp:Panel ID="txtMenuNameTip" runat="server"></asp:Panel></td>
        </tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litPageLink" runat="server" Mandatory="True" /></td>
    	    <td class="td_field_input"><zr:TextBox id="txtPageLink" CssClass="long_input" runat="server" MaxLength="100" Required="true"></zr:TextBox></td>
    	    <td class="td_field_info"><asp:Panel ID="txtPageLinkTip" runat="server"></asp:Panel></td>
        </tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litToolTip" runat="server"/></td>
    	    <td class="td_field_input"><zr:TextBox id="txtToolTip" CssClass="normal_input" runat="server" MaxLength="100"></zr:TextBox></td>
    	    <td class="td_field_info"><asp:Panel ID="txtToolTipTip" runat="server"></asp:Panel></td>
        </tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litImage" runat="server" /></td>
    	    <td class="td_field_input"><zr:TextBox id="txtImage" CssClass="long_input" runat="server" MaxLength="200"></zr:TextBox></td>
    	    <td class="td_field_info"><asp:Panel ID="txtImageTip" runat="server"></asp:Panel></td>
        </tr>
	    <tr class="tr_field">
    	    <td class="td_field_label"><zr:Literal ID="litPermission" runat="server" Mandatory="True" /></td>
    	    <td class="td_field_input"><zr:DropDownList ID="ddlPermission" runat="server" class="normal_select" ValidateOneSelected="true"></zr:DropDownList></td>
    	    <td class="td_field_info"><asp:Panel ID="ddlPermissionTip" runat="server"></asp:Panel></td>
        </tr>
		<tr class="tr_field">
			<td class="td_field_label"><zr:Literal ID="litIsDisplay" runat="server" Mandatory="True" /></td>
			<td>			
    			<zr:ValidationRadioButton runat="server" id="rdbDisplay0" name="IsDisplay" pattern="validate-one-required" />
    			<zr:ValidationRadioButton runat="server" id="rdbDisplay1" name="IsDisplay" pattern="validate-one-required" />
            </td>
			<td><asp:Panel ID="rdbDisplay0Tip" runat="server" class="msgNormal"><asp:Literal ID="litRdbDisplayTip" runat="server" /></asp:Panel></td>
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