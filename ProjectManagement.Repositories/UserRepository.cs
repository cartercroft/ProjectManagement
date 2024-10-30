using AutoMapper;
using LayerAbstractions.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.EF;
using ProjectManagement.Models;

namespace ProjectManagement.Repositories
{
    public class UserRepository : IRepository<Guid, User>
    {
        private readonly IMapper _mapper;
        private readonly ProjectManagementContext _context;
        private readonly DbSet<User> _users;
        public UserRepository(ProjectManagementContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _users = _context.Users;
        }

        public void Delete(User user)
        {
            throw new NotImplementedException();
        }

        public User Get(Guid id)
        {
            var user = _users.FirstOrDefault(r => r.Id == id);
            if (user == null)
            {
                throw new KeyNotFoundException($"No {typeof(User).Name} found with ID {id}.");
            }
            return user;
        }

        public User Get(User id)
        {
            User user = _users.FirstOrDefault(m => m.Id.Equals(id));

            if (user == null)
            {
                throw new KeyNotFoundException($"No {typeof(User).Name} found with ID {id}.");
            }
            return user;
        }

        public List<User> GetAll()
        {
            return _users.ToList();
        }
        public User Save(User user)
        {
            throw new NotImplementedException();
        }
        protected virtual void Add(User user)
        {
            throw new NotImplementedException();
        }
        protected virtual void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
