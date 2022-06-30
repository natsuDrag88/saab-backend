using System;
using System.Collections.Generic;

#nullable disable

namespace saab.Model
{
    public partial class ComparativaHistorico
    {
        public int Id { get; set; }
        public string Cargo { get; set; }
        public decimal? Tarifa { get; set; }
        public decimal? CargoSinSaaS { get; set; }
        public decimal? CargoConSaas { get; set; }
        public decimal? AhorroBruto { get; set; }
        public int CentroDeCarga { get; set; }
        public int AlgoritmoJerarquia { get; set; }
        public string Mes { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string MesTarifa { get; set; }
    }
}
