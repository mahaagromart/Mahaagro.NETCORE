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

                // Fetch products from DLProduct (data layer)
                _MLProduct.ProductList = _DLProduct.GetAllProducts();

                // Check if products exist
                if (_MLProduct.ProductList.Any())
                {
                    return Ok(new DBReturnData
                    {
                        Dataset = _MLProduct.ProductList,
                        Code = 200,
                        Message = "SUCCESS",
                        Retval = "SUCCESS"
                    });
                }
                else
                {
                    return NotFound(new DBReturnData
                    {
                        Dataset = null,
                        Code = 404,
                        Message = "No products found",
                        Retval = "FAILED"
                    });
                }
            }
            catch (Exception ex)
            {

                new DALBASE().ErrorLog("GetAllProducts", "Controller", ex.ToString());

                return StatusCode(500, new DBReturnData
                {
                    Dataset = null,
                    Code = 500,
                    Message = "Internal Server Error",
                    Retval = "FAILED"
                });
            }
        }

        [Route("InsertProduct")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> InsertProduct(MlGetProduct _MlGetProduct)
        {
            MLProduct _MLProduct = new();
            DLProduct _DLProduct = new();
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {

                _DBReturnData = _DLProduct.InsertProduct(_MlGetProduct);
               // if (_MLProduct.)
               if(_DBReturnData.Code == 200)
               {
                    _DBReturnData.Status = DBEnums.Status.SUCCESS;
                    _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
               }
               else
               {
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();

               }
            }
            catch (Exception ex)
            {
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Retval = null;
            }
            return new[] { _DBReturnData };

        }

        //[Route("InsertProduct")]
        //[HttpPost]
        //[JwtAuthorization(Roles = [Roles.Admin])]
        //public ActionResult<IEnumerable<DBReturnData>> InsertProduct(MlGetProduct _MlGetProduct)
        //{
        //    MLProduct _MLProduct = new();
        //    DLProduct _DLProduct = new();
        //    DBReturnData _DBReturnData = new();

        //    DataSet _DataSet = new();
        //    try
        //    {

        //        _MLProduct = _DLProduct.InsertProduct(_MlGetProduct);
        //        _DBReturnData.Code = 200;
        //        _DBReturnData.Message = "SUCCESS";
        //        _DBReturnData.Retval = "SUCCESS";

        //    }
        //    catch (Exception ex)
        //    {
        //        _DBReturnData.Code = 500;
        //        _DBReturnData.Message = "Internal Server Error";
        //        _DBReturnData.Retval = null;
        //    }
        //    return new[] { _DBReturnData };

        //}


        //[Route("UpdateProduct")]
        //[HttpPut]
        //[JwtAuthorization(Roles = [Roles.Admin])]
        //public ActionResult<IEnumerable<DBReturnData>> UpdateProduct(MlGetProduct _MlGetProduct)
        //{
        //    MLProduct _MLProduct = new();
        //    DLProduct _DLProduct = new();
        //    DBReturnData _DBReturnData = new();

        //    DataSet _DataSet = new();
        //    try
        //    {

        //        _MLProduct = _DLProduct.UpdateProduct(_MlGetProduct);
        //        _DBReturnData.Code = 200;
        //        _DBReturnData.Message = "SUCCESS";
        //        _DBReturnData.Retval = "SUCCESS";

                
        //    }
        //    catch (Exception ex)
        //    {
        //        _DBReturnData.Code = 500;
        //        _DBReturnData.Message = "Internal Server Error";
        //        _DBReturnData.Retval = null;
        //    }
        //    return new[] { _DBReturnData };

        //}

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

                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Retval = null;
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


                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Retval = null;
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
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Retval = null;
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
                    _DBReturnData.Code = 200;
                    _DBReturnData.Message = "SUCCESS";
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

        [Route("InsertInhouseProduct")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> InsertInhouseProduct(MLInsertInhouseProduct _MLInsertInhouseProduct)
        {
            DLProduct _DLProduct = new();
            DBReturnData _DBReturnData = new();

            try
            {

                _DBReturnData = _DLProduct.InsertInhouseProduct(_MLInsertInhouseProduct);
                _DBReturnData.Code = 200;
                _DBReturnData.Message = "SUCCESS";
                _DBReturnData.Retval = "SUCCESS";

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
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Retval = null;
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
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Retval = null;
            }
            return new[] { _DBReturnData };

        }

        #endregion InhouseProduct


    }
}
