using ProjectManagement.Classes;
using ProjectManagement.Public.Models;
using System.Security.Claims;

namespace ProjectManagement.UI.Components.Auth
{
    public interface ILoginManager
    {
        /// <summary>
        /// Tries to login with the given credentials and returns a flag of whether or not the login was successful.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>A flag of whether of not the login was successful.</returns>
        public Task<bool> Login(string username, string password);
        public Task<Response> Register(RegisterRequestModel model);
        public Task<Response> Logout();
    }
}
