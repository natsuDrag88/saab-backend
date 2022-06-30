using System.Collections.Generic;
using saab.Dto;
using saab.Dto.Saving;
using saab.Dto.Comparison;

namespace saab.Services.Hierarchy
{
    public interface IHierarchyService
    {
        public List<List<Information>> GetEnergyParametersHierarchy(string period,
            string project, int hierarchy1, int hierarchy2);

        public List<List<Information>> GetChargesCfe(string period, string project
            , int hierarchy1, int hierarchy2);

        public List<HierarchyDto> GetHierarchies();
        public TotalsProject GetTotalSavingByPosition(string period, int project, string position);
        
    }
}