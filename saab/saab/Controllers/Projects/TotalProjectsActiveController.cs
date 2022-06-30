using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using saab.Dto;
using saab.Model.Error;
using saab.Services.Projects;
using saab.Util.Enum;
using saab.Util.ResponseDictionary;
using Swashbuckle.AspNetCore.Annotations;

namespace saab.Controllers.Projects
{
    [Route("api/v1/proyectos/totales")]
    [ApiController]
    public class ProjectsActiveController : ControllerBase
    {
        private readonly IProjectsService _projectsService;

        public ProjectsActiveController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Información de resumen de facturas.",
            Description = "Información de resumen de facturas por proyectos",
            OperationId = "Get",
            Tags = new[] { "Proyectos" })]
        [SwaggerResponse(200, "Success", typeof(TotalsProject))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public Task<ObjectResult> Get([BindRequired, FromQuery] string estado)
        {
            try
            {
                if (!string.IsNullOrEmpty(estado))
                {
                    Enum.TryParse(estado, out Status status);
                    var totalProjectsActives = _projectsService.GetTotalProjectsActives(status);
                    HttpContext.Response.StatusCode = 200;
                    return Task.FromResult(StatusCode(StatusCodes.Status200OK, JsonConvert.SerializeObject(totalProjectsActives)));
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