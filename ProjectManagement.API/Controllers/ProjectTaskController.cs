using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Public.Models;
using ProjectManagement.Services;

namespace ProjectManagement.API.Controllers
{
    public class ProjectTaskController : APIControllerBase
    {
        private readonly ProjectTaskService _taskService;
        public ProjectTaskController(ProjectTaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpGet]
        public ProjectTaskViewModel Get(int id)
        {
            return _taskService.Get(id);
        }
        [HttpPost]
        public void Save(ProjectTaskViewModel task)
        {
            _taskService.Save(task);
        }
        [HttpPost]
        public void Delete(ProjectTaskViewModel task)
        {
            _taskService.Delete(task);
        }
    }
}
