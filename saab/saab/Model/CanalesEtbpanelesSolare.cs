using System;
using System.Collections.Generic;

#nullable disable

namespace saab.Model
{
    public partial class CanalesEtbpanelesSolare
    {
        public int Idmedidor { get; set; }
        public int FechaUnix { get; set; }
        public DateTime? FechaNormal { get; set; }
        public int? Offset { get; set; }
        public decimal? MeterPvPower { get; set; }
    }
}
