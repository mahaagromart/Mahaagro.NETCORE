using System.Data;
using ECOMAPP.CommonRepository;
using ECOMAPP.ModelLayer;
using Microsoft.Extensions.Configuration;
using Razorpay.Api;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace ECOMAPP.DataLayer
{
    public class DLOrder
    {
        private readonly IConfiguration _configuration;
        private RazorpayClient _razorpayClient;

        public DLOrder(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DBReturnData CreateOrder(MLOrder _MLOrder)
        {
            DataSet _DataSet = new();
            DBReturnData _DBReturnData = new();
            List<dynamic> orders = new();

            try
            {
                // Fetch Product Data
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_PRODUCT";
                    _DBAccess.AddParameters("@Action", "CHECKPRODUCTBYVARIENTID");
                    _DBAccess.AddParameters("@VID", _MLOrder.VarientID);
                    _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();
                }

                if (_DataSet.Tables.Count < 2)
                {
                    _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    _DBReturnData.Message = "Invalid dataset returned from DB";
                    _DBReturnData.Retval = "FAILURE";
                    return _DBReturnData;
                }

                DataTable _DataTable = _DataSet.Tables[0];
                string Retval = _DataSet.Tables[1].Rows[0]["Retval"]?.ToString();

                if (Retval == "SUCCESS" && _DataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in _DataTable.Rows)
                    {
                        var currentOrder = new
                        {
                            ProductName = row["PRODUCT_NAME"]?.ToString(),
                            VarientName = row["VAREINTS_NAME"]?.ToString(),
                            PackageLength = row["PACKAGE_LENGTH"] == DBNull.Value ? null : row["PACKAGE_LENGTH"]?.ToString(),
                            PackageWidth = row["PACKAGE_WIDTH"] == DBNull.Value ? null : row["PACKAGE_WIDTH"]?.ToString(),
                            PackageHeight = row["PACKAGE_HEIGHT"] == DBNull.Value ? null : row["PACKAGE_HEIGHT"]?.ToString(),
                            PackageDiameter = row["PACKAGE_DIAMETER"] == DBNull.Value ? null : row["PACKAGE_DIAMETER"]?.ToString(),
                            PackageWeight = row["PACKAGE_WEIGHT"] == DBNull.Value ? null : row["PACKAGE_WEIGHT"]?.ToString(),
                            Pricing = row["PRICING"] == DBNull.Value ? null : row["PRICING"]?.ToString(),
                            SellingPrice = row["SELLING_PRICE"] == DBNull.Value ? null : row["SELLING_PRICE"]?.ToString(),
                            CurrentStockQuantity = row["CURRENT_STOCK_QUANTITY"] == DBNull.Value ? null : row["CURRENT_STOCK_QUANTITY"]?.ToString(),
                            MinimumOrderQty = row["MINIMUM_ORDER_QUANTITY"] == DBNull.Value ? null : row["MINIMUM_ORDER_QUANTITY"]?.ToString(),
                            DiscountType = row["DISCOUNT_TYPE"]?.ToString(),
                            DiscountAmount = row["DISCOUNT_AMOUNT"] == DBNull.Value ? null : row["DISCOUNT_AMOUNT"]?.ToString(),
                            TaxCalculation = row["TAX_CALCULATION"]?.ToString(),
                            Tax_Amount = row["TAX_AMOUNT"] == DBNull.Value ? null : row["TAX_AMOUNT"]?.ToString(),
                            CalculatedPrice = row["CALCULATED_PRICE"] == DBNull.Value ? null : row["CALCULATED_PRICE"]?.ToString(),
                            ProdID = row["PROD_ID"]?.ToString(),
                            VarientID = row["VARIENTS_ID"]?.ToString(),
                        };
                        orders.Add(currentOrder);
                    }

                    // Initialize Razorpay Client
                    var keyId = _configuration["Razorpay:KeyId"];
                    var keySecret = _configuration["Razorpay:KeySecret"];
                    _razorpayClient = new RazorpayClient(keyId, keySecret);

                    var firstOrder = orders.FirstOrDefault();
                    int price = Convert.ToInt32(firstOrder?.CalculatedPrice ?? "0");

                    if (firstOrder != null && price > 0)
                    {
                        var options = new Dictionary<string, object>
                                {
                                { "amount", price * 100 }, // Razorpay requires amount in paise
                                { "currency", "INR" },
                                { "receipt", Guid.NewGuid().ToString() },
                                { "payment_capture", "1" }
                      };

                        // Create Razorpay Order
                        // Create Razorpay Order
                        Order razorOrder = _razorpayClient.Order.Create(options);
                        var razorpayOrderId = razorOrder["id"].ToString();
                        // Convert to JSON string
                        string razorOrderJson = JsonConvert.SerializeObject(razorOrder.Attributes, Formatting.Indented);
                        if (razorpayOrderId != null) {
                 

                                using (DBAccess _DBAccess = new())
                                {
                                    _DBAccess.DBProcedureName = "SP_ORDER";
                                    _DBAccess.AddParameters("@Action", "INSERTORDER");
                                    _DBAccess.AddParameters("@USER_ID", _MLOrder.CustomerID);
                                    _DBAccess.AddParameters("@VARIENT_ID", _MLOrder.VarientID);
                                    _DBAccess.AddParameters("@ORDER_ID", razorpayOrderId);
                                    _DBAccess.AddParameters("@AMOUNT", price);
                                    _DBAccess.AddParameters("@CURRENCY", "INR");

                                    _DataSet = _DBAccess.DBExecute();
                                    _DBAccess.Dispose();

                                }
                                }
                           



                            // Deserialize to RazorpayOrder model (optional)
                            RazorpayOrder orderDetails = JsonConvert.DeserializeObject<RazorpayOrder>(razorOrderJson);

                        // Prepare Success Response
                        _DBReturnData.Dataset = orderDetails;
                        _DBReturnData.Status = DBEnums.Status.SUCCESS;
                        _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                        _DBReturnData.Message = "Order Created Successfully";
                        _DBReturnData.OrderId = razorpayOrderId;
                        _DBReturnData.Retval = "SUCCESS";
                    }
                    else
                    {
                        _DBReturnData.Dataset = null;
                        _DBReturnData.Status = DBEnums.Status.FAILURE;
                        _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                        _DBReturnData.Message = "Invalid order data or zero price";
                        _DBReturnData.Retval = "FAILURE";
                    }
                }
                else
                {
                    _DBReturnData.Dataset = null;
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;
                    _DBReturnData.Message = "Order creation failed - No products found";
                    _DBReturnData.Retval = "FAILURE";
                }
            }
            catch (Exception ex)
            {
                // Log Exception
                DALBASE _DALBASE = new();
                _DALBASE.ErrorLog("CreateOrderController", "DLOrder", ex.ToString());

                // Error Response
                _DBReturnData.Dataset = null;
                _DBReturnData.Status = DBEnums.Status.FAILURE;
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                _DBReturnData.Message = "An internal error occurred";
                _DBReturnData.Retval = "FAILURE";
            }

            return _DBReturnData;
        }

        public DBReturnData VerifyOrder(string OrderId, string PaymnetId)
        {
            DataSet _DataSet = new();
            DBReturnData _DBReturnData = new();

            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_ORDER";
                    _DBAccess.AddParameters("@ACTION", "PAYMENTVERIFIED");
                    _DBAccess.AddParameters("@ORDER_ID", OrderId);
                    _DBAccess.AddParameters("@PAYMENT_ID", PaymnetId);
                    _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();
                }
                string Retval = _DataSet.Tables[0].Rows[0]["Retval"]?.ToString();

                if (Retval == "SUCCESS")
                {


                    _DBReturnData.Dataset = null;
                    _DBReturnData.Status = DBEnums.Status.SUCCESS;
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                    _DBReturnData.Message = "Order Verified Successfully";
                    _DBReturnData.Retval = "SUCCESS";
                }
                else
                {
                    _DBReturnData.Dataset = null;
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                    _DBReturnData.Message = "Order Verification False";
                    _DBReturnData.Retval = "FAILURE";
                }

            }
            catch(Exception e)
            {
                DALBASE _DALBASE = new();
                _DALBASE.ErrorLog("VerifyPayment", "DLOrder", e.ToString());
                _DBReturnData.Dataset = null;
                _DBReturnData.Status = DBEnums.Status.FAILURE;
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                _DBReturnData.Message = "An internal error occurred";
                _DBReturnData.Retval = "FAILURE";

            }
            return _DBReturnData;
        }

    }
}
