using DataLayerAbstractions;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models
{
    public class Project : ModelBase
    {
        [Required]
        public string Name { get; set; } = null!;
        public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}
