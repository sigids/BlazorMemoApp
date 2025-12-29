using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorMemoApp.Migrations
{
    /// <inheritdoc />
    public partial class buyerallocheader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "MemoAllocations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemoAllocations_BuyerId",
                table: "MemoAllocations",
                column: "BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemoAllocations_MemoAddresses_BuyerId",
                table: "MemoAllocations",
                column: "BuyerId",
                principalTable: "MemoAddresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemoAllocations_MemoAddresses_BuyerId",
                table: "MemoAllocations");

            migrationBuilder.DropIndex(
                name: "IX_MemoAllocations_BuyerId",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "MemoAllocations");
        }
    }
}
