using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProjectManagement.Classes;
using ProjectManagement.Models;
using ProjectManagement.Public.Models;
using ProjectManagement.Repositories;
using System.Security.Claims;
namespace ProjectManagement.Services
{
    public class UserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly UserRepository _userRepository;
        public UserService(UserManager<User> userManager,
            IMapper mapper,
            UserRepository userRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public void Delete(UserViewModel viewModel)
        {
            var user = _mapper.Map<User>(viewModel);
            _userManager.DeleteAsync(user);
        }

        public UserViewModel Get(Guid id)
        {
            return _mapper.Map<UserViewModel>(_userRepository.Get(id));
        }

        public IEnumerable<UserViewModel> GetAll()
        {
            return _mapper.Map<List<UserViewModel>>(_userRepository.GetAll());
        }

        public UserViewModel Save(UserViewModel viewModel)
        {
            _userManager.UpdateAsync(_mapper.Map<User>(viewModel));
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, viewModel.Id.ToString())
            });
            var principal = new ClaimsPrincipal(claimsIdentity);
            var user = AsyncHelper.RunSync<User>(() => _userManager.GetUserAsync(principal));
            return _mapper.Map<UserViewModel>(_userRepository.Save(_mapper.Map<User>(viewModel)));
        }
    }
}
