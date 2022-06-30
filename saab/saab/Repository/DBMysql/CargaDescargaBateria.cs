using System;
using System.Collections.Generic;
using System.Linq;
using saab.Data;
using saab.Dto.Concentrate;
using saab.Util.Enum;

namespace saab.Repository.DBMysql
{
    public class CargaDescargaBateria : ICargaDescargaBateria
    {
        private readonly SaabContext _context;

        public CargaDescargaBateria(SaabContext context)
        {
            _context = context;
        }

        public List<ConcentrateBattery> GetConcentratePeriod(int idMeter, DateTime initialDate, DateTime finalDate)
        {
            return (from cdb in _context.CargaDescargaBateria
                where cdb.Idmedidor == idMeter
                where cdb.FechaNormal >= initialDate && cdb.FechaNormal <= finalDate
                select new ConcentrateBattery()
                {
                    Fecha = cdb.FechaNormal,
                    BatterySoc = cdb.BatterySoc,
                    Offset = cdb.Offset
                }).ToList();
        }
    }
}