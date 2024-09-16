using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProjectApp.Migrations
{
    public partial class one : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfPurchase = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.PurchaseId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentStocks",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    RentPerBook = table.Column<double>(type: "float", nullable: false),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentStocks", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_RentStocks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesStocks",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    PricePerBook = table.Column<double>(type: "float", nullable: false),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesStocks", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_SalesStocks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseDetails",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PricePerBook = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDetails", x => new { x.PurchaseId, x.BookId });
                    table.ForeignKey(
                        name: "FK_PurchaseDetails_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseDetails_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => new { x.UserId, x.BookId });
                    table.ForeignKey(
                        name: "FK_Cart_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cart_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    FeedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fines",
                columns: table => new
                {
                    FineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    NumberOfBooksFined = table.Column<int>(type: "int", nullable: false),
                    NumbeOfBooksToPayFine = table.Column<int>(type: "int", nullable: false),
                    FineAmount = table.Column<double>(type: "float", nullable: false),
                    FinePending = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RentDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fines", x => x.FineId);
                    table.ForeignKey(
                        name: "FK_Fines_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentCart",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    RentId = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFined = table.Column<int>(type: "int", nullable: false),
                    RentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentCart", x => new { x.UserId, x.BookId });
                    table.ForeignKey(
                        name: "FK_RentCart_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentCart_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rents",
                columns: table => new
                {
                    RentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CartType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfRent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BooksRented = table.Column<int>(type: "int", nullable: false),
                    BooksToBeReturned = table.Column<int>(type: "int", nullable: false),
                    Progress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rents", x => x.RentId);
                    table.ForeignKey(
                        name: "FK_Rents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateOfSale = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoOfBooks = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    FinalAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SaleId);
                    table.ForeignKey(
                        name: "FK_Sales_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SuperRentCart",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    RentId = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFined = table.Column<int>(type: "int", nullable: false),
                    RentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperRentCart", x => new { x.UserId, x.BookId });
                    table.ForeignKey(
                        name: "FK_SuperRentCart_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuperRentCart_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCredentials",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    HashKey = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCredentials", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserCredentials_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FineDetails",
                columns: table => new
                {
                    FineId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    FinePaidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FineAmount = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FineDetails", x => new { x.FineId, x.BookId });
                    table.ForeignKey(
                        name: "FK_FineDetails_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FineDetails_Fines_FineId",
                        column: x => x.FineId,
                        principalTable: "Fines",
                        principalColumn: "FineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentDetails",
                columns: table => new
                {
                    RentId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentDetails", x => new { x.RentId, x.BookId });
                    table.ForeignKey(
                        name: "FK_RentDetails_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentDetails_Fines_FineId",
                        column: x => x.FineId,
                        principalTable: "Fines",
                        principalColumn: "FineId");
                    table.ForeignKey(
                        name: "FK_RentDetails_Rents_RentId",
                        column: x => x.RentId,
                        principalTable: "Rents",
                        principalColumn: "RentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleDetails",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleDetails", x => new { x.SaleId, x.BookId });
                    table.ForeignKey(
                        name: "FK_SaleDetails_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleDetails_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "SaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Category", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "Harper Lee", "Fiction", "A novel about the serious issues of rape and racial inequality.", "To Kill a Mockingbird" },
                    { 2, "George Orwell", "Dystopian", "A story set in a totalitarian society ruled by Big Brother.", "1984" },
                    { 3, "F. Scott Fitzgerald", "Classic", "A novel about the American dream and the roaring twenties.", "The Great Gatsby" },
                    { 4, "Herman Melville", "Adventure", "A sea captain's journey to hunt the white whale, Moby Dick.", "Moby Dick" },
                    { 5, "Jane Austen", "Romance", "A romantic novel that critiques the British landed gentry at the end of the 18th century.", "Pride and Prejudice" },
                    { 6, "J.D. Salinger", "Fiction", "A novel about teenage rebellion and angst.", "The Catcher in the Rye" },
                    { 7, "J.R.R. Tolkien", "Fantasy", "A fantasy novel about the adventures of Bilbo Baggins.", "The Hobbit" },
                    { 8, "Aldous Huxley", "Science Fiction", "A dystopian novel set in a futuristic society.", "Brave New World" },
                    { 9, "Leo Tolstoy", "Historical Fiction", "A novel that intertwines the lives of five families during the Napoleonic Wars.", "War and Peace" },
                    { 10, "Paulo Coelho", "Philosophical Fiction", "A novel about a young shepherd's journey to find treasure.", "The Alchemist" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Phone", "Role", "Status" },
                values: new object[,]
                {
                    { 1, "Spidey", "8378499039", "Admin", "Active" },
                    { 2, "Peter Parker", "8293377843", "User", "Active" },
                    { 3, "Ben Parker", "3847299304", "Premium User", "Active" }
                });

            migrationBuilder.InsertData(
                table: "RentCart",
                columns: new[] { "BookId", "UserId", "DueDate", "IsFined", "RentDate", "RentId" },
                values: new object[] { 2, 2, new DateTime(2024, 6, 27, 15, 18, 16, 350, DateTimeKind.Local).AddTicks(5927), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "RentStocks",
                columns: new[] { "BookId", "QuantityInStock", "RentPerBook" },
                values: new object[,]
                {
                    { 1, 10, 5.0 },
                    { 2, 10, 10.0 },
                    { 3, 10, 15.0 },
                    { 4, 10, 8.0 },
                    { 5, 8, 12.0 },
                    { 6, 10, 7.0 },
                    { 7, 12, 6.0 }
                });

            migrationBuilder.InsertData(
                table: "Rents",
                columns: new[] { "RentId", "Amount", "BooksRented", "BooksToBeReturned", "CartType", "DateOfRent", "DueDate", "Progress", "UserId" },
                values: new object[,]
                {
                    { 1, 15.0, 2, 1, "Rent Cart", new DateTime(2024, 6, 27, 15, 16, 16, 350, DateTimeKind.Local).AddTicks(5869), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Return pending", 2 },
                    { 2, 0.0, 2, 1, "Super Cart", new DateTime(2024, 6, 27, 15, 16, 16, 350, DateTimeKind.Local).AddTicks(5887), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Return pending", 3 }
                });

            migrationBuilder.InsertData(
                table: "SalesStocks",
                columns: new[] { "BookId", "PricePerBook", "QuantityInStock" },
                values: new object[,]
                {
                    { 1, 30.0, 10 },
                    { 2, 50.0, 5 },
                    { 3, 10.0, 10 },
                    { 4, 40.0, 8 },
                    { 5, 60.0, 6 },
                    { 6, 25.0, 12 },
                    { 7, 20.0, 15 }
                });

            migrationBuilder.InsertData(
                table: "SuperRentCart",
                columns: new[] { "BookId", "UserId", "DueDate", "IsFined", "RentDate", "RentId" },
                values: new object[] { 4, 3, new DateTime(2024, 6, 27, 15, 18, 16, 350, DateTimeKind.Local).AddTicks(5945), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.InsertData(
                table: "UserCredentials",
                columns: new[] { "UserId", "HashKey", "Password" },
                values: new object[,]
                {
                    { 1, new byte[] { 120, 121, 122 }, new byte[] { 115, 116, 114, 105, 110, 103 } },
                    { 2, new byte[] { 120, 121, 122 }, new byte[] { 115, 116, 114, 105, 110, 103 } },
                    { 3, new byte[] { 120, 121, 122 }, new byte[] { 115, 116, 114, 105, 110, 103 } }
                });

            migrationBuilder.InsertData(
                table: "RentDetails",
                columns: new[] { "BookId", "RentId", "FineId", "Price", "ReturnDate", "status" },
                values: new object[,]
                {
                    { 1, 1, null, 5.0, new DateTime(2024, 6, 27, 15, 17, 16, 350, DateTimeKind.Local).AddTicks(5907), "Returned" },
                    { 2, 1, null, 10.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Return pending" },
                    { 3, 2, null, 0.0, new DateTime(2024, 6, 27, 15, 17, 16, 350, DateTimeKind.Local).AddTicks(5909), "Returned" },
                    { 4, 2, null, 0.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Return pending" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_BookId",
                table: "Cart",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_BookId",
                table: "Feedbacks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_UserId",
                table: "Feedbacks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FineDetails_BookId",
                table: "FineDetails",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Fines_UserId",
                table: "Fines",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_BookId",
                table: "PurchaseDetails",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_RentCart_BookId",
                table: "RentCart",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_RentDetails_BookId",
                table: "RentDetails",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_RentDetails_FineId",
                table: "RentDetails",
                column: "FineId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_UserId",
                table: "Rents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_BookId",
                table: "SaleDetails",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_UserId",
                table: "Sales",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SuperRentCart_BookId",
                table: "SuperRentCart",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "FineDetails");

            migrationBuilder.DropTable(
                name: "PurchaseDetails");

            migrationBuilder.DropTable(
                name: "RentCart");

            migrationBuilder.DropTable(
                name: "RentDetails");

            migrationBuilder.DropTable(
                name: "RentStocks");

            migrationBuilder.DropTable(
                name: "SaleDetails");

            migrationBuilder.DropTable(
                name: "SalesStocks");

            migrationBuilder.DropTable(
                name: "SuperRentCart");

            migrationBuilder.DropTable(
                name: "UserCredentials");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Fines");

            migrationBuilder.DropTable(
                name: "Rents");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
