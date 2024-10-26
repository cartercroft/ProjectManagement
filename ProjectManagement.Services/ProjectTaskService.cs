using AutoMapper;
using DataLayerAbstractions;
using ProjectManagement.Models;
using ProjectManagement.Public.Models;
using ProjectManagement.Repositories;

namespace ProjectManagement.Services
{
    public class ProjectTaskService : ServiceBase<int, ProjectTaskViewModel, ProjectTask, ProjectTaskRepository>
    {
        private readonly ProjectTaskRepository _taskRepository;
        private readonly IMapper _mapper;
        public ProjectTaskService(ProjectTaskRepository taskRepository ,IMapper mapper) : base(taskRepository, mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }
    }
}
