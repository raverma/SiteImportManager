using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

/// <summary>
/// Summary description for DataLayer
/// </summary>
public class DataLayer
{
	
    SqlConnection _dbSqlConnection;
    private static SqlDataAdapter da;
    private static SqlCommand cmd;
    private string _dbConnectionString;
    private static DataLayer _singletonInstance;

    public string DBConnectionString
    {
        get { return _dbConnectionString; }
        set { _dbConnectionString = value; }
    }
    public static DataLayer GetInstance()
    {
        //For the first request, the _singletonInstance object is instansiated
        if (_singletonInstance == null)
        {
            _singletonInstance = new DataLayer();
            _singletonInstance.ConfigureApplication();
        }

        return _singletonInstance;
    }

    private void ConfigureApplication()
    {
        ConfigureConnectionString();
    }
    private void ConfigureConnectionString()
    {
        _dbConnectionString = WebConfigurationManager.ConnectionStrings["PxMigration"].ConnectionString;
        _dbSqlConnection = new SqlConnection(_dbConnectionString);
    }

    public DataSet GetAgilixImportStatus(string _spname)
    {
        
        DataSet ds = new DataSet();
        da = new SqlDataAdapter(_spname, _dbConnectionString);
        da.Fill(ds);
        return ds;
    }

}