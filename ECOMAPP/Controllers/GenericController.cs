using System.Reflection;
using ECOMAPP.DataLayer;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using static ECOMAPP.MiddleWare.AppEnums;
using static ECOMAPP.ModelLayer.MLFormHelper;

namespace ECOMAPP.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenericController : Controller
    {
        [Route("GetFaq")]
        [HttpPost]
       // [JwtAuthorization(Roles = [Roles.Admin,Roles.User,Roles.Vendor,Roles.OrderManager,Roles.HelpAndSupport,Roles.ReprotAnalysis])]
        public JsonResult GetFaq()
        {

            MlFormHelper MlFormHelper = new();
            DLFormHelper dLFormHelper = new();
            try
            {
                MlFormHelper = dLFormHelper.GetFaq();

            }
            catch (Exception ex)
            {
                MlFormHelper.Message = "Error: " + ex.Message;
                MlFormHelper.Code = 500;
                MlFormHelper.Retval = "Error";
            }
            
            return Json(MlFormHelper);
        }

    }
}
