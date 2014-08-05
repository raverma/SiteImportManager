
<%@ Page Title="Insert SiteID" Language="C#" MasterPageFile="~/Site.master" EnableEventValidation="true"
AutoEventWireup="true"  CodeFile="Initiate.aspx.cs" 
Inherits="_Default" %>
<%@ Register Assembly="AjaxControlToolkit" 
Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>



<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
 <link type="text/css" rel="stylesheet" href="Styles/style.css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   
<body>
    <script type="text/javascript">
    //Start:-- code changes by sumit as on 28th April for Pop up Window implementation
    function OpenPopup() {
        window.open("SiteHelp.aspx?pagename=Initiate.aspx","List","scrollbars=yes,resizable=yes,width=400,height=400");
    return false;
}
   //End:-- code changes by sumit as on 28th April for Pop up Window implementation
</script>
    <div><asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label></div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<br />
        <div>
        <asp:Label ID="Label1" runat="server" Text="SiteID: " Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtSiteID" CssClass="tb6" runat="server"></asp:TextBox>
       
             <asp:Button ID="btnSearch" runat="server" 
            Text="Search By Name" 
        onclick="btnSearch_Click" />
            <%--<asp:LinkButton ID="lnkhelp" runat="server"  Text="Help" ></asp:LinkButton>--%>
       </div>
    <br />
    <div>
        <asp:Label ID="Label2" runat="server" Text="Environment: " Font-Bold="True"></asp:Label>&nbsp;
        <asp:RadioButtonList ID="rblPxEnvironment" runat="server" AutoPostBack = "true"
            DataTextField = "EnvShortDesc" DataValueField = "EnvID" 
            onselectedindexchanged="rblPxEnvironment_SelectedIndexChanged">
        </asp:RadioButtonList>
        
    </div>
    <br />
   <div>
        <asp:Label ID="Label5" runat="server" Text="DomainID: " Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp
        <asp:DropDownList ID="ddlDomain" CssClass="tb6" runat="server" DataTextField = "Name" DataValueField = "DomainId"></asp:DropDownList>
      </div>      
    <br />
    
    <div>
        <asp:Label ID="Label4" runat="server" Text="Dest Server Type: " Font-Bold="True"></asp:Label>&nbsp;
        <asp:RadioButtonList ID="rblDestServerType" runat="server" DataTextField = "DestServerTypeDesc" DataValueField = "DestServerTypeID">
        </asp:RadioButtonList>
        
    </div>
    <br />

    <div>
    <table>
    <tr><td colspan="3"><asp:Label ID="lblStatus" runat="server" Text="Set Status" Font-Bold="true"/></td></tr>
    <tr><td colspan="3">
        <asp:RadioButton ID="optReady" runat="server" GroupName="Status" 
            Text="Ready to Process" Checked="True" />
        </td></tr>
    <tr><td colspan="3">
        <asp:RadioButton ID="optNotReady" runat="server" GroupName="Status" 
            Text="Not Ready to Process" />
        </td></tr>
    <tr><td>
        <asp:RadioButton ID="optScheduled" runat="server" GroupName="Status"
            Text="Scheduled for Date" oncheckedchanged="optScheduled_CheckedChanged1"/>
        <asp:TextBox ID="txtImportDt" runat="server" EnableViewState = "True" Enabled="False"></asp:TextBox>
        <asp:Button ID="btnCalendar"  runat="server" Text="..." onclick="btnCalendar_Click" />
        <asp:Calendar ID="Calendar1" runat="server"  Visible ="false"
            onselectionchanged="Calendar1_SelectionChanged"></asp:Calendar>
        </td>
    </tr>
    
    
     
    </table>
    </div>
    
    <strong>Comments</strong><br />
    
    <asp:TextBox ID="txtComments" TextMode="MultiLine" runat="server" Height="50px" 
        Width="548px"></asp:TextBox>
    
    <br />
    <br />
  <asp:HiddenField ID="hiddenText" runat="server" />
        <asp:HiddenField ID="hiddenSiteName" runat="server" />  
<asp:GridView ID="gvAgilixImport" runat="server" Visible  ="False" Width="904px" 
            CellPadding="1" ForeColor="#333333" GridLines="None" 
            AutoGenerateColumns="False" 
            onselectedindexchanged="gvAgilixImport_SelectedIndexChanged" 
            autogenerateselectbutton="True"

            >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="SiteId" HeaderText="SiteId">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="False">
                    </HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="EntityId" HeaderText="EntityId">
                  <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="False">
                    </HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="PX Environment" HeaderText="PX Environment">
                  <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="False">
                    </HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Dest Server Type" HeaderText="Dest Server Type">
                  <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="False">
                    </HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Import Status" HeaderText="Import Status">
                  <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="False">
                    </HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Import Date" HeaderText="Import Date">
                  <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="False">
                    </HeaderStyle>
                </asp:BoundField>
               
               
             
                <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "SiteUpdate.aspx?id="+Eval("ImportId") %>' Text="Update it!"  ></asp:HyperLink> 
                </ItemTemplate>
                </asp:TemplateField>
               
               
             
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
  

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <br />
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <br />
    <asp:Button ID="btnInsert"  runat="server" Text ="Insert SiteID" onclick="btnInsert_Click" />&nbsp;
    <asp:Button ID="btnUpdateEntity"  runat="server" Text ="Update Selected Entity" onclick="btnUpdateEntity_Click" Visible = "false" />&nbsp;
    <asp:Button ID="btnNewEntity"  runat="server" Text ="Create New Entity" onclick="btnNewEntity_Click" Visible = "false" />&nbsp;
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Home.aspx">Main Menu</asp:HyperLink>
 
 
 
</body>
</asp:Content>