using Library2._2.Commands.BookCommands;
using Library2._2.Queries.BookQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library2._2.Controllers
{
    [ApiController]
    [Route("/api/Book")]
    public class BookController : Controller
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPut("[action]")]
        //Добавляет книгу в БД
        public async Task<ActionResult<int>> AddBook([FromBody] AddBookCommand command, CancellationToken token)
        {
            return await _mediator.Send(command, token);
        }

        [HttpDelete("[action]/{id}")]
        //Удаляет книгу из БД
        public async Task<IActionResult> DeleteBook(int id, CancellationToken token)
        {
            await _mediator.Send(new DeleteBookCommand() { Id = id }, token);
            return NoContent();
        }

        [HttpGet("[action]/{id}")]
        //Возвращает автора книги по ее названию
        public async Task<IActionResult> GetAuthor(int id, CancellationToken token)
        {
            var author = await _mediator.Send(new GetAuthorByIdQuery() { Id = id }, token);
            return CreatedAtAction("GetAuthor", author);
        }

        [HttpGet("[action]/{id}")]
        //Возвращает жанр книги из бд по её названию
        public async Task<IActionResult> GetGenre(int id, CancellationToken token)
        {
            var genre = await _mediator.Send(new GetGenreByIdQuery() { Id = id }, token);
            return CreatedAtAction("GetGenre", genre);
        }
    }
}
