using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using saab.Dto.Concentrate;
using saab.Model;

namespace saab.Repository
{
    public interface IMedidorBessRepository
    {
        public List<ConcentrateTypePeriod> GetConcentratePeriodTypeBess(DateTime initialDate, DateTime finalDate,
            string idMeter);
        public List<ConcentrateTypePeriodKwE> GetConcentratePeriodTypeBessKwEs(DateTime initialDate, DateTime finalDate,
            string idMeter);
        public List<ConcentrateTypePeriodKwR> GetConcentratePeriodTypeBessKwRs(DateTime initialDate, DateTime finalDate,
            string idMeter);
    }
}