using Microsoft.AspNetCore.Mvc;
using DataLayerAbstractions;
using Microsoft.AspNetCore.Authorization;
using LayerAbstractions.Interfaces;
using LayerAbstractions;

namespace DataLayerAbstractions
{
    [Route("/api/[controller]/[action]")]
    [ApiController]

    public class APIControllerBase<TKey,
        TViewModel,
        TDataModel,
        TService,
        TRepository> : ControllerBase
        where TViewModel : IViewModel<TKey>
        where TDataModel : ModelBase<TKey>
        where TRepository : RepositoryBase<TKey, TDataModel>
        where TService : ServiceBase<TKey, TViewModel, TDataModel, TRepository>
    {
        private readonly TService _service;
        public APIControllerBase(TService service)
        {
            _service = service;
        }
        [HttpGet]
        [Authorize]
        public TViewModel Get(TKey id)
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
