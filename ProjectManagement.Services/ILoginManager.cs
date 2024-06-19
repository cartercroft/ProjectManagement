using ProjectManagement.Public.Models;
using System.Security.Claims;

namespace ProjectManagement.Classes
{
    public interface ILoginManager
    {
        public Task Login(string username, string password);
        public Task<Response> Register(RegisterRequestModel model);
    }
}
