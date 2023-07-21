using Library2._2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library2._2.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();

        public ApplicationContext() { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { }
    }
}
