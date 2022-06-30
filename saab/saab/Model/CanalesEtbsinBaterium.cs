using System;
using System.Collections.Generic;

#nullable disable

namespace saab.Model
{
    public partial class CanalesEtbsinBaterium
    {
        public int IdcentroDeCarga { get; set; }
        public int FechaUnix { get; set; }
        public DateTime? FechaNormal { get; set; }
        public int? Offset { get; set; }
        public decimal? MeterSiteDemand { get; set; }
        public decimal? MeterSiteNetPvDemand { get; set; }
    }
}
