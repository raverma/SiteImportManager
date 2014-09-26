using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            LoadEnvironment();
            populateDomain(Convert.ToInt32(rblPxEnvironment.SelectedValue));
            LoadDestServerType();
            btnCalendar.Enabled = false;
            //txtImportDt.Text = DateTime.Now.ToLongDateString();
            this.btnSearch.Attributes.Add("onclick", "javascript:return OpenPopup()");

            //this.HyperLink1.Attributes.Add("onclick", "javascript:return OpenPopup()");
        }

        if (this.PreviousPage != null)
        {
            Page prevPage = this.PreviousPage;
            HiddenField SiteID = (HiddenField)prevPage.FindControl("hiddenText");
            if (SiteID != null)
            {
                txtSiteID.Text = SiteID.Value;
            }


        }

    }

   

    public bool ValidateFields()
    {
        if (String.IsNullOrEmpty(txtSiteID.Text))
        {
            lblError.Text = "Please Enter SiteID";
            return false;
        }
        
        
        bool containsLetter = false;

        string SiteID = txtSiteID.Text.Trim();
        for (int i = 0; i < SiteID.Length; i++)
        {
            if (!char.IsNumber(SiteID[i]))
            {
                containsLetter = true;
            }
        }

        if (containsLetter)
        {
            lblError.Text = "SiteID must be Integer";
            return false;
        } 
        if (!existSiteID(Convert.ToInt32(txtSiteID.Text)))
        {
            lblError.Text = "SiteID does not exist";
            return false;
        }

        if (optScheduled.Checked && txtImportDt.Text == string.Empty)
        {
            lblError.Text = "Please select the Schedule Date";
            return false;
        }
        if (txtImportDt.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtImportDt.Text).Date < DateTime.Now.Date)
            {
                lblError.Text = "Schedule Date can't be less than Current Date";
                return false;
            }
        }
        return true;
    }

    public void processSite(int siteID, int pxEnv, DateTime importDate, int entityID, int destServerTypeID,int domainID, int statusId, string comments )
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PxMigration"].ToString());
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
        SqlParameter param4 = new SqlParameter("@ImportOn", importDate );
        cmd.Parameters.Add(param4);
        SqlParameter param5 = new SqlParameter("@DestServerTypeID", destServerTypeID);
        cmd.Parameters.Add(param5);
        SqlParameter param6 = new SqlParameter("@DomainID", domainID);
        cmd.Parameters.Add(param6);
        SqlParameter param7 = new SqlParameter("@StatusId", statusId);
        cmd.Parameters.Add(param7);
        SqlParameter param8 = new SqlParameter("@Comments", comments);
        cmd.Parameters.Add(param8);
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        con.Close();
    }
   

    protected void gvAgilixImport_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = gvAgilixImport.SelectedRow;
        hiddenText.Value = row.Cells[2].Text; ;
    }
    

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        gvAgilixImport.Visible = false;
        btnUpdateEntity.Visible = false;
        btnNewEntity.Visible = false;

        if (!ValidateFields())
            return;

        DataTable dtAgilixImport = VerifySiteInAgilixImport(Convert.ToInt32(txtSiteID.Text), Convert.ToInt32(rblPxEnvironment.SelectedItem.Value));
       
        if (dtAgilixImport.Rows.Count == 0)
        {
            processSite(Convert.ToInt32(txtSiteID.Text), Convert.ToInt32(rblPxEnvironment.SelectedItem.Value),(txtImportDt.Text==""? DateTime.MaxValue :Convert.ToDateTime(txtImportDt.Text)), -1, Convert.ToInt32(rblDestServerType.SelectedItem.Value), Convert.ToInt32(ddlDomain.SelectedValue), GetStatusId(), txtComments.Text);
            txtSiteID.Text = string.Empty;
            txtImportDt.Text = DateTime.Now.ToLongDateString();
            lblError.Text = "Record Inserted Successfully";
        }
        else 
        {
            gvAgilixImport.Visible = true;
            btnUpdateEntity.Visible = true;
            btnNewEntity.Visible = true;
            gvAgilixImport.DataSource = dtAgilixImport;
            gvAgilixImport.DataBind();
            gvAgilixImport.SelectedIndex = -1;
            lblError.Text = "This Site id already Imported, please select one of them or go for a new one";

        }
    }

    private bool existSiteID(int SiteId)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PxMigration"].ToString());
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter da;

        con.Open();
        cmd = new SqlCommand("[bsi_GetSite]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter param1 = new SqlParameter("@site_id", SiteId);
        cmd.Parameters.Add(param1);
        dt = new DataTable();
        da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        if (dt.Rows.Count > 0)
            return true;
        else
            return false;
    }

    protected void LoadEnvironment()
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["PxMigration"].ToString());
        SqlCommand objCmd = new SqlCommand("select EnvShortDesc, EnvID from info.tblPXEnvironment", objConn);
        objConn.Open();
        rblPxEnvironment.DataSource = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
        rblPxEnvironment.DataBind();
        if (rblPxEnvironment.Items.Count > 0)
            rblPxEnvironment.Items[0].Selected = true;
        objConn.Close();
    }

    protected void LoadDestServerType()
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["PxMigration"].ToString());
        SqlCommand objCmd = new SqlCommand("select DestServerTypeDesc, DestServerTypeID from info.tblDestServerType", objConn);
        objConn.Open();
        rblDestServerType.DataSource = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
        rblDestServerType.DataBind();
        if (rblDestServerType.Items.Count > 0)
            rblDestServerType.Items[0].Selected = true;
        objConn.Close();
    }

   

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        DateTime dt = Calendar1.SelectedDate;
        txtImportDt.Text = dt.ToLongDateString();
        Calendar1.Visible = false;

    }
    protected void btnCalendar_Click(object sender, EventArgs e)
    {
        Calendar1.Visible = true;
    }




    protected void btnUpdateEntity_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        if (gvAgilixImport.SelectedRow == null)
        {
            lblError.Text = "Please select one of the existing Entity Ids";
            return;
        }
        if (Convert.ToInt32(gvAgilixImport.SelectedRow.Cells[2].Text) == -1)
        {
            lblError.Text = "A scheduled job is already in the queue for this Site: " + gvAgilixImport.SelectedRow.Cells[1].Text + " and Environment: " + gvAgilixImport.SelectedRow.Cells[3].Text + ". Please select some other record.";
            return;
        }


        processSite(Convert.ToInt32(txtSiteID.Text), Convert.ToInt32(rblPxEnvironment.SelectedItem.Value), (txtImportDt.Text == "" ? DateTime.MaxValue : Convert.ToDateTime(txtImportDt.Text)), Convert.ToInt32(gvAgilixImport.SelectedRow.Cells[2].Text), Convert.ToInt32(rblDestServerType.SelectedItem.Value), Convert.ToInt32(ddlDomain.SelectedValue), GetStatusId(), txtComments.Text);
        txtSiteID.Text = string.Empty;
        txtImportDt.Text = DateTime.Now.ToLongDateString();
        gvAgilixImport.Visible = false;
        lblError.Text = "Record Inserted Successfully";
    }
    protected void btnNewEntity_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        processSite(Convert.ToInt32(txtSiteID.Text), Convert.ToInt32(rblPxEnvironment.SelectedItem.Value), (txtImportDt.Text == "" ? DateTime.MaxValue : Convert.ToDateTime(txtImportDt.Text)), -1, Convert.ToInt32(rblDestServerType.SelectedItem.Value), Convert.ToInt32(ddlDomain.SelectedValue), GetStatusId(), txtComments.Text);
        txtSiteID.Text = string.Empty;
        txtImportDt.Text = DateTime.Now.ToLongDateString();
        gvAgilixImport.Visible = false;
        lblError.Text = "Record Inserted Successfully";
    }

    //Start:-- code changes by sumit as on 28th April Page Redirect
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/SiteHelp.aspx?pagename=Initiate.aspx");
    }
    //End:-- code changes by sumit as on 28th April Page Redirect

    protected void rblPxEnvironment_SelectedIndexChanged(object sender, EventArgs e)
    {
        populateDomain(Convert.ToInt32(rblPxEnvironment.SelectedValue));
    }
    protected void populateDomain(int envID)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["PxMigration"].ToString());
        SqlCommand objCmd = new SqlCommand("SELECT [DomainID] , [Name]  FROM [Import].[AgilixDomains] Where [EnvID] = " + envID, objConn);
        objConn.Open();
        ddlDomain.DataSource = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
        ddlDomain.DataBind();
        objConn.Close();
    }
    private int GetStatusId()
    {
        if (optReady.Checked)
            return 1;
        else if (optNotReady.Checked)
            return 5;
        else if (optScheduled.Checked)
            return 6;
        else
            return 0;  //this wont happen
    }
   
    private DataTable VerifySiteInAgilixImport(int siteId, int pxEnvID)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PxMigration"].ToString());
        con.Open();
        SqlCommand cmd = new SqlCommand("agilixImport_SearchItem", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter parSiteId = new SqlParameter("@siteId", SqlDbType.Int);
        SqlParameter parStartDate = new SqlParameter("@startDate", SqlDbType.VarChar, 10);
        SqlParameter parEndDate = new SqlParameter("@endDate", SqlDbType.VarChar, 10);
        SqlParameter parStatusId = new SqlParameter("@statusID", SqlDbType.Int);
        SqlParameter parPXEnv = new SqlParameter("@pxEnv", SqlDbType.VarChar, 50);
        SqlParameter parErrorLogId = new SqlParameter("@ErrorLogId", SqlDbType.Int);

        parSiteId.SqlValue = siteId;
        parStartDate.SqlValue = DBNull.Value;
        parEndDate.SqlValue = DBNull.Value;
        parPXEnv.SqlValue = pxEnvID;
        parStatusId.SqlValue = DBNull.Value;
        parErrorLogId.SqlValue = DBNull.Value;

        cmd.Parameters.Add(parSiteId);
        cmd.Parameters.Add(parStartDate);
        cmd.Parameters.Add(parEndDate);
        cmd.Parameters.Add(parStatusId);
        cmd.Parameters.Add(parPXEnv);
        cmd.Parameters.Add(parErrorLogId);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        return dt;
    }


    protected void optScheduled_CheckedChanged1(object sender, EventArgs e)
    {
        if (optScheduled.Checked)
            btnCalendar.Enabled = true;
        else
            btnCalendar.Enabled = false;
    }
}











