using System;
using System.Collections.Generic;

#nullable disable

namespace saab.Model
{
    public partial class CuadroTarifario
    {
        public string Tarifa { get; set; }
        public string Segmento { get; set; }
        public string Unidades { get; set; }
        public string Concepto { get; set; }
        public string Division { get; set; }
        public string IntHorario { get; set; }
        public decimal? CargosTrifarios { get; set; }
        public string Periodo { get; set; }
    }
}
