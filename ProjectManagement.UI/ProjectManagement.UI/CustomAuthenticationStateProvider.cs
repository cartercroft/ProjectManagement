using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ProjectManagement.Classes;
using ProjectManagement.Clients;
using ProjectManagement.Public.Models;
using ProjectManagement.UI.Components.Auth;

namespace ProjectManagement.UI
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider, ILoginManager
    {
        private AuthenticationState _authenticationState;
        private AuthClient _authClient;
        private ProtectedSessionStorage _session;
        private AuthService _authService;
        public CustomAuthenticationStateProvider(AuthService authService, AuthClient authClient, ProtectedSessionStorage sessionStorage)
        {
            _authClient = authClient;
            _authenticationState = new AuthenticationState(authService.CurrentUser);
            _authService = authService;
            _session = sessionStorage;

            authService.UserChanged += (newUser) =>
            {
                AuthenticationState = new AuthenticationState(newUser);
            };
        }
        private AuthenticationState AuthenticationState { 
            get { return _authenticationState; }
            set
            {
                _authenticationState = value;
                NotifyAuthenticationStateChanged(Task.FromResult(_authenticationState));
            }
        }
        public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
            Task.FromResult(_authenticationState);
        public async Task<bool> Login(string email, string password)
        {
            var response = await _authClient.SignIn(email, password);
            if(response != null 
                && response.Tokens != null
                && response.ClaimsPrincipal != null
                && response.Tokens.AccessToken != null)
            {
                _authService.CurrentUser = response.ClaimsPrincipal;
                AuthenticationState = new AuthenticationState(_authService.CurrentUser);
                
                await _session.SetAsync("AuthToken", response.Tokens.AccessToken);
                return true;
            }
            return false;
        }
        public async Task<Response> Register(RegisterRequestModel model)
        {
            return await _authClient.Register(model);
        }
    }
}
