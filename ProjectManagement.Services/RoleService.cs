using AutoMapper;
using LayerAbstractions.Interfaces;
using ProjectManagement.Models;
using ProjectManagement.Public.Models;
using ProjectManagement.Repositories;

namespace ProjectManagement.Services
{
    public class RoleService : IService<Guid, RoleViewModel, Role, RoleRepository>
    {
        private readonly IMapper _mapper;
        private readonly RoleRepository _roleRepository;
        public RoleService(RoleRepository roleRepository, IMapper mapper)
        {

            _roleRepository = roleRepository;
            _mapper = mapper;

        }
        public void Delete(RoleViewModel viewModel)
        {
            _roleRepository.Delete(_mapper.Map<Role>(viewModel));
        }

        public RoleViewModel Get(Guid id)
        {
            return _mapper.Map<RoleViewModel>(_roleRepository.Get(id));
        }

        public IEnumerable<RoleViewModel> GetAll()
        {
            return _mapper.Map<List<RoleViewModel>>(_roleRepository.GetAll());
        }

        public RoleViewModel Save(RoleViewModel viewModel)
        {
            return _mapper.Map<RoleViewModel>(_roleRepository.Save(_mapper.Map<Role>(viewModel)));
        }
    }
}
