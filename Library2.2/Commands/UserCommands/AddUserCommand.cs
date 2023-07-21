using Library2._2.Interfaces.UserInterfaces;
using MediatR;

namespace Library2._2.Commands.UserCommands
{
    public class AddUserCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public class AddUserCommandHandler : IRequestHandler<AddUserCommand, int>
        {
            private readonly IAddDeleteUser _addUser;

            public AddUserCommandHandler(IAddDeleteUser addUser)
            {
                _addUser = addUser;
            }

            public async Task<int> Handle(AddUserCommand command, CancellationToken cancellationToken)
            {
                return _addUser.Add(command.Name, command.Password);
            }
        }
    }
}
