<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestForm.aspx.cs" Inherits="TestForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 53%;
        }
        .style2
        {
            width: 126px;
        }
        .style3
        {
            color: #FF0066;
        }
        .style4
        {
            width: 126px;
            color: #FF0066;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <tr>
                <td class="style4">
                    &nbsp;</td>
                <td class="style3">
                    NOTE: This page points to test dev webservice of Site Builder</td>
            </tr>
            <tr>
                <td class="style2">
                    Site Id</td>
                <td>
                    <asp:TextBox ID="txtSiteId" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Entity Id</td>
                <td>
                    <asp:TextBox ID="txtEntityId" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Folder Path</td>
                <td>
                    <asp:TextBox ID="txtFolderPath" runat="server" Width="192px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Domain Id</td>
                <td>
                    <asp:TextBox ID="txtDomain" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td>
                    <asp:HyperLink ID="hlink_testdev" runat="server" 
                        NavigateUrl="~/SiteExport.aspx">To Export from SB prod, click here</asp:HyperLink>
                </td>
            </tr>
        </table>
    
    </div>
    <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
        Text="Submit" />
    </form>
</body>
</html>
