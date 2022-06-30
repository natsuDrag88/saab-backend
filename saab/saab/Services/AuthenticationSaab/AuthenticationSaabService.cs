using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using saab.Dto.Auth;
using saab.Model.Request;

namespace saab.Services.AuthenticationSaab
{
    public class AuthenticationSaabService : IAuthenticationSaabService
    {

        public DataRequest GetAuthenticationSaabRequest(InputAuth autParams, IConfiguration section)
        {
            var serviceUrl = section.GetValue<string>(key:"service_url");
            
            var query = new Dictionary<string, string>
            {
                ["CuentaUsuario"] = autParams.username,
                ["Contrasena"] = autParams.password
            };
            
            var json = JsonConvert.SerializeObject(query);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var dataRequest = new DataRequest
            {
                urlService = serviceUrl,
                dataService = data
            };
            
            return dataRequest;
        }

        public ResultTokenAuth GenerateToken(string username, string name, IConfiguration section)
        {
            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            var tokenKey = Encoding.ASCII.GetBytes(section.GetValue<string>(key:"token"));

            //3. Create JETdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username)
                    }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            var tokenUser = new ResultTokenAuth()
            {
                token = tokenHandler.WriteToken(token),
                username = username,
                name = name
            };
            return tokenUser;
        }
    }
}