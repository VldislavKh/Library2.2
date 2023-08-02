using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }

        [Column("Роль")]
        public string Name { get; set; }
    }
}
