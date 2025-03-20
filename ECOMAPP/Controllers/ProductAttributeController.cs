using ECOMAPP.CommonRepository;
using ECOMAPP.DataLayer;
using ECOMAPP.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using static ECOMAPP.MiddleWare.AppEnums;
using static ECOMAPP.ModelLayer.MLProductattribute;

namespace ECOMAPP.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductAttributeController : Controller
    {
        [Route("GetAllAttribute")]
        [HttpGet]
    
        public ActionResult<IEnumerable<DBReturnData>> GetAllAttribute()
        {
            MLProductattribute _MLProductattribute = new();
            DLProductattribute _DLProductattribute = new();
            DBReturnData _DBReturnData = new();

            try
            {
                _MLProductattribute.ProductAttributeList = _DLProductattribute.GetAllAttribute();
                if( _MLProductattribute.ProductAttributeList.Count > 0)
                {
                    _DBReturnData.Dataset = _MLProductattribute.ProductAttributeList;
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                    _DBReturnData.Message =DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
                }
                else
                {
                    _DBReturnData.Dataset = null;
                    _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                    _DBReturnData.Message =DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
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


        [Route("InsertAttribute")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> InsertAttribute(MLInsertProductAttribute _MLInsertProductAttribute)
        {
            MLProductattribute _MLProductattribute = new();
            DLProductattribute _DLProductattribute = new();
            DBReturnData _DBReturnData = new();
            try
            {
                _DBReturnData = _DLProductattribute.InsertAttribute(_MLInsertProductAttribute);
                
            }
            catch (Exception ex)
            {
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.ToString();
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();

            }

            return new[] { _DBReturnData };
        }

        [Route("UpdateAttribute")]
        [HttpPut]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> UpdateAttribute(MLUpdateProductAttribute _MLUpdateProductAttribute)
        {
            MLProductattribute _MLProductattribute = new();
            DLProductattribute _DLProductattribute = new();
            DBReturnData _DBReturnData = new();
            try
            {
                _DBReturnData = _DLProductattribute.UpdateAttribute(_MLUpdateProductAttribute);
              
            }
            catch (Exception ex)
            {
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.ToString();
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();

            }

            return new[] { _DBReturnData };
        }


        [Route("DeleteAttribute")]
        [HttpDelete]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> DeleteAttribute(MLDeleteProductAttribute _MLDeleteProductAttribute)
        {
            MLProductattribute _MLProductattribute = new();
            DLProductattribute _DLProductattribute = new();
            DBReturnData _DBReturnData = new();
            try
            {
                _DBReturnData = _DLProductattribute.DeleteAttribute(_MLDeleteProductAttribute);
                
            }
            catch (Exception ex)
            {
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.ToString();
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();

            }

            return new[] { _DBReturnData };
        }

    }


}
