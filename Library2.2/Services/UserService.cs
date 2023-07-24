using Library2._2.Entities;
using Library2._2.Infrastructure;
using Library2._2.Interfaces.UserInterfaces;
using Library2._2.Migrations;
using System.Security.Cryptography;
using System.Text;

namespace Library2._2.Services
{
    public class UserService : IAddDeleteUser, IGetUsersInfo
    {
        private readonly ApplicationContext _context;

        public UserService(ApplicationContext context)
        {
            _context = context;
        }

        public int Add(string name, string password)
        {
            var user = new User();
            user.Name = name;
            user.Password = CreateSHA256(password);

            _context.Add(user);
            _context.SaveChanges();

            return user.Id;
        }

        public void Delete(int id)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        private static string CreateSHA256(string input)
        {
            using SHA256 hash = SHA256.Create();
            return Convert.ToHexString(hash.ComputeHash(Encoding.ASCII.GetBytes(input)));
        }
    }
}
