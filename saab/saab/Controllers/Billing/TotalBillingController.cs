using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using saab.Data;
using saab.Dto;
using saab.Model.Error;
using saab.Services.Billing;
using saab.Util.Enum;
using saab.Util.Project;
using saab.Util.ResponseDictionary;
using Swashbuckle.AspNetCore.Annotations;

namespace saab.Controllers.Billing
{
    [Route("api/v1/facturas/totales")]
    [ApiController]
    public class TotalBillingController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public TotalBillingController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Información de total de facturación por periodo.",
            Description = "Información de total de facturación por periodo anual o mensual de todos los proyectos.",
            OperationId = "Get",
            Tags = new[] { "Facturacion" })]
        [SwaggerResponse(200, "Success", typeof(TotalsProject))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public Task<ObjectResult> Get([BindRequired, FromQuery] string periodo)
        {
            try
            {
                var dictPeriod = DateUtil.GetDictPeriod(period: periodo);
                if (!string.IsNullOrEmpty(dictPeriod["year"]))
                {
                    var totalBilling = _billingService.GetInvoiceTotal(periodo);
                    HttpContext.Response.StatusCode = 200;
                    return Task.FromResult(StatusCode(StatusCodes.Status200OK,
                        JsonConvert.SerializeObject(totalBilling)));
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