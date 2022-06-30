using System.Collections.Generic;
using System.Linq;
using saab.Dto;
using saab.Repository;
using saab.Util.Project;

namespace saab.Services.ElectricRates
{
    public class ElectricRates : IElectricRates
    {
        private readonly ICuadroTarifarioRepository _cuadroTarifarioRepository;
        private readonly ICentroDeCargaRepository _centroDeCargaRepository;

        public ElectricRates(ICuadroTarifarioRepository cuadroTarifarioRepository, ICentroDeCargaRepository centroDeCargaRepository)
        {
            _cuadroTarifarioRepository = cuadroTarifarioRepository;
            _centroDeCargaRepository = centroDeCargaRepository;
        }

        public List<ElectricRateDto> GetElectricRates(string periodStart, string periodEnd, string rate,
            string division)
        {
            var periodStartDate = DateUtil.ConvertPeriodToDate(periodStart);
            var periodEndDate = DateUtil.ConvertPeriodToDate(periodEnd);
            var listDates = DateUtil.ListBetweenTwoDates(periodStartDate, periodEndDate);
            var listPeriodDb = listDates.Select(DateUtil.ConvertDateToPeriodDb).ToList();

            var rates = _cuadroTarifarioRepository.GetElectricRates(listPeriod: listPeriodDb, rate: rate,
                division: division);

            return (from p in rates orderby p.Period select p).ToList();
        }
        
        public List<ElectricRateDto> GetElectricRatesRpu(string periodStart, string periodEnd, string rpu)
        {
            var dataCentroCarga = _centroDeCargaRepository.GetRecordGeneralByRpu(rpu: rpu);
            return this.GetElectricRates(periodStart: periodStart, periodEnd: periodEnd, rate: dataCentroCarga.TarifaCfe,
                division: dataCentroCarga.DivisionTarifaria);
        }

        public List<GenericObject<string, string>> GetRates()
        {
            return _cuadroTarifarioRepository.GetRates();
        }


        public List<string> GetDivision()
        {
            return _cuadroTarifarioRepository.GetDivision();
        }
    }
}