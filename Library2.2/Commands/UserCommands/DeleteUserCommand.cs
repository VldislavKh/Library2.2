using Library2._2.Interfaces.UserInterfaces;
using MediatR;

namespace Library2._2.Commands.UserCommands
{
    public class DeleteUserCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
        {
            private readonly IAddDeleteUser _deleteUser;

            public DeleteUserCommandHandler(IAddDeleteUser deleteUser)
            {
                _deleteUser = deleteUser;
            }

            public async Task<Unit> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
            {
                _deleteUser.Delete(command.Id);
                return Unit.Value;
            }

        }
    }
}
