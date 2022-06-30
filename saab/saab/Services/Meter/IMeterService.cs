using System.Collections.Generic;
using saab.Dto.Meter;

namespace saab.Services.Meter
{
    public interface IMeterService
    {
        public List<DataMeter> GetMeter(string centroCarga);
    }
}