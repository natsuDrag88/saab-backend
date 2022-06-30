using System.Net.Http;

namespace saab.Model.Request
{
    public class DataRequest
    {
        public string urlService { get; set; }
        
        public StringContent dataService { get; set; }
    }
}