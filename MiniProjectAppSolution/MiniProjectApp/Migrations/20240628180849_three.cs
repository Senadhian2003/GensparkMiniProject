using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProjectApp.Migrations
{
    public partial class three : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "Address",
                value: "123 Mockingbird Lane");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                column: "Address",
                value: "456 Big Brother St");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                column: "Address",
                value: "789 Gatsby Ave");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                column: "Address",
                value: "101 Moby Dock Rd");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                column: "Address",
                value: "202 Pride Blvd");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 6,
                column: "Address",
                value: "303 Catcher St");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 7,
                column: "Address",
                value: "404 Hobbiton Ln");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 8,
                column: "Address",
                value: "505 Brave New World Blvd");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 9,
                column: "Address",
                value: "606 War and Peace Dr");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 10,
                column: "Address",
                value: "707 Alchemist Ave");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "Address",
                value: "Some Address 1");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                column: "Address",
                value: "Some Address 2");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                column: "Address",
                value: "Some Address 3");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                column: "Address",
                value: "Some Address 4");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                column: "Address",
                value: "Some Address 5");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 6,
                column: "Address",
                value: "Some Address 6");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 7,
                column: "Address",
                value: "Some Address 7");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 8,
                column: "Address",
                value: "Some Address 8");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 9,
                column: "Address",
                value: "Some Address 9");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 10,
                column: "Address",
                value: "Some Address 10");

            migrationBuilder.UpdateData(
                table: "RentCart",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { 2, 2 },
                column: "DueDate",
                value: new DateTime(2024, 6, 28, 23, 32, 59, 249, DateTimeKind.Local).AddTicks(8589));

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 1, 1 },
                column: "ReturnDate",
                value: new DateTime(2024, 6, 28, 23, 31, 59, 249, DateTimeKind.Local).AddTicks(8570));

            migrationBuilder.UpdateData(
                table: "RentDetails",
                keyColumns: new[] { "BookId", "RentId" },
                keyValues: new object[] { 3, 2 },
                column: "ReturnDate",
                value: new DateTime(2024, 6, 28, 23, 31, 59, 249, DateTimeKind.Local).AddTicks(8572));

            migrationBuilder.UpdateData(
                table: "Rents",
                keyColumn: "RentId",
                keyValue: 1,
                column: "DateOfRent",
                value: new DateTime(2024, 6, 28, 23, 30, 59, 249, DateTimeKind.Local).AddTicks(8523));

            migrationBuilder.UpdateData(
                table: "Rents",
                keyColumn: "RentId",
                keyValue: 2,
                column: "DateOfRent",
                value: new DateTime(2024, 6, 28, 23, 30, 59, 249, DateTimeKind.Local).AddTicks(8544));

            migrationBuilder.UpdateData(
                table: "SuperRentCart",
                keyColumns: new[] { "BookId", "UserId" },
                keyValues: new object[] { 4, 3 },
                column: "DueDate",
                value: new DateTime(2024, 6, 28, 23, 32, 59, 249, DateTimeKind.Local).AddTicks(8668));
        }
    }
}
