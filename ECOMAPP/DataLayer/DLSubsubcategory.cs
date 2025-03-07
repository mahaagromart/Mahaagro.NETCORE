using System.Data;
using ECOMAPP.CommonRepository;
using ECOMAPP.ModelLayer;
using static ECOMAPP.ModelLayer.MLSubsubcategory;

namespace ECOMAPP.DataLayer
{
    public class DLSubsubcategory
    {
        public List<MLSubsubcategory.SubsubCategory> GetAllSubsubCategory()
        {
            MLSubsubcategory _MLSubsubcategory = new();
            DBReturnData _DBReturnData = new();
            DALBASE _DALBASE = new();

            DataSet _DataSet = new DataSet();
            try
            {
                _MLSubsubcategory.SubsubCategoryList = new List<MLSubsubcategory.SubsubCategory>();
                using(DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_SUBSUBCATEGORY";
                    _DBAccess.AddParameters("@Action", "GETALLSUBSUBCATEGORY");
                    _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();
                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = _DataSet.Tables[0];
                    string Retval = _DataSet.Tables[1].Rows[0]["RETVAL"]?.ToString() ?? "";
                    if (Retval == "SUCCESS")
                    {
                        foreach (DataRow Row in _DataTable.Rows)
                        {
                            var SubsubCategory = new MLSubsubcategory.SubsubCategory
                            {
                                Id = Row["id"] == DBNull.Value ? 0 : Convert.ToInt32(Row["Id"]),
                                SUBSUBCATEGORY_NAME = Row["SUBSUBCATEGORY_NAME"]?.ToString() ?? string.Empty,
                                SUBCATEGORY_NAME = Row["SUBCATEGORY_NAME"]?.ToString() ?? string.Empty,
                                CATEGORY_NAME = Row["CATEGORY_NAME"]?.ToString() ?? string.Empty,
                                CreationDate = Row["CreationDate"]?.ToString() ?? string.Empty,
                                PRIORITY = Row["PRIORITY"] == DBNull.Value ? 0 : Convert.ToInt32(Row["PRIORITY"])
                            };
                            _MLSubsubcategory.SubsubCategoryList.Add(SubsubCategory);

                        }
                    }
                    else
                    {
                        _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                        _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();

                    }
                }
                else
                {
           
                    _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                }
            }
            catch(Exception ex)
            {
                _DALBASE.ErrorLog("GetAllSubsubCategory", "DLSubsubcategory", ex.Message);
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();

            }
            return _MLSubsubcategory.SubsubCategoryList;
        }

        public DBReturnData InsertSubsubCategory(MLInsertsubsubcategory Data)
        {
            MLSubsubcategory _MLSubsubcategory = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();


            try
            {
                DataSet _Dataset = new();
                using(DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_SUBSUBCATEGORY";
                    _DBAccess.AddParameters("@Action", "INSERTSUBSUBCATEGORY");
                    _DBAccess.AddParameters("@SUBSUBCATEGORY_NAME", Data.SUBSUBCATEGORY_NAME);
                    _DBAccess.AddParameters("@CATEGORY_NAME", Data.CATEGORY_NAME);
                    _DBAccess.AddParameters("@SUBCATEGORY_NAME", Data.SUBCATEGORY_NAME);
                    _DBAccess.AddParameters("@PRIORITY", Data.PRIORITY);
                    _Dataset = _DBAccess.DBExecute();
                    _DBAccess.Dispose();

                }
                if(_Dataset!=null && _Dataset.Tables.Count > 0)
                {
                    DataTable _DataTable = _Dataset.Tables[0];
                    foreach (DataRow Row in _DataTable.Rows)
                    {
                        if (Row["RETVAL"]?.ToString() == "SUCCESS")
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

            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("InsertSubsubCategory", "DLSubsubcategory", ex.Message);
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                _DBReturnData.Message =DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();

            }
            return _DBReturnData;

        }


        public DBReturnData UpdateSubsubCategory(MLUpdateSubsubcategory Data)
        {
            MLSubsubcategory _MLSubsubcategory = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();


            try
            {
                DataSet _Dataset = new();
                using (DBAccess _DBAccess = new())
                {
                    
                    _DBAccess.DBProcedureName = "SP_SUBSUBCATEGORY";
                    _DBAccess.AddParameters("@Action", "UPDATESUBSUBCATEGORY");
                    _DBAccess.AddParameters("@ID", Data.id);
                    _DBAccess.AddParameters("@SUBSUBCATEGORY_NAME", Data.SUBSUBCATEGORY_NAME);
                    _DBAccess.AddParameters("@CATEGORY_NAME", Data.CATEGORY_NAME);
                    _DBAccess.AddParameters("@SUBCATEGORY_NAME", Data.SUBCATEGORY_NAME);
                    _DBAccess.AddParameters("@PRIORITY", Data.PRIORITY);
                    _Dataset = _DBAccess.DBExecute();
                    _DBAccess.Dispose();


                }
                if (_Dataset != null && _Dataset.Tables.Count > 0)
                {
                    DataTable _DataTable = _Dataset.Tables[0];
                    foreach (DataRow Row in _DataTable.Rows)
                    {
                        if (Row["RETVAL"]?.ToString() == "SUCCESS")
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

            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("UpdateSubsubCategory", "DLSubsubcategory", ex.Message);
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();

            }
            return _DBReturnData;

        }


        public DBReturnData DeleteSubsubCategory(MLDeletesubsubcategory Data)
        {
            MLSubsubcategory _MLSubsubcategory = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();


            try
            {
                DataSet _Dataset = new();
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_SUBSUBCATEGORY";
                    _DBAccess.AddParameters("@Action", "INSERTSUBSUBCATEGORY");
                    _DBAccess.AddParameters("@id", Data.id);
              


                }
                if (_Dataset != null && _Dataset.Tables.Count > 0)
                {
                    DataTable _DataTable = _Dataset.Tables[0];
                    foreach (DataRow Row in _DataTable.Rows)
                    {
                        if (Row["RETVAL"]?.ToString() == "SUCCESS")
                        {
                            _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                            _DBReturnData.Code = DBEnums.Codes.SUCCESS;

                        }
                        else
                        {
                            _DBReturnData.Message =DBEnums.Status.FAILURE.ToString();
                            _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                _DALBASE.ErrorLog("DeleteSubsubCategory", "DLSubsubcategory", ex.Message);
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.ToString();
            }
            return _DBReturnData;

        }
    }
}
