using ECOMAPP.CommonRepository;
using ECOMAPP.DataLayer;
using ECOMAPP.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using static ECOMAPP.MiddleWare.AppEnums;
using static ECOMAPP.ModelLayer.MLTestimonial;

namespace ECOMAPP.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class TestimonialController : Controller
    {
        [Route("GetAllTestimonial")]
        [HttpGet]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> GetAllTestimonial()
        {
            DLTestimonial _DLTestimonial = new();
            MLTestimonial _MLTestimonial = new();
            DBReturnData _DBReturnData = new();
            try
            {
                _MLTestimonial.TestimonialList = _DLTestimonial.GetAllTestimonial();
                if (_MLTestimonial.TestimonialList.Count > 0)
                {
                    _DBReturnData.Dataset = _MLTestimonial.TestimonialList[0];
                    _DBReturnData.Code = 200;
                    _DBReturnData.Message = "";
                    _DBReturnData.Retval = "SUCCESS";
                }
                else
                {
                    _DBReturnData.Dataset = null;
                    _DBReturnData.Retval = "FAILED";
                    _DBReturnData.Message = "internal server error due to";
                    _DBReturnData.Code = 500;
                }

            

            } catch (Exception ex)
            {
                _DBReturnData.Dataset = null;
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error";
                _DBReturnData.Retval = null;

            }

            return new[] { _DBReturnData };

        }


        [Route("InsertEcommerceTestimonial")]
        [HttpPost]
        public ActionResult<IEnumerable<DBReturnData>> InsertEcommerceTestimonial(MLInsertTestimonial _MLInsertTestimonial)
        {
            DLTestimonial _DLTestimonial = new();
            DBReturnData _DBReturnData = new();

            try
            {
                _DBReturnData = _DLTestimonial.InsertEcommerceTestimonial(_MLInsertTestimonial);

            }
            catch (Exception ex)
            {
                _DBReturnData.Dataset = null;
                _DBReturnData.Retval = "FAILED";
                _DBReturnData.Message = "internal server error due to";
                _DBReturnData.Code = 500;
            }

            return new[] { _DBReturnData };
        }


        [Route("UpdateEcommerceTestimonial")]
        [HttpPost]
        public ActionResult<IEnumerable<DBReturnData>> UpdateEcommerceTestimonial(MLUpdateTestimonial _MLUpdateTestimonial)
        {
            DLTestimonial _DLTestimonial = new();
            DBReturnData _DBReturnData = new();

            try
            {
                _DBReturnData = _DLTestimonial.UpdateEcommerceTestimonial(_MLUpdateTestimonial);

            }
            catch (Exception ex)
            {
                _DBReturnData.Dataset = null;
                _DBReturnData.Retval = "FAILED";
                _DBReturnData.Message = "internal server error due to";
                _DBReturnData.Code = 500;
            }

            return new[] { _DBReturnData };
        }


        [Route("DeleteEcommerceTestimonial")]
        [HttpDelete]
        public ActionResult<IEnumerable<DBReturnData>> DeleteEcommerceTestimonial(MLDeleteTestimonial _MLDeleteTestimonial)
        {
            DLTestimonial _DLTestimonial = new();
            MLTestimonial _MLTestimonial = new();
            DBReturnData _DBReturnData = new();

            try
            {
                _DBReturnData = _DLTestimonial.DeleteEcommerceTestimonial(_MLDeleteTestimonial);
              

            }
            catch (Exception ex)
            {
                _DBReturnData.Dataset = null;
                _DBReturnData.Retval = "FAILED";
                _DBReturnData.Message = "internal server error due to";
                _DBReturnData.Code = 500;
            }

            return new[] { _DBReturnData };
        }
    }
}
