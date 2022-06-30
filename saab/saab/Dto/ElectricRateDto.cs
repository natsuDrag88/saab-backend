using System;
using Newtonsoft.Json;

namespace saab.Dto
{
    public class ElectricRateDto
    {
        [JsonProperty(PropertyName = "Periodo")]
        public DateTime Period { get; set; }
        [JsonProperty(PropertyName = "Segmento")]
        public string Segment { get; set; }
        [JsonProperty(PropertyName = "Unidades")]
        public string Units { get; set; }
        [JsonProperty(PropertyName = "Conceptos")]
        public string Concept { get; set; }
        [JsonProperty(PropertyName = "Division")]
        public string Division { get; set; }
        [JsonProperty(PropertyName = "IntervaloHora")]
        public string TimeInterval { get; set; }
        [JsonProperty(PropertyName = "CargoTarifario")]
        public decimal? FeeCharges { get; set; }
    }
}