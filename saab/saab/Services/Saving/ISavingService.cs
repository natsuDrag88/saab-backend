using System.Collections.Generic;
using saab.Dto;
using saab.Dto.Saving;
using saab.Model;

namespace saab.Services.Saving
{
    public interface ISavingService
    {
        public DesglosePagoHistorico GetDesglosePagoHistoricoById(int id);
        public TotalsProject GetSavingTotal(string period, int? client = null, string rpu = null, string typeProcess = "bruto");
        public int GetMaxJerarquiaDesglose(int idCentroCarga, Dictionary<string, string> dictPeriod);
        public decimal GetSavingTotalProject(List<CentrosDeCarga> listDataCentroCarga,
            Dictionary<string, string> dictPeriod, string typeProcess = "bruto");
    }
}