using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using saab.Dto;
using saab.Dto.Saving;
using saab.Model.Error;
using saab.Services.Billing;
using saab.Util.Enum;
using saab.Util.Project;
using saab.Util.ResponseDictionary;
using Swashbuckle.AspNetCore.Annotations;

namespace saab.Controllers.Saving
{
    [Route("api/v1/ahorros/promedio")]
    [ApiController]
    public class AverageSavingsController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public AverageSavingsController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "",
            Description = "",
            OperationId = "Get",
            Tags = new[] { "Ahorros" })]
        [SwaggerResponse(200, "Success", typeof(Average))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public async Task<ObjectResult> Get([BindRequired, FromQuery] string periodo, 
            [BindRequired, FromQuery] string tipoOperacion)
        {
            try
            {
                var dictPeriod = DateUtil.GetDictPeriod(period: periodo);
                if (!string.IsNullOrEmpty(dictPeriod["year"]))
                {
                    
                    var resultAverage = _billingService.GetAverageSaving(year: dictPeriod["year"], operation:tipoOperacion);
                    return await Task.FromResult(StatusCode(StatusCodes.Status200OK,
                        JsonConvert.SerializeObject(resultAverage)));
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