using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Models;

namespace MiniProjectApp.Context
{
    public class LibraryManagementContext : DbContext
    {
        public LibraryManagementContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet <User> Users { get; set; }
        public DbSet <UserCredential> UserCredentials { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet <Cart> Cart { get; set; }
        public DbSet<SalesStock> SalesStocks { get; set; }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
              new Book() { Id=1, Title="Ben 10", Author="Leo", Category="Thriller", Description = "xyz" },
              new Book() { Id = 2, Title = "Aerodynamics", Author = "Ralph", Category = "Education", Description = "xyz" },
               new Book() { Id = 3, Title = "Spiderman", Author = "Stan Lee", Category = "Comic", Description = "xyz" }
              );


            modelBuilder.Entity<SalesStock>().HasData(
               new SalesStock() { BookId = 1, PricePerBook=30, QuantityInStock=10 },
               new SalesStock() { BookId = 2, PricePerBook = 50, QuantityInStock = 5 },
                new SalesStock() { BookId = 3, PricePerBook = 10, QuantityInStock = 10 }
               );

            // Configure composite key
            modelBuilder.Entity<Cart>()
                .HasKey(sc => new { sc.UserId, sc.BookId });

            modelBuilder.Entity<SaleDetail>()
                .HasKey(sd => new { sd.SaleId, sd.BookId });

            // Configure the relationship
            modelBuilder.Entity<Cart>()
                .HasOne(sc => sc.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(sc => sc.UserId);

            
        }

    }
}
