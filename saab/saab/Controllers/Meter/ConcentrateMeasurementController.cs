using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using saab.Dto.Concentrate;
using saab.Model.Error;
using saab.Services.Concentrate;
using saab.Util.Enum;
using saab.Util.ResponseDictionary;
using Swashbuckle.AspNetCore.Annotations;

namespace saab.Controllers.Meter
{
    [Route("api/v1/medidores/mediciones")]
    [ApiController]
    public class ConcentrateMeasurementController : ControllerBase
    {
        private readonly IConcentrateService _concentrateService;
        
        public ConcentrateMeasurementController(IConcentrateService concentrateService)
        {
            _concentrateService = concentrateService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "",
            Description = "",
            OperationId = "Get",
            Tags = new[] { "Medidor" })]
        [SwaggerResponse(200, "Success", typeof(ConcentrateTypePeriod))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public async Task<ObjectResult> Get([FromQuery] InputConcentrateTypePeriod inputConcentrate)
        {
            try
            {
                var resultConcentrateTypePeriod = _concentrateService.GetConcentrateTypePeriod(
                    inputConcentrate: inputConcentrate);
                return await Task.FromResult(StatusCode(StatusCodes.Status200OK,
                    JsonConvert.SerializeObject(resultConcentrateTypePeriod)));
            }
            catch (Exception e)
            {
                var resultDictionary =
                    ResponseDictionary.GetDictionaryError400(description: MessagesRequest.ErrorPeriod,
                        error: e.Message);
                return await Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, resultDictionary));
            }
        }
    }
}