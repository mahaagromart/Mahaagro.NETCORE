using System.Data;
using ECOMAPP.CommonRepository;
using ECOMAPP.ModelLayer;
using static ECOMAPP.ModelLayer.MLProductattribute;
using static ECOMAPP.ModelLayer.MLVarients;

namespace ECOMAPP.DataLayer
{
    public class DLVarients
    {
        public List<MLVarients.Varients> GetAllVarients()
        {
            MLVarients _MLVarients = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {

                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "";
                    _DBAccess.AddParameters("@Action", "");
                    _DataSet = _DBAccess.DBExecute();


                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = new();
                    string Retval = _DataSet.Tables[1].Rows[0]["RETVAL"]?.ToString() ?? "";

                    foreach (DataRow Row in _DataTable.Rows)
                    {
                        if (Retval == "SUCCESS")
                        {
                            MLVarients.Varients Varients = new MLVarients.Varients()
                            {



                                Id = Convert.IsDBNull(Row["Id"]) ? 0 : Convert.ToInt32(Row["Id"]),
                                Product_Id = Row["Product_Id"]?.ToString() ?? string.Empty,
                                Quantity = Convert.IsDBNull(Row["Quantity"]) ? 0 : Convert.ToInt32(Row["Quantity"]),
                                ProductName = Row["ProductName"]?.ToString() ?? string.Empty,
                                Images = Row["Images"]?.ToString() ?? string.Empty,
                                Description = Row["Description"]?.ToString() ?? string.Empty,
                                Ratings = Row["Ratings"]?.ToString() ?? string.Empty,
                                Category_id = Row["Category_id"]?.ToString() ?? string.Empty,
                                CreationDate = Row["CreationDate"]?.ToString() ?? string.Empty
                            };
                            _MLVarients.VarientsList.Add(Varients);
                        }

                        else
                        {
                            _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                            _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;

                        }

                    }
                }

            }
            catch(Exception ex)
            {
                _DALBASE.ErrorLog("GetAllVarients", "DLVarients", ex.ToString());
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;

            }

            return _MLVarients.VarientsList;
        }


        public MLVarients InsertVarients()
        {
            MLVarients _MLVarients = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "";
                    _DBAccess.AddParameters("@Action", "");
                    _DataSet = _DBAccess.DBExecute();

                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = _DataSet.Tables[0];
                    foreach(DataRow _DataRow in _DataTable.Rows)
                    {
                        if (_DataRow["RETVAL"]?.ToString() == "SUCCESS")
                        {
                            _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                            _DBReturnData.Code = DBEnums.Codes.SUCCESS;

                        }
                        else
                        {
                            _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                            _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                        }
                    }


                }
                else
                {
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                }
            }catch(Exception ex)
            {
                _DALBASE.ErrorLog("InsertVarients", "DLVarients", ex.ToString());
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
            }


            return  _MLVarients;
        }


        public MLVarients UpdateVarients()
        {
            MLVarients _MLVarients = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "";
                    _DBAccess.AddParameters("@Action", "");
                    _DataSet = _DBAccess.DBExecute();

                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = _DataSet.Tables[0];
                    foreach (DataRow _DataRow in _DataTable.Rows)
                    {
                        if (_DataRow["RETVAL"]?.ToString() == "SUCCESS")
                        {
                            _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                            _DBReturnData.Code = DBEnums.Codes.SUCCESS;

                        }
                        else
                        {
                            _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                            _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                        }
                    }


                }
                else
                {
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                }
            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("UpdateVarients", "DLVarients", ex.Message.ToString());
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
            }


            return _MLVarients;
        }

        public MLVarients DeleteVarients( )
        {
            MLVarients _MLVarients = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "";
                    _DBAccess.AddParameters("@Action", "");
                    _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();

                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = _DataSet.Tables[0];
                    foreach (DataRow _DataRow in _DataTable.Rows)
                    {
                        if (_DataRow["RETVAL"]?.ToString() == "SUCCESS")
                        {
                            _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                            _DBReturnData.Code = DBEnums.Codes.SUCCESS;

                        }
                        else
                        {
                            _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                            _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                        }
                    }


                }
                else
                {
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                }
            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("DeleteVarients", "DLVarients", ex.ToString());
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
            }


            return _MLVarients;
        }
        

    }
}
