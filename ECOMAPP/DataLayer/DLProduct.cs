using ECOMAPP.CommonRepository;
using ECOMAPP.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using static ECOMAPP.CommonRepository.DBEnums;
using static ECOMAPP.ModelLayer.MLCategoryDTO;
using static ECOMAPP.ModelLayer.MLProduct;
using static System.Net.Mime.MediaTypeNames;

namespace ECOMAPP.DataLayer
{
    public class DLProduct
    {
        public List<MLProduct.MlGetProduct> GetAllProducts(MlGetProduct data)
        {
            MLProduct _MLProduct = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_PRODUCT";
                    _DBAccess.AddParameters("@Action", "SELECTPRODUCT");
                    _DataSet = _DBAccess.DBExecute();
                    _DataSet.Dispose();

                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = new();
                    string Retval = _DataSet.Tables[1].Rows[0]["RETVAL"]?.ToString() ?? "";
                    foreach (DataRow row in _DataTable.Rows)
                    {
                        if (Retval == "SUCCESS")
                        {
                            MLProduct.MlGetProduct Product = new MLProduct.MlGetProduct()
                            {


                                Id = Convert.IsDBNull(row["Id"]) ? 0 : Convert.ToInt32(row["Id"]),
                                Category_id = row["Category_id"]?.ToString() ?? string.Empty,
                                Product_Name = row["Product_Name"]?.ToString() ?? string.Empty,
                                CreationDate = row["CreationDate"]?.ToString() ?? string.Empty,
                                Image = row["Image"]?.ToString() ?? string.Empty,
                                Description = row["Description"]?.ToString() ?? string.Empty,
                                Rating = row["Rating"]?.ToString() ?? string.Empty,
                                SubSubcategory_id = Convert.IsDBNull(row["SubSubcategory_id"]) ? 0 : Convert.ToInt32(row["SubSubcategory_id"]),
                                UpdationDate = row["UpdationDate"]?.ToString() ?? string.Empty,
                                Price = Convert.IsDBNull(row["Price"]) ? 0 : Convert.ToInt32(row["Price"])
                            };

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

            } catch (Exception ex) 
            {
                _DALBASE.ErrorLog("GetAllProducts", "DLProduct", ex.ToString());
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Code = 500;
            }

            return _MLProduct.ProductList;
        }

        public MLProduct InsertProduct(MlGetProduct data)
        {
            MLProduct _MLProduct = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new(); 

            DataSet _DataSet = new();
            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_PRODUCT";
                    _DBAccess.AddParameters("@Action", "INSERTPRODUCT");
                    _DBAccess.AddParameters("@Product_Name", data.Product_Name ?? "");
                    _DBAccess.AddParameters("@Product_Name", data.Product_id ?? "");
                    _DBAccess.AddParameters("@Quantity", data.Quantity);
                    _DBAccess.AddParameters("@Image", data.Image ?? "");
                    _DBAccess.AddParameters("@Description", data.Description ?? "");
                    _DBAccess.AddParameters("@Rating", data.Rating ?? "");
                    _DBAccess.AddParameters("@Category_id", data.Category_id ?? "");
                    _DBAccess.AddParameters("@CreationDate", data.CreationDate ?? "");
                    _DBAccess.AddParameters("@SubSubcategory_id", data.SubSubcategory_id);
                    _DBAccess.AddParameters("@UpdationDate", data.UpdationDate ?? "");
                    _DBAccess.AddParameters("@Price", data.Price);
                    _DataSet = _DBAccess.DBExecute();
                    _DataSet.Dispose();

                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = new();
                    foreach (DataRow row in _DataSet.Tables)
                    {
                        if (row["RETVAL"]?.ToString() == "SUCCESS")
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
                _DALBASE.ErrorLog("InsertProduct", "DLProduct", ex.ToString());
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Code = 500;

            }

            return _MLProduct;
        }



        public MLProduct UpdateProduct(MlGetProduct data)
        {
            MLProduct _MLProduct = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_PRODUCT";
                    _DBAccess.AddParameters("@Action", "INSERTPRODUCT");
                    _DBAccess.AddParameters("@Product_Name", data.Product_Name ?? "");
                    _DBAccess.AddParameters("@Product_Name", data.Product_id ?? "");
                    _DBAccess.AddParameters("@Quantity", data.Quantity);
                    _DBAccess.AddParameters("@Image", data.Image ?? "");
                    _DBAccess.AddParameters("@Description", data.Description ?? "");
                    _DBAccess.AddParameters("@Rating", data.Rating ?? "");
                    _DBAccess.AddParameters("@Category_id", data.Category_id ?? "");
                    _DBAccess.AddParameters("@CreationDate", data.CreationDate ?? "");
                    _DBAccess.AddParameters("@SubSubcategory_id", data.SubSubcategory_id);
                    _DBAccess.AddParameters("@UpdationDate", data.UpdationDate ?? "");
                    _DBAccess.AddParameters("@Price", data.Price);
                    _DataSet = _DBAccess.DBExecute();
                    _DataSet.Dispose();

                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = new();
                    foreach (DataRow row in _DataSet.Tables)
                    {
                        if (row["RETVAL"]?.ToString() == "SUCCESS")
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
                _DALBASE.ErrorLog("UpdateProduct", "DLProduct", ex.ToString());
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Code = 500;
            }

            return _MLProduct;
        }


        public MLProduct DeleteProduct(MLDeleteProduct data)
        {
            MLProduct _MLProduct = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_PRODUCT";
                    _DBAccess.AddParameters("@Action", "INSERTPRODUCT");
                    _DBAccess.AddParameters("@Product_Name", data.Product_id ?? "");
                    _DataSet = _DBAccess.DBExecute();
                    _DataSet.Dispose();

                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = new();
                    foreach (DataRow row in _DataSet.Tables)
                    {
                        if (row["RETVAL"]?.ToString() == "SUCCESS")
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
                _DALBASE.ErrorLog("DeleteProduct", "DLProduct", ex.ToString());
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Code = 500;
            }

            return _MLProduct;
        }



        public MLProduct ProductToggleCertified(MLToggleCertified data)
        {
            MLProduct _MLProduct = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_PRODUCT";
                    _DBAccess.AddParameters("@Action", "INSERTPRODUCT");
                    _DBAccess.AddParameters("@Product_Name", data.Product_id ?? "");
                    _DataSet = _DBAccess.DBExecute();
                    _DataSet.Dispose();

                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = new();
                    foreach (DataRow row in _DataSet.Tables)
                    {
                        if (row["RETVAL"]?.ToString() == "SUCCESS")
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
                _DALBASE.ErrorLog("ProductToggleCertified", "DLProduct", ex.ToString());
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Code = 500;

            }

            return _MLProduct;
        }

        public MLProduct ProductToggleStatus(MLToggleStatus data)
        {
            MLProduct _MLProduct = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_PRODUCT";
                    _DBAccess.AddParameters("@Action", "INSERTPRODUCT");
                    _DBAccess.AddParameters("@Product_Name", data.Product_id ?? "");
                    _DataSet = _DBAccess.DBExecute();
                    _DataSet.Dispose();

                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = new();
                    foreach (DataRow row in _DataSet.Tables)
                    {
                        if (row["RETVAL"]?.ToString() == "SUCCESS")
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
                _DALBASE.ErrorLog("ProductToggleStatus", "DLProduct", ex.ToString());
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Code = 500;

            }

            return _MLProduct;
        }





        #region  InhouseProduct
        public List<MLProduct.MLGetInhouseProduct> GetAllInhouseProducts(MLGetInhouseProduct data)
        {
            MLProduct _MLProduct = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_PRODUCT";
                    _DBAccess.AddParameters("@Action", "SELECTPRODUCT");
                    _DataSet = _DBAccess.DBExecute();
                    _DataSet.Dispose();

                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = new();
                    string Retval = _DataSet.Tables[1].Rows[0]["RETVAL"]?.ToString() ?? "";
                    foreach (DataRow row in _DataSet.Tables)
                    {
                        if (Retval == "SUCCESS")
                        {
                            MLProduct.MLGetInhouseProduct InhouseProduct = new MLProduct.MLGetInhouseProduct()
                            {


                                Id = Convert.IsDBNull(row["Id"]) ? 0 : Convert.ToInt32(row["Id"]),
                                Category_id = row["Category_id"]?.ToString() ?? string.Empty,
                                Product_Name = row["Product_Name"]?.ToString() ?? string.Empty,
                                CreationDate = row["CreationDate"]?.ToString() ?? string.Empty,
                                Image = row["Image"]?.ToString() ?? string.Empty,
                                Description = row["Description"]?.ToString() ?? string.Empty,
                                Rating = row["Rating"]?.ToString() ?? string.Empty,
                                SubSubcategory_id = Convert.IsDBNull(row["SubSubcategory_id"]) ? 0 : Convert.ToInt32(row["SubSubcategory_id"]),
                                UpdationDate = row["UpdationDate"]?.ToString() ?? string.Empty,
                                Price = Convert.IsDBNull(row["Price"]) ? 0 : Convert.ToInt32(row["Price"])
                            };
                            _MLProduct.InhouseProductList.Add(InhouseProduct);

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
                _DALBASE.ErrorLog("GetAllInhouseProducts", "DLProduct", ex.ToString());
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Code = 500;
            }

            return _MLProduct.InhouseProductList;
        }

        public MLProduct InsertInhouseProduct(MLInsertInhouseProduct data)
        {
            MLProduct _MLProduct = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_PRODUCT";
                    _DBAccess.AddParameters("@Action", "INSERTPRODUCT");
                    _DBAccess.AddParameters("@Product_Name", data.Product_Name ?? "");
                    _DBAccess.AddParameters("@Product_Name", data.Product_id ?? "");
                    _DBAccess.AddParameters("@Quantity", data.Quantity);
                    _DBAccess.AddParameters("@Image", data.Image ?? "");
                    _DBAccess.AddParameters("@Description", data.Description ?? "");
                    _DBAccess.AddParameters("@Rating", data.Rating ?? "");
                    _DBAccess.AddParameters("@Category_id", data.Category_id ?? "");
                    _DBAccess.AddParameters("@CreationDate", data.CreationDate ?? "");
                    _DBAccess.AddParameters("@SubSubcategory_id", data.SubSubcategory_id);
                    _DBAccess.AddParameters("@UpdationDate", data.UpdationDate ?? "");
                    _DBAccess.AddParameters("@Price", data.Price);
                    _DataSet = _DBAccess.DBExecute();
                    _DataSet.Dispose();

                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = new();
                    foreach (DataRow row in _DataSet.Tables)
                    {
                        if (row["RETVAL"]?.ToString() == "SUCCESS")
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
                _DALBASE.ErrorLog("InsertInhouseProduct", "DLProduct", ex.ToString());
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Code = 500;

            }

            return _MLProduct;
        }

        public MLProduct UpdateInhouseProduct(MLUpdateInhouseProduct data)
        {
            MLProduct _MLProduct = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_PRODUCT";
                    _DBAccess.AddParameters("@Action", "INSERTPRODUCT");
                    _DBAccess.AddParameters("@Product_Name", data.Product_Name ?? "");
                    _DBAccess.AddParameters("@Product_Name", data.Product_id ?? "");
                    _DBAccess.AddParameters("@Quantity", data.Quantity);
                    _DBAccess.AddParameters("@Image", data.Image ?? "");
                    _DBAccess.AddParameters("@Description", data.Description ?? "");
                    _DBAccess.AddParameters("@Rating", data.Rating ?? "");
                    _DBAccess.AddParameters("@Category_id", data.Category_id ?? "");
                    _DBAccess.AddParameters("@CreationDate", data.CreationDate ?? "");
                    _DBAccess.AddParameters("@SubSubcategory_id", data.SubSubcategory_id);
                    _DBAccess.AddParameters("@UpdationDate", data.UpdationDate ?? "");
                    _DBAccess.AddParameters("@Price", data.Price);
                    _DataSet = _DBAccess.DBExecute();
                    _DataSet.Dispose();

                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = new();
                    foreach (DataRow row in _DataSet.Tables)
                    {
                        if (row["RETVAL"]?.ToString() == "SUCCESS")
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
                _DALBASE.ErrorLog("UpdateInhouseProduct", "DLProduct", ex.ToString());
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Code = 500;

            }

            return _MLProduct;
        }

        public MLProduct DeleteInhouseProduct(MLDeleteInhouseProduct data)
        {
            MLProduct _MLProduct = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_PRODUCT";
                    _DBAccess.AddParameters("@Action", "INSERTPRODUCT");
                    _DBAccess.AddParameters("@Id", data.Id);
                    _DataSet = _DBAccess.DBExecute();
                    _DataSet.Dispose();

                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = new();
                    foreach (DataRow row in _DataSet.Tables)
                    {
                        if (row["RETVAL"]?.ToString() == "SUCCESS")
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
                _DALBASE.ErrorLog("DeleteInhouseProduct", "DLProduct", ex.ToString());
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Code = 500;

            }

            return _MLProduct;
        }

        #endregion InhouseProduct

    }
}
