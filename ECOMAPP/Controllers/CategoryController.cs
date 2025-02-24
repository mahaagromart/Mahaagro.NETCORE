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
        [HttpGet]
        [JwtAuthorization(Roles = [Roles.Admin])]
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
                objReturnData.Status = DBEnums.Status.SUCCESS;
                objReturnData.Message = DBEnums.Status.SUCCESS.ToString();


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
                objReturnData.Status = DBEnums.Status.SUCCESS;
                objReturnData.Message = DBEnums.Status.SUCCESS.ToString();

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
                ObjMLCategory = objDlcat.DeleteProductCategory(_MlDeleteCategoryData);
                _DBReturnData.Code = 200;
                _DBReturnData.Message = "";
                _DBReturnData.Retval = "SUCCESS";



            }
            catch (Exception ex)
            {
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Retval = null;
            }

            return new[] { _DBReturnData };


        }



    }
}
