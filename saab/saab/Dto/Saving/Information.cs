using System;

namespace saab.Dto.Saving
{
    public class Information
    {
        public string Concepto { get; set; }
        
        public decimal? OperacionconSaaS { get; set; }
        
        public decimal? OperacionsinSaaS { get; set; }
        
        public decimal? Ahorros { get; set; }
        
        public DateTime? FechaActualizacion { get; set; }
    }

}