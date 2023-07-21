using Library2._2.Commands.AuthorCommands;
using Library2._2.Queries;
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

        [HttpDelete("[action]/{id}")]
        // Удаляет автора из указанной БД
        public async Task<IActionResult> DeleteAuthor(int id, CancellationToken token)
        {
            await _mediator.Send(new DeleteAuthorCommand() { AuthorId = id }, token);
            return NoContent();
        }

        [HttpGet("[action]")]
        //Возвращает список всех авторов из бд 
        public async Task<IActionResult> GetAllAuthors(CancellationToken token)
        {

            var authors = await _mediator.Send(new GetAllAuthorsQuery(), token);
            return CreatedAtAction("GetAllAuthors", authors);
        }

        
    }
}
