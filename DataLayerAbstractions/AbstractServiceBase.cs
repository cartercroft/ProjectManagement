using LayerAbstractions.Interfaces;

namespace LayerAbstractions
{
    public abstract class AbstractServiceBase<TKey, TViewModel, TDataModel, TRepository> : IService<TKey, TViewModel, TDataModel, TRepository>
    {
        public abstract void Delete(TViewModel viewModel);
        public abstract TViewModel Get(TKey id);
        public abstract IEnumerable<TViewModel> GetAll();
        protected abstract TViewModel InternalSave(TViewModel viewModel);
        protected abstract void PostSave(TViewModel viewModel);
        protected abstract void PreSave(TViewModel viewModel);
        public abstract TViewModel Save(TViewModel viewModel);
    }
}