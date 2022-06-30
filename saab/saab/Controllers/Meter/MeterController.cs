using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using saab.Dto.Meter;
using saab.Dto.Saving;
using saab.Model.Error;
using saab.Services.Meter;
using saab.Util.Enum;
using saab.Util.ResponseDictionary;
using Swashbuckle.AspNetCore.Annotations;

namespace saab.Controllers.Meter
{
    [Route("api/v1/medidores")]
    [ApiController]
    public class MeterController : ControllerBase
    {
        private readonly IMeterService _meterService;

        public MeterController(IMeterService meterService)
        {
            _meterService = meterService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "",
            Description = "",
            OperationId = "Get",
            Tags = new[] { "Medidor" })]
        [SwaggerResponse(200, "Success", typeof(DataMeter))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public async Task<ObjectResult> Get([BindRequired, FromQuery] string centroCarga)
        {
            try
            {
                var resultListMeter = _meterService.GetMeter(centroCarga: centroCarga);
                if (resultListMeter.Count > 0)
                {
                    return await Task.FromResult(StatusCode(StatusCodes.Status200OK,
                        JsonConvert.SerializeObject(resultListMeter)));
                }
                return await Task.FromResult(StatusCode(StatusCodes.Status204NoContent,
                    JsonConvert.SerializeObject(resultListMeter)));
                
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