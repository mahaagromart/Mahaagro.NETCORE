using ECOMAPP.CommonRepository;
using ECOMAPP.DataLayer;
using ECOMAPP.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using static ECOMAPP.MiddleWare.AppEnums;
using static ECOMAPP.ModelLayer.MLSubsubcategory;

namespace ECOMAPP.Controllers
{
    [Route("[Controller]")]
    [ApiController]

    public class SubsubCategoryController : Controller
    {
        [Route("GetAllSubsubCategory")]
        [HttpGet]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> GetAllSubsubCategory()
        {
            DLSubsubcategory _DLSubsubcategory = new();
            MLSubsubcategory _MLSubsubcategory = new();
            DBReturnData _DBReturnData = new();

            try
            {
                _MLSubsubcategory.SubsubCategoryList = _DLSubsubcategory.GetAllSubsubCategory();
                if (_MLSubsubcategory.SubsubCategoryList.Count > 0)
                {
                    _DBReturnData.Dataset = _MLSubsubcategory.SubsubCategoryList;
                    _DBReturnData.Code = 200;
                    _DBReturnData.Message = "";
                    _DBReturnData.Retval = "SUCCESS";
                }
                else
                {
                    _DBReturnData.Dataset = null;
                    _DBReturnData.Retval = "FAILED";
                    _DBReturnData.Message = "internal server error due to";
                    _DBReturnData.Code = 500;
                }

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
        [Route("InsertSubsubCategory")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> InsertSubsubCategory(MLInsertsubsubcategory _MLInsertsubsubcategory)
        {
            DLSubsubcategory _DLSubsubcategory = new();
            MLSubsubcategory _MLSubsubcategory = new();
            DBReturnData _DBReturnData = new();


            try
            {
                _MLSubsubcategory = _DLSubsubcategory.InsertSubsubCategory(_MLInsertsubsubcategory);
                _DBReturnData.Code = 200;
                _DBReturnData.Message = "";
                _DBReturnData.Retval = "SUCCESS";


            }



            catch (Exception ex)
            {
                _DBReturnData.Dataset = null;
                _DBReturnData.Retval = "FAILED";
                _DBReturnData.Message = "internal server error due to";
                _DBReturnData.Code = 500;

            }
            return new[] { _DBReturnData };


        }


        [Route("UpdateSubsubCategory")]
        [HttpPut]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> UpdateSubsubCategory(MLUpdateSubsubcategory _MLUpdateSubsubcategory)
        {
            DLSubsubcategory _DLSubsubcategory = new();
            MLSubsubcategory _MLSubsubcategory = new();
            DBReturnData _DBReturnData = new();
            try
            {
                _MLSubsubcategory = _DLSubsubcategory.UpdateSubsubCategory(_MLUpdateSubsubcategory);
                _DBReturnData.Code = 200;
                _DBReturnData.Message = "";
                _DBReturnData.Retval = "SUCCESS";
            }
            catch (Exception ex)
            {
           
                _DBReturnData.Retval = "FAILED";
                _DBReturnData.Message = "internal server error due to";
                _DBReturnData.Code = 500;

            }
            return new[] { _DBReturnData };
        }


        [Route("DeleteSubsubCategory")]
        [HttpDelete]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> DeleteSubsubCategory(MLDeletesubsubcategory _MLDeletesubsubcategory)
        {
            DLSubsubcategory _DLSubsubcategory = new();
            MLSubsubcategory _MLSubsubcategory = new();
            DBReturnData _DBReturnData = new();
            try
            {
                _MLSubsubcategory = _DLSubsubcategory.DeleteSubsubCategory(_MLDeletesubsubcategory);
                _DBReturnData.Code = 200;
                _DBReturnData.Message = "";
                _DBReturnData.Retval = "SUCCESS";

            }
            catch (Exception ex)
            {
                _DBReturnData.Dataset = null;
                _DBReturnData.Retval = "FAILED";
                _DBReturnData.Message = "internal server error due to";
                _DBReturnData.Code = 500;

            }
            return new [] { _DBReturnData };
        }
    }



}
