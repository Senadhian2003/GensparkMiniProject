using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProjectApp.Migrations
{
    public partial class six : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublisherId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublisherId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublisherId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublisherId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "PublisherId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "PublisherId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "PublisherId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "PublisherId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "PublisherId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                column: "PublisherId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "RentCart",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { 2, 2 },
                column: "DueDate",
                value: new DateTime(2024, 7, 2, 23, 49, 5, 942, DateTimeKind.Local).AddTicks(5791));

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 1, 1 },
                column: "ReturnDate",
                value: new DateTime(2024, 7, 2, 23, 48, 5, 942, DateTimeKind.Local).AddTicks(5770));

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 3, 2 },
                column: "ReturnDate",
                value: new DateTime(2024, 7, 2, 23, 48, 5, 942, DateTimeKind.Local).AddTicks(5772));

            migrationBuilder.UpdateData(
                table: "Rents",
                keyColumn: "RentId",
                keyValue: 1,
                column: "DateOfRent",
                value: new DateTime(2024, 7, 2, 23, 47, 5, 942, DateTimeKind.Local).AddTicks(5732));

            migrationBuilder.UpdateData(
                table: "Rents",
                keyColumn: "RentId",
                keyValue: 2,
                column: "DateOfRent",
                value: new DateTime(2024, 7, 2, 23, 47, 5, 942, DateTimeKind.Local).AddTicks(5752));

            migrationBuilder.UpdateData(
                table: "SuperRentCart",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { 4, 3 },
                column: "DueDate",
                value: new DateTime(2024, 7, 2, 23, 49, 5, 942, DateTimeKind.Local).AddTicks(5804));

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_PublisherId",
                table: "Books");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublisherId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublisherId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublisherId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublisherId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "PublisherId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "PublisherId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "PublisherId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "PublisherId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "PublisherId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                column: "PublisherId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "RentCart",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { 2, 2 },
                column: "DueDate",
                value: new DateTime(2024, 7, 2, 23, 27, 26, 729, DateTimeKind.Local).AddTicks(6198));

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 1, 1 },
                column: "ReturnDate",
                value: new DateTime(2024, 7, 2, 23, 26, 26, 729, DateTimeKind.Local).AddTicks(6187));

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 3, 2 },
                column: "ReturnDate",
                value: new DateTime(2024, 7, 2, 23, 26, 26, 729, DateTimeKind.Local).AddTicks(6188));

            migrationBuilder.UpdateData(
                table: "Rents",
                keyColumn: "RentId",
                keyValue: 1,
                column: "DateOfRent",
                value: new DateTime(2024, 7, 2, 23, 25, 26, 729, DateTimeKind.Local).AddTicks(6149));

            migrationBuilder.UpdateData(
                table: "Rents",
                keyColumn: "RentId",
                keyValue: 2,
                column: "DateOfRent",
                value: new DateTime(2024, 7, 2, 23, 25, 26, 729, DateTimeKind.Local).AddTicks(6170));

            migrationBuilder.UpdateData(
                table: "SuperRentCart",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { 4, 3 },
                column: "DueDate",
                value: new DateTime(2024, 7, 2, 23, 27, 26, 729, DateTimeKind.Local).AddTicks(6209));
        }
    }
}
