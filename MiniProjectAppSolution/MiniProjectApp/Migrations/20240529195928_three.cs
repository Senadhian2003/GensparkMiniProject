using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProjectApp.Migrations
{
    public partial class three : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BooksFined",
                table: "Rents");

            migrationBuilder.CreateTable(
                name: "SuperRentCart",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    RentId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_SuperRentCart_BookId",
                table: "SuperRentCart",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuperRentCart");

            migrationBuilder.AddColumn<int>(
                name: "BooksFined",
                table: "Rents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
