using Library2._2.Entities;
using Library2._2.Infrastructure;
using Library2._2.Interfaces.RoleInterfaces;

namespace Library2._2.Services
{
    public class RoleService : IAddDeleteRole, IGetRolesInfo, ISetRole
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

        public int Set(int userId,  int roleId)
        {
            var role = _context.Roles.SingleOrDefault(x => x.Id == roleId);
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null) 
            {
                throw new ArgumentNullException(nameof(user), "Пользователя с заданным Id не существует!");   
            }
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role), "Роли с заданным Id не существует!");
            }

            user.RoleId = roleId;
            _context.Users.Update(user);
            _context.SaveChanges();

            return user.Id;
        }
    }
}
