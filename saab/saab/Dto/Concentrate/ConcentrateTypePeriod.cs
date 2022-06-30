using System;

namespace saab.Dto.Concentrate
{
    public class ConcentrateTypePeriod
    {
        public DateTime Fecha { get; set; }
        public decimal KwhE { get; set; }
        public decimal KwhR { get; set; }
        public int Offset { get; set; }
    }
}