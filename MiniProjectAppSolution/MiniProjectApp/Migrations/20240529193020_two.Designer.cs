﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniProjectApp.Context;

#nullable disable

namespace MiniProjectApp.Migrations
{
    [DbContext(typeof(LibraryManagementContext))]
    [Migration("20240529193020_two")]
    partial class two
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MiniProjectApp.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Harper Lee",
                            Category = "Fiction",
                            Description = "A novel about the serious issues of rape and racial inequality.",
                            Title = "To Kill a Mockingbird"
                        },
                        new
                        {
                            Id = 2,
                            Author = "George Orwell",
                            Category = "Dystopian",
                            Description = "A story set in a totalitarian society ruled by Big Brother.",
                            Title = "1984"
                        },
                        new
                        {
                            Id = 3,
                            Author = "F. Scott Fitzgerald",
                            Category = "Classic",
                            Description = "A novel about the American dream and the roaring twenties.",
                            Title = "The Great Gatsby"
                        },
                        new
                        {
                            Id = 4,
                            Author = "Herman Melville",
                            Category = "Adventure",
                            Description = "A sea captain's journey to hunt the white whale, Moby Dick.",
                            Title = "Moby Dick"
                        },
                        new
                        {
                            Id = 5,
                            Author = "Jane Austen",
                            Category = "Romance",
                            Description = "A romantic novel that critiques the British landed gentry at the end of the 18th century.",
                            Title = "Pride and Prejudice"
                        },
                        new
                        {
                            Id = 6,
                            Author = "J.D. Salinger",
                            Category = "Fiction",
                            Description = "A novel about teenage rebellion and angst.",
                            Title = "The Catcher in the Rye"
                        },
                        new
                        {
                            Id = 7,
                            Author = "J.R.R. Tolkien",
                            Category = "Fantasy",
                            Description = "A fantasy novel about the adventures of Bilbo Baggins.",
                            Title = "The Hobbit"
                        },
                        new
                        {
                            Id = 8,
                            Author = "Aldous Huxley",
                            Category = "Science Fiction",
                            Description = "A dystopian novel set in a futuristic society.",
                            Title = "Brave New World"
                        },
                        new
                        {
                            Id = 9,
                            Author = "Leo Tolstoy",
                            Category = "Historical Fiction",
                            Description = "A novel that intertwines the lives of five families during the Napoleonic Wars.",
                            Title = "War and Peace"
                        },
                        new
                        {
                            Id = 10,
                            Author = "Paulo Coelho",
                            Category = "Philosophical Fiction",
                            Description = "A novel about a young shepherd's journey to find treasure.",
                            Title = "The Alchemist"
                        });
                });

            modelBuilder.Entity("MiniProjectApp.Models.Cart", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("UserId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("MiniProjectApp.Models.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackId"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FeedbackId");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("MiniProjectApp.Models.Fine", b =>
                {
                    b.Property<int>("RentId")
                        .HasColumnType("int");

                    b.Property<double>("FineAmount")
                        .HasColumnType("float");

                    b.Property<DateTime?>("FinePaidDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumberOfBooksFined")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RentId");

                    b.ToTable("Fines");
                });

            modelBuilder.Entity("MiniProjectApp.Models.Purchase", b =>
                {
                    b.Property<int>("PurchaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PurchaseId"), 1L, 1);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("DateOfPurchase")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PurchaseId");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("MiniProjectApp.Models.PurchaseDetail", b =>
                {
                    b.Property<int>("PurchaseId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<double>("PricePerBook")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("PurchaseId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("PurchaseDetails");
                });

            modelBuilder.Entity("MiniProjectApp.Models.Rent", b =>
                {
                    b.Property<int>("RentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RentId"), 1L, 1);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("BooksFined")
                        .HasColumnType("int");

                    b.Property<int>("BooksToBeReturned")
                        .HasColumnType("int");

                    b.Property<string>("CartType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfRent")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("FineAmount")
                        .HasColumnType("float");

                    b.Property<string>("Progress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RentId");

                    b.HasIndex("UserId");

                    b.ToTable("Rents");
                });

            modelBuilder.Entity("MiniProjectApp.Models.RentCart", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("RentId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("RentCart");
                });

            modelBuilder.Entity("MiniProjectApp.Models.RentDetail", b =>
                {
                    b.Property<int>("RentId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RentId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("RentDetails");
                });

            modelBuilder.Entity("MiniProjectApp.Models.RentStock", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityInStock")
                        .HasColumnType("int");

                    b.Property<double>("RentPerBook")
                        .HasColumnType("float");

                    b.HasKey("BookId");

                    b.ToTable("RentStocks");

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            QuantityInStock = 10,
                            RentPerBook = 5.0
                        },
                        new
                        {
                            BookId = 2,
                            QuantityInStock = 10,
                            RentPerBook = 10.0
                        },
                        new
                        {
                            BookId = 3,
                            QuantityInStock = 10,
                            RentPerBook = 15.0
                        },
                        new
                        {
                            BookId = 4,
                            QuantityInStock = 10,
                            RentPerBook = 8.0
                        },
                        new
                        {
                            BookId = 5,
                            QuantityInStock = 8,
                            RentPerBook = 12.0
                        },
                        new
                        {
                            BookId = 6,
                            QuantityInStock = 10,
                            RentPerBook = 7.0
                        },
                        new
                        {
                            BookId = 7,
                            QuantityInStock = 12,
                            RentPerBook = 6.0
                        });
                });

            modelBuilder.Entity("MiniProjectApp.Models.Sale", b =>
                {
                    b.Property<int>("SaleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SaleId"), 1L, 1);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("DateOfSale")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("SaleId");

                    b.HasIndex("UserId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("MiniProjectApp.Models.SaleDetail", b =>
                {
                    b.Property<int>("SaleId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.HasKey("SaleId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("SaleDetails");
                });

            modelBuilder.Entity("MiniProjectApp.Models.SalesStock", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<double>("PricePerBook")
                        .HasColumnType("float");

                    b.Property<int>("QuantityInStock")
                        .HasColumnType("int");

                    b.HasKey("BookId");

                    b.ToTable("SalesStocks");

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            PricePerBook = 30.0,
                            QuantityInStock = 10
                        },
                        new
                        {
                            BookId = 2,
                            PricePerBook = 50.0,
                            QuantityInStock = 5
                        },
                        new
                        {
                            BookId = 3,
                            PricePerBook = 10.0,
                            QuantityInStock = 10
                        },
                        new
                        {
                            BookId = 4,
                            PricePerBook = 40.0,
                            QuantityInStock = 8
                        },
                        new
                        {
                            BookId = 5,
                            PricePerBook = 60.0,
                            QuantityInStock = 6
                        },
                        new
                        {
                            BookId = 6,
                            PricePerBook = 25.0,
                            QuantityInStock = 12
                        },
                        new
                        {
                            BookId = 7,
                            PricePerBook = 20.0,
                            QuantityInStock = 15
                        });
                });

            modelBuilder.Entity("MiniProjectApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MiniProjectApp.Models.UserCredential", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<byte[]>("HashKey")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UserCredentials");
                });

            modelBuilder.Entity("MiniProjectApp.Models.Cart", b =>
                {
                    b.HasOne("MiniProjectApp.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniProjectApp.Models.User", "User")
                        .WithMany("CartItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MiniProjectApp.Models.Feedback", b =>
                {
                    b.HasOne("MiniProjectApp.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniProjectApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MiniProjectApp.Models.Fine", b =>
                {
                    b.HasOne("MiniProjectApp.Models.Rent", "Rent")
                        .WithOne("Fine")
                        .HasForeignKey("MiniProjectApp.Models.Fine", "RentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rent");
                });

            modelBuilder.Entity("MiniProjectApp.Models.PurchaseDetail", b =>
                {
                    b.HasOne("MiniProjectApp.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniProjectApp.Models.Purchase", "Purchase")
                        .WithMany("PurchaseDetailsList")
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Purchase");
                });

            modelBuilder.Entity("MiniProjectApp.Models.Rent", b =>
                {
                    b.HasOne("MiniProjectApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MiniProjectApp.Models.RentCart", b =>
                {
                    b.HasOne("MiniProjectApp.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniProjectApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MiniProjectApp.Models.RentDetail", b =>
                {
                    b.HasOne("MiniProjectApp.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniProjectApp.Models.Rent", "Rent")
                        .WithMany("RentDetailsList")
                        .HasForeignKey("RentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Rent");
                });

            modelBuilder.Entity("MiniProjectApp.Models.RentStock", b =>
                {
                    b.HasOne("MiniProjectApp.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("MiniProjectApp.Models.Sale", b =>
                {
                    b.HasOne("MiniProjectApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MiniProjectApp.Models.SaleDetail", b =>
                {
                    b.HasOne("MiniProjectApp.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniProjectApp.Models.Sale", "Sale")
                        .WithMany("SaleDetailList")
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Sale");
                });

            modelBuilder.Entity("MiniProjectApp.Models.SalesStock", b =>
                {
                    b.HasOne("MiniProjectApp.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("MiniProjectApp.Models.UserCredential", b =>
                {
                    b.HasOne("MiniProjectApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MiniProjectApp.Models.Purchase", b =>
                {
                    b.Navigation("PurchaseDetailsList");
                });

            modelBuilder.Entity("MiniProjectApp.Models.Rent", b =>
                {
                    b.Navigation("Fine")
                        .IsRequired();

                    b.Navigation("RentDetailsList");
                });

            modelBuilder.Entity("MiniProjectApp.Models.Sale", b =>
                {
                    b.Navigation("SaleDetailList");
                });

            modelBuilder.Entity("MiniProjectApp.Models.User", b =>
                {
                    b.Navigation("CartItems");
                });
#pragma warning restore 612, 618
        }
    }
}
