using Library2._2.Interfaces.RoleInterfaces;
using MediatR;

namespace Library2._2.Commands.RoleCommands
{
    public class AddRoleCommand : IRequest<int>
    {
        public string Name { get; set; }

        public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, int>
        {
            private readonly IAddDeleteRole _addRole;

            public AddRoleCommandHandler(IAddDeleteRole addRole)
            {
                _addRole = addRole;
            }

            public async Task<int> Handle(AddRoleCommand command, CancellationToken cancellationToken)
            {
                return _addRole.Add(command.Name);
            }
        }
    }
}
