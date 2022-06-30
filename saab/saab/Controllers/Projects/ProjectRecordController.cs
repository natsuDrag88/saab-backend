using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using saab.Dto;
using saab.Dto.Project;
using saab.Model.Error;
using saab.Services.Projects;
using saab.Util.Enum;
using saab.Util.ResponseDictionary;
using Swashbuckle.AspNetCore.Annotations;

namespace saab.Controllers.Projects
{
    [Route("api/v1/proyectos/info_general")]
    [ApiController]
    public class ProjectRecordController : ControllerBase
    {
        private readonly IProjectsService _projectsService;

        public ProjectRecordController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Información .",
            Description = "..",
            OperationId = "Get",
            Tags = new[] { "Proyectos" })]
        [SwaggerResponse(200, "Success", typeof(ProjectRecord))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public Task<ObjectResult> Get([BindRequired, FromQuery] int idProyecto)
        {
            try
            {
                var resultTotalsCentroCarga = _projectsService.GetRecordGeneralByProject(idProyecto);
                HttpContext.Response.StatusCode = 200;
                return Task.FromResult(StatusCode(StatusCodes.Status200OK,
                    JsonConvert.SerializeObject(resultTotalsCentroCarga)));
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 400;
                var resultDictionary = ResponseDictionary.GetDictionaryError400(error: e.Message);
                return Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, resultDictionary));
            }
        }
    }
}