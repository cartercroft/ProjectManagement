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
            if (viewModel.Id > 0) 
            {
                Update(viewModel);
            }
            else
            {
                Add(viewModel);
            }
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
            throw new NotImplementedException();
        }

        public override void Add(TViewModel viewModel)
        {
            var model = MapClientModelToDataModel(viewModel);
            _repository.Save(model);
        }

        public override void Update(TViewModel viewModel)
        {
            var model = MapClientModelToDataModel(viewModel);
            _repository.Save(model);
        }

        protected TDataModel MapClientModelToDataModel(TViewModel model)
        {
            return _mapper.Map<TDataModel>(model);
        }
    }
}
