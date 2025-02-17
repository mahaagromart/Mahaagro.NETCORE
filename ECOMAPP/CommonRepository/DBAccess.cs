using System.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.SqlClient;

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

    public class DALBASE
    {
        public void ErrorLog(string MethodName, string ClassName, string Message)
        {
            try
            {
                using (DBAccess Db = new DBAccess())
                {
                    Db.DBProcedureName = "SP_ErrorLog";
                    Db.DBParameterList = new List<DBParameters>{ 
                        new DBParameters{
                        ParameterName = "@MethodName",
                        ParameterValue = MethodName                        
                        },
                        new DBParameters
                        {
                            ParameterName = "@ClassName",
                            ParameterValue = ClassName
                        },
                        new DBParameters
                        {
                            ParameterName = "@ErrorMsg",
                            ParameterValue = Message
                        }
                    };
                    //DataSet ds = new DataSet();
                    Db.DBExecute();
                    Db.Dispose();
                    return;
                }
            }
            catch (Exception ex) {
                return;
            }

        }

        public void requestresponse(string MethodName, string Request, string Response, string Exception)
        {
            try
            {
                using(DBAccess Db = new DBAccess())
                {
                    Db.DBProcedureName = "SP_REQUESTRESPONSELOG";
                    Db.AddParameters("@Action", "INSERTINFODETAIL");
                    Db.AddParameters("@MethodName",MethodName);
                    Db.AddParameters("@Request",Request);
                    Db.AddParameters("@Response",Response );
                    Db.AddParameters("@Exception",Exception);
                    Db.DBExecute();
                    Db.Dispose();
                }

            }
            catch (Exception ex) 
            {
                return;    
            }
        }

        public static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }




    }

}
