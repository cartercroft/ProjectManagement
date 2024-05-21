using Microsoft.EntityFrameworkCore;
using ProjectManagement.Models;

namespace ProjectManagement.Repositories
{
    public class ProjectManagementContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public ProjectManagementContext(){}
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

        }
    }
}
