using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProjectApp.Migrations
{
    public partial class seven : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FineDetails_Fines_RentId",
                table: "FineDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_FineDetails_Rents_RentId",
                table: "FineDetails",
                column: "RentId",
                principalTable: "Rents",
                principalColumn: "RentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FineDetails_Rents_RentId",
                table: "FineDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_FineDetails_Fines_RentId",
                table: "FineDetails",
                column: "RentId",
                principalTable: "Fines",
                principalColumn: "RentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
