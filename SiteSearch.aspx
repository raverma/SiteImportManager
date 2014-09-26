<%@ Page Title="Site Search" Language="C#" MasterPageFile="~/Site.master" 
AutoEventWireup="true" CodeFile="~/SiteSearch.aspx.cs" 
Inherits="SiteSearch" %>


<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="theme/style.css" rel="stylesheet" type="text/css" media="all" />
        <script type="text/javascript" src="js/jquery-1.3.1.min.js"></script>
        <script type="text/javascript" src="js/jquery.betterTooltip.js"></script>
         <link type="text/css" rel="stylesheet" href="Styles/style.css" />
         <script src="Scripts/DatePicker.js" type="text/javascript"></script>
         <link href="Styles/datepicker.css" rel="stylesheet" type="text/css" />
 
        <script type="text/javascript">

            function OpenPopup() {
                window.open("SiteHelp.aspx?pagename=SiteSearch.aspx", "List", "scrollbars=yes,resizable=yes,width=400,height=400");
                return false;
            }

            function popWin(url) {
                myWindow = window.open(url, "myWindow", "location=0,status=1,width=500,height=500");
            }

            $(document).ready(function () {
                $('.tTip').betterTooltip({ speed: 150, delay: 300 });
            });
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server"  ContentPlaceHolderID="MainContent">
       
<div class="form">
    <div >Search by one or more of the following options</div>
    <div class="form">
    
        <table >
            <tr>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr >
                <td >
                    <asp:Label ID="lblSiteId"  runat="server" Text="Site Id" />
                </td>
                <td >
                    <asp:TextBox ID="txtSiteId" CssClass="tb2"  runat="server"></asp:TextBox>
                   <%-- <asp:HyperLink ID="HyperLink1" runat="server"  BackColor="#99CCFF" 
                        NavigateUrl="~/SiteHelp.aspx?pagename=SiteSearch.aspx" >Help</asp:HyperLink>--%>
                   <asp:LinkButton ID="lnkhelp" runat="server"  Text="Help" ></asp:LinkButton>   
                        
                </td>
                <td colspan="2" align="right">
                    Site Name</td>
                <td >
                    <asp:TextBox ID="txtSiteName" CssClass="tb2" runat="server" Width="466px"></asp:TextBox>
                    </td>
            </tr>
            <tr >
                <td >
                    <asp:Label ID="lblDate" runat="server" Text="Date from" />
                </td>
                <td >
                <input class="tb2" name="StartDate"/> 
                <input type="button" value="select" onclick="displayDatePicker('StartDate');"/>
                </td>
                <td colspan="2" align="right">
                 To&nbsp;
                </td>
                <td>
                    <input class="tb2" name="EndDate"/><input type="button" value="select" onclick="displayDatePicker('EndDate');"/></td>
            </tr>
            <tr  >
                <td >
                    <asp:Label ID="lblStatus" runat="server" Text="Status" />
                </td>
                <td >
                    <asp:DropDownList ID="ddlStatus" CssClass="tb2"  runat="server" Height="26px" Width="170px">
                    </asp:DropDownList>
                </td>
                <td colspan="2">
                    &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr  >
                <td valign="top" >
                    PX Environment</td>
                <td >
                    <asp:CheckBoxList ID="chkLstPxEnv" CssClass="tb2" runat="server" Width="163px">
                    </asp:CheckBoxList>
                </td>
                <td colspan="2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr >
                <td >
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Home.aspx">Main Menu</asp:HyperLink>
                </td>
                <td >
                    <asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" 
                        Text="Search" Width="151px" />
                </td>
                <td colspan="2">
                    &nbsp;</td>
                <td>
                    <asp:Label ID="ErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="form">
        <asp:GridView ID="grdSearchResult" runat="server" 
            CellPadding="3" AutoGenerateColumns="False" 
            OnRowCommand = "grdSearchResult_RowCommand"   CssClass="mGrid" 
            onrowdatabound="grdSearchResult_RowDataBound">
         
            <Columns>
                <asp:BoundField DataField="Dest Server Type_ID" HeaderText="a">
                  <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="False">
                    </HeaderStyle>
                </asp:BoundField>

                <asp:BoundField DataField="PX Environment_ID" HeaderText="b">
                  <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="False">
                    </HeaderStyle>
                </asp:BoundField>

                <asp:BoundField DataField="Import Status_ID" HeaderText="c">
                  <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="False">
                    </HeaderStyle>
                </asp:BoundField>
                
                <asp:ButtonField ButtonType="button" CommandName="ProcessAgain" 
                     HeaderText="Process Again" Text="Process Again" />
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
                
                 <asp:BoundField DataField="Date Created" HeaderText="Date Created">
                  <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="False">
                    </HeaderStyle>
                </asp:BoundField>
              
        <asp:TemplateField  ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>
            <div style="text-align:left">
              <asp:Image class="tTip" id="cloud1" Width="20" Height="20" Visible='<%# CheckIfErrorExist(Eval("Error")) %>' runat="server" ToolTip='<%# Bind("Error") %>' ImageUrl='<%# CheckIfTitleExists(Eval("Error"), "/images/buttonhover.gif") %>' />
            </div>
               <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/images/buttonhover.gif" />
               <asp:PopupControlExtender ID="PopupControlExtender1" runat="server"
               PopupControlID="Panel1"
               TargetControlID="Image1"
               DynamicContextKey='<%# Eval("SiteId") %>'
               DynamicControlID="Panel1"
               DynamicServiceMethod="GetDynamicContent" Position="Bottom">
               --%>
             </ItemTemplate>

        <ItemStyle HorizontalAlign="Left" ></ItemStyle>
        </asp:TemplateField> 
               
        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>
            <div style="text-align:left">
              <asp:Image class="tTip" id="cloud1" Width="20" Height="20" Visible='<%# CheckIfErrorExist(Eval("Comments")) %>' runat="server" ToolTip='<%# Bind("Comments") %>' ImageUrl='<%# CheckIfTitleExists(Eval("Error"), "/images/buttonhover.gif") %>' />
            
          
            
            </div>
               <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/images/buttonhover.gif" />
               <asp:PopupControlExtender ID="PopupControlExtender1" runat="server"
               PopupControlID="Panel1"
               TargetControlID="Image1"
               DynamicContextKey='<%# Eval("SiteId") %>'
               DynamicControlID="Panel1"
               DynamicServiceMethod="GetDynamicContent" Position="Bottom">
               --%>
             </ItemTemplate>
             
        <ItemStyle HorizontalAlign="Left" ></ItemStyle>
             
        </asp:TemplateField> 
                <asp:TemplateField>
                    <ItemTemplate>
                <asp:Hyperlink ID="Hyperlink3" Runat="server"  NavigateUrl='<%# Eval("ImportID", "SiteUpdate.aspx?ID={0}") %>'
                    Visible='<%# CommentExist(Eval("[Import Status_ID]")) %>' 
                    onclick="window.showModalDialog (this.href, 'popupwindow', 'dialogwidth=450px,dialogheight=250px'); return false;">
                    Update it!
                </asp:Hyperlink>
                       
                    </ItemTemplate>
                </asp:TemplateField>
               
               
            </Columns>
          
        </asp:GridView>
    </div>
    </div>

</asp:Content>