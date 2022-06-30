using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using saab.Dto.Saving;
using saab.Model.Error;
using saab.Services.Hierarchy;
using saab.Util.Enum;
using saab.Util.ResponseDictionary;
using Swashbuckle.AspNetCore.Annotations;

namespace saab.Controllers.Comparisons
{
    [Route("api/v1/comparacion/saving")]
    [ApiController]
    public class TotalSavingCompareController : ControllerBase
    {
        private readonly IHierarchyService _hierarchyService;

        public TotalSavingCompareController(IHierarchyService hierarchyService)
        {
            _hierarchyService = hierarchyService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Comparación por fuentes de información.",
            Description = "Comparacion de parámetros energéticos y cargos CFE.",
            OperationId = "Get",
            Tags = new[] { "Historico" })]
        [SwaggerResponse(200, "Success", typeof(object))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public async Task<ObjectResult> Get([BindRequired, FromQuery] int proyecto,
            [BindRequired, FromQuery] string periodo,[BindRequired, FromQuery] string cargo)
        {
            try
            {
                if (!string.IsNullOrEmpty(periodo))
                {
                    var res = _hierarchyService.GetTotalSavingByPosition(periodo, proyecto,cargo);
                    return await Task.FromResult(StatusCode(StatusCodes.Status200OK, JsonConvert.SerializeObject(res)));
                }

                var resultDictionary =
                    ResponseDictionary.GetDictionaryError400(description: MessagesRequest.ErrorPeriod);
                return await Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, resultDictionary));
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