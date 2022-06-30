using System;

namespace saab.Dto.Project
{
    public class ProjectRecord
    {
        public string Direccion { get; set; }
        public string TarifasCfe { get; set; }
        public decimal? DemandaContratada { get; set; }
        public string TipoContrato { get; set; }
        public int? Duracion { get; set; }
        public DateTime? FechaFirma { get; set; }
        public DateTime? FechaInicioOperacion { get; set; }
        public decimal FactorAhorro { get; set; }
        public decimal? PenaTerminacionAnticipada { get; set; }
        public decimal? Capacidad { get; set; }
        public string Instalador { get; set; }
        public string Fabricante { get; set; }
        public string Controlador { get; set; }
        public decimal? Capex { get; set; }
        public decimal? Opex { get; set; }
        public decimal? IngresoMensualEsperado { get; set; }
    }
}