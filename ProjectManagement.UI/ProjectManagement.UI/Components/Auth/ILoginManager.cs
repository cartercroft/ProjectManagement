using LayerAbstractions;
using ProjectManagement.Classes;
using ProjectManagement.Public.Models.Auth;
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
        public Task<bool> Login(LoginModel model);
        public Task<Response> Register(LoginModel model);
        public Task Logout();
    }
}
