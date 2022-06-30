using System;
using System.Collections.Generic;
using System.Linq;
using saab.Data;
using saab.Dto.Concentrate;

namespace saab.Repository.DBMysql
{
    public class MedidorEtbRepository : IMedidorEtbRepository
    {
        private readonly SaabContext _context;

        public MedidorEtbRepository(SaabContext context)
        {
            _context = context;
        }
        
        public List<ConcentrateTypePeriod> GetConcentratePeriodTypeEtb(DateTime initialDate, DateTime finalDate,
            string idMeter)
        {
            return (from tm in _context.MedicionesEtbs
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

        public List<ConcentrateTypePeriodKwE> GetConcentratePeriodTypeEtbKwEs(DateTime initialDate, DateTime finalDate, string idMeter)
        {
            return (from tm in _context.MedicionesEtbs
                join m in _context.Medidores
                    on tm.Idmedidor equals m.Id
                join cc in _context.CentrosDeCargas
                    on m.CentroDeCarga equals cc.Id
                orderby tm.Fecha
                where tm.Fecha >= initialDate && tm.Fecha <= finalDate
                where m.Id == int.Parse(idMeter)
                select new ConcentrateTypePeriodKwE()
                {
                    Fecha = tm.Fecha,
                    KwhE = tm.KWhE,
                    Offset = tm.Offset
                }).ToList();
        }

        public List<ConcentrateTypePeriodKwR> GetConcentratePeriodTypeEtbKwRs(DateTime initialDate, DateTime finalDate, string idMeter)
        {
            return (from tm in _context.MedicionesEtbs
                join m in _context.Medidores
                    on tm.Idmedidor equals m.Id
                join cc in _context.CentrosDeCargas
                    on m.CentroDeCarga equals cc.Id
                orderby tm.Fecha
                where tm.Fecha >= initialDate && tm.Fecha <= finalDate
                where m.Id == int.Parse(idMeter)
                select new ConcentrateTypePeriodKwR()
                {
                    Fecha = tm.Fecha,
                    KwhR = tm.KWhR,
                    Offset = tm.Offset
                }).ToList();
        }
    }
}