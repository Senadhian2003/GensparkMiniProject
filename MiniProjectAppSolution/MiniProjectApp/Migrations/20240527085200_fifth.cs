using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProjectApp.Migrations
{
    public partial class fifth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentSale_Books_BookId",
                table: "CurrentSale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrentSale",
                table: "CurrentSale");

            migrationBuilder.RenameTable(
                name: "CurrentSale",
                newName: "CurrentSales");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrentSales",
                table: "CurrentSales",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentSales_Books_BookId",
                table: "CurrentSales",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentSales_Books_BookId",
                table: "CurrentSales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrentSales",
                table: "CurrentSales");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Cart");

            migrationBuilder.RenameTable(
                name: "CurrentSales",
                newName: "CurrentSale");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrentSale",
                table: "CurrentSale",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentSale_Books_BookId",
                table: "CurrentSale",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
