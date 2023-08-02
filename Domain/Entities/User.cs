using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Column("Имя")]
        public string Name { get; set; }

        [Column("Пароль")]
        public string Password { get; set; }

        [Column("Id роли")]
        public int RoleId { get; set; }

        public Role? Role { get; set; }
    }
}
