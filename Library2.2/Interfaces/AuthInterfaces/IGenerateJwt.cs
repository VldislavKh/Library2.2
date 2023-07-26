using Library2._2.Entities;

namespace Library2._2.Interfaces.AuthInterfaces
{
    public interface IGenerateJwt
    {
        public string GenerateJWT(User user);
    }
}
