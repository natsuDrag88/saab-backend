using System;

namespace saab.Dto.Alerts
{
    public class AlertDetailModel
    {

        public string cliente { get; set; }
        
        public string rpu { get; set; }
        
        public decimal? ahorroMinimoEsperado { get; set; }
        
        public decimal? ahorroBrutoMes { get; set; }
        
        public DateTime? fechaActualizacion { get; set; }

    }
}