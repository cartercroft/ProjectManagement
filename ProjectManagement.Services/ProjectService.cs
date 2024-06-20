using AutoMapper;
using DataLayerAbstractions;
using ProjectManagement.Models;
using ProjectManagement.Public.Models;
using ProjectManagement.Repositories;

namespace ProjectManagement.Services
{
    public class ProjectService : ServiceBase<ProjectViewModel, ProjectRepository, Project>
    {
        private ProjectRepository _projectRepository;
        private ProjectTaskService _taskService;
        private IMapper _mapper;
        public ProjectService(ProjectRepository projectRepository,
            IMapper mapper,
            ProjectTaskService taskService) : base(projectRepository, mapper)
        {
            _mapper = mapper;
            _taskService = taskService;
            _projectRepository = projectRepository;
        }
        public async Task<List<ProjectViewModel>> GetAllProjectsForUser(Guid userId)
        {
            return _mapper.Map<List<ProjectViewModel>>(await Task.Run(() => _projectRepository.GetAllProjectsForUser(userId)));
        }
    }
}
