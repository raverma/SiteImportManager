using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public sealed class DatabaseObjects
{
    //One way to implement singleton
    private static readonly SqlConnection _conn;
    private static SqlDataAdapter _da;

    static DatabaseObjects()
    {
        string _connStr = ConfigurationManager.ConnectionStrings["PxMigration"].ToString();
        _conn = new SqlConnection(_connStr);
        _conn.Open();
    }

    public static SqlConnection _Conn
    {
        get
        {
            return _conn;
        }
    }

    public DataSet GetAgilixImportData(string _spname)
    {
        DataSet _ds = new DataSet();
        _da = new SqlDataAdapter(_spname, _Conn);
        _da.Fill(_ds);
        return _ds;
    }

    public DataSet GetAgilixImportData(string _spname, int _iParamtId)
    {
        SqlCommand _cmd;
        SqlDataAdapter _da;
        DataSet _ds = new DataSet();
        _cmd = new SqlCommand(_spname, _Conn);
        _cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter _param1 = new SqlParameter("@ImportId", _iParamtId);
        _cmd.Parameters.Add(_param1);
        _da = new SqlDataAdapter(_cmd);
        _da.Fill(_ds);
        return _ds;
    }

    public void PutAgilixImportData(int _id, string[] _strdata)
    {

            SqlCommand _cmd;
            DataTable _dt;
            SqlDataAdapter _da;

            _cmd = new SqlCommand("aglixImport_UpdateData", _Conn);
            _cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter _param1 = new SqlParameter("@ImportId", _id);
            _cmd.Parameters.Add(_param1);
            SqlParameter _param2 = new SqlParameter("@StatusId", Convert.ToInt32(_strdata[0]));
            _cmd.Parameters.Add(_param2);
            SqlParameter _param3 = new SqlParameter("@DestServerTypeID", Convert.ToInt32(_strdata[1]));
            _cmd.Parameters.Add(_param3);
            SqlParameter _param4 = new SqlParameter("@Comments", _strdata[2]);
            _cmd.Parameters.Add(_param4);

            _dt = new DataTable();
            _da = new SqlDataAdapter(_cmd);
            _da.Fill(_dt);
            _Conn.Close();
    }
}
