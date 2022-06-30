using System;

namespace saab.Dto.Alerts
{
    public class AlertLowerExpectedSavings
    {
        public string  Cliente { get; set; }
        public string  Rpu { get; set; }
        public decimal?  AhorroMinimoEsperado { get; set; }
        public decimal?  AhorroCalculado { get; set; }
        public DateTime? FechaActual { get; set; }
    }
}