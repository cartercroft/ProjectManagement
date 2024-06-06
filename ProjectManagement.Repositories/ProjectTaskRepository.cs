using AutoMapper;
using DataLayerAbstractions;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Models;

namespace ProjectManagement.Repositories
{
    public class ProjectTaskRepository : RepositoryBase<ProjectTask>
    {
        private readonly ProjectManagementContext _context;
        public ProjectTaskRepository(ProjectManagementContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
        }
        //public List<ProjectTask> GetProjectTasksForProject(int projectId)
        //{
        //    List<ProjectTask> returnVal = new List<ProjectTask>();
        //    returnVal = _context.ProjectTasks.Where(t => t.ProjectId == projectId).ToList();
        //    return returnVal;
        //}
    }
}
