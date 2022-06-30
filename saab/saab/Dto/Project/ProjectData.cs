using System;

namespace saab.Dto.Project
{
    public class ProjectData
    {
        public string Proyecto { get; set; }
        public string Rpu { get; set; }
        public string Estatus { get; set; }
        public string TipoContrato { get; set; }
        public DateTime FechaInicio { get; set; }
        public int? Duracion { get; set; }
        public decimal? CapacidadBateria { get; set; }
        public decimal? DemandaContratada { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string TafifaCfe { get; set; }
        public string Epc { get; set; }
        public string FabricanteBateria { get; set; }
        public string Controlador { get; set; }
        public decimal? PenaTerminacionAnticipada { get; set; }
        public decimal? Capex { get; set; }
        public decimal? Opex { get; set; }
        public decimal? IngresosAnualesEsperados { get; set; }
        public decimal FactorAhorro { get; set; }
    }
}