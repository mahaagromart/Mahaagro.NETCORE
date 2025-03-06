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
                    _DBReturnData.Code = 200;
                    _DBReturnData.Message = "";
                    _DBReturnData.Retval = "SUCCESS";
                }
                else
                {
                    _DBReturnData.Dataset = null;
                    _DBReturnData.Code = 200;
                    _DBReturnData.Message = "FAILED";
                    _DBReturnData.Retval = "FAILED";

                }

            }
            catch(Exception ex)
            {
                _DBReturnData.Dataset = null;
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Retval = null;

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
                _DBReturnData.Dataset = null;
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Retval = null;

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
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Retval = null;

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
                _DBReturnData.Dataset = null;
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Retval = null;

            }

            return new[] { _DBReturnData };
        }


    }
}
