using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteUpdate : System.Web.UI.Page
{
    #region Declaring Global Variable
    private int _intId = 0;
    public string _strSite = string.Empty;
    public string _strStatus = string.Empty;
    public string _strDestType = string.Empty;
    public string _strComment = string.Empty;
    public string _strEnv = string.Empty;
    public string _strDate = string.Empty;
    protected string _strError = string.Empty;
    private const string _spGetSiteID= "AGILIXIMPORT_GETSITEID";
    private const string _spGetImportStatus = "agilixImport_Status";
    private const string _spGetImportServerType = "agilixImport_ServerType";
    private const string _spGetImportEnv = "agilixImport_Environment";
    System.Data.DataSet _dsAgRecords = new System.Data.DataSet();
    DatabaseObjects _clsData;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        _intId  = Convert.ToInt32(Request.QueryString["id"]);
        _strSite = string.Empty;
        try
        {
            //_intId = 146; //here is the hard code value and it will be replace by querystring
            if (!Page.IsPostBack)
            {
                //It calls the Single Ton Connection 
                _clsData = new DatabaseObjects();
                //fills the data in the dropdown
                FillData(_clsData, drpStatus, _spGetImportStatus);
                FillData(_clsData, drpServerType, _spGetImportServerType);
                FillData(_clsData, drpEnv, _spGetImportEnv);
                //fill the existing data in the page
                _dsAgRecords = ExistingRecords(_dsAgRecords);
                txtComment.Text = _strComment;
                calander.Value = _strDate;
            }
        }
        catch (Exception exp)
        {
            _strError = "Error : " + exp.Message;
        }
        //Response.End();
    }

    private System.Data.DataSet ExistingRecords(System.Data.DataSet _dsAgRecords)
    {
        _dsAgRecords = _clsData.GetAgilixImportData(_spGetSiteID, _intId);
        _strSite = Convert.ToString(_dsAgRecords.Tables[0].Rows[0]["SITEID"]);
        _strStatus = Convert.ToString(_dsAgRecords.Tables[0].Rows[0]["STATUSSHORTDESC"]);
        _strEnv = Convert.ToString(_dsAgRecords.Tables[0].Rows[0]["ENVSHORTDESC"]);
        _strComment = Convert.ToString(_dsAgRecords.Tables[0].Rows[0]["Comments"]);
        _strDestType = Convert.ToString(_dsAgRecords.Tables[0].Rows[0]["DestServerTypeDesc"]);
        drpEnv.SelectedIndex = drpEnv.Items.IndexOf(drpEnv.Items.FindByValue(Convert.ToString(_dsAgRecords.Tables[0].Rows[0]["EnvID"])));
        drpServerType.SelectedIndex = drpServerType.Items.IndexOf(drpServerType.Items.FindByText(_strDestType));
        drpStatus.SelectedIndex = drpStatus.Items.IndexOf(drpStatus.Items.FindByText(_strStatus));
        _strDate = Convert.ToString(_dsAgRecords.Tables[0].Rows[0]["ScheduleImportOn"]);
        return _dsAgRecords;
    }

    private void FillData(DatabaseObjects _clsData, DropDownList drp, string _spName)
    {
        drp.DataSource = _clsData.GetAgilixImportData(_spName);
        drp.DataBind();

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        _clsData = new DatabaseObjects();

        string[] _strData = new string[4] { drpStatus.SelectedItem.Value , 
                                            drpServerType.SelectedItem.Value,  
                                            txtComment.Text.Trim(), calander.Value };
        //updating the data
        try
        {
            UpdatingData(_strData);
            _strError = "Record Updated Successfully";
            _dsAgRecords = ExistingRecords(_dsAgRecords);
        }
        catch (Exception exp)
        {
            _strError = "Error : " + exp.Message;
        }
        GC.Collect();
    }

    private void UpdatingData(string[] _strData)
    {
        _clsData.PutAgilixImportData(_intId, _strData);
    }
}