using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using saab.Data;
using saab.Dto.Project;
using saab.Model;
using saab.Model.Error;
using saab.Services.Clients;
using saab.Util.Enum;
using saab.Util.ResponseDictionary;
using Swashbuckle.AspNetCore.Annotations;

namespace saab.Controllers.Clients
{
    [Route("api/v1/clientes/lista")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IClientsService _clientsService;

        public ListController(SaabContext context, IClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Información de una lista de clientes.",
            Description = "Información de una lista de clientes por estado.",
            OperationId = "Get",
            Tags = new[] { "Clientes" })]
        [SwaggerResponse(200, "Success", typeof(List<ClientLightWeight>))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public Task<ObjectResult> Get([BindRequired, FromQuery] string estado)
        {
            try
            {
                if (!string.IsNullOrEmpty(estado))
                {
                    Enum.TryParse(estado, out Status statusCentroCarga);
                    var clients = _clientsService.GetListLightWeight(statusCentroCarga);
                    return Task.FromResult(
                        StatusCode(StatusCodes.Status200OK, JsonConvert.SerializeObject(clients)));
                }

                var resultDictionary = ResponseDictionary.GetDictionaryError400(description: MessagesRequest.Error);
                return Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, resultDictionary));
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 400;
                var resultDictionary =
                    ResponseDictionary.GetDictionaryError400(description: MessagesRequest.ErrorPeriod,
                        error: e.Message);
                return Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, resultDictionary));
            }
        }
    }
}