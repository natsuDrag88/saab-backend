using System;
using System.Collections.Generic;

#nullable disable

namespace saab.Model
{
    public partial class FactoresDeCarga
    {
        public string GrupoTarifario { get; set; }
        public decimal? FactorDeCarga { get; set; }
        public string Periodo { get; set; }
    }
}
