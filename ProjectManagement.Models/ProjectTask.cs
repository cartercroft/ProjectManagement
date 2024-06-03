using DataLayerAbstractions;
using ProjectManagement.Classes;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Models
{
    public class ProjectTask : ModelBase
    {
        public string Title { get; set; } = null!;
        public Enumeration.ProjectTaskStatus Status { get; set; } = Enumeration.ProjectTaskStatus.NotSet;
        public string Description { get; set; } = null!;
        public int ProjectId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Project Project { get; set; } = null!;
    }
}
