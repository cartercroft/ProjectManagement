using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Public.Models;
using ProjectManagement.Services;

namespace ProjectManagement.API.Controllers
{
    public class ProjectController : APIControllerBase
    {
        private readonly ProjectService _projectService;
        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public void Save(ProjectViewModel project)
        {
            _projectService.Save(project);
        }
        [HttpGet]
        public List<ProjectViewModel> GetAll() 
        {
            return _projectService.GetAll();
        }
    }
}
