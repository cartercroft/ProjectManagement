
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ProjectManagement.Classes;
using ProjectManagement.Public.Models;
using ProjectManagement.Public.Models.Auth;

namespace ProjectManagement.Clients
{
    public class AuthClient : ProjectManagementClientBase
    {
        private readonly AuthService _authService;
        private ProtectedLocalStorage _localStorage;

        public AuthClient(IHttpClientFactory httpClientFactory,
            AuthService authService,
            ProtectedLocalStorage localStorage) : base(httpClientFactory, localStorage)
        {
            _authService = authService;
            _localStorage = localStorage;
        }
        public async Task<SignInResultModel> SignIn(string email, string password)
        {
            var result = new SignInResultModel();

            LoginModel loginModel = new LoginModel
            {
                Username = email,
                Password = password
            };

            var response = await PostAsync<JWTResponse>("login", loginModel);
            if(!response.IsSuccess || response.Result == null)
                return null;

            JWTResponse? jwtResponse = response.Result;

            if (jwtResponse != null
                && !string.IsNullOrEmpty(jwtResponse.AuthToken))
            {
                result.TokenInformation = jwtResponse;
                result.ClaimsPrincipal = JWTHelper.GetClaimsPrincipalFromToken(jwtResponse.AuthToken, "Default");
            }

            return result;
        }
        public async Task<Response> Register(RegisterRequestModel model)
        {
            return await PostAsync("Register", model);
        }
        public async Task<Response> Logout()
        {
            return await PostAsync("Logout", new { returnUrl = "/" });
        }
    }
}
