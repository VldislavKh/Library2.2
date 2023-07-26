using Library2._2.Commands.UserCommands;
using Library2._2.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        //добавление пользователя
        [Authorize(Roles = "moderator, admin")]
        [HttpPut("[action]")]
        public async Task<ActionResult<int>> AddUser(AddUserCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        //удаление пользователя
        [Authorize(Roles = "admin")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteUserCommand() { Id = id }, cancellationToken);
            return NoContent();
        }

        //все пользователи
        [Authorize(Roles = "moderator, admin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var users = await _mediator.Send(new GetAllUsersQuery(), cancellationToken);
            return CreatedAtAction("GetAllUsers", users);
        }
    }
}
