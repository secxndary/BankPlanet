using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cards.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class TransactionDecimalAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Transactions",
                newName: "SenderCardId");

            migrationBuilder.RenameColumn(
                name: "RecipientId",
                table: "Transactions",
                newName: "RecipientCardId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenderCardId",
                table: "Transactions",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "RecipientCardId",
                table: "Transactions",
                newName: "RecipientId");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Transactions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
