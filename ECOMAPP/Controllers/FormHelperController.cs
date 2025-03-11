using System.Reflection;
using ECOMAPP.DataLayer;
using ECOMAPP.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using static ECOMAPP.ModelLayer.MLFormHelper;
using static ECOMAPP.ModelLayer.MLFormHelper.MlFormHelper;

namespace ECOMAPP.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FormHelperController : Controller
    {


        [Route("GetAllCountry")]
        [HttpGet]
        public ActionResult<MlFormHelper> GetAllCountry()
        {
            MlFormHelper MlFormHelper = new();
            DLFormHelper dLFormHelper = new();
            try
            {
                MlFormHelper = dLFormHelper.GetAllCountry();
            }
            catch (Exception ex)
            {
                MlFormHelper.Message = "Error: " + ex.Message;
                MlFormHelper.Code = 500;
                MlFormHelper.Retval = "Error";
            }            
            return Ok(MlFormHelper);

        }


        [Route("GetStatesByCountry")]
        [HttpPost]
        public ActionResult<MlFormHelper> GetStatesByCountry([FromBody] MlFormData data)
        {

            MlFormHelper MlFormHelper = new();
            DLFormHelper dLFormHelper = new();
            try
            {
                MlFormHelper = dLFormHelper.GetStatesByCountry(data.CountryId);
            }
            catch (Exception ex)
            {
                MlFormHelper.Message = "Error: " + ex.Message;
                MlFormHelper.Code = 500;
                MlFormHelper.Retval = "Error";

            }
            
            return Ok(MlFormHelper);

        }

        [Route("GetCityByState")]
        [HttpPost]
        public ActionResult<MlFormHelper> GetCityByState([FromBody] MlFormData data)
        {
            MlFormHelper MlFormHelper = new();
            DLFormHelper dLFormHelper = new();
            try
            {
                MlFormHelper = dLFormHelper.GetCityByState(data.StateId);
            }
            catch (Exception ex)
            {
                MlFormHelper.Message = "Error: " + ex.Message;
                MlFormHelper.Code = 500;
                MlFormHelper.Retval = "Error";
            }

            return Ok(MlFormHelper);
        }


    }
}
