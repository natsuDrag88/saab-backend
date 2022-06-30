namespace saab.Dto.Project
{
    public class SummaryModel
    {

        public string cliente { get; set; }
        
        public string rpu { get; set; }
        
        public decimal? ahorroBrutoMesActual { get; set; }
        
        public decimal? facturacionMesAnterior { get; set; }
        
        public decimal? facturacionAnual { get; set; }

    }
}