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
        //public List<MLProduct.MlGetProduct> GetAllProducts()
        //{
        //    List<MLProduct.MlGetProduct> products = new();
        //    DALBASE _DALBASE = new();

        //    try
        //    {
        //        using (DBAccess _DBAccess = new())
        //        {
        //            _DBAccess.DBProcedureName = "SP_PRODUCT";
        //            _DBAccess.AddParameters("@Action", "SELECTPRODUCTANDPRICELOGISTICES");

        //            using DataSet _DataSet = _DBAccess.DBExecute();


        //            if (_DataSet == null || _DataSet.Tables.Count < 4)
        //                return products;

        //            DataTable productTable = _DataSet.Tables[0];
        //            DataTable pricingTable = _DataSet.Tables[1];
        //            DataTable logisticsTable = _DataSet.Tables[2];
        //            DataTable imagesTable=_DataSet.Tables[3];

        //            foreach (DataRow productRow in productTable.Rows)
        //            {
        //                string PROD_ID = productRow["PROD_ID"]?.ToString() ?? string.Empty;

        //                #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        //                DataRow pricingRow = pricingTable.AsEnumerable().FirstOrDefault(row => row["PROD_ID"]?.ToString() == PROD_ID);

        //                DataRow logisticsRow = logisticsTable.AsEnumerable().FirstOrDefault(row => row["PROD_ID"]?.ToString() == PROD_ID);

        //                string Retval = _DataSet.Tables[4].Rows[0]["RETVAL"]?.ToString() ?? "";

        //                if (Retval  == "SUCCESS")
        //                {

        //                    MLProduct.MlGetProduct product = new()
        //                    {
        //                        Id = productId,
        //                        CATEGORY_ID = productRow["CATEGORY"]?.ToString() ?? string.Empty,
        //                        Product_Name = productRow["Product_Name"]?.ToString() ?? string.Empty,
        //                        SUB_CATEGORY_ID = productRow["SUB_CATEGORY"]?.ToString() ?? string.Empty,
        //                        IMAGES = productRow["IMAGES"]?.ToString() ?? string.Empty,
        //                        BRAND = productRow["BRAND"]?.ToString() ?? string.Empty,
        //                        sku = productRow["sku"]?.ToString() ?? string.Empty,
        //                        UNIT = productRow["UNIT"]?.ToString() ?? string.Empty,
        //                        TAGS_INPUT = productRow["TAGS_INPUT"] != DBNull.Value
        //                       ? productRow["TAGS_INPUT"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries)
        //                       : Array.Empty<string>(),
        //                        HSN = productRow["HSN"]?.ToString() ?? string.Empty,


        //                        PRICING = pricingRow != null ? Convert.ToInt32(pricingRow["PRICING"]) : 0,
        //                        MAXIMUM_RETAIL_PRICE = pricingRow != null ? Convert.ToInt32(pricingRow["MAXIMUM_RETAIL_PRICE"]) : 0,
        //                        SELLING_PRICE = pricingRow != null ? Convert.ToInt32(pricingRow["SELLING_PRICE"]) : 0,
        //                        MINIMUM_ORDER_QUANTITY = pricingRow != null ? Convert.ToInt32(pricingRow["MINIMUM_ORDER_QUANTITY"]) : 0,
        //                        CURRENT_STOCK_QUANTITY = pricingRow != null ? Convert.ToInt32(pricingRow["CURRENT_STOCK_QUANTITY"]) : 0,
        //                        DISCOUNT_TYPE = pricingRow?["DISCOUNT_TYPE"]?.ToString() ?? string.Empty,
        //                        DISCOUNT_AMOUNT = pricingRow?["DISCOUNT_AMOUNT"]?.ToString() ?? string.Empty,
        //                        TAX_AMOUNT = pricingRow?["TAX_AMOUNT"]?.ToString() ?? string.Empty,
        //                        TAX_CALCULATION = pricingRow?["TAX_CALCULATION"]?.ToString() ?? string.Empty,
        //                        CALCULATED_PRICE = pricingRow?["CALCULATED_PRICE"]?.ToString() ?? string.Empty,

        //                        PACKAGE_WEIGHT = logisticsRow != null ? Convert.ToInt32(logisticsRow["PACKAGE_WEIGHT"]) : 0,
        //                        PACKAGE_SHAPE = logisticsRow?["PACKAGE_SHAPE"]?.ToString() ?? string.Empty,
        //                        PACKAGE_LENGTH = logisticsRow?["PACKAGE_LENGTH"]?.ToString() ?? string.Empty,
        //                        PACKAGE_WIDTH = logisticsRow?["PACKAGE_WIDTH"]?.ToString() ?? string.Empty,
        //                        PACKAGE_HEIGHT = logisticsRow?["PACKAGE_HEIGHT"]?.ToString() ?? string.Empty,
        //                        PACKAGE_DIAMETER = logisticsRow?["PACKAGE_DIAMETER"]?.ToString() ?? string.Empty,
        //                        PACKAGE_TOTAL_VOLUME = logisticsRow?["PACKAGE_TOTAL_VOLUME"]?.ToString() ?? string.Empty,
        //                    };

        //                    products.Add(product);
        //                }


        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _DALBASE.ErrorLog("GetAllProducts", "DLProduct", ex.ToString());
        //    }

        //    return products;
        //}


        public List<MLProduct.MlGetProduct> GetAllProducts()
        {
            List<MLProduct.MlGetProduct> products = new();
            DALBASE _DALBASE = new();

            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_PRODUCT";
                    _DBAccess.AddParameters("@Action", "SELECTPRODUCTANDPRICELOGISTICES");

                    using DataSet _DataSet = _DBAccess.DBExecute();

                    // Ensure all required tables exist
                    if (_DataSet == null || _DataSet.Tables.Count < 5)
                        return products;

                    DataTable productTable = _DataSet.Tables[0];
                    DataTable pricingTable = _DataSet.Tables[1];
                    DataTable logisticsTable = _DataSet.Tables[2];
                    DataTable imagesTable = _DataSet.Tables[3];

                    string retval = _DataSet.Tables[4].Rows[0]["RETVAL"]?.ToString() ?? "";

                    if (retval == "SUCCESS")
                    {
                        foreach (DataRow productRow in productTable.Rows)
                        {
                            string PROD_ID = productRow["PROD_ID"]?.ToString() ?? string.Empty;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                            DataRow pricingRow = pricingTable.AsEnumerable()
                                .FirstOrDefault(row => row["PROD_ID"]?.ToString() == PROD_ID);
                            DataRow logisticsRow = logisticsTable.AsEnumerable()
                                .FirstOrDefault(row => row["PROD_ID"]?.ToString() == PROD_ID);

                                                    // Get all images for the product
                                                    List<MLImages> images = imagesTable.AsEnumerable()
                                              .Where(row => row["PROD_ID"]?.ToString() == PROD_ID)
                                              .OrderBy(row => Convert.ToInt32(row["ID"])) // Sort by priority
                                              .Select(row => new MLImages
                                              {
                                                  ID = Convert.ToInt32(row["ID"]),
                                                  Product_Images = row["Product_Images"]?.ToString() ?? string.Empty,
                                                  PROD_ID = row["PROD_ID"]?.ToString() ?? string.Empty
                                              })
                                              .ToList();


                            MLProduct.MlGetProduct product = new()
                            {
                                PROD_ID = PROD_ID,
                                Id = productRow["ID"] != DBNull.Value ? Convert.ToInt32(productRow["ID"]) : 0,
                                CATEGORY_ID = productRow["CATEGORY_ID"]?.ToString() ?? string.Empty,
                                CERTIFICATION = productRow["CERTIFICATION"] != DBNull.Value ? Convert.ToInt32(productRow["CERTIFICATION"]) : 0,
                                STATUS = productRow["STATUS"] != DBNull.Value ? Convert.ToInt32(productRow["STATUS"]) : 0,
                                Product_Name = productRow["Product_Name"]?.ToString() ?? string.Empty,
                                Product_Description = productRow["PRODUCT_DESCRIPTION"]?.ToString() ?? string.Empty,
                                RATING = productRow["RATING"]?.ToString() ?? string.Empty,
                                SUB_CATEGORY_ID = productRow["SUB_CATEGORY_ID"]?.ToString() ?? string.Empty,
                                IMAGES = images.Count > 0 ? images.First().Product_Images : string.Empty, // Set single image
                                IMAGESLIST = images.Count > 0 ? images : null, // Set full list (nullable)
                                BRAND = productRow["BRAND"]?.ToString() ?? string.Empty,
                                sku = productRow["sku"]?.ToString() ?? string.Empty,
                                UNIT = productRow["UNIT"]?.ToString() ?? string.Empty,
                                TAGS_INPUT = productRow["TAGS_INPUT"] != DBNull.Value
                                    ? productRow["TAGS_INPUT"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries)
                                    : Array.Empty<string>(),
                                HSN = productRow["HSN"]?.ToString() ?? string.Empty,

                                // Pricing
                                PRICING = pricingRow != null && pricingRow["PRICING"] != DBNull.Value
                                    ? Convert.ToInt32(pricingRow["PRICING"])
                                    : 0,

                                MAXIMUM_RETAIL_PRICE = pricingRow != null && pricingRow["MAXIMUM_RETAIL_PRICE"] != DBNull.Value
                                    ? Convert.ToInt32(pricingRow["MAXIMUM_RETAIL_PRICE"])
                                    : 0,

                                SELLING_PRICE = pricingRow != null && pricingRow["SELLING_PRICE"] != DBNull.Value
                                    ? Convert.ToInt32(pricingRow["SELLING_PRICE"])
                                    : 0,

                                MINIMUM_ORDER_QUANTITY = pricingRow != null && pricingRow["MINIMUM_ORDER_QUANTITY"] != DBNull.Value
                                    ? Convert.ToInt32(pricingRow["MINIMUM_ORDER_QUANTITY"])
                                    : 0,

                                CURRENT_STOCK_QUANTITY = pricingRow != null && pricingRow["CURRENT_STOCK_QUANTITY"] != DBNull.Value
                                    ? Convert.ToInt32(pricingRow["CURRENT_STOCK_QUANTITY"])
                                    : 0,

                                DISCOUNT_TYPE = pricingRow?["DISCOUNT_TYPE"]?.ToString() ?? string.Empty,
                                DISCOUNT_AMOUNT = pricingRow?["DISCOUNT_AMOUNT"]?.ToString() ?? string.Empty,
                                TAX_AMOUNT = pricingRow?["TAX_AMOUNT"]?.ToString() ?? string.Empty,
                                TAX_CALCULATION = pricingRow?["TAX_CALCULATION"]?.ToString() ?? string.Empty,
                                CALCULATED_PRICE = pricingRow?["CALCULATED_PRICE"]?.ToString() ?? string.Empty,

                                // Logistics
                                PACKAGE_WEIGHT = logisticsRow != null && logisticsRow["PACKAGE_WEIGHT"] != DBNull.Value
                                    ? Convert.ToInt32(logisticsRow["PACKAGE_WEIGHT"])
                                    : 0,

                                PACKAGE_SHAPE = logisticsRow?["PACKAGE_SHAPE"]?.ToString() ?? string.Empty,
                                PACKAGE_LENGTH = logisticsRow?["PACKAGE_LENGTH"]?.ToString() ?? string.Empty,
                                PACKAGE_WIDTH = logisticsRow?["PACKAGE_WIDTH"]?.ToString() ?? string.Empty,
                                PACKAGE_HEIGHT = logisticsRow?["PACKAGE_HEIGHT"]?.ToString() ?? string.Empty,
                                PACKAGE_DIAMETER = logisticsRow?["PACKAGE_DIAMETER"]?.ToString() ?? string.Empty,
                                PACKAGE_TOTAL_VOLUME = logisticsRow?["PACKAGE_TOTAL_VOLUME"]?.ToString() ?? string.Empty,
                            };

                            products.Add(product);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("GetAllProducts", "DLProduct", ex.ToString());
            }

            return products;
        }

        public DBReturnData InsertProduct(MlGetProduct data)
        {
            MLProduct _MLProduct = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {
                using (DBAccess _DBAccess = new())
                {





                    string tagsString = data.TAGS_INPUT != null ? string.Join(",", data.TAGS_INPUT) : "";
                    _DBAccess.DBProcedureName = "SP_PRODUCT";
                    _DBAccess.AddParameters("@Action", "INSERTPRODUCT");
                    _DBAccess.AddParameters("@Product_Name", data.Product_Name ?? "");
                    _DBAccess.AddParameters("@Product_Description", data.Product_Description ?? "");
                    _DBAccess.AddParameters("@SUB_CATEGORY_ID", data.SUB_CATEGORY_ID);
                    _DBAccess.AddParameters("@CATEGORY_ID", data.CATEGORY_ID);
                    _DBAccess.AddParameters("@IMAGES", data.IMAGES ?? "");
                    _DBAccess.AddParameters("@BRAND", data.BRAND ?? "");
                    _DBAccess.AddParameters("@sku", data.sku ?? "");
                    _DBAccess.AddParameters("@UNIT", data.UNIT ?? "");
                    _DBAccess.AddParameters("@HSN", data.HSN ?? "");
                    _DBAccess.AddParameters("@TAGS_INPUT", tagsString);

                    //pricing
                    _DBAccess.AddParameters("@PRICING", data.PRICING);
                    _DBAccess.AddParameters("@MAXIMUM_RETAIL_PRICE", data.MAXIMUM_RETAIL_PRICE);
                    _DBAccess.AddParameters("@SELLING_PRICE", data.SELLING_PRICE);
                    _DBAccess.AddParameters("@MINIMUM_ORDER_QUANTITY", data.MINIMUM_ORDER_QUANTITY);
                    _DBAccess.AddParameters("@CURRENT_STOCK_QUANTITY", data.CURRENT_STOCK_QUANTITY);
                    _DBAccess.AddParameters("@DISCOUNT_TYPE", data.DISCOUNT_TYPE);
                    _DBAccess.AddParameters("@DISCOUNT_AMOUNT", data.DISCOUNT_AMOUNT);
                    _DBAccess.AddParameters("@TAX_AMOUNT", data.TAX_AMOUNT);
                    _DBAccess.AddParameters("@TAX_CALCULATION", data.TAX_CALCULATION);
                    _DBAccess.AddParameters("@CALCULATED_PRICE", data.CALCULATED_PRICE);

                    //logistices starts
                    _DBAccess.AddParameters("@PACKAGE_WEIGHT", data.PACKAGE_WEIGHT);
                    _DBAccess.AddParameters("@PACKAGE_SHAPE", data.PACKAGE_SHAPE);
                    _DBAccess.AddParameters("@PACKAGE_LENGTH", data.PACKAGE_LENGTH);
                    _DBAccess.AddParameters("@PACKAGE_WIDTH", data.PACKAGE_WIDTH);
                    _DBAccess.AddParameters("@PACKAGE_HEIGHT", data.PACKAGE_HEIGHT);
                    _DBAccess.AddParameters("@PACKAGE_DIAMETER", data.PACKAGE_DIAMETER);
                    _DBAccess.AddParameters("@PACKAGE_TOTAL_VOLUME", data.PACKAGE_TOTAL_VOLUME);
                    _DBAccess.AddParameters("@VAREINTS_NAME", data.VAREINTS_NAME);



                    _DataSet = _DBAccess.DBExecute();
                    _DataSet.Dispose();


                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {

                    DataTable _DataTable = _DataSet.Tables[0];
                    foreach (DataRow row in _DataTable.Rows)
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

            //return _MLProduct;
            return _DBReturnData;
        }



        //public MLProduct UpdateProduct(MlGetProduct data)
        //{
        //    MLProduct _MLProduct = new();
        //    DALBASE _DALBASE = new();
        //    DBReturnData _DBReturnData = new();

        //    DataSet _DataSet = new();
        //    try
        //    {
        //        using (DBAccess _DBAccess = new())
        //        {
        //            _DBAccess.DBProcedureName = "SP_PRODUCT";
        //            _DBAccess.AddParameters("@Action", "INSERTPRODUCT");
        //            _DBAccess.AddParameters("@Product_Name", data.Product_Name ?? "");
        //            _DBAccess.AddParameters("@Product_Name", data.Product_id ?? "");
        //            _DBAccess.AddParameters("@Quantity", data.Quantity);
        //            _DBAccess.AddParameters("@Image", data.Image ?? "");
        //            _DBAccess.AddParameters("@Description", data.Description ?? "");
        //            _DBAccess.AddParameters("@Rating", data.Rating ?? "");
        //            _DBAccess.AddParameters("@Category_id", data.Category_id ?? "");
        //            _DBAccess.AddParameters("@CreationDate", data.CreationDate ?? "");
        //            _DBAccess.AddParameters("@SubSubcategory_id", data.SubSubcategory_id);
        //            _DBAccess.AddParameters("@UpdationDate", data.UpdationDate ?? "");
        //            _DBAccess.AddParameters("@Price", data.Price);
        //            _DataSet = _DBAccess.DBExecute();
        //            _DataSet.Dispose();

        //        }
        //        if (_DataSet != null && _DataSet.Tables.Count > 0)
        //        {
        //            DataTable _DataTable = new();
        //            foreach (DataRow row in _DataSet.Tables)
        //            {
        //                if (row["RETVAL"]?.ToString() == "SUCCESS")
        //                {

        //                    _DBReturnData.Message = "SUCCESS";
        //                    _DBReturnData.Code = 200;
        //                }
        //                else
        //                {
        //                    _DBReturnData.Message = "FAILED";
        //                    _DBReturnData.Code = 401;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            _DBReturnData.Message = "NOT EXISTS";
        //            _DBReturnData.Code = 400;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        _DALBASE.ErrorLog("UpdateProduct", "DLProduct", ex.ToString());
        //        _DBReturnData.Message = "Internal Server Error";
        //        _DBReturnData.Code = 500;
        //    }

        //    return _MLProduct;
        //}


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
