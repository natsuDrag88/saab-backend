using System;
using System.Collections.Generic;
using System.Linq;
using saab.Data;
using saab.Dto.Concentrate;

namespace saab.Repository.DBMysql
{
    public class MedidorBessRepository : IMedidorBessRepository
    {
        private readonly SaabContext _context;

        public MedidorBessRepository(SaabContext context)
        {
            _context = context;
        }
        
        public List<ConcentrateTypePeriod> GetConcentratePeriodTypeBess(DateTime initialDate, DateTime finalDate,
            string idMeter)
        {
            return (from tm in _context.MedicionesBesses
                join m in _context.Medidores
                    on tm.Idmedidor equals m.Id
                join cc in _context.CentrosDeCargas
                    on m.CentroDeCarga equals cc.Id
                orderby tm.Fecha
                where tm.Fecha >= initialDate && tm.Fecha <= finalDate
                where m.Id == int.Parse(idMeter)
                select new ConcentrateTypePeriod()
                {
                    Fecha = tm.Fecha,
                    KwhE = tm.KWhE,
                    KwhR = tm.KWhR,
                    Offset = tm.Offset
                }).ToList();
        }
    }
}