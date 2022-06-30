using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using saab.Dto.Saving;
using saab.Model.Error;
using saab.Services.Concentrate;
using saab.Util.Enum;
using saab.Util.Project;
using saab.Util.ResponseDictionary;
using Swashbuckle.AspNetCore.Annotations;

namespace saab.Controllers.Concentrate
{
    [Route("api/v1/concentrado")]
    [ApiController]
    public class ConcentrateController : ControllerBase
    {
        private readonly IConcentrateService _concentrateService;

        public ConcentrateController(IConcentrateService concentrateService)
        {
            _concentrateService = concentrateService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "",
            Description = "",
            OperationId = "Get",
            Tags = new[] { "Concentrado" })]
        [SwaggerResponse(200, "Success", typeof(Average))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public async Task<ObjectResult> Get([BindRequired, FromQuery] string periodo)
        {
            try
            {
                var dictPeriod = DateUtil.GetDictPeriod(period: periodo);
                if (!string.IsNullOrEmpty(dictPeriod["year"]))
                {
                    var resultConcentrate = _concentrateService.GetConcentrate(dictPeriod: dictPeriod);
                    return await Task.FromResult(StatusCode(StatusCodes.Status200OK,
                        JsonConvert.SerializeObject(resultConcentrate)));
                }
                var resultDictionary =
                    ResponseDictionary.GetDictionaryError400(description: MessagesRequest.ErrorPeriod);
                return await Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, resultDictionary));

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