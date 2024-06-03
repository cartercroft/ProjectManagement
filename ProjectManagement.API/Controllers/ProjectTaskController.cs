using DataLayerAbstractions;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Models;
using ProjectManagement.Public.Models;
using ProjectManagement.Repositories;
using ProjectManagement.Services;

namespace ProjectManagement.API.Controllers
{
    public class ProjectTaskController : APIControllerBase<ProjectTaskViewModel, ProjectTask, ProjectTaskService, ProjectTaskRepository>
    {
        private readonly ProjectTaskService _taskService;
        public ProjectTaskController(ProjectTaskService taskService) : base(taskService)
        {
            _taskService = taskService;
        }
        
    }
}
