<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="favorite.aspx.cs" MasterPageFile="sehome.master" Inherits="Johnny.CMS.WebUI.favorite" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">

<div id="bd">
<div class="left-column">
	<div class="content-box">
		<h3>收藏列表</h3>
		<div style="padding:10px 0;">
		<ul class="features outline">
			<li>
				<h4><a href="websites.aspx">实用网址</a></h4>
				<p>
					一些实用的网址包括生活，工作，编程开发等。
				</p>
			</li>
			<br/>
			<li>
				<h4><a href="websites.html">开发资源</a></h4>
				<p>
					开发软件时常用的图标，工具等资源。
				</p>
			</li>
			<br/>
		</ul>
		</div>		
	</div>
</div>

<div class="right-column" style="padding-top:15px;">
	<div class="side-box"><div class="side-box-inner">
	<h5>In this Section</h5>
	<ul class="features">
		<li><a href="/company/">Meet the Ext Team</a></li><li><a href="/company/dual.php">Dual Licensing Model</a></li><li><a href="/company/customers.php">Customers</a></li><li><a href="/company/contact.php">Contact Us</a></li>		</ul>
</div></div>
	</div>

<div style="clear:both"></div>
</div><!-- end bd -->
</asp:Content>
