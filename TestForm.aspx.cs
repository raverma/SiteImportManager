using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        
        int iSiteId;
        string retMsg;
        try
        {
            SiteExportService_Temp.Export.Export exportws = new SiteExportService_Temp.Export.Export();
            exportws.Timeout = 1200000;
            string iEntityId = string.Empty;
            if (txtEntityId.Text == "-1")
            {
                iEntityId = string.Empty;
            }
            if (int.TryParse(txtSiteId.Text, out iSiteId))
            {
                retMsg = exportws.ExportSite(iSiteId, iEntityId, txtDomain.Text, txtFolderPath.Text);
                Response.Write(retMsg);
            }
            else
            {
                Response.Write("Invalid Site Id");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message );
        }
    }
}