using System;
using System.Collections.Generic;

#nullable disable

namespace saab.Model
{
    public partial class DiasFeriado
    {
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        public string Detalle { get; set; }
    }
}
