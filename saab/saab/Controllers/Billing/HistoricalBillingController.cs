using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using saab.Data;
using saab.Model;
using saab.Model.Error;
using saab.Services.Billing;
using saab.Util.Enum;
using saab.Util.Project;
using saab.Util.ResponseDictionary;
using Swashbuckle.AspNetCore.Annotations;

namespace saab.Controllers.Billing
{
    [Route("api/v1/facturas/historico")]
    [ApiController]
    public class HistoricalBillingController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public HistoricalBillingController(SaabContext context, IBillingService billingService)
        {
            _billingService = billingService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = ".",
            Description = ".",
            OperationId = "Get",
            Tags = new[] { "Proyectos" })]
        [SwaggerResponse(200, "Success", typeof(List<HistoricalBilling>))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public async Task<ObjectResult> Get([BindRequired, FromQuery] string rpu, [BindRequired, FromQuery] string periodoIni,
            [BindRequired, FromQuery] string periodoFin)
        {
            try
            {
                var dictPeriodFin = DateUtil.GetDictPeriod(period: periodoIni);
                var dictPeriodIni = DateUtil.GetDictPeriod(period: periodoFin);
                var listRpu = rpu.Split(",");
                if (!string.IsNullOrEmpty(dictPeriodIni["year"]) && !string.IsNullOrEmpty(dictPeriodFin["year"]))
                {
                    var historical = _billingService.GetHistoricalRpuBillings(listRpu: listRpu, periodIni: periodoIni, 
                        periodEnd:periodoFin);
                    if (historical.Count > 0)
                    {
                        return await Task.FromResult(StatusCode(StatusCodes.Status200OK,
                            JsonConvert.SerializeObject(historical)));
                    }
                    else
                    {
                        var resultDictionary = ResponseDictionary.GetDictionaryError204();
                        return await Task.FromResult(StatusCode(StatusCodes.Status204NoContent, resultDictionary));
                    }
                }
                else
                {
                    HttpContext.Response.StatusCode = 400;
                    var resultDictionary =
                        ResponseDictionary.GetDictionaryError400(description: MessagesRequest.ErrorPeriod);
                    return await Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, resultDictionary));
                }
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 400;
                var resultDictionary =
                    ResponseDictionary.GetDictionaryError400(description: MessagesRequest.ErrorPeriod,
                        error: e.Message);
                return await Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, resultDictionary));
            }
        }
    }
}