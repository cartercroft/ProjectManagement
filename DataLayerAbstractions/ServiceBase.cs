using AutoMapper;

namespace DataLayerAbstractions
{
    public class ServiceBase<TViewModel, TRepository, TDataModel> : AbstractServiceBase<TViewModel> 
        where TViewModel : ViewModelBase
        where TDataModel : ModelBase
        where TRepository : RepositoryBase<TDataModel>
    {
        private readonly RepositoryBase<TDataModel> _repository;
        private readonly IMapper _mapper;
        public ServiceBase(RepositoryBase<TDataModel> repository,
            IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }
        protected override void PostSave(TViewModel viewModel) { }
        protected override void PreSave(TViewModel viewModel) { }
        public override sealed TViewModel InternalSave(TViewModel viewModel)
        {
            PreSave(viewModel);
            var model = _mapper.Map<TDataModel>(viewModel);
            int id = _repository.Save(model);
            PostSave(viewModel);
            return Get(id);
        }
        public override void Delete(TViewModel viewModel)
        {
            var model = MapClientModelToDataModel(viewModel);
            _repository.Delete(model);
        }
        public override TViewModel Get(int id)
        {
            var dataModel = _repository.Get(id);
            return _mapper.Map<TViewModel>(dataModel);
        }
        public override IEnumerable<TViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<TViewModel>>(_repository.GetAll());
        }
        public virtual TViewModel Save(TViewModel viewModel) => InternalSave(viewModel);
        protected TDataModel MapClientModelToDataModel(TViewModel model)
        {
            return _mapper.Map<TDataModel>(model);
        }
    }
}
