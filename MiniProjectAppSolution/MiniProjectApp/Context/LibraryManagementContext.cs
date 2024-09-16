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
        public DbSet<Author> Authors { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

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
     new Book { Id = 1, Title = "To Kill a Mockingbird", AuthorId = 1, PublisherId = 1, Category = "Fiction", Description = "A novel about the serious issues of rape and racial inequality." },
     new Book { Id = 2, Title = "1984", AuthorId = 2, PublisherId = 2, Category = "Dystopian", Description = "A story set in a totalitarian society ruled by Big Brother." },
     new Book { Id = 3, Title = "The Great Gatsby", AuthorId = 3, PublisherId = 3, Category = "Classic", Description = "A novel about the American dream and the roaring twenties." },
     new Book { Id = 4, Title = "Moby Dick", AuthorId = 4, PublisherId = 4, Category = "Adventure", Description = "A sea captain's journey to hunt the white whale, Moby Dick." },
     new Book { Id = 5, Title = "Pride and Prejudice", AuthorId = 5, PublisherId = 5, Category = "Romance", Description = "A romantic novel that critiques the British landed gentry at the end of the 18th century." },
     new Book { Id = 6, Title = "The Catcher in the Rye", AuthorId = 6, PublisherId = 6, Category = "Fiction", Description = "A novel about teenage rebellion and angst." },
     new Book { Id = 7, Title = "The Hobbit", AuthorId = 7, PublisherId = 7, Category = "Fantasy", Description = "A fantasy novel about the adventures of Bilbo Baggins." },
     new Book { Id = 8, Title = "Brave New World", AuthorId = 8, PublisherId = 8, Category = "Science Fiction", Description = "A dystopian novel set in a futuristic society." },
     new Book { Id = 9, Title = "War and Peace", AuthorId = 9, PublisherId = 9, Category = "Historical Fiction", Description = "A novel that intertwines the lives of five families during the Napoleonic Wars." },
     new Book { Id = 10, Title = "The Alchemist", AuthorId = 10, PublisherId = 10, Category = "Philosophical Fiction", Description = "A novel about a young shepherd's journey to find treasure." }
 );



            modelBuilder.Entity<Author>().HasData(
        new Author { Id = 1, AuthorName = "Harper Lee", Phone = "123-456-7890", Address = "123 Mockingbird Lane" },
        new Author { Id = 2, AuthorName = "George Orwell", Phone = "234-567-8901", Address = "456 Big Brother St" },
        new Author { Id = 3, AuthorName = "F. Scott Fitzgerald", Phone = "345-678-9012", Address = "789 Gatsby Ave" },
        new Author { Id = 4, AuthorName = "Herman Melville", Phone = "456-789-0123", Address = "101 Moby Dock Rd" },
        new Author { Id = 5, AuthorName = "Jane Austen", Phone = "567-890-1234", Address = "202 Pride Blvd" },
        new Author { Id = 6, AuthorName = "J.D. Salinger", Phone = "678-901-2345", Address = "303 Catcher St" },
        new Author { Id = 7, AuthorName = "J.R.R. Tolkien", Phone = "789-012-3456", Address = "404 Hobbiton Ln" },
        new Author { Id = 8, AuthorName = "Aldous Huxley", Phone = "890-123-4567", Address = "505 Brave New World Blvd" },
        new Author { Id = 9, AuthorName = "Leo Tolstoy", Phone = "901-234-5678", Address = "606 War and Peace Dr" },
        new Author { Id = 10, AuthorName = "Paulo Coelho", Phone = "012-345-6789", Address = "707 Alchemist Ave" }
    );

            modelBuilder.Entity<Publisher>().HasData(
     new Publisher { Id = 1, PublisherName = "Penguin Random House", City = "New York", State = "NY", Country = "USA" },
     new Publisher { Id = 2, PublisherName = "HarperCollins", City = "New York", State = "NY", Country = "USA" },
     new Publisher { Id = 3, PublisherName = "Simon & Schuster", City = "New York", State = "NY", Country = "USA" },
     new Publisher { Id = 4, PublisherName = "Hachette Livre", City = "Paris", State = "Île-de-France", Country = "France" },
     new Publisher { Id = 5, PublisherName = "Macmillan Publishers", City = "London", State = "Greater London", Country = "UK" },
     new Publisher { Id = 6, PublisherName = "Scholastic", City = "New York", State = "NY", Country = "USA" },
     new Publisher { Id = 7, PublisherName = "Pearson", City = "London", State = "Greater London", Country = "UK" },
     new Publisher { Id = 8, PublisherName = "Springer Nature", City = "Berlin", State = "Berlin", Country = "Germany" },
     new Publisher { Id = 9, PublisherName = "Cengage", City = "Boston", State = "MA", Country = "USA" },
     new Publisher { Id = 10, PublisherName = "Wiley", City = "Hoboken", State = "NJ", Country = "USA" }
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
             new User() { Id=1, Name="Spidey", Phone="8378499039", Role="Admin", Status = "Active" },
             new User() { Id=2, Name="Peter Parker", Phone= "8293377843", Role = "User", Status = "Active" },
              new User() { Id = 3, Name = "Ben Parker", Phone = "3847299304", Role = "Premium User", Status = "Active" }
           );

            modelBuilder.Entity<UserCredential>().HasData(
            new UserCredential() { UserId=1, Password= Encoding.UTF8.GetBytes("string"), HashKey= Encoding.UTF8.GetBytes("xyz") },
            new UserCredential() { UserId = 2, Password = Encoding.UTF8.GetBytes("string"), HashKey = Encoding.UTF8.GetBytes("xyz") },
            new UserCredential() { UserId = 3, Password = Encoding.UTF8.GetBytes("string"), HashKey = Encoding.UTF8.GetBytes("xyz") }
          );

            modelBuilder.Entity<Rent>().HasData(
           new Rent() {  RentId = 1, DateOfRent=DateTime.Now.AddMinutes(-4), Amount = 15, BooksToBeReturned = 1, CartType = "Rent Cart", UserId = 2, BooksRented=2, Progress="Return pending"},
            new Rent() { RentId = 2, DateOfRent = DateTime.Now.AddMinutes(-4), Amount = 0, BooksToBeReturned = 1, CartType = "Super Cart", UserId = 3, BooksRented = 2, Progress = "Return pending" }

         );

            modelBuilder.Entity<RentDetail>().HasData(
          new RentDetail() { RentId = 1, BookId = 1, Price = 5, ReturnDate = DateTime.Now.AddMinutes(-3), status="Returned" },
          new RentDetail() { RentId = 1, BookId = 2, Price = 10,status = "Return pending" },
          new RentDetail() { RentId = 2, BookId = 3, Price = 0, ReturnDate = DateTime.Now.AddMinutes(-3), status = "Returned" },
           new RentDetail() { RentId = 2, BookId = 4, Price = 0, status = "Return pending" }

        );

            modelBuilder.Entity<RentCart>().HasData(
         new RentCart() { RentId = 1, BookId = 2, DueDate=DateTime.Now.AddMinutes(-2), IsFined=0, UserId=2 }
       );

            modelBuilder.Entity<SuperRentCart>().HasData(
        new RentCart() { RentId = 2, BookId = 4, DueDate = DateTime.Now.AddMinutes(-2), IsFined = 0, UserId = 3 }
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
             .HasKey(fd => new { fd.FineId, fd.BookId });

            // Configure the relationship
            modelBuilder.Entity<Cart>()
                .HasOne(sc => sc.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(sc => sc.UserId);


            

        }

    }
}
