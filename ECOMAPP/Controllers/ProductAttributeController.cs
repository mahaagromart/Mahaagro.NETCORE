﻿using ECOMAPP.CommonRepository;
using ECOMAPP.DataLayer;
using ECOMAPP.ModelLayer;
using Microsoft.AspNetCore.Mvc;
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
                    _DBReturnData.Code = 200;
                    _DBReturnData.Message = "";
                    _DBReturnData.Retval = "SUCCESS";
                }
                else
                {
                    _DBReturnData.Dataset = null;
                    _DBReturnData.Code = 400;
                    _DBReturnData.Message = "internal server error due to";
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
        [Route("InsertAttribute")]
        [HttpPost]

        public ActionResult<IEnumerable<DBReturnData>> InsertAttribute(MLInsertProductAttribute _MLInsertProductAttribute)
        {
            MLProductattribute _MLProductattribute = new();
            DLProductattribute _DLProductattribute = new();
            DBReturnData _DBReturnData = new();
            try
            {
                _MLProductattribute = _DLProductattribute.InsertAttribute(_MLInsertProductAttribute);
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
        [Route("UpdateAttribute")]
        [HttpPut]
        public ActionResult<IEnumerable<DBReturnData>> UpdateAttribute(MLUpdateProductAttribute _MLUpdateProductAttribute)
        {
            MLProductattribute _MLProductattribute = new();
            DLProductattribute _DLProductattribute = new();
            DBReturnData _DBReturnData = new();
            try
            {
                _MLProductattribute = _DLProductattribute.UpdateAttribute(_MLUpdateProductAttribute);
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
        [Route("DeleteAttribute")]
        [HttpDelete]
     
        public ActionResult<IEnumerable<DBReturnData>> DeleteAttribute(MLDeleteProductAttribute _MLDeleteProductAttribute)
        {
            MLProductattribute _MLProductattribute = new();
            DLProductattribute _DLProductattribute = new();
            DBReturnData _DBReturnData = new();
            try
            {
                _MLProductattribute = _DLProductattribute.DeleteAttribute(_MLDeleteProductAttribute);
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
