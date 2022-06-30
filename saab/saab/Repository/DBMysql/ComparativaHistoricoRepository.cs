using System.Collections.Generic;
using System.Linq;
using saab.Data;
using saab.Dto.Saving;
using saab.Model;

namespace saab.Repository.DBMysql
{
    public class ComparativaHistoricoRepository : IComparativaHistoricoRepository
    {
        private readonly SaabContext _context;

        public ComparativaHistoricoRepository(SaabContext context)
        {
            _context = context;
        }

        public List<Information> GetDataComparativaHistorico(string period, string project, int hierarchy,
            string[] listValues)
        {
            return (from x in _context.ComparativaHistoricos
                where x.CentroDeCarga == int.Parse(project)
                where x.Mes == period
                where x.AlgoritmoJerarquia == hierarchy
                where (listValues.Contains((string)x.Cargo))
                select new Information()
                {
                    Concepto = x.Cargo,
                    OperacionconSaaS = x.CargoConSaas,
                    OperacionsinSaaS = x.CargoSinSaaS,
                    Ahorros = x.AhorroBruto,
                    FechaActualizacion = x.FechaActualizacion
                }).ToList();
        }

        public List<ComparativaHistorico> GetByPeriodAndProjectAndPosition(string period, int project, string position)
        {
            return _context.ComparativaHistoricos
                .Where(x => x.Mes == period)
                .Where(x => x.CentroDeCarga == project)
                .Where(x => x.Cargo == position)
                .ToList();
        }

        public decimal? GetGrossSavingById(int id)
        {
            return _context.ComparativaHistoricos
                .Where(x => x.Id == id)
                .Sum(x => x.AhorroBruto);
        }

    }
}