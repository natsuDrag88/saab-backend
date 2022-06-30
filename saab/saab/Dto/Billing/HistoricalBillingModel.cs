using System;

namespace saab.Dto.Billing
{
    public class HistoricalBillingModel
    {
        public string periodo { get; set; }
        
        public string cliente { get; set; }
        
        public string rpu { get; set; }
        
        public DateTime? fechaCreacion { get; set; }
        
        public string folioFiscal { get; set; }
        
        public string folioContable { get; set; }
        
        public decimal? montoTotal { get; set; }
        
        public decimal? ajusteMesAnterior { get; set; }
        
        public bool? activo { get; set; }
    }
}