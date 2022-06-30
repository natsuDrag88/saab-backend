using System.Collections.Generic;
using saab.Dto.Alerts;
using saab.Model;

namespace saab.Repository
{
    public interface IDesglosePagoHistoricoRepository
    {
        public decimal? GetTotalById(int idDesglosePago);
        public List<DesglosePagoHistorico> GetByCentroCargaPeriod(int centroCarga, string period);
        public DesglosePagoHistorico GetDesglosePagoHistoricoById(int id);
        public List<string> GetPeriodsByCentroCargaAndPeriodYear(int centroCarga, string periodYear);
        public AlertUpdateDelay GetAlertUpdateDelay(int idCentroCarga, int idDesglosePagoHistoricos);
    }
}