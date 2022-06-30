using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using saab.Dto.Concentrate;
using saab.Model;

namespace saab.Repository
{
    public interface IMedidorOperatiRepository
    {
        public List<ConcentrateTypePeriod> GetConcentratePeriodTypeOpe(DateTime initialDate, DateTime finalDate,
            string idMeter);
        public List<ConcentrateTypePeriodKwE> GetConcentratePeriodTypeOpeKwEs(DateTime initialDate, DateTime finalDate,
            string idMeter);
        public List<ConcentrateTypePeriodKwR> GetConcentratePeriodTypeOpPeriodKwRs(DateTime initialDate, DateTime finalDate,
            string idMeter);
    }
}