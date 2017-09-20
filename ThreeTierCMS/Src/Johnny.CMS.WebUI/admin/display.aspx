<%@ Page Language="C#" MasterPageFile="~/admin/general.master" AutoEventWireup="true" CodeBehind="display.aspx.cs" Inherits="Johnny.CMS.admin.display" %>

<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.WebPopUp" TagPrefix="cc1" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">
    <table class="table_field" cellSpacing="1" cellPadding="2" align="center">
            <tr class="tr_title">
                  <td align="center" colspan="2"><strong>������������Ϣ</strong></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label">�������������</td>
        	    <td class="td_server_info" ><asp:Label ID="lbServerName" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >������IP��ַ</td>
        	    <td class="td_server_info" ><asp:Label ID="lbIp" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >����������</td>
        	    <td class="td_server_info" ><asp:Label ID="lbDomain" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >�������˿�</td>
        	    <td class="td_server_info" ><asp:Label ID="lbPort" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >������IIS�汾</td>
        	    <td class="td_server_info" ><asp:Label ID="lbIISVer" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >���ļ������ļ���</td>
        	    <td class="td_server_info" ><asp:Label ID="lbPhPath" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >����������ϵͳ</td>
        	    <td class="td_server_info" ><asp:Label ID="lbOperat" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >ϵͳ�����ļ���</td>
        	    <td class="td_server_info" ><asp:Label ID="lbSystemPath" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >�������ű���ʱʱ��</td>
        	    <td class="td_server_info" ><asp:Label ID="lbTimeOut" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >����������������</td>
        	    <td class="td_server_info" ><asp:Label ID="lbLan" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >.NET Framework �汾</td>
        	    <td class="td_server_info" ><asp:Label ID="lbAspnetVer" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >��������ǰʱ��</td>
        	    <td class="td_server_info" ><asp:Label ID="lbCurrentTime" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >������IE�汾</td>
        	    <td class="td_server_info" ><asp:Label ID="lbIEVer" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >�������ϴ�����������������</td>
        	    <td class="td_server_info" ><asp:Label ID="lbServerLastStartToNow" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >�߼�������</td>
        	    <td class="td_server_info" ><asp:Label ID="lbLogicDriver" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >CPU ����</td>
        	    <td class="td_server_info" ><asp:Label ID="lbCpuNum" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >CPU ����</td>
        	    <td class="td_server_info" ><asp:Label ID="lbCpuType" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >�����ڴ�</td>
        	    <td class="td_server_info" ><asp:Label ID="lbMemory" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >��ǰ����ռ���ڴ�</td>
        	    <td class="td_server_info" ><asp:Label ID="lbMemoryPro" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >Asp.net��ռ�ڴ�</td>
        	    <td class="td_server_info" ><asp:Label ID="lbMemoryNet" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >Asp.net��ռCPU</td>
        	    <td class="td_server_info" ><asp:Label ID="lbCpuNet" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >��ǰSession����</td>
        	    <td class="td_server_info" ><asp:Label ID="lbSessionNum" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >��ǰSessionID</td>
        	    <td class="td_server_info" ><asp:Label ID="lbSession" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >��ǰϵͳ�û���</td>
        	    <td class="td_server_info" ><asp:Label ID="lbUser" runat="server"></asp:Label></td>
            </tr> 
        </table>
        <cc1:PopupWin ID="popupMsn" runat="server" ActionType="OpenLink" DarkShadow="0, 0, 0"
            DockMode="BottomRight" GradientDark="255, 153, 0" GradientLight="251, 238, 187"
            LightShadow="255, 192, 128" Link="http://www.google.com" LinkTarget="_blank"
            Shadow="128, 64, 0" Style="z-index: 116; left: 367px; position: absolute; top: 225px"
            TextColor="0, 0, 0" Visible="False" Width="216px" HideAfter="4000" />
</asp:Content>

