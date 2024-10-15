using Microsoft.AspNetCore.Mvc;
using DataLayerAbstractions;
using Microsoft.AspNetCore.Authorization;

namespace DataLayerAbstractions
{
    [Route("/api/[controller]/[action]")]
    [ApiController]

    public class APIControllerBase<TViewModel,
        TDataModel,
        TService,
        TRepository> : ControllerBase
        where TViewModel : ViewModelBase
        where TDataModel : ModelBaseWithId
        where TRepository : RepositoryBase<TDataModel>
        where TService : ServiceBase<TViewModel, TRepository, TDataModel>
    {
        private readonly TService _service;
        public APIControllerBase(TService service)
        {
            _service = service;
        }
        [HttpGet]
        [Authorize]
        public TViewModel Get(int id)
        {
            return _service.Get(id);
        }
        [HttpGet]
        [Authorize]
        public IEnumerable<TViewModel> GetAll()
        {
            return _service.GetAll();
        }
        [HttpPost]
        [Authorize]
        public TViewModel Save(TViewModel viewModel)
        {
            return _service.Save(viewModel);
        }
        [HttpPost]
        [Authorize]
        public void Delete(TViewModel viewModel)
        {
            _service.Delete(viewModel);
        }
    }
}
