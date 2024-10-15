using DataLayerAbstractions;
using Microsoft.AspNetCore.Authorization;
using ProjectManagement.API.Auth;
using ProjectManagement.Models;
using ProjectManagement.Public.Models;
using ProjectManagement.Repositories;
using ProjectManagement.Services;

namespace ProjectManagement.API.Controllers
{
    [Authorize(Policy = PolicyNames.SuperUserOnly)]
    public class ProjectController : APIControllerBase<ProjectViewModel, Project, ProjectService, ProjectRepository>
    {
        private readonly ProjectService _projectService;
        public ProjectController(ProjectService projectService) : base(projectService)
        {
            _projectService = projectService;
        }
    }
}
