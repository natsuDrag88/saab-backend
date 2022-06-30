using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using saab.Dto.Billing;
using saab.Dto.Saving;
using saab.Model;

namespace saab.Repository
{
    public interface IFacturacionRepository
    {
        public List<InformationHistorical> GetHistoricalMonths(string centroCarga, IEnumerable<string> periods);
        public  List<Invoice> GetDataInvoiceById(List<string> idInvoice);
        public decimal? GetTotalBillingByProjectAndPeriod(string centroCarga, string period);
        public DateTime? GetDateOfIssueTotalBillingByProjectAndPeriod(string centroCarga, string period);
        public List<HistoricalBilling> GetHistoricalBillingsByClienteAndPeriod(int client,List<string> periodsDb);
        public Average GetDataAverage(string year, bool typeOperation);
        public List<Periods> GetDListPeriodsByYear(string year);
        public BillingTotal GetBillingTotal(string period);
        public Task<bool> GetDataAlertWithoutInvoiceIssuance(string periodString, int idCentroCarga);
        public List<HistoricalBilling> GetHistoricalBillingsByRpuAndPeriod(string[] listRpu, List<string> periods);
    }
}