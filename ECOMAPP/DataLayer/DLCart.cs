using System.Data;
using ECOMAPP.CommonRepository;
using ECOMAPP.ModelLayer;
using Google.Api.Gax.ResourceNames;
using static ECOMAPP.ModelLayer.MLCart;

namespace ECOMAPP.DataLayer
{
    public class DLCart
    {
        public List<ProductDtoCart> GetCartData(string userId)
        {
            MLCart _MLCart = new() { ProductsCart = new List<ProductDtoCart>() };
            DBReturnData _DBReturnData = new();
            DataSet _DataSet = new();

            try
            {
                using DBAccess _DBAccess = new DBAccess();
                _DBAccess.DBProcedureName = "SP_CART";
                _DBAccess.AddParameters("@ACTION", "GETCART");
                _DBAccess.AddParameters("@USERID", userId);
                _DataSet = _DBAccess.DBExecute();
                _DBAccess.Dispose();

                if (_DataSet?.Tables.Count > 0)
                {
                    var DataTable = _DataSet.Tables[0];

                    if (_DataSet.Tables.Count > 1 && _DataSet.Tables[1].Rows.Count > 0)
                    {
                        string RetVal = _DataSet.Tables[1].Rows[0]["RETVAL"]?.ToString();

                        if (RetVal == "SUCCESS")
                        {
                            foreach (DataRow row in DataTable.Rows)
                            {
                                ProductDtoCart product = new ProductDtoCart
                                {
                                    ProductId = row["PROD_ID"].ToString(),
                                    Quantity = row["Quantity"] != DBNull.Value ? Convert.ToInt32(row["Quantity"]) : (int?)null,
                                    VarientsId = row["VARIENTS_ID"].ToString(),
                                    ProductImages = row["PRODUCT_IMAGES"] != DBNull.Value
                                        ? row["PRODUCT_IMAGES"].ToString().Split(',').ToList()
                                        : new List<string>(),
                                    PackageDiameter = row["PACKAGE_DIAMETER"].ToString(),
                                    PackageHeight = row["PACKAGE_HEIGHT"].ToString(),
                                    PackageLength = row["PACKAGE_LENGTH"].ToString(),
                                    PackageShape = row["PACKAGE_SHAPE"].ToString(),
                                    PackageTotalVolume = row["PACKAGE_TOTAL_VOLUME"].ToString(),
                                    PackageWeight = row["PACKAGE_WEIGHT"].ToString(),
                                    PackageWidth = row["PACKAGE_WIDTH"].ToString(),
                                    CalculatedPrice = row["CALCULATED_PRICE"].ToString(),
                                    CurrentStockQuantity = row["CURRENT_STOCK_QUANTITY"] != DBNull.Value ? Convert.ToInt32(row["CURRENT_STOCK_QUANTITY"]) : (int?)null,
                                    DiscountAmount = row["DISCOUNT_AMOUNT"].ToString(),
                                    DiscountType = row["DISCOUNT_TYPE"].ToString(),
                                    MaximumRetailPrice = row["MAXIMUM_RETAIL_PRICE"] != DBNull.Value ? Convert.ToInt32(row["MAXIMUM_RETAIL_PRICE"]) : (int?)null,
                                    MinimumOrderQuantity = row["MINIMUM_ORDER_QUANTITY"] != DBNull.Value ? Convert.ToInt32(row["MINIMUM_ORDER_QUANTITY"]) : (int?)null,
                                    Pricing = row["PRICING"] != DBNull.Value ? Convert.ToInt32(row["PRICING"]) : (int?)null,
                                    SellingPrice = row["SELLING_PRICE"] != DBNull.Value ? Convert.ToInt32(row["SELLING_PRICE"]) : (int?)null,
                                    TaxAmount = row["TAX_AMOUNT"].ToString(),
                                    TaxCalculation = row["TAX_CALCULATION"].ToString(),
                                    Brand = row["BRAND"].ToString(),
                                    CategoryId = row["CATEGORY_ID"] != DBNull.Value ? Convert.ToInt32(row["CATEGORY_ID"]) : (int?)null,
                                    ProductDescription = row["PRODUCT_DESCRIPTION"].ToString(),
                                    ProductName = row["PRODUCT_NAME"].ToString(),
                                    SubCategoryId = row["SUB_CATEGORY_ID"] != DBNull.Value ? Convert.ToInt32(row["SUB_CATEGORY_ID"]) : (int?)null,
                                    SubSubCategoryId = row["SUB_SUB_CATEGORY_ID"] != DBNull.Value ? Convert.ToInt32(row["SUB_SUB_CATEGORY_ID"]) : (int?)null,
                                    TagsInput = row["TAGS_INPUT"].ToString(),
                                    Unit = row["UNIT"].ToString(),
                                    Hsn = row["HSN"].ToString(),
                                    Sku = row["SKU"].ToString(),
                                    VarientsName = row["VAREINTS_NAME"].ToString(),
                                };

                                _MLCart.ProductsCart.Add(product);
                            }
                        }
                        else
                        {
                            _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                            _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                            _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new DALBASE().ErrorLog("GetCartData", "DLCart", ex.ToString());
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
            }

            return _MLCart.ProductsCart;
        }



        public DBReturnData InsertCartData(MLInsertCart _MLInsertCart, string? UserID)
        {
            DBReturnData _DBReturnData = new();
            DataSet _DataSet = new();
            DALBASE _DALBASE = new DALBASE();


            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "[SP_CART]";
                    _DBAccess.AddParameters("@Action", "INSERTCART");
                    _DBAccess.AddParameters("@PROD_ID", _MLInsertCart.PROD_ID);
                    _DBAccess.AddParameters("@VARIENTS_ID", _MLInsertCart.VARIENTS_ID);
                    _DBAccess.AddParameters("@Quantity", _MLInsertCart.Quantity);
                    _DBAccess.AddParameters("@USERID", UserID ?? "");
                    _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();
                }
                DataTable _DataTable = _DataSet.Tables[0];
                string RetVal = _DataTable.Rows[0]["RETVAL"]?.ToString();

                if (RetVal == "SUCCESS")
                {
                    _DBReturnData.Status = DBEnums.Status.SUCCESS;
                    _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;



                }
                else if (RetVal == "Alerady In Cart")
                {
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Code = DBEnums.Codes.CONFLICT;
                }
                else
                {
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;

                }




            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("InsertCartData", "DLCart", ex.Message);
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();

            }



            return _DBReturnData;
        }



        public DBReturnData UpdateCartData(MLInsertCart _MLInsertCart, string? UserID)
        {
            DBReturnData _DBReturnData = new();
            DataSet _DataSet = new();
            DALBASE _DALBASE = new DALBASE();


            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "[SP_CART]";
                    _DBAccess.AddParameters("@Action", "UPDATECART");
                    _DBAccess.AddParameters("@PROD_ID", _MLInsertCart.PROD_ID);
                    _DBAccess.AddParameters("@VARIENTS_ID", _MLInsertCart.VARIENTS_ID);
                    _DBAccess.AddParameters("@Quantity", _MLInsertCart.Quantity);
                    _DBAccess.AddParameters("@USERID", UserID ?? "");
                    _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();
                }
                DataTable _DataTable = _DataSet.Tables[0];
                string RetVal = _DataTable.Rows[0]["RETVAL"]?.ToString();

                if (RetVal == "SUCCESS")
                {
                    _DBReturnData.Status = DBEnums.Status.SUCCESS;
                    _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;



                }
                else if (RetVal == "Item Not Exists")
                {
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Code = DBEnums.Codes.CONFLICT;

                }
                else
                {
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;

                }




            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("UpdateCartData", "DLCart", ex.Message);
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();

            }



            return _DBReturnData;
        }



        public DBReturnData DeleteCartData()
        {
            DBReturnData _DBReturnData = new();
            DataSet _DataSet = new();
            DALBASE _DALBASE = new DALBASE();


            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "";
                    _DBAccess.AddParameters("", "");
                    _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();
                }
                DataTable _DataTable = _DataSet.Tables[0];
                string RetVal = _DataTable.Rows[0]["RETVAL"]?.ToString();

                if (RetVal == "SUCCESS")
                {
                    _DBReturnData.Status = DBEnums.Status.SUCCESS;
                    _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;



                }
                else
                {
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;

                }




            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("DeleteCartData", "DLCart", ex.Message);
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();

            }



            return _DBReturnData;
        }
    }

}
