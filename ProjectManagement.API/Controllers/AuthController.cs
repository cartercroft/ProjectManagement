using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.API.Services;
using ProjectManagement.Classes;
using ProjectManagement.Public.Models.Auth;
using System.IdentityModel.Tokens.Jwt;

namespace ProjectManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<JWTResponse> Login(LoginModel loginModel)
        {
            return await _authService.Login(loginModel);
        }

        [HttpPost]
        public async Task Register(LoginModel requestModel)
        {
            await _authService.Register(requestModel);
        }

        [HttpPost]
        public async Task AddEmailToRole(string email, string role)
        {
            await _authService.AddUserToRole(email, role);
        }
    }
}
