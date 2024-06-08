using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Models;

namespace ProjectManagement.EF
{
    public class ProjectManagementContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Project> PM_Projects { get; set; }
        public DbSet<ProjectTask> PM_Tasks { get; set; }
        public ProjectManagementContext(DbContextOptions<ProjectManagementContext> options) : base(options)
        {
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=.\\SQLEXPRESS;Database=ProjectManagement;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
