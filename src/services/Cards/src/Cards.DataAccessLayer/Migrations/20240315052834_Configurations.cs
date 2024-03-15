using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cards.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Configurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Cards",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CardTypes",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CardTypes",
                type: "character varying(3000)",
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_RecipientCardId",
                table: "Transactions",
                column: "RecipientCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SenderCardId",
                table: "Transactions",
                column: "SenderCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Cards_RecipientCardId",
                table: "Transactions",
                column: "RecipientCardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Cards_SenderCardId",
                table: "Transactions",
                column: "SenderCardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Cards_RecipientCardId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Cards_SenderCardId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_RecipientCardId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_SenderCardId",
                table: "Transactions");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Cards",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CardTypes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CardTypes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(3000)",
                oldMaxLength: 3000,
                oldNullable: true);
        }
    }
}
