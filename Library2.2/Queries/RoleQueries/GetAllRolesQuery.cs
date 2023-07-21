using Library2._2.Entities;
using Library2._2.Interfaces.RoleInterfaces;
using MediatR;

namespace Library2._2.Queries.RoleQueries
{
    public class GetAllRolesQuery : IRequest<List<Role>>
    {
        public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, List<Role>>
        {
            private readonly IGetRolesInfo _rolesInfo;

            public GetAllRolesQueryHandler(IGetRolesInfo rolesInfo)
            {
                _rolesInfo = rolesInfo;
            }

            public async Task<List<Role>> Handle(GetAllRolesQuery query, CancellationToken cancellationToken)
            {
                return _rolesInfo.GetAll();
            }
        }
    }
}
