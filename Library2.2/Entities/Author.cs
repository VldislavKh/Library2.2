using System.ComponentModel.DataAnnotations.Schema;

namespace Library2._2.Entities
{
    public class Author
    {
        public int Id { get; set; }

        [Column("ФИО")]
        public string Name { get; set; }

        [Column("Дата рождения")]
        public DateOnly BirthDate { get; set; }

        [Column("Дата смерти")]
        public DateOnly? DeathDate { get; set; }


        public List<Book>? Books { get; set; } = new();
    }
}
