using System;
using System.Collections.Generic;
using System.Linq;
using saab.Dto;
using saab.Dto.Saving;
using saab.Model;
using saab.Repository;
using saab.Services.Projects;
using saab.Util.Enum;
using saab.Util.Project;

namespace saab.Services.Saving
{
    public class SavingService : ISavingService
    {
        private readonly IDesglosePagoHistoricoRepository _desglosePagoHistoricoRepository;
        private readonly IProjectsService _projectsService;

        public SavingService(IDesglosePagoHistoricoRepository desglosePagoHistoricoRepository,
            IProjectsService projectsService)
        {
            _desglosePagoHistoricoRepository = desglosePagoHistoricoRepository;
            _projectsService = projectsService;
        }

        public TotalsProject GetSavingTotal(string period, int? client, string rpu = null, string typeProcess = "bruto")
        {
            var statusCentroCarga = Convert.ToUInt64((int)Enum.Parse(typeof(Status), Status.activo.ToString()));
            var listDataCentroCarga =
                _projectsService.GetProjectsByStatusOptionalFilters(statusCentroCarga, client, rpu: rpu);
            var dictPeriod = DateUtil.GetDictPeriod(period: period);

            return new TotalsProject
            {
                unidad = "Ahorros",
                total = GetSavingTotalProject(listDataCentroCarga: listDataCentroCarga, dictPeriod:dictPeriod,
                    typeProcess: typeProcess)
            };
        }

        public int GetMaxJerarquiaDesglose(int idCentroCarga, Dictionary<string, string> dictPeriod)
        {
            var highestHierarchy = 0;

            var periodString = DateUtil.ConvertMonthToString(month: int.Parse(dictPeriod["month"])) + "_" +
                               dictPeriod["year"];

            var dataDesglose =
                _desglosePagoHistoricoRepository.GetByCentroCargaPeriod(centroCarga: idCentroCarga,
                    period: periodString);

            if (dataDesglose.Count <= 0) return highestHierarchy;
            var dataMaxJerarquiaDesglose = dataDesglose.OrderByDescending(x => x.FechaActualizacion)
                .ThenByDescending(x => x.AlgoritmoJerarquia).FirstOrDefault();
            if (dataMaxJerarquiaDesglose != null) highestHierarchy = dataMaxJerarquiaDesglose.Id;

            return highestHierarchy;
        }


        public DesglosePagoHistorico GetDesglosePagoHistoricoById(int id)
        {
            return _desglosePagoHistoricoRepository.GetDesglosePagoHistoricoById(id);
        }

        public decimal GetSavingTotalProject(List<CentrosDeCarga> listDataCentroCarga,
            Dictionary<string, string> dictPeriod, string typeProcess)
        {
            var total = new decimal(0);
            foreach (var centroCarga in listDataCentroCarga)
            {
                if (string.IsNullOrEmpty(dictPeriod["month"]))
                {
                    var resultsMaxValue = GetMaxJerarquiaDesgloseByPeriodYear(centroCarga.Id, dictPeriod);
                    total = resultsMaxValue.Aggregate(total,
                        (current, resultMaxValue) => current + TypeSaving(resultMaxValue, typeProcess));
                }
                else
                {
                    var resultMaxValue = GetMaxJerarquiaDesglose(centroCarga.Id, dictPeriod);
                    total = total + TypeSaving(resultMaxValue, typeProcess);
                }
            }

            return total;
        }

        private decimal TypeSaving(int resultMaxValue, string typeProcess = "bruto")
        {
            var resultSaving = new decimal(0);
            var desglosePagoHistoricoById =
                _desglosePagoHistoricoRepository.GetDesglosePagoHistoricoById(resultMaxValue);
            if (desglosePagoHistoricoById != null)
            {
                resultSaving = typeProcess == "bruto" ? desglosePagoHistoricoById.AhorroBruto ?? 0 : desglosePagoHistoricoById.AhorroNeto ?? 0;
            }
            return resultSaving;
        }

        private List<int> GetMaxJerarquiaDesgloseByPeriodYear(int idCentroCarga, Dictionary<string, string> dictPeriod)
        {
            var periodString = dictPeriod["year"];

            var periodsYear =
                _desglosePagoHistoricoRepository.GetPeriodsByCentroCargaAndPeriodYear(idCentroCarga, periodString);

            return (from period in periodsYear
                select _desglosePagoHistoricoRepository
                    .GetByCentroCargaPeriod(centroCarga: idCentroCarga, period: period)
                    .OrderByDescending(x => x.FechaActualizacion)
                    .ThenByDescending(x => x.AlgoritmoJerarquia)
                    .FirstOrDefault()
                into dataMaxJerarquiaDesglose
                where dataMaxJerarquiaDesglose != null
                select dataMaxJerarquiaDesglose.Id).ToList();
        }
    }
}