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
                        _DBReturnData.Code = 400;
                        _DBReturnData.Message = "FAILED";
                    }
                }
                else
                {
                    _DBReturnData.Code = 404;
                    _DBReturnData.Message = "No subcategories found.";
                }
            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("GetAllSubCategory", "DLSubcategory", ex.Message);
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error";
            }

           
            return _MLSubcategory.SubcategoryList;  
        }

        public MLSubcategory InsertSubCategory(MLInsertSubcategory Data)
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
                else
                {
                    _DBReturnData.Message = "NOT EXISTS";
                    _DBReturnData.Code = 400;

                }
            }
            catch (Exception ex)
            {

                _DALBASE.ErrorLog("InsertSubCategory", "DLSubcategory", ex.Message.ToString());
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Code = 500;

            }
            return _MLSubcategory;





            }

        public MLSubcategory UpdateSubCategory(MLUpdateSubcategory Data)
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
                else
                {
                    _DBReturnData.Message = "NOT EXISTS";
                    _DBReturnData.Code = 400;

                }
            }
            catch (Exception ex)
            {

                _DALBASE.ErrorLog("InsertSubCategory", "DLSubcategory", ex.Message.ToString());
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Code = 500;

            }
            return _MLSubcategory;





        }


        public MLSubcategory DeleteSubCategory(MLDeleteSubCateggory Data)
        {
            DLSubcategory _DLSubcategory = new DLSubcategory();
            MLSubcategory _MLSubcategory = new MLSubcategory();
            DALBASE _DALBASE=new DALBASE();
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
                if(_DataSet !=null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = _DataSet.Tables[0];
                    foreach (DataRow _DataRow in _DataTable.Rows)
                    {
                        if (_DataRow["RETVAL"]?.ToString() == "SUCCESS")
                        {
                            _DBReturnData.Message = "SUCCESS";
                            _DBReturnData.Code = 200;

                        }
                        else
                        {
                            _DBReturnData.Message = "FAILED";
                            _DBReturnData.Code = 400;

                        }
                    }
                }

            }
            catch(Exception ex)
            {
                _DALBASE.ErrorLog("DeleteSubCategory", "DLSubcategory", ex.Message);
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Code = 400;

            }
            return _MLSubcategory;

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
                using(DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_SUBCATEGORY";
                    _DBAccess.AddParameters("@Action", "GETSUBCATEGORYTHROUGHID");
                    _DBAccess.AddParameters("@Category_id", Data.Category_id);
                    _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();
                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable dataTable = _DataSet.Tables[0];
                    string Retval = _DataSet.Tables[1].Rows[0]["RETVAL"]?.ToString() ?? "";


                    if (Retval == "SUCCESS")
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            var subcategory  = new MLSubcategory.Subcategory
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
                        _DBReturnData.Code = 400;
                        _DBReturnData.Message = "FAILED";
                    }
                }
                else
                {
                    _DBReturnData.Code = 404;
                    _DBReturnData.Message = "No subcategories found.";
                }

            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("GetSubCategoryThroughCategoryId", "DLSubcategory", ex.Message);
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Code = 400;

            }
            return _MLSubcategory;

        }

        public MLSubcategory GetSubCategoryById(MLGetThroughId Data)
        {
            MLSubcategory _MLSubcate = new MLSubcategory();
            DLSubcategory _DLSubcate = new DLSubcategory();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new DBReturnData();


            try
            {
                DataSet _DataSet = new();
                using (DBAccess _DBACCESS = new DBAccess())
                {
                    _DBACCESS.DBProcedureName = "SP_SUBCATEGORY";
                    _DBACCESS.AddParameters("@Action", "SELECTBYCATEGORYID");
                    _DBACCESS.AddParameters("@id",Data.id);
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
                            _DBReturnData.Message = "SUCCESS";
                            _DBReturnData.Code = 200;

                        }
                        else
                        {
                            _DBReturnData.Message = "FAILED";
                            _DBReturnData.Code = 400;

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("GetSubCategoryById", "DLSubcategory", ex.Message);
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Code = 400;
            }
            return _MLSubcate;

        }


    }
} 
