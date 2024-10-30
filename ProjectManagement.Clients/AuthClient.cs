
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http;
using ProjectManagement.Classes;
using ProjectManagement.Public.Models;
using ProjectManagement.Public.Models.Auth;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace ProjectManagement.Clients
{
    public class AuthClient : ProjectManagementClientBase
    {
        private readonly AuthService _authService;
        private ProtectedSessionStorage _session;

        public AuthClient(IHttpClientFactory httpClientFactory,
            AuthService authService,
            ProtectedSessionStorage session) : base(httpClientFactory, session)
        {
            _authService = authService;
            _session = session;
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

            await _session.SetAsync("AuthToken", jwtResponse.AuthToken);

            var securityToken = JWTHelper.GetTokenFromString(jwtResponse.AuthToken);
            result.ClaimsPrincipal = new ClaimsPrincipal(
            new ClaimsIdentity(securityToken?
            .Claims
            .Select(c => new Claim(c.Type, c.Value))
            .ToList(), "Default"));

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
