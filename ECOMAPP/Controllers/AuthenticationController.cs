using Microsoft.AspNetCore.Mvc;

namespace ECOMAPP.Controllers
{
    public class AuthenticationController : Controller
    {



        public class RegistrationData
        {
            public string Firstname { get; set; } = string.Empty;
            public string Lastname { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
            public string StateId { get; set; } = string.Empty;
            public string CityId { get; set; } = string.Empty;
            public string CountryId { get; set; } = string.Empty;
            public string MobileNumber { get; set; } = string.Empty;
            public string EmailId { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;

        }
        public class AuthenticationDTO
        {

            public int Code { get; set; }
            public string Message { get; set; } = null;
            public string Retval { get; set; } = null;

            public string Token { get; set; }

            public class AuthenticationEntites
            {

                public string Desgination { get; set; } = null;
                public string DesignationName { get; set; } = null;
                public string DesignationId { get; set; } = null;
                public string UserId { get; set; } = null;
                public string FirstName { get; set; } = null;
                public string LastName { get; set; } = null;
                public string Status { get; set; } = null;


            }

            public class UserProfileEntites
            {
                public string ProfileImage { get; set; } = null;
                public string EmailId { get; set; } = null;
                public string JoiningDate { get; set; } = null;
                public string FirstName { get; set; } = null;
                public string LastName { get; set; } = null;
                public string DesignationName { get; set; } = null;
                public string DesignationDescription { get; set; } = null;
                public string CountryName { get; set; } = null;
                public string EmojiUTF { get; set; } = null;
                public string CurrencyName { get; set; } = null;
                public string StateName { get; set; } = null;
                public string CityName { get; set; } = null;
            }

            public List<AuthenticationDTO.UserProfileEntites> UserProfilesEntity { get; set; }
            public List<AuthenticationDTO.AuthenticationEntites> AuthenticationsList { get; geet; }

        }


    }
}
