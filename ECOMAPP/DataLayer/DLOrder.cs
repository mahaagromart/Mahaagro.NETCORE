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


        public async Task<DBReturnData> CreateOrderAsync(MLOrder _MLOrder)
        {
            DataSet _DataSet = new();
            DBReturnData _DBReturnData = new();
            List<dynamic> orders = new();
            decimal totalDeliveryCharges = 0;
            Dictionary<string, string> sellerByVarientId = new();
            try
            {
    
                if (_MLOrder.VarientID == null || _MLOrder.VarientID.Length == 0)
                {
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                    _DBReturnData.Message = "No Variant IDs provided";
                    _DBReturnData.Retval = "FAILURE";
                    return _DBReturnData;
                }

           
                foreach (var varientId in _MLOrder.VarientID)
                {

                    using (DBAccess _DBAccess = new())
                    {
                        _DBAccess.DBProcedureName = "SP_PRODUCT";
                        _DBAccess.AddParameters("@Action", "CHECKPRODUCTBYVARIENTID");
                        #pragma warning disable CS8604 // Possible null reference argument.
                        _DBAccess.AddParameters("@VID", varientId);
                        _DataSet = _DBAccess.DBExecute();
                    }

                    if (_DataSet.Tables.Count < 2)
                    {
                        continue;
                    }

                    DataTable _DataTable = _DataSet.Tables[0];
                    string Retval = _DataSet.Tables[1].Rows[0]["Retval"]?.ToString();

                    if (Retval == "SUCCESS" && _DataTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in _DataTable.Rows)
                        {
                            var varient = row["VARIENTS_ID"]?.ToString();
                            var seller = row["UserId"]?.ToString();
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
                                VarientID = varient,
                                PickupAddress = row["Address"]?.ToString(),
                                SellerId = seller
                            };

                            orders.Add(currentOrder);
                            if (!string.IsNullOrEmpty(varient) && !string.IsNullOrEmpty(seller))
                            {
                                sellerByVarientId[varient] = seller;
                            }
                        }
                    }
                }

                if (orders.Count == 0)
                {
                    _DBReturnData.Dataset = null;
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;
                    _DBReturnData.Message = "Order creation failed - No products found";
                    _DBReturnData.Retval = "FAILURE";
                    return _DBReturnData;
                }

                // Calculate total product price
                int totalProductPrice = orders.Sum(o => Convert.ToInt32(o.CalculatedPrice ?? "0"));

                // Calculate delivery charges for each variant
                Dictionary<string, decimal> deliveryChargesByVarient = new();

                foreach (var order in orders)
                {
                    decimal deliveryCharges = await GetDeliveryCharges("", order, _MLOrder.DeliveryAddress);
                    totalDeliveryCharges += deliveryCharges;
                    deliveryChargesByVarient[order.VarientID] = deliveryCharges;
                }

                // ✅ Add delivery charges to total price
                int totalPrice = totalProductPrice + Convert.ToInt32(totalDeliveryCharges);

                if (totalPrice > 0)
                {
                    // Razorpay Payment Integration
                    var keyId = _configuration["Razorpay:KeyId"];
                    var keySecret = _configuration["Razorpay:KeySecret"];
                    _razorpayClient = new RazorpayClient(keyId, keySecret);

                    var options = new Dictionary<string, object>
            {
                { "amount", totalPrice * 100 }, // amount in paise
                { "currency", "INR" },
                { "receipt", Guid.NewGuid().ToString() },
                { "payment_capture", "1" }
            };

                    Order razorOrder = _razorpayClient.Order.Create(options);
                    var razorpayOrderId = razorOrder["id"].ToString();

                    string razorOrderJson = JsonConvert.SerializeObject(razorOrder.Attributes, Formatting.Indented);
                    RazorpayOrder orderDetails = JsonConvert.DeserializeObject<RazorpayOrder>(razorOrderJson);

                    if (razorpayOrderId != null)
                    {
                        using (DBAccess _DBAccess = new())
                        {
                            _DBAccess.DBProcedureName = "SP_ORDER";
                            _DBAccess.AddParameters("@Action", "INSERTORDER");
                            _DBAccess.AddParameters("@USER_ID", _MLOrder.UserId);
                             var varientString = string.Join(",", _MLOrder.VarientID);
                            _DBAccess.AddParameters("@VARIENTS_ID", varientString);
                            _DBAccess.AddParameters("@ORDER_ID", razorpayOrderId);
                            _DBAccess.AddParameters("@AMOUNT", totalPrice); 
                            _DBAccess.AddParameters("@CURRENCY", "INR");

                            _DBAccess.DBExecute();
                            _DBAccess.Dispose();

                        }
                            string Retval = _DataSet.Tables[1].Rows[0]["Retval"]?.ToString();
                    }

                    foreach (var varientId in _MLOrder.VarientID)
                    {
                        if (!deliveryChargesByVarient.ContainsKey(varientId) || !sellerByVarientId.ContainsKey(varientId))
                            continue;

                        string sellerId = sellerByVarientId[varientId];
                        decimal deliveryCharge = deliveryChargesByVarient[varientId];

                        using (DBAccess _DBAccess = new()){
                            _DBAccess.DBProcedureName = "SP_ORDER";
                            _DBAccess.AddParameters("@ACTION", "INSERTORDERBYDELIVERY");
                            _DBAccess.AddParameters("@USER_ID", _MLOrder.UserId);
                            _DBAccess.AddParameters("@VARIENT_ID", varientId);
                            _DBAccess.AddParameters("@SellerId", sellerId);
                            _DBAccess.AddParameters("@AMOUNT", deliveryCharge.ToString());
                            _DBAccess.DBExecute();
                            _DBAccess.Dispose();
                        }
          
                    }

                    _DBReturnData.Dataset = new
                    {
                        OrderDetails = orderDetails,
                        TotalPrice = totalPrice,
                        TotalDeliveryCharges = totalDeliveryCharges
                    };
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
            catch (Exception ex)
            {
                DALBASE _DALBASE = new();
                _DALBASE.ErrorLog("CreateOrderController", "DLOrder", ex.ToString());

                _DBReturnData.Dataset = null;
                _DBReturnData.Status = DBEnums.Status.FAILURE;
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                _DBReturnData.Message = "An internal error occurred";
                _DBReturnData.Retval = "FAILURE";
            }

            return _DBReturnData;
        }

        private async Task<decimal> GetDeliveryCharges(string orderId, dynamic order, string deliveryAddress)
        {
            decimal totalDeliveryCharges = 0;

         
            var credentials = new Credentials
            {
                username = "username@test.com",
                request_type = "QS-LEAP",
                request_action = "PICKUP",
                pickup_address_id = "x6vj12er",
                eeApiToken = "a7b85c1afa18e10656dfda90b121",
                business_type = "E-commerce"
            };

            try
            {

                var deliveryRequest = new
                {
                    OrderId = orderId,
                    Items = new[] { order },
                    PickupAddress = order.PickupAddress,
                    DeliveryAddress = deliveryAddress,
                    Credentials = credentials  
                };

                using (HttpClient client = new HttpClient())
                {
  
                    var response = await client.PostAsJsonAsync("https://api.quickshift.in/qs_ecom/externalAPI/public/createShipment", deliveryRequest);

                    if (response.IsSuccessStatusCode)
                    {

                        var deliveryResponse = await response.Content.ReadFromJsonAsync<dynamic>();


                        totalDeliveryCharges = deliveryResponse?.TotalDeliveryCharges ?? 0;
                    }
                    else
                    {
                        totalDeliveryCharges = 0;
                    }
                }
            }
            catch (Exception ex)
            {

                DALBASE _DALBASE = new();
                _DALBASE.ErrorLog("GetDeliveryCharges", "DeliveryAPI", ex.ToString());

                totalDeliveryCharges = 0;
            }

            return totalDeliveryCharges;
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
