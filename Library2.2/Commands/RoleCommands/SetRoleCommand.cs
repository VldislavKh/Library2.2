using Library2._2.Interfaces.RoleInterfaces;
using MediatR;

namespace Library2._2.Commands.RoleCommands
{
    public class SetRoleCommand : IRequest<int>
    {
        public int UserId { get; set; }
        
        public int RoleId { get; set; }

        public class SetRoleCommandHandler : IRequestHandler<SetRoleCommand, int>
        {
            private readonly ISetRole _setRole;

            public SetRoleCommandHandler(ISetRole setRole)
            {
                _setRole = setRole;
            }

            public async Task<int> Handle(SetRoleCommand command, CancellationToken cancellationToken)
            {
                return _setRole.Set(command.UserId, command.RoleId);
            }
        }
    }
}
