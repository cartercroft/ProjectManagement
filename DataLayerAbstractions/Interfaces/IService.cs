namespace LayerAbstractions.Interfaces
{
    public interface IService<TKey, TViewModel, TDataModel, TRepository>
    {
        TViewModel Get(TKey id);
        IEnumerable<TViewModel> GetAll();
        void Delete(TViewModel viewModel);
        TViewModel Save(TViewModel viewModel);
    }
}
