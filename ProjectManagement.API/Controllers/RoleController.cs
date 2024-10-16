using LayerAbstractions.Interfaces;
using ProjectManagement.Models;
using ProjectManagement.Public.Models;
using ProjectManagement.Repositories;
using ProjectManagement.Services;

namespace ProjectManagement.API.Controllers
{
    public class RoleController : IAPIController<Guid, RoleViewModel, Role, RoleService, RoleRepository>
    {
        private readonly RoleService _roleService;
        public RoleController(RoleService roleService) 
        {
            _roleService = roleService;
        }
    }
}
