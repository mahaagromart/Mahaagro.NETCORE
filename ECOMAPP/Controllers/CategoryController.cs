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
        EcommerceCategoryDTO ObjDLCategoryDTO = new();

        //[JwtAuthorization(Roles = new[] { Roles.User, Roles.Vendor })]
        //DLCatagory catdl = new();



        #region insertedcat commented

        //[Route("InsertProductCategory")]
        //[HttpPost]
        //public IEnumerable<DBReturnData> InsertProductCategory([FromBody] MLCrudCategory _mlCrudCategory)
        //{
        //    ObjDLCategory = new DLCatagory();
        //    var objReturnData = new DBReturnData();

        //    try
        //    {
        //        // Here you may apply reference handling if necessary
        //        var options = new JsonSerializerOptions
        //        {
        //            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
        //        };

        //        objReturnData.Dataset = ObjDLCategory.InsertProductCategory(_mlCrudCategory);
        //        //objReturnData.Dataset = new DataSet();
        //        objReturnData.Status = DBEnums.Status.SUCCESS;
        //        objReturnData.Message = DBEnums.Status.SUCCESS.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception (implement logging)
        //        objReturnData.Dataset = null;
        //        objReturnData.Status = DBEnums.Status.FAILURE;
        //        objReturnData.Message = DBEnums.Status.FAILURE.ToString() + " DUE TO " + ex.Message;
        //    }

        //    return new[] { objReturnData };
        //}


        #endregion


        #region Commented code

        [Route("GetAllCategory")]
        [HttpGet]
        //[JwtAuthorization(Roles = new[] { Roles.User, Roles.Vendor })]

        public JsonResult InsertProductCategory([FromBody])
        {
            ObjDLCategory = new DLCatagory();
            DBReturnData objReturnData = new DBReturnData();
            try
            {
                objReturnData.Dataset = ObjDLCategory.GetAllCategory();
               
            }
            catch (Exception ex)
            {
                objReturnData.Dataset = null;
                objReturnData.Status = DBEnums.Status.FAILURE;
                objReturnData.Message = DBEnums.Status.FAILURE.ToString() + " due to " + ex.Message;
            }
            return new[] { objReturnData };
        }



        //[Route("UpdateProductCategory")]
        //[HttpPut]
        //[JwtAuthorization(Roles = new[] { Roles.User, Roles.Vendor })]
        public IEnumerable<DBReturnData> UpdateProductCategory([FromBody] MlInsertProductCategoryData _MlInsertProductdata)
        {
            var objDLProduct = new DLProduct();
            var objReturnData = new DBReturnData();

            try
            {

                ObjDLCategoryDTO = ObjDLCategory.InsertProductCategory(_MlInsertProductdata);
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


        //[Route("DeleteEcommerceCategory")]
        //[HttpPut]
        //[JwtAuthorization(Roles = new[] { Roles.User, Roles.Vendor })]
        //public IEnumerable<DBReturnData> DeleteEcommerceCategory([FromBody] MLFilterProduct mLFilterProduct)
        //{
        //    var objDLProduct = new DLProduct();
        //    var objReturnData = new DBReturnData();

        //    try
        //    {

        //        objReturnData.Dataset = objDLProduct.DeleteEcommerceCategory(mLFilterProduct);
        //        objReturnData.Status = DBEnums.Status.SUCCESS;
        //        objReturnData.Message = DBEnums.Status.SUCCESS.ToString();


        //    }
        //    catch (Exception ex)
        //    {
        //        objReturnData.Dataset = null;
        //        objReturnData.Status = DBEnums.Status.FAILURE;
        //        objReturnData.Message = DBEnums.Status.FAILURE.ToString() + " DUE TO " + ex.ToString();


        //    }
        //    return new[] { objReturnData };
        //}


        #endregion

        [Route("InsertProductCategory")]
        [HttpPost]
        public JsonResult InsertProductCategory([FromBody] MlInsertProductCategoryData _MlInsertProductdata)
        {

            try
            {

                 ObjDLCategoryDTO =  ObjDLCategory.InsertProductCategory(_MlInsertProductdata);

            }
            catch (Exception ex)
            {
            
                
            }
            return Json(ObjDLCategoryDTO);

        }


    }
}
