using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using saab.Dto.Auth;
using saab.Model.Request;

namespace saab.Services.AuthenticationSaab
{
    public interface IAuthenticationSaabService
    {
        public DataRequest GetAuthenticationSaabRequest(InputAuth autParams, IConfiguration section);

        public ResultTokenAuth GenerateToken(string username, string name, IConfiguration section);

    }
}