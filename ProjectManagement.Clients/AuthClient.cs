
using ProjectManagement.Classes;
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
        public async Task<ClaimsPrincipal?> SignIn(string email, string password)
        {
            var response = await PostAsync<TokenProvider>("login", new {email = email, password = password});
            if(!response.IsSuccess || response.Result == null)
                return null;

            TokenProvider? tokenProvider = response.Result;
            var profileHeaders = new Dictionary<string, string>();
            profileHeaders.Add("Authorization", $"Bearer {tokenProvider?.AccessToken}");

            var userProfileRequest = await GetAsync<UserProfile>("Manage/Info", headers: profileHeaders);
            if (!userProfileRequest.IsSuccess || userProfileRequest.Result == null)
                return null;

            var userProfile = userProfileRequest.Result;
            return new ClaimsPrincipal(
                new ClaimsIdentity(userProfile
                .Claims
                .Select(c => new Claim(c.Key, c.Value))
                .ToList())   
            );
        }
    }
}
