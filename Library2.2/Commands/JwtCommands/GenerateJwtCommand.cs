using Library2._2.Entities;
using Library2._2.Interfaces.AuthInterfaces;
using MediatR;

namespace Library2._2.Commands.JwtCommands
{
    public class GenerateJwtCommand : IRequest<string>
    {
        public User User { get; set; }

        public class GenerateJwtCommandHandler : IRequestHandler<GenerateJwtCommand, string> 
        {
            private readonly IGenerateJwt _generateJwt;

            public GenerateJwtCommandHandler(IGenerateJwt generateJwt)
            {
                _generateJwt = generateJwt;
            }

            public async Task<string> Handle(GenerateJwtCommand command, CancellationToken cancellationToken)
            {
                return _generateJwt.GenerateJWT(command.User);
            }
        }
    }
}
