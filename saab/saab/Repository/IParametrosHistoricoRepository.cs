using System.Collections.Generic;
using saab.Dto.Saving;

namespace saab.Repository
{
    public interface IParametrosHistoricoRepository
    {
        public List<Information> GetDataParametrosHistorico(string period, string project, int hierarchy,
            string[] listValues);

    }
}