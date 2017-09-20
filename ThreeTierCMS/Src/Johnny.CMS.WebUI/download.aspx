<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="download.aspx.cs" MasterPageFile="sehome.master" Inherits="Johnny.CMS.WebUI.download" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">

<div id="bd">
<div class="left-column">
	<h3>下载软件</h3>
	<div class="big-box-solid">		
	    <div class="big-box-solid-inner">	    	
		<h4>注意</h4>
		<p>引用请注明出处，版权归本作者所有。此处所有软件都是基于.NET开发，如要正常使用，必须首先安装微软的.NET Framework 2.0。<a href="download.php?dl=extjs310">下载.NET Framework2.0</a></br>注意：Vista及Windows 7已经内置了该工具，无需再安装。
		</p>
	    </div>		
	</div>
	<div class="big-box"><div class="big-box-inner">
		<h5><asp:Literal ID="lblSoftwareName" runat="server"></asp:Literal></h5>
		<table cellspacing="0" class="dl">
			<tr>
				<td>
					<b><asp:Literal ID="lblReleaseName" runat="server"></asp:Literal></b>
					<asp:Literal ID="lblReleaseDescription" runat="server"></asp:Literal>
				</td>
				<td style="width:80px;">
					<asp:Literal ID="lblDownloadUrl" runat="server"></asp:Literal>
				</td>
			</tr>	
			<tr>
				<td>
					<b><asp:Literal ID="lblDocumentTitle" runat="server"></asp:Literal></b>
					<p><asp:Literal ID="lblDocumentDescription" runat="server"></asp:Literal></p>
				</td>
				<td style="width:80px;">
					<asp:Literal ID="lblDocumentUrl" runat="server"></asp:Literal>
				</td>
			</tr>
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
