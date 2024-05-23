namespace ProjectManagement.Services
{
    public abstract class AbstractServiceBase<TViewModel>
    {
        public abstract TViewModel Get(int id);
        public abstract List<TViewModel> GetAll();
        /// <summary>
        /// Handles deciding whether to Add or Update a model in the DB.
        /// </summary>
        /// <param name="viewModel"></param>
        public abstract void Save(TViewModel viewModel);
        public abstract void Delete(TViewModel viewModel);
        protected abstract void PreSave();
        protected abstract void PostSave();
    }
}
