using Library2._2.Entities;
using Library2._2.Interfaces.AuthorInterfaces;
using MediatR;

namespace Library2._2.Queries.AuthorQueries
{
    public class GetMaxBooksAuthorsQuery : IRequest<List<Author>>
    {
        public class GetMaxBooksAuthorsQueryHandler : IRequestHandler<GetMaxBooksAuthorsQuery, List<Author>>
        {
            private readonly IGetAuthorsInfo _authorsInfo;

            public GetMaxBooksAuthorsQueryHandler(IGetAuthorsInfo authorsInfo)
            {
                _authorsInfo = authorsInfo;
            }

            public async Task<List<Author>> Handle(GetMaxBooksAuthorsQuery query, CancellationToken CancellationToken)
            {
                return _authorsInfo.GetMaxBooksAuthors();
            }
        }
    }
}
