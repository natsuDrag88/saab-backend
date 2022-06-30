using System.Collections.Generic;

namespace saab.Dto.Saving
{
    public class SourcesInformation
    {
        public List<List<Information>> ParametrosEnergeticos { set; get; }
        
        public List<List<Information>> CargosCfe { set; get; }
    }
}