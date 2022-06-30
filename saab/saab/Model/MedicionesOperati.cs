using System;
using System.Collections.Generic;

#nullable disable

namespace saab.Model
{
    public partial class MedicionesOperati
    {
        public DateTime Fecha { get; set; }
        public int Idmedidor { get; set; }
        public decimal KWhE { get; set; }
        public decimal KWhR { get; set; }
        public decimal KVarhQ1 { get; set; }
        public decimal KVarhQ2 { get; set; }
        public decimal KVarhQ3 { get; set; }
        public decimal KVarhQ4 { get; set; }
        public int Offset { get; set; }
    }
}
