using System.Collections.Generic;
using saab.Dto;

namespace saab.Repository
{
    public interface ICuadroTarifarioRepository
    {
        public bool GetAlertFareDelayCfe(string periodString);

        public List<ElectricRateDto> GetElectricRates(List<string> listPeriod, string rate, string division);

        public List<string> GetDivision();
        public List<GenericObject<string, string>> GetRates();
    }
}