using LayerAbstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Models
{
    public class Project : ModelBase<int>
    {
        [Required]
        public string Name { get; set; } = null!;
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}
