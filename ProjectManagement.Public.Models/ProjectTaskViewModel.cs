using DataLayerAbstractions;
using ProjectManagement.Classes;

namespace ProjectManagement.Public.Models
{
    public class ProjectTaskViewModel : ViewModelBase
    {
        public string Title { get; set; } = null!;
        public Enumeration.ProjectTaskStatus? Status { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ProjectId { get; set; }
        public ProjectTaskViewModel? PreviousState { get; set; } = null;
    }
}
