using LayerAbstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ProjectManagement.Classes;
using ProjectManagement.Models;
using ProjectManagement.Public.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectManagement.API.Services
{
    //https://ravindradevrani.medium.com/net-7-jwt-authentication-and-role-based-authorization-5e5e56979b67
    public interface IAuthService
    {
        Task<Response> Register(LoginModel model);
        Task<JWTResponse> Login(LoginModel model);
        Task AddUserToRole(string email, string role);
    }
    public class AuthService : IAuthService
    {
        private const int __TOKEN_EXPIRY_MINUTES__ = 30;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthService(UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;

        }
        public async Task<Response> Register(LoginModel model)
        {
            Response response = new Response();
            var userExists = await _userManager.FindByEmailAsync(model.Username);
            if (userExists != null)
            {
                response.ErrorMessage = "A user with that email already exists.";
                return response;
            }

            User user = new User()
            {
                Email = model.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var createUserResult = await _userManager.CreateAsync(user, model.Password);
            if (!createUserResult.Succeeded)
            {
                response.ErrorMessage = "User creation failed. Please contact a developer.";
                return response;
            }

            await AddUserToRole(user, RoleNames.Default);

            return response;
        }
        public async Task<JWTResponse> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Username);
            if (user == null
                || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new JWTResponse();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.Email),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            JWTResponse jwt = new JWTResponse();
            jwt.AuthToken = GenerateToken(authClaims);
            return jwt;
        }
        public async Task AddUserToRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await AddUserToRole(user, roleName);
            }
        }
        public async Task AddUserToRole(User user, string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                Role role = new Role();
                role.CreatedWhen = DateTime.Now;
                role.UpdatedWhen = DateTime.Now;
                role.Name = roleName;
                await _roleManager.CreateAsync(role);
            }
           await _userManager.AddToRoleAsync(user, roleName);
        }
        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(__TOKEN_EXPIRY_MINUTES__),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims, "jwt")
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}