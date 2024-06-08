using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProjectManagement.Models;

namespace ProjectManagement.EF
{
    public class UserRepository : UserStore<User, Role, ProjectManagementContext, Guid>
    {
        public UserRepository(ProjectManagementContext context,
            IdentityErrorDescriber? describer = null) : base(context, describer)
        {
        }
    }
}
