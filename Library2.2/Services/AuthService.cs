using Library2._2.Entities;
using Library2._2.Infrastructure;
using Library2._2.Interfaces.AuthInterfaces;
using Library2._2.Options;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Library2._2.Services
{
    public class AuthService : IAuth, IGenerateJwt
    {
        private readonly ApplicationContext _context;
        private readonly IOptions<AuthOptions> _authOptions;
        public AuthService(ApplicationContext context, IOptions<AuthOptions> authOptions)
        {
            _context = context;
            _authOptions = authOptions;
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

        public string GenerateJWT(User user)
        {
            var authParams = _authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            var role = _context.Roles.SingleOrDefault(r => r.Id == user.RoleId);
            claims.Add(new Claim("role", role.Name.ToString()));

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
