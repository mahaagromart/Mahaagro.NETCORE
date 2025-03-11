using ECOMAPP.CommonRepository;
using ECOMAPP.DataLayer;
using ECOMAPP.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using static ECOMAPP.MiddleWare.AppEnums;

namespace ECOMAPP.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class VarientsController : Controller
    {

        [Route("GetAllVarients")]
        [HttpGet]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> GetAllVarients()
        {
            MLVarients _MLVarients = new();
            DLVarients _DLVarients = new();
            DBReturnData _DBReturnData = new();

            _MLVarients.VarientsList = _DLVarients.GetAllVarients();
            try
            {
               
                if (_MLVarients.VarientsList.Count > 0)
                {
                    _DBReturnData.Dataset = _MLVarients.VarientsList;
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
            catch(Exception ex)
            {
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;

            }
            return new[] { _DBReturnData } ;


        }


  
        [Route("InsertVarients")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> InsertVarients()
        {
            MLVarients _MLVarients = new();
            DLVarients _DLVarients = new();
            DBReturnData _DBReturnData = new();

            _MLVarients = _DLVarients.InsertVarients();
            try
            {
       
                _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();

            }
            catch (Exception ex)
            {
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;

            }
            return new[] { _DBReturnData };


        }

        [Route("UpdateVarients")]
        [HttpPut]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> UpdateVarients()
        {
            MLVarients _MLVarients = new();
            DLVarients _DLVarients = new();
            DBReturnData _DBReturnData = new();

            _MLVarients = _DLVarients.UpdateVarients();
            try
            {

                _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();

            }
            catch (Exception ex)
            {
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_GATEWAY;

            }
            return new[] { _DBReturnData };


        }
        [Route("DeleteVarients")]
        [HttpDelete]
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> DeleteVarients()
        {
            MLVarients _MLVarients = new();
            DLVarients _DLVarients = new();
            DBReturnData _DBReturnData = new();

            _MLVarients = _DLVarients.DeleteVarients();
            try
            {

                _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();

            }
            catch (Exception ex)
            {
                _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_GATEWAY;

            }
            return new[] { _DBReturnData };


        }

        //END TODO YET TO IMPLEMET IT'S A WROMG SCHEMA

    }
}
