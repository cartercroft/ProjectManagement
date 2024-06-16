using Microsoft.AspNetCore.Components.Authorization;
using ProjectManagement.Classes;
using ProjectManagement.Clients;

namespace ProjectManagement.UI
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider, ILoginManager
    {
        private AuthenticationState _authenticationState;
        private AuthClient _authClient;
        public CustomAuthenticationStateProvider(AuthService service, AuthClient authClient)
        {
            _authClient = authClient;
            _authenticationState = new AuthenticationState(service.CurrentUser);

            service.UserChanged += (newUser) =>
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
        public async Task Login(string email, string password)
        {
            var response = await _authClient.SignIn(email, password);
            if(response != null)
            {
                AuthenticationState = new AuthenticationState(response);
            }
        }
    }
}
