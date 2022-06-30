using System.Collections.Generic;

namespace saab.Dto.Alerts
{
    public class AlertDetail
    {
        public List<AlertLowerExpectedSavings> AhorrosMenoresEsperados { set; get; }
        public List<AlertUpdateDelay> RetrasoActualizacion { set; get; }
        public List<AlertWithoutCfeInvoice> SinFacturaCfe { set; get; }
        public List<AlertWithoutInvoiceIssuance> SinEmisionFactura { set; get; }
        public List<AlertCfeRateDelay> RetrasoTarifaCfe { set; get; }
    }
}