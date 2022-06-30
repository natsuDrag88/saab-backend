using System;
using System.Collections.Generic;
using System.Linq;
using saab.Dto.Alerts;
using saab.Dto.Project;
using saab.Model;
using saab.Repository;
using saab.Services.Clients;
using saab.Services.Projects;
using saab.Services.Saving;
using saab.Util;
using saab.Util.Enum;
using saab.Util.Project;

namespace saab.Services.Alerts
{
    public class DetailAlertService : IDetailAlertService
    {
        private readonly ulong _status;
        private readonly IClientsService _clientsService;
        private readonly IProjectsService _projectsService;
        private readonly ISavingService _savingService;
        private readonly IFacturacionRepository _facturacionRepository;
        private readonly IDesglosePagoHistoricoRepository _desglosePagoHistoricoRepository;
        private readonly IXmlCfeRepository _xmlCfeRepository;
        private readonly ICuadroTarifarioRepository _cuadroTarifarioRepository;

        public DetailAlertService(
            IClientsService clientsService,
            IProjectsService projectsService, ISavingService savingService,
            ICuadroTarifarioRepository cuadroTarifarioRepository,
            IDesglosePagoHistoricoRepository desglosePagoHistoricoRepository,
            IFacturacionRepository facturacionRepository, IXmlCfeRepository xmlCfeRepository)
        {
            _clientsService = clientsService;
            _projectsService = projectsService;
            _savingService = savingService;
            _cuadroTarifarioRepository = cuadroTarifarioRepository;
            _desglosePagoHistoricoRepository = desglosePagoHistoricoRepository;
            _facturacionRepository = facturacionRepository;
            _xmlCfeRepository = xmlCfeRepository;
            _status = Convert.ToUInt64((int)Enum.Parse(typeof(Status), Status.activo.ToString()));
        }

        public AlertDetail GetAlertDetail(string period)
        {
            var listAlert = GetListAlertDetail(period);
            var alertResult = new AlertDetail
            {
                AhorrosMenoresEsperados = new List<AlertLowerExpectedSavings>(),
                RetrasoActualizacion = new List<AlertUpdateDelay>(),
                SinFacturaCfe = new List<AlertWithoutCfeInvoice>(),
                SinEmisionFactura = new List<AlertWithoutInvoiceIssuance>(),
                RetrasoTarifaCfe = new List<AlertCfeRateDelay>(),
            };
            if (listAlert.Count <= 0) return null;
            foreach (var alert in listAlert)
            {
                if (alert.RetrasoActualizacion != null)
                    alertResult.RetrasoActualizacion.Add(alert.RetrasoActualizacion);
                if (alert.AhorrosMenoresEsperados != null)
                    alertResult.AhorrosMenoresEsperados.Add(alert.AhorrosMenoresEsperados);
                if (alert.RetrasoTarifaCfe != null)
                    alertResult.RetrasoTarifaCfe.Add(alert.RetrasoTarifaCfe);
                if (alert.SinEmisionFactura != null)
                    alertResult.SinEmisionFactura.Add(alert.SinEmisionFactura);
                if (alert.SinFacturaCfe != null)
                    alertResult.SinFacturaCfe.Add(alert.SinFacturaCfe);
            }

            return alertResult;
        }

        private List<GetResulAlerts> GetListAlertDetail(string period)
        {
            var dictPeriod = DateUtil.GetDictPeriod(period: period);
            var dictPeriodBilling =  DateUtil.GetDictPeriod(DateUtil.ConvertDateToPeriod(DateUtil.ConvertPeriodToDate(period).AddMonths(-1)));

            var listClient = _clientsService.GetListLightWeight(Status.activo);
            var periodString = string.IsNullOrEmpty(dictPeriod["month"])
                ? dictPeriod["year"]
                : DateUtil.ConvertMonthToString(month: int.Parse(dictPeriod["month"])) + "_" + dictPeriod["year"];
            var periodBillingString = string.IsNullOrEmpty(dictPeriodBilling["month"])
                ? dictPeriodBilling["year"]
                : DateUtil.ConvertMonthToString(month: int.Parse(dictPeriodBilling["month"])) + "_" + dictPeriodBilling["year"];
            var alerts = new List<GetResulAlerts>();
            foreach (var client in listClient)
            {
                var dataProjects = _projectsService.GetProjectsByStatusOptionalFilters(1, client: client.Id);
                alerts.AddRange(from project in dataProjects
                    let dataMaxJerarquiaDesgloseHistorico =
                        _savingService.GetDesglosePagoHistoricoById(
                            _savingService.GetMaxJerarquiaDesglose(project.Id, dictPeriod))
                    where dataMaxJerarquiaDesgloseHistorico != null
                    let alertMinorSaving = GetAlertMinorSaving(desglose: dataMaxJerarquiaDesgloseHistorico,
                        centroCarga: project, client: client)
                    let alertUpdateDelay =
                        GetAlertUpdateDelay(desglose: dataMaxJerarquiaDesgloseHistorico, centroCarga: project)
                    let alertNoInvoiceCfe = GetAlertNoInvoiceCfe(client: client, centroCarga: project,
                        dictPeriod: dictPeriodBilling, periodString: periodBillingString)
                    let alertNoInvoice =
                        GetAlertNoInvoice(client: client, centroCarga: project, periodString: periodBillingString)
                    let alertFareDelayCfe = GetAlertFareDelayCfe(periodString: periodString)
                    select new GetResulAlerts
                    {
                        AhorrosMenoresEsperados = alertMinorSaving,
                        RetrasoActualizacion = alertUpdateDelay,
                        SinFacturaCfe = alertNoInvoiceCfe,
                        SinEmisionFactura = alertNoInvoice,
                        RetrasoTarifaCfe = alertFareDelayCfe
                    });
            }

            return alerts;
        }


        public List<AlertGeneralModel> GetListAlertGeneral(string period)
        {
            var listAlert = GetListAlertDetail(period);
            var dictionaryAlert = new Dictionary<string, int>();

            foreach (var alert in listAlert)
            {
                if (alert.AhorrosMenoresEsperados != null)
                    dictionaryAlert = GenericFunctions.CountKeyValue(dictionaryAlert, "AhorrosMenoresEsperados");
                if (alert.RetrasoActualizacion != null)
                    dictionaryAlert = GenericFunctions.CountKeyValue(dictionaryAlert, "RetrasoActualizacion");
                if (alert.SinEmisionFactura != null)
                    dictionaryAlert = GenericFunctions.CountKeyValue(dictionaryAlert, "SinEmisionFactura");
                if (alert.SinFacturaCfe != null)
                    dictionaryAlert = GenericFunctions.CountKeyValue(dictionaryAlert, "SinFacturaCfe");
                if (alert.RetrasoTarifaCfe != null)
                    dictionaryAlert = GenericFunctions.CountKeyValue(dictionaryAlert, "RetrasoTarifaCfe");
            }

            var countAlerts = dictionaryAlert
                .Select(n => new AlertGeneralModel { Alerta = n.Key, Cantidad = n.Value }).ToList();
            return countAlerts;
        }


        private AlertUpdateDelay GetAlertUpdateDelay(DesglosePagoHistorico desglose, CentrosDeCarga centroCarga)
        {
            AlertUpdateDelay resultAlertDetail = null;
            var dataAlert =
                _desglosePagoHistoricoRepository.GetAlertUpdateDelay(idCentroCarga: centroCarga.Id, desglose.Id);
            var dateDifference = (DateTime.UtcNow - dataAlert.FechaActualizacion);
            if (dateDifference is { Days: > 5 })
            {
                resultAlertDetail = dataAlert;
            }

            return resultAlertDetail;
        }

        private AlertWithoutInvoiceIssuance GetAlertNoInvoice(ClientLightWeight client, CentrosDeCarga centroCarga,
            string periodString)
        {
            AlertWithoutInvoiceIssuance resultAlertDetail = null;
            var dataAlert =
                _facturacionRepository.GetDataAlertWithoutInvoiceIssuance(periodString: periodString,
                    idCentroCarga: centroCarga.Id);
            if (dataAlert.Result == false)
            {
                resultAlertDetail = new AlertWithoutInvoiceIssuance
                {
                    Cliente = client.Cliente,
                    Rpu = centroCarga.Rpu,
                    MesFacturacion = periodString,
                };
            }

            return resultAlertDetail;
        }

        private AlertCfeRateDelay GetAlertFareDelayCfe(string periodString)
        {
            AlertCfeRateDelay resultAlertDetail = null;
            var dataAlert = _cuadroTarifarioRepository.GetAlertFareDelayCfe(periodString: periodString);
            if (dataAlert == false)
            {
                resultAlertDetail = new AlertCfeRateDelay
                {
                    SinRegistroTarifarioCfe = periodString
                };
            }

            return resultAlertDetail;
        }

        private AlertWithoutCfeInvoice GetAlertNoInvoiceCfe(ClientLightWeight client, CentrosDeCarga centroCarga,
            IReadOnlyDictionary<string, string> dictPeriod, string periodString)
        {
            AlertWithoutCfeInvoice resultAlertDetail = null;
            var lastPeriodString = dictPeriod["year"] + DateUtil.ConvertStringMonthToInt(period: periodString);

            var dataAlert = _xmlCfeRepository.GetDataAlertNoInvoice(period: lastPeriodString, rpu: centroCarga.Rpu);
            if (dataAlert == false)
            {
                resultAlertDetail = new AlertWithoutCfeInvoice
                {
                    Cliente = client.Cliente,
                    Rpu = centroCarga.Rpu,
                    MesFacturacion = periodString
                };
            }

            return resultAlertDetail;
        }

        private AlertLowerExpectedSavings GetAlertMinorSaving(DesglosePagoHistorico desglose,
            CentrosDeCarga centroCarga, ClientLightWeight client)
        {
            AlertLowerExpectedSavings summaryAlert = null;
            if ((desglose.AhorroBruto <= centroCarga.AhorroMinimoMensual))
            {
                summaryAlert = new AlertLowerExpectedSavings
                {
                    Cliente = client.Cliente,
                    Rpu = centroCarga.Rpu,
                    AhorroMinimoEsperado = centroCarga.AhorroMinimoMensual,
                    AhorroCalculado = desglose.AhorroBruto,
                    FechaActual = desglose.FechaActualizacion
                };
            }

            return summaryAlert;
        }
    }
}