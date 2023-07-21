using Library2._2.Entities;
using Library2._2.Interfaces.AuthorInterfaces;
using MediatR;

namespace Library2._2.Queries.AuthorQueries
{
    public class GetAuthorsBooksQuery : IRequest<List<Book>>
    {
        public int AuthorId;
        public class GetAuthorsBooksQueryHandler : IRequestHandler<GetAuthorsBooksQuery, List<Book>>
        {
            private readonly IGetAuthorsInfo _authorsInfo;

            public GetAuthorsBooksQueryHandler(IGetAuthorsInfo authorsInfo)
            {
                _authorsInfo = authorsInfo;
            }

            public async Task<List<Book>> Handle(GetAuthorsBooksQuery query, CancellationToken cancellationToken)
            {
                return _authorsInfo.GetBooks(query.AuthorId);
            }
        }
    }
}
