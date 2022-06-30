using System;

namespace saab.Dto.Alerts
{
    public class AlertUpdateDelay
    {
        public string Cliente { get; set; }
        public string Rpu { get; set; }
        public string MesOperacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}