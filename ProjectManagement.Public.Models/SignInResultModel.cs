using ProjectManagement.Classes;
using System.Security.Claims;

namespace ProjectManagement.Public.Models
{
    public class SignInResultModel
    {
        public ClaimsPrincipal? ClaimsPrincipal { get; set; }
        public TokenProvider? Tokens { get; set; }
    }
}
