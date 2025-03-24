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
        public ActionResult<DBReturnData> GetAllProducts()
        {
            try
            {
                DLProduct _DLProduct = new();
                DBReturnData _DBReturnData = _DLProduct.GetAllProducts();

                if (_DBReturnData.Dataset != null)
                {
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                    _DBReturnData.Message = "SUCCESS";
                    _DBReturnData.Retval = "SUCCESS";
                    _DBReturnData.Status = DBEnums.Status.SUCCESS;
                    return Ok(_DBReturnData);
                }
                else
                {
                    _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;
                    _DBReturnData.Message = "NOT_FOUND";
                    _DBReturnData.Retval = "NOT_FOUND";
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    return NotFound(_DBReturnData);
                }
            }
            catch (Exception ex)
            {
                new DALBASE().ErrorLog("GetAllProducts", "Controller", ex.ToString());

                return StatusCode(500, new DBReturnData
                {
                    Dataset = null,
                    Code = DBEnums.Codes.BAD_REQUEST,
                    Message = "Internal Server Error",
                    Retval = "FAILURE"
                });
            }
        }



        [Route("GetProductsByUserId")]
        [HttpPost]
        public ActionResult<DBReturnData> GetProductsByUserId(MLGetProductByUserId _MlGetProductByUserId)
        {
            try
            {
                DLProduct _DLProduct = new();
                DBReturnData _DBReturnData = _DLProduct.GetProductsByUserId(_MlGetProductByUserId);

                if (_DBReturnData.Dataset != null)
                {
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                    _DBReturnData.Message = "SUCCESS";
                    _DBReturnData.Retval = "SUCCESS";
                    _DBReturnData.Status = DBEnums.Status.SUCCESS;
                    return Ok(_DBReturnData);
                }
                else
                {
                    _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;
                    _DBReturnData.Message = "NOT_FOUND";
                    _DBReturnData.Retval = "NOT_FOUND";
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    return NotFound(_DBReturnData);
                }
            }
            catch (Exception ex)
            {
                new DALBASE().ErrorLog("GetAllProducts", "Controller", ex.ToString());

                return StatusCode(500, new DBReturnData
                {
                    Dataset = null,
                    Code = DBEnums.Codes.BAD_REQUEST,
                    Message = "Internal Server Error",
                    Retval = "FAILURE"
                });
            }
        }







        [Route("GetAllNewProdcutRequest")]
        [HttpGet]
        public ActionResult<DBReturnData> GetAllNewProdcutRequest()
        {
            try
            {
                DLProduct _DLProduct = new();
                DBReturnData _DBReturnData = _DLProduct.NewProductRequestBySeller();

                if (_DBReturnData.Dataset != null)
                {
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                    _DBReturnData.Message = "SUCCESS";
                    _DBReturnData.Retval = "SUCCESS";
                    _DBReturnData.Status = DBEnums.Status.SUCCESS;
                    return Ok(_DBReturnData);
                }
                else
                {
                    _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;
                    _DBReturnData.Message = "NOT_FOUND";
                    _DBReturnData.Retval = "NOT_FOUND";
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    return NotFound(_DBReturnData);
                }
            }
            catch (Exception ex)
            {
                new DALBASE().ErrorLog("GetAllProducts", "Controller", ex.ToString());

                return StatusCode(500, new DBReturnData
                {
                    Dataset = null,
                    Code = DBEnums.Codes.BAD_REQUEST,
                    Message = "Internal Server Error",
                    Retval = "FAILURE"
                });
            }
        }

        [Route("GetApprovedProducts")]
        [HttpGet]
        public ActionResult<DBReturnData> GetApprovedProducts()
        {
            try
            {
                DLProduct _DLProduct = new();
                DBReturnData _DBReturnData = _DLProduct.ApprovedProducts();

                if (_DBReturnData.Dataset != null)
                {
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                    _DBReturnData.Message = "SUCCESS";
                    _DBReturnData.Retval = "SUCCESS";
                    _DBReturnData.Status = DBEnums.Status.SUCCESS;
                    return Ok(_DBReturnData);
                }
                else
                {
                    _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;
                    _DBReturnData.Message = "NOT_FOUND";
                    _DBReturnData.Retval = "NOT_FOUND";
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    return NotFound(_DBReturnData);
                }
            }
            catch (Exception ex)
            {
                new DALBASE().ErrorLog("GetAllProducts", "Controller", ex.ToString());

                return StatusCode(500, new DBReturnData
                {
                    Dataset = null,
                    Code = DBEnums.Codes.BAD_REQUEST,
                    Message = "Internal Server Error",
                    Retval = "FAILURE"
                });
            }
        }

        [Route("GetDeniedProducts")]
        [HttpGet]
        public ActionResult<DBReturnData> GetDeniedProducts()
        {
            try
            {
                DLProduct _DLProduct = new();
                DBReturnData _DBReturnData = _DLProduct.DeniedProduct();

                if (_DBReturnData.Dataset != null)
                {
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                    _DBReturnData.Message = "SUCCESS";
                    _DBReturnData.Retval = "SUCCESS";
                    _DBReturnData.Status = DBEnums.Status.SUCCESS;
                    return Ok(_DBReturnData);
                }
                else
                {
                    _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;
                    _DBReturnData.Message = "NOT_FOUND";
                    _DBReturnData.Retval = "NOT_FOUND";
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    return NotFound(_DBReturnData);
                }
            }
            catch (Exception ex)
            {
                new DALBASE().ErrorLog("GetAllProducts", "Controller", ex.ToString());

                return StatusCode(500, new DBReturnData
                {
                    Dataset = null,
                    Code = DBEnums.Codes.BAD_REQUEST,
                    Message = "Internal Server Error",
                    Retval = "FAILURE"
                });
            }
        }



        [Route("InsertProduct")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin , Roles.Vendor])]
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


        //[Route("ProductToggleCertified")]
        //[HttpPut]
        //[JwtAuthorization(Roles = [Roles.Admin])]
        //public ActionResult<IEnumerable<DBReturnData>> ProductToggleCertified(MLToggleCertified _MLToggleCertified)
        //{
        //    DLProduct _DLProduct = new();
        //    DBReturnData _DBReturnData = new();
           
        //    try
        //    {

        //        _DBReturnData = _DLProduct.ProductToggleCertified(_MLToggleCertified);
        //        if (_DBReturnData.Message == "SUCCESS")
        //        {
        //            _DBReturnData.Status = DBEnums.Status.SUCCESS;
        //            _DBReturnData.Code = DBEnums.Codes.SUCCESS;
        //            _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
        //        }
        //        else
        //        {
        //            _DBReturnData.Status = DBEnums.Status.FAILURE;
        //            _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;
        //            _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
        //        }
   

        //    }
        //    catch (Exception ex)
        //    {


        //        _DBReturnData.Dataset = null;
        //        _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
        //        _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
        //        _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
        //    }

        //    return new[] { _DBReturnData };

        //}

        //[Route("ProductToggleStatus")]
        //[HttpPut]
        //[JwtAuthorization(Roles = [Roles.Admin])]
        //public ActionResult<IEnumerable<DBReturnData>> ProductToggleStatus(MLToggleStatus _MLToggleStatus)
        //{
        //    DLProduct _DLProduct = new();
        //    DBReturnData _DBReturnData = new();
        //    try
        //    {
        //        _DBReturnData = _DLProduct.ProductToggleStatus(_MLToggleStatus);
        //    }
        //    catch (Exception ex)
        //    {
        //        _DBReturnData.Dataset = null;
        //        _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
        //        _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
        //        _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
        //    }

        //    return new[] { _DBReturnData };

        //}




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
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() +ex.Message.ToString();
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();

            }
            return new[] { _DBReturnData };

        }

        #endregion InhouseProduct



        #region GET PRoduct by categroyID
        [Route("GetProductBycategory")]
        [HttpPost]
        public ActionResult<DBReturnData> GetProductByCategory(MLGetProrductByCategoryId mlGetProductByCategory)
        {
            DLProduct _DLProduct = new();
            DBReturnData _DBReturnData = new();

            try
            {
                _DBReturnData = _DLProduct.GetProductBycategory(mlGetProductByCategory);

                if (_DBReturnData != null && _DBReturnData.Code == DBEnums.Codes.SUCCESS)
                {
                    return Ok(_DBReturnData);
                }
                else
                {
                    return NotFound(new DBReturnData
                    {
                        Dataset = null,
                        Code = DBEnums.Codes.NOT_FOUND,
                        Message = "No product details found",
                        Retval = "NOT_FOUND"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new DBReturnData
                {
                    Dataset = null,
                    Code = DBEnums.Codes.INTERNAL_SERVER_ERROR,
                    Message = "An error occurred while fetching product details",
                    Retval = "FAILURE"
                });
            }
        }


        #endregion


        #region GET product complet info with productID

        //[Route("GetCompletProductDescription")]
        //[HttpPost]
        //public ActionResult<IEnumerable<DBReturnData>> GetCompletProductDescription(MLGetCompletProductDescription mLGetCompletProductDescription)
        //{
        //    MLProduct _MLProduct = new();
        //    DLProduct _DLProduct = new();
        //    DBReturnData _DBReturnData = new();


        //    try
        //    {
        //        _MLProduct.CompleteProductDescription = _DLProduct.GetCompletProductDescription(mLGetCompletProductDescription);
        //        if (_MLProduct.CompleteProductDescription.Count > 0)
        //        {
        //            _DBReturnData.Dataset = _MLProduct.CompleteProductDescription;
        //            _DBReturnData.Code = DBEnums.Codes.SUCCESS;
        //            _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
        //            _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();

        //        }
        //        else
        //        {
        //            _DBReturnData.Dataset = null;
        //            _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;
        //            _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
        //            _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
        //        }



        //    }
        //    catch (Exception ex)
        //    {

        //        _DBReturnData.Dataset = null;
        //        _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
        //        _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
        //        _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
        //    }
        //    return new[] { _DBReturnData };



        //}

        [Route("GetCompletProductDescription")]
        [HttpPost]
        public ActionResult<DBReturnData> GetCompletProductDescription(MLGetCompletProductDescription mLGetCompletProductDescription)
        {
            DLProduct _DLProduct = new();
            DBReturnData _DBReturnData = new();

            try
            {
                _DBReturnData = _DLProduct.GetCompletProductDescription(mLGetCompletProductDescription);

                if (_DBReturnData != null && _DBReturnData.Code == DBEnums.Codes.SUCCESS)
                {
                    return Ok(_DBReturnData);
                }
                else
                {
                    return NotFound(new DBReturnData
                    {
                        Dataset = null,
                        Code = DBEnums.Codes.NOT_FOUND,
                        Message = "No product details found",
                        Retval = "NOT_FOUND"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new DBReturnData
                {
                    Dataset = null,
                    Code = DBEnums.Codes.INTERNAL_SERVER_ERROR,
                    Message = "An error occurred while fetching product details",
                    Retval = "FAILURE"
                });
            }
        }



        #endregion
        [Route("ProductToggleCertificate")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<DBReturnData> ProductToggleCertification(MlProductToggleCertification mlProductToggleCertification)
        {
            DLProduct _DLProduct = new();
            DBReturnData _DBReturnData = new();
            try
            {

                _DBReturnData = _DLProduct.ProductToggleCertification(mlProductToggleCertification);


                if (_DBReturnData.Message == "SUCCESS")
                {
                    _DBReturnData.Status = DBEnums.Status.SUCCESS;
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                    _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
                }
                else
                {
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;
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

            return _DBReturnData ;
        }

        [Route("ProductToggleStatus")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<DBReturnData> ProductToggleStatus(MlProductToggleCertification mlProductToggleCertification)
        {
            DLProduct _DLProduct = new();
            DBReturnData _DBReturnData = new();
            try
            {

                _DBReturnData = _DLProduct.ProductToggleStatus(mlProductToggleCertification);



                if (_DBReturnData.Message == "SUCCESS")
                {
                    _DBReturnData.Status = DBEnums.Status.SUCCESS;
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                    _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
                }
                else
                {
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;
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

            return _DBReturnData;
        }
    }
}