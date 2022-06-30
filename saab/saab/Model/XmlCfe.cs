using System;
using System.Collections.Generic;

#nullable disable

namespace saab.Model
{
    public partial class XmlCfe
    {
        public string Rpu { get; set; }
        public string Periodo { get; set; }
        public decimal? KwhTotal { get; set; }
        public decimal? KWhBaseConsumo { get; set; }
        public decimal? KWhIntermediaConsumo { get; set; }
        public decimal? KWhPuntaConsumo { get; set; }
        public decimal? KWhSemipuntaConsumo { get; set; }
        public decimal? KWBaseDemanda { get; set; }
        public decimal? KWIntermediaDemanda { get; set; }
        public decimal? KWPuntaDemanda { get; set; }
        public decimal? KWSemipuntaDemanda { get; set; }
        public decimal? KWMaxDemanda { get; set; }
        public decimal? KVarh { get; set; }
        public decimal? FactorPotencia { get; set; }
        public decimal? CargoSuministro { get; set; }
        public decimal? CargoDistribucion { get; set; }
        public decimal? CargoTransmision { get; set; }
        public decimal? CargoCenace { get; set; }
        public decimal? CargoGeneracionB { get; set; }
        public decimal? CargoGeneracionI { get; set; }
        public decimal? CargoGeneracionP { get; set; }
        public decimal? CargoGeneracionSp { get; set; }
        public decimal? CargoCapacidad { get; set; }
        public decimal? CargoScnMem { get; set; }
        public decimal? ImporteCargoFijo { get; set; }
        public decimal? ImporteEnergia { get; set; }
        public decimal? ImporteBonifFactorPotencia { get; set; }
        public decimal? Importe2Bt { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Iva { get; set; }
        public decimal? TotalFacturado { get; set; }
        public string UuidCfe { get; set; }
        public string Rfc { get; set; }
        public decimal? ImporteDap { get; set; }
    }
}
