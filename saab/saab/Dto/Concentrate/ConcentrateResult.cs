using System.Collections.Generic;

namespace saab.Dto.Concentrate
{
    public class ConcentrateResult
    {
        public List<ConcentrateTypePeriodKwE> ConsumoCfe { get; set; }
        
        public List<ConcentrateTypePeriodKwE> CargaBateria { get; set; }
        
        public List<ConcentrateTypePeriodKwR> DescargaBateria { get; set; }
        
        public List<ConcentrateBattery> EstadoBateria { get; set; }
    }
}