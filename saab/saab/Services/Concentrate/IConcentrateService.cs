using System.Collections.Generic;
using saab.Dto.Concentrate;

namespace saab.Services.Concentrate
{
    public interface IConcentrateService
    {
        public List<ConcentrateMonth> GetConcentrate(Dictionary<string, string> dictPeriod);
        public List<ConcentrateTypePeriod> GetConcentrateTypePeriod(InputConcentrateTypePeriod inputConcentrate);
        public List<ConcentratePeriodBip> GetConcentratePeriodBip(InputConcentratePeriodBip inputConcentrate);
    }
}