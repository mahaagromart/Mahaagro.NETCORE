using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECOMAPP.CommonRepository;
using ECOMAPP.DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using static ECOMAPP.ModelLayer.MLAuthetication;

namespace ECOMAPP.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {

        DLAuthentication authentication = new DLAuthentication();
        DALBASE dalbase = new();

        #region Login 

        [Route("Login")]
        [HttpPost]
        public JsonResult Login([FromBody] MlLoginRequest loginRequest )
        {
            AuthenticationDTO result = new AuthenticationDTO();
            try
            {


                result = authentication.Login(loginRequest.EmailId,loginRequest.PhoneNumber, loginRequest.Password);

                if (result.Code == 200)
                {

                    IConfiguration conf = (new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build());
                    // private string DBConnectionString = conf["ConnectionStrings:DBCON"].ToString();

                    if (conf != null)
                    {
                        var secretKey = conf["AppConstants:JwtSecretKey"].ToString();
                        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                        var claims = new[]
                        {

                            new Claim("UserId", result.AuthenticationsList[0].UserId),
                            new Claim("DesignationId",result.AuthenticationsList[0].DesignationId),
                            new Claim("DesignationName",result.AuthenticationsList[0].DesignationName),
                        };

                        var token = new JwtSecurityToken(
                             claims: claims,
                             expires: DateTime.Now.AddDays(14),
                             signingCredentials: credentials
                        );


                        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                        result.Token = tokenString;

                    }
                }
                else
                {
                    result.Code = 401;
                    result.Message = "Login Failed phone number email or password possibly incorrect!!";
                    result.Retval = "Failed";
                }


            }
            catch (Exception ex)
            {
                DALBASE err = new();
                err.ErrorLog("Authentication", "Login", ex.ToString());

            };

            JsonResult json = Json(JsonConvert.SerializeObject(result));
            return Json(result);


        }

        #endregion

        #region Registrarion For User other higher access level cannot register here 

        [Route("Register")]
        [HttpPost]
        public JsonResult Register([FromBody] RegistrationData rdata)
        {
            try
            {
                AuthenticationDTO result = new();
                DLAuthentication dlauth = new();
                result = dlauth.Register(rdata); 

                if (result.Code == 200)
                {
                
                    JsonResult json = Json(JsonConvert.SerializeObject(result));
                    return Json(result);

                }
                else
                {
                    result.Code = 400;
                    result.Message = "Failed to Register";
                    JsonResult json = Json(JsonConvert.SerializeObject(result));
                    return Json(result);
                }


            }
            catch (Exception ex)
            {
                DALBASE dALBASE = new();
                dALBASE.ErrorLog("Register", "AuthController", ex.ToString());
                AuthenticationDTO res = new AuthenticationDTO
                {
                    Code = 500,
                    Message = "Internal Server Error"
                };

                return Json(res);

            }

        }

        #endregion

        #region Verify Email work in progress wait for changes on git


        [Route("VerifyEmail")]
        [HttpPost]
        public JsonResult VerifyEmail([FromBody] MlVerifyEmail verifyEmail)
        {
            try
            {

            }catch (Exception es)
            {
                dalbase.ErrorLog();
            }

        }

        #endregion

    }
}
