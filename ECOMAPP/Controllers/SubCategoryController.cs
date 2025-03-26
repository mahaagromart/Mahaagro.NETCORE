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
                    DBReturnData.Code = DBEnums.Codes.SUCCESS;
                    DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                    DBReturnData.Retval =DBEnums.Status.SUCCESS.ToString();

                }
                else
                {
                    DBReturnData.Dataset = null;
                    DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                    DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();


                }

            }
            catch (Exception ex)
            {
  
                DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                DBReturnData.Message = DBEnums.Status.FAILURE.ToString() +ex.ToString();
                DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();

            }

            return new[] { DBReturnData };

        }
        [Route("InsertSubCategory")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin , Roles.Vendor])]
        public ActionResult<IEnumerable<DBReturnData>> InsertSubCategory(MLInsertSubcategory _MLInsertSubcategory)
        {
            DLSubcategory _DLSubcategory = new();
            DBReturnData DBReturnData = new();
            try
            {
                DBReturnData = _DLSubcategory.InsertSubCategory(_MLInsertSubcategory);
            }
            catch (Exception ex)
            {
              
                DBReturnData.Code = DBEnums.Codes.SUCCESS;
                DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
                DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();

            }



            return new[] { DBReturnData };
        }


        [Route("UpdateSubCategory")]
        [HttpPut]
        [JwtAuthorization(Roles = [Roles.Admin , Roles.Vendor])]
        public ActionResult<IEnumerable<DBReturnData>> UpdateSubCategory(MLUpdateSubcategory _MLUpdateSubcategory)
        {
            DLSubcategory _DLSubcategory = new();
            DBReturnData _DBReturnData = new();
            try
            {
                _DBReturnData = _DLSubcategory.UpdateSubCategory(_MLUpdateSubcategory);
                _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();

            }
            catch (Exception ex)
            {
                
                _DBReturnData.Code = DBEnums.Codes.BAD_GATEWAY;
                _DBReturnData.Message =DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();

            }
            return new[] { _DBReturnData };
        }


        [Route("DeleteSubCategory")]
        [HttpDelete]
        [JwtAuthorization(Roles = [Roles.Admin , Roles.Vendor])]
        public ActionResult<IEnumerable<DBReturnData>> DeleteSubCategory(MLDeleteSubCateggory _MLDeleteSubCateggory)
        {
       
            DLSubcategory _DLSubcategory = new();
            DBReturnData _DBReturnData = new();
            try
            {
                    _DBReturnData = _DLSubcategory.DeleteSubCategory(_MLDeleteSubCateggory);
                if (_DBReturnData.Message == "SUCCESS")
               
                {
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                    _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();

                }
             
            }
            catch (Exception ex)
            {
      
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                _DBReturnData.Message = DBEnums.Status.FAILURE + ex.Message.ToString();
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();

            }
            return new[] { _DBReturnData };


        }


  

        [Route("GetSubCategoryThroughCategoryId")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin,Roles.Vendor])]
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
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                    _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
                }
                else
                {
                    _DBReturnData.Dataset = null;
                    _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                }
            }
            catch (Exception ex)
            {
                _DBReturnData.Dataset = "FAILED";
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                _DBReturnData.Message =DBEnums.Status.FAILURE  + ex.Message;
                _DBReturnData.Retval = DBEnums.Status.FAILURE + ex.Message;
            }
            return new[] { _DBReturnData };
        }


        [Route("GetSubCategoryById")]
        [HttpGet]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> GetSubCategoryById(MLGetThroughId _MLGetThroughId)
        {
            DLSubcategory _DLSubcategory = new();
            DBReturnData _DBReturnData = new();
            try
            {
                _DBReturnData = _DLSubcategory.GetSubCategoryById(_MLGetThroughId);
               
            }
            catch (Exception ex)
            {
                _DBReturnData.Dataset = null;
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                _DBReturnData.Message =DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
            }
            return new[] { _DBReturnData };
        }
    }
}
