using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using ProjectManagement.Models;
using ProjectManagement.Public.Models;
using ProjectManagement.Repositories;
using ProjectManagement.Services;

namespace ProjectManagement.API.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]

    public class APIControllerBase<TViewModel,
        TDataModel,
        TService,
        TRepository> : ControllerBase
        where TViewModel : ViewModelBase
        where TDataModel : ModelBase
        where TRepository : RepositoryBase<TDataModel>
        where TService : ServiceBase<TViewModel, TRepository, TDataModel>
    {
        private readonly TService _service;
        public APIControllerBase(TService service)
        {
            _service = service;
        }
        [HttpGet]
        public TViewModel Get(int id)
        {
            return _service.Get(id);
        }
        [HttpGet]
        public List<TViewModel> GetAll()
        {
            return _service.GetAll();
        }
        [HttpPost]
        public void Save(TViewModel viewModel)
        {
            _service.Save(viewModel);
        }
        [HttpPost]
        public void Delete(TViewModel viewModel)
        {
            _service.Delete(viewModel);
        }
    }
}
