<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeBehind="rolepermission.aspx.cs" Inherits="Johnny.CMS.admin.access.rolepermission" %>

<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.Button" TagPrefix="zr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">
    <script type="text/javascript" src="../scripts/multiselector.js"></script>
    <table class="table_field" cellSpacing="1" cellPadding="2" align="center">
	    <tr class="tr_field">
    	    <td align="center"><asp:Label ID="lblRoleName" runat="server"></asp:Label></td>
    	    <td colspan="4"><asp:DropDownList ID="ddlRoles" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>
   	    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>        
    </table>  
    <table align="center" cellspacing="0" border="0" width="98%">
        <tr><td style="height:10px"></td></tr>
        <tr>
            <td>
                <zr:Button ID="btnSave" runat="server" ApplyOnClickEvent="False" ButtonType="Save" OnClick="btnSave_Click" />
                <zr:ResetButton ID="btnReset" runat="server" ApplyOnClickEvent="False" />
                <input id="hdnAllSelected" type="hidden" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>