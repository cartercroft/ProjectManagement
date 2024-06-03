using AutoMapper;
using DataLayerAbstractions;
using ProjectManagement.Models;
using ProjectManagement.Public.Models;
using ProjectManagement.Repositories;

namespace ProjectManagement.Services
{
    public class ProjectService : ServiceBase<ProjectViewModel, ProjectRepository, Project>
    {
        public ProjectService(ProjectRepository projectRepository ,IMapper mapper) : base(projectRepository, mapper){}
    }
}
