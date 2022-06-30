using System;
using System.Numerics;

namespace saab.Dto.Concentrate
{
    public class ConcentrateBattery
    {
        public DateTime Fecha { get; set; }
        
        public decimal BatterySoc { get; set; }
        
        public int? Offset { get; set; }
        
    }
}