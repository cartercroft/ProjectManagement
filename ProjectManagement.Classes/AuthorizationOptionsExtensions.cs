using Microsoft.AspNetCore.Authorization;

namespace ProjectManagement.Classes
{
    public static class AuthorizationOptionsExtensions
    {
        public static void AddCustomPolicies(this AuthorizationOptions opt)
        {
            opt.AddPolicy(PolicyNames.SuperUserOnly,
                policy => policy.RequireRole(RoleNames.Admin, RoleNames.Owner));
        }
    }
}
