using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencySupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Transactions",
                newName: "OriginalAmount");

            migrationBuilder.AddColumn<decimal>(
                name: "ConvertedAmount",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ExchangeRate",
                table: "Transactions",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CurrencyCode",
                table: "Transactions",
                column: "CurrencyCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Currencies_CurrencyCode",
                table: "Transactions",
                column: "CurrencyCode",
                principalTable: "Currencies",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Currencies_CurrencyCode",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CurrencyCode",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ConvertedAmount",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ExchangeRate",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "OriginalAmount",
                table: "Transactions",
                newName: "Amount");
        }
    }
}
