using DataLayerAbstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Models
{
    public class Project : ModelBase
    {
        [Required]
        public string Name { get; set; } = null!;
        [NotMapped]
        public virtual ICollection<ProjectTask> Tasks { get; set; }
    }
}
