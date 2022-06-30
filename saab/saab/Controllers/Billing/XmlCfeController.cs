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
    [ApiController]
    public class XmlCfeController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public XmlCfeController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        [HttpGet]
        [Route("api/v1/facturas/xml")]
        [SwaggerOperation(
            Summary = "",
            Description = "",
            OperationId = "Get",
            Tags = new[] { "Facturacion" })]
        [SwaggerResponse(200, "Success", typeof(BillingOverallSummary))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public Task<ObjectResult> Get([BindRequired, FromQuery] string periodoIni,
            [BindRequired, FromQuery] string periodoFin, [BindRequired, FromQuery] string rpu)
        {
            try
            {
                if (!string.IsNullOrEmpty(periodoIni))
                {
                    HttpContext.Response.StatusCode = 200;
                    var result = _billingService.GetBillsCfeByPeriods(periodStart: periodoIni,
                        periodEnd: periodoFin, rpu: rpu);
                    return Task.FromResult(StatusCode(StatusCodes.Status200OK, JsonConvert.SerializeObject(result)));
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


        [HttpGet]
        [Route("api/v1/facturas/xml/opciones_desglose")]
        [SwaggerOperation(
            Summary = "",
            Description = "",
            OperationId = "Get",
            Tags = new[] { "Facturacion" })]
        [SwaggerResponse(200, "Success", typeof(BillingOverallSummary))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public Task<ObjectResult> GetBillItemizationOptions()
        {
            try
            {
                HttpContext.Response.StatusCode = 200;
                var result = _billingService.GetBillItemizationOptions();
                return Task.FromResult(StatusCode(StatusCodes.Status200OK, JsonConvert.SerializeObject(result)));
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