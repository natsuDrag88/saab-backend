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
    [Route("api/v1/proyectos/capacidad_instalada/totales")]
    [ApiController]
    public class InstalledCapacityController : ControllerBase
    {
        private readonly IProjectsService _projectsService;

        public InstalledCapacityController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Información de capacidad total de todos los proyectos.",
            Description = "Es la suma total de la capacidad instalada de todos los centros de carga.",
            OperationId = "Get",
            Tags = new[] { "Proyectos" })]
        [SwaggerResponse(200, "Success", typeof(TotalsProject))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public Task<ObjectResult> Get([BindRequired, FromQuery] string estado)
        {
            try
            {
                Enum.TryParse(estado, out Status status);
                var resultTotalsCentroCarga = _projectsService.GetInstalledCapacityProjects(status);
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