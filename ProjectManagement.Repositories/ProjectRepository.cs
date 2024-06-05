using DataLayerAbstractions;
using ProjectManagement.Models;

namespace ProjectManagement.Repositories
{
    public class ProjectRepository : RepositoryBase<Project>
    {
        private ProjectManagementContext _context;
        public ProjectRepository(ProjectManagementContext context) : base(context)
        {
            _context = context;
        }
    }
}
