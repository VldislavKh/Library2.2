using Library2._2.Entities;
using Library2._2.Interfaces.BookInterfaces;
using MediatR;

namespace Library2._2.Queries.BookQueries
{
    public class GetAuthorByIdQuery : IRequest<Author>
    {
        public int Id { get; set; }
        public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author>
        {
            private readonly IGetBooksInfo _getBooksInfo;

            public GetAuthorByIdQueryHandler(IGetBooksInfo getAuthorsInfo)
            {
                _getBooksInfo = getAuthorsInfo;
            }

            public async Task<Author> Handle(GetAuthorByIdQuery query, CancellationToken cancellationToken)
            {
                return _getBooksInfo.GetAuthor(query.Id);
            }
        }

    }
}
