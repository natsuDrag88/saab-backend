using Newtonsoft.Json;

namespace saab.Dto.Project
{
    public class ProjectClient
    {
        [JsonProperty(PropertyName = "IdProyecto")]
        public int IdCentroCarga { get; set; }
        [JsonProperty(PropertyName = "IdCliente")]
        public int IdClient { get; set; }
        [JsonProperty(PropertyName = "Cliente")]
        public string Client { get; set; }
        [JsonProperty(PropertyName = "Rpu")]
        public string Rpu { get; set; }
        [JsonProperty(PropertyName = "DiaPlazoPago")]
        public int? PaymentDeadlineDays { get; set; }
        [JsonProperty(PropertyName = "Tarifa")]
        public string Rate { get; set; }
    }
}