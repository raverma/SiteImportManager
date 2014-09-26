<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeFile="SiteHelp.aspx.cs" Inherits="SiteHelp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
// <![CDATA[
    </script>
    <script language="javascript" type="text/javascript">
     //Start:-- code changes by sumit as on 28th April for Pop up Window implementation
    function GetRowValue(val, sname, id)
    {
        // ControlID can instead be passed as query string to the popup window
        //alert(sname);
        if(id=="SiteSearch.aspx") {
            window.opener.document.getElementById("ctl00_MainContent_txtSiteName").value = sname;
            window.opener.document.getElementById("ctl00_MainContent_txtSiteId").value = val;
        }
        else {
            window.opener.document.getElementById("ctl00_MainContent_txtSiteID").value = val;
        }
        window.close();
    }
    //End:-- code changes by sumit as on 28th April for Pop up Window implementation
</script>

</head>
<body onload="doInit()">
    <form id="form1" runat="server">
    <div>
        Enter the first few characters for Site Name to search:<br />
&nbsp;<asp:TextBox ID="txtSiteName" runat="server" Width="259px"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" 
            onclick="btnSearch_Click" />
    </div>

        <div style="width: 770px">  <asp:GridView ID="grdSearchResult" runat="server" 
            Width="766px"  DataKeyNames="bsi_site_id"
            onrowdatabound="grdSearchResult_RowDataBound" 
            onselectedindexchanged="grdSearchResult_SelectedIndexChanged" 
            AllowPaging="True" 
            onpageindexchanging="grdSearchResult_PageIndexChanging" 
            onsorting="grdSearchResult_Sorting" ShowFooter="True" CellPadding="4" 
                ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="bsi_site_id">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("bsi_site_id") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("bsi_site_id") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="bsi_title">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("bsi_title") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("bsi_title") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="bsi_site_type" HeaderText="bsi_site_type">
                    </asp:BoundField>
                </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    </asp:GridView>
        <asp:HiddenField ID="hiddenText" runat="server" />
        <asp:HiddenField ID="hiddenSiteName" runat="server" />
        <input id="btnOk"  value="Close" type="button"
                onclick="Done()" /></div>
    <div>
    <table>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    </table></div>
    </form>
</body>
</html>
