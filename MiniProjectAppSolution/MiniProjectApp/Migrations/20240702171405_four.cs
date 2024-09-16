using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProjectApp.Migrations
{
    public partial class four : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublisherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "City", "Country", "PublisherName", "State" },
                values: new object[,]
                {
                    { 1, "New York", "USA", "Penguin Random House", "NY" },
                    { 2, "New York", "USA", "HarperCollins", "NY" },
                    { 3, "New York", "USA", "Simon & Schuster", "NY" },
                    { 4, "Paris", "France", "Hachette Livre", "" },
                    { 5, "London", "UK", "Macmillan Publishers", "" },
                    { 6, "New York", "USA", "Scholastic", "NY" },
                    { 7, "London", "UK", "Pearson", "" },
                    { 8, "Berlin", "Germany", "Springer Nature", "" },
                    { 9, "Boston", "USA", "Cengage", "MA" },
                    { 10, "Hoboken", "USA", "Wiley", "NJ" }
                });

            migrationBuilder.UpdateData(
                table: "RentCart",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { 2, 2 },
                column: "DueDate",
                value: new DateTime(2024, 7, 2, 22, 42, 4, 731, DateTimeKind.Local).AddTicks(3653));

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 1, 1 },
                column: "ReturnDate",
                value: new DateTime(2024, 7, 2, 22, 41, 4, 731, DateTimeKind.Local).AddTicks(3641));

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 3, 2 },
                column: "ReturnDate",
                value: new DateTime(2024, 7, 2, 22, 41, 4, 731, DateTimeKind.Local).AddTicks(3643));

            migrationBuilder.UpdateData(
                table: "Rents",
                keyColumn: "RentId",
                keyValue: 1,
                column: "DateOfRent",
                value: new DateTime(2024, 7, 2, 22, 40, 4, 731, DateTimeKind.Local).AddTicks(3610));

            migrationBuilder.UpdateData(
                table: "Rents",
                keyColumn: "RentId",
                keyValue: 2,
                column: "DateOfRent",
                value: new DateTime(2024, 7, 2, 22, 40, 4, 731, DateTimeKind.Local).AddTicks(3630));

            migrationBuilder.UpdateData(
                table: "SuperRentCart",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { 4, 3 },
                column: "DueDate",
                value: new DateTime(2024, 7, 2, 22, 42, 4, 731, DateTimeKind.Local).AddTicks(3663));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Books");

            migrationBuilder.UpdateData(
                table: "RentCart",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { 2, 2 },
                column: "DueDate",
                value: new DateTime(2024, 6, 28, 23, 36, 48, 571, DateTimeKind.Local).AddTicks(3567));

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 1, 1 },
                column: "ReturnDate",
                value: new DateTime(2024, 6, 28, 23, 35, 48, 571, DateTimeKind.Local).AddTicks(3541));

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 3, 2 },
                column: "ReturnDate",
                value: new DateTime(2024, 6, 28, 23, 35, 48, 571, DateTimeKind.Local).AddTicks(3544));

            migrationBuilder.UpdateData(
                table: "Rents",
                keyColumn: "RentId",
                keyValue: 1,
                column: "DateOfRent",
                value: new DateTime(2024, 6, 28, 23, 34, 48, 571, DateTimeKind.Local).AddTicks(3340));

            migrationBuilder.UpdateData(
                table: "Rents",
                keyColumn: "RentId",
                keyValue: 2,
                column: "DateOfRent",
                value: new DateTime(2024, 6, 28, 23, 34, 48, 571, DateTimeKind.Local).AddTicks(3360));

            migrationBuilder.UpdateData(
                table: "SuperRentCart",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { 4, 3 },
                column: "DueDate",
                value: new DateTime(2024, 6, 28, 23, 36, 48, 571, DateTimeKind.Local).AddTicks(3580));
        }
    }
}
