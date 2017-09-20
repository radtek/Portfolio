<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="releasehistory.aspx.cs" MasterPageFile="sehome.master" Inherits="Johnny.CMS.WebUI.releasehistory" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">

<div id="bd">
<div class="left-column">
	<h3>历史版本</h3>
	<div class="big-box-solid">		
	    <div class="big-box-solid-inner">	    	
			<h4>注意</h4>
			<p>引用请注明出处，版权归本作者所有。</p>			
	    </div>		
	</div>
	<div class="big-box"><div class="big-box-inner">				
		<table cellspacing="0" class="dl">
			<tr>
				<td class="software"><asp:Literal ID="lblSoftwareName" runat="server"></asp:Literal></td>
				<td class="download"><a href="download.aspx">下载最新版本</a></td>
			</tr>
		</table>
		<table cellspacing="0" class="dl">			
			<asp:Literal ID="lblReleaseList" runat="server"></asp:Literal>
		</table>		
	</div></div>	
	
</div>

<div class="right-column" style="padding-top:15px;">
	<div class="side-box"><div class="side-box-inner">
		<h5>In this Section</h5>
		<ul class="features">
			<li><a href="/company/">纠错</a></li><li><a href="/company/dual.php">我也来推荐</a></li><li><a href="/company/customers.php">Customers</a></li><li><a href="/company/contact.php">Contact Us</a></li>		</ul>
	</div></div>
</div>

<div style="clear:both"></div>
</div><!-- end bd -->
</asp:Content>
