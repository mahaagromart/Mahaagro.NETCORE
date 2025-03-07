using ECOMAPP.ModelLayer;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using ECOMAPP.CommonRepository;
using static ECOMAPP.CommonRepository.DBEnums;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;

namespace ECOMAPP.DataLayer
{
    public class DLCatagory 
    {

        MLCategoryDTO objMLCategory = new();
        DALBASE DS = new DALBASE();
        public MLCategoryDTO GetAllCategory()
        {
            DataSet dataSet = new DataSet();
            try
            {
                objMLCategory.CategoryList = new List<MLCategoryDTO.Category>();

                using DBAccess Db = new();
                Db.DBProcedureName = "SP_CATEGORY";
                Db.AddParameters("@Action", "SELECTCATAGORY");
                dataSet = Db.DBExecute();
                Db.Dispose();
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    DataTable datatable = dataSet.Tables[0];

                    string retval = dataSet.Tables[1].Rows[0]["RETVAL"]?.ToString() ?? "";

       

                    foreach (DataRow row in datatable.Rows)
                    {

                        if (retval == "SUCCESS")
                        {


                            MLCategoryDTO.Category Category = new MLCategoryDTO.Category
                            {
                                
                                Category_id = Convert.IsDBNull(row["Category_id"]) ? 0 : Convert.ToInt32(row["Category_id"]),
                                Category_Name = row["Category_Name"]?.ToString() ?? string.Empty,
                                CreationDate = row["CreationDate"]?.ToString() ?? string.Empty,
                                Image = row["Image"]?.ToString() ?? string.Empty,
                                Priority = Convert.IsDBNull(row["priority"]) ? 0 : Convert.ToInt32(row["priority"]),
                                Status = row["Status"].ToString() ?? string.Empty,

                            };
                            objMLCategory.CategoryList.Add(Category);
                        }
                    }

                }

            }
            catch (Exception ex) 
            {

                DS.ErrorLog("DLCategory", "GetAllCategory", ex.ToString());
                objMLCategory.Message = "Internal Server Error";
                objMLCategory.Code = 500;

            }

            return objMLCategory;

        }
        public MLCategoryDTO InsertProductCategory(MlInsertProductCategoryData data)
        {
            MLCategoryDTO category = new MLCategoryDTO();
            try
            {
                DataSet ds = new DataSet();
                using (DBAccess Db = new())
                {
                    Db.DBProcedureName = "SP_CATEGORY";
                    Db.AddParameters("@Action", "INSERTCATEGORY");
                    Db.AddParameters("@Category_Name",data.Category_Name??"");
                    Db.AddParameters("@Image",data.Image??"");
                    Db.AddParameters("@Priority",data.priority);
                    ds = Db.DBExecute();
                    Db.Dispose();
                }
                if(ds != null && ds.Tables.Count > 0)
                {
                    DataTable Dtable = ds.Tables[0];
                    foreach (DataRow dr in Dtable.Rows) 
                    {
                        if (dr["RETVAL"]?.ToString() == "SUCCESS")
                        {
                            category.Message = "SUCCESS";
                            category.Code = 200; 
                        }
                        else
                        {
                            category.Message = "failed to insert";
                            category.Code = 401;
                        }
                    }


                }
                else
                {
                    category.Message = "NOT EXISTS";
                    category.Code = 400;
                }



            }
            catch (Exception ex) 
            {
                DS.ErrorLog("DLCategory", "InsertProductCategory", ex.ToString());
                objMLCategory.Message = "Internal Server Error";
                objMLCategory.Code = 500;
            }
            return category;


        }
        public MLCategoryDTO UpdateProductCategory(MlUpdateProductCategoryData data)
        {
            MLCategoryDTO category = new MLCategoryDTO();
            try
            {
                DataSet ds = new DataSet();
                using (DBAccess Db = new())
                {
                    Db.DBProcedureName = "SP_CATEGORY";
                    Db.AddParameters("@Action", "UPDATECATEGORY");
                    Db.AddParameters("@Category_id", data.Category_id);
                    Db.AddParameters("@Category_Name", data.Category_Name ?? "");
                    Db.AddParameters("@Image", data.Image ?? "");
                    ds = Db.DBExecute();
                    Db.Dispose();
                }
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable Dtable = ds.Tables[0];
                    foreach (DataRow dr in Dtable.Rows)
                    {
                        if (dr["RETVAL"]?.ToString() == "SUCCESS")
                        {
                            category.Message = "SUCCESS";
                            category.Code = 200;
                        }
                        else
                        {
                            category.Message = "failed to insert";
                            category.Code = 401;
                        }
                    }


                }
                else
                {
                    category.Message = "NOT EXISTS";
                    category.Code = 400;
                }



            }
            catch (Exception ex)
            {
                DS.ErrorLog("DLCategory", "UpdateProductCategory", ex.ToString());
                objMLCategory.Message = "Internal Server Error";
                objMLCategory.Code = 500;
            }
            return category;


        }
        public DBReturnData DeleteProductCategory(MlDeleteProductCategory data)
        {
            MLCategoryDTO _MLCategoryDTO = new MLCategoryDTO();
            DBReturnData _DBReturnData = new();

            try
            {
                DataSet _DataSet = new();
                using (DBAccess dB = new DBAccess())
                {
                    dB.DBProcedureName = "SP_CATEGORY";
                    dB.AddParameters("@Action", "DELETECATEGORY");
                    dB.AddParameters("@Category_id", data.Category_id);
                    _DataSet = dB.DBExecute();
                }

                if (_DataSet != null && _DataSet.Tables.Count > 0 && _DataSet.Tables[0].Rows.Count > 0)
                {
                    string Retval = _DataSet.Tables[0].Rows[0]["RETVAL"]?.ToString() ?? "";
                    if (Retval == "SUCCESS")
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
                else
                {
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                }
            }
            catch (Exception ex)
            {
                DS.ErrorLog("DLCategory", "DeleteProductCategory", ex.ToString());
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
            }

            return _DBReturnData;
        }





    }
}
