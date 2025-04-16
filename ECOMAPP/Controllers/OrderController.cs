using System.Security.Cryptography;
using System.Text;
using ECOMAPP.CommonRepository;
using ECOMAPP.DataLayer;
using ECOMAPP.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using static ECOMAPP.MiddleWare.AppEnums;


namespace ECOMAPP.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DLOrder _dLOrder;
        private readonly IConfiguration _configuration;

        public OrderController(DLOrder dLOrder, IConfiguration configuration)
        {
            _dLOrder = dLOrder;
            _configuration = configuration;
        }

        [HttpPost("CreateOrder")]
        [JwtAuthorization(Roles = [Roles.Admin, Roles.Vendor, Roles.User])]
        public DBReturnData CreateOrderAsync(MLOrder _MLOrder)
        {
            // Create the order and return an array with one item
            DBReturnData _DBReturnData = _dLOrder.CreateOrderAsync(_MLOrder);
            return _DBReturnData;
        }



        //[HttpPost("CreateOrder")]
        //[JwtAuthorization(Roles = [Roles.Admin, Roles.Vendor, Roles.User])]
        //public JsonResult CreateOrder(MLOrder _MLOrder)
        //{
        //    try
        //    {
        //        var result = _dLOrder.CreateOrder(_MLOrder);
        //        return new JsonResult(result) { StatusCode = 200 };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new
        //        {
        //            Status = DBEnums.Status.FAILURE,
        //            Code = DBEnums.Codes.INTERNAL_SERVER_ERROR,
        //            Message = "An error occurred while creating the order",
        //            Error = ex.Message
        //        })
        //        { StatusCode = 500 };
        //    }
        //}



        [HttpPost("VerifyPayment")]
        [JwtAuthorization(Roles = [Roles.Admin, Roles.Vendor, Roles.User])]
        public ActionResult<IEnumerable<DBReturnData>> VerifyPayment(RazorpayPaymentVerification paymentVerification)
        {
            DBReturnData _DBReturnData = new();
            try
            {
                var keySecret = _configuration["Razorpay:KeySecret"];


                string payload = $"{paymentVerification.razorpay_order_id}|{paymentVerification.razorpay_payment_id}";
                string generatedSignature = CreateHMACSHA256Signature(payload, keySecret);





                if (generatedSignature == paymentVerification.razorpay_signature)
                {

                    _DBReturnData = _dLOrder.VerifyOrder(paymentVerification.razorpay_order_id, paymentVerification.razorpay_payment_id);

                    if (_DBReturnData.Retval == "SUCCESS")
                    {

                        _DBReturnData.Status = DBEnums.Status.SUCCESS;
                        _DBReturnData.Message = DBEnums.Codes.ORDER_VERIFIED.ToString();
                        _DBReturnData.Retval = DBEnums.Codes.SUCCESS.ToString();
                    }
                    else
                    {

                        _DBReturnData.Status = DBEnums.Status.FAILURE;
                        _DBReturnData.Message = DBEnums.Codes.ORDER_NOT_VERIFIED.ToString();
                        _DBReturnData.Retval = DBEnums.Codes.ORDER_NOT_VERIFIED.ToString();
                    }
                }
                else
                {
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    _DBReturnData.Message = DBEnums.Codes.ORDER_NOT_VERIFIED.ToString();
                    _DBReturnData.Retval = DBEnums.Codes.ORDER_NOT_VERIFIED.ToString();

                }
            }
            catch (Exception ex)
            {
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString() + "due to" + ex.ToString();
            }
            return new[] { _DBReturnData };
        }

        private string CreateHMACSHA256Signature(string payload, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] messageBytes = Encoding.UTF8.GetBytes(payload);

            using (var hmac = new HMACSHA256(keyBytes))
            {
                byte[] hashMessage = hmac.ComputeHash(messageBytes);
                return BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
            }
        }
        [HttpGet("GetServiceAvailability")]
        public async Task<ActionResult<DBReturnData>> GetServiceAvailability(string pincode)
        {
            DBReturnData _DBReturnData = new DBReturnData();

            try
            {
                // Call DLOrder and receive the whole DBReturnData
                var result = await _dLOrder.GetServiceAvailability(pincode);

                // Just return what DLOrder already structured
                return Ok(result);
            }
            catch (Exception ex)
            {
                _DBReturnData.Status = DBEnums.Status.FAILURE;
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                _DBReturnData.Message = "Internal server error occurred.";
                _DBReturnData.Retval = "FAILURE due to: " + ex.Message;
                return StatusCode(500, _DBReturnData);
            }
        }

    }

}
