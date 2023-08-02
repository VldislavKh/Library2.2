using Domain.Entities;
using Library2._2.Interfaces.AuthorInterfaces;
using MediatR;

namespace Library2._2.Queries.AuthorQueries
{
    public class GetAllAuthorsQuery : IRequest<List<Author>>
    {
        public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, List<Author>>
        {
            private readonly IGetAuthorsInfo _authorsInfo;

            public GetAllAuthorsQueryHandler(IGetAuthorsInfo authorsInfo)
            {
                _authorsInfo = authorsInfo;
            }

            public async Task<List<Author>> Handle(GetAllAuthorsQuery query, CancellationToken cancellationToken)
            {
                return _authorsInfo.GetAll();
            }
        }
    }
}
