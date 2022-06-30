using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using saab.Dto.Billing;
using saab.Model.Error;
using saab.Services.Billing;
using saab.Util.Enum;
using saab.Util.Project;
using saab.Util.ResponseDictionary;
using Swashbuckle.AspNetCore.Annotations;

namespace saab.Controllers.Billing
{
    
    [Route("api/v1/facturas/resumen_proyectos")]
    [ApiController]
    public class BillingSummary : ControllerBase
    {
        private readonly IBillingService _billingService;

        public BillingSummary(IBillingService billingService)
        {
            _billingService = billingService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Información de resumen de facturas.",
            Description = "Información de resumen de facturas por proyectos",
            OperationId = "Get",
            Tags = new[] { "Facturacion" })]
        [SwaggerResponse(200, "Success", typeof(BillingOverallSummary))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public Task<ObjectResult> Get([BindRequired, FromQuery] string periodo)
        {
            try
            {
                var dictPeriod = DateUtil.GetDictPeriod(period: periodo);
                if (!string.IsNullOrEmpty(dictPeriod["year"]))
                {

                    HttpContext.Response.StatusCode = 200;
                    var summary = _billingService.GetSummaryByProjects(periodo);
                    return Task.FromResult(StatusCode(StatusCodes.Status200OK, JsonConvert.SerializeObject(summary)));
                }
                else
                {
                    HttpContext.Response.StatusCode = 400;
                    var resultDictionary =
                        ResponseDictionary.GetDictionaryError400(description: MessagesRequest.ErrorPeriod);
                    return Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, resultDictionary));
                }
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

