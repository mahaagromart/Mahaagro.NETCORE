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
    public class DLProduct:DALBASE
    {



        public DBReturnData GetAllProducts()
        {
            List<MlGetProducts> products = new();
            DALBASE _DALBASE = new();
            DBReturnData _DbReturn = new(); // Ensure _DbReturn is properly initialized

            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_PRODUCT";
                    _DBAccess.AddParameters("@Action", "SELECTPRODUCTANDPRICELOGISTICES");

                    using DataSet _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();

                    if (_DataSet == null || _DataSet.Tables.Count == 0)
                    {
                        _DbReturn.Dataset = null;
                        _DbReturn.Code = DBEnums.Codes.NOT_FOUND;
                        _DbReturn.Message = "No products found";
                        _DbReturn.Retval = "NOT_FOUND";
                        return _DbReturn;
                    }

                    DataTable productTable = _DataSet.Tables[0];
                    string retval = _DataSet.Tables[1].Rows[0]["RETVAL"]?.ToString();

                    if (retval == "SUCCESS")
                    {
                        var productGroups = productTable.AsEnumerable()
                            .GroupBy(row => row["PROD_ID"].ToString());

                        foreach (var productGroup in productGroups)
                        {
                            var firstProduct = productGroup.First();
                            string PROD_ID = firstProduct["PROD_ID"]?.ToString() ?? string.Empty;

                            MlGetProducts product = new()
                            {
                                PROD_ID = PROD_ID,
                                Product_Name = firstProduct["PRODUCT_NAME"]?.ToString() ?? string.Empty,
                                Product_Description = firstProduct["PRODUCT_DESCRIPTION"]?.ToString() ?? string.Empty,
                                BRAND = firstProduct["BRAND"]?.ToString() ?? string.Empty,
                                UNIT = firstProduct["UNIT"]?.ToString() ?? string.Empty,
                                CATEGORY_ID = firstProduct["CATEGORY_ID"].ToString() ?? string.Empty,
                                SUB_CATEGORY_ID = firstProduct["SUB_CATEGORY_ID"].ToString() ?? string.Empty,
                                SUB_SUB_CATEGORY_ID = firstProduct["SUB_SUB_CATEGORY_ID"].ToString() ?? string.Empty,
                                Variants = new List<MlProductVariant>()
                            };

                            var variantRows = productGroup.GroupBy(row => row["VARIENTS_ID"].ToString());

                            foreach (var variantGroup in variantRows)
                            {
                                var firstVariant = variantGroup.First();
                                string VARIENTS_ID = firstVariant["VARIENTS_ID"]?.ToString() ?? string.Empty;

                                var variant = new MlProductVariant
                                {
                                    VARIENTS_ID = VARIENTS_ID,
                                    Product_Name = firstVariant["VARIANTWISE_NAME"]?.ToString() ?? string.Empty,
                                    Varient_Name = firstVariant["VAREINTS_NAME"]?.ToString() ?? string.Empty,
                                    SKU = firstVariant["SKU"]?.ToString() ?? string.Empty,
                                    HSN = firstVariant["HSN"]?.ToString() ?? string.Empty,

                                    Pricing = new MlProductPricing
                                    {
                                        PRICING = firstVariant["PRICING"]?.ToString() ?? "0",
                                        MAXIMUM_RETAIL_PRICE = firstVariant["MAXIMUM_RETAIL_PRICE"]?.ToString() ?? "0",
                                        SELLING_PRICE = firstVariant["SELLING_PRICE"]?.ToString() ?? "0",
                                        MINIMUM_ORDER_QUANTITY = firstVariant["MINIMUM_ORDER_QUANTITY"]?.ToString() ?? "0",
                                        CURRENT_STOCK_QUANTITY = firstVariant["CURRENT_STOCK_QUANTITY"]?.ToString() ?? "0",
                                        DISCOUNT_TYPE = firstVariant["DISCOUNT_TYPE"]?.ToString() ?? "0",
                                        DISCOUNT_AMOUNT = firstVariant["DISCOUNT_AMOUNT"]?.ToString() ?? "0",
                                        TAX_AMOUNT = firstVariant["TAX_AMOUNT"]?.ToString() ?? "0",
                                        TAX_CALCULATION = firstVariant["TAX_CALCULATION"]?.ToString() ?? "0",
                                        CALCULATED_PRICE = firstVariant["CALCULATED_PRICE"]?.ToString() ?? "0",
                                    },

                                    Logistics = new MlProductLogistics
                                    {
                                        PACKAGE_SHAPE = firstVariant["PACKAGE_SHAPE"]?.ToString() ?? string.Empty,
                                        PACKAGE_LENGTH = firstVariant["PACKAGE_LENGTH"]?.ToString() ?? "0",
                                        PACKAGE_WIDTH = firstVariant["PACKAGE_WIDTH"]?.ToString() ?? "0",
                                        PACKAGE_HEIGHT = firstVariant["PACKAGE_HEIGHT"]?.ToString() ?? "0",
                                        PACKAGE_WEIGHT = firstVariant["PACKAGE_WEIGHT"]?.ToString() ?? "0",
                                        PACKAGE_DIAMETER = firstVariant["PACKAGE_DIAMETER"]?.ToString() ?? "0",
                                        PACKAGE_TOTAL_VOLUME = firstVariant["PACKAGE_TOTAL_VOLUME"]?.ToString() ?? "0",
                                    },

                                    ImageGallery = variantGroup
                                        .Select(row => new MLImages
                                        {
                                            Product_Images = row["PRODUCT_IMAGES"]?.ToString() ?? string.Empty
                                        })
                                        .Distinct()
                                        .ToList()
                                };

                                product.Variants.Add(variant);
                            }

                            product.ThumbnailImage = product.Variants.FirstOrDefault()?.ImageGallery?.FirstOrDefault()?.Product_Images ?? string.Empty;
                            products.Add(product);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("GetAllProducts", "DLProduct", ex.ToString());
                _DbReturn.Dataset = null;
                _DbReturn.Code = DBEnums.Codes.BAD_REQUEST;
                _DbReturn.Message = "Error fetching products";
                _DbReturn.Retval = "FAILURE";
                return _DbReturn;
            }

            _DbReturn.Dataset = products;
            _DbReturn.Code = DBEnums.Codes.SUCCESS;
            _DbReturn.Message = "Products fetched successfully";
            _DbReturn.Retval = "SUCCESS";
            return _DbReturn;
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
                    _DBAccess.AddParameters("@SUB_SUB_CATEGORY_ID", data.SUB_SUB_CATEGORY_ID);
                    _DBAccess.AddParameters("@Images", data.ThumbnailImage ?? "");
                    _DBAccess.AddParameters("@BRAND", data.BRAND ?? "");
                    _DBAccess.AddParameters("@sku", data.sku ?? "");
                    _DBAccess.AddParameters("@UNIT", data.UNIT ?? "");
                    _DBAccess.AddParameters("@HSN", data.HSN ?? "");
                    _DBAccess.AddParameters("@TAGS_INPUT", tagsString);
                    _DBAccess.AddParameters("@PROD_ID",data.PROD_ID);
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
                    _DBAccess.AddParameters("@CALCULATED_MINIMUM_ORDER_PRICE", data.CALCULATED_MINIMUM_ORDER_PRICE);


                    //logistices starts
                    _DBAccess.AddParameters("@PACKAGE_WEIGHT", data.PACKAGE_WEIGHT);
                    _DBAccess.AddParameters("@PACKAGE_SHAPE", data.PACKAGE_SHAPE);
                    _DBAccess.AddParameters("@PACKAGE_LENGTH", data.PACKAGE_LENGTH);
                    _DBAccess.AddParameters("@PACKAGE_WIDTH", data.PACKAGE_WIDTH);
                    _DBAccess.AddParameters("@PACKAGE_HEIGHT", data.PACKAGE_HEIGHT);
                    _DBAccess.AddParameters("@PACKAGE_DIAMETER", data.PACKAGE_DIAMETER);
                    _DBAccess.AddParameters("@PACKAGE_TOTAL_VOLUME", data.PACKAGE_TOTAL_VOLUME);


                    //VAREITIES
                    _DBAccess.AddParameters("@VAREINTS_NAME", data.VAREINTS_NAME);




                    _DataSet = _DBAccess.DBExecute();
                    _DataSet.Dispose();


                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = _DataSet.Tables[0];
                    if (_DataTable.Rows.Count > 0)
                    {
                        string retval = _DataTable.Rows[0]["RETVAL"].ToString();
                        string prodId = _DataTable.Rows[0]["PROD_ID"].ToString();

                        if (retval == "SUCCESS")
                        {
                            _DBReturnData.Message = prodId; 
                            _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                        }
                        else
                        {
                            _DBReturnData.Message = "Failed to insert product";
                            _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                        }
                    }
                }
                else
                {
                    _DBReturnData.Message = "Failed to insert product.";
                    _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                }


            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("InsertProduct", "DLProduct", ex.ToString());
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;

            }


            return _DBReturnData;
        }


    



        public DBReturnData DeleteProduct(MLDeleteProduct data)
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
                    _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                }

            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("DeleteProduct", "DLProduct", ex.ToString());
                _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
            }

            return _DBReturnData;
        }



        public DBReturnData ProductToggleCertified(MLToggleCertified data)
        {
  
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
                _DALBASE.ErrorLog("ProductToggleCertified", "DLProduct", ex.ToString());
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;

            }

            return _DBReturnData;
        }

        public DBReturnData ProductToggleStatus(MLToggleStatus data)
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
                _DALBASE.ErrorLog("ProductToggleStatus", "DLProduct", ex.ToString());
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;

            }

            return _DBReturnData;
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
                _DALBASE.ErrorLog("GetAllInhouseProducts", "DLProduct", ex.ToString());
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
            }

            return _MLProduct.InhouseProductList;
        }

     
        public DBReturnData UpdateInhouseProduct(MLUpdateInhouseProduct data)
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
                    _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                }

            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("UpdateInhouseProduct", "DLProduct", ex.ToString());
                _DBReturnData.Message =DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;

            }

            return _DBReturnData;
        }

        public DBReturnData DeleteInhouseProduct(MLDeleteInhouseProduct data)
        {
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
                            _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                        }
                        else
                        {
                            _DBReturnData.Message =DBEnums.Status.FAILURE.ToString();
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
                _DALBASE.ErrorLog("DeleteInhouseProduct", "DLProduct", ex.ToString());
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;

            }

            return _DBReturnData;
        }





        #endregion InhouseProduct


        public DBReturnData GetProductBycategory(MLGetProrductByCategoryId mLGetProrductByCategoryId)
        {
            List<MlGetProducts> products = new();
            DALBASE _DALBASE = new();
            DBReturnData _DbReturn = new(); // Ensure _DbReturn is properly initialized

            try
            {

        

                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_PRODUCTACTIONS";
                    _DBAccess.AddParameters("@Action", "SELECTPRODUCTSBYCATEGORYID");
                    _DBAccess.AddParameters("@CategoryId", mLGetProrductByCategoryId.Id);
                    _DBAccess.AddParameters("@PaginationStart", mLGetProrductByCategoryId.PaginatioStart);

                    using DataSet _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();

                    if (_DataSet == null || _DataSet.Tables.Count == 0)
                    {
                        _DbReturn.Dataset = null;
                        _DbReturn.Code = DBEnums.Codes.NOT_FOUND;
                        _DbReturn.Message = "No product details found";
                        _DbReturn.Retval = "NOT_FOUND";
                        return _DbReturn;
                    }

                    DataTable productTable = _DataSet.Tables[0];
                    string retval = _DataSet.Tables[^1].Rows[0]["RETVAL"]?.ToString(); 

                    if (retval == "SUCCESS")
                    {
                        var productGroups = productTable.AsEnumerable()
                            .GroupBy(row => row["PROD_ID"].ToString());

                        foreach (var productGroup in productGroups)
                        {
                            var firstProduct = productGroup.First();
                            string PROD_ID = firstProduct["PROD_ID"]?.ToString() ?? string.Empty;

                            MlGetProducts product = new()
                            {
                                PROD_ID = PROD_ID,
                                Product_Name = firstProduct["DEFAULTPRODUCTNAME"]?.ToString() ?? string.Empty,
                                Product_Description = firstProduct["PRODUCT_DESCRIPTION"]?.ToString() ?? string.Empty,
                                BRAND = firstProduct["BRAND"]?.ToString() ?? string.Empty,
                                UNIT = firstProduct["UNIT"]?.ToString() ?? string.Empty,
                                CATEGORY_ID = firstProduct["CATEGORY_ID"]?.ToString() ?? string.Empty,
                                SUB_CATEGORY_ID = firstProduct["SUB_CATEGORY_ID"]?.ToString() ?? string.Empty,
                                SUB_SUB_CATEGORY_ID = firstProduct["SUB_SUB_CATEGORY_ID"]?.ToString() ?? string.Empty,
                                Variants = new List<MlProductVariant>()
                            };

                            var variantGroups = productGroup.GroupBy(row => row["VARIENTS_ID"].ToString());

                            foreach (var variantGroup in variantGroups)
                            {
                                var firstVariant = variantGroup.First();
                                string VARIENTS_ID = firstVariant["VARIENTS_ID"]?.ToString() ?? string.Empty;

                                var variant = new MlProductVariant
                                {
                                    VARIENTS_ID = VARIENTS_ID,
                                    Product_Name = firstVariant["VARIANTWISE_NAME"]?.ToString() ?? string.Empty,
                                    Varient_Name = firstVariant["VARIENTNAME"]?.ToString() ?? string.Empty,
                                    SKU = firstVariant["SKU"]?.ToString() ?? string.Empty,
                                    HSN = firstVariant["HSN"]?.ToString() ?? string.Empty,

                                    Pricing = new MlProductPricing
                                    {
                                        PRICING = firstVariant["PRICING"]?.ToString() ?? "0",
                                        MAXIMUM_RETAIL_PRICE = firstVariant["MAXIMUM_RETAIL_PRICE"]?.ToString() ?? "0",
                                        SELLING_PRICE = firstVariant["SELLING_PRICE"]?.ToString() ?? "0",
                                        MINIMUM_ORDER_QUANTITY = firstVariant["MINIMUM_ORDER_QUANTITY"]?.ToString() ?? "0",
                                        CURRENT_STOCK_QUANTITY = firstVariant["CURRENT_STOCK_QUANTITY"]?.ToString() ?? "0",
                                        DISCOUNT_TYPE = firstVariant["DISCOUNT_TYPE"]?.ToString() ?? "0",
                                        DISCOUNT_AMOUNT = firstVariant["DISCOUNT_AMOUNT"]?.ToString() ?? "0",
                                        TAX_AMOUNT = firstVariant["TAX_AMOUNT"]?.ToString() ?? "0",
                                        TAX_CALCULATION = firstVariant["TAX_CALCULATION"]?.ToString() ?? "0",
                                        CALCULATED_PRICE = firstVariant["CALCULATED_PRICE"]?.ToString() ?? "0",
                                    },

                                    Logistics = new MlProductLogistics
                                    {
                                        PACKAGE_SHAPE = firstVariant["PACKAGE_SHAPE"]?.ToString() ?? string.Empty,
                                        PACKAGE_LENGTH = firstVariant["PACKAGE_LENGTH"]?.ToString() ?? "0",
                                        PACKAGE_WIDTH = firstVariant["PACKAGE_WIDTH"]?.ToString() ?? "0",
                                        PACKAGE_HEIGHT = firstVariant["PACKAGE_HEIGHT"]?.ToString() ?? "0",
                                        PACKAGE_WEIGHT = firstVariant["PACKAGE_WEIGHT"]?.ToString() ?? "0",
                                        PACKAGE_DIAMETER = firstVariant["PACKAGE_DIAMETER"]?.ToString() ?? "0",
                                        PACKAGE_TOTAL_VOLUME = firstVariant["PACKAGE_TOTAL_VOLUME"]?.ToString() ?? "0",
                                    },

                                    // Collect images for the variant
                                    ImageGallery = variantGroup
                                        .Select(row => new MLImages
                                        {
                                            Product_Images = row["PRODUCT_IMAGES"]?.ToString() ?? string.Empty
                                        })
                                        .Where(img => !string.IsNullOrEmpty(img.Product_Images)) // Remove empty images
                                        .Distinct()
                                        .ToList()
                                };

                                product.Variants.Add(variant);
                            }

                            // Set product thumbnail from the first variant’s first image
                            product.ThumbnailImage = product.Variants.FirstOrDefault()?.ImageGallery?.FirstOrDefault()?.Product_Images ?? string.Empty;
                            products.Add(product);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("GetCompletProductDescription", "DLProduct", ex.ToString());
                _DbReturn.Dataset = null;
                _DbReturn.Code = DBEnums.Codes.BAD_REQUEST;
                _DbReturn.Message = "Error fetching product details";
                _DbReturn.Retval = "FAILURE";
                return _DbReturn;
            }

            _DbReturn.Dataset = products;
            _DbReturn.Code = DBEnums.Codes.SUCCESS;
            _DbReturn.Message = "Product details fetched successfully";
            _DbReturn.Retval = "SUCCESS";
            return _DbReturn;
        }


        //public List<ProductsList> GetProductBycategory(MLGetProrductByCategoryId mLGetProrductByCategoryId)
        //{
        //    List<ProductsList> productsLists = new List<ProductsList>();
        //    DataSet dataSet = new();

        //    try
        //    {
        //        using (DBAccess dBAccess = new DBAccess())
        //        {
        //            dBAccess.DBProcedureName = "SP_PRODUCTACTIONS";
        //            dBAccess.AddParameters("@Action", "SELECTPRODUCTSBYCATEGORYID");
        //            dBAccess.AddParameters("@CategoryId", mLGetProrductByCategoryId.Id);
        //            dBAccess.AddParameters("@PaginationStart",mLGetProrductByCategoryId.PaginatioStart);
        //            dBAccess.AddParameters("@PaginationEnd", mLGetProrductByCategoryId.PaginationEnd);
        //            dataSet = dBAccess.DBExecute();
        //            dBAccess.Dispose();
        //        }

        //        if (dataSet.Tables[1].Rows[0]["RETVAL"].ToString() == "SUCCESS")
        //        {
        //            var productsDict = new Dictionary<string, ProductsList>();

        //            foreach (DataRow row in dataSet.Tables[0].Rows)
        //            {
        //                string productId = row["PRODUCT_ID"].ToString();
        //                string productName = row["DEFAULTPRODUCTNAME"].ToString();
        //                string varientName = row["VARIENTNAME"].ToString();
        //                string price = row["MRP"].ToString();
        //                string imageUrl = row["IMAGEURL"].ToString();

        //                if (!productsDict.ContainsKey(productId))
        //                {
        //                    productsDict[productId] = new ProductsList
        //                    {
        //                        ProductId = productId,
        //                        VarientList = new List<VarientList>()
        //                    };
        //                }

        //                var product = productsDict[productId];
        //                var varient = new VarientList
        //                {
        //                    ProductName = productName??"",
        //                    Price = price??"",
        //                    Reviews = null, // Assuming reviews are not provided in the current dataset
        //                    Images = new string[] { imageUrl??"" }
        //                };

        //                product.VarientList.Add(varient);
        //            }

        //            productsLists = productsDict.Values.ToList();
        //        }
        //    }

        //    catch (Exception c)
        //    {
        //        ErrorLog("Get product", "Dlproducts", c.ToString());
        //    }


        //    return productsLists;
        //}





        //public List<ProductDescriptionList> GetCompletProductDescription(MLGetCompletProductDescription mLGetCompletProductDescription)
        //{
        //    List<ProductDescriptionList> productDescriptionLists = new List<ProductDescriptionList>();
        //    DataSet dataSet = new();

        //    try
        //    {
        //        using (DBAccess dBAccess = new DBAccess())
        //        {
        //            dBAccess.DBProcedureName = "SP_PRODUCTACTIONS";
        //            dBAccess.AddParameters("@Action", "GetCompletProductDescription");
        //            dBAccess.AddParameters("@ProductId", mLGetCompletProductDescription.ProductId);
        //            dataSet = dBAccess.DBExecute();
        //            dBAccess.Dispose();
        //        }

        //        if (dataSet.Tables[1].Rows[0]["RETVAL"].ToString() == "Success")
        //        {
        //            var productsDict = new Dictionary<string, ProductDescriptionList>();

        //            foreach (DataRow row in dataSet.Tables[0].Rows)
        //            {
        //                string ProductName = row["PRODUCT_NAME"].ToString();
        //                string ProductDescription = row["PRODUCT_DESCRIPTION"].ToString();
        //                string Brand = row["BRAND"].ToString();
        //                string Unit = row["UNIT"].ToString();
        //                string Price = row["MRP"].ToString();
        //                string ProductId = row["PRODUCT_ID"].ToString();
        //                string Reviews = "";
        //                string Images = row["IMAGEURL"].ToString();
        //                string VaientWiseName = row["VAIENTWISENAME"].ToString();
        //                string DefaultProductName = row["DefaultProductName"].ToString();
        //                string VarientName = row["VARIENTNAME"].ToString();
        //                string VarientId = row["VARIENTID"].ToString();
        //                string CalculatedPrice = row["CALCULATED_PRICE"].ToString();
        //                string CurrentStockQuantity = row["CURRENT_STOCK_QUANTITY"].ToString();
        //                string DiscountAmount = row["DISCOUNT_AMOUNT"].ToString();
        //                string Pricing = row["PRICING"].ToString();
        //                string SellingPrice = row["SELLING_PRICE"].ToString();
        //                string Mrp = row["MRP"].ToString();
        //                string ImageUrl = row["IMAGEURL"].ToString();
        //                string packageShape = row["PACKAGE_SHAPE"].ToString();
        //                string packageLength = row["PACKAGE_LENGTH"].ToString();
        //                string packageWidth = row["PACKAGE_WIDTH"].ToString();
        //                string packageHeight = row["PACKAGE_HEIGHT"].ToString();
        //                string packageWeight = row["PACKAGE_WEIGHT"].ToString();
        //                string packageDiameter = row["PACKAGE_DIAMETER"].ToString();
        //                string packageTotalVolume = row["PACKAGE_TOTAL_VOLUME"].ToString();




        //                if (!productsDict.ContainsKey(ProductId))
        //                {
        //                    productsDict[ProductId] = new ProductDescriptionList
        //                    {
        //                        ProductId = ProductId,
        //                        VarientList = new List<CompleteVarientList>()
        //                    };
        //                }

        //                var product = productsDict[ProductId];
        //                var varient = new CompleteVarientList
        //                {
        //                    //ProductName = productName ?? "",
        //                    //Price = price ?? "",
        //                    //Reviews = null, // Assuming reviews are not provided in the current dataset
        //                    //Images = new string[] { imageUrl ?? "" }

        //                    ProductName = ProductName ?? "",
        //                    Price = Price ?? "",
        //                    Reviews = Reviews ?? "",
        //                    Images = new string[] { ImageUrl ?? "" },
        //                    ProductId = ProductId ?? "",
        //                    ProductDescription = ProductDescription ?? "",
        //                    Unit = Unit ?? "",
        //                    Brand = Brand ?? "",
        //                    VaientWiseName = VaientWiseName ?? "",
        //                    DefaultProductName = DefaultProductName ?? "",
        //                    VarientName = VarientName ?? "",
        //                    VarientId = VarientId ?? "",
        //                    CalculatedPrice = CalculatedPrice ?? "",
        //                    CurrentStockQuantity = CurrentStockQuantity ?? "",
        //                    DiscountAmount = DiscountAmount ?? "",
        //                    Pricing = Pricing ?? "",
        //                    SellingPrice = SellingPrice ?? "",
        //                    Mrp = Mrp ?? "",
        //                    ImageUrl = ImageUrl ?? "",
        //                    PackageShape = packageShape ?? "",
        //                    packageLength = packageLength ?? "",
        //                    packageWidth =  packageWidth ?? "",
        //                    packageHeight = packageWeight ?? "",
        //                    packageWeight = packageWeight ?? "",
        //                    packageDiameter = packageDiameter ?? "",
        //                    packageTotalVolume = packageTotalVolume ?? ""

        //                };

        //                product.VarientList.Add(varient);
        //            }

        //            productDescriptionLists = productsDict.Values.ToList();
        //        }
        //    }

        //    catch (Exception c)
        //    {
        //        ErrorLog("Get product", "Dlproducts", c.ToString());
        //    }


        //    return productDescriptionLists;
        //}

        public DBReturnData GetCompletProductDescription(MLGetCompletProductDescription mLGetCompletProductDescription)
        {
            List<MlGetProducts> products = new();
            DALBASE _DALBASE = new();
            DBReturnData _DbReturn = new(); // Ensure _DbReturn is properly initialized

            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_PRODUCTACTIONS";
                    _DBAccess.AddParameters("@Action", "GetCompletProductDescription");
                    _DBAccess.AddParameters("@ProductId", mLGetCompletProductDescription.ProductId);

                    using DataSet _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();

                    if (_DataSet == null || _DataSet.Tables.Count == 0)
                    {
                        _DbReturn.Dataset = null;
                        _DbReturn.Code = DBEnums.Codes.NOT_FOUND;
                        _DbReturn.Message = "No product details found";
                        _DbReturn.Retval = "NOT_FOUND";
                        return _DbReturn;
                    }

                    DataTable productTable = _DataSet.Tables[0];
                    string retval = _DataSet.Tables[^1].Rows[0]["RETVAL"]?.ToString(); // Using the last table for RETVAL

                    if (retval == "SUCCESS")
                    {
                        var productGroups = productTable.AsEnumerable()
                            .GroupBy(row => row["PROD_ID"].ToString());

                        foreach (var productGroup in productGroups)
                        {
                            var firstProduct = productGroup.First();
                            string PROD_ID = firstProduct["PROD_ID"]?.ToString() ?? string.Empty;

                            MlGetProducts product = new()
                            {
                                PROD_ID = PROD_ID,
                                Product_Name = firstProduct["PRODUCT_NAME"]?.ToString() ?? string.Empty,
                                Product_Description = firstProduct["PRODUCT_DESCRIPTION"]?.ToString() ?? string.Empty,
                                BRAND = firstProduct["BRAND"]?.ToString() ?? string.Empty,
                                UNIT = firstProduct["UNIT"]?.ToString() ?? string.Empty,
                                CATEGORY_ID = firstProduct["CATEGORY_ID"]?.ToString() ?? string.Empty,
                                SUB_CATEGORY_ID = firstProduct["SUB_CATEGORY_ID"]?.ToString() ?? string.Empty,
                                SUB_SUB_CATEGORY_ID = firstProduct["SUB_SUB_CATEGORY_ID"]?.ToString() ?? string.Empty,
                                Variants = new List<MlProductVariant>()
                            };

                            var variantGroups = productGroup.GroupBy(row => row["VARIENTS_ID"].ToString());

                            foreach (var variantGroup in variantGroups)
                            {
                                var firstVariant = variantGroup.First();
                                string VARIENTS_ID = firstVariant["VARIENTS_ID"]?.ToString() ?? string.Empty;

                                var variant = new MlProductVariant
                                {
                                    VARIENTS_ID = VARIENTS_ID,
                                    Product_Name = firstVariant["VARIANTWISE_NAME"]?.ToString() ?? string.Empty,
                                    Varient_Name = firstVariant["VAREINTS_NAME"]?.ToString() ?? string.Empty,
                                    SKU = firstVariant["SKU"]?.ToString() ?? string.Empty,
                                    HSN = firstVariant["HSN"]?.ToString() ?? string.Empty,

                                    Pricing = new MlProductPricing
                                    {
                                        PRICING = firstVariant["PRICING"]?.ToString() ?? "0",
                                        MAXIMUM_RETAIL_PRICE = firstVariant["MAXIMUM_RETAIL_PRICE"]?.ToString() ?? "0",
                                        SELLING_PRICE = firstVariant["SELLING_PRICE"]?.ToString() ?? "0",
                                        MINIMUM_ORDER_QUANTITY = firstVariant["MINIMUM_ORDER_QUANTITY"]?.ToString() ?? "0",
                                        CURRENT_STOCK_QUANTITY = firstVariant["CURRENT_STOCK_QUANTITY"]?.ToString() ?? "0",
                                        DISCOUNT_TYPE = firstVariant["DISCOUNT_TYPE"]?.ToString() ?? "0",
                                        DISCOUNT_AMOUNT = firstVariant["DISCOUNT_AMOUNT"]?.ToString() ?? "0",
                                        TAX_AMOUNT = firstVariant["TAX_AMOUNT"]?.ToString() ?? "0",
                                        TAX_CALCULATION = firstVariant["TAX_CALCULATION"]?.ToString() ?? "0",
                                        CALCULATED_PRICE = firstVariant["CALCULATED_PRICE"]?.ToString() ?? "0",
                                    },

                                    Logistics = new MlProductLogistics
                                    {
                                        PACKAGE_SHAPE = firstVariant["PACKAGE_SHAPE"]?.ToString() ?? string.Empty,
                                        PACKAGE_LENGTH = firstVariant["PACKAGE_LENGTH"]?.ToString() ?? "0",
                                        PACKAGE_WIDTH = firstVariant["PACKAGE_WIDTH"]?.ToString() ?? "0",
                                        PACKAGE_HEIGHT = firstVariant["PACKAGE_HEIGHT"]?.ToString() ?? "0",
                                        PACKAGE_WEIGHT = firstVariant["PACKAGE_WEIGHT"]?.ToString() ?? "0",
                                        PACKAGE_DIAMETER = firstVariant["PACKAGE_DIAMETER"]?.ToString() ?? "0",
                                        PACKAGE_TOTAL_VOLUME = firstVariant["PACKAGE_TOTAL_VOLUME"]?.ToString() ?? "0",
                                    },

                                    // Collect images for the variant
                                    ImageGallery = variantGroup
                                        .Select(row => new MLImages
                                        {
                                            Product_Images = row["PRODUCT_IMAGES"]?.ToString() ?? string.Empty
                                        })
                                        .Where(img => !string.IsNullOrEmpty(img.Product_Images)) // Remove empty images
                                        .Distinct()
                                        .ToList()
                                };

                                product.Variants.Add(variant);
                            }

                            // Set product thumbnail from the first variant’s first image
                            product.ThumbnailImage = product.Variants.FirstOrDefault()?.ImageGallery?.FirstOrDefault()?.Product_Images ?? string.Empty;
                            products.Add(product);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("GetCompletProductDescription", "DLProduct", ex.ToString());
                _DbReturn.Dataset = null;
                _DbReturn.Code = DBEnums.Codes.BAD_REQUEST;
                _DbReturn.Message = "Error fetching product details";
                _DbReturn.Retval = "FAILURE";
                return _DbReturn;
            }

            _DbReturn.Dataset = products;
            _DbReturn.Code = DBEnums.Codes.SUCCESS;
            _DbReturn.Message = "Product details fetched successfully";
            _DbReturn.Retval = "SUCCESS";
            return _DbReturn;
        }


    }
}
