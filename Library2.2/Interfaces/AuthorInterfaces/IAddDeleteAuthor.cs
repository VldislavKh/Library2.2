namespace Library2._2.Interfaces.AuthorInterfaces
{
    public interface IAddDeleteAuthor
    {
        public int Add(string name, DateOnly birthDate, DateOnly? deathDate);
        public void Delete(int id);
    }
}
