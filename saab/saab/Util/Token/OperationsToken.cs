using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace saab.Util.Token
{
    public class OperationsToken
    {
        
        private readonly string _authToken;
        
        public OperationsToken(string tokenBearer)
        {
            _authToken = tokenBearer.Split(' ').Last();
        }

        public Task<int> DecoderToken()
        {
            int clientId;
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(_authToken);
                var claims = jwtSecurityToken.Claims.ToList();
                var tokenInfo = claims.ToDictionary(claim => claim.Type, claim => claim.Value);

                clientId = int.Parse(tokenInfo["nameid"]);
            }
            catch (Exception)
            {
                clientId = 0;
            }
            return Task.FromResult(clientId);
        }
    }
}