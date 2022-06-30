using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using saab.Dto.Comparison;
using saab.Dto.Saving;
using saab.Model.Error;
using saab.Services.Hierarchy;
using saab.Util.Enum;
using saab.Util.ResponseDictionary;
using Swashbuckle.AspNetCore.Annotations;

namespace saab.Controllers.Comparisons
{
    [Route("api/v1/jerarquias")]
    [ApiController]
    public class HierarchyController : ControllerBase
    {
        private readonly IHierarchyService _hierarchyService;

        public HierarchyController(IHierarchyService hierarchyService)
        {
            _hierarchyService = hierarchyService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "",
            Description = "",
            OperationId = "Get",
            Tags = new[] { "Historico" })]
        [SwaggerResponse(200, "Success", typeof(List<HierarchyDto>))]
        [SwaggerResponse(400, "BadRequest", type: typeof(ErrorResponse))]
        public async Task<ObjectResult> Get()
        {
            try
            {
                var hierarchies = _hierarchyService.GetHierarchies();
                return await Task.FromResult(StatusCode(StatusCodes.Status200OK,
                    JsonConvert.SerializeObject(hierarchies)));
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