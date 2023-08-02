using Domain.Entities;
using Library2._2.Interfaces.AuthInterfaces;
using MediatR;

namespace Library2._2.Queries.AuthQueries
{
    public class AuthenticateUserQuery : IRequest<User>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class AuthenticateUserQueryHandler : IRequestHandler<AuthenticateUserQuery, User>
        {
            private readonly IAuth _auth;

            public AuthenticateUserQueryHandler(IAuth auth)
            {
                _auth = auth;
            }

            public async Task<User> Handle(AuthenticateUserQuery query, CancellationToken cancellationToken)
            {
                return _auth.Authenticate(query.Username, query.Password);
            }
        }
    }
}
