using System.Data;
using System.Data.SqlClient;

namespace ECOMAPP.CommonRepository
{
    public class DBAccess:IDisposable
    {
        #region Public Properties
        public string DBProcedureName { get; set; }
        public List<DBParameters> DBParameterList { get; set; }
        #endregion

        #region Private Properties

        private SqlConnection _ConnectionString = null;
        private SqlCommand _SqlCommand = null;

        static IConfiguration conf = (new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build());
        private string DBConnectionString = conf["ConnectionStrings:DBCON"].ToString();

        #endregion

        #region Methods

        public void AddParameters(string _ParameterName, object _ParameterValue)
        {
            if (_ParameterName != "")
            {
                if (DBParameterList == null) DBParameterList = new List<DBParameters>();

                DBParameterList.Add(new DBParameters
                {
                    ParameterName = _ParameterName,
                    ParameterValue = _ParameterValue ?? DBNull.Value,
                });
            }
        }
        public DataSet DBExecute()
        {
            DataSet _DataSet = new DataSet();

            using (_ConnectionString = new SqlConnection(DBConnectionString))
            {
                _SqlCommand = new SqlCommand(DBProcedureName, _ConnectionString);
                _SqlCommand.CommandType = CommandType.StoredProcedure;

                if (DBParameterList != null)
                {
                    foreach (DBParameters param in DBParameterList)
                    {
                        _SqlCommand.Parameters.AddWithValue(param.ParameterName, param.ParameterValue);
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(_SqlCommand);
                sda.Fill(_DataSet);
            }
            return _DataSet;
        }
        public void Dispose()
        {
            DBConnectionString = null;
            DBProcedureName = null;
            DBParameterList = null;

            _ConnectionString = null;
            _SqlCommand = null;
        }

        #endregion
    }
}
