using LayerAbstractions.Interfaces;

namespace LayerAbstractions
{
    public class ModelBase<TKey> : IModel<TKey>
    {
        public TKey Id { get; set; }
        public DateTime CreatedWhen { get; set; }
        public DateTime UpdatedWhen { get; set; }
        public bool IsDeleted { get; set; }
    }
}
