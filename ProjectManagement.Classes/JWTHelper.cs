using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProjectManagement.Classes
{
    public static class JWTHelper
    {
        private static JwtSecurityToken? GetTokenFromString(string? tokenString)
        {
            if (string.IsNullOrEmpty(tokenString))
            {
                return null;
            }
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(tokenString);
        }
        public static ClaimsIdentity GetClaimsIdentityFromToken(string? tokenString, string authenticationType)
        {
            var token = GetTokenFromString(tokenString);
            if (token is null)
            {
                return new ClaimsIdentity();
            }
            var claims = ReplaceBrokenRoleClaims(token.Claims.ToList());
            return new ClaimsIdentity(claims, authenticationType);
        }
        public static ClaimsPrincipal GetClaimsPrincipalFromToken(string? tokenString, string authenticationType)
        {
            return new ClaimsPrincipal(GetClaimsIdentityFromToken(tokenString, authenticationType));
        }

        //Temporary hack until I figure out why the ClaimTypes.Role is getting changed to "role" from the API -> Client
        private static List<Claim>? ReplaceBrokenRoleClaims(List<Claim>? claims)
        {
            var brokenClaims = new List<Claim>(claims?.Where(c => c.Type.Equals("role", StringComparison.CurrentCultureIgnoreCase)));
            var fixedClaims = brokenClaims?.Select(c => new Claim(ClaimTypes.Role, c.Value));

            if (fixedClaims?.Any() ?? false)
            {
                foreach(var claim in brokenClaims)
                {
                    claims.Remove(claim);
                }

                claims.AddRange(fixedClaims);
            }
            return claims;
        }
    }
}
