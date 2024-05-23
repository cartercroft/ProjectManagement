using AutoMapper;
using ProjectManagement.Models;
using ProjectManagement.Public.Models;
using ProjectManagement.Repositories;

namespace ProjectManagement.Services
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
        protected override void PostSave() { }
        protected override void PreSave() { }
        public override sealed void Save(TViewModel viewModel)
        {
            PreSave();
            var model = _mapper.Map<TDataModel>(viewModel);
            _repository.Save(model);
            PostSave();
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
        public override List<TViewModel> GetAll()
        {
            return _mapper.Map<List<TViewModel>>(_repository.GetAll());
        }
        protected TDataModel MapClientModelToDataModel(TViewModel model)
        {
            return _mapper.Map<TDataModel>(model);
        }
    }
}
