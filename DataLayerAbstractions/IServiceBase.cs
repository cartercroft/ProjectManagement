namespace DataLayerAbstractions
{
    public interface IServiceBase<TViewModel>
    {
        public abstract TViewModel Get(int id);
        public abstract IEnumerable<TViewModel> GetAll();
        /// <summary>
        /// Handles deciding whether to Add or Update a model in the DB.
        /// </summary>
        /// <param name="viewModel"></param>
        TViewModel InternalSave(TViewModel viewModel);
        public void Delete(TViewModel viewModel);
        public TViewModel Save(TViewModel viewModel);
        protected void PreSave(TViewModel viewModel);
        protected void PostSave(TViewModel viewModel);
    }
}
