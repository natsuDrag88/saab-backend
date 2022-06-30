using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using saab.Dto.Concentrate;
using saab.Model;

namespace saab.Repository
{
    public interface IMedidorEtbRepository
    {
        public List<ConcentrateTypePeriod> GetConcentratePeriodTypeEtb(DateTime initialDate, DateTime finalDate,
            string idMeter);
    }
}