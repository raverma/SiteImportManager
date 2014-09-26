using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class SiteHelp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["PageName"] = Request.QueryString["pagename"].ToString();
        }
    }
    
    public void PopulateGrid()
    {
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["PxMigration"].ConnectionString);
        SqlCommand cmd = new SqlCommand("SearchOnSiteName",con);
        cmd.CommandTimeout = 720;
        cmd.CommandType=CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@SearchString",txtSiteName.Text));
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdSearchResult.DataSource=dt;
        grdSearchResult.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        PopulateGrid();
    }
    protected void grdSearchResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onclick",
            ClientScript.GetPostBackClientHyperlink(grdSearchResult, "Select$" +
            e.Row.RowIndex.ToString()));
            e.Row.Style.Add("cursor", "pointer");

            //Start:-- code changes by sumit as on 28th April for pop up window implentetation
            string bsiID = ((Label)e.Row.FindControl("Label1")).Text;
            bsiID = RemoveSpecialChar(bsiID);
            string sName = ((Label)e.Row.FindControl("Label2")).Text;
            sName = RemoveSpecialChar(sName);
            string id = Request.QueryString["pagename"].ToString();
            ((Label)e.Row.FindControl("Label1")).Attributes.Add("onclick", "javascript:GetRowValue('" + bsiID + "' ,'" + sName + "', '" + id + "' )");
            //End:-- code changes by sumit as on 28th April for pop up window implentetation
    
        }
    }

    //Start:-- code changes by sumit as on 28th April for special char implentetation
    private string RemoveSpecialChar(string myString)
    {
        string removeStr = myString;

        removeStr = removeStr.Replace("O&#39;", "'");
        removeStr = removeStr.Replace("\t", "");
        removeStr = removeStr.Replace("\n", "");
        removeStr = removeStr.Replace("&#146;", "''");
        removeStr = removeStr.Replace("%20", " ");
        removeStr = removeStr.Replace("%22", ('"').ToString());
        removeStr = removeStr.Replace("%27", "'");
        removeStr = removeStr.Replace("%2C", ",");
        removeStr = removeStr.Replace("%3F", "?");
        removeStr = removeStr.Replace("%25", "%");
        removeStr = removeStr.Replace("%5B", "[");
        removeStr = removeStr.Replace("%5D", "]");
        removeStr = removeStr.Replace("%7B", "{");
        removeStr = removeStr.Replace("%7D", "}");
        removeStr = removeStr.Replace("%40", "@");
        removeStr = removeStr.Replace("%23", "#");
        removeStr = removeStr.Replace("%24", "$");
        removeStr = removeStr.Replace("%26", "&");
        removeStr = removeStr.Replace("%2F", "/");
        removeStr = removeStr.Replace("%5C", "\\");
        removeStr = removeStr.Replace("%3C", "<");
        removeStr = removeStr.Replace("%3E", ">");
        removeStr = removeStr.Replace("%7C", "|");
        removeStr = removeStr.Replace("%3B", ";");
        removeStr = removeStr.Replace("%60", "`");
        removeStr = removeStr.Replace("%2B", "+");
        removeStr = removeStr.Replace("%3D", "=");
        removeStr = removeStr.Replace("%5E", "^");
        removeStr = removeStr.Replace("%09", "  ");
        removeStr = removeStr.Replace("%3A", ":");
        // etc
        removeStr = removeStr.TrimEnd();
        return removeStr;
    }
    //End:-- code changes by sumit as on 28th April for special char implentetation


    protected void grdSearchResult_SelectedIndexChanged(object sender, EventArgs e)
    {
        string gridviewvalue = grdSearchResult.SelectedDataKey.Value.ToString();
        //hiddenSiteName.Value = grdSearchResult.Selected);   //grdSearchResult[1,grdSearchResult.SelectedIndex].ToString();
        hiddenText.Value = gridviewvalue;

        GridViewRow[] rowArray = new GridViewRow[grdSearchResult.Rows.Count];
        grdSearchResult.Rows.CopyTo(rowArray, 0);

        // Iterate though the array and display the value in the first cell of the row.
        int j = -1;
        foreach (GridViewRow row in rowArray)
        {
            j++;
            if (j == grdSearchResult.SelectedIndex)
            {
                hiddenSiteName.Value = row.Cells[1].Text;
            }
        }


        if (ViewState["PageName"] != null)
        {
            Server.Transfer(Convert.ToString(ViewState["PageName"]), true);
        }
        
    }
    protected void grdSearchResult_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void grdSearchResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        PopulateGrid();

        grdSearchResult.PageIndex = e.NewPageIndex;
        grdSearchResult.DataBind();
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (ViewState["PageName"] != null)
        {
            Server.Transfer(Convert.ToString(ViewState["PageName"]), true);
        }
    }
}