namespace LayerAbstractions.Interfaces
{
    public interface IViewModel<TKey>
    {
        public TKey Id { get; set; }
    }
}
