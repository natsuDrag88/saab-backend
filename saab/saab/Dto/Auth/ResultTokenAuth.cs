using Microsoft.IdentityModel.Tokens;

namespace saab.Dto.Auth
{
    public class ResultTokenAuth
    {
        public string token { get; set; }
        public string username { get; set; }
        public string name { get; set; }
    }
}