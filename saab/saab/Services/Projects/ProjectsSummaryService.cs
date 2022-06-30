using System.Collections.Generic;
using System.Linq;
using saab.Dto.Project;
using saab.Repository;
using saab.Services.Billing;
using saab.Services.Saving;
using saab.Util.Project;

namespace saab.Services.Projects
{
    public class ProjectsSummaryService : IProjectsSummaryService
    {
        private readonly ISavingService _savingService;
        private readonly IBillingService _billingService;
        private readonly IProjectsService _projectsService;


        public ProjectsSummaryService(ISavingService savingService,
            IBillingService billingService, IProjectsService projectsService)
        {
            _savingService = savingService;
            _billingService = billingService;
            _projectsService = projectsService;
        }

        public List<SummaryModel> GetSummary(string period)
        {
            var listProjectClients = _projectsService.GetDataProjectsClientEnabled();
            var periodDatetime = DateUtil.ConvertPeriodToDate(period);
            var periodLast = DateUtil.ConvertDateToPeriod(periodDatetime.AddMonths(-1));

            return listProjectClients.Select(project => new SummaryModel
                {
                    cliente = project.Client,
                    rpu = project.Rpu,
                    ahorroBrutoMesActual = _savingService
                        .GetSavingTotal(period: period, client: project.IdClient, rpu: project.Rpu).total,
                    facturacionMesAnterior = _billingService
                        .GetInvoiceTotal(periodLast, client: project.IdClient, rpu: project.Rpu).total,
                    facturacionAnual = _billingService.GetInvoiceTotal(period.Substring(0, 4), rpu: project.Rpu).total
                })
                .ToList();
        }
    }
}