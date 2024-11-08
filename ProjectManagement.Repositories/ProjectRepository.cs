using AutoMapper;
using DataLayerAbstractions;
using ProjectManagement.EF;
using ProjectManagement.Models;
using System.Linq.Expressions;

namespace ProjectManagement.Repositories
{
    public class ProjectRepository : RepositoryBase<int, Project>
    {
        private ProjectManagementContext _context;
        public ProjectRepository(ProjectManagementContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
        }
        protected override List<Expression<Func<Project, object>>> AlwaysInclude => new()
        {
            p => p.Tasks
        };
        public List<Project> GetAllProjectsForUser(Guid userId)
        {
            return GetAll().FindAll(x => x.UserId == userId);
        }
    }
}
