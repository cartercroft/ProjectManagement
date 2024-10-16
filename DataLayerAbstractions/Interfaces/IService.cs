namespace LayerAbstractions.Interfaces
{
    public interface IService<TKey, TViewModel, TDataModel, TRepository>
    {
        abstract TViewModel Get(TKey id);
        abstract IEnumerable<TViewModel> GetAll();
        /// <summary>
        /// Handles deciding whether to Add or Update a model in the DB.
        /// </summary>
        /// <param name="viewModel"></param>
        void Delete(TViewModel viewModel);
        TViewModel Save(TViewModel viewModel);
    }
}
