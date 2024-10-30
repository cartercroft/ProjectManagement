using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;

namespace ProjectManagement.Classes
{
    public static class JWTHelper
    {
        private static readonly JsonWebTokenHandler _handler = new JsonWebTokenHandler();
        public static JwtSecurityToken? GetTokenFromString(string tokenString)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(tokenString);
            return jsonToken as JwtSecurityToken;
        }
    }
}
