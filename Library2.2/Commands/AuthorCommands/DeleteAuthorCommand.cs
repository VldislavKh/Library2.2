using Library2._2.Interfaces.AuthorInterfaces;
using MediatR;

namespace Library2._2.Commands.AuthorCommands
{
    public class DeleteAuthorCommand : IRequest
    {
        public int AuthorId { get; set; }

        public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
        {
            private readonly IAddDeleteAuthor _deleteAuthor;

            public DeleteAuthorCommandHandler(IAddDeleteAuthor deleteAuthor)
            {
                _deleteAuthor = deleteAuthor;
            }

            public async Task<Unit> Handle(DeleteAuthorCommand command, CancellationToken cancellationToken)
            {
                _deleteAuthor.Delete(command.AuthorId);
                return Unit.Value;
            }
        }
    }
}
