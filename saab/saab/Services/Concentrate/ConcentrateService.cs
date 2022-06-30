using System.Collections.Generic;
using System.Linq;
using saab.Dto.Concentrate;
using saab.Model;
using saab.Repository;
using saab.Services.Hierarchy;
using saab.Services.Saving;
using saab.Util.Project;

namespace saab.Services.Concentrate
{
    public class ConcentrateService : IConcentrateService
    {
        private readonly IFacturacionRepository _facturacionRepository;
        private readonly ICentroDeCargaRepository _centroDeCargaRepository;
        private readonly ISavingService _savingService;
        private readonly IHierarchyService _hierarchyService;
        private readonly IMedidorBessRepository _medidorBessRepository;
        private readonly IMedidorEtbRepository _medidorEtbRepository;
        private readonly IMedidorOperatiRepository _medidorOperatiRepository;
        private readonly IMedidorRepository _medidorRepository;
        private readonly IPeriodoBipCfeRepository _periodoBipCfeRepository;

        public ConcentrateService(IFacturacionRepository facturacionRepository, ISavingService savingService,
            ICentroDeCargaRepository centroDeCargaRepository, IHierarchyService hierarchyService, 
            IMedidorRepository medidorRepository, IMedidorOperatiRepository medidorOperatiRepository, 
            IMedidorEtbRepository medidorEtbRepository, IMedidorBessRepository medidorBessRepository, 
            IPeriodoBipCfeRepository periodoBipCfeRepository)
        {
            _facturacionRepository = facturacionRepository;
            _savingService = savingService;
            _centroDeCargaRepository = centroDeCargaRepository;
            _hierarchyService = hierarchyService;
            _medidorRepository = medidorRepository;
            _medidorOperatiRepository = medidorOperatiRepository;
            _medidorEtbRepository = medidorEtbRepository;
            _medidorBessRepository = medidorBessRepository;
            _periodoBipCfeRepository = periodoBipCfeRepository;
        }

        public List<ConcentrateMonth> GetConcentrate(Dictionary<string, string> dictPeriod)
        {
            var listPeriods = DateUtil.GetLastMonthbyYear(dictPeriod: dictPeriod);
            var listProjects = _centroDeCargaRepository.GetAll();
            return (from period in listPeriods select GetValuesbyMonth(period: period, projects: listProjects) into concentrateMonth let valueConcentrate = concentrateMonth.AhorroBruto + concentrateMonth.AhorroNeto + concentrateMonth.TotalFacurado + concentrateMonth.TotalAjusteMesAnterior + concentrateMonth.TotalFacturadoCfe + concentrateMonth.AhorroBrutoPunta + concentrateMonth.AhorroBrutoCapacidad + concentrateMonth.AhorroBrutoDistribucion where valueConcentrate != 0 select concentrateMonth).ToList();
        }

        public List<ConcentrateTypePeriod> GetConcentrateTypePeriod(InputConcentrateTypePeriod inputConcentrate)
        {
            var initialDate = DateUtil.GetInitialDate(date: inputConcentrate.FechaInicial);
            var finalDate = DateUtil.GetFinalDate(date: inputConcentrate.FechaFinal);
            var resultProvider = _medidorRepository.GetNameProvider(idProvider: inputConcentrate.Medidor);
            var resultPeriod = resultProvider.Provedor switch
            {
                "OPE" => _medidorOperatiRepository.GetConcentratePeriodTypeOpe(initialDate: initialDate,
                    finalDate: finalDate, idMeter: inputConcentrate.Medidor),
                "BESS" => _medidorBessRepository.GetConcentratePeriodTypeBess(initialDate: initialDate,
                    finalDate: finalDate, idMeter: inputConcentrate.Medidor),
                "ETB" => _medidorEtbRepository.GetConcentratePeriodTypeEtb(initialDate: initialDate,
                    finalDate: finalDate, idMeter: inputConcentrate.Medidor),
                _ => null
            };

            return resultPeriod;
        }

        public List<ConcentratePeriodBip> GetConcentratePeriodBip(InputConcentratePeriodBip inputConcentrate)
        {
            var initialDate = DateUtil.GetInitialDate(date: inputConcentrate.FechaInicial);
            var finalDate = DateUtil.GetFinalDate(date: inputConcentrate.FechaFinal);
            var resultRateCfe = _medidorRepository.GetRateCfe(idProvider: inputConcentrate.Medidor);
            var listTypePeriod = inputConcentrate.TipoPeriodo.Split(',');
            var listPeriods = DateUtil.GetListPeriodsFiveMinutes(initialDate: initialDate, finalDate: finalDate);
            var resultPeriodBip = _periodoBipCfeRepository.GetConcentratePeriod(initialDate: initialDate, finalDate: finalDate, 
                typePeriod: listTypePeriod, rateCfe: resultRateCfe.TarifaCfe);
            foreach (var period in listPeriods)
            {
                var concentratePeriodBip = resultPeriodBip.FirstOrDefault(x => x.Fecha == period);
                if (concentratePeriodBip != null) continue;
                var newValue = new ConcentratePeriodBip()
                {
                    Fecha = period,
                    TipoPeriodo = null
                };
                resultPeriodBip.Add(newValue);
            }
            return resultPeriodBip;
        }

        private ConcentrateMonth GetValuesbyMonth(string period, List<CentrosDeCarga> projects)
        {
            var ahorroBrutoPunta = new decimal?(0);
            var ahorroBrutoCapacidad = new decimal?(0);
            var ahorroBrutoDistribucion = new decimal?(0);
            var periodString = DateUtil.ConvertPeriodMonthNametoNumber(period: period);
            var dictPeriod = DateUtil.GetDictPeriod(period: periodString);
            var resultGrossSaving = _savingService.GetSavingTotalProject(listDataCentroCarga: projects,
                dictPeriod: dictPeriod);
            var resultNetSaving = _savingService.GetSavingTotalProject(listDataCentroCarga: projects,
                dictPeriod: dictPeriod, typeProcess: "neto");
            var listBillingTotal = _facturacionRepository.GetBillingTotal(period: period);
            foreach (var project in projects)
            {
                var ahorroPunta = _hierarchyService.GetTotalSavingByPosition(period: periodString,
                    project: project.Id, position: "EnergiaPunta($/kWh)");
                var ahorroCapacidad = _hierarchyService.GetTotalSavingByPosition(period: periodString,
                    project: project.Id, position: "Capacidad($/kWh)");
                var ahorroDistribucion = _hierarchyService.GetTotalSavingByPosition(period: periodString,
                    project: project.Id, position: "Distribución($/kWh)");
                ahorroBrutoPunta = ahorroBrutoPunta + ahorroPunta.total;
                ahorroBrutoCapacidad = ahorroBrutoCapacidad + ahorroCapacidad.total;
                ahorroBrutoDistribucion = ahorroBrutoDistribucion + ahorroDistribucion.total;
            }

            var resultMont = new ConcentrateMonth()
            {
                Periodo = period,
                AhorroBruto = resultGrossSaving,
                AhorroNeto = resultNetSaving,
                TotalFacurado = listBillingTotal != null ? listBillingTotal.TotalFacturado : 0,
                TotalAjusteMesAnterior = listBillingTotal != null ? listBillingTotal.TotalAjusteMesAnterior : 0,
                TotalFacturadoCfe = listBillingTotal != null ? listBillingTotal.TotalFacturadoCfe : 0,
                AhorroBrutoPunta = ahorroBrutoPunta,
                AhorroBrutoCapacidad = ahorroBrutoCapacidad,
                AhorroBrutoDistribucion = ahorroBrutoDistribucion
            };
            return resultMont;
        }
    }
}