using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Models;

namespace MiniProjectApp.Context
{
    public class LibraryManagementContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=4N8CBX3\SQLEXPRESS;Integrated Security=true;Initial Catalog=libraryManagement;");
        }

        public DbSet <User> Users { get; set; }
        public DbSet <UserCredential> UserCredentials { get; set; }
        public DbSet <SuperCart> SuperCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure composite key
            modelBuilder.Entity<SuperCart>()
                .HasKey(sc => new { sc.UserId, sc.BookId });

            // Configure the relationship
            modelBuilder.Entity<SuperCart>()
                .HasOne(sc => sc.User)
                .WithMany(u => u.SuperCartItems)
                .HasForeignKey(sc => sc.UserId);

            
        }

    }
}
