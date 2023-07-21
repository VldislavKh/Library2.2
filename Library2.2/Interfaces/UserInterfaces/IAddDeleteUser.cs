namespace Library2._2.Interfaces.UserInterfaces
{
    public interface IAddDeleteUser
    {
        public int Add(string username, string password);
        public void Delete(int id);
    }
}
