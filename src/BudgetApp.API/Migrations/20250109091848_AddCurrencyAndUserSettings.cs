using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BudgetApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencyAndUserSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Flag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                    table.UniqueConstraint("AK_Currencies_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationPreferencesJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BudgetPreferencesJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSettings_Currencies_CurrencyCode",
                        column: x => x.CurrencyCode,
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSettings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "CreatedAt", "DisplayOrder", "Flag", "IsActive", "IsDeleted", "Name", "Symbol", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "USD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "🇺🇸", true, false, "US Dollar", "$", null },
                    { 2, "EUR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "🇪🇺", true, false, "Euro", "€", null },
                    { 3, "TRY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "🇹🇷", true, false, "Turkish Lira", "₺", null },
                    { 4, "GBP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "🇬🇧", true, false, "British Pound", "£", null },
                    { 5, "JPY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "🇯🇵", true, false, "Japanese Yen", "¥", null },
                    { 6, "CNY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "🇨🇳", true, false, "Chinese Yuan", "¥", null },
                    { 7, "AUD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "🇦🇺", true, false, "Australian Dollar", "$", null },
                    { 8, "CAD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "🇨🇦", true, false, "Canadian Dollar", "$", null },
                    { 9, "CHF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, "🇨🇭", true, false, "Swiss Franc", "Fr", null },
                    { 10, "HKD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "🇭🇰", true, false, "Hong Kong Dollar", "$", null },
                    { 11, "NZD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "🇳🇿", true, false, "New Zealand Dollar", "$", null },
                    { 12, "SEK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, "🇸🇪", true, false, "Swedish Krona", "kr", null },
                    { 13, "KRW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, "🇰🇷", true, false, "South Korean Won", "₩", null },
                    { 14, "SGD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, "🇸🇬", true, false, "Singapore Dollar", "$", null },
                    { 15, "NOK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, "🇳🇴", true, false, "Norwegian Krone", "kr", null },
                    { 16, "MXN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, "🇲🇽", true, false, "Mexican Peso", "$", null },
                    { 17, "INR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, "🇮🇳", true, false, "Indian Rupee", "₹", null },
                    { 18, "RUB", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, "🇷🇺", true, false, "Russian Ruble", "₽", null },
                    { 19, "ZAR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, "🇿🇦", true, false, "South African Rand", "R", null },
                    { 20, "BRL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, "🇧🇷", true, false, "Brazilian Real", "R$", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Code",
                table: "Currencies",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_CurrencyCode",
                table: "UserSettings",
                column: "CurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_UserId",
                table: "UserSettings",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSettings");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
