using System.Collections.Generic;
using System.Linq;
using saab.Data;
using saab.Dto.Saving;

namespace saab.Repository.DBMysql
{
    public class ParametrosHistoricoRepository : IParametrosHistoricoRepository
    {
        private readonly SaabContext _context;

        public ParametrosHistoricoRepository(SaabContext context)
        {
            _context = context;
        }

        public List<Information> GetDataParametrosHistorico(string period, string project, int hierarchy,
            string[] listValues)
        {
            return (from item in (from x in _context.ParametrosHistoricos
                    where x.CentroDeCarga == int.Parse(project)
                    where x.Mes == period
                    where x.AlgoritmoJerarquia == hierarchy
                    select new Information()
                    {
                        Concepto = x.Concepto,
                        OperacionconSaaS = x.ConsumoConSaaS,
                        OperacionsinSaaS = x.ConsumoSinSaaS,
                        Ahorros = x.CantidadEvitada,
                        FechaActualizacion = x.FechaActualizacion
                    }).ToList()
                where listValues.Contains(item.Concepto)
                select item).ToList();
        }
    }
}