using static ECOMAPP.ModelLayer.MLAuthetication;
using System.Data;
using ECOMAPP.ModelLayer;
using ECOMAPP.CommonRepository;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

namespace ECOMAPP.DataLayer
{
    public class DLAuthentication:DALBASE
    {
        AuthenticationDTO authenticationDTO = new();
        DLAuthentication dlauthentication = new();
        DALBASE dalbase = new();

        #region Login 
        public AuthenticationDTO Login(string EmailId, string PhoneNumber, string Password)
        {
            authenticationDTO.AuthenticationsList = new List<AuthenticationDTO.AuthenticationEntites>();
            try
            {
                DataSet dataSet = new DataSet();    
                using (DBAccess Db = new DBAccess()) 
                {
                    Db.DBProcedureName = "SP_REGISTRATION";
                    Db.AddParameters("@Action", "LOGINUSER");
                    Db.AddParameters("@Email", EmailId);
                    Db.AddParameters("@PhoneNumber",PhoneNumber);
                    Db.AddParameters("@Password", ComputeSha256Hash(Password));
                    dataSet = Db.DBExecute();
                    Db.Dispose();
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


    }
}
