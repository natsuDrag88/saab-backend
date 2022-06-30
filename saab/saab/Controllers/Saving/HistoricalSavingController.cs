using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using saab.Dto;
using saab.Model.Error;
using saab.Services.Billing;
using saab.Services.Saving;
using saab.Util.Enum;
using saab.Util.Project;
using saab.Util.ResponseDictionary;
using Swashbuckle.AspNetCore.Annotations;

namespace saab.Controllers.Saving
{
    [Route("api/v1/ahorros/historico")]
    [ApiController]
    public class HistoricalSavingController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public HistoricalSavingController(IBillingService billingService)
        {
            _billingService = billingService;
        }


        [HttpGet]
        [SwaggerOperation(
            Summary = "",
            Description = "",
            OperationId = "Get",
            Tags = new[] { "Ahorros" })]
        [SwaggerResponse(200, "Success", typeof(TotalsProject))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public async Task<ObjectResult> Get([BindRequired, FromQuery] string proyecto,
            [BindRequired, FromQuery] string periodoIni,[BindRequired, FromQuery] string periodoFin)
        {
            try
            {
                string resultDictionary;
                if (!string.IsNullOrEmpty(periodoIni) && !string.IsNullOrEmpty(periodoFin))
                {
                    var resultSavingBill = _billingService.GetDataHistoricalSavingBill(project: proyecto,
                        periodStart: periodoIni,periodEnd: periodoFin);
                    if (resultSavingBill.Count > 0)
                    {
                        return await Task.FromResult(StatusCode(StatusCodes.Status200OK,
                            JsonConvert.SerializeObject(resultSavingBill)));
                    }

                    resultDictionary = ResponseDictionary.GetDictionaryError204();
                    return await Task.FromResult(StatusCode(StatusCodes.Status204NoContent, resultDictionary));
                }

                resultDictionary = ResponseDictionary.GetDictionaryError400(description: MessagesRequest.ErrorPeriod);
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