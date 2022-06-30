using System.Collections.Generic;
using saab.Dto.Meter;
using saab.Repository;

namespace saab.Services.Meter
{
    public class MeterService : IMeterService
    {
        private readonly IMedidorRepository _medidorRepository;

        public MeterService(IMedidorRepository medidorRepository)
        {
            _medidorRepository = medidorRepository;
        }

        public List<DataMeter> GetMeter(string centroCarga)
        {
            return _medidorRepository.GetDataMeter(centroCarga: centroCarga);
        }
    }
}