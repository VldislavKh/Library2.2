using Domain.Entities;
using Library2._2.Interfaces.UserInterfaces;
using MediatR;

namespace Library2._2.Queries.UserQueries
{
    public class GetAllUsersQuery : IRequest<List<User>>
    {
        public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<User>>
        {
            private readonly IGetUsersInfo _usersInfo;

            public GetAllUsersQueryHandler(IGetUsersInfo usersInfo)
            {
                _usersInfo = usersInfo;
            }

            public async Task<List<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
            {
                return _usersInfo.GetAll();
            }
        }
    }
}
