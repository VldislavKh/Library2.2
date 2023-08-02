using Domain.Entities;

namespace Library2._2.Interfaces.BookInterfaces
{
    public interface IGetBooksInfo
    {
        public Author GetAuthor(int id);

        public string GetGenre(int id);
    }
}
