using static ECOMAPP.ModelLayer.MLAuthetication;
using System.Data;
using ECOMAPP.ModelLayer;
using ECOMAPP.CommonRepository;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using ECOMAPP.ModelLayer.EcommerceAPI.DTO;
using System.Diagnostics;
using Azure.Core;
using static ECOMAPP.MiddleWare.AppEnums;
using System.IdentityModel.Tokens.Jwt;


namespace ECOMAPP.DataLayer
{
    public class DLAuthentication:DALBASE
    {


        #region Login 
        public AuthenticationDTO Login(string EmailId, string PhoneNumber, string Password)
        {

            AuthenticationDTO authenticationDTO = new();
            authenticationDTO.AuthenticationsList = new List<AuthenticationDTO.AuthenticationEntites>();
            try
            {
                DataSet dataSet = new DataSet();    
                if((string.IsNullOrEmpty(PhoneNumber) || PhoneNumber == "") && !string.IsNullOrEmpty(EmailId) )
                {
                    using (DBAccess Db = new DBAccess())
                    {
                        Db.DBProcedureName = "SP_REGISTRATION";
                        Db.AddParameters("@Action", "LOGINUSEREEMAIL");
                        Db.AddParameters("@Email", EmailId);
                        Db.AddParameters("@Password", ComputeSha256Hash(Password));
                        dataSet = Db.DBExecute();
                        Db.Dispose();
                    }

                }else if( (string.IsNullOrEmpty(EmailId) || EmailId == "") && !string.IsNullOrEmpty(PhoneNumber))
                {
                    using (DBAccess Db = new DBAccess())
                    {
                        Db.DBProcedureName = "SP_REGISTRATION";
                        Db.AddParameters("@Action", "LOGINUSERPHONE");
                        Db.AddParameters("@PhoneNumber", PhoneNumber);
                        Db.AddParameters("@Password", ComputeSha256Hash(Password));
                        dataSet = Db.DBExecute();
                        Db.Dispose();
                    }
                }else if (!string.IsNullOrEmpty(EmailId) && !string.IsNullOrEmpty(PhoneNumber))
                {

                    using (DBAccess Db = new DBAccess())
                    {
                        Db.DBProcedureName = "SP_REGISTRATION";
                        Db.AddParameters("@Action", "LOGINUSEREEMAIL");
                        Db.AddParameters("@Email", EmailId);
                        Db.AddParameters("@Password", ComputeSha256Hash(Password));
                        dataSet = Db.DBExecute();
                        Db.Dispose();
                    }

                }
                else
                {
                    authenticationDTO.Code = 400;
                    authenticationDTO.Message = "User Not Exists";
                    authenticationDTO.Retval = "Failed";

                    return authenticationDTO;
                }
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    DataTable table = dataSet.Tables[0];

                    foreach (DataRow row in table.Rows)
                    {
                        if (Convert.ToString(row["RETVAL"]) == "UserNotVerified")
                        {
                            authenticationDTO.Code = 401;
                            authenticationDTO.Message = "User Not Verified";
                            authenticationDTO.Retval = "Failed";
                            return authenticationDTO;
                        }
                        else
                        {
                            authenticationDTO.AuthenticationsList.Add(
                                new AuthenticationDTO.AuthenticationEntites
                                {
                                    DesignationName = Convert.ToString(row["DesignationName"]),
                                    DesignationId = Convert.ToString(row["DesignationId"]),
                                    UserId = Convert.ToString(row["UserId"]),
                                    FirstName = Convert.ToString(row["FirstName"]),
                                    LastName = Convert.ToString(row["Lastname"])
                                }
                            );
                            authenticationDTO.Code = 200;
                            authenticationDTO.Message = "Success";
                            authenticationDTO.Retval = "Success";
                        }
                    }
                }
                else
                {
                    authenticationDTO.Code = 400;
                    authenticationDTO.Message = "User Not Exists";
                    authenticationDTO.Retval = "Failed";
                }

            }
            catch (Exception ex)
            {
                ErrorLog("Authentication", "Register", ex.ToString());
                authenticationDTO.Code = 400;
                authenticationDTO.Message = "User Not Exists";
                authenticationDTO.Retval = "Failed";


            }
            return authenticationDTO;
        }

        #endregion

        #region Registration

        public AuthenticationDTO Register(RegistrationData rdata)
        {

            AuthenticationDTO authenticationDTO = new();

            authenticationDTO.AuthenticationsList = new List<AuthenticationDTO.AuthenticationEntites>();

            // we need to return Exception immidiatly if any of rdata field is empty for safety in registration 

            var property = rdata.GetType().GetProperties();

            foreach (var data in property)
            {
                var value = data.GetValue(rdata) as string;
                if (string.IsNullOrEmpty(value))
                {
                    authenticationDTO.Code = 400;
                    authenticationDTO.Message = $"Empty {data.Name}";
                    authenticationDTO.Retval = "Failed";
                    return authenticationDTO;
                }

            }
            try
            {
                DataSet ret = new();
                using (DBAccess Db = new())
                {
                    Db.DBProcedureName = "SP_REGISTRATION";
                    Db.AddParameters("@Action", "REGISTERUSER");
                    Db.AddParameters("@Firstname", rdata.Firstname);
                    Db.AddParameters("@Lastname", rdata.Lastname);
                    Db.AddParameters("@Address", rdata.Address);
                    Db.AddParameters("@StateId", rdata.StateId);
                    Db.AddParameters("@CityId", rdata.CityId);
                    Db.AddParameters("@CountryId", rdata.CountryId);
                    Db.AddParameters("@Email", rdata.EmailId);
                    Db.AddParameters("@PhoneNumber", rdata.MobileNumber);
                    Db.AddParameters("@Password", ComputeSha256Hash(rdata.Password));
                    ret = Db.DBExecute();
                    Db.Dispose();

                }

                if (ret != null && ret.Tables.Count > 0)
                {
                    DataTable dtbl = ret.Tables[0];
                    foreach (DataRow row in dtbl.Rows)
                    {
                        string retval = row["Retval"]?.ToString() ?? "";
                        if (retval == "SUCCESS")
                        {
                            authenticationDTO.Message = "Success";
                            authenticationDTO.Retval = "UserRegistered";
                            authenticationDTO.Code = 200;
                        }
                        else if (retval == "EmailExists")
                        {
                            authenticationDTO.Message = "Failed";
                            authenticationDTO.Retval = "Email Exists";
                            authenticationDTO.Code = 422;
                        }
                    }

                }
                else
                {
                    authenticationDTO.Message = "Failed";
                    authenticationDTO.Retval = "FailedToRegister";
                    authenticationDTO.Code = 400;
                }

            }
            catch (Exception ex) 
            {
                authenticationDTO.Message = "Failed";
                authenticationDTO.Retval = "FailedToRegister";
                authenticationDTO.Code = 500;
                ErrorLog("register,","Authentication",ex.ToString());
            }
           

            return authenticationDTO;
        }

        #endregion

        #region VerifyEmail 

        public AuthenticationDTO VerifyEmail(MlVerifyEmail mdata)
        {

            AuthenticationDTO authenticationDTO = new();

            try
            {
                DataSet ret = new();
                using DBAccess Db = new();
                Db.DBProcedureName = "SP_OTPSERVICE";
                Db.AddParameters("@Action", mdata.Email == null? "" : "VerifyEmail");
                Db.AddParameters("@EmailId", mdata.Email ?? "");
                Db.AddParameters("@Otp", mdata.Otp ?? "");
                ret = Db.DBExecute();
                
                if (ret != null && ret.Tables.Count > 0)
                {
                    DataTable dataTable = ret.Tables[0];
                    foreach(DataRow row in dataTable.Rows)
                    {
                        string retval = row["RETVAL"]?.ToString() ?? "";
                        if (retval == "SUCCESS")
                        {
                            authenticationDTO.Code = 200;
                            authenticationDTO.Message = "EmailVerified";
                            authenticationDTO.Retval = "Success";
                        }
                        else
                        {
                            authenticationDTO.Code = 401;
                            authenticationDTO.Message = "Email Not Verified";
                            authenticationDTO.Retval = "Failed";

                        }

                    }

                }
                else
                {
                    authenticationDTO.Code = 400;
                    authenticationDTO.Message = "Failed to verify email";
                    authenticationDTO.Retval = "Failed";
                }

            }
            catch (Exception ex)
            {
                ErrorLog("Verify Email", "Authentication", ex.ToString());
                authenticationDTO.Code = 500;
                authenticationDTO.Message = "Internal Server Error";
                authenticationDTO.Retval = "Failed";
            }

            return authenticationDTO;
        }


        #endregion


        #region VerifyPhone 

        public AuthenticationDTO VerifyPhone(MlVerifyPhone mdata)
        {
            AuthenticationDTO authenticationDTO = new();

            try
            {
                DataSet ret = new();
                using DBAccess Db = new();
                Db.DBProcedureName = "SP_OTPSERVICE";
                Db.AddParameters("@Action", mdata.PhoneNumber == null ? "" : "VerifyPhone");
                Db.AddParameters("@PhoneNumber", mdata.PhoneNumber ?? "");
                Db.AddParameters("@Otp", mdata.Otp ?? "");
                ret = Db.DBExecute();

                if (ret != null && ret.Tables.Count > 0)
                {
                    DataTable dataTable = ret.Tables[0];
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string retval = row["RETVAL"]?.ToString() ?? "";
                        if (retval == "SUCCESS")
                        {
                            authenticationDTO.Code = 200;
                            authenticationDTO.Message = "PhoneVerified";
                            authenticationDTO.Retval = "Success";
                        }
                        else
                        {
                            authenticationDTO.Code = 401;
                            authenticationDTO.Message = "Phone Not Verified";
                            authenticationDTO.Retval = "Failed";

                        }

                    }

                }
                else
                {
                    authenticationDTO.Code = 400;
                    authenticationDTO.Message = "Failed to verify Phone";
                    authenticationDTO.Retval = "Failed";
                }

            }
            catch (Exception ex)
            {
                ErrorLog("Verify Email", "Authentication", ex.ToString());
                authenticationDTO.Code = 500;
                authenticationDTO.Message = "Internal Server Error";
                authenticationDTO.Retval = "Failed";
            }

            return authenticationDTO;
        }


        #endregion

        #region SendOtpPhone

        public AuthenticationDTO SendOtpEmail(MlSendOtpEmail mlSendOtpEmail)
        {
            AuthenticationDTO authenticationDTO = new AuthenticationDTO();

            try
            {
                DBSendEmailSE dALBASE = new();


                string OTP = GenerateRandomNumber();

                DataSet retval = new();
                using (DBAccess db = new())
                {
                    db.DBProcedureName = "SP_OTPSERVICE";
                    db.AddParameters("@Action", "InsertOTP");
                    db.AddParameters("@Otp", OTP);
                    db.AddParameters("@EmailId", mlSendOtpEmail.Email ?? "");
                    retval = db.DBExecute();
                    db.Dispose();
                }
                
                string FirstName = "";
                string LastName = "";
                
                if(retval != null && retval.Tables.Count > 0)
                {
                    string status = retval.Tables[0].Rows[0]["RETVAL"].ToString() ?? "";
                    if (status != "" && status == "SUCCESS")
                    {
                        FirstName = retval.Tables[0].Rows[0]["FirstName"].ToString() ?? "";
                        LastName = retval.Tables[0].Rows[0]["LastName"].ToString() ?? "";
                    }
                    else
                    {
                        authenticationDTO.Code = 400;
                        authenticationDTO.Retval = "Phone number or email not exists";
                        authenticationDTO.Message = "Otp Failed to Sent ";
                        return authenticationDTO;
                    }
                    

                }
                string MailBody = dALBASE.MailOtpBody(OTP, FirstName + " " + FirstName);
                bool Mailstatus = dALBASE.SendEmail(mlSendOtpEmail.Email??"", MailBody, "OTP For Validation");

                if (Mailstatus)
                {
                        authenticationDTO.Code = 200;
                        authenticationDTO.Retval = "Success";
                        authenticationDTO.Message = "Otp Sent Succesfully";
                }
                else
                {
                    using (DBAccess db = new())
                    {
                        db.DBProcedureName = "SP_OTPSERVICE";
                        db.AddParameters("@Action", "FallBack");
                        db.AddParameters("@EmailId", mlSendOtpEmail.Email??"");
                        db.DBExecute();
                        db.Dispose();
                    }
                    authenticationDTO.Code = 500;
                    authenticationDTO.Retval = "Failed";
                    authenticationDTO.Message = "Otp Failed to Sent ";

                }
                
            }
            catch (Exception ae)
            {
                ErrorLog("SendOtpEmail", "Authentication", ae.ToString());
                authenticationDTO.Code = 500;
                authenticationDTO.Retval = "Failed";
                authenticationDTO.Message = "Otp Failed to Sent ";

            }
            return authenticationDTO;


        }


        #endregion

        #region Forget password otp trigger

        public AuthenticationDTO ForgetPasswordOtpTrigger(MlSendOtpEmail mlSendOtpEmail)
        {

            AuthenticationDTO _authdto = new();
            DBSendEmailSE dALBASE = new();

            string Email = mlSendOtpEmail.Email ?? "";
            try
            {
                string OTP = GenerateRandomNumber();
                DataSet InsertOtp = new();
                using (DBAccess Db = new())
                {
                    Db.DBProcedureName = "SP_OTPSERVICE";
                    Db.AddParameters("@Action", "InsertOTP");
                    Db.AddParameters("@EmailId", Email);
                    Db.AddParameters("@Otp", OTP);
                    InsertOtp = Db.DBExecute();
                    Db.Dispose();
                }

                if(InsertOtp != null && InsertOtp.Tables.Count > 0)
                {
                    string? sp_otpserviceretval = InsertOtp.Tables[0].Rows[0]["RETVAL"].ToString();
                    string? FirstName, LastName;

                    if (sp_otpserviceretval == "SUCCESS")
                    {
                        FirstName = InsertOtp.Tables[0].Rows[0]["FirstName"].ToString() ?? "";
                        LastName = InsertOtp.Tables[0].Rows[0]["LastName"].ToString() ?? "";

                        string MailBody = dALBASE.MailOtpBody(OTP, FirstName + " " + FirstName);
                        bool Mailstatus = dALBASE.SendEmail(mlSendOtpEmail.Email ?? "", MailBody, "OTP For Forgetpassword");

                        if (Mailstatus)
                        {
                            _authdto.Code = 200;
                            _authdto.Retval = "Success";
                            _authdto.Message = "Otp Sent Succesfully";
                        }
                        else
                        {
                            using (DBAccess db = new())
                            {
                                db.DBProcedureName = "SP_OTPSERVICE";
                                db.AddParameters("@Action", "FallBack");
                                db.AddParameters("@EmailId", mlSendOtpEmail.Email ?? "");
                                db.DBExecute();
                                db.Dispose();
                            }
                            _authdto.Code = 500;
                            _authdto.Retval = "Failed";
                            _authdto.Message = "Otp Failed to Sent ";

                        }

                    }
                    else
                    {
                        _authdto.Code = 400;
                        _authdto.Retval = "Phone number or email not exists";
                        _authdto.Message = "Otp Failed to Sent ";
                        return _authdto;
                    }

                }
                else
                {
                    _authdto.Code = 500;
                    _authdto.Message = "Failed to send otp";
                    _authdto.Retval = "Failed";
                    _authdto.AuthenticationsList = null;

                }




            }
            catch (Exception s)
            {
                ErrorLog("ForgetPassword", "Authentication", s.ToString());
                _authdto.Code = 500;
                _authdto.Message = "Failed to send otp";
                _authdto.Retval = "Failed";
                _authdto.AuthenticationsList = null;
            }

            return _authdto;

        }




        #endregion

        #region Forgetpassword otpvalidate

        public AuthenticationDTO ForgetPasswordOtpValidate(string Email, string OTP, string Password)
        {
            AuthenticationDTO _objAuthenticatioDTO = new AuthenticationDTO();
            try
            {
                DALBASE dLAuthentication = new();

                DataSet spotpserviece = new();

                using(DBAccess Db = new())
                {
                    Db.DBProcedureName = "SP_OTPSERVICE";
                    Db.AddParameters("@Action", "PasswordResetOtpValidation");
                    Db.AddParameters("@EmailId",Email);
                    Db.AddParameters("@Otp", OTP);
                    Db.AddParameters("@Password", ComputeSha256Hash(Password));
                    spotpserviece = Db.DBExecute();
                    Db.Dispose();
                }

                if(spotpserviece != null && spotpserviece.Tables.Count > 0)
                {
                    string? retval = spotpserviece.Tables[0].Rows[0]["RETVAL"].ToString();

                    if (retval == "SUCCESS")
                    {
                        _objAuthenticatioDTO.Code = 200;
                        _objAuthenticatioDTO.Message = "Reseted Succesfully";
                        _objAuthenticatioDTO.Retval = "Success";
                    }
                    else if (retval == "OTPExpired")
                    {
                        _objAuthenticatioDTO.Code = 401;
                        _objAuthenticatioDTO.Message = "Otp Expired";
                        _objAuthenticatioDTO.Retval = "Fail";

                    }
                    else
                    {
                        _objAuthenticatioDTO.Code = 409;
                        _objAuthenticatioDTO.Message = "Expired";
                        _objAuthenticatioDTO.Retval = "Expired";

                    }
                }
                else
                {
                    _objAuthenticatioDTO.Code = 500;
                    _objAuthenticatioDTO.Message = "Internal Server Error";
                    _objAuthenticatioDTO.Retval = "Failed";
                }    
            
            }
            catch (Exception ed)
            {
                ErrorLog("ForgetPasswordOtpValidate", "Authentication", ed.ToString());
                _objAuthenticatioDTO.Code = 500;
                _objAuthenticatioDTO.Message = "Internal Server Error";
                _objAuthenticatioDTO.Retval = "Failed";
            }

            return _objAuthenticatioDTO;
        }

        #endregion

        #region GetUserProfile gives all detail of user based on the JWT token present in the AuthorizationParameter

        public AuthenticationDTO GetUserProfile(string UserId)
        {
            AuthenticationDTO objAuthDto = new();
            objAuthDto.UserProfilesEntity = [];

            try
            {
                DataSet spregistration = new();
                using(DBAccess Db = new())
                {
                    Db.DBProcedureName = "SP_REGISTRATION";
                    Db.AddParameters("@Action", "GETUSERPROFILE");
                    Db.AddParameters("@UserIdAct", UserId);
                    spregistration = Db.DBExecute();
                    Db.Dispose();
                }

                if(spregistration != null && spregistration.Tables.Count > 0)
                {
                    string? retval = spregistration.Tables[1].Rows[0]["RETVAL"].ToString();
                    if(retval == "SUCCESS")
                    {
                        foreach(DataRow row in spregistration.Tables[0].Rows)
                        {
                            objAuthDto.UserProfilesEntity.Add(
                                new AuthenticationDTO.UserProfileEntites
                                {
                                    ProfileImage = Convert.ToString(row["ProfileImage"]),
                                    EmailId = Convert.ToString(row["EmailId"]),
                                    JoiningDate = Convert.ToString(row["JoiningDate"]),
                                    FirstName = Convert.ToString(row["FirstName"]),
                                    LastName = Convert.ToString(row["LastName"]),
                                    DesignationName = Convert.ToString(row["DesignationName"]),
                                    DesignationDescription = Convert.ToString(row["DesignationDescription"]),
                                    CountryName = Convert.ToString(row["CountryName"]),
                                    EmojiUTF = Convert.ToString(row["EmojiUTF"]),
                                    CurrencyName = Convert.ToString(row["CurrencyName"]),
                                    StateName = Convert.ToString(row["StateName"]),
                                    CityName = Convert.ToString(row["CityName"]),
                                    PhoneNumber = Convert.ToString(row["PhoneNumber"])
                            });
                        }
                    }
                }
                else
                {
                    objAuthDto.Code = 409;
                    objAuthDto.Message = "No Values Values";
                    objAuthDto.Retval = "Failed to get user Profile";
                }
              
            }
            catch (Exception ed)
            {
                objAuthDto.Code = 500;
                objAuthDto.Message = "Internal Server Error";
                objAuthDto.Retval = "Failed to get users";

                ErrorLog("GetUserProfile", "AuthenticationDTO", ed.ToString());
            }
            return objAuthDto;
        }




        #endregion

        #region Update user profile


        public AuthenticationDTO UpdateUserProfile(string UserId, string ProfilePath)
        {
            AuthenticationDTO authenticationDTO = new AuthenticationDTO();
            // authenticationDTO.UserProfilesEntity = new List<AuthenticationDTO.UserProfileEntites>();

            try
            {
               
                DataSet spreg = new();
                using(DBAccess Db = new())
                {
                    Db.DBProcedureName = "SP_REGISTRATION";
                    //db.AddInParameter(command, "@Action", DbType.String, "UPDATEUSER");

                    Db.AddParameters("@Action", "UPDATEUSER");
                    Db.AddParameters("@UserIdAct", UserId);
                    Db.AddParameters("@UserProfilePath", ProfilePath);
                    spreg = Db.DBExecute();
                    Db.Dispose();
                }
                if (spreg != null && spreg.Tables.Count > 0)
                {
                    string? retval = spreg.Tables[0].Rows[0]["RETVAL"].ToString();
                    if(retval == "SUCCESS")
                    {
                        authenticationDTO.Code = 200;
                        authenticationDTO.Message = "Updated profile path";
                        authenticationDTO.Retval = "Success";
                    }
                }
                else
                {
                    authenticationDTO.Code = 401;
                    authenticationDTO.Message = "Updated profile path failed";
                    authenticationDTO.Retval = "failed";
                }
                
            }
            catch (Exception c)
            {
                authenticationDTO.Code = 500;
                authenticationDTO.Message = $"Internal Server Error {c.ToString()}";
                authenticationDTO.Retval = "Failed";

            }
            return authenticationDTO;
        }

        #endregion


        public string GenerateRandomNumber()
        {
            Random random = new();
            int number = random.Next(1000, 10000);
            return number.ToString();
        }

    }
}
