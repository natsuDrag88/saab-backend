using System;
using System.Collections.Generic;
using saab.Dto.Concentrate;

namespace saab.Repository
{
    public interface ICargaDescargaBateria
    {
        public List<ConcentrateBattery> GetConcentratePeriod(int idMeter, DateTime initialDate, DateTime finalDate);
    }
}