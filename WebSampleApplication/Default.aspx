<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebSampleApplication._Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="QueryText" runat="server"></asp:TextBox>
        <asp:Button ID="SubmitButton" runat="server" Text="Search" />
        <br />
        <asp:Literal ID="ResultHtml" runat="server" />
    
    </div>
    </form>
</body>
</html>
