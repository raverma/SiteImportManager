using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class Default2 : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["registration"].ToString());
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter("aglixImport_GetItem", con);
            adp.Fill(ds);
            MyGridView.DataSource = ds;
            MyGridView.DataBind();
            //MyGridView.DataKeys
        
        }
    }

    protected void MyGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onclick",
            ClientScript.GetPostBackClientHyperlink(MyGridView, "Select$" +
            e.Row.RowIndex.ToString()));
            e.Row.Style.Add("cursor", "pointer");
        }
    }

    protected void MyGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox1.Text = MyGridView.SelectedValue.ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["registration"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("aglixImport_GetItem", con);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        MyGridView.DataSource = ds;
        MyGridView.DataBind();
    }

    
}
