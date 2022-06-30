using System;
using System.Collections.Generic;

#nullable disable

namespace saab.Model
{
    public partial class DesglosePagoHistorico
    {
        public int Id { get; set; }
        public decimal? AhorroBruto { get; set; }
        public decimal? AhorroNeto { get; set; }
        public decimal? PagoFrv { get; set; }
        public decimal? AjusteMesAnterior { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Iva { get; set; }
        public decimal? Total { get; set; }
        public int CentroDeCarga { get; set; }
        public int AlgoritmoJerarquia { get; set; }
        public string Mes { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
