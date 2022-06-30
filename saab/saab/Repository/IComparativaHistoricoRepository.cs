using System.Collections.Generic;
using saab.Dto.Saving;
using saab.Model;

namespace saab.Repository
{
    public interface IComparativaHistoricoRepository
    {
        public List<Information> GetDataComparativaHistorico(string period, string project, int hierarchy,
            string[] listValues);

        public List<ComparativaHistorico> GetByPeriodAndProjectAndPosition(string period, int project, string position);
        public decimal? GetGrossSavingById(int id);
    }
}