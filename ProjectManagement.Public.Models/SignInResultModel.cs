using ProjectManagement.Public.Models.Auth;
using System.Security.Claims;

namespace ProjectManagement.Public.Models
{
    public class SignInResultModel
    {
        public JWTResponse TokenInformation { get; set; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
