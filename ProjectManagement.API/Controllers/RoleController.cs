using LayerAbstractions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Classes;
using ProjectManagement.Models;
using ProjectManagement.Public.Models;
using ProjectManagement.Repositories;
using ProjectManagement.Services;

namespace ProjectManagement.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    [Authorize(Policy = PolicyNames.SuperUserOnly)]
    public class RoleController : ControllerBase, IAPIController<Guid, RoleViewModel, Role, RoleService, RoleRepository>
    {
        private readonly RoleService _roleService;
        public RoleController(RoleService roleService) 
        {
            _roleService = roleService;
        }

        [HttpPost]
        public void Delete(RoleViewModel viewModel)
        {
            _roleService.Delete(viewModel);
        }

        [HttpGet]
        public RoleViewModel Get(Guid id)
        {
            return _roleService.Get(id);
        }

        [HttpGet]
        public IEnumerable<RoleViewModel> GetAll()
        {
            return _roleService.GetAll();
        }

        [HttpPost]
        public RoleViewModel Save(RoleViewModel viewModel)
        {
            return _roleService.Save(viewModel);
        }
    }
}
