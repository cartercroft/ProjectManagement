using DataLayerAbstractions;
using LayerAbstractions.Interfaces;

namespace ProjectManagement.Public.Models
{
    public class ProjectViewModel : IViewModel<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? DueDate { get; set; }
        public List<ProjectTaskViewModel> Tasks { get; set; } = new List<ProjectTaskViewModel>();
    }
}
