<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmplReportViewer.aspx.cs" Inherits="SMPL46.Reports.SmplReportViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <rsweb:reportviewer id="rptViewer" runat="server" borderstyle="None" clientidmode="AutoID" pagecountmode="Actual" processingmode="Remote" viewstatemode="Enabled" internalborderstyle="Ridge" waitcontroldisplayafter="10" showdocumentmapbutton="False">
            </rsweb:reportviewer>
        </div>
    </form>
</body>
</html>
