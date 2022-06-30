using System;
using System.Collections.Generic;

#nullable disable

namespace saab.Model
{
    public partial class ProporcionConsumoBloqueHorario
    {
        public string Tarifa { get; set; }
        public string Sistema { get; set; }
        public string IntHorario { get; set; }
        public string Periodo { get; set; }
        public decimal Consumo { get; set; }
    }
}
