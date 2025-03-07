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
                        _DBReturnData.Code = 400;
                        _DBReturnData.Message = "FAILED";

                    }
                }
                else
                {
           
                    _DBReturnData.Code = 404;
                    _DBReturnData.Message = "No Subsubcategories found.";
                }
            }
            catch(Exception ex)
            {
                _DALBASE.ErrorLog("GetAllSubsubCategory", "DLSubsubcategory", ex.Message);
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error";

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
                    _DBAccess.AddParameters("@SUBCATEGORY_ID", Data.SUBCATEGORY_ID);
                    _DBAccess.AddParameters("@CATEGORY_ID", Data.CATEGORY_ID);
                    //_DBAccess.AddParameters("@CATEGORY_ID", Data.CATEGORY_ID);
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
                            _DBReturnData.Message = "SUCCESS";
                            _DBReturnData.Code = 200;

                        }
                        else
                        {
                            _DBReturnData.Message = "FAILED";
                            _DBReturnData.Code = 401;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("InsertSubsubCategory", "DLSubsubcategory", ex.Message);
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error";

            }
            return _DBReturnData;

        }


        public MLSubsubcategory UpdateSubsubCategory(MLUpdateSubsubcategory Data)
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
                            _DBReturnData.Message = "SUCCESS";
                            _DBReturnData.Code = 200;

                        }
                        else
                        {
                            _DBReturnData.Message = "FAILED";
                            _DBReturnData.Code = 401;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("UpdateSubsubCategory", "DLSubsubcategory", ex.Message);
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error";

            }
            return _MLSubsubcategory;

        }


        public MLSubsubcategory DeleteSubsubCategory(MLDeletesubsubcategory Data)
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
                    _DBAccess.AddParameters("@Action", "DELETESUBSUBCATEGORY"); // Fixing Action
                    _DBAccess.AddParameters("@id", Data.id);

                    // Execute the stored procedure and get the dataset
                    //_Dataset = _DBAccess.ExecuteDataSet();
                }

                if (_Dataset != null && _Dataset.Tables.Count > 0)
                {
                    // First Table - Data
                    DataTable _DataTable = _Dataset.Tables[0];
                    _MLSubsubcategory.SubsubCategoryList = new List<MLSubsubcategory.SubsubCategory>();

                    foreach (DataRow row in _DataTable.Rows)
                    {
                        MLSubsubcategory.SubsubCategory data = new MLSubsubcategory.SubsubCategory
                        {
                            //Id = Convert.ToInt32(row["Id"]),
                            //Name = row["Name"].ToString(),
                            //Description = row["Description"].ToString()
                            // Add more fields as per your database schema
                        };
                        _MLSubsubcategory.SubsubCategoryList.Add(data);
                    }

                    // Second Table - RETVAL
                    if (_Dataset.Tables.Count > 1 && _Dataset.Tables[1].Rows.Count > 0)
                    {
                        string? retVal = _Dataset.Tables[1].Rows[0]["RETVAL"]?.ToString();
                        if (retVal == "SUCCESS")
                        {
                            _DBReturnData.Message = "SUCCESS";
                            _DBReturnData.Code = 200;
                        }
                        else
                        {
                            _DBReturnData.Message = "FAILED";
                            _DBReturnData.Code = 401;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("DeleteSubsubCategory", "DLSubsubcategory", ex.Message);
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error";
            }

            return _MLSubsubcategory;
        }


        public MLSubsubcategory GetSubsubCategoryBySubCategoryId(MLSubsubcategoryBySubCategoryId Data)
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
                    _DBAccess.AddParameters("@Action", "GETSUBSUBCATEGORYBYSUBCATEGORYID");
                    _DBAccess.AddParameters("@ID", Data.id);
                    _Dataset = _DBAccess.DBExecute();
                }

                if (_Dataset != null && _Dataset.Tables.Count > 0)
                {
                    // First Table - Actual Data
                    if (_Dataset.Tables.Count > 0 && _Dataset.Tables[0].Rows.Count > 0)
                    {
                        DataTable _DataTable = _Dataset.Tables[0];
                        _MLSubsubcategory.SubsubCategoryList = new List<MLSubsubcategory.SubsubCategory>();

                        foreach (DataRow row in _DataTable.Rows)
                        {
                            MLSubsubcategory.SubsubCategory subcategory = new MLSubsubcategory.SubsubCategory
                            {
                           
                                Id = row["id"] == DBNull.Value ? 0 : Convert.ToInt32(row["Id"]),
                                SUBSUBCATEGORY_NAME = row["SUBSUBCATEGORY_NAME"]?.ToString() ?? string.Empty,
                                CATEGORY_NAME = row["Category_Name"]?.ToString() ?? string.Empty,
                                SUBCATEGORY_NAME = row["SubCategory_Name"]?.ToString() ?? string.Empty,
                                PRIORITY = row["Priority"] == DBNull.Value ? 0 : Convert.ToInt32(row["Priority"])
                            };
                            _MLSubsubcategory.SubsubCategoryList.Add(subcategory);
                        }
                    }

                    // Second Table - RETVAL
                    if (_Dataset.Tables.Count > 1 && _Dataset.Tables[1].Rows.Count > 0)
                    {
                        string? retVal = _Dataset.Tables[1].Rows[0]["RETVAL"]?.ToString();
                        if (retVal == "SUCCESS")
                        {
                            _DBReturnData.Message = "SUCCESS";
                            _DBReturnData.Code = 200;
                        }
                        else
                        {
                            _DBReturnData.Message = "FAILED";
                            _DBReturnData.Code = 401;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("GetSubsubCategoryBySubCategoryId", "DLSubsubcategory", ex.Message);
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error";
            }

            return _MLSubsubcategory;
        }


    }
}
