﻿using Library2._2.Commands.UserCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library2._2.Controllers
{
    [ApiController]
    [Route("/api/User")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<int>> AddUser(AddUserCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteUserCommand() { Id = id }, cancellationToken);
            return NoContent();
        }
    }
}
