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

            if(await HandleTokenIfExpired(token))
            {
                return new AuthenticationState(new ClaimsPrincipal());
            }

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
            //TODO: May need to implement some sort of token 'blacklist' that tokens get added to upon logout to prevent sessions from persisting past when a user needs them.
            await _localStorage.DeleteAsync("AuthToken");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        /// <summary>
        ///  Checkes whether or not a token is expired.
        /// </summary>
        /// <param name="token">The JWT token.</param>
        /// <returns>A flag indicating whether or not the token has expired. True = Token has expired, False = Token is still valid.</returns>
        private async Task<bool> HandleTokenIfExpired(string? token)
        {
            if (token is not null && !JWTHelper.CheckTokenIsValid(token))
            {
                await _localStorage.DeleteAsync("AuthToken");
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                return true;
            }
            return false;
        }
    }
}
