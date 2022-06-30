namespace saab.Dto.Saving
{
    public class InformationHistorical
    {
        public string Periodo { get; set; }
        
        public decimal? AhorroBruto { get; set; }
        
        public decimal? AhorroNeto { get; set; }
        
        public decimal? PagoFrv { get; set; }
        
        public decimal? CargoSinSaas { get; set; }
        
        public decimal? CargoConSaas { get; set; }

    }
}