using System.Collections;
using System.Data;
using ECOMAPP.CommonRepository;
using ECOMAPP.DataLayer;
using ECOMAPP.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using static ECOMAPP.MiddleWare.AppEnums;
using static ECOMAPP.ModelLayer.MLProduct;

namespace ECOMAPP.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {



        [Route("GetAllProducts")]
        [HttpGet]
        public ActionResult<IEnumerable<DBReturnData>> GetAllProducts()
        {
            try
            {
                MLProduct _MLProduct = new();
                DLProduct _DLProduct = new();
                DBReturnData _DBReturnData = new();


                _MLProduct.ProductList = _DLProduct.GetAllProducts();


                if (_MLProduct.ProductList.Any())
                {
                    return Ok(new DBReturnData
                    {
                        Dataset = _MLProduct.ProductList,
                        Code = DBEnums.Codes.SUCCESS,
                        Message = DBEnums.Status.SUCCESS.ToString(),
                        Retval = DBEnums.Status.SUCCESS.ToString()
                    });
                }
                else
                {
                    return NotFound(new DBReturnData
                    {
                        Dataset = null,
                        Code = DBEnums.Codes.NOT_FOUND,
                        Message = DBEnums.Status.FAILURE.ToString(),
                        Retval = DBEnums.Status.FAILURE.ToString()
                    });
                }
            }
            catch (Exception ex)
            {

                new DALBASE().ErrorLog("GetAllProducts", "Controller", ex.ToString());

                return StatusCode(500, new DBReturnData
                {
                    Dataset = null,
                    Code = DBEnums.Codes.BAD_REQUEST,
                    Message = DBEnums.Status.FAILURE.ToString(),
                    Retval = DBEnums.Status.FAILURE.ToString()
                });
            }
        }

        [Route("InsertProduct")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin, Roles.Vendor])]
        public ActionResult<IEnumerable<DBReturnData>> InsertProduct(MlGetProduct _MlGetProduct)
        {
            MLProduct _MLProduct = new();
            DLProduct _DLProduct = new();
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {
                _DBReturnData = _DLProduct.InsertProduct(_MlGetProduct);

                if (_DBReturnData.Code == DBEnums.Codes.SUCCESS)
                {
                    _DBReturnData.Status = DBEnums.Status.SUCCESS;

                    _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
                }
                else
                {
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();

                }


            }
            catch (Exception ex)
            {
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
                _DBReturnData.Retval = null;
            }
            return new[] { _DBReturnData };

        }




        [Route("DeleteProduct")]
        [HttpDelete]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> DeleteProduct(MLDeleteProduct _MLDeleteProduct)
        {
            MLProduct _MLProduct = new();
            DLProduct _DLProduct = new();
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {

                _DBReturnData = _DLProduct.DeleteProduct(_MLDeleteProduct);

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


        [Route("ProductToggleCertified")]
        [HttpPut]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> ProductToggleCertified(MLToggleCertified _MLToggleCertified)
        {
            DLProduct _DLProduct = new();
            DBReturnData _DBReturnData = new();

            try
            {

                _DBReturnData = _DLProduct.ProductToggleCertified(_MLToggleCertified);


            }
            catch (Exception ex)
            {


                _DBReturnData.Dataset = null;
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
            }

            return new[] { _DBReturnData };

        }

        [Route("ProductToggleStatus")]
        [HttpPut]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> ProductToggleStatus(MLToggleStatus _MLToggleStatus)
        {
            DLProduct _DLProduct = new();
            DBReturnData _DBReturnData = new();
            try
            {
                _DBReturnData = _DLProduct.ProductToggleStatus(_MLToggleStatus);
            }
            catch (Exception ex)
            {
                _DBReturnData.Dataset = null;
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
            }

            return new[] { _DBReturnData };

        }




        #region InhouseProduct


        [Route("GetAllInhouseProducts")]
        [HttpGet]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> GetAllInhouseProducts(MLGetInhouseProduct _MLGetInhouseProduct)
        {
            MLProduct _MLProduct = new();
            DLProduct _DLProduct = new();
            DBReturnData _DBReturnData = new();


            try
            {
                _MLProduct.InhouseProductList = _DLProduct.GetAllInhouseProducts(_MLGetInhouseProduct);
                if (_MLProduct.InhouseProductList.Count > 0)
                {
                    _DBReturnData.Dataset = _MLProduct.InhouseProductList;
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

                _DBReturnData.Dataset = null;
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
            }
            return new[] { _DBReturnData };

        }




        [Route("UpdateInhouseProduct")]
        [HttpPut]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> UpdateInhouseProduct(MLUpdateInhouseProduct _MLUpdateInhouseProduct)
        {
            DLProduct _DLProduct = new();
            DBReturnData _DBReturnData = new();

            try
            {
                _DBReturnData = _DLProduct.UpdateInhouseProduct(_MLUpdateInhouseProduct);

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

        [Route("DeleteInhouseProduct")]
        [HttpDelete]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> DeleteInhouseProduct(MLDeleteInhouseProduct _MLDeleteInhouseProduct)
        {
            DLProduct _DLProduct = new();
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {
                _DBReturnData = _DLProduct.DeleteInhouseProduct(_MLDeleteInhouseProduct);

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

        #endregion InhouseProduct



        #region GET PRoduct by categroyID
        [Route("GetProductBycategory")]
        [HttpPost]
        public ActionResult<IEnumerable<DBReturnData>> GetProductBycategory(MLGetProrductByCategoryId mLGetProrductByCategoryId)
        {
            MLProduct _MLProduct = new();
            DLProduct _DLProduct = new();
            DBReturnData _DBReturnData = new();


            try
            {
                _MLProduct.productsLists = _DLProduct.GetProductBycategory(mLGetProrductByCategoryId);
                if (_MLProduct.productsLists.Count > 0)
                {
                    _DBReturnData.Dataset = _MLProduct.productsLists;
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                    _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();

                }
                else
                {
                    _DBReturnData.Dataset = null;
                    _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                }



            }
            catch (Exception ex)
            {

                _DBReturnData.Dataset = null;
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
            }
            return new[] { _DBReturnData };



        }


        #endregion



    }
}