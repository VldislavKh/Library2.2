using Library2._2.Interfaces.BookInterfaces;
using MediatR;

namespace Library2._2.Commands.BookCommands
{
    public class AddBookCommand : IRequest<int>
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public int AuthorId { get; set; }

        public class AddBookToDbCommandHandler : IRequestHandler<AddBookCommand, int>
        {
            private readonly IAddDeleteBook _addBook;

            public AddBookToDbCommandHandler(IAddDeleteBook addBook)
            {
                _addBook = addBook;
            }

            public async Task<int> Handle(AddBookCommand command, CancellationToken cancellationToken)
            {
                return _addBook.Add(command.Title, command.Year, command.Genre, command.AuthorId);
            }
        }
    }
}
