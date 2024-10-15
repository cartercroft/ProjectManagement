using DataLayerAbstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Models
{
    public class Project : ModelBaseWithId
    {
        [Required]
        public string Name { get; set; } = null!;
        public virtual List<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}
