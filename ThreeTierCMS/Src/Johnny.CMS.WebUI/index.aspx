<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" MasterPageFile="sehome.master" Inherits="Johnny.CMS.WebUI.index" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">
<link rel="stylesheet" type="text/css" href="css/index.css" />

	
<div id="bd">
<link rel="stylesheet" type="text/css" href="css/index.css" />

<div class="full-box" style="margin-top:15px;">
	<div class="full-box-inner" style="position:relative;zoom:1;">
		<div class="home">
			<p class="home-title">Johnny的个人网站</p>
			<p class="home-subtitle">——纯粹的编程爱好者</p>
			<p class="home-description">我虽然爱好编程，但不是技术狂人。在我看来，写出来的软件有用才是最重要的，而不是追求一些花哨的技巧。选择了这个行业，注定要一辈子学习。我所作的很多东西都是慢慢积累而来，分享出来，希望对大家有用。天道酬勤，坚持到底！</p>			
			<ul class="features" style="float:left;width:180px;">
				<li><a href="sapindex.aspx">SAP</a></li>
				<li><a href="mysoftware.aspx">软件项目</a></li>
				<li><a href="blog.aspx">Blog</a></li>
			</ul>
			<ul class="features" style="float:left;width:180px;margin-left:20px;">
				<li><a href="dotnetindex.aspx">.NET</a></li>
				<li><a href="dotnetindex.html">共享网盘</a></li>
				<li><a href="favorite.aspx">收藏</a></li>
			</ul>
			<p class="home-more"style="float:left;"><a href="softwaredetail.html"><img src="images/indexmore.png" /></a></p>		
		</div>		
		<div class="home-logo" style="float:right;"><a href="softwaredetail.html"><img width="340" height="230" src="images/indexpage.png" /></a></div>		
	</div>
</div>

<div id="home">
<div class="left-column">
	<div class="home-block">
        <asp:Literal ID="lblSoftware1" runat="server"></asp:Literal>
	</div>	
	<h5 style="margin-top:20px;">
		<div class="rss"><a href="http://feeds.feedburner.com/extblog" title="RSS Feed"></a></div>
		Blog
	</h5>
	<ul class="features box">
        <asp:Literal ID="lblBlogList" runat="server"></asp:Literal>
		<li><a href="blog.aspx"><b>更多 <img class="arrowimage" src="images/arrow.png" /></b></a></li>
	</ul>
</div>

<div class="right-column">
	<div class="home-block">
		<asp:Literal ID="lblSoftware2" runat="server"></asp:Literal>
	</div>

	<h5 style="margin-top:20px;">实用网址</h5>
	<div class="box">		
		<div class="favorites">
			<asp:Literal ID="lblWebsites" runat="server"></asp:Literal>	
		</div>
	</div>	
</div>

</div>

<div style="clear:both"></div>
</div><!-- end bd -->
</asp:Content>
