using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

public class JwtAuthorizationAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _secretKey;
    public string[] Roles { get; set; }

    public JwtAuthorizationAttribute()
    {
        var conf = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                                              .AddJsonFile("appsettings.json")
                                              .Build();
        _secretKey = conf["AppConstants:JwtSecretKey"];
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        try
            {
            var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                context.Result = new JsonResult(new { Code = 401, Message = "Unauthorized Request", Retval = "Failed" })
                { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();
            var principal = ValidateJwtToken(token);
            if (principal == null)
            {
                context.Result = new JsonResult(new { Code = 401, Message = "Invalid token", Retval = "Failed" })
                { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }

            var designationName = principal.Claims.FirstOrDefault(c => c.Type == "DesignationName")?.Value;
            if (!string.IsNullOrEmpty(designationName) && Roles != null && !Roles.Contains(designationName))
            {
                context.Result = new JsonResult(new { Code = 401, Message = "Invalid token", Retval = "Failed" })
                { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }

            context.HttpContext.User = principal;
        }
        catch (Exception ex)
        {
            context.Result = new JsonResult(new { Code = 500, Message = ex.Message, Success = false })
            { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }

    private ClaimsPrincipal ValidateJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            return principal;
        }
        catch
        {
            return null;
        }
    }
}
