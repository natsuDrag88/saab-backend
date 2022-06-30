using System;
using System.Collections.Generic;
using System.Linq;
using saab.Data;
using saab.Dto.Concentrate;

namespace saab.Repository.DBMysql
{
    public class PeriodoBipCfeRepository : IPeriodoBipCfeRepository
    {
        private readonly SaabContext _context;

        public PeriodoBipCfeRepository(SaabContext context)
        {
            _context = context;
        }

        public List<ConcentratePeriodBip> GetConcentratePeriod(DateTime initialDate, DateTime finalDate,
            string[] typePeriod, string rateCfe)
        {
            return (from pbc in _context.PeriodosBipCves
                orderby pbc.Fecha
                where pbc.Fecha >= initialDate && pbc.Fecha <= finalDate
                where pbc.TarifaCfe == rateCfe
                where typePeriod.Contains(pbc.PeriodoCfe)
                select new ConcentratePeriodBip()
                {
                    Fecha = pbc.Fecha,
                    TipoPeriodo = pbc.PeriodoCfe
                }).ToList();
        }
    }
}