using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Sockets;
using saab.Dto;
using saab.Dto.Billing;
using saab.Dto.Saving;
using saab.Model;
using saab.Repository;
using saab.Services.Projects;
using saab.Services.Saving;
using saab.Util.Enum;
using saab.Util.Project;

namespace saab.Services.Billing
{
    public class BillingService : IBillingService
    {
        private readonly IProjectsService _projectsService;
        private readonly ISavingService _savingService;
        private readonly IDesglosePagoHistoricoRepository _desglosePagoHistoricoRepository;
        private readonly IFacturacionRepository _facturacionRepository;
        private readonly IXmlCfeRepository _xmlCfeRepository;

        public BillingService(IDesglosePagoHistoricoRepository desglosePagoHistoricoRepository,
            ISavingService savingService, IProjectsService projectsService,
            IFacturacionRepository facturacionRepository, IXmlCfeRepository xmlCfeRepository)
        {
            _desglosePagoHistoricoRepository = desglosePagoHistoricoRepository;
            _savingService = savingService;
            _projectsService = projectsService;
            _facturacionRepository = facturacionRepository;
            _xmlCfeRepository = xmlCfeRepository;
        }

        public TotalsProject GetInvoiceTotal(string period, int? client, string rpu)
        {
            var projects = _projectsService.GetProjectsByStatusOptionalFilters(1, client: client, rpu: rpu);
            decimal? total = new decimal(0.0);
            total = projects.Aggregate(total,
                (current, project) => current + GetInvoiceTotalByPeriodAndProject(project.Id, period));

            return new TotalsProject { unidad = "Moneda", total = total };
        }

        private decimal? GetInvoiceTotalByPeriodAndProject(int project, string period)
        {
            var dictPeriod = DateUtil.GetDictPeriod(period: period);
            var periodString = string.IsNullOrEmpty(dictPeriod["month"])
                ? dictPeriod["year"]
                : DateUtil.ConvertMonthToString(month: int.Parse(dictPeriod["month"])) + "_" + dictPeriod["year"];
            var total = _facturacionRepository.GetTotalBillingByProjectAndPeriod(project.ToString(),
                periodString);
            return total;
        }


        public TotalsProject GetEstimateToInvoiceTotal(string period)
        {
            var dictPeriod = DateUtil.GetDictPeriod(period: period);
            var projects = _projectsService.GetProjectsByStatusOptionalFilters(1);
            var estimateTotal = new decimal(0.0);
            estimateTotal = projects.Select(project => _savingService.GetMaxJerarquiaDesglose(project.Id, dictPeriod))
                .Select(idDp => _desglosePagoHistoricoRepository.GetTotalById(idDp))
                .Where(total => total.HasValue)
                .Aggregate(estimateTotal, (current, total) => (decimal)(current + total));
            return new TotalsProject { unidad = "Moneda", total = estimateTotal };
        }

        public List<BillingOverallSummary> GetSummaryByProjects(string period)
        {
            var dictPeriodBreakDown =
                DateUtil.GetDictPeriod(DateUtil.ConvertDateToPeriod(DateUtil.ConvertPeriodToDate(period).AddMonths(1)));

            var dictPeriod = DateUtil.GetDictPeriod(period: period);
            var periodString = DateUtil.ConvertMonthToString(month: int.Parse(dictPeriod["month"])) + "_" +
                               dictPeriod["year"];
            var projects = _projectsService.GetDataProjectsClientEnabled();

            return (from project in projects
                let maxJerarquia = _savingService.GetMaxJerarquiaDesglose(project.IdCentroCarga, dictPeriodBreakDown)
                let dateOfIssue =
                    _facturacionRepository.GetDateOfIssueTotalBillingByProjectAndPeriod(
                        project.IdCentroCarga.ToString(), periodString)
                where dateOfIssue.HasValue
                select new BillingOverallSummary
                {
                    Cliente = project.Client,
                    Rpu = project.Rpu,
                    MontoFacturado =
                        GetInvoiceTotal(period: period, client: project.IdClient, rpu: project.Rpu).total ?? 0,
                    FechaEmisionUltimaFactura = (DateTime)dateOfIssue,
                    FechaLimitePago = ((DateTime)dateOfIssue).AddDays(project.PaymentDeadlineDays ?? 0),
                    FacturacionHistorica = GetInvoiceTotalByPeriodAndProject(project.IdCentroCarga, period[..4]) ?? 0,
                    EstimacionMesActual = _desglosePagoHistoricoRepository.GetTotalById(maxJerarquia) ?? 0
                }).ToList();
        }

        public List<HistoricalBilling> GetHistoricalBillings(int client, string periodIni, string periodEnd)
        {
            var ini = DateUtil.ConvertPeriodToDate(periodIni);
            var end = DateUtil.ConvertPeriodToDate(periodEnd);
            var list = DateUtil.ListBetweenTwoDates(ini, end);

            var periodsDb = list.Select(DateUtil.ConvertDateToPeriodCustomDb).ToList();


            return _facturacionRepository.GetHistoricalBillingsByClienteAndPeriod(client, periodsDb);
        }
        
        public List<HistoricalBilling> GetHistoricalRpuBillings(string[] listRpu, string periodIni, string periodEnd)
        {
            var ini = DateUtil.ConvertPeriodToDate(periodIni);
            var end = DateUtil.ConvertPeriodToDate(periodEnd);
            var list = DateUtil.ListBetweenTwoDates(ini, end);

            var periodsDb = list.Select(DateUtil.ConvertDateToPeriodCustomDb).ToList();


            return _facturacionRepository.GetHistoricalBillingsByRpuAndPeriod(listRpu: listRpu, periods: periodsDb);
        }

        public List<Invoice> GetInvoice(List<string> idInvoice)
        {
            var dataInvoice = _facturacionRepository.GetDataInvoiceById(idInvoice: idInvoice);
            return dataInvoice;
        }

        public byte[] GenerateByteZip(List<Invoice> dataInvoice, Guid uuid)
        {
            var nameTemporal = Path.GetRandomFileName();
            
            var pathResources =
                Path.Combine(Environment.CurrentDirectory);
            var pathFiles = Path.Combine(pathResources, $"{uuid.ToString()}-{nameTemporal}");
            var pathZip = Path.Combine(pathResources, $"{uuid.ToString()}-{nameTemporal}.zip");
            SaveFiles(dataInvoice: dataInvoice, pathFiles: pathFiles, uuid: uuid);
            GenerateZip(pathFiles: pathFiles, pathZip: pathZip);
            var byteZip = GenerateByte(pathZip: pathZip);
            DeleteFiles(pathFiles: pathFiles, pathZip: pathZip);
            return byteZip;
        }

        public List<InformationHistorical> GetDataHistoricalSavingBill(string project, string periodStart,
            string periodEnd)
        {
            var periodStartDate = DateUtil.ConvertPeriodToDate(periodStart);
            var periodEndDate = DateUtil.ConvertPeriodToDate(periodEnd);
            var listDates = DateUtil.ListBetweenTwoDates(periodStartDate, periodEndDate);
            var listPeriodDb = listDates.Select(DateUtil.ConvertDateToPeriodCustomDb);
            return _facturacionRepository.GetHistoricalMonths(centroCarga: project, periods: listPeriodDb);
        }


        public TotalsProject GetAverageSaving(string year, string operation)
        {
            var typeOperation = new Dictionary<string, string>
            {
                { "neto", "true" },
                { "bruto", "false" }
            };
            if (!typeOperation.TryGetValue(operation, out var entry)) return null;
            var boolOperation = Convert.ToBoolean(entry);
            var avg = _facturacionRepository.GetDataAverage(year: year, typeOperation: boolOperation);
            var total = (decimal?)0;
            if (avg != null)
                total = avg.ResultAverage;
            return new TotalsProject { unidad = "energy", total = total };
        }


        public List<BillItemizationOption> GetBillItemizationOptions()
        {
            return _xmlCfeRepository.GetBillItemizationOptions();
        }

        public List<BillsCfe> GetBillsCfeByPeriods(string periodStart, string periodEnd,
            int? project, string rpu = null)
        {
            const ulong status = (ulong)1;
            var periodStartDate = DateUtil.ConvertPeriodToDate(periodStart);
            var periodEndDate = DateUtil.ConvertPeriodToDate(periodEnd);
            var listDates = DateUtil.ListBetweenTwoDates(periodStartDate, periodEndDate);
            var listPeriodDb = listDates.Select(DateUtil.ConvertDateToPeriod).ToList();
            var projects = _projectsService.GetProjectsByStatusOptionalFilters(statusCentroCarga: status,
                project: project,
                rpu: rpu);
            var listRpu = projects.Select(x => x.Rpu).ToList();

            return _xmlCfeRepository.GetBillCfeByPeriodsAndRpu(listRpu: listRpu, listPeriods: listPeriodDb);
        }

        private void DeleteFiles(string pathFiles, string pathZip)
        {
            if (Directory.Exists(pathFiles))
            {
                Directory.Delete(pathFiles, true);
            }

            if (File.Exists(pathZip))
            {
                File.Delete(pathZip);
            }
        }

        private byte[] GenerateByte(string pathZip)
        {
            var fileBytes = new byte[] { };
            if (File.Exists(pathZip))
            {
                fileBytes = File.ReadAllBytes(pathZip);
            }

            return fileBytes;
        }

        private void GenerateZip(string pathFiles, string pathZip)
        {
            ZipFile.CreateFromDirectory(pathFiles, pathZip);
        }

        private void SaveFiles(List<Invoice> dataInvoice, string pathFiles, Guid uuid)
        {
            Directory.CreateDirectory(pathFiles);
            foreach (var invoice in dataInvoice)
            {
                var pathPdf = Path.Combine(pathFiles,uuid + "_" + invoice.Periodo + ".pdf");
                var pathXml = Path.Combine(pathFiles,uuid + "_" + invoice.Periodo + ".xml");
            
                File.WriteAllBytes(pathPdf, invoice.FacturaPdf);
                File.WriteAllBytes(pathXml, invoice.FacturaXml);
            }
        }


    }
}