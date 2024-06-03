using System.ComponentModel.DataAnnotations;

namespace DataLayerAbstractions
{
    public class ModelBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedWhen { get; set; }
        public DateTime UpdatedWhen { get; set; }
        public bool IsDeleted { get; set; }
    }
}
