using System;
using System.Collections.Generic;

#nullable disable

namespace saab.Model
{
    public partial class TemporadasCfe
    {
        public int Id { get; set; }
        public string Sistema { get; set; }
        public string Tarifa { get; set; }
        public string Temporada { get; set; }
        public string Periodo { get; set; }
        public string Año { get; set; }
    }
}
