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
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                    _DBReturnData.Message =DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
                }
                else
                {
                    _DBReturnData.Dataset = null;
                    _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                }

            

            } catch (Exception ex)
            {
    
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                _DBReturnData.Message =DBEnums.Status.FAILURE.ToString() +ex.Message.ToString();
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();

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
 
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
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
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
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
       
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
            }

            return new[] { _DBReturnData };
        }
    }
}
