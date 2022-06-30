namespace saab.Dto.Concentrate
{
    public class ConcentrateMonth
    {
        public string Periodo { get; set; }
        public decimal AhorroBruto { get; set; }
        public decimal AhorroNeto { get; set; }
        public decimal? TotalFacurado { get; set; }
        public decimal? TotalAjusteMesAnterior { get; set; }
        public decimal? TotalFacturadoCfe { get; set; }
        public decimal? AhorroBrutoPunta { get; set; }
        public decimal? AhorroBrutoCapacidad { get; set; }
        public decimal? AhorroBrutoDistribucion { get; set; }
    }
}