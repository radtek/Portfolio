<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Johnny.CMS.admin.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head>
        <title><%=AdminPageTitle%></title>
    </head>  
    <frameset rows="84,*" frameborder="NO" border="0" framespacing="0" name="topset">
	    <frame name="topFrame" scrolling="NO" noresize src="top.aspx">
	    <frameset cols="175,*" framespacing="0" frameborder="no" border="0" name="middleset" >
            <frame name="leftFrame" noresize marginWidth="0" marginHeight="0" scrolling="no" src="menu.aspx">
		    <frame name="mainFrame" src="display.aspx">
	    </frameset>
    </frameset>
    <noframes>
	    <body bgcolor="#FFFFFF">
	        <p><asp:Literal ID="lblFrame" Runat="server"></asp:Literal></p>
	    </body>
    </noframes>
</html>
