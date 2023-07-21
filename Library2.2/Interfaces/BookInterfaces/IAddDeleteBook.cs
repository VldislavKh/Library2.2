namespace Library2._2.Interfaces.BookInterfaces
{
    public interface IAddDeleteBook
    {
        public int Add(string title, int year, string genre, int authorId);
        public void Delete(int id);
    }
}
