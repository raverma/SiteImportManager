<%@ Page Language="C#" EnableEventValidation="false"  AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form2" runat="server">
    <div>
       
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:GridView ID="MyGridView" runat="server"
OnRowDataBound="MyGridView_RowDataBound"
OnSelectedIndexChanged="MyGridView_SelectedIndexChanged"
DataKeyNames="SiteID">
</asp:GridView>
<asp:Button ID="Button1" runat="server" Text="Update data" 
        PostBackUrl="~/Default.aspx" />

  
    </div>
    </form>
</body>
</html>
