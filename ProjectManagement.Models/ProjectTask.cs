using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models
{
    public class ProjectTask : ModelBase
    {
        [Key]
        public string Status { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
    }
}
