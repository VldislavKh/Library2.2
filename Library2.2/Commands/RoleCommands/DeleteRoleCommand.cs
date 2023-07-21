using Library2._2.Interfaces.AuthorInterfaces;
using Library2._2.Interfaces.RoleInterfaces;
using MediatR;

namespace Library2._2.Commands.RoleCommands
{
    public class DeleteRoleCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
        {
            private readonly IAddDeleteRole _deleteRole;

            public DeleteRoleCommandHandler(IAddDeleteRole deleteRole)
            {
                _deleteRole = deleteRole;
            }

            public async Task<Unit> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
            {
                _deleteRole.Delete(command.Id);
                return Unit.Value;
            }
        }
    }
}
