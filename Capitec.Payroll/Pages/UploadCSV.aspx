<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadCSV.aspx.cs" Inherits="Capitec.Payroll.Pages.UploadCSV" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 287px;
        }
        .auto-style2 {
            text-align: center;
            margin-left: 80px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width:100%;">
                <tr>
                    <td colspan="2">Upload CSV</td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label1" runat="server" Text="Select File"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload" runat="server"  />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">&nbsp;</td>
                    <td>
                        <asp:Button ID="btnUpload" runat="server" Text="Upload File" OnClick="btnUpload_Click" />
                        <br />
                        <asp:Label ID="ErrorMessage" runat="server"></asp:Label>
                        <br />
                        <asp:Button ID="btnViewReport" runat="server" OnClick="btnViewReport_Click" Text="View Report" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="2">
                        <asp:GridView ID="GridView1" runat="server">
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="2">&nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
