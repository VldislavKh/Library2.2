using Library2._2.Entities;

namespace Library2._2.Interfaces.AuthorInterfaces
{
    public interface IGetAuthorsInfo
    {
        public List<Author> GetAll();

        public List<Book> GetBooks(int id);

        public List<Author> GetMaxBooksAuthors();
    }
}
