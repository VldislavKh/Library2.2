using Library2._2.Entities;
using Library2._2.Infrastructure;
using Library2._2.Interfaces.AuthInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Library2._2.Services
{
    public class AuthService : IAuth
    {
        private readonly ApplicationContext _context;
        public AuthService(ApplicationContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            return _context.Users.SingleOrDefault(u => u.Name == username && u.Password == CreateSHA256(password));
        }

        private string CreateSHA256(string input)
        {
            using SHA256 hash = SHA256.Create();
            return Convert.ToHexString(hash.ComputeHash(Encoding.ASCII.GetBytes(input)));
        }

    }
}
