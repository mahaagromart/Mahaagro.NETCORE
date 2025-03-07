using ECOMAPP.CommonRepository;
using ECOMAPP.DataLayer;
using ECOMAPP.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using static ECOMAPP.MiddleWare.AppEnums;

namespace ECOMAPP.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrandController : Controller
    {

        [Route("GetAllBrand")]
        [HttpGet]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> GetAllBrand()
        {
            DLBrand _DLBrand = new();
            MLBrand _MLBrand = new();
            DBReturnData _DBReturnData = new();
            try
            {
                _MLBrand.BrandList = _DLBrand.GetAllBrand();
                if (_MLBrand.BrandList.Count > 0)
                {


                    _DBReturnData.Dataset = _MLBrand.BrandList;
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                    _DBReturnData.Message =DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
                }
                else
                {
                    _DBReturnData.Dataset = null;
                    _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                    _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();

                }

            }
            catch(Exception ex)
            {
    
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();

            }

            return new[] { _DBReturnData };
        }
        [Route("InsertBrand")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin])]

        public ActionResult<IEnumerable<DBReturnData>> InsertBrand()
        {
            DLBrand _DLBrand = new();
            DBReturnData _DBReturnData = new();
            try
            {
                    _DBReturnData = _DLBrand.InsertBrand();
                    
                
            }
            catch (Exception ex)
            {
        
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();

            }

            return new[] { _DBReturnData };
        }



        [Route("UpdateBrand")]
        [HttpPut]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> UpdateBrand()
        {
            DLBrand _DLBrand = new();
            DBReturnData _DBReturnData = new();
            try
            {
                _DBReturnData = _DLBrand.UpdateBrand();
          

            }
            catch (Exception ex)
            {
                _DBReturnData.Dataset = null;
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();

            }

            return new[] { _DBReturnData };
        }


        [Route("DeleteBrand")]
        [HttpDelete]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> DeleteBrand()
        {
            DLBrand _DLBrand = new();
            DBReturnData _DBReturnData = new();
            try
            {
                _DBReturnData = _DLBrand.DeleteBrand();
            

            }
            catch (Exception ex)
            {
          
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();

            }

            return new[] { _DBReturnData };
        }


    }
}
