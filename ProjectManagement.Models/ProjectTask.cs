using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models
{
    public class ProjectTask : ModelBase
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        public string Description {  get; set; }
    }
}
