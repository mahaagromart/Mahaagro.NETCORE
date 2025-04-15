using System.Data;
using ECOMAPP.CommonRepository;
using ECOMAPP.ModelLayer;
using Microsoft.Extensions.Configuration;
using Razorpay.Api;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using static ECOMAPP.ModelLayer.MLVarients;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Transactions;

namespace ECOMAPP.DataLayer
{
    public class DLOrder
    {
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;
        private readonly ILogger<DLOrder> _logger;
        private static readonly HttpClient _httpClient = new HttpClient();
        private RazorpayClient _razorpayClient;

        public DLOrder(IConfiguration configuration, TokenService tokenService, ILogger<DLOrder> logger = null)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _logger = logger;

            string razorpayKey = _configuration["Razorpay:Key"];
            string razorpaySecret = _configuration["Razorpay:Secret"];
            if (!string.IsNullOrEmpty(razorpayKey) && !string.IsNullOrEmpty(razorpaySecret))
            {
                _razorpayClient = new RazorpayClient(razorpayKey, razorpaySecret);
            }
        }
            public DBReturnData CreateOrderAsync(MLOrder _MLOrder)
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


                    foreach (var varientQtyStr in _MLOrder.VarientID)
                    {
                        string[] parts = varientQtyStr.Split(',');
                        string varientId = parts[0];
                        int quantity = parts.Length > 1 ? Convert.ToInt32(parts[1]) : 1;

                        using (DBAccess _DBAccess = new())
                        {
                            _DBAccess.DBProcedureName = "SP_PRODUCT";
                            _DBAccess.AddParameters("@Action", "CHECKPRODUCTBYVARIENTID");
                            #pragma warning disable CS8604 // Possible null reference argument.
                            _DBAccess.AddParameters("@VID", varientId);
                            _DBAccess.AddParameters("@QTY",quantity);
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

                if (orders.Count != _MLOrder.VarientID.Length)
                {
                    _DBReturnData.Dataset = null;
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;
                    _DBReturnData.Message = "Some items in your cart did not meet the minimum order quantity or stock requirements. Please clear the cart and try again.";
                    _DBReturnData.Retval = "FAILURE";
                    return _DBReturnData;
                }



                int totalProductPrice = orders.Sum(o => Convert.ToInt32(o.CalculatedPrice ?? "0"));

             
                    Dictionary<string, decimal> deliveryChargesByVarient = new();


                    foreach (var order in orders)
                    {
                        sellerByVarientId.TryGetValue(order.VarientID, out string sellerId);
                        int deliveryCharges =  GetDeliveryCharges(order);
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
                        string transactionId = GetTransactionId();

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
                                _DBAccess.AddParameters("@TRANSACTIONID",transactionId);

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

                            using (DBAccess _DBAccess = new())
                            {
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
                        _DBReturnData.OrderId = transactionId;
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

      
      public string GetTransactionId()
      {
            Random random = new Random();
            string txnId = "TXN-" + DateTime.Now.ToString("yyyyMMdd") + random.Next(1000, 9999).ToString();
            return txnId;
        }

        public AddressInfo GetSellerAndUserAddress(string sellerId, string userId, string deliveryAddress)
        {
            AddressInfo result = new();

            using (var dbAccess = new DBAccess())
            {
                // Fetch seller data
                dbAccess.DBProcedureName = "SP_ORDER";
                dbAccess.AddParameters("@ACTION", "GETSELLERPINCODEANDSELLERPINCODE");
                dbAccess.AddParameters("@USER_ID", sellerId);

                DataSet sellerDataSet = dbAccess.DBExecute();
                if (sellerDataSet?.Tables.Count > 0 && sellerDataSet.Tables[0].Rows.Count > 0)
                {
                    DataRow row = sellerDataSet.Tables[0].Rows[0]; 
                    result.SellerPinCode = row["PINCODE"]?.ToString();
                    result.SellerAddress = row["ADDRESS"]?.ToString();
                }
            }

            using (var dbAccess = new DBAccess())
            {
                // Fetch user data
                dbAccess.DBProcedureName = "SP_ORDER";
                dbAccess.AddParameters("@ACTION", "GETUSERPINCODEANDSELLERPINCODE");
                dbAccess.AddParameters("@SelectedAddressBlock", deliveryAddress);
                dbAccess.AddParameters("@USER_ID", userId);

                DataSet userDataSet = dbAccess.DBExecute();
                if (userDataSet?.Tables.Count > 0 && userDataSet.Tables[0].Rows.Count > 0)
                {
                    DataRow row = userDataSet.Tables[0].Rows[0]; 
                    result.UserPinCode = row["pincode"]?.ToString();
                    result.UserAddress = row["ADDRESS"]?.ToString();
                }
            }

            return result;
        }


        public int GetDeliveryCharges(dynamic order)
        {
            decimal totalDeliveryCharges = 0;


            decimal volWeight = (Convert.ToDecimal(order.PackageLength) * Convert.ToDecimal(order.PackageWidth) * Convert.ToDecimal(order.PackageHeight)) / 5000;
            decimal weight = Convert.ToDecimal(order.PackageWeight);


            decimal finalWeight = Math.Max(volWeight, weight);

            decimal baseCharge = 90;

            if (finalWeight <= 2000)
            {
                totalDeliveryCharges = baseCharge;  
            }
            else
            {
             
                decimal additionalWeight = finalWeight - 2000;
                int additionalKg = (int)Math.Ceiling(additionalWeight / 1000); 
                totalDeliveryCharges = baseCharge + (additionalKg * 35);  
            }

           
            decimal gst = totalDeliveryCharges * 18.0m / 100;  


            totalDeliveryCharges += gst;

            return (int)Math.Round(totalDeliveryCharges);
        }

        public class TokenService
        {
            private readonly IConfiguration _configuration;
            private readonly IMemoryCache _cache;
            private static readonly HttpClient _httpClient = new HttpClient();
            private const string CacheKey = "EkartAccessToken";

            public TokenService(IConfiguration configuration, IMemoryCache cache)
            {
                _configuration = configuration;
                _cache = cache;
            }

            public async Task<string> GetAccessTokenAsync()
            {
                // Try to get the token from cache
                if (_cache.TryGetValue(CacheKey, out TokenCacheEntry cachedEntry) &&
                    cachedEntry != null &&
                    DateTime.UtcNow < cachedEntry.ExpirationTime)
                {
                    return cachedEntry.AccessToken; // Return cached token if not expired
                }

                // Fetch new token if not in cache or expired
                string newToken = await FetchAccessTokenAsync();
                if (!string.IsNullOrEmpty(newToken))
                {
                    // Cache the token with a 24-hour expiration
                    var cacheEntry = new TokenCacheEntry
                    {
                        AccessToken = newToken,
                        ExpirationTime = DateTime.UtcNow.AddHours(24)
                    };
                    _cache.Set(CacheKey, cacheEntry, TimeSpan.FromHours(24));
                }

                return newToken;
            }

            private async Task<string> FetchAccessTokenAsync()
            {
                try
                {
                    string user = _configuration["Ekart:username"];
                    string pass = _configuration["Ekart:password"];

                    if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
                    {
                        Console.WriteLine("Username or password missing in configuration");
                        return string.Empty;
                    }

                    var secretData = new
                    {
                        username = user,
                        password = pass
                    };

                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
                        "https://app.elite.ekartlogistics.in/integrations/v2/auth/token/EKART_67a1e3aeb43c30b894d7d235",
                        secretData);

                    response.EnsureSuccessStatusCode();
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var tokenData = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);
                    return tokenData.TryGetValue("access_token", out var accessToken) ? accessToken : string.Empty;
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"HTTP error while fetching token: {ex.Message}");
                    return string.Empty;
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error parsing token response: {ex.Message}");
                    return string.Empty;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                    return string.Empty;
                }
            }

            // Class to store token and expiration in cache
            private class TokenCacheEntry
            {
                public string AccessToken { get; set; }
                public DateTime ExpirationTime { get; set; }
            }
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
