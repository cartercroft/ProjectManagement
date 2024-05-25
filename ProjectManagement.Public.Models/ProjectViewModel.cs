namespace ProjectManagement.Public.Models
{
    public class ProjectViewModel : ViewModelBase
    {
        public ICollection<ProjectTaskViewModel> Tasks { get; set; } = new List<ProjectTaskViewModel>();
    }
}
