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
                _MLVarients.VarientsList = _DLVarients.GetAllVarients();
                if (_MLVarients.VarientsList.Count > 0)
                {
                    _DBReturnData.Dataset = _MLVarients.VarientsList;
                    _DBReturnData.Code = 200;
                    _DBReturnData.Message = "SUCCESS";
                    _DBReturnData.Retval = "SUCCESS";

                }
                else
                {
                    _DBReturnData.Dataset = null;
                    _DBReturnData.Code = 200;
                    _DBReturnData.Message = "FAILED";
                    _DBReturnData.Retval = "FAILED";
                }

            }
            catch(Exception ex)
            {
                _DBReturnData.Retval = "FAILED";
                _DBReturnData.Message = "internal server error due to" + ex.ToString();
                _DBReturnData.Code = 500;

            }
            return new[] { _DBReturnData } ;


        }


        //START TODO YET TO IMPLEMET IT'S A WROMG SCHEMA
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
       
                _DBReturnData.Code = 200;
                _DBReturnData.Message = "SUCCESS";
                _DBReturnData.Retval = "SUCCESS";

            }
            catch (Exception ex)
            {
                _DBReturnData.Retval = "FAILED";
                _DBReturnData.Message = "internal server error due to" + ex.ToString();
                _DBReturnData.Code = 500;

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

                _DBReturnData.Code = 200;
                _DBReturnData.Message = "SUCCESS";
                _DBReturnData.Retval = "SUCCESS";

            }
            catch (Exception ex)
            {
                _DBReturnData.Retval = "FAILED";
                _DBReturnData.Message = "internal server error due to" + ex.ToString();
                _DBReturnData.Code = 500;

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

                _DBReturnData.Code = 200;
                _DBReturnData.Message = "SUCCESS";
                _DBReturnData.Retval = "SUCCESS";

            }
            catch (Exception ex)
            {
                _DBReturnData.Retval = "FAILED";
                _DBReturnData.Message = "internal server error due to" + ex.ToString();
                _DBReturnData.Code = 500;

            }
            return new[] { _DBReturnData };


        }

        //END TODO YET TO IMPLEMET IT'S A WROMG SCHEMA

    }
}
