
using ProjectManagement.Classes;
using System.Security.Claims;

namespace ProjectManagement.Clients
{
    public class AuthClient : ProjectManagementClientBase
    {
        private readonly AuthService _authService;
        public AuthClient(IHttpClientFactory httpClientFactory, AuthService authService) : base(httpClientFactory, "")
        {
            _authService = authService;
        }
        public async Task SignIn(string email, string password)
        {
            var response = await PostAsync<TokenProvider>("login", new {email = email, password = password});
            if(response.IsSuccess)
            {
                TokenProvider? tokenProvider = response.Result;
                var profileHeaders = new Dictionary<string, string>();
                profileHeaders.Add("Authorization", $"Bearer {tokenProvider?.AccessToken}");
                var userProfileRequest = await GetAsync<UserProfile>("Manage/Info", headers: profileHeaders);
                if (userProfileRequest.IsSuccess)
                {
                    var userProfile = userProfileRequest.Result;
                    var identity = new ClaimsIdentity(
                        new[]
                        {
                            new Claim(ClaimTypes.Name, userProfile.Username)
                        },
                        "Custom Authentication");
                    var newUser = new ClaimsPrincipal(identity);
                    _authService.CurrentUser = newUser;
                }
            }
        }
    }
}
