#nullable enable
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using saab.Dto.Auth;
using saab.Model.Error;
using saab.Services.AuthenticationSaab;
using Swashbuckle.AspNetCore.Annotations;

namespace saab.Controllers.Authentication
{
    [Route("auth/autenticar")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationSaabService _authenticationService;

        public AuthenticationController(IConfiguration iConfig, IAuthenticationSaabService authenticationService)
        {
            _configuration = iConfig;
            _authenticationService = authenticationService;
        } 
        
        // GET: api/Authentication
        [HttpGet]
        [SwaggerOperation(
            Summary = "Validar credenciales del usuario",
            Description = "Este servicio valida las credenciales del usuario y regresa el token de autenticación si " +
                          "los datos son correctos.",
            OperationId = "Get",
            Tags = new[] {"Autenthication"})]
        [SwaggerResponse(200, "Success", typeof(ResultTokenAuth))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        [SwaggerResponse(401, "Unauthorized", type: typeof(ErrorResponse))]
        public async Task<object?> Get([FromQuery] InputAuth authenticationModel)
        {
            var dataRequest = _authenticationService.GetAuthenticationSaabRequest(
                autParams: authenticationModel, section: _configuration.GetSection(key: "Authentication"));

            using var http = new HttpClient();
            var result = await http.PostAsync(dataRequest.urlService, dataRequest.dataService);
            var contents = await result.Content.ReadAsStringAsync();
            
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var jsonValue = JObject.Parse(contents);;
                var tokenUser = _authenticationService.GenerateToken(username: authenticationModel.username, 
                    name: jsonValue["nombre"]?.ToString(), section: _configuration.GetSection(key: "TokenApplication"));
                return await Task.FromResult(StatusCode(StatusCodes.Status200OK, JsonConvert.SerializeObject(tokenUser)));
            }
            HttpContext.Response.StatusCode = 204;
            var resultAuthentication = new
            {
                code = 10204,
                mensaje = "Las credenciales ingresadas son incorrectas.",
                descripcion = JsonConvert.DeserializeObject<object>(contents)
            };
            return StatusCode(StatusCodes.Status401Unauthorized, JsonConvert.SerializeObject(resultAuthentication));
        }
    }
}