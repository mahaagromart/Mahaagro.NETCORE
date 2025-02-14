using System.Collections;
using ECOMAPP.CommonRepository;
using ECOMAPP.DataLayer;
using ECOMAPP.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using static ECOMAPP.MiddleWare.AppEnums;

namespace ECOMAPP.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        DLProduct objDLProduct;

        [Route("DBPostProduct")]
        [HttpPost]
        public IEnumerable<DBReturnData> DBPostProduct([FromBody] MLCrudProduct _mlCrudProduct)
        {
            objDLProduct = new DLProduct();
            DBReturnData objReturnData = new DBReturnData();
            try
            {
                objReturnData.Dataset = objDLProduct.DBPostProduct(_mlCrudProduct);
                objReturnData.Status = DBEnums.Status.SUCCESS;
                objReturnData.Message = DBEnums.Status.SUCCESS.ToString();
            }
            catch (Exception ex)
            {
                objReturnData.Dataset = null;
                objReturnData.Status = DBEnums.Status.FAILURE;
                objReturnData.Message = DBEnums.Status.FAILURE.ToString() + " due to " + ex.Message;
            }
            return new[] { objReturnData };
        }




        [Route("DBGetProduct")]
        [HttpGet]
        public IEnumerable<DBReturnData> DBGetProduct([FromBody] MLFilterProduct _mlFilterProduct)
        {
            objDLProduct = new DLProduct();
            DBReturnData objReturnData = new DBReturnData();
            try
            {
                objReturnData.Dataset = objDLProduct.DBGetProduct(_mlFilterProduct);
                objReturnData.Status = DBEnums.Status.SUCCESS;
                objReturnData.Message = DBEnums.Status.SUCCESS.ToString();
            }
            catch (Exception ex)
            {
                objReturnData.Dataset = null;
                objReturnData.Status = DBEnums.Status.FAILURE;
                objReturnData.Message = DBEnums.Status.FAILURE.ToString() + " due to " + ex.Message;
            }
            return new[] { objReturnData };
        }
       
        
        
        [Route("DBUpdateProduct")]
        [HttpPut]
        public IEnumerable<DBReturnData> DBUpdateProduct([FromBody] MLFilterProduct mLFilterProduct)
        {
            var objDLProduct = new DLProduct();
            var objReturnData = new DBReturnData();

            try
            {
            
                objReturnData.Dataset = objDLProduct.DBUpdateProduct(mLFilterProduct);
                objReturnData.Status = DBEnums.Status.SUCCESS;
                objReturnData.Message = DBEnums.Status.SUCCESS.ToString();

              
            }
            catch (Exception ex)
            {
                objReturnData.Dataset = null;
                objReturnData.Status = DBEnums.Status.FAILURE;
                objReturnData.Message = DBEnums.Status.FAILURE.ToString() + " DUE TO " + ex.ToString();

                
            }
            return new[] { objReturnData };
        }


        [Route("TestToken")]
        [HttpPost]
        [JwtAuthorization(Roles = new[] { Roles.User, Roles.Vendor })]
        public string TestToken()
        {
             
            return "ok";


        }

    }
}
