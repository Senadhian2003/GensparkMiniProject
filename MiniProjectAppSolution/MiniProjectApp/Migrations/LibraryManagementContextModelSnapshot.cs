﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniProjectApp.Context;

#nullable disable

namespace MiniProjectApp.Migrations
{
    [DbContext(typeof(LibraryManagementContext))]
    partial class LibraryManagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                            Author = "Leo",
                            Category = "Thriller",
                            Description = "xyz",
                            Title = "Ben 10"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Ralph",
                            Category = "Education",
                            Description = "xyz",
                            Title = "Aerodynamics"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Stan Lee",
                            Category = "Comic",
                            Description = "xyz",
                            Title = "Spiderman"
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

            modelBuilder.Entity("MiniProjectApp.Models.Sale", b =>
                {
                    b.HasOne("MiniProjectApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
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

            modelBuilder.Entity("MiniProjectApp.Models.User", b =>
                {
                    b.Navigation("CartItems");
                });
#pragma warning restore 612, 618
        }
    }
}
