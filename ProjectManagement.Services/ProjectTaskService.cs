using AutoMapper;
using DataLayerAbstractions;
using ProjectManagement.Models;
using ProjectManagement.Public.Models;
using ProjectManagement.Repositories;

namespace ProjectManagement.Services
{
    public class ProjectTaskService : ServiceBase<ProjectTaskViewModel, ProjectTaskRepository, ProjectTask>
    {
        public ProjectTaskService(ProjectTaskRepository taskRepository ,IMapper mapper) : base(taskRepository, mapper){}
    }
}
