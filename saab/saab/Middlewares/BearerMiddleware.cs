using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using saab.Services.AuthenticationSaab;

namespace saab.Middlewares
{
    public class BearerAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public BearerAuthMiddleware(RequestDelegate next, IConfiguration iConfig)
        {
            _next = next;
            _configuration = iConfig;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string authHeader = httpContext.Request.Headers["Authorization"];

            if (authHeader != null)
            {
                var auth = authHeader.Split(new[] { ' ' })[1];
                var resultValidationToken = ValidateToken(token: auth,
                    section: _configuration.GetSection(key: "TokenApplication"));
                if (resultValidationToken)
                {
                    await _next(httpContext);
                }
                else
                {
                    httpContext.Response.StatusCode = 401;
                    httpContext.Response.ContentType = "application/json";
                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new
                    {
                        code = 10401,
                        message = "Usuario no valido.",
                        description = "El tipo de usuario no tiene permitido el acceso a la aplicación."
                    }));
                }
            }
            else
            {
                httpContext.Response.StatusCode = 400;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    code = 10400,
                    message = "El token es requerido.",
                    description = "No se ha proporcionado un token valido."
                }));
            }
        }

        private static bool ValidateToken(string token, IConfiguration section)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(section.GetValue<string>(key:"token"));
            var isValid = false;
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var username = jwtToken.Claims.First(x => x.Type == "unique_name").Value;
                isValid = !string.IsNullOrEmpty(username);
                return isValid;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return isValid;
            }
            
        }
    }
}