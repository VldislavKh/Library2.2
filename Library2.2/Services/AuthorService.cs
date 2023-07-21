using Library2._2.Entities;
using Library2._2.Infrastructure;

namespace Library2._2.Services
{
    public class AuthorService
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
    }
}
