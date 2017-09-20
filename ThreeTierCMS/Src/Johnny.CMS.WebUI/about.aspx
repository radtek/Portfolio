<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="about.aspx.cs" MasterPageFile="sehome.master" Inherits="Johnny.CMS.WebUI.about" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">

<div id="bd">
<div class="left-column">
	<div class="content-box" style="margin-top:15px;">
		<h5>关于本站</h5>	
		<p><asp:Literal ID="lblShortDescription" runat="server"></asp:Literal></p>
	</div>
	<div class="content-box" style="margin-top:15px;">
		<h5>所涉及的开源控件</h5>	
	</div>
	<div>
	<div class="content-link" style="margin-top:15px;">
        <asp:Literal ID="lblWebsites" runat="server"></asp:Literal>	
	</div>
	<div style="clear:both"></div>
	</div>
	<div class="content-box" style="margin-top:25px;">
		<div class="morelist"><a href="bestpractice.html">更多</a></div>
		<h5>相关技术及最佳实践</h5>	
	</div>
	<br/>
	<div class="big-box"><div class="big-box-inner">
        <asp:Literal ID="lblBestPractice" runat="server"></asp:Literal>
	</div></div>
</div>

<div class="right-column" style="padding-top:15px;">
	<div class="side-box"><div class="side-box-inner">
	<h5>自主开发的软件</h5>
	<ul class="features">
		<li><a href="softwarelist.html">.NET开发助手V1.0</a></li>
		<li><a href="softwarelist.html">开心助手V2.0（多账户版）</a></li>
		<li><a href="softwarelist.html">CMS内容管理系统V1.0</a></li>
	</ul>
	</div></div>
</div>
<div class="right-column" style="padding-top:5px;">
	<div class="side-box"><div class="side-box-inner">
	<h5>友情链接</h5>
	<ul class="features">
		<li><a href="softwarelist.html">CSDN</a></li>
		<li><a href="http://sourceforge.net/">SourceForge</a></li>
		<li><a href="softwarelist.html">CMS内容管理系统V1.0</a></li>
	</ul>
	</div></div>
</div>
<div style="clear:both"></div>
</div><!-- end bd -->
</asp:Content>
