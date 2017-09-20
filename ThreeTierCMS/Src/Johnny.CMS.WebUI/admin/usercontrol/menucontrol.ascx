<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menucontrol.ascx.cs" Inherits="Johnny.CMS.admin.usercontrol.menucontrol" %>
<tr class="tr_field">
  <td rowspan="4" align="center" style="width: 10%"><asp:Label ID="lblMenuCategory" runat="server" ></asp:Label></td>
  <td rowspan="4" align="left" valign="middle" style="width: 25%">
      <asp:ListBox ID="lstLeft" runat="server" Height="250px" Width="100%" SelectionMode="Multiple"></asp:ListBox></td>
  <td align="center" style="width: 7%"><input id="btnSelect" class="btn_mouseout" type="button" value="  >  " runat="server" /></td>
  <td rowspan="4" align="left" valign="middle" style="width: 25%">
      <select size="4" id="lstRight" style="height:250px;width:100%;" runat="server"></select></td>
  <td rowspan="4" align="left" style="width: 33%"></td>
</tr>
<tr class="tr_field">
  <td align="center"><input id="btnSelectAll" class="btn_mouseout" type="button" value=" >> " runat="server" /></td>
</tr>
<tr class="tr_field">
  <td align="center"><input id="btnUnselect" class="btn_mouseout" type="button" value="  <  " runat="server" /></td>
</tr>
<tr class="tr_field">
  <td align="center"><input id="btnUnselectAll" class="btn_mouseout" type="button" value=" << " runat="server" /><input id="hdnSelected" type="hidden" runat="server" /></td>
</tr>
