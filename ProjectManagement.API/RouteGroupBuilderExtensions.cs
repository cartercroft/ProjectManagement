using Microsoft.AspNetCore.Identity;
using ProjectManagement.Models;

namespace ProjectManagement.API
{
    public static class RouteGroupBuilderExtensions
    {
        public static RouteGroupBuilder ApplyCustomEndpointConfiguration(this RouteGroupBuilder builder)
        {
            builder.MapPost("/logout", async (SignInManager<User> signInManager) =>
            {
                await signInManager.SignOutAsync().ConfigureAwait(false);
            });
            return builder;
        } 
    }
}
