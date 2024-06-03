using DataLayerAbstractions;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Models
{
    public class ProjectTask : ModelBase
    {
        public string Title { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ProjectId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Project Project { get; set; } = null!;
    }
}
