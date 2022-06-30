using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using saab.Dto.Project;
using saab.Model.Error;
using saab.Services.Projects;
using saab.Util.ResponseDictionary;
using Swashbuckle.AspNetCore.Annotations;

namespace saab.Controllers.Projects
{
    [Route("api/v1/proyectos/lista_proyectos")]
    [ApiController]
    public class ProjectsAllController : ControllerBase
    {
        private readonly IProjectsService _projectsService;

        public ProjectsAllController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Información .",
            Description = "..",
            OperationId = "Get",
            Tags = new[] { "Proyectos" })]
        [SwaggerResponse(200, "Success", typeof(ProjectData))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public Task<ObjectResult> Get()
        {
            try
            {
                var resultProjects = _projectsService.GetAllProjects();
                return Task.FromResult(StatusCode(StatusCodes.Status200OK,
                    JsonConvert.SerializeObject(resultProjects)));
            }
            catch (Exception e)
            {
                var resultDictionary = ResponseDictionary.GetDictionaryError400(error: e.Message);
                return Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, resultDictionary));
            }
            
        }
    }
}