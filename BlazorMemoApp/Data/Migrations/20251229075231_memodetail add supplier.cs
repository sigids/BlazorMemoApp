using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorMemoApp.Migrations
{
    /// <inheritdoc />
    public partial class memodetailaddsupplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Supplier",
                table: "MemoDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Memos_BuyerId",
                table: "Memos",
                column: "BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Memos_MemoAddresses_BuyerId",
                table: "Memos",
                column: "BuyerId",
                principalTable: "MemoAddresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memos_MemoAddresses_BuyerId",
                table: "Memos");

            migrationBuilder.DropIndex(
                name: "IX_Memos_BuyerId",
                table: "Memos");

            migrationBuilder.DropColumn(
                name: "Supplier",
                table: "MemoDetails");
        }
    }
}
