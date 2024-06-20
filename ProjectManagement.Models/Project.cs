using DataLayerAbstractions;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models
{
    public class Project : ModelBase
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public Guid UserId { get; set; }
        public virtual List<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}
