using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using saab.Dto.Saving;
using saab.Model.Error;
using saab.Services.ElectricRates;
using saab.Util.Enum;
using saab.Util.Project;
using saab.Util.ResponseDictionary;
using Swashbuckle.AspNetCore.Annotations;

namespace saab.Controllers.ElectricRate
{
    [ApiController]
    public class ElectricRatesController : ControllerBase
    {
        private readonly IElectricRates _electricRates;

        public ElectricRatesController(IElectricRates electricRates)
        {
            _electricRates = electricRates;
        }

        [HttpGet]
        [Route("api/v1/cuadro_tarifario/catalogo/tarifas")]
        [SwaggerOperation(
            Summary = "",
            Description = "",
            OperationId = "Get",
            Tags = new[] { "Cuadro_Tarifario" })]
        [SwaggerResponse(200, "Success", typeof(Average))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public async Task<ObjectResult> GetRates()
        {
            try
            {
                var resultConcentrate = _electricRates.GetRates();
                return await Task.FromResult(StatusCode(StatusCodes.Status200OK,
                    JsonConvert.SerializeObject(resultConcentrate)));
            }
            catch (Exception e)
            {
                var resultDictionary =
                    ResponseDictionary.GetDictionaryError400(description: MessagesRequest.ErrorPeriod,
                        error: e.Message);
                return await Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, resultDictionary));
            }
        }

        [HttpGet]
        [Route("api/v1/cuadro_tarifario/catalogo/division")]
        [SwaggerOperation(
            Summary = "",
            Description = "",
            OperationId = "Get",
            Tags = new[] { "Cuadro_Tarifario" })]
        [SwaggerResponse(200, "Success", typeof(Average))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public async Task<ObjectResult> GetDivision()
        {
            try
            {
                var resultConcentrate = _electricRates.GetDivision();
                return await Task.FromResult(StatusCode(StatusCodes.Status200OK,
                    JsonConvert.SerializeObject(resultConcentrate)));
            }
            catch (Exception e)
            {
                var resultDictionary =
                    ResponseDictionary.GetDictionaryError400(description: MessagesRequest.ErrorPeriod,
                        error: e.Message);
                return await Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, resultDictionary));
            }
        }

        [HttpGet]
        [Route("api/v1/cuadro_tarifario")]
        [SwaggerOperation(
            Summary = "",
            Description = "",
            OperationId = "Get",
            Tags = new[] { "Cuadro_Tarifario" })]
        [SwaggerResponse(200, "Success", typeof(Average))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public async Task<ObjectResult> Get([BindRequired, FromQuery] string periodoIni,
            [BindRequired, FromQuery] string periodoFin, [BindRequired, FromQuery] string rpu)
        {
            try
            {
                //var resultConcentrate = _electricRates.GetElectricRates(periodStart: periodoIni, periodEnd: periodoFin,
                //    rate: tarifa, division: division);
                var resultConcentrate = _electricRates.GetElectricRatesRpu(periodStart: periodoIni, 
                    periodEnd: periodoFin, rpu: rpu);
                return await Task.FromResult(StatusCode(StatusCodes.Status200OK,
                    JsonConvert.SerializeObject(resultConcentrate)));
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