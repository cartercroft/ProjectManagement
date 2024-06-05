namespace DataLayerAbstractions
{
    public abstract class AbstractServiceBase<TViewModel>
    {
        public abstract TViewModel Get(int id);
        public abstract IEnumerable<TViewModel> GetAll();
        /// <summary>
        /// Handles deciding whether to Add or Update a model in the DB.
        /// </summary>
        /// <param name="viewModel"></param>
        public abstract TViewModel InternalSave(TViewModel viewModel);
        public abstract void Delete(TViewModel viewModel);
        protected abstract void PreSave(TViewModel viewModel);
        protected abstract void PostSave(TViewModel viewModel);
    }
}
