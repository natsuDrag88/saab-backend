using System;
using System.Collections.Generic;

#nullable disable

namespace saab.Model
{
    public partial class Factura
    {
        public ulong IdDeFactura { get; set; }
        public string Filename { get; set; }
        public string IdCentroDeCarga { get; set; }
        public string Periodo { get; set; }
        public byte[] FacturaPdf { get; set; }
        public byte[] FacturaXml { get; set; }
        public string Uuid { get; set; }
        public string Serie { get; set; }
        public string Folio { get; set; }
        public DateTime? FechaDeCreacion { get; set; }
        public int? AlgoritmoJerarquia { get; set; }
        public decimal? Subtotal { get; set; }
        public bool? Activo { get; set; }
        public decimal? CargoSinSaas { get; set; }
        public decimal? CargoConSaas { get; set; }
        public decimal? AhorroBruto { get; set; }
        public decimal? AhorroNeto { get; set; }
        public decimal? PagoAfrv { get; set; }
        public decimal? AjusteMesAnterior { get; set; }
        public bool? FacturaConsolidada { get; set; }
        public decimal? Iva { get; set; }
        public decimal? MontoTotal { get; set; }
    }
}
