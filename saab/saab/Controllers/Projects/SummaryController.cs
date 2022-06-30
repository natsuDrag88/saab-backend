using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using saab.Data;
using saab.Dto.Project;
using saab.Model.Error;
using saab.Services.Projects;
using saab.Util.Enum;
using saab.Util.Project;
using saab.Util.ResponseDictionary;
using Swashbuckle.AspNetCore.Annotations;

namespace saab.Controllers.Projects
{
    [Route("api/v1/proyectos/resumen")]
    [ApiController]
    public class SummaryController : ControllerBase
    {
        private readonly IProjectsSummaryService _projectsSummaryService;

        public SummaryController(IProjectsSummaryService projectsSummaryService)
        {
            _projectsSummaryService = projectsSummaryService;
        }


        [HttpGet]
        [SwaggerOperation(
            Summary = "Resumen general de proyectos con información del mes actual.",
            Description = "Resumen general de proyectos con información de ahorros y facturación del periodo.",
            OperationId = "Get",
            Tags = new[] { "Proyectos" })]
        [SwaggerResponse(200, "Success", typeof(SummaryModel))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public Task<ObjectResult> Get([BindRequired, FromQuery] string periodo)
        {
            try
            {
                var dictPeriod = DateUtil.GetDictPeriod(period: periodo);
                if (!string.IsNullOrEmpty(dictPeriod["year"]))
                {
                    var listSummary = _projectsSummaryService.GetSummary(periodo);
                    if (listSummary.Count > 0)
                    {
                        return Task.FromResult(StatusCode(StatusCodes.Status200OK,
                            JsonConvert.SerializeObject(listSummary)));
                    }

                    var resultDictionary = ResponseDictionary.GetDictionaryError204();
                    return Task.FromResult(StatusCode(StatusCodes.Status204NoContent, resultDictionary));
                }
                else
                {
                    var resultDictionary =
                        ResponseDictionary.GetDictionaryError400(description: MessagesRequest.ErrorPeriod);
                    return Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, resultDictionary));
                }
            }
            catch (Exception e)
            {
                var resultDictionary =
                    ResponseDictionary.GetDictionaryError400(description: MessagesRequest.ErrorPeriod,
                        error: e.Message);
                return Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, resultDictionary));
            }
        }
    }
}