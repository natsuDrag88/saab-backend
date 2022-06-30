using System;

namespace saab.Dto.Billing
{
    public class BillingOverallSummary
    {
        public string Cliente { get; set; }
        public string Rpu { get; set; }
        public decimal MontoFacturado { get; set; }
        public DateTime? FechaEmisionUltimaFactura { get; set; }
        public DateTime? FechaLimitePago { get; set; }
        public decimal FacturacionHistorica { get; set; }
        public decimal EstimacionMesActual { get; set; }
    }
}