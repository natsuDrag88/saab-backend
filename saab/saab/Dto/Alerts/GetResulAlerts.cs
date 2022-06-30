namespace saab.Dto.Alerts
{
    public class GetResulAlerts
    {
        public AlertLowerExpectedSavings AhorrosMenoresEsperados { get; set; }
        public AlertUpdateDelay RetrasoActualizacion { get; set; }
        public AlertWithoutCfeInvoice SinFacturaCfe { get; set; }
        public AlertWithoutInvoiceIssuance SinEmisionFactura { get; set; }
        public AlertCfeRateDelay RetrasoTarifaCfe { get; set; }
    }
}