using static ECOMAPP.MiddleWare.AppEnums;
using Microsoft.AspNetCore.Mvc;
using ECOMAPP.DataLayer;
using ECOMAPP.CommonRepository;
using ECOMAPP.ModelLayer;
using System.Text.Json;
using System.Data;


namespace ECOMAPP.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        DLCatagory ObjDLCategory = new();
        MLCategoryDTO ObjMLCategory = new();


        [Route("GetAllCategory")]
        [HttpPost]

        //[JwtAuthorization(Roles = new[] { Roles.User, Roles.Vendor })]
        public JsonResult GetAllCategory()
        {
            try
            {
                ObjMLCategory = ObjDLCategory.GetAllCategory();
                if (ObjMLCategory.CategoryList.Count > 0)
                {
                    ObjMLCategory.Code = 200;
                    ObjMLCategory.Message = "SUCCESS";
                    ObjMLCategory.Retval = "SUCCESS";

                }
            }
            catch (Exception ex)
            {
                ObjMLCategory.Retval = "Failed";
                ObjMLCategory.Message = "internal server error due to" + ex.ToString();
                ObjMLCategory.Code = 500;
            }
            return Json(ObjMLCategory);
        }


        [Route("UpdateProductCategory")]
        [HttpPut]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public JsonResult  UpdateProductCategory([FromBody] MlUpdateProductCategoryData _MlUpdateProductdata)
        {
      
            var objReturnData = new DBReturnData();

            try
            {

                ObjMLCategory = ObjDLCategory.UpdateProductCategory(_MlUpdateProductdata);
                if (ObjMLCategory.Code == 200)
                {
                    objReturnData.Status = DBEnums.Status.SUCCESS;
                    objReturnData.Message = DBEnums.Status.SUCCESS.ToString();

                }
                

            }
            catch (Exception ex)
            {
                objReturnData.Dataset = null;
                objReturnData.Status = DBEnums.Status.FAILURE;
                objReturnData.Message = DBEnums.Status.FAILURE.ToString() + " DUE TO " + ex.ToString();


            }
            return Json( objReturnData );
        }


        [Route("InsertProductCategory")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public JsonResult InsertProductCategory([FromBody] MlInsertProductCategoryData _MlInsertProductdata)
        {
    
            var objReturnData = new DBReturnData();

            try
            {


                ObjMLCategory = ObjDLCategory.InsertProductCategory(_MlInsertProductdata);
                if(ObjMLCategory.Code == 200)
                {
                    objReturnData.Status = DBEnums.Status.SUCCESS;
                    objReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                }

            }
            catch (Exception ex)
            {

                objReturnData.Status = DBEnums.Status.FAILURE;
                objReturnData.Message = DBEnums.Status.FAILURE.ToString() + " DUE TO " + ex.ToString();


            }
            return Json(ObjMLCategory);

        }


        [Route("DeleteProductCategory")]
        [HttpDelete]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> DeleteProductCategory([FromBody] MlDeleteProductCategory _MlDeleteCategoryData)
        {
            DLCatagory objDlcat = new();
            DBReturnData _DBReturnData = new();
            try
            {
                _DBReturnData = objDlcat.DeleteProductCategory(_MlDeleteCategoryData);
                if(_DBReturnData.Message == "SUCCESS")
                {
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                    _DBReturnData.Message =DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();

                }
                else
                {
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                    _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();

                }


            }
            catch (Exception ex)
            {
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
            }

            return new[] { _DBReturnData };


        }



    }
}
