using Library2._2.Commands.AuthorCommands;
using Library2._2.Queries.AuthorQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        //[Authorize /*(Roles = "moderator, admin")*/]
        [HttpPost("[action]")]
        // Добавляет автора в бд 
        public async Task<ActionResult<int>> AddAuthor([FromBody] AddAuthorCommand command, CancellationToken token)
        {
            return await _mediator.Send(command, token);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("[action]/{id}")]
        // Удаляет автора из указанной БД
        public async Task<IActionResult> DeleteAuthor(int id, CancellationToken token)
        {
            await _mediator.Send(new DeleteAuthorCommand() { AuthorId = id }, token);
            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        //Возвращает список всех авторов из бд 
        public async Task<IActionResult> GetAllAuthors(CancellationToken token)
        {
            var authors = await _mediator.Send(new GetAllAuthorsQuery(), token);
            return CreatedAtAction("GetAllAuthors", authors);
        }

        [Authorize(Roles = "moderator, admin, user")]
        [HttpGet("[action]/{id}")]
        // Возвращает список книг автора    
        public async Task<IActionResult> GetAuthorsBooks(int id, CancellationToken token)
        {
            var books = await _mediator.Send(new GetAuthorsBooksQuery() { AuthorId = id }, token);
            return CreatedAtAction("GetAuthorsBooks", books);
        }

        [Authorize(Roles = "moderator, admin, user")]
        [HttpGet("[action]")]
        //Возвращает список авторов, у которых максмальное количество книг 
        public async Task<IActionResult> GetMaxBooksAuthors(CancellationToken token)
        {
            var authors = await _mediator.Send(new GetMaxBooksAuthorsQuery(), token);
            return CreatedAtAction("GetMaxBooksAuthors", authors);
        }
    }
}
