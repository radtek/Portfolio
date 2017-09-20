<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="top.aspx.cs" Inherits="Johnny.CMS.admin.top" %>

<%@ Register Assembly="Johnny.Controls.Web" Namespace="Johnny.Controls.Web.WebTab" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="style/topmenu.css" type="text/css" rel="stylesheet" />
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
    <form id="form1" runat="server">
    <div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="background-image:url(images/topmenu/logo_bg.jpg);width:100%;height:46px">
      <tr>
        <td>
          <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
              <td class="td_field_logo"><img id="" src="images/topmenu/logo.gif" alt="logo" /></td>
              <td class="td_field_user"><asp:Label ID="lblWelcome" Runat="server"></asp:Label><asp:HyperLink ID="lblLogonUser" runat="server" NavigateUrl="access/profile.aspx" target="mainFrame" class="hyperlink" Text="[lblLogonUser]" ></asp:HyperLink></td>
              <td class="td_field_link">
                  <asp:HyperLink ID="linkLogout" runat="server" NavigateUrl="~/admin/logout.aspx" Target="_top" class="hyperlink"></asp:HyperLink>&nbsp;©®&nbsp;
                  <asp:HyperLink ID="linkHomepage" runat="server" NavigateUrl="~/index.aspx" Target="_top" class="hyperlink"></asp:HyperLink>&nbsp;©®&nbsp;
                  <asp:HyperLink ID="linkProfile" runat="server" NavigateUrl="~/admin/access/profile.aspx" Target="mainFrame" class="hyperlink"></asp:HyperLink>&nbsp;©®&nbsp;
                  <asp:HyperLink ID="linkPassword" runat="server" NavigateUrl="~/admin/access/passwordreset.aspx" Target="mainFrame"  class="hyperlink"></asp:HyperLink>&nbsp;©®&nbsp;
                  <asp:HyperLink ID="linkAbout" runat="server" NavigateUrl="~/admin/admin_about.aspx" Target="mainFrame" class="hyperlink"></asp:HyperLink>&nbsp;©®&nbsp;
                  <asp:HyperLink ID="linkHelp" runat="server" NavigateUrl="~/admin/admin_help.aspx" Target="mainFrame" class="hyperlink"></asp:HyperLink>&nbsp;&nbsp;
              </td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
    <cc1:WebTab ID="WebTab1" runat="server" ScriptPath="" SelectedIndex="0" />
    </div>    
    </form>
</body>
</html>
<script language="JavaScript" type="text/javascript">
//var defaultTab = document.getElementById('SouEiTabPage1');

//defaultTab.className='button_select';
var theDownedButtonObj=document.getElementById('JohnnyTabPage0');
function CheckBTN1(theObj,URL,URLmain)
{
    if (theObj.className!='button_select')
    {
        theObj.className='button_select';
        theDownedButtonObj.className='button_down';
        theDownedButtonObj=theObj;
        top.frames["leftFrame"].location=URL;
        top.frames["mainFrame"].location=URLmain;        
    }

}
</script>