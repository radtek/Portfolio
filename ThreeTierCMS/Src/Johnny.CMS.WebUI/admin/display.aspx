<%@ Page Language="C#" MasterPageFile="~/admin/general.master" AutoEventWireup="true" CodeBehind="display.aspx.cs" Inherits="Johnny.CMS.admin.display" %>

<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.WebPopUp" TagPrefix="cc1" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">
    <table class="table_field" cellSpacing="1" cellPadding="2" align="center">
            <tr class="tr_title">
                  <td align="center" colspan="2"><strong>服务器基本信息</strong></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label">服务器计算机名</td>
        	    <td class="td_server_info" ><asp:Label ID="lbServerName" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >服务器IP地址</td>
        	    <td class="td_server_info" ><asp:Label ID="lbIp" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >服务器域名</td>
        	    <td class="td_server_info" ><asp:Label ID="lbDomain" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >服务器端口</td>
        	    <td class="td_server_info" ><asp:Label ID="lbPort" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >服务器IIS版本</td>
        	    <td class="td_server_info" ><asp:Label ID="lbIISVer" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >本文件所在文件夹</td>
        	    <td class="td_server_info" ><asp:Label ID="lbPhPath" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >服务器操作系统</td>
        	    <td class="td_server_info" ><asp:Label ID="lbOperat" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >系统所在文件夹</td>
        	    <td class="td_server_info" ><asp:Label ID="lbSystemPath" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >服务器脚本超时时间</td>
        	    <td class="td_server_info" ><asp:Label ID="lbTimeOut" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >服务器的语言种类</td>
        	    <td class="td_server_info" ><asp:Label ID="lbLan" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >.NET Framework 版本</td>
        	    <td class="td_server_info" ><asp:Label ID="lbAspnetVer" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >服务器当前时间</td>
        	    <td class="td_server_info" ><asp:Label ID="lbCurrentTime" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >服务器IE版本</td>
        	    <td class="td_server_info" ><asp:Label ID="lbIEVer" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >服务器上次启动到现在已运行</td>
        	    <td class="td_server_info" ><asp:Label ID="lbServerLastStartToNow" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >逻辑驱动器</td>
        	    <td class="td_server_info" ><asp:Label ID="lbLogicDriver" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >CPU 总数</td>
        	    <td class="td_server_info" ><asp:Label ID="lbCpuNum" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >CPU 类型</td>
        	    <td class="td_server_info" ><asp:Label ID="lbCpuType" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >虚拟内存</td>
        	    <td class="td_server_info" ><asp:Label ID="lbMemory" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >当前程序占用内存</td>
        	    <td class="td_server_info" ><asp:Label ID="lbMemoryPro" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >Asp.net所占内存</td>
        	    <td class="td_server_info" ><asp:Label ID="lbMemoryNet" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >Asp.net所占CPU</td>
        	    <td class="td_server_info" ><asp:Label ID="lbCpuNet" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >当前Session数量</td>
        	    <td class="td_server_info" ><asp:Label ID="lbSessionNum" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >当前SessionID</td>
        	    <td class="td_server_info" ><asp:Label ID="lbSession" runat="server"></asp:Label></td>
            </tr>
            <tr class="tr_field">
        	    <td class="td_server_label" >当前系统用户名</td>
        	    <td class="td_server_info" ><asp:Label ID="lbUser" runat="server"></asp:Label></td>
            </tr> 
        </table>
        <cc1:PopupWin ID="popupMsn" runat="server" ActionType="OpenLink" DarkShadow="0, 0, 0"
            DockMode="BottomRight" GradientDark="255, 153, 0" GradientLight="251, 238, 187"
            LightShadow="255, 192, 128" Link="http://www.google.com" LinkTarget="_blank"
            Shadow="128, 64, 0" Style="z-index: 116; left: 367px; position: absolute; top: 225px"
            TextColor="0, 0, 0" Visible="False" Width="216px" HideAfter="4000" />
</asp:Content>

