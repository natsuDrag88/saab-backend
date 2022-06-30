using System;

namespace saab.Model
{
    public class HistoricalBilling
    {
        public ulong Id { get; set; }
        public int Anio { get; set; }
        public string Mes { get; set; }
        public string Client { get; set; }
        public string Rpu { get; set; }
        public DateTime Fecha { get; set; }
        public string Folio { get; set; }
        public string FolioContable { get; set; }
        public string FolioFiscal { get; set; }
        public decimal Monto { get; set; }
        public decimal AjusteMesAnterior { get; set; }
        public string Estado { get; set; }
    }
}