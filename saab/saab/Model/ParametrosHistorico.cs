using System;
using System.Collections.Generic;

#nullable disable

namespace saab.Model
{
    public partial class ParametrosHistorico
    {
        public int Id { get; set; }
        public string Concepto { get; set; }
        public decimal? ConsumoSinSaaS { get; set; }
        public decimal? ConsumoConSaaS { get; set; }
        public decimal? CantidadEvitada { get; set; }
        public int CentroDeCarga { get; set; }
        public int AlgoritmoJerarquia { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string MesTarifa { get; set; }
        public string Mes { get; set; }
    }
}
