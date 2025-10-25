using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Grajciar.InternetBanking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class data_seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "Id", "Address", "BankCode", "Name", "SwiftCode" },
                values: new object[,]
                {
                    { 1, "Masarykova XYZ, Brno", "0001", "Banka 1", "BANK1CZ" },
                    { 2, "Masarykova XYZ, Brno", "0020", "Banka 2", "BANK2CZ" },
                    { 3, "Masarykova XYZ, Brno", "0300", "Banka 3", "BANK3CZ" }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 14, 52, 46, 642, DateTimeKind.Utc).AddTicks(6986));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 14, 52, 46, 642, DateTimeKind.Utc).AddTicks(8155));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 14, 52, 46, 642, DateTimeKind.Utc).AddTicks(8159));

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "AccountNumber", "Balance", "BankId", "CreatedAt", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, "123456789", 1000m, 1, new DateTime(2025, 10, 25, 14, 52, 46, 643, DateTimeKind.Utc).AddTicks(8297), 0, 1 },
                    { 2, "321654987", 500m, 2, new DateTime(2025, 10, 25, 14, 52, 46, 643, DateTimeKind.Utc).AddTicks(8741), 3, 2 },
                    { 3, "987654321", 2000m, 3, new DateTime(2025, 10, 25, 14, 52, 46, 643, DateTimeKind.Utc).AddTicks(8743), 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "Card",
                columns: new[] { "Id", "AccountId", "CardHolderName", "CardNumber", "CreatedAt", "ExpirationDate", "IsBlocked", "SecurityCode", "Type" },
                values: new object[,]
                {
                    { 1, 1, "Petr Grajciar", "0123456789012345", new DateTime(2025, 10, 25, 14, 52, 46, 644, DateTimeKind.Utc).AddTicks(1892), new DateTime(2026, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "123", 0 },
                    { 2, 2, "Karel Chleba", "1234951274563654", new DateTime(2025, 10, 25, 14, 52, 46, 644, DateTimeKind.Utc).AddTicks(2013), new DateTime(2027, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "213", 1 },
                    { 3, 3, "Šimon Rohlík", "1478852365891452", new DateTime(2025, 10, 25, 14, 52, 46, 644, DateTimeKind.Utc).AddTicks(2014), new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "231", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Card",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 11, 29, 18, 991, DateTimeKind.Utc).AddTicks(1188));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 11, 29, 18, 991, DateTimeKind.Utc).AddTicks(2390));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 25, 11, 29, 18, 991, DateTimeKind.Utc).AddTicks(2394));
        }
    }
}
