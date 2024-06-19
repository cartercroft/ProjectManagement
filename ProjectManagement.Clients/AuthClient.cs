
using ProjectManagement.Classes;
using ProjectManagement.Public.Models;
using System.Security.Claims;

namespace ProjectManagement.Clients
{
    public class AuthClient : ClientBase
    {
        private readonly AuthService _authService;
        public AuthClient(IHttpClientFactory httpClientFactory, AuthService authService) : base(httpClientFactory.CreateClient("ProjectManagementClient"))
        {
            _authService = authService;
        }
        public async Task<SignInResultModel> SignIn(string email, string password)
        {
            SignInResultModel result = new SignInResultModel();
            var response = await PostAsync<TokenProvider>("login", new {email = email, password = password});
            if(!response.IsSuccess || response.Result == null)
                return null;

            TokenProvider? tokenProvider = response.Result;
            result.Tokens = tokenProvider;

            var profileHeaders = new Dictionary<string, string>();
            profileHeaders.Add("Authorization", $"Bearer {tokenProvider?.AccessToken}");

            var userProfileRequest = await GetAsync<UserProfile>("Manage/Info", headers: profileHeaders);
            if (!userProfileRequest.IsSuccess || userProfileRequest.Result == null)
                return null;

            var userProfile = userProfileRequest.Result;
            result.ClaimsPrincipal = new ClaimsPrincipal(
                new ClaimsIdentity(userProfile
                .Claims
                .Select(c => new Claim(c.Key, c.Value))
                .ToList(), "Default")   
            );

            return result;
        }
        public async Task<Response> Register(RegisterRequestModel model)
        {
            return await PostAsync("Register", model);
        }
    }
}
