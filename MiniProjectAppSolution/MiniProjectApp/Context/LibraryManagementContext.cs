using Microsoft.EntityFrameworkCore;
using MiniProjectApp.Models;
using System.Text;

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
        public DbSet<RentCart> RentCart { get; set; }
        public DbSet<SuperRentCart> SuperRentCart { get; set; }
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

        public DbSet<FineDetail> FineDetails { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
     new Book() { Id = 1, Title = "To Kill a Mockingbird", Author = "Harper Lee", Category = "Fiction", Description = "A novel about the serious issues of rape and racial inequality." },
     new Book() { Id = 2, Title = "1984", Author = "George Orwell", Category = "Dystopian", Description = "A story set in a totalitarian society ruled by Big Brother." },
     new Book() { Id = 3, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Category = "Classic", Description = "A novel about the American dream and the roaring twenties." },
     new Book() { Id = 4, Title = "Moby Dick", Author = "Herman Melville", Category = "Adventure", Description = "A sea captain's journey to hunt the white whale, Moby Dick." },
     new Book() { Id = 5, Title = "Pride and Prejudice", Author = "Jane Austen", Category = "Romance", Description = "A romantic novel that critiques the British landed gentry at the end of the 18th century." },
     new Book() { Id = 6, Title = "The Catcher in the Rye", Author = "J.D. Salinger", Category = "Fiction", Description = "A novel about teenage rebellion and angst." },
     new Book() { Id = 7, Title = "The Hobbit", Author = "J.R.R. Tolkien", Category = "Fantasy", Description = "A fantasy novel about the adventures of Bilbo Baggins." },
     new Book() { Id = 8, Title = "Brave New World", Author = "Aldous Huxley", Category = "Science Fiction", Description = "A dystopian novel set in a futuristic society." },
     new Book() { Id = 9, Title = "War and Peace", Author = "Leo Tolstoy", Category = "Historical Fiction", Description = "A novel that intertwines the lives of five families during the Napoleonic Wars." },
     new Book() { Id = 10, Title = "The Alchemist", Author = "Paulo Coelho", Category = "Philosophical Fiction", Description = "A novel about a young shepherd's journey to find treasure." }
 );

            modelBuilder.Entity<SalesStock>().HasData(
     new SalesStock() { BookId = 1, PricePerBook = 30, QuantityInStock = 10 },
     new SalesStock() { BookId = 2, PricePerBook = 50, QuantityInStock = 5 },
     new SalesStock() { BookId = 3, PricePerBook = 10, QuantityInStock = 10 },
     new SalesStock() { BookId = 4, PricePerBook = 40, QuantityInStock = 8 },
     new SalesStock() { BookId = 5, PricePerBook = 60, QuantityInStock = 6 },
     new SalesStock() { BookId = 6, PricePerBook = 25, QuantityInStock = 12 },
     new SalesStock() { BookId = 7, PricePerBook = 20, QuantityInStock = 15 }
 );

            modelBuilder.Entity<RentStock>().HasData(
                new RentStock() { BookId = 1, RentPerBook = 5, QuantityInStock = 10 },
                new RentStock() { BookId = 2, RentPerBook = 10, QuantityInStock = 10 },
                new RentStock() { BookId = 3, RentPerBook = 15, QuantityInStock = 10 },
                new RentStock() { BookId = 4, RentPerBook = 8, QuantityInStock = 10 },
                new RentStock() { BookId = 5, RentPerBook = 12, QuantityInStock = 8 },
                new RentStock() { BookId = 6, RentPerBook = 7, QuantityInStock = 10 },
                new RentStock() { BookId = 7, RentPerBook = 6, QuantityInStock = 12 }
            );

            modelBuilder.Entity<User>().HasData(
             new User() { Id=1, Name="Spidey", Phone="8378499039", Role="Admin"},
             new User() { Id=2, Name="Peter Parker", Phone= "8293377843", Role = "User"},
              new User() { Id = 3, Name = "Ben Parker", Phone = "3847299304", Role = "Premium User" }
           );

            modelBuilder.Entity<UserCredential>().HasData(
            new UserCredential() { UserId=1, Password= Encoding.UTF8.GetBytes("string"), HashKey= Encoding.UTF8.GetBytes("xyz"), Status="Active" },
            new UserCredential() { UserId = 2, Password = Encoding.UTF8.GetBytes("string"), HashKey = Encoding.UTF8.GetBytes("xyz"), Status = "Active" },
            new UserCredential() { UserId = 3, Password = Encoding.UTF8.GetBytes("string"), HashKey = Encoding.UTF8.GetBytes("xyz"), Status = "Active" }
          );



            // Configure composite key
            modelBuilder.Entity<Cart>()
                .HasKey(sc => new { sc.UserId, sc.BookId });

            modelBuilder.Entity<RentCart>()
               .HasKey(rc => new { rc.UserId, rc.BookId });

            modelBuilder.Entity<SuperRentCart>()
              .HasKey(src => new { src.UserId, src.BookId });

            modelBuilder.Entity<SaleDetail>()
                .HasKey(sd => new { sd.SaleId, sd.BookId });

            modelBuilder.Entity<PurchaseDetail>()
               .HasKey(pd => new { pd.PurchaseId, pd.BookId });

            modelBuilder.Entity<RentDetail>()
              .HasKey(rd => new { rd.RentId, rd.BookId });

            modelBuilder.Entity<FineDetail>()
             .HasKey(fd => new { fd.RentId, fd.BookId });

            // Configure the relationship
            modelBuilder.Entity<Cart>()
                .HasOne(sc => sc.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(sc => sc.UserId);


            

        }

    }
}
