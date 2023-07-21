using Library2._2.Interfaces.AuthorInterfaces;
using MediatR;

namespace Library2._2.Commands.AuthorCommands
{
    public class AddAuthorCommand : IRequest<int>
    {
        //Параметры автора
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateOnly? DeathDate { get; set; }

        public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, int>
        {
            private readonly IAddDeleteAuthor _addAuthor;

            public AddAuthorCommandHandler(IAddDeleteAuthor addAuthor)
            {
                _addAuthor = addAuthor;
            }

            public async Task<int> Handle(AddAuthorCommand command, CancellationToken cancellationToken)
            {
                return _addAuthor.Add(command.Name, command.BirthDate, command.DeathDate);
            }
        }
    }
}
