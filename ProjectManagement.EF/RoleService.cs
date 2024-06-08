using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ProjectManagement.Models;

namespace ProjectManagement.EF
{
    public class RoleService : RoleManager<Role>
    {
        public RoleService(IRoleStore<Role> store,
            IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<RoleManager<Role>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }
}
