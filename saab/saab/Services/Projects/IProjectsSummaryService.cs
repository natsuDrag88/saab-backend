using System.Collections.Generic;
using saab.Dto.Project;

namespace saab.Services.Projects
{
    public interface IProjectsSummaryService
    {
        List<SummaryModel> GetSummary(string period);
    }
}