using Library2._2.Entities;
using Library2._2.Infrastructure;
using Library2._2.Interfaces.BookInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Library2._2.Services
{
    public class BookService : IAddDeleteBook, IGetBooksInfo
    {
        private readonly ApplicationContext _context;
        private readonly IDistributedCache _cache;

        public BookService(ApplicationContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public int Add(string title, int year, string genre, int authorId)
        {
            var book = new Book();
            book.Title = title;
            book.Year = year;
            book.Genre = genre;
            book.AuthorId = authorId;
            var author = _context.Authors.FirstOrDefault(x => x.Id == authorId);
            book.Author = author;

            _context.Add(book);
            _context.SaveChanges();
            return book.Id;
        }

        public void Delete(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        public Author GetAuthor(int id)
        {
            Book? book = null;

            var bookString = _cache.GetString(id.ToString());

            if (bookString != null)
            {
                book = JsonConvert.DeserializeObject<Book>(bookString);
            }
            else
            {
                book = _context.Books
                    .Include(x => x.Author)
                    .SingleOrDefault(x => x.Id == id);                    

                if (book != null)
                {
                    bookString = JsonConvert.SerializeObject(book, new JsonSerializerSettings
                                                            {
                                                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                            });

                    _cache.SetString(id.ToString(), bookString);
                }
            }
            return book.Author;
        }

        public string GetGenre(int id)
        {
            return _context.Books.SingleOrDefault(x => x.Id == id).Genre;
        }
    }
}
