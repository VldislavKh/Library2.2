using Library2._2.Commands.AuthorCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library2._2.Controllers
{
    [ApiController]
    [Route("/api/Author")]
    public class AuthorController : Controller
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPut("[action]")]
        // Добавляет автора в бд 
        public async Task<ActionResult<int>> AddAuthor([FromBody] AddAuthorCommand command, CancellationToken token)
        {
            return await _mediator.Send(command, token);
        }
    }
}
