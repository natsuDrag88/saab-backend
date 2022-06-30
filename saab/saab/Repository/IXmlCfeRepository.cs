using System.Collections.Generic;
using saab.Dto.Billing;

namespace saab.Repository
{
    public interface IXmlCfeRepository
    {
        public bool GetDataAlertNoInvoice(string period, string rpu);
        public List<BillItemizationOption> GetBillItemizationOptions();

        public List<BillsCfe> GetBillCfeByPeriodsAndRpu(List<string> listPeriods, List<string> listRpu);
        
    }
}