<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Johnny.CMS.admin.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="Copyright" content="www.zhuangrong.net" />
    <title><%=LoginPageTitle%></title>
    <link rel="icon" href="../favicon.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="../favicon.ico" type="image/x-icon" /> 
    <link href="style/login.css" type="text/css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/login/top_bg.gif">
    <tr>
      <td style="height: 55px;width:10%;"></td>
      <td style="height: 55px;width:40%;">&nbsp;</td>
      <td style="height: 55px;width:50%;padding-right:10px;"></td>
    </tr>
  </table>

  <table width="100%" height="297" border="0" cellpadding="0" cellspacing="10">
    <tr>
      <td height="74" colspan="3">&nbsp;</td>
    </tr>
    <tr>
      <td width="442" valign="top"><div align="right">
        <table width="100%" style="height:160px" border="0" cellpadding="1" cellspacing="5">
          <tr>
            <td><div align="right"><img src="images/login/logo.gif" width="194" height="44" /></div></td>
          </tr>
          <tr>
            <td><div align="right" class="STYLE1">Johnny .NET CMS V1.0</div></td>
          </tr>
          
          <tr>
            <td></td>
          </tr>
        </table>
        </div></td>
      <td style="width:1px;" bgcolor="#CCCCCC"></td>
      <td style="width:508px;" valign="top">
        <table width="100%" style="height:193px" border="0" cellpadding="1" cellspacing="5">

          <tr>
            <td colspan="2"><div align="left">
                <asp:Label ID="lblAdminName" Runat="server"></asp:Label>&nbsp;
                <asp:TextBox ID="txtAdminName" runat="server" Width="129px" CssClass="username"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="VAdminName" runat="server" ControlToValidate="txtAdminName"></asp:RequiredFieldValidator><script language="javascript" type="text/javascript">document.getElementById('txtAdminName').focus();</script>
            </div></td>
          </tr>
          <tr>
            <td colspan="2"><div align="left">
                <asp:Label ID="lblPassword" Runat="server"></asp:Label>&nbsp;
                <asp:TextBox ID="txtPassword" runat="server"  CssClass="password" TextMode="Password" Width="129px" MaxLength="18"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="VPassword" runat="server" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                </div></td>
          </tr>
          <tr>
            <td style="width:53%;" align="left">
                <asp:Label ID="lblVerifyCode" Runat="server"></asp:Label>&nbsp;
                <asp:TextBox ID="txtVerifyCode" CssClass="vercode" runat="server" Width="52px"></asp:TextBox>&nbsp;
                <script type="text/javascript" language="JavaScript">                
                   var numkey = Math.random();
                   numkey = Math.round(numkey*10000);
                   document.write("<img src=\"../utility/verifycode.aspx?k="+ numkey +"\" onClick=\"this.src+=Math.random();return false;\" onfocus=\"this.blur();\" style=\"cursor:pointer;\" hspace=\"4\" style=\"vertical-align:bottom\"");
                </script>
            </td>
          </tr>
          <tr>
            <td style="width:53%;" align="left">
                <asp:Label ID="lblStatus" runat="server" CssClass="error"></asp:Label></td>
          </tr>
          <tr>
            <td colspan="2" style="height:36px;">
                <asp:ImageButton ID="imgbtnLogin" runat="server" ImageUrl="images/login/signin.gif" OnClick="imgbtnLogin_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="imgbtnCancel" runat="server" ImageUrl="images/login/reset.gif" OnClientClick="javascript:window.close(); return;" />
            </td>
          </tr>
        </table></td>
    </tr>
  </table>
  <br />
  <br />
  <br />
  <br />
  <br />
  <table width="100%" height="56" border="0" cellpadding="0" cellspacing="0" background="images/login/bottom_bg.gif">
    <tr>
      <td><div align="center" class="STYLE2">
          <span style="font-size:10px;"><asp:Label ID="lblCopyRight" Runat="server"></asp:Label></span>
          </div></td>
    </tr>
  </table>
</form>
</body>
</html>
