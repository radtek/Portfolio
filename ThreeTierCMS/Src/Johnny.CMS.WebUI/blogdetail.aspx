<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="blogdetail.aspx.cs" MasterPageFile="sehome.master" Inherits="Johnny.CMS.WebUI.blogdetail" %>

<asp:Content ID="cntPage" ContentPlaceHolderID="cphPage" runat="Server" EnableViewState="false">

<div id="bd">
<div class="left-column">
	<h3 style="text-align:center;"><asp:Literal ID="lblTitle" runat="server"></asp:Literal></h3>
	<div>		
		<div style="text-align:center;"><span><asp:Literal ID="lblUpdateTime" runat="server"></asp:Literal></span><span style="padding-left:10"><a href="#support-table">我要评论(<span style="color:#cc0000;">10</span>)</a></span></div>
		<div style="float:right;margin-top:-18;">[<a href="javascript:bodytoft('ArtBody')">繁&nbsp;</a>|<a href="javascript:bodytojt('ArtBody')">&nbsp;简</a>]&nbsp;
                        [<a href="javascript:fontSize('b','ArtBody')">大&nbsp;</a>|<a href="javascript:fontSize('c','ArtBody')">&nbsp;中&nbsp;</a>|<a href="javascript:fontSize('m','ArtBody')">&nbsp;小</a>] </div>
	</div>
	<br>
    <div class="articlecontent" id="ArtBody">	
	　　<asp:Literal ID="lblDescription" runat="server"></asp:Literal>
    </div>
    <div class="mframe">
	<div class="tl"></div>
	<div class="tr"></div>
	<div class="tm">
		<span class="tt">本文评论</span>
		 
	</div>
	<div class="wrapper">
	<div class="ml"></div>
	<div class="mr"></div>
	<div class="mm">
				<div style="padding:5px">
			<div>    2012/11/24 19:08:36 | admin    <div style=" border-bottom: 1px dotted #000000;">        <p>宝宝出生啦宝宝出生啦宝宝出生啦宝宝出生啦</p>        <div style="clear:both;"></div>    </div></div>
			<div>    2012/11/24 19:08:36 | admin    <div style=" border-bottom: 1px dotted #000000;">        <p>宝宝出生啦宝宝出生啦宝宝出生啦宝宝出生啦</p>        <div style="clear:both;"></div>    </div></div>
			
			</div>
	</div>
	</div>
	<div class="bl"></div>
	<div class="br"></div>
	<div class="bm"></div>
	</div>

	<div class="mframe">
	<div class="tl"></div>
	<div class="tr"></div>
	<div class="tm">
		<span class="tt">发表评论</span>
	</div>
	<div class="wrapper">
	<div class="ml"></div>
	<div class="mr"></div>
	<div class="mm">
		<form id="remarkForm" action="remark.aspx?id=100" method="post" onsubmit="return checkRemark();" style="padding:5px">
			<script type="text/javascript" language="javascript">
			    function checkRemark() {
			        var form = document.getElementById("remarkForm");
			        var remarkSize = 200;
			        if (form.body.value == "") {
			            alert("请填写评论内容");
			            form.body.focus();
			            return false;
			        }
			        if (form.username.value == "") {
			            alert("请填写姓名");
			            form.username.focus();
			            return false;
			        }
			        if (form.body.value.length > remarkSize) {
			            form.body.value = form.body.value.substr(0, remarkSize);
			            showLen(form.body);
			            form.body.focus();
			            alert("评论内容不可以超过" + remarkSize + "字,已帮你删除多余部分");
			            return false;
			        }
			        if (form.username.value.length > 10) {
			            alert("姓名不可以超过10个字");
			            form.username.focus();
			            return false;
			        }
			        form.submit.disabled = true;
			        form.vcode.value = VCode("中移动、网通董事长：TD能赶上奥运时机");
			        return true;
			    }
			    function showLen(obj) {
			        document.getElementById("bodyLen").value = obj.value.length;
			    }
			</script>
			<p style="background: #fdffd2; border:1px solid #ddd; padding: 12px; margin:12px 0 16px;">请对您的言行负责，并遵守中华人民共和国有关法律法规，尊重网上道德。</p>
			<input type="radio" name="face" value="1" checked="checked"/><img src="images/face/icon_1.gif" 
			alt=""/><input type="radio" name="face" value="2"/><img src="images/face/icon_2.gif" 
			alt=""/><input type="radio" name="face" value="3"/><img src="images/face/icon_3.gif" 
			alt=""/><input type="radio" name="face" value="4"/><img src="images/face/icon_4.gif" 
			alt=""/><input type="radio" name="face" value="5"/><img src="images/face/icon_5.gif" 
			alt=""/><input type="radio" name="face" value="6"/><img src="images/face/icon_6.gif" 
			alt=""/><input type="radio" name="face" value="7"/><img src="images/face/icon_7.gif" 
			alt=""/><input type="radio" name="face" value="8"/><img src="images/face/icon_8.gif" 
			alt=""/><input type="radio" name="face" value="9"/><img src="images/face/icon_9.gif" 
			alt=""/><input type="radio" name="face" value="10"/><img src="images/face/icon_10.gif" 
			alt=""/><br/><input type="radio" name="face" value="11"/><img src="images/face/icon_11.gif" 
			alt=""/><input type="radio" name="face" value="12"/><img src="images/face/icon_12.gif" 
			alt=""/><input type="radio" name="face" value="13"/><img src="images/face/icon_13.gif" 
			alt=""/><input type="radio" name="face" value="14"/><img src="images/face/icon_14.gif" 
			alt=""/><input type="radio" name="face" value="15"/><img src="images/face/icon_15.gif" 
			alt=""/><input type="radio" name="face" value="16"/><img src="images/face/icon_16.gif" 
			alt=""/><input type="radio" name="face" value="17"/><img src="images/face/icon_17.gif" 
			alt=""/><input type="radio" name="face" value="18"/><img src="images/face/icon_18.gif"
			alt=""/><input type="radio" name="face" value="19"/><img src="images/face/icon_19.gif"
			alt=""/><input type="radio" name="face" value="20"/><img src="images/face/icon_20.gif" alt=""/><br/>
			<div style="padding-top:5px">
			点评：
			<textarea name="body" cols="48" rows="4" onkeydown="showLen(this)" onkeyup="showLen(this)"></textarea> 字数<input type="text" id="bodyLen" size="3" readonly="readonly" style="border-width:0;background:transparent;"/>
			</div>
			<div style="padding-top:5px">
			姓名：
			<input type="text" id="i_username" name="username" value="" maxlength="15" size="15"/>
			<input type="submit" name="submit" id="i_submit" value=" 发 表 "/>
			</div>
			<script type="text/javascript">
			    var remarkmember = false;
			    var allowremark = true;
			    if (remarkmember) {
			        document.getElementById("i_username").readonly = true;
			        document.write("(限会员登录后发表评论)");
			    }
			    if (!allowremark) {
			        document.getElementById("i_submit").disabled = true;
			    }
			</script>
			<script type="text/javascript" src="inc/clientDate.js"></script>
			<input type="hidden" name="vcode" value=""/>
			
		</form>
	</div>
	</div>
	<div class="bl"></div>
	<div class="br"></div>
	<div class="bm"></div>
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
