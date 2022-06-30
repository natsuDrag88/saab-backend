using System;
using System.Collections.Generic;

#nullable disable

namespace saab.Model
{
    public partial class PeriodosCfe
    {
        public int IdPeriodosCfe { get; set; }
        public string Tarifa { get; set; }
        public string Sistema { get; set; }
        public string Estacion { get; set; }
        public string DiaDeLaSemana { get; set; }
        public string Horario { get; set; }
        public string Tipo { get; set; }
        public DateTime? Año { get; set; }
    }
}
