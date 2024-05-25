using ProjectManagement.Models;

namespace ProjectManagement.Repositories
{
    public class ProjectRepository : RepositoryBase<Project>
    {
        public ProjectRepository(ProjectManagementContext context) : base(context){}
    }
}
