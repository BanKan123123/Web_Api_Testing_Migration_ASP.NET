using Microsoft.EntityFrameworkCore;
using Web_Api_Testing_Migration.Models;

namespace Web_Api_Testing_Migration.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<author> author { get; set; }
        public DbSet<category> category { get; set; }
        public DbSet<book> book { get; set; }
        public DbSet<bookView> bookView { get; set; }
        public DbSet<chapter> chapter { get; set; }
        public DbSet<completedbook> completedbook { get; set; }
        public DbSet<suggestbook> suggestbook { get; set; }
        public DbSet<users> users { get; set; }
        public DbSet<categoriesonbook> categoriesonbooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<categoriesonbook>()
                .HasKey(cob => new { cob.bookId, cob.categoryId });
            modelBuilder.Entity<categoriesonbook>()
                .HasOne(cob => cob.book)  
                .WithMany(b => b.categories)
                .HasForeignKey(cob => cob.bookId);
            modelBuilder.Entity<categoriesonbook>()
                .HasOne(cob => cob.category)
                .WithMany(c => c.books)
                .HasForeignKey(cob => cob.categoryId);

            modelBuilder.Entity<book>()
                .Property(b => b.id)
                .ValueGeneratedOnAdd();
        }
    }
}
