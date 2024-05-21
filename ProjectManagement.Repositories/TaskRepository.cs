using Microsoft.EntityFrameworkCore;
using ProjectManagement.Models;

namespace ProjectManagement.Repositories
{
    public class TaskRepository : RepositoryBase<ProjectTask>
    {
        public TaskRepository(ProjectManagementContext context, DbSet<ProjectTask> dbSet) : base(context, dbSet){}
    }
}
