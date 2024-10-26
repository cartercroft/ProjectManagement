using LayerAbstractions;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models
{
    public class Project : ModelBase<int>
    {
        [Required]
        public string Name { get; set; } = null!;
        public virtual List<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}
