using System.Data;
using ECOMAPP.CommonRepository;
using ECOMAPP.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using static ECOMAPP.ModelLayer.MLCategoryDTO;
using static ECOMAPP.ModelLayer.MLSubcategory;


namespace ECOMAPP.DataLayer
{
    public class DLSubcategory
    {

        public List<MLSubcategory.Subcategory> GetAllSubCategory()
        {
            MLSubcategory _MLSubcategory = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();


            DataSet _Dataset = new DataSet();
            try
            {
                _MLSubcategory.SubcategoryList = new List<MLSubcategory.Subcategory>();

                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_SUBCATEGORY";
                    _DBAccess.AddParameters("@Action", "GETALLSUBCATEGORY");
                    _Dataset = _DBAccess.DBExecute();
                    _DBAccess.Dispose();

                }

                if (_Dataset != null && _Dataset.Tables.Count > 0)
                {
                    DataTable dataTable = _Dataset.Tables[0];
                    string Retval = _Dataset.Tables[1].Rows[0]["RETVAL"]?.ToString() ?? "";


                    if (Retval == "SUCCESS")
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            var subcategory = new MLSubcategory.Subcategory
                            {
                                id = row["id"] == DBNull.Value ? 0 : Convert.ToInt32(row["id"]),
                                Category_Name = row["Category_Name"]?.ToString() ?? string.Empty,
                                Subcategory_Name = row["Subcategory_Name"]?.ToString() ?? string.Empty,
                                CreationDate = row["CreationDate"]?.ToString() ?? string.Empty,
                                Priority = row["priority"] == DBNull.Value ? 0 : Convert.ToInt32(row["priority"])
                            };
                            _MLSubcategory.SubcategoryList.Add(subcategory);
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
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("GetAllSubCategory", "DLSubcategory", ex.Message);
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
            }


            return _MLSubcategory.SubcategoryList;
        }

        public DBReturnData InsertSubCategory(MLInsertSubcategory Data)
        {

            DBReturnData _DBReturnData = new DBReturnData();
            DALBASE _DALBASE = new();
            try
            {
                DataSet _DataSet = new();
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_SUBCATEGORY";
                    _DBAccess.AddParameters("@Action", "INSERTSUBCATEGORY");
                    _DBAccess.AddParameters("@Subcategory_Name", Data.Subcategory_Name);
                    _DBAccess.AddParameters("@Category_Name", Data.Category_Name);
                    _DBAccess.AddParameters("@Category_id", Data.Category_id);
                    _DBAccess.AddParameters("@Priority", Data.Priority);
                    _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();

                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = _DataSet.Tables[0];
                    foreach (DataRow dr in _DataTable.Rows)
                    {
                        if (dr["RETVAL"]?.ToString() == "SUCCESS")
                        {
                            _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                            _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                        }
                        else
                        {
                            _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                            _DBReturnData.Code =DBEnums.Codes.INTERNAL_SERVER_ERROR;
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

                _DALBASE.ErrorLog("InsertSubCategory", "DLSubcategory", ex.Message.ToString());
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;

            }

            return _DBReturnData;

        }

        public DBReturnData UpdateSubCategory(MLUpdateSubcategory Data)
        {
            MLSubcategory _MLSubcategory = new MLSubcategory();
            DBReturnData _DBReturnData = new DBReturnData();
            DALBASE _DALBASE = new();
            try
            {
                DataSet _DataSet = new();
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_SUBCATEGORY";
                    _DBAccess.AddParameters("@Action", "UPDATESUBCATEGORY");
                    _DBAccess.AddParameters("@id", Data.id);
                    _DBAccess.AddParameters("@Subcategory_Name", Data.Subcategory_Name);
                    _DBAccess.AddParameters("@Category_Name", Data.Category_Name);
                    _DBAccess.AddParameters("@Category_id", Data.Category_id);
                    _DBAccess.AddParameters("@Priority", Data.Priority);
                    _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();

                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = _DataSet.Tables[0];
                    foreach (DataRow dr in _DataTable.Rows)
                    {
                        if (dr["RETVAL"]?.ToString() == "SUCCESS")
                        {

                            _DBReturnData.Message =DBEnums.Status.SUCCESS.ToString();
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

                _DALBASE.ErrorLog("InsertSubCategory", "DLSubcategory", ex.Message.ToString());
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;

            }
            return _DBReturnData;

        }


        public DBReturnData DeleteSubCategory(MLDeleteSubCateggory Data)
        {
            DLSubcategory _DLSubcategory = new DLSubcategory();
            MLSubcategory _MLSubcategory = new MLSubcategory();
            DALBASE _DALBASE = new DALBASE();
            DBReturnData _DBReturnData = new DBReturnData();
            try
            {
                DataSet _DataSet = new();
                using (DBAccess _DBAccess = new DBAccess())
                {
                    _DBAccess.DBProcedureName = "SP_SUBCATEGORY";
                    _DBAccess.AddParameters("@Action", "DELETESUBCATEGORY");
                    _DBAccess.AddParameters("@id", Data.id);
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

            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("DeleteSubCategory", "DLSubcategory", ex.Message);
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;

            }
            return _DBReturnData;

        }

      

        public MLSubcategory GetSubCategoryThroughCategoryId(MLGetThroughCategoryId Data)
        {
            MLSubcategory _MLSubcategory = new MLSubcategory();
            DLSubcategory _DLSubcategory = new DLSubcategory();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();

            try
            {
                DataSet _DataSet = new();
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_SUBCATEGORY";
                    _DBAccess.AddParameters("@Action", "GETSUBCATEGORYTHROUGHID");
                    _DBAccess.AddParameters("@Category_id", Data.Category_id);
                    _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();
                }
               
                if (_DataSet != null && _DataSet.Tables.Count > 0 && _DataSet.Tables[0].Rows.Count > 0)
                {
                    DataTable dataTable = _DataSet.Tables[0];
                    string Retval = _DataSet.Tables[1].Rows[0]["RETVAL"]?.ToString() ?? "";

                    if (Retval == "SUCCESS")
                    {
                        _MLSubcategory.SubCategoryListByCategory = new List<MLSubcategory.MLGetbySubCategory>();

                        foreach (DataRow row in dataTable.Rows)
                        {
                            var subcategory = new MLSubcategory.MLGetbySubCategory
                            {
                                id = row["id"] == DBNull.Value ? 0 : Convert.ToInt32(row["id"]),
                                Category_id = row["Category_id"] == DBNull.Value ? 0 : Convert.ToInt32(row["Category_id"]),
                                Category_Name = row["Category_Name"]?.ToString() ?? string.Empty,
                                Subcategory_Name = row["Subcategory_Name"]?.ToString() ?? string.Empty
                            };
                            _MLSubcategory.SubCategoryListByCategory.Add(subcategory);
                        }
                    }
                }
                else
                {
                    _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                    _DBReturnData.Message =DBEnums.Status.FAILURE.ToString();
                }
            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("GetSubCategoryThroughCategoryId", "DLSubcategory", ex.Message);
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;

            }
            return _MLSubcategory;

        }


        public DBReturnData GetSubCategoryById(MLGetThroughId Data)
        {
            MLSubcategory _MLSubcate = new MLSubcategory();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new DBReturnData();


            try
            {
                DataSet _DataSet = new();
                using (DBAccess _DBACCESS = new DBAccess())
                {
                    _DBACCESS.DBProcedureName = "SP_SUBCATEGORY";
                    _DBACCESS.AddParameters("@Action", "SELECTBYCATEGORYID");
                    _DBACCESS.AddParameters("@id", Data.id);
                    _DataSet = _DBACCESS.DBExecute();
                    _DBACCESS.Dispose();


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

            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("GetSubCategoryById", "DLSubcategory", ex.Message);
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
            }
            return _DBReturnData;

        }


    }
}
