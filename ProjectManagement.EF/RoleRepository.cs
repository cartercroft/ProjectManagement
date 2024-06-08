using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProjectManagement.Models;
namespace ProjectManagement.EF
{
    public class RoleRepository : RoleStore<Role, ProjectManagementContext, Guid>
    {
        public RoleRepository(ProjectManagementContext context,
            IdentityErrorDescriber? describer = null) : base(context, describer)
        {
        }
    }
}
