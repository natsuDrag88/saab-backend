using System.Collections.Generic;
using saab.Dto.Meter;
using saab.Model;

namespace saab.Repository
{
    public interface IMedidorRepository
    {
        List<DataMeter> GetDataMeter(string centroCarga);
        public Medidore GetNameProvider(string idProvider);
        public RateMeter GetRateCfe(string idProvider);
        public Medidore GetIdProvider(string typeProvider, string typeSource);
    }
}