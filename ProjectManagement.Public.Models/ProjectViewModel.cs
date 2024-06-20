using DataLayerAbstractions;

namespace ProjectManagement.Public.Models
{
    public class ProjectViewModel : ViewModelBase
    {
        public string Name { get; set; } = null!;
        public DateTime? DueDate { get; set; }
        public Guid UserId { get; set; }
        public List<ProjectTaskViewModel> Tasks { get; set; } = new List<ProjectTaskViewModel>();
    }
}
