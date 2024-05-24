namespace ProjectManagement.Models
{
    public class Project : ModelBase
    {
        public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}
