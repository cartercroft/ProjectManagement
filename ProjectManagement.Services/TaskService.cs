using AutoMapper;
using ProjectManagement.Models;
using ProjectManagement.Public.Models;
using ProjectManagement.Repositories;

namespace ProjectManagement.Services
{
    public class TaskService : ServiceBase<ProjectTaskViewModel, TaskRepository, ProjectTask>
    {
        public TaskService(TaskRepository taskRepository
            ,IMapper mapper) : base(taskRepository, mapper)
        {
        }
    }
}
