<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="blog.aspx.cs" MasterPageFile="sehome.master" Inherits="Johnny.CMS.WebUI.blog" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">

<div id="bd">
<link rel="stylesheet" type="text/css" href="/assets/css/blog.css" />
<div class="left-column">
        <div>    		
			<asp:Literal ID="lblBlogList" runat="server"></asp:Literal>
		</div>
</div>
<div class="right-column">
    <ul>
		<li><EMBED src="flash/clock.swf" width=200 height=200 type=application/x-shockwave-flash wmode="transparent" ></li>
		<li>
			<form method="get" id="searchform" action="http://www.extjs.com/blog/">
				<div class="side-box"><div class="side-box-inner" style="padding:0 8px 8px;">
					<input style="width:120px;" type="text" value="" name="s" id="s" />
					<input type="submit" id="searchsubmit" value="Search" />
				</div></div>
			</form>
		</li>
		<li>
			<div class="side-box"><div class="side-box-inner">
			<h5>博客档案</h5>
			<ul class="features">
				<li><a href='http://www.extjs.com/blog/2009/12/' title='December 2009'>2010年1月 (1) </a></li>
				<li><a href='http://www.extjs.com/blog/2009/11/' title='November 2009'>2009年12月 (3) </a></li>
				<li><a href='http://www.extjs.com/blog/2009/11/' title='November 2009'>2009年12月 (3) </a></li>
				<li><a href='http://www.extjs.com/blog/2009/11/' title='November 2009'>2009年12月 (3) </a></li>
				<li><a href='http://www.extjs.com/blog/2009/11/' title='November 2009'>2009年12月 (3) </a></li>
				<li><a href='http://www.extjs.com/blog/2009/11/' title='November 2009'>2009年12月 (3) </a></li>
				<li><a href='http://www.extjs.com/blog/2009/11/' title='November 2009'>2009年12月 (3) </a></li>
				<li><a href='http://www.extjs.com/blog/2009/11/' title='November 2009'>2009年12月 (3) </a></li>
				<li><a href='http://www.extjs.com/blog/2009/11/' title='November 2009'>2009年12月 (3) </a></li>
				<li><a href='http://www.extjs.com/blog/2009/11/' title='November 2009'>2009年12月 (3) </a></li>
				<li><a href='http://www.extjs.com/blog/2009/11/' title='November 2009'>2009年12月 (3) </a></li>
				<li><a href='http://www.extjs.com/blog/2009/11/' title='November 2009'>2009年12月 (3) </a></li>
				<li><a href='http://www.extjs.com/blog/2009/11/' title='November 2009'>2009年12月 (3) </a></li>
				<li><a href='http://www.extjs.com/blog/2009/11/' title='November 2009'>2009年12月 (3) </a></li>
			</ul>
			</div></div>
		</li>
		<li>
			<div class="side-box"><div class="side-box-inner">
			<h5>博客分类</h5>
			<ul class="features">
				<li><a href='http://www.extjs.com/blog/2009/12/' title='December 2009'>ASP.NET (1) </a></li>
				<li><a href='http://www.extjs.com/blog/2009/11/' title='November 2009'>ABAP (3) </a></li>
				<li><a href='http://www.extjs.com/blog/2009/11/' title='November 2009'>AJAX (3) </a></li>
				<li><a href='http://www.extjs.com/blog/2009/11/' title='November 2009'>Flex (3) </a></li>
			</ul>
			</div></div>
		</li>
	</ul>
</div>
<div style="clear:both"></div>
</div><!-- end bd -->
</asp:Content>
