using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Models;
using ProjectManagement.Services;

namespace ProjectManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {
        private readonly TaskService _taskService;
        public ProjectTaskController(TaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpGet]
        public ProjectTask Get(int id)
        {
            return _taskService.Get(id);
        }
        [HttpPost]
        public void Save(ProjectTask task)
        {
            _taskService.Save(task);
        }
        [HttpPost]
        public void Delete(ProjectTask task)
        {
            _taskService.Delete(task);
        }
    }
}
