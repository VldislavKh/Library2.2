using Library2._2.Interfaces.BookInterfaces;
using MediatR;

namespace Library2._2.Queries.BookQueries
{
    public class GetGenreByIdQuery : IRequest<string>
    {
        public int Id { get; set; }

        public class GetGenreByIdQueryHandler : IRequestHandler<GetGenreByIdQuery, string>
        {
            private readonly IGetBooksInfo _booksInfo;

            public GetGenreByIdQueryHandler(IGetBooksInfo booksInfo)
            {
                _booksInfo = booksInfo;
            }

            public async Task<string> Handle(GetGenreByIdQuery query, CancellationToken cancellationToken)
            {
                return _booksInfo.GetGenre(query.Id);
            }
        }
    }
}
