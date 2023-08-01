using Library2._2.Commands.BookCommands;
using Library2._2.Queries.BookQueries;
using Library2._2.RabbitMQ;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Library2._2.Controllers
{
    [ApiController]
    [Route("/api/Book")]
    public class BookController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IRabbitProducer _rabbitProducer;
        private readonly ILogger<BookController> _logger;

        public BookController(IMediator mediator, IRabbitProducer rabbitProducer, ILogger<BookController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _rabbitProducer = rabbitProducer;
            _logger = logger;
        }

        //[Authorize(Roles = "moderator, admin")]
        [HttpPost("[action]")]
        //Добавляет книгу в БД
        public async Task<ActionResult<int>> AddBook([FromBody] AddBookCommand command, CancellationToken token)
        {
            try
            {
                _logger.LogInformation($"Добавление книги.", DateTime.UtcNow);
                var bookData = await _mediator.Send(command, token);
                _rabbitProducer.SendBookMessage(bookData);
                _logger.LogInformation($"Книга добавлена.", DateTime.UtcNow);
                return bookData;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка добавления книги. Причина: {ex.Message}", DateTime.UtcNow);
                return -1;
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("[action]/{id}")]
        //Удаляет книгу из БД
        public async Task<IActionResult> DeleteBook(int id, CancellationToken token)
        {
            await _mediator.Send(new DeleteBookCommand() { Id = id }, token);
            return NoContent();
        }

        //[Authorize(Roles = "moderator, admin, user")]
        [HttpGet("[action]/{id}")]
        //Возвращает автора книги по ее названию
        public async Task<IActionResult> GetAuthor(int id, CancellationToken token)
        {
            var author = await _mediator.Send(new GetAuthorByIdQuery() { Id = id }, token);
            return Ok(author);
        }

        [Authorize(Roles = "moderator, admin, user")]
        [HttpGet("[action]/{id}")]
        //Возвращает жанр книги из бд по её названию
        public async Task<IActionResult> GetGenre(int id, CancellationToken token)
        {
            var genre = await _mediator.Send(new GetGenreByIdQuery() { Id = id }, token);
            return Ok(genre);
        }
    }
}
