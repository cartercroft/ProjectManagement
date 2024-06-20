using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DataLayerAbstractions
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
        private readonly Guid _userId;
        public APIControllerBase(TService service)
        {
            _service = service;
        }
        [HttpGet]
        [Authorize]
        public virtual TViewModel Get(int id)
        {
            return _service.Get(id);
        }
        [HttpGet]
        [Authorize]
        public virtual IEnumerable<TViewModel> GetAll()
        {
            return _service.GetAll();
        }
        [HttpPost]
        [Authorize]
        public virtual TViewModel Save(TViewModel viewModel)
        {
            return _service.Save(viewModel);
        }
        [HttpPost]
        [Authorize]
        public virtual void Delete(TViewModel viewModel)
        {
            _service.Delete(viewModel);
        }
        protected Guid GetCurrentUserId()
        {
            var user = HttpContext.User;
            var userIdClaim = user?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            Guid userId;
            if (user == null
                || userIdClaim == null
                || !Guid.TryParse(userIdClaim.Value, out userId))
            {
                return Guid.Empty;
            }
            return userId;
        }
    }
}
