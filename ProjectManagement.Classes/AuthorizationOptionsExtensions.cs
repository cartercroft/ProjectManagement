using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
