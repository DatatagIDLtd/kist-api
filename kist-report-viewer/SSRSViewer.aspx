<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SSRSViewer.aspx.cs" Inherits="kist_report_viewer.SSRSViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html style="width : 100%; height: 100%; margin: 0; padding: 0;" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="width : 100%; height: 100%; margin: 0; padding: 0;"  >
    <form runat="server"  style="width : 100%; height: 100%; margin: 0; padding: 0;">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ssrsViewer" runat="server" ShowPrintButton="false" Width="100%" Height="600px" AsyncRendering="true" ZoomMode="Percent" KeepSessionAlive="true" SizeToReportContent="false"></rsweb:ReportViewer>
    </form>
</body>
</html>
