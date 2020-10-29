<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthWiseInfo.aspx.cs" Inherits="MessManagementSystem.Reports.Viewer.MonthWiseInfo" %>

<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
        <div style="position: relative">
            <asp:Button ID="printButton" runat="server" OnClick="printButton_Click" Text="" />
            <iframe id="frmPrint" name="IframeName" width="100%" runat="server" style="display: none"></iframe>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="1500">
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
