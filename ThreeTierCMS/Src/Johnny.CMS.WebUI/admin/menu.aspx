<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menu.aspx.cs" Inherits="Johnny.CMS.admin.menu" %>

<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.LeftMenu" TagPrefix="zr" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="style/leftmenu.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <table class="leftmenu_bd">
        <tr><td valign="top">        
          <zr:LeftMenu ID="LeftMenu1" runat="server"></zr:LeftMenu>
        </td></tr>
     </table>
    <script language="javascript" type="text/javascript">
     function show_hide(DivID,ImgID)
     {
        if (document.getElementById(DivID).style.display=='none')
        {
            document.getElementById(DivID).style.display='block';
            document.getElementById(ImgID).src='images/leftmenu/menu_expand.gif';
        }
        else
        {
            document.getElementById(DivID).style.display='none';
            document.getElementById(ImgID).src='images/leftmenu/menu_collapse.gif';
        }
    }
    </script>
    </form>
</body>
</html>
