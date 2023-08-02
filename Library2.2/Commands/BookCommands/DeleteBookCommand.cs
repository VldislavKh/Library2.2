using Domain.Infrastructure;
using Library2._2.Interfaces.BookInterfaces;
using MediatR;

namespace Library2._2.Commands.BookCommands
{
    public class DeleteBookCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Unit>
        {
            private readonly ApplicationContext _context;
            private readonly IAddDeleteBook _deleteBook;

            public DeleteBookCommandHandler(IAddDeleteBook deleteBook)
            {
                _deleteBook = deleteBook;
            }

            public async Task<Unit> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
            {
                _deleteBook.Delete(command.Id);
                return Unit.Value;
            }
        }
    }
}
