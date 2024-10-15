using LayerAbstractions;
using System.ComponentModel.DataAnnotations;

namespace DataLayerAbstractions
{
    public class ModelBaseWithId : AbstractModelBase
    {
        [Key]
        public int Id { get; set; }
    }
}
