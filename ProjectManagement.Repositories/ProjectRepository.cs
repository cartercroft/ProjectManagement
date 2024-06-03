using DataLayerAbstractions;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Models;
using System.Linq.Expressions;

namespace ProjectManagement.Repositories
{
    public class ProjectRepository : RepositoryBase<Project>
    {
        private ProjectManagementContext _context;
        public ProjectRepository(ProjectManagementContext context) : base(context)
        {
            _context = context;
        }
        protected override List<Expression<Func<Project, object>>> AlwaysInclude => new List<Expression<Func<Project, object>>> 
        {
            p => p.Tasks
        }; 
    }
}
