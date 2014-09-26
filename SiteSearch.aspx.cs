using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class SiteSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            populatePxEnvironment();
            populateStatusDropdown();
            //Start:-- code changes by sumit as on 28th April for pop up window
            this.lnkhelp.Attributes.Add("onclick", "javascript:return OpenPopup()");
            //End:-- code changes by sumit as on 28th April for pop up window
        }
        if (this.PreviousPage != null)
        {
            
            Page prevPage = this.PreviousPage;
            HiddenField SiteID = (HiddenField)prevPage.FindControl("hiddenText");
            HiddenField SiteName = (HiddenField)prevPage.FindControl("hiddenSiteName");
            if (SiteID != null)
            {
                txtSiteId.Text = SiteID.Value;
                txtSiteName.Text = SiteName.Value;
                //chkSiteId.Checked = true;
                
            }

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (IsValidCriteria())
            SearchOnCriteria();
       
    }

    private bool IsValidCriteria()
    {
        if (txtSiteId.Text != null)
        {
            bool containsLetter = false;
            string SiteID = txtSiteId.Text.Trim();
            for (int i = 0; i < SiteID.Length; i++)
            {
                if (!char.IsNumber(SiteID[i]))
                {
                    containsLetter = true;
                }
            }

            if (containsLetter)
            {
                ErrorMsg.Text = "SiteID must be Integer";
                return false;
            }
        }

        if (Request.Form["StartDate"].ToString() != string.Empty && Request.Form["EndDate"].ToString() != string.Empty && Convert.ToDateTime(Request.Form["StartDate"].ToString()) > Convert.ToDateTime(Request.Form["EndDate"].ToString()))
        {
            ErrorMsg.Text = "From Date cannot be greater than To Date";
            return false;
        }
        else
        {
            ErrorMsg.Text = string.Empty;
            return true;
        }
    }

    private void SearchOnCriteria()
    {
        try
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["PxMigration"].ConnectionString);
            SqlCommand cmd = new SqlCommand("agilixImport_SearchItem", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter parSiteId = new SqlParameter("@siteId", SqlDbType.Int);
            SqlParameter parStartDate = new SqlParameter("@startDate", SqlDbType.VarChar, 10);
            SqlParameter parEndDate = new SqlParameter("@endDate", SqlDbType.VarChar, 10);
            SqlParameter parStatusId = new SqlParameter("@statusID", SqlDbType.Int);
            SqlParameter parPXEnv = new SqlParameter("@pxEnv", SqlDbType.VarChar, 50);
            SqlParameter parErrorLogId = new SqlParameter("@ErrorLogId", SqlDbType.Int);

            parSiteId.SqlValue = DBNull.Value;
            parStartDate.SqlValue = DBNull.Value;
            parEndDate.SqlValue = DBNull.Value;
            parPXEnv.SqlValue = DBNull.Value;
            parStatusId.SqlValue = DBNull.Value;
            parErrorLogId.SqlValue = DBNull.Value;

            if (txtSiteId.Text != string.Empty)
                parSiteId.Value = txtSiteId.Text;
            //if (chkStatus.Checked)
            parStatusId.Value = Convert.ToInt32(ddlStatus.SelectedValue);
            if (Request.Form["StartDate"].ToString() != string.Empty && Request.Form["EndDate"].ToString() != string.Empty)
            {
                parStartDate.Value = String.Format("{0}", Request.Form["StartDate"]);
                parEndDate.Value = String.Format("{0}", Request.Form["EndDate"]);
            }
            List<string> sPxEnv = new List<string>();
            foreach (ListItem li in chkLstPxEnv.Items)
            {
                if (li.Selected)
                {
                    sPxEnv.Add(li.Value.ToString());
                }
            }
            if (sPxEnv.Count > 0)
            {
                parPXEnv.Value = string.Join(",", sPxEnv.ToArray());
            }
            cmd.Parameters.Add(parSiteId);
            cmd.Parameters.Add(parStartDate);
            cmd.Parameters.Add(parEndDate);
            cmd.Parameters.Add(parStatusId);
            cmd.Parameters.Add(parPXEnv);
            cmd.Parameters.Add(parErrorLogId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);


            grdSearchResult.DataSource = dt;
            grdSearchResult.Columns[0].Visible = true;
            grdSearchResult.Columns[1].Visible = true;
            grdSearchResult.Columns[2].Visible = true;
            grdSearchResult.DataBind();
            grdSearchResult.Columns[0].Visible = false;
            grdSearchResult.Columns[1].Visible = false;
            grdSearchResult.Columns[2].Visible = false;
            if (dt.Rows.Count == 0)
            {
                grdSearchResult.Caption = "No Result Found";
            }
            else
            {
                grdSearchResult.Caption = string.Empty;
            }
        }
        catch
        {


        }
    }
    private void populatePxEnvironment()
    {
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["PxMigration"].ConnectionString);
        SqlCommand cmd = new SqlCommand("agilixImport_GetPxEnvironments", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        chkLstPxEnv.DataSource = dt;
        chkLstPxEnv.DataTextField = "EnvShortDesc";
        chkLstPxEnv.DataValueField = "EnvId";
        chkLstPxEnv.DataBind();
        
    }
    private void populateStatusDropdown()
    {
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["PxMigration"].ConnectionString);
        SqlCommand cmd = new SqlCommand("agilixImport_GetStatuses", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlStatus.DataSource = dt;
        ddlStatus.DataTextField = "statusShortDesc";
        ddlStatus.DataValueField = "statusID";
        ddlStatus.DataBind();
        ListItem li = new ListItem("All", "99"); //Assigning 99 because it will allow us to add more status in near future
        ddlStatus.Items.Add(li);
        ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue("99")); // make default value : All
        
    }

  
    protected void grdSearchResult_RowCommand(object sender,System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {

        string currentCommand = e.CommandName;
        if (currentCommand == "ProcessAgain")
        {
            GridViewRow row = grdSearchResult.Rows[int.Parse(e.CommandArgument.ToString())];
            int siteId = Convert.ToInt32(row.Cells[4].Text);
            int pxEnv = Convert.ToInt32(row.Cells[1].Text);
            string pxEnvDesc = row.Cells[6].Text;
            DateTime importDate = DateTime.Now;
            int entityID = Convert.ToInt32(row.Cells[5].Text);
            int destServerTypeID = Convert.ToInt32(row.Cells[0].Text);
            int statusID = Convert.ToInt32(row.Cells[2].Text);

            if ((entityID != -1) && (statusID != 1))
            {
                processSite(siteId, pxEnv, importDate, entityID, destServerTypeID);
                ErrorMsg.Text = "Record Inserted Successfully.";
            }
            else
                ErrorMsg.Text = "A scheduled job is already in the queue for this Site: " + siteId + " and Environment: " + pxEnvDesc + ".";
        }
        
    }

    //Start:-- code changes by sumit as on 28th April for checking the value is exist or not
    public string CheckIfTitleExists(object strval, string value)
    {
        if (strval.ToString() == string.Empty)
            return string.Empty;
        else
            return value;
    }

    public bool CheckIfErrorExist(object strval)
    {
        if (strval.ToString() == string.Empty)
            return false;
        else
            return true;
    }

    public bool CommentExist(object Comments)
    {
        if( (Convert.ToString(Comments) =="1") || (Convert.ToString(Comments) =="5") ||(Convert.ToString(Comments) =="6") )
            return true;
        else
            return false;
    }

    public string CheckVisibility(object strval)
    {
        if (strval.ToString() == string.Empty)
            return "none";
        else
            return "block";
    }
    

    public void processSite(int siteID, int pxEnv, DateTime importDate, int entityID, int destServerTypeID)
    {
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["PxMigration"].ToString());
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter da;

        con.Open();
        string strError = string.Empty;
        cmd = new SqlCommand("[px_sp_InsertAgilixImport]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter param1 = new SqlParameter("@SiteId", siteID);
        cmd.Parameters.Add(param1);
        SqlParameter param2 = new SqlParameter("@EntityID", entityID);
        cmd.Parameters.Add(param2);
        SqlParameter param3 = new SqlParameter("@PXEnvId", pxEnv);
        cmd.Parameters.Add(param3);
        SqlParameter param4 = new SqlParameter("@ImportOn", importDate);
        cmd.Parameters.Add(param4);
        SqlParameter param5 = new SqlParameter("@DestServerTypeID", destServerTypeID);
        cmd.Parameters.Add(param5);
        //SqlParameter param6 = new SqlParameter("@Comments", txtComments);
        //cmd.Parameters.Add(param6);
        

        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        con.Close();
    }


    [System.Web.Services.WebMethodAttribute()]
    public static string GetDynamicContent(string contextKey)
    {
        
        StringBuilder b = new StringBuilder();

        b.Append("<table style='background-color:#f3f3f3; border: #336699 3px solid; ");
        b.Append("width:350px; font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>");

        b.Append("<tr><td colspan='3' style='background-color:#336699; color:white;'>");
        b.Append("<b>Product Details</b>"); b.Append("</td></tr>");
        b.Append("<tr><td style='width:80px;'><b>Unit Price</b></td>");
        b.Append("<td style='width:80px;'><b>Stock</b></td>");
        b.Append("<td><b>Description</b></td></tr>");

        b.Append("<tr>");
        b.Append("<td>$" + "saurabh" + "</td>");
        b.Append("</tr>");

        b.Append("</table>");

        return b.ToString();
    }
    protected void grdSearchResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      
    }
}