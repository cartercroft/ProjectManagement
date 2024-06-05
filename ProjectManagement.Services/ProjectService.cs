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
        public override ProjectViewModel Get(int id)
        {
            ProjectViewModel viewModel = _mapper.Map<ProjectViewModel>(_projectRepository.Get(id));
            //I would be able to get away with letting my repository's AlwaysInclude handle this, but having issues with entity tracking.
            viewModel.Tasks = _taskService.GetProjectTasksForProject(id);
            return viewModel;
        }
        public override IEnumerable<ProjectViewModel> GetAll()
        {
            var viewModels = _mapper.Map<List<ProjectViewModel>>(_projectRepository.GetAllNoInclusions());
            foreach(var viewModel in viewModels)
            {
                viewModel.Tasks = _taskService.GetProjectTasksForProject(viewModel.Id);
            }
            return viewModels;
        }
        protected override void PreSave(ProjectViewModel viewModel)
        {

        }
        protected override void PostSave(ProjectViewModel viewModel)
        {
            foreach (var task in viewModel.Tasks)
            {
                _taskService.Save(task);
            }
        }
    }
}
