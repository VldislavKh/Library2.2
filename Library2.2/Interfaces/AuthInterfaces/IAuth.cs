using Domain.Entities;

namespace Library2._2.Interfaces.AuthInterfaces
{
    public interface IAuth
    {
        public User Authenticate(string username, string password);
    }
}
