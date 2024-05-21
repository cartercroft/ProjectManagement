namespace ProjectManagement.Services
{
    public abstract class AbstractServiceBase<TViewModel>
    {
        public abstract TViewModel Get(int id);
        public abstract List<TViewModel> GetAll();
        /// <summary>
        /// Handles adding the model to the DB.
        /// </summary>
        /// <param name="viewModel"></param>
        public abstract void Add(TViewModel viewModel);
        /// <summary>
        /// Handles updating the model in the DB.
        /// </summary>
        /// <param name="viewModel"></param>
        public abstract void Update(TViewModel viewModel);
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
