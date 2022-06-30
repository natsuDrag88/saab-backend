using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using saab.Data;
using saab.Dto.Alerts;
using saab.Model.Error;
using saab.Services.Alerts;
using saab.Util.Enum;
using saab.Util.Project;
using saab.Util.ResponseDictionary;
using Swashbuckle.AspNetCore.Annotations;

namespace saab.Controllers.Alerts
{
    [Route("api/v1/alertas/detallado")]
    [ApiController]
    public class DetailAlertsController : ControllerBase
    {
        private readonly IDetailAlertService _alertDetailService;

        public DetailAlertsController(IDetailAlertService detailAlertService)
        {
            _alertDetailService = detailAlertService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Informacion detallada de las alertas.",
            Description = "Información detallada de las alertas del mes actual.",
            OperationId = "Get",
            Tags = new[] { "Alertas" })]
        [SwaggerResponse(200, "Success", typeof(AlertDetail))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public async Task<ObjectResult> Get([BindRequired, FromQuery] string periodo)
        {
            try
            {
                var dictPeriod = DateUtil.GetDictPeriod(period: periodo);
                if (!string.IsNullOrEmpty(dictPeriod["year"]))
                {
                    var listAlert = _alertDetailService.GetAlertDetail(periodo);
                    if (listAlert != null)
                    {
                        return await Task.FromResult(StatusCode(StatusCodes.Status200OK,
                            JsonConvert.SerializeObject(listAlert)));
                    }

                    var resultDictionary = ResponseDictionary.GetDictionaryError204();
                    return await Task.FromResult(StatusCode(StatusCodes.Status204NoContent, resultDictionary));
                }
                else
                {
                    var resultDictionary =
                        ResponseDictionary.GetDictionaryError400(description: MessagesRequest.ErrorPeriod);
                    return await Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, resultDictionary));
                }
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