using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grajciar.InternetBanking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedAccountNumberLengthInSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountNumber", "CreatedAt" },
                values: new object[] { "12345675324553489", new DateTime(2025, 12, 16, 0, 54, 42, 417, DateTimeKind.Utc).AddTicks(8994) });

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountNumber", "CreatedAt" },
                values: new object[] { "3216542132324987", new DateTime(2025, 12, 16, 0, 54, 42, 417, DateTimeKind.Utc).AddTicks(9443) });

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AccountNumber", "CreatedAt" },
                values: new object[] { "98765534534544321", new DateTime(2025, 12, 16, 0, 54, 42, 417, DateTimeKind.Utc).AddTicks(9445) });

            migrationBuilder.UpdateData(
                table: "Card",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 0, 54, 42, 418, DateTimeKind.Utc).AddTicks(2552));

            migrationBuilder.UpdateData(
                table: "Card",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 0, 54, 42, 418, DateTimeKind.Utc).AddTicks(2674));

            migrationBuilder.UpdateData(
                table: "Card",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 0, 54, 42, 418, DateTimeKind.Utc).AddTicks(2675));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountNumber", "CreatedAt" },
                values: new object[] { "123456789", new DateTime(2025, 12, 16, 0, 38, 59, 555, DateTimeKind.Utc).AddTicks(2918) });

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountNumber", "CreatedAt" },
                values: new object[] { "321654987", new DateTime(2025, 12, 16, 0, 38, 59, 555, DateTimeKind.Utc).AddTicks(3361) });

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AccountNumber", "CreatedAt" },
                values: new object[] { "987654321", new DateTime(2025, 12, 16, 0, 38, 59, 555, DateTimeKind.Utc).AddTicks(3363) });

            migrationBuilder.UpdateData(
                table: "Card",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 0, 38, 59, 555, DateTimeKind.Utc).AddTicks(6501));

            migrationBuilder.UpdateData(
                table: "Card",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 0, 38, 59, 555, DateTimeKind.Utc).AddTicks(6621));

            migrationBuilder.UpdateData(
                table: "Card",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 0, 38, 59, 555, DateTimeKind.Utc).AddTicks(6622));
        }
    }
}
