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

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }

        public DbSet<RentStock> RentStocks { get; set; }

        public DbSet<Rent> Rents { get; set; }

        public DbSet<RentDetail> RentDetails { get; set; }

        public DbSet<Fine> Fines { get; set; }



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


            modelBuilder.Entity<RentStock>().HasData(
              new RentStock() { BookId = 1,  RentPerBook = 5, QuantityInStock = 10 },
              new RentStock() { BookId = 2, RentPerBook = 10, QuantityInStock = 10 },
              new RentStock() { BookId = 3, RentPerBook = 15, QuantityInStock = 10 }
              );



            // Configure composite key
            modelBuilder.Entity<Cart>()
                .HasKey(sc => new { sc.UserId, sc.BookId });

            modelBuilder.Entity<SaleDetail>()
                .HasKey(sd => new { sd.SaleId, sd.BookId });

            modelBuilder.Entity<PurchaseDetail>()
               .HasKey(pd => new { pd.PurchaseId, pd.BookId });
            modelBuilder.Entity<RentDetail>()
              .HasKey(rd => new { rd.RentId, rd.BookId });

            // Configure the relationship
            modelBuilder.Entity<Cart>()
                .HasOne(sc => sc.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(sc => sc.UserId);


            

        }

    }
}
