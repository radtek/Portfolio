<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="errorpage.aspx.cs" Inherits="Johnny.CMS.admin.errorpage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>����ʧ��</title>
<link href="/admin/style/errorpage.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin-top:50px;">
    <table style="width:65%;height:180px;" border="0" align="center" cellspacing="1" cellpadding="5" class="table">
        <tr class="tr_bg_title">
            <td class="error_title" style="height:38px;" colspan="2"><font color="red"><img src="/admin/images/no.gif" />��Ǹ������ʧ��</font></td>
        </tr>
        <tr class="tr_bg_content">
            <td align="center"><img src="/admin/images/error.gif" border="0"></td>
            <td><asp:Label ID="lblMessage" runat="server"></asp:Label></br><font color="#395575">����������</font>
            <ul>
                <li>
                    <a href='/admin/login.aspx' class='list_link' target=_top>ת����¼ҳ��</a><br></span>
                </li>
                <li>
                    <a href='javascript:history.back();' class='list_link'>������һ��</a>
                </li>
            </ul>
            </td>
        </tr>
    </table>
</body>
</html>