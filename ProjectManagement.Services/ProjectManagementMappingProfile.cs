using AutoMapper;
using ProjectManagement.Models;
using ProjectManagement.Public.Models;
namespace ProjectManagement.Services
{
    public class ProjectManagementMappingProfile : Profile
    {
        public ProjectManagementMappingProfile()
        {
            CreateMap<ProjectTask, ProjectTaskViewModel>().ReverseMap();
        }
    }
}
