using Microsoft.EntityFrameworkCore;
using ProjectManagement.Models;

namespace ProjectManagement.Repositories
{
    public class ProjectTaskRepository : RepositoryBase<ProjectTask>
    {
        public ProjectTaskRepository(ProjectManagementContext context) : base(context){}
    }
}
