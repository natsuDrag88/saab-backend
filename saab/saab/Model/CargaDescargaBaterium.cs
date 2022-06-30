using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

#nullable disable

namespace saab.Model
{
    public partial class CargaDescargaBaterium
    {
        public int Idmedidor { get; set; }
        public int FechaUnix { get; set; }
        public DateTime FechaNormal { get; set; }
        public int? Offset { get; set; }
        public decimal BatterySoc { get; set; }
        public decimal? EssPower { get; set; }
    }
}
