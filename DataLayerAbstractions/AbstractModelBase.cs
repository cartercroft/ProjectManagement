namespace LayerAbstractions
{
    public abstract class AbstractModelBase
    {
        public DateTime CreatedWhen { get; set; }
        public DateTime UpdatedWhen { get; set; }
        public bool IsDeleted { get; set; }
    }
}
