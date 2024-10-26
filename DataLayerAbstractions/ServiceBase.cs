using AutoMapper;
using LayerAbstractions;
using LayerAbstractions.Interfaces;

namespace DataLayerAbstractions
{
    public class ServiceBase<TKey, TViewModel, TDataModel, TRepository> : AbstractServiceBase<TKey, TViewModel, TDataModel, TRepository>
        where TViewModel : IViewModel<TKey>
        where TDataModel : ModelBase<TKey>
        where TRepository : RepositoryBase<TKey, TDataModel>
    {
        private readonly RepositoryBase<TKey, TDataModel> _repository;
        private readonly IMapper _mapper;
        public ServiceBase(RepositoryBase<TKey, TDataModel> repository,
            IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }
        protected override void PreSave(TViewModel viewModel) {}
        protected override void PostSave(TViewModel viewModel) {}
        protected sealed override TViewModel InternalSave(TViewModel viewModel)
        {
            PreSave(viewModel);
            var model = _mapper.Map<TDataModel>(viewModel);
            model = _repository.Save(model);
            var newViewModel = _mapper.Map<TViewModel>(model);
            PostSave(newViewModel);

            return newViewModel;
        }
        public override void Delete(TViewModel viewModel)
        {
            var model = MapClientModelToDataModel(viewModel);
            _repository.Delete(model);
        }
        public override TViewModel Get(TKey id)
        {
            var dataModel = _repository.Get(id);
            return _mapper.Map<TViewModel>(dataModel);
        }
        public override IEnumerable<TViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<TViewModel>>(_repository.GetAll());
        }
        public override TViewModel Save(TViewModel viewModel) => InternalSave(viewModel);
        protected TDataModel MapClientModelToDataModel(TViewModel model)
        {
            return _mapper.Map<TDataModel>(model);
        }
    }
}
