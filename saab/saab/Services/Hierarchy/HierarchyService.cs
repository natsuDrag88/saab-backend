using System;
using System.Collections.Generic;
using System.Linq;
using saab.Dto;
using saab.Dto.Comparison;
using saab.Dto.Saving;
using saab.Repository;
using saab.Repository.DBMysql;
using saab.Services.Saving;
using saab.Util.Enum;
using saab.Util.Project;


namespace saab.Services.Hierarchy
{
    public class HierarchyService : IHierarchyService
    {
        private readonly IParametrosHistoricoRepository _parametrosHistoricoRepository;
        private readonly IComparativaHistoricoRepository _comparativaHistoricoRepository;

        private static readonly string[] ValuesEnergyData =
        {
            "Energía en Base (kWh)", "Energía en Intermedio (kWh)",
            "Energía en Punta (kWh)", "Energía Total del Periodo (kWh)", "Demanda Máx en Base (kW)",
            "Demanda Máx en Intermedio (kW)", "Demanda Máx en Punta (kW)", "Demanda Equivalente (kW)",
            "Demanda Aplicable Capacidad (kW)", "Demanda Aplicable Distribución (kW)"
        };

        private static readonly string[] ValuesChargesCfe =
        {
            "Suministro($/mes)", "EnergíaBase($/KWh)",
            "EnergíaIntermedia($/KWh)", "EnergíaPunta($/KWh)", "Transmisión", "CENACE", "SCnMEM", "Distribución($/kWh)",
            "Capacidad($/kWh)", "2%MediciónenBajaTensión (%)", "DerechodeAlumbradoPúblico (%)"
        };

        public HierarchyService(IParametrosHistoricoRepository parametrosHistoricoRepository,
            IComparativaHistoricoRepository comparativaHistoricoRepository)
        {
            _parametrosHistoricoRepository = parametrosHistoricoRepository;
            _comparativaHistoricoRepository = comparativaHistoricoRepository;
        }

        public List<List<Information>> GetEnergyParametersHierarchy(string period, string project, int hierarchy1,
            int hierarchy2)
        {
            var periodString = DateUtil.ConvertPeriodToPeriodDb(period);
            var hierarchies = new List<int>(new int[] { hierarchy1, hierarchy2 });
            return hierarchies.Select(itemHierarchy =>
                GetDataEnergyParameters(period: periodString, centroCarga: project, hierarchy: itemHierarchy)).ToList();
        }

        public List<List<Information>> GetChargesCfe(string period, string project, int hierarchy1, int hierarchy2)
        {
            var periodString = DateUtil.ConvertPeriodToPeriodDb(period);
            var hierarchies = new List<int>(new int[] { hierarchy1, hierarchy2 });
            return hierarchies.Select(itemHierarchy =>
                GetDataChargesCfe(period: periodString, centroCarga: project, hierarchy: itemHierarchy)).ToList();
        }

        public List<HierarchyDto> GetHierarchies()
        {
            var hierarchies = new List<HierarchyDto>
            {
                
                new HierarchyDto
                {
                    Id = 1, Source = "CFE - Principal CFE - Principal Batería", Description = "Factura CFE - Principal - Principal",
                    Order = 1,
                },
                new HierarchyDto
                {
                    Id = 2, Source = "CFE - Respaldo 1 CFE - Principal Batería", Description = "Factura CFE - Respaldo1 - Principal", Order = 1,
                },
                new HierarchyDto
                {
                    Id = 3, Source = " CFE - Principal CFE - Respaldo 1 Batería", Description = "Factura CFE - Principal - Respaldo1", Order = 1,
                },
                new HierarchyDto
                {
                    Id = 4, Source = "CFE - Principal CFE - Respaldo 2 Batería", Description = "Factura CFE - Principal - Respaldo2", Order = 1,
                },
                new HierarchyDto
                    { Id = 5, Source = "CFE - Respaldo 1 CFE - Respaldo 1 Batería", Description = "Factura CFE - Respaldo1 - Respaldo1", Order = 1, },
                new HierarchyDto
                {
                    Id = 6, Source = "CFE - Respaldo 1 CFE - Respaldo 2 Batería", Description = "Factura CFE - Respaldo1 - Respaldo2", Order = 1,
                },
                new HierarchyDto
                    { Id = 7, Source = "Principal CFE - Principal Batería", Description = "Principal - Principal", Order = 1, },
                new HierarchyDto { Id = 8, Source = "Respaldo 1 CFE - Principal Batería", Description = "Respaldo1 - Principal", Order = 1, },
                new HierarchyDto { Id = 9, Source = "Principal CFE - Respaldo 1 Batería", Description = "Principal - Respaldo1", Order = 1, },
                new HierarchyDto
                    { Id = 10, Source = "Principal CFE - Respaldo 2 Batería", Description = "Principal - Respaldo2", Order = 1, },
                new HierarchyDto { Id = 11, Source = "Respaldo 1 CFE - Respaldo 1 Batería", Description = "Respaldo1 - Respaldo1", Order = 1, },
                new HierarchyDto { Id = 12, Source = "Respaldo 1 CFE - Respaldo 2 Batería", Description = "Respaldo1 - Respaldo2", Order = 1, }
            };
            return hierarchies;
        }

        public TotalsProject GetTotalSavingByPosition(string period, int project, string position)
        {
            var dictPeriod = DateUtil.GetDictPeriod(period: period);
            var maxHierarchy = GetMaxJerarquia(idCentroCarga: project, dictPeriod: dictPeriod, position);

            return new TotalsProject
            {
                unidad = "Ahorros",
                total = _comparativaHistoricoRepository.GetGrossSavingById(maxHierarchy)
            };
        }

        public int GetMaxJerarquia(int idCentroCarga, Dictionary<string, string> dictPeriod, string position)
        {
            var highestHierarchy = 0;

            var periodString = DateUtil.ConvertMonthToString(month: int.Parse(dictPeriod["month"])) + "_" +
                               dictPeriod["year"];

            var dataDesglose =
                _comparativaHistoricoRepository.GetByPeriodAndProjectAndPosition(project: idCentroCarga,
                    period: periodString, position: position);

            if (dataDesglose.Count <= 0) return highestHierarchy;
            var dataMaxJerarquiaDesglose = dataDesglose.OrderByDescending(x => x.FechaActualizacion)
                .ThenByDescending(x => x.AlgoritmoJerarquia).FirstOrDefault();
            if (dataMaxJerarquiaDesglose != null) highestHierarchy = dataMaxJerarquiaDesglose.Id;

            return highestHierarchy;
        }

        private List<Information> GetDataEnergyParameters(string period, string centroCarga, int hierarchy)
        {
            return _parametrosHistoricoRepository.GetDataParametrosHistorico(period: period,
                project: centroCarga, hierarchy: hierarchy, listValues: ValuesEnergyData);
        }

        private List<Information> GetDataChargesCfe(string period, string centroCarga, int hierarchy)
        {
            return _comparativaHistoricoRepository.GetDataComparativaHistorico(period: period,
                project: centroCarga, hierarchy: hierarchy, listValues: ValuesChargesCfe);
        }
    }
}