using System;
using System.Collections.Generic;
using saab.Dto;
using saab.Dto.Billing;
using saab.Dto.Saving;
using saab.Model;

namespace saab.Services.Billing
{
    public interface IBillingService
    {
        public TotalsProject GetInvoiceTotal(string period, int? client = null, string rpu = null);
        public TotalsProject GetEstimateToInvoiceTotal(string period);
        public List<BillingOverallSummary> GetSummaryByProjects(string period);
        public List<HistoricalBilling> GetHistoricalBillings(int client, string periodIni, string periodEnd);
        public  List<Invoice> GetInvoice(List<string> idInvoice);
        public byte[] GenerateByteZip(List<Invoice> dataInvoice, Guid uuid);

        public List<InformationHistorical> GetDataHistoricalSavingBill(string project, string periodStart,
            string periodEnd);

        public TotalsProject GetAverageSaving(string year, string operation);

        public List<BillItemizationOption> GetBillItemizationOptions();

        public List<BillsCfe> GetBillsCfeByPeriods(string periodStart, string periodEnd,
            int? project = null, string rpu = null);

        public List<HistoricalBilling> GetHistoricalRpuBillings(string[] listRpu, string periodIni, string periodEnd);
    }
}