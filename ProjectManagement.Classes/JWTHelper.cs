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
            var claims = ReplaceBrokenIdentityClaims(token.Claims.ToList());
            return new ClaimsIdentity(claims, authenticationType);
        }
        public static ClaimsPrincipal GetClaimsPrincipalFromToken(string? tokenString, string authenticationType)
        {
            return new ClaimsPrincipal(GetClaimsIdentityFromToken(tokenString, authenticationType));
        }

        //Temporary hack until I figure out why the ClaimTypes.Role is getting changed to "role" from the API -> Client
        private static List<Claim>? ReplaceBrokenIdentityClaims(List<Claim>? claims)
        {
            Dictionary<string, string> claimTypeMap = new Dictionary<string, string>()
            {
                { "role", ClaimTypes.Role },
                { "unique_name", ClaimTypes.Name },
                { "nameidentifier", ClaimTypes.NameIdentifier }
            };
            var brokenClaims = new List<Claim>(claims?.Where(c => claimTypeMap.ContainsKey(c.Type)) ?? new List<Claim>());
            var fixedClaims = brokenClaims?.Select(c => new Claim(claimTypeMap[c.Type], c.Value));

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
        public static long GetTokenExpirationTime(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var tokenExp = jwtSecurityToken.Claims.First(claim => claim.Type.Equals("exp")).Value;
            var ticks = long.Parse(tokenExp);
            return ticks;
        }

        public static bool CheckTokenIsValid(string token)
        {
            var tokenTicks = GetTokenExpirationTime(token);
            var tokenDate = DateTimeOffset.FromUnixTimeSeconds(tokenTicks).UtcDateTime;

            var now = DateTime.Now.ToUniversalTime();

            var valid = tokenDate >= now;

            return valid;
        }
    }
}
