using DataLayerAbstractions;
using Microsoft.EntityFrameworkCore;
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
        public override List<Project> GetAll()
        {
            return _context.Projects.Where(p => !p.IsDeleted).Include(p => p.Tasks).ToList();
        }
    }
}
