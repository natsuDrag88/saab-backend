using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace saab.Services.ValidateToken
{

    public class ValidateTokenService
    {
        private readonly IConfiguration _configuration;
        private bool _validUser;
        private string _contents;
        
        public ValidateTokenService(IConfiguration iConfig)  
        {  
            _configuration = iConfig;  
        } 

        public async Task<Tuple<bool, string>> ValidateBearerToken(string token)
        {
            IConfiguration section = _configuration.GetSection(key: "ValidateToken");
            var serviceUrl = section.GetValue<string>(key:"service_url");
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                try
                {
                    var result = await http.GetAsync(serviceUrl);
                    _contents = await result.Content.ReadAsStringAsync();
                    _validUser = result.StatusCode == HttpStatusCode.OK;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
            }
            return new Tuple<bool, string>(_validUser, _contents);
        }
    }
}