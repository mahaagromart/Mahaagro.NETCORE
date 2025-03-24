using System.Data;
using System.IdentityModel.Tokens.Jwt;
using ECOMAPP.CommonRepository;
using ECOMAPP.DataLayer;
using ECOMAPP.ModelLayer;
using Microsoft.AspNetCore.Mvc;

namespace ECOMAPP.Controllers
{
    public class CartController : Controller
    {



        [Route("GetCartData")]
        [HttpGet]
        public ActionResult<IEnumerable<DBReturnData>> GetCartData()
        {
            DBReturnData _DBReturnData = new();
            DLCart _DLCart = new();
            MLCart _MLCart = new();


            try
            {
                _MLCart.CartDataList = _DLCart.GetCartData();
                if (_MLCart.CartDataList != null && _MLCart.CartDataList.Count > 0)

                {
                    _DBReturnData.Dataset = _MLCart.CartDataList;
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
                _DBReturnData.Message =DBEnums.Status.FAILURE + ex.Message;
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
               
            }
            return new[] { _DBReturnData };
        }


        [Route("InsertCartData")]
        [HttpPost]
        public ActionResult<IEnumerable<DBReturnData>> InsertCartData()
        {
            DBReturnData _DBReturnData = new();
            DLCart _DLCart = new();
            DataSet _DataSet = new();

            try
            {
                _DBReturnData = _DLCart.InsertCartData();
                if (_DBReturnData.Code == DBEnums.Codes.SUCCESS)
                {
                    _DBReturnData.Retval=DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Message=DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;

                }
                else
                {
                    _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;

                }



            }
            catch (Exception ex) { 
                     _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                     _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() +ex.Message.ToString();
                     _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
            

            }
            return new[] {_DBReturnData};

        }




        [Route("UpdateCartData")]
        [HttpPut]
        public ActionResult<IEnumerable<DBReturnData>> UpdateCartData()
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
                _DBReturnData = _DLCart.UpdateCartData();
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