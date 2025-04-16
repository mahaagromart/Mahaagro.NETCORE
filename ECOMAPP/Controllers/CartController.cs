using System.Data;
using System.IdentityModel.Tokens.Jwt;
using ECOMAPP.CommonRepository;
using ECOMAPP.DataLayer;
using ECOMAPP.ModelLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static ECOMAPP.MiddleWare.AppEnums;
using static ECOMAPP.ModelLayer.MLCart;

namespace ECOMAPP.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController : Controller
    {



        [Route("GetCartData")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.User, Roles.OrderManager, Roles.ReprotAnalysis, Roles.Admin,Roles.Vendor])]
        public ActionResult<IEnumerable<DBReturnData>> GetCartData()
        {
            DBReturnData _DBReturnData = new();
            DLCart _DLCart = new();
            MLCart _MLCart = new();

            string? JwtToken = Request.Headers["Authorization"];
            JwtToken = JwtToken["Bearer ".Length..].Trim();
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(JwtToken);
            var tokenS = jsonToken as JwtSecurityToken;
            var UserId = tokenS?.Claims.First(claim => claim.Type == "UserId").Value;

            try
            {
                _MLCart.ProductsCart = _DLCart.GetCartData(UserId ?? "");
                if (_MLCart.ProductsCart != null && _MLCart.ProductsCart.Count > 0)
                {
                    _DBReturnData.Dataset = _MLCart.ProductsCart;
                    _DBReturnData.Status = DBEnums.Status.SUCCESS;
                    _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;


                }
                else
                {
                    _DBReturnData.Dataset = null;
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;

                }



            }
            catch (Exception ex)
            {
                _DBReturnData.Status = DBEnums.Status.FAILURE;
                _DBReturnData.Message = DBEnums.Status.FAILURE + ex.Message;
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;

            }
            return new[] { _DBReturnData };
        }


        [Route("InsertCartData")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.User, Roles.OrderManager, Roles.ReprotAnalysis, Roles.Admin, Roles.Vendor])]
        public ActionResult<IEnumerable<DBReturnData>> InsertCartData([FromBody] MLInsertCart mLInsertCart)
        {
            DBReturnData _DBReturnData = new();
            DLCart _DLCart = new();
            DataSet _DataSet = new();

            string? JwtToken = Request.Headers["Authorization"];
            JwtToken = JwtToken["Bearer ".Length..].Trim();
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(JwtToken);
            var tokenS = jsonToken as JwtSecurityToken;
            var UserId = tokenS?.Claims.First(claim => claim.Type == "UserId").Value;


            try
            {
                _DBReturnData = _DLCart.InsertCartData(mLInsertCart, UserId);

            }
            catch (Exception ex)
            {
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;


            }
            return new[] { _DBReturnData };

        }




        [Route("UpdateCartData")]
        [HttpPost]
        public ActionResult<IEnumerable<DBReturnData>> UpdateCartData([FromBody] MLInsertCart mLInsertCart)
        {
            DBReturnData _DBReturnData = new();
            DLCart _DLCart = new();
            DataSet _DataSet = new();

            string? JwtToken = Request.Headers["Authorization"];
            JwtToken = JwtToken["Bearer ".Length..].Trim();
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(JwtToken);
            var tokenS = jsonToken as JwtSecurityToken;
            var UserId = tokenS?.Claims.First(claim => claim.Type == "UserId").Value;

            try
            {
                _DBReturnData = _DLCart.UpdateCartData(mLInsertCart, UserId);

            }
            catch (Exception ex)
            {
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;


            }
            return new[] { _DBReturnData };

        }

        [Route("UpdateCartDataChangeQuantity")]
        [HttpPost]
        public ActionResult<IEnumerable<DBReturnData>> UpdateCartDataChangeQuantity([FromBody] MLInsertCart mLInsertCart)
        {
            DBReturnData _DBReturnData = new();
            DLCart _DLCart = new();
            DataSet _DataSet = new();

            string? JwtToken = Request.Headers["Authorization"];
            JwtToken = JwtToken["Bearer ".Length..].Trim();
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(JwtToken);
            var tokenS = jsonToken as JwtSecurityToken;
            var UserId = tokenS?.Claims.First(claim => claim.Type == "UserId").Value;

            try
            {
                _DBReturnData = _DLCart.UpdateCartDataChangeQuantity(mLInsertCart, UserId);

            }
            catch (Exception ex)
            {
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;


            }
            return new[] { _DBReturnData };

        }



        [Route("DeleteCartData")]
        [HttpDelete]
        public ActionResult<IEnumerable<DBReturnData>> DeleteCartData()
        {
            DBReturnData _DBReturnData = new();
            DLCart _DLCart = new();
            DataSet _DataSet = new();

            try
            {
                _DBReturnData = _DLCart.DeleteCartData();
                if (_DBReturnData.Code == DBEnums.Codes.SUCCESS)
                {
                    _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;

                }
                else
                {
                    _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;

                }



            }
            catch (Exception ex)
            {
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;


            }
            return new[] { _DBReturnData };

        }
    }
}