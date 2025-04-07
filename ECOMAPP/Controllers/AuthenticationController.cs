
﻿using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using ECOMAPP.CommonRepository;
using ECOMAPP.DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using static ECOMAPP.ModelLayer.MLAuthetication;
using static ECOMAPP.MiddleWare.AppEnums;
using Microsoft.AspNetCore.Authorization;
using static ECOMAPP.ModelLayer.MLAuthetication.AuthenticationDTO;


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
        public JsonResult Login([FromBody] MlLoginRequest loginRequest)
        {
            AuthenticationDTO result = new AuthenticationDTO();
            try
            {

                result = authentication.Login(loginRequest.EmailId??"", loginRequest.PhoneNumber ?? "", loginRequest.Password ?? "");

                if (result.Code == 200)
                {

                    IConfiguration conf = (new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build());
                    // private string DBConnectionString = conf["ConnectionStrings:DBCON"].ToString();

                    if (conf != null)
                    {
                        var secretKey = conf["AppConstants:JwtSecretKey"]?.ToString();
                        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey??""));
                        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                        if (result.AuthenticationsList != null && result.AuthenticationsList.Count>0)
                        {

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
                }
                


            }
            catch (Exception ex)
            {
                DALBASE err = new();
                err.ErrorLog("Authentication", "Login", ex.ToString());
                result.Code = 500;
                result.Message = "Internal Server Error";
                result.Retval = "Failed";
                result.AuthenticationsList = [];
            };

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

        #region Verify Email {send the email and the otp to this endoitnt after triggering send email for email verification}


        [Route("VerifyEmail")]
        [HttpPost]
        public JsonResult VerifyEmail([FromBody] MlVerifyEmail verifyEmail)
        {
            AuthenticationDTO result = new();
            DLAuthentication dlauth = new();
            try
            {
                result = dlauth.VerifyEmail(verifyEmail);


            }
            catch (Exception es)
            {
                result.Code = 500;
                result.Message = "Failed";
                result.Retval = "Failed";
                dalbase.ErrorLog("VerifyEmail","authentication",es.ToString());
            }
            return Json(result);
        }

        #endregion


        #region Phone Verification {same as verify email but for phone number}
        //Create Mail Verification

        [Route("VerifyPhone")]
        [HttpPost]
        public JsonResult VerifyPhone([FromBody] MlVerifyPhone data)
        {

            AuthenticationDTO result = new();
            DLAuthentication dlauth = new();
            try
            {
                result = dlauth.VerifyPhone(data);


            }
            catch (Exception es)
            {
                result.Code = 500;
                result.Message = "Failed";
                result.Retval = "Failed";
                dalbase.ErrorLog("VerifyEmail", "authentication", es.ToString());
            }
            return Json(result);

        }

        #endregion

        #region Verify Email work in progress wait for changes on git


        //[Route("VerifyEmail")]
        //[HttpPost]
        //public JsonResult VerifyEmail([FromBody] MlVerifyEmail verifyEmail)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception es)
        //    {
        //        dalbase.ErrorLog();
        //    }

        //}

        #endregion



        

        #region send otp Email {send otp on the registerd users email id for thier verificatoion

        [Route("SendOtpEmail")]
        [HttpPost]
        public JsonResult SendOtpEmail([FromBody] MlSendOtpEmail mlSendOtpEmail)
        {
            AuthenticationDTO objAuthDTO = new AuthenticationDTO();
            DLAuthentication dlauth = new();
            try
            {
               objAuthDTO = dlauth.SendOtpEmail(mlSendOtpEmail);

            }
            catch (Exception ex)
            {
                objAuthDTO.Code = 500;
                objAuthDTO.Retval = "Internal server error";
                objAuthDTO.Message = "Unknown Exception on server";
                dlauth.ErrorLog("VerifyEmail ", "Authentication", ex.ToString());
            }
           
            return Json(objAuthDTO);
            

        }

        #endregion

        #region Forgetpassword otp trigger

        [Route("ForgetPasswordOtpTrigger")]
        [HttpPost]
        public JsonResult ForgetPasswordOtpTrigger([FromBody] MlSendOtpEmail mlSendOtpEmail)
        {
            AuthenticationDTO objAuthDTO = new();
            DLAuthentication dlauth = new();
            try
            {
                 objAuthDTO = dlauth.ForgetPasswordOtpTrigger(mlSendOtpEmail);

            }
            catch (Exception ed)
            {
                dlauth.ErrorLog("VerifyEmail ", "Authentication", ed.ToString());
                objAuthDTO.Code = 500;
                objAuthDTO.Message = "Internal Server Error";
                objAuthDTO.Retval = "Internal server error";
                objAuthDTO.AuthenticationsList = null;
            }

            return Json(objAuthDTO);
        

        }

        #endregion

        #region ForgetPasswordOtpValidate
        [Route("ForgetPasswordOtpValidate")]
        [HttpPost]
        public JsonResult ForgetPasswordOtpValidate([FromBody] MlForgetPasswordOtpValidate mlForgetPasswordOtpValidate)
        {

            AuthenticationDTO objAuthDTO = new();
            DLAuthentication dLAuthentication = new();
            try
            {
                objAuthDTO = dLAuthentication.ForgetPasswordOtpValidate(mlForgetPasswordOtpValidate.Email, mlForgetPasswordOtpValidate.OTP, mlForgetPasswordOtpValidate.Password);

            }
            catch (Exception ed)
            {
                dLAuthentication.ErrorLog("VerifyEmail ", "Authentication", ed.ToString());
                objAuthDTO.Code = 500;
                objAuthDTO.Message = "Internal Server Error";
                objAuthDTO.Retval = "Failed";
            }
            //return Json(new { Code = 500, Status = "Failed to Authenticate" });
            return Json(objAuthDTO);
            //return Json(authenticationBAL.VerifyEmail(Email));


        }


        #endregion

        #region getuserprofile
        [Route("GetUserProfile")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin, Roles.Vendor, Roles.User, Roles.ReprotAnalysis])]
        public JsonResult GetUserProfile()
        {

            AuthenticationDTO objauthdto = new();
            DLAuthentication dLAuthentication = new();
            string? JwtToken = Request.Headers.Authorization;
            if (string.IsNullOrEmpty(JwtToken))
            {
                objauthdto.Code = 401;
                objauthdto.Message = "Authorization header is missing";
                return Json(objauthdto);

            }
            JwtToken = JwtToken["Bearer ".Length..].Trim();
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(JwtToken);
            var tokenS = jsonToken as JwtSecurityToken;

            var UserIdH = tokenS?.Claims.First(claim => claim.Type == "UserId")?.Value;

            if (UserIdH == "" || UserIdH == null)
            {
                objauthdto.Code = 401;
                objauthdto.Retval = "access denied";
                objauthdto.Message = "permission denied";
                return Json(objauthdto);
            }

            try
            {
                objauthdto = dLAuthentication.GetUserProfile(UserIdH);
                objauthdto.Code = 200;
                objauthdto.Message = "Sucess";
                objauthdto.Retval = "Sucess";
            }
            catch (Exception a)
            {
                objauthdto.Code = 500;
                objauthdto.Retval = "Internal Server Error";
                objauthdto.Message = "Internal server error";
                dLAuthentication.ErrorLog("getuserProfile ", "Authentication", a.ToString());
            }
            return Json(objauthdto);

        }
        #endregion

        //READ THIS FOR ADDRESS
        //THER ARE 3 ADDRESS TOTAL 
        //STARTING FROM ADDRESS ADDRESSONE ADDRESSTWO
        //THER ARE 3 PINCODE TOTAL
        //STARTING FROM PINCODE PINCODE1 PINCODE2
        //AND A SELECTED INDEX IT WILL ACT AS CURRENT SELECTED ADDRESS FOR THE USER WITH THE PINCODE
        //LETS ASSUME WE SELECT INDEX 0 FROM INDEXS 0-3 
        //IF THE SELECTED INDEX IS 0 THEN THE 
        //ADDRESS WILL BE THE DEFAULT AND PINCODE WILL BE THE DEFAULT
        //IF THE SELECTED INDEX IS 1 THEN THE
        //ADDRESSONE WILL BE THE DEFAULT AND PINCODE1 WILL BE THE DEFAULT

        //WE HAVE THIS METHODS TO INTERACT WITH THIS
        //UPDATEADDRESSINDEX -- change the index of current address
        //addnewaddress_pincode -- add new address with pincode this will be appended to address if address is filled then addressone will be occupied if ran out of space then no mercy
        //editaddress_pincode -- provide the updateaddressindex and fill the values to update
        //

        [Route("updateaddressindex")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin, Roles.User, Roles.Vendor])]
        public ActionResult<IEnumerable<DBReturnData>> updateaddressindex([FromBody] UpdateAddressIndex updateAddressIndex)
        {
            string JwtToken = Request.Headers["Authorization"];
            JwtToken = JwtToken!["Bearer ".Length..].Trim();
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(JwtToken);
            var tokenS = jsonToken as JwtSecurityToken;
            var UserId = tokenS?.Claims.First(claim => claim.Type == "UserId").Value;
            DLAuthentication dLAuthentication = new();
            return new[] { dLAuthentication.updateaddressindex(updateAddressIndex,UserId ?? "")};
        }


        [Route("EditaddressPincode")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin, Roles.User, Roles.Vendor])]
        public ActionResult<IEnumerable<DBReturnData>> EditaddressPincode([FromBody] EditaddressPincode editaddressPincode)
        {
            string JwtToken = Request.Headers["Authorization"];
            JwtToken = JwtToken!["Bearer ".Length..].Trim();
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(JwtToken);
            var tokenS = jsonToken as JwtSecurityToken;
            var UserId = tokenS?.Claims.First(claim => claim.Type == "UserId").Value;
            DLAuthentication dLAuthentication = new();
            return new[] { dLAuthentication.EditaddressPincode(editaddressPincode,UserId ?? "")};
        }




        #region update user profile

        [Route("UpdateUserProfile")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin, Roles.User, Roles.Vendor ])]
        public JsonResult UpdateUserProfile([FromBody] UpdateUserProfile updateUserProfile)
        {
            DLAuthentication dLAuthentication = new();
            AuthenticationDTO objauthdto = new();
            string? JwtToken = Request.Headers["Authorization"];
            if (JwtToken == null)
            {
                objauthdto.Code = 401;
                objauthdto.Message = "Invalid parameters";
                objauthdto.Retval = "Failed";
                return Json(objauthdto);
            } 
            JwtToken = JwtToken["Bearer ".Length..].Trim();
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(JwtToken);
            var tokenS = jsonToken as JwtSecurityToken;
            var UserId = tokenS?.Claims.First(claim => claim.Type == "UserId").Value;
            
            if (UserId == "" || UserId == null)
            {
                objauthdto.Code = 401;
                objauthdto.Retval = "access denied";
                objauthdto.Message = "permission denied";
                return Json(objauthdto);

            }
            try
            {
                objauthdto = dLAuthentication.UpdateUserProfile(UserId, updateUserProfile.ProfilePath);

            }
            catch (Exception ex)
            {
                objauthdto.Code = 500;
                objauthdto.Retval = "Internal Server Error";
                objauthdto.Message = "Unexpected Internal server error";
                dLAuthentication.ErrorLog("UpdateUserProfile", "authentication", ex.ToString());
            }
            return Json(objauthdto);

        }






        #endregion


    }
}