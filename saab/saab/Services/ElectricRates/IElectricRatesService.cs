using System.Collections.Generic;
using saab.Dto;
using saab.Dto.Billing;

namespace saab.Services.ElectricRates
{
    public interface IElectricRates
    {
        public List<ElectricRateDto> GetElectricRates(string periodStart, string periodEnd, string rate,
            string division);

        public List<GenericObject<string, string>> GetRates();
        public List<string> GetDivision();
        public List<ElectricRateDto> GetElectricRatesRpu(string periodStart, string periodEnd, string rpu);
    }
}