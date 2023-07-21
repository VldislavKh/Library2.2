using Library2._2.Entities;
using Library2._2.Infrastructure;
using Library2._2.Interfaces.RoleInterfaces;

namespace Library2._2.Services
{
    public class RoleService : IAddDeleteRole, IGetRolesInfo
    {
        private readonly ApplicationContext _context;

        public RoleService(ApplicationContext context)
        {
            _context = context;
        }

        public int Add(string name)
        {
            var role = new Role();
            role.Name = name;

            _context.Add(role);
            _context.SaveChanges();

            return role.Id;
        }

        public void Delete(int id)
        {
            var role = _context.Roles.SingleOrDefault(x => x.Id == id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
        }

        public List<Role> GetAll()
        {
            return _context.Roles.ToList();
        }
    }
}
