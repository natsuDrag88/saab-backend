using System;
using System.Collections.Generic;

#nullable disable

namespace saab.Model
{
    public partial class PeriodosBipCfe
    {
        public DateTime Fecha { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int Dia { get; set; }
        public int Hora { get; set; }
        public int Min { get; set; }
        public int DiaSemana { get; set; }
        public string Estacion { get; set; }
        public string TarifaCfe { get; set; }
        public string Sistema { get; set; }
        public string PeriodoCfe { get; set; }
    }
}
