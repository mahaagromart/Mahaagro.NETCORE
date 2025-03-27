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
        public ActionResult<IEnumerable<DBReturnData>> CreateOrder(MLOrder _MLOrder)
        {
            DBReturnData _DBReturnData = new();


            return new[] { _DBReturnData = _dLOrder.CreateOrder(_MLOrder) };

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
            return new []{ _DBReturnData};
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
    }

   
}
