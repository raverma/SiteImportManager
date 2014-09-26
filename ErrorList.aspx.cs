using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

public partial class ErrorList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SearchOnCriteria();
   
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
            parErrorLogId.SqlValue =Convert.ToInt32(Request.QueryString["errorId"]);

         
            cmd.Parameters.Add(parSiteId);
            cmd.Parameters.Add(parStartDate);
            cmd.Parameters.Add(parEndDate);
            cmd.Parameters.Add(parStatusId);
            cmd.Parameters.Add(parPXEnv);
            cmd.Parameters.Add(parErrorLogId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);


            dvErrorList.DataSource = dt;
            dvErrorList.DataBind();
            if (dt.Rows.Count == 0)
            {
                dvErrorList.Caption = "No Result Found";

            }
            else
                dvErrorList.Caption = string.Empty;
        }
        catch(Exception exp)
        {


        }
    }
}