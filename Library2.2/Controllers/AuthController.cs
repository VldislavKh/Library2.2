using Library2._2.Commands.JwtCommands;
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
        

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;

        }

        [HttpPost ("[action]")]
        public async Task<IActionResult> Login([FromBody] Login request)
        {
            var user = await _mediator.Send(new AuthenticateUserQuery 
            {Username = request.Name, Password = request.Password });

            if (user != null)
            {
                //Generate JWT
                return Ok(await _mediator.Send(new GenerateJwtCommand {User = user })); 
            }

            return Unauthorized();
        }

        
    }
}
