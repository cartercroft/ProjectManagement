using DataLayerAbstractions;

namespace ProjectManagement.Public.Models
{
    public class ProjectTaskViewModel : ViewModelBase
    {
        public string Title { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
    }
}
