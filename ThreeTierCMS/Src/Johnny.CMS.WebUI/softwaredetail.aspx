<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="softwaredetail.aspx.cs" MasterPageFile="sehome.master" Inherits="Johnny.CMS.WebUI.softwaredetail" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">

<div id="bd">
<div class="left-column">
	<h3><asp:Literal ID="lblSoftwareName" runat="server"></asp:Literal></h3>
	<div>		
		<div style="text-align:center;"><span><asp:Literal ID="lblUpdateTime" runat="server"></asp:Literal></span><span style="padding-left:10"><a href="#support-table">我要评论(<span style="color:#cc0000;">10</span>)</a></span></div>
		<div style="float:right;margin-top:-18;">[<a href="javascript:bodytoft('ArtBody')">繁&nbsp;</a>|<a href="javascript:bodytojt('ArtBody')">&nbsp;简</a>]&nbsp;
                        [<a href="javascript:fontSize('b','ArtBody')">大&nbsp;</a>|<a href="javascript:fontSize('c','ArtBody')">&nbsp;中&nbsp;</a>|<a href="javascript:fontSize('m','ArtBody')">&nbsp;小</a>] </div>
	</div>
	<br>
    <div class="articlecontent" id="ArtBody">	
	　　<asp:Literal ID="lblDescription" runat="server"></asp:Literal>
    </div>
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
