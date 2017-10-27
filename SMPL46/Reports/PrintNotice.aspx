<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintNotice.aspx.cs" Inherits="SMPL46.Reports.PrintNotice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Notice</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <img height="73" alt="LB Ealing" src="../images/lo_ealing.gif" width="128"/>
        <br/>
    </div>
    <div>
        <asp:Literal ID="litContent" runat="server" ></asp:Literal>
    </div>
    </form>
</body>
</html>
