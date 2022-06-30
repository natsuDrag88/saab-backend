namespace saab.Dto.Billing
{
    public class Invoice
    {
        public string Periodo { get; set; }
        public byte[] FacturaPdf { get; set; }

        public byte[] FacturaXml { get; set; }
    }
}