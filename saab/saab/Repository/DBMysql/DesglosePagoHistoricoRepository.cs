using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using saab.Data;
using saab.Dto.Alerts;
using saab.Model;


namespace saab.Repository.DBMysql
{
    public class DesglosePagoHistoricoRepository : IDesglosePagoHistoricoRepository
    {
        private readonly SaabContext _context;

        public DesglosePagoHistoricoRepository(SaabContext context)
        {
            _context = context;
        }

        public decimal? GetTotalById(int idDesglosePago)
        {
            return _context.DesglosePagoHistoricos
                .Where(x => x.Id == idDesglosePago)
                .Sum(x => x.Total);
        }

        public List<DesglosePagoHistorico> GetByCentroCargaPeriod(int centroCarga, string period)
        {
            return _context.DesglosePagoHistoricos.Where(x => x.CentroDeCarga == centroCarga)
                .Where(x => x.Mes.Contains(period)).ToList();
        }

        public List<string> GetPeriodsByCentroCargaAndPeriodYear(int centroCarga, string periodYear)
        {
            return _context.DesglosePagoHistoricos.Where(x => x.CentroDeCarga == centroCarga)
                .Where(x => x.Mes.Contains(periodYear))
                .GroupBy(x => (x.Mes))
                .Select(x => (x.Key)).ToList();
            return null;
        }

        public AlertUpdateDelay GetAlertUpdateDelay(int idCentroCarga, int idDesglosePagoHistoricos)
        {
            return (from cc in _context.CentrosDeCargas
                join c in _context.Clientes
                    on cc.Idcliente equals c.Id
                where cc.Id == idCentroCarga
                join dg in _context.DesglosePagoHistoricos
                    on cc.Id equals dg.CentroDeCarga
                where dg.Id == idDesglosePagoHistoricos
                select new AlertUpdateDelay
                {
                    Cliente = c.Alias,
                    Rpu = cc.Rpu,
                    MesOperacion = dg.Mes,
                    FechaActualizacion = dg.FechaActualizacion
                }).FirstOrDefault();
        }

        public DesglosePagoHistorico GetDesglosePagoHistoricoById(int id)
        {
            return _context.DesglosePagoHistoricos.FirstOrDefault(x => x.Id == id);
        }
    }
}