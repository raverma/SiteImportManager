<%@ Page Title="Error Log" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="~/ErrorList.aspx.cs" 
Inherits="ErrorList" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <body>
   
    <div>
    </div>
        <table class="style1">
            <tr>
                <td><strong>Error Log Detail</strong></td>
            </tr>
            <tr>
                <td class="style2">
    
     <asp:DetailsView ID="dvErrorList" runat="server" AutoGenerateRows="False" 
             Height="50px" Width="125px" AllowPaging="True" HeaderText="Error Log Detail">
            
            <Fields>
                <asp:BoundField DataField="SiteId" HeaderText="SiteId">
                </asp:BoundField>
                <asp:BoundField DataField="EntityId" HeaderText="EntityId">
                </asp:BoundField>
                <asp:BoundField DataField="Error" HeaderText="Error">
                </asp:BoundField>
            </Fields>
        </asp:DetailsView>
    
                </td>
            </tr>
        </table>
  
   
</body>
</asp:Content>




