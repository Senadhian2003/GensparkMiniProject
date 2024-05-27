using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProjectApp.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuperCarts");

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => new { x.UserId, x.BookId });
                    table.ForeignKey(
                        name: "FK_Cart_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrentSale",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    PricePerBook = table.Column<double>(type: "float", nullable: false),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentSale", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_CurrentSale_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Category", "Description", "Title" },
                values: new object[] { 1, "Leo", "Thriller", "xyz", "Ben 10" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Category", "Description", "Title" },
                values: new object[] { 2, "Ralph", "Education", "xyz", "Aerodynamics" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Category", "Description", "Title" },
                values: new object[] { 3, "Stan Lee", "Comic", "xyz", "Spiderman" });

            migrationBuilder.InsertData(
                table: "CurrentSale",
                columns: new[] { "BookId", "PricePerBook", "QuantityInStock" },
                values: new object[] { 1, 30.0, 10 });

            migrationBuilder.InsertData(
                table: "CurrentSale",
                columns: new[] { "BookId", "PricePerBook", "QuantityInStock" },
                values: new object[] { 2, 50.0, 5 });

            migrationBuilder.InsertData(
                table: "CurrentSale",
                columns: new[] { "BookId", "PricePerBook", "QuantityInStock" },
                values: new object[] { 3, 10.0, 10 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "CurrentSale");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.CreateTable(
                name: "SuperCarts",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperCarts", x => new { x.UserId, x.BookId });
                    table.ForeignKey(
                        name: "FK_SuperCarts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
