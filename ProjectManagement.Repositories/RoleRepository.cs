using AutoMapper;
using DataLayerAbstractions;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.EF;
using ProjectManagement.Models;

namespace ProjectManagement.Repositories
{
    public class RoleRepository : IRepository<Role, Guid>
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

        public void Delete(Role model)
        {
            _roles.Remove(model);
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

        public List<Role> GetAll()
        {
            throw new NotImplementedException();
        }

        public Role Save(Role model)
        {
            throw new NotImplementedException();
        }
    }
}
