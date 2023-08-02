using Domain.Entities;
using Domain.Infrastructure;
using Library2._2.Interfaces.AuthorInterfaces;

namespace Library2._2.Services
{
    public class AuthorService : IAddDeleteAuthor, IGetAuthorsInfo
    {
        private readonly ApplicationContext _context;
        public AuthorService(ApplicationContext context)
        {
            _context = context;
        }
        public int Add(string name, DateOnly birthDate, DateOnly? deathDate)
        {
            var author = new Author();
            author.Name = name;
            author.BirthDate = birthDate;
            author.DeathDate = deathDate;

            _context.Add(author);
            _context.SaveChanges();
            return author.Id;
        }

        public void Delete(int id)
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == id);

            if (author != null)
            {
                _context.Remove(author);
                _context.SaveChanges();
            }
        }

        public List<Author> GetAll()
        {
            return _context.Authors.ToList();
        }

        public List<Book> GetBooks(int id)
        {
            return _context.Books.Where(x => x.AuthorId == id).ToList();
        }

        public List<Author> GetMaxBooksAuthors()
        {
            int maxBooks = _context.Authors.Max(x => x.Books.Count);
            return _context.Authors.Where(x => x.Books.Count == maxBooks)
                                    .OrderBy(x => x.Name).ToList();
        }
    }
}
