using ECOMAPP.CommonRepository;
using ECOMAPP.DataLayer;
using ECOMAPP.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using static ECOMAPP.MiddleWare.AppEnums;
using static ECOMAPP.ModelLayer.MLSubcategory;
using static ECOMAPP.ModelLayer.MLSubsubcategory;

namespace ECOMAPP.Controllers
{
    [Route("[Controller]")]
    [ApiController]

    public class SubsubCategoryController : Controller
    {
        [Route("GetAllSubsubCategory")]
        [HttpGet]

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
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                    _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
                }
                else
                {
                    _DBReturnData.Dataset = null;
                    _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                }

            }
            catch (Exception ex)
            {

                _DBReturnData.Dataset = null;
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
            }

            return new[] { _DBReturnData };


        }
        [Route("InsertSubsubCategory")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin , Roles.Vendor])]
        public ActionResult<IEnumerable<DBReturnData>> InsertSubsubCategory(MLInsertsubsubcategory _MLInsertsubsubcategory)
        {
            DLSubsubcategory _DLSubsubcategory = new();
            DBReturnData _DBReturnData = new();


            try
            {
                _DBReturnData = _DLSubsubcategory.InsertSubsubCategory(_MLInsertsubsubcategory);

            }
            catch (Exception ex)
            {
                _DBReturnData.Dataset = null;
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;

            }
            return new[] { _DBReturnData };


        }


        [Route("UpdateSubsubCategory")]
        [HttpPut]
        [JwtAuthorization(Roles = [Roles.Admin, Roles.Vendor])]
        public ActionResult<IEnumerable<DBReturnData>> UpdateSubsubCategory(MLUpdateSubsubcategory _MLUpdateSubsubcategory)
        {
            DLSubsubcategory _DLSubsubcategory = new();
            DBReturnData _DBReturnData = new();
            try
            {
                _DBReturnData = _DLSubsubcategory.UpdateSubsubCategory(_MLUpdateSubsubcategory);

            }
            catch (Exception ex)
            {

                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;

            }
            return new[] { _DBReturnData };
        }


        [Route("DeleteSubsubCategory")]
        [HttpDelete]
        [JwtAuthorization(Roles = [Roles.Admin , Roles.Vendor])]
        public ActionResult<IEnumerable<DBReturnData>> DeleteSubsubCategory(MLDeletesubsubcategory _MLDeletesubsubcategory)
        {
            DLSubsubcategory _DLSubsubcategory = new();
            MLSubsubcategory _MLSubsubcategory = new();
            DBReturnData _DBReturnData = new();
            try
            {
                _DBReturnData = _DLSubsubcategory.DeleteSubsubCategory(_MLDeletesubsubcategory);


            }
            catch (Exception ex)
            {
                _DBReturnData.Dataset = null;
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;

            }
            return new[] { _DBReturnData };
        }


        //[Route("GetSubCategoryThroughCategoryId")]
        //[HttpPost]
        //[JwtAuthorization(Roles = [Roles.Admin, Roles.Vendor])]
        //public ActionResult<IEnumerable<DBReturnData>> GetSubCategoryThroughCategoryId([FromBody] MLSubsubcategoryBySubCategoryId _MLSubsubcategoryBySubCategoryId)
        //{
        //    if (_MLSubsubcategoryBySubCategoryId == null || _MLSubsubcategoryBySubCategoryId.id == 0)
        //    {
        //        return BadRequest(new { Message = "SubSubCategory cannot be null or zero." });
        //    }

        //    DLSubsubcategory _DLSubsubcategory = new();
        //    MLSubsubcategory _MLSubsubcategory = new();
        //    DBReturnData _DBReturnData = new();

        //    var subcategoryData = _DLSubsubcategory.GetSubSubCategoryBySubCategoryId(_MLSubsubcategoryBySubCategoryId);
        //    if (subcategoryData != null)
        //    {
        //        _MLSubsubcategory.SubsubCategoryList = subcategoryData.SubsubCategoryList;
        //    }

        //    try
        //    {
        //        if (_MLSubsubcategory.SubsubCategory != null && _MLSubsubcategory.SubsubCategoryList.Count > 0)
        //        {
        //            _DBReturnData.Dataset = _MLSubsubcategory.SubsubCategoryList;
        //            _DBReturnData.Code = DBEnums.Codes.SUCCESS;
        //            _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
        //            _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
        //        }
        //        else
        //        {
        //            _DBReturnData.Dataset = null;
        //            _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
        //            _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
        //            _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _DBReturnData.Dataset = "FAILED";
        //        _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
        //        _DBReturnData.Message = DBEnums.Status.FAILURE + ex.Message;
        //        _DBReturnData.Retval = DBEnums.Status.FAILURE + ex.Message;
        //    }
        //    return new[] { _DBReturnData };
        //}


        [Route("GetSubSubCategoryBySubCategoryId")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin, Roles.Vendor])]
        public ActionResult<IEnumerable<DBReturnData>> GetSubSubCategoryBySubCategoryId([FromBody] MLSubsubcategoryBySubCategoryId _MLSubsubcategoryBySubCategoryId)
        {
            DLSubsubcategory _DLSubsubcategory = new();
            if (_MLSubsubcategoryBySubCategoryId == null || _MLSubsubcategoryBySubCategoryId.id == 0)
            {
                return BadRequest(new { Message = "SubSubCategory cannot be null or zero." });
            }

            DBReturnData _DBReturnData = _DLSubsubcategory.GetSubSubCategoryBySubCategoryId(_MLSubsubcategoryBySubCategoryId);

            return new[] { _DBReturnData };
        }

    }



}