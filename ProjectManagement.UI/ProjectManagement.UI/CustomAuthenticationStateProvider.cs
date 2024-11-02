using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ProjectManagement.Classes;
using ProjectManagement.Clients;
using ProjectManagement.Public.Models.Auth;
using ProjectManagement.UI.Components.Auth;
using System.Security.Claims;

namespace ProjectManagement.UI
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider, ILoginManager
    {
        private readonly AuthClient _authClient;
        private readonly ProtectedLocalStorage _localStorage;
        public CustomAuthenticationStateProvider(AuthClient authClient, ProtectedLocalStorage localStorage) : base()
        {
            _authClient = authClient;
            _localStorage = localStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = (await _localStorage.GetAsync<string>("AuthToken")).Value;
            var principal = JWTHelper.GetClaimsPrincipalFromToken(token, "jwt");
            return new AuthenticationState(principal);
        }
        public async Task<bool> Login(string email, string password)
        {
            var response = await _authClient.SignIn(email, password);
            if (response?.ClaimsPrincipal != null
               && !string.IsNullOrEmpty(response?.TokenInformation?.AuthToken))
            {
                await _localStorage.SetAsync("AuthToken", response.TokenInformation.AuthToken);
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                return true;
            }
            return false;
        }
        public async Task<Response> Register(RegisterRequestModel model)
        {
            return await _authClient.Register(model);
        }
        public async Task Logout()
        {
            Response response = await _authClient.Logout();
            if (response.IsSuccess)
            {
                await _localStorage.DeleteAsync("AuthToken");
            }
            else 
            {
                //TODO: Logging
            }
        }
    }
}
