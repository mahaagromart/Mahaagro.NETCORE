using ECOMAPP.CommonRepository;
using ECOMAPP.DataLayer;
using ECOMAPP.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using static ECOMAPP.MiddleWare.AppEnums;
using static ECOMAPP.ModelLayer.MLSubcategory;

namespace ECOMAPP.Controllers
{

    [Route("[Controller]")]
    [ApiController]

    public class SubCategoryController : Controller
    {
        [Route("GetAllSubCategory")]
        [HttpGet]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> GetAllSubCategory()
        {
            DLSubcategory _DLSubcategory = new();
            MLSubcategory _MLSubcategory = new();
            DBReturnData DBReturnData = new();

            try
            {
                _MLSubcategory.SubcategoryList = _DLSubcategory.GetAllSubCategory();

                if (_MLSubcategory.SubcategoryList.Count > 0)
                {
                    DBReturnData.Dataset = _MLSubcategory.SubcategoryList;
                    DBReturnData.Code = 200;
                    DBReturnData.Message = "SUCCESS";
                    DBReturnData.Retval = "SUCCESS";

                }
                else
                {
                    DBReturnData.Dataset = null;
                    DBReturnData.Retval = "FAILED";
                    DBReturnData.Message = "internal server error due to";
                    DBReturnData.Code = 500;


                }

            }
            catch (Exception ex)
            {
                DBReturnData.Dataset = null;
                DBReturnData.Code = 500;
                DBReturnData.Message = "Internal Server Error";
                DBReturnData.Retval = null;

            }

            return new[] { DBReturnData };

        }
        [Route("InsertSubCategory")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> InsertSubCategory(MLInsertSubcategory _MLInsertSubcategory)
        {
            DLSubcategory _DLSubcategory = new();
            MLSubcategory _MLSubcategory = new();
            DBReturnData DBReturnData = new();
            try
            {
                _MLSubcategory = _DLSubcategory.InsertSubCategory(_MLInsertSubcategory);
                DBReturnData.Code = 200;
                DBReturnData.Message = "SUCCESS";
                DBReturnData.Retval = "SUCCESS";

            }
            catch (Exception ex)
            {
                DBReturnData.Dataset = null;
                DBReturnData.Code = 500;
                DBReturnData.Message = "Internal Server Error" + ex.Message.ToString();
                DBReturnData.Retval = null;

            }



            return new[] { DBReturnData };
        }


        [Route("UpdateSubCategory")]
        [HttpPut]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> UpdateSubCategory(MLUpdateSubcategory _MLUpdateSubcategory)
        {
            MLSubcategory _MLSubcategory = new();
            DLSubcategory _DLSubcategory = new();
            DBReturnData _DBReturnData = new();
            try
            {
                _MLSubcategory = _DLSubcategory.UpdateSubCategory(_MLUpdateSubcategory);
                _DBReturnData.Code = 200;
                _DBReturnData.Message = "";
                _DBReturnData.Retval = "SUCCESS";

            }
            catch (Exception ex)
            {
                _DBReturnData.Dataset = null;
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error" + ex.Message.ToString();
                _DBReturnData.Retval = null;

            }
            return new[] { _DBReturnData };
        }


        [Route("DeleteSubCategory")]
        [HttpDelete]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> DeleteSubCategory(MLDeleteSubCateggory _MLDeleteSubCateggory)
        {
            MLSubcategory _MLSubcategory = new();
            DLSubcategory _DLSubcategory = new();
            DBReturnData _DBReturnData = new();
            try
            {
                _MLSubcategory = _DLSubcategory.DeleteSubCategory(_MLDeleteSubCateggory);
                _DBReturnData.Code = 200;
                _DBReturnData.Message = "";
                _DBReturnData.Retval = "SUCCESS";


            }
            catch (Exception ex)
            {
                _DBReturnData.Dataset = null;
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error" + ex.Message.ToString();
                _DBReturnData.Retval = null;

            }
            return new[] { _DBReturnData };


        }


        //[Route("GetSubCategoryThroughCategoryId")]
        //[HttpGet]
        //[JwtAuthorization(Roles = [Roles.Admin])]
        //public ActionResult<IEnumerable<DBReturnData>> GetSubCategoryThroughCategoryId(MLGetThroughCategoryId _MLGetThroughCategoryId)
        //{
        //    MLSubcategory _MLSubcategory = new();
        //    DLSubcategory _DLSubcategory = new();
        //    DBReturnData _DBReturnData = new();

        //    try
        //    {
        //        _MLSubcategory = _DLSubcategory.GetSubCategoryThroughCategoryId(_MLGetThroughCategoryId);
        //        _DBReturnData.Code = 200;
        //        _DBReturnData.Message = "";
        //        _DBReturnData.Retval = "SUCCESS";


        //    }
        //    catch (Exception ex)
        //    {
        //        _DBReturnData.Dataset = null;
        //        _DBReturnData.Code = 500;
        //        _DBReturnData.Message = "Internal Server Error" + ex.Message.ToString();
        //        _DBReturnData.Retval = null;

        //    }
        //    return new[] { _DBReturnData };

        //}

        [Route("GetSubCategoryThroughCategoryId")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> GetSubCategoryThroughCategoryId([FromBody] MLGetThroughCategoryId _MLGetThroughCategoryId)
        {
            if (_MLGetThroughCategoryId == null || _MLGetThroughCategoryId.Category_id == 0)
            {
                return BadRequest(new { Message = "Category_id cannot be null or zero." });
            }

            MLSubcategory _MLSubcategory = new();
            DLSubcategory _DLSubcategory = new();
            DBReturnData _DBReturnData = new();

            var subcategoryData = _DLSubcategory.GetSubCategoryThroughCategoryId(_MLGetThroughCategoryId);
            if (subcategoryData != null)
            {
                _MLSubcategory.SubCategoryListByCategory = subcategoryData.SubCategoryListByCategory;
            }

            try
            {
                if (_MLSubcategory.SubCategoryListByCategory != null && _MLSubcategory.SubCategoryListByCategory.Count > 0)
                {
                    _DBReturnData.Dataset = _MLSubcategory.SubCategoryListByCategory;
                    _DBReturnData.Code = 200;
                    _DBReturnData.Message = "SUCCESS";
                    _DBReturnData.Retval = "SUCCESS";
                }
                else
                {
                    _DBReturnData.Dataset = null;
                    _DBReturnData.Retval = "FAILED";
                    _DBReturnData.Message = "No subcategories found.";
                    _DBReturnData.Code = 404;
                }
            }
            catch (Exception ex)
            {
                _DBReturnData.Dataset = "FAILED";
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error: " + ex.Message;
                _DBReturnData.Retval = "FAILED";
            }
            return new[] { _DBReturnData };
        }


        [Route("GetSubCategoryById")]
        [HttpGet]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> GetSubCategoryById(MLGetThroughId _MLGetThroughId)
        {
            MLSubcategory _MLSubcategory = new();
            DLSubcategory _DLSubcategory = new();
            DBReturnData _DBReturnData = new();
            try
            {
                _MLSubcategory = _DLSubcategory.GetSubCategoryById(_MLGetThroughId);
                _DBReturnData.Code = 200;
                _DBReturnData.Message = "";
                _DBReturnData.Retval = "SUCCESS";
            }
            catch (Exception ex)
            {
                _DBReturnData.Dataset = null;
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error" + ex.Message.ToString();
                _DBReturnData.Retval = null;
            }
            return new[] { _DBReturnData };
        }
    }
}
