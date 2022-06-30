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
    [Route("api/v1/comparacion/jerarquias")]
    [ApiController]
    public class CompareSourcesInformationController : ControllerBase
    {
        private readonly IHierarchyService _hierarchyService;

        public CompareSourcesInformationController(IHierarchyService hierarchyService)
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
        public async Task<ObjectResult> Get([BindRequired, FromQuery] string proyecto,
            [BindRequired, FromQuery] string periodo, [BindRequired, FromQuery] int jerarquia1,
            [BindRequired, FromQuery] int jerarquia2)
        {
            try
            {
                string resultDictionary;
                if (!string.IsNullOrEmpty(proyecto))
                {
                    
                    var energyParameters = this._hierarchyService.GetEnergyParametersHierarchy(
                        period: periodo, project: proyecto,
                        hierarchy1: jerarquia1, hierarchy2: jerarquia2 );
                    
                    var cargosCfe = this._hierarchyService.GetChargesCfe(period: periodo, project: proyecto,
                        hierarchy1: jerarquia1, hierarchy2: jerarquia2 );
                    
                    if (energyParameters.SelectMany(sublist => sublist).ToList().Any() &&
                        cargosCfe.SelectMany(sublist => sublist).ToList().Any())
                    {
                        var sourcesInformation = new SourcesInformation
                        {
                            ParametrosEnergeticos = energyParameters,
                            CargosCfe = cargosCfe
                        };
                        return await Task.FromResult(StatusCode(StatusCodes.Status200OK,
                            JsonConvert.SerializeObject(sourcesInformation)));
                    }

                    resultDictionary = ResponseDictionary.GetDictionaryError204();
                    return await Task.FromResult(StatusCode(StatusCodes.Status204NoContent, resultDictionary));
                }

                resultDictionary = ResponseDictionary.GetDictionaryError400(description: MessagesRequest.ErrorPeriod);
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