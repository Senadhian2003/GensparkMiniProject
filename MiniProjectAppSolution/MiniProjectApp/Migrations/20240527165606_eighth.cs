using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProjectApp.Migrations
{
    public partial class eighth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleDtails",
                table: "SaleDtails");

            migrationBuilder.RenameTable(
                name: "SaleDtails",
                newName: "SaleDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleDetails",
                table: "SaleDetails",
                columns: new[] { "SaleId", "BookId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleDetails",
                table: "SaleDetails");

            migrationBuilder.RenameTable(
                name: "SaleDetails",
                newName: "SaleDtails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleDtails",
                table: "SaleDtails",
                columns: new[] { "SaleId", "BookId" });
        }
    }
}
