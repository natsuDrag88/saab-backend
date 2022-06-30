using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using saab.Dto.Concentrate;
using saab.Model;

namespace saab.Repository
{
    public interface IPeriodoBipCfeRepository
    {
        public List<ConcentratePeriodBip> GetConcentratePeriod(DateTime initialDate, DateTime finalDate,
            string[] typePeriod, string rateCfe);
    }
}