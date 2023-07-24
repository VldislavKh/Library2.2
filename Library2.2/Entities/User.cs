using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Library2._2.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Column("Имя")]
        public string Name { get; set; }

        [Column("Пароль")]
        public string Password { get; set; }

        [Column("Id роли")]
        public Role? Role { get; set; }
    }
}
