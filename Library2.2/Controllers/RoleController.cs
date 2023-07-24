using Library2._2.Commands.RoleCommands;
using Library2._2.Entities;
using Library2._2.Queries.RoleQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library2._2.Controllers
{
    [ApiController]
    [Route("/api/Role")]
    public class RoleController : Controller
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<int>> AddRole([FromBody] AddRoleCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteRole(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteRoleCommand() { Id = id }, cancellationToken);
            return NoContent();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
        {
            var roles = await _mediator.Send(new GetAllRolesQuery(), cancellationToken);
            return CreatedAtAction("GetAllRoles", roles);
        }

        [HttpPatch("[action]")]
        public async Task<ActionResult<int>> SetRole([FromBody] SetRoleCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }
    }
}
