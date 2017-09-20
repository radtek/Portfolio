<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_SendMail.aspx.cs" Inherits="Johnny.CMS.Admin.Admin_SendMail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Width="324px">ajohn_2000zr@hotmail.com</asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></div>
    <asp:TextBox ID="TextBox2" runat="server" Height="142px" TextMode="MultiLine" 
        Width="215px"></asp:TextBox>
    </form>
</body>
</html>
