<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="websites.aspx.cs" MasterPageFile="sehome.master" Inherits="Johnny.CMS.WebUI.websites" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">

<div id="bd">
<div class="left-column">
	<h3>实用网址</h3>
	<p style="margin-left:10px">整理了一些常用的网址链接，希望对大家也有用。如果你有好的网站链接，可以通过右边的“我也来推荐”来增加链接。</p>
	<asp:Literal ID="lblWebsites" runat="server"></asp:Literal>	
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
