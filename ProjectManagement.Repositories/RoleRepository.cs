using AutoMapper;
using LayerAbstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.EF;
using ProjectManagement.Models;

namespace ProjectManagement.Repositories
{
    public class RoleRepository : IRepository<Guid, Role>
    {
        private readonly IMapper _mapper;
        private readonly ProjectManagementContext _context;
        private readonly DbSet<Role> _roles;
        public RoleRepository(ProjectManagementContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _roles = _context.Roles;
        }

        public void Delete(Role role)
        {
            _roles.Remove(role);
            _context.SaveChanges();
        }

        public Role Get(Guid id)
        {
            var role = _roles.FirstOrDefault(r => r.Id == id);
            if (role == null)
            {
                throw new KeyNotFoundException($"No {typeof(Role).Name} found with ID {id}.");
            }
            return role;
        }

        public Role Get(Role id)
        {
            Role role = _roles.FirstOrDefault(m => m.Id.Equals(id));

            if (role == null)
            {
                throw new KeyNotFoundException($"No {typeof(Role).Name} found with ID {id}.");
            }
            return role;
        }

        public List<Role> GetAll()
        {
            return _roles.ToList();
        }

        public Role Save(Role role)
        {
            throw new NotImplementedException();
        }
        protected virtual void Add(Role role)
        {
            role.CreatedWhen = role.UpdatedWhen;
            if (_roles.Find(role.Id) != null)
            {
                throw new Exception($"{typeof(Role).Name} Entity already exists and cannot be added.");
            }
            _roles.Add(role);
        }
        protected virtual void Update(Role role)
        {
            var existingModel = _roles.Find(role.Id);
            if (existingModel == null)
                throw new Exception("Tried to update a role that did not exist. Contact dev.");

            existingModel = _mapper.Map(role, existingModel);
            _roles.Update(existingModel);
        }
    }
}
