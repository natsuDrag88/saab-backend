using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using saab.Dto;
using saab.Model.Error;
using saab.Services.Saving;
using saab.Util.Enum;
using saab.Util.Project;
using saab.Util.ResponseDictionary;
using Swashbuckle.AspNetCore.Annotations;

namespace saab.Controllers.Saving
{
    [Route("api/v1/ahorros/totales")]
    [ApiController]
    public class TotalSavingController : ControllerBase
    {
        private readonly ISavingService _savingService;

        public TotalSavingController(ISavingService savingService)
        {
            _savingService = savingService;
        }


        [HttpGet]
        [SwaggerOperation(
            Summary = "Información total de ahorros mensuales.",
            Description = "Información total de ahorros mensuales de todos los proyectos.",
            OperationId = "Get",
            Tags = new[] { "Ahorros" })]
        [SwaggerResponse(200, "Success", typeof(TotalsProject))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public Task<ObjectResult> Get([BindRequired, FromQuery] string periodo, [FromQuery] string proyecto, [FromQuery] string tipoOperacion)
        {
            try
            {
                var dictPeriod = DateUtil.GetDictPeriod(period: periodo);
                if (!string.IsNullOrEmpty(dictPeriod["year"]))
                {
                    var result = string.IsNullOrEmpty(proyecto)
                        ? _savingService.GetSavingTotal(period:periodo,typeProcess:tipoOperacion)
                        : _savingService.GetSavingTotal(period: periodo, rpu: proyecto,typeProcess:tipoOperacion);
                    HttpContext.Response.StatusCode = 200;
                    return Task.FromResult(StatusCode(StatusCodes.Status200OK, JsonConvert.SerializeObject(result)));
                }

                var resultDictionary = ResponseDictionary.GetDictionaryError400(description: MessagesRequest.ErrorPeriod);
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