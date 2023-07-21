using System.ComponentModel.DataAnnotations.Schema;

namespace Library2._2.Entities
{
    public class Role
    {
        public int Id { get; set; }

        [Column("Роль")]
        public string Name { get; set; }
    }
}
