using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Column("Название")]
        public string Title { get; set; }

        [Column("Год издания")]
        public int Year { get; set; }

        [Column("Жанр")]
        public string Genre { get; set; }

        [Column("Id автора")]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
