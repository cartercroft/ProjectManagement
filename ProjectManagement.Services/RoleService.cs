using LayerAbstractions.Interfaces;
using ProjectManagement.Models;
using ProjectManagement.Public.Models;
using ProjectManagement.Repositories;

namespace ProjectManagement.Services
{
    public class RoleService : IService<Guid, RoleViewModel, Role, RoleRepository>
    {
        public void Delete(RoleViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public RoleViewModel Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoleViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public RoleViewModel Save(RoleViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
