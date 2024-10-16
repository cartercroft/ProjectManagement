using DataLayerAbstractions;
using LayerAbstractions;
using ProjectManagement.Classes;

namespace ProjectManagement.Models
{
    public class ProjectTask : ModelBase<int>
    {
        public string Title { get; set; } = null!;
        public Enumeration.ProjectTaskStatus Status { get; set; } = Enumeration.ProjectTaskStatus.NotSet;
        public string Description { get; set; } = null!;
        public int ProjectId { get; set; }
    }
}
