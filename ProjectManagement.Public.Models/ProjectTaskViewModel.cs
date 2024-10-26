using DataLayerAbstractions;
using LayerAbstractions.Interfaces;
using ProjectManagement.Classes;

namespace ProjectManagement.Public.Models
{
    public class ProjectTaskViewModel : IViewModel<int>
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public Enumeration.ProjectTaskStatus? Status { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ProjectId { get; set; }
        public ProjectTaskViewModel? PreviousState { get; set; } = null;
    }
}
