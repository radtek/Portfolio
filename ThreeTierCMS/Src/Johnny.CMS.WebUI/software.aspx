<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="software.aspx.cs" MasterPageFile="sehome.master" Inherits="Johnny.CMS.WebUI.software" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">

<div id="bd">
<div class="left-column">
	<h3>软件项目一览</h3>
	<p>所有软件都是基于.NET技术开发，纯粹是编程爱好的成果。</p>
	<asp:Literal ID="lblWebsites" runat="server"></asp:Literal>	
</div>

<div class="right-column" style="padding-top:15px;">
	<div class="side-box"><div class="side-box-inner">
		<h5>下载软件</h5>
		<ul class="features">
			<li><a href="download.html">开心助手V2.0（多账户版）</a></li>
			<li><a href="download.html">.NET开发助手V1.0</a></li>
			<li><a href="download.html">CMS内容管理系统V1.0</a></li>
		</ul>
	</div></div>
</div>

<div style="clear:both"></div>
</div><!-- end bd -->
</asp:Content>
