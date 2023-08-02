using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<TestTableHangfire> TestTableHangfires => Set<TestTableHangfire>();

        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Library");
        }
    }
}
