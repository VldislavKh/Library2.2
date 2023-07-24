using Library2._2.Entities;
using Library2._2.Options;
using Library2._2.Queries.AuthQueries;
using Library2._2.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Library2._2.Controllers
{
    [ApiController]
    [Route("/api/Auth")]
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IOptions<AuthOptions> _authOptions;

        public AuthController(IMediator mediator, IOptions<AuthOptions> authOptions)
        {
            _mediator = mediator;
            _authOptions = authOptions;
        }

        [HttpPost ("[action]")]
        public async Task<IActionResult> Login([FromBody] Login request)
        {
            var user = await _mediator.Send(new AuthenticateUserQuery {Username = request.Name, Password = request.Password });

            if (user != null)
            {
                //Generate JWT
                return Ok(GenerateJWT(user)); 
            }

            return Unauthorized();
        }

        private string GenerateJWT(User user)
        {
            var authParams = _authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime), 
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
