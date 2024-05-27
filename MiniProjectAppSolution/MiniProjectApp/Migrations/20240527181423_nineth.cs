using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProjectApp.Migrations
{
    public partial class nineth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentSales");

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

            migrationBuilder.InsertData(
                table: "SalesStocks",
                columns: new[] { "BookId", "PricePerBook", "QuantityInStock" },
                values: new object[] { 1, 30.0, 10 });

            migrationBuilder.InsertData(
                table: "SalesStocks",
                columns: new[] { "BookId", "PricePerBook", "QuantityInStock" },
                values: new object[] { 2, 50.0, 5 });

            migrationBuilder.InsertData(
                table: "SalesStocks",
                columns: new[] { "BookId", "PricePerBook", "QuantityInStock" },
                values: new object[] { 3, 10.0, 10 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesStocks");

            migrationBuilder.CreateTable(
                name: "CurrentSales",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    PricePerBook = table.Column<double>(type: "float", nullable: false),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentSales", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_CurrentSales_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CurrentSales",
                columns: new[] { "BookId", "PricePerBook", "QuantityInStock" },
                values: new object[] { 1, 30.0, 10 });

            migrationBuilder.InsertData(
                table: "CurrentSales",
                columns: new[] { "BookId", "PricePerBook", "QuantityInStock" },
                values: new object[] { 2, 50.0, 5 });

            migrationBuilder.InsertData(
                table: "CurrentSales",
                columns: new[] { "BookId", "PricePerBook", "QuantityInStock" },
                values: new object[] { 3, 10.0, 10 });
        }
    }
}
