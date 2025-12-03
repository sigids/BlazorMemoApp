using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorMemoApp.Migrations
{
    /// <inheritdoc />
    public partial class spibomdetailfk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpiBomDetailModel_MemoDetails_MemoDetailModelId",
                table: "SpiBomDetailModel");

            migrationBuilder.DropIndex(
                name: "IX_SpiBomDetailModel_MemoDetailModelId",
                table: "SpiBomDetailModel");

            migrationBuilder.DropColumn(
                name: "MemoDetailModelId",
                table: "SpiBomDetailModel");

            migrationBuilder.AddColumn<int>(
                name: "MemoDetailId",
                table: "SpiBomDetailModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SpiBomDetailModel_MemoDetailId",
                table: "SpiBomDetailModel",
                column: "MemoDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpiBomDetailModel_MemoDetails_MemoDetailId",
                table: "SpiBomDetailModel",
                column: "MemoDetailId",
                principalTable: "MemoDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpiBomDetailModel_MemoDetails_MemoDetailId",
                table: "SpiBomDetailModel");

            migrationBuilder.DropIndex(
                name: "IX_SpiBomDetailModel_MemoDetailId",
                table: "SpiBomDetailModel");

            migrationBuilder.DropColumn(
                name: "MemoDetailId",
                table: "SpiBomDetailModel");

            migrationBuilder.AddColumn<int>(
                name: "MemoDetailModelId",
                table: "SpiBomDetailModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpiBomDetailModel_MemoDetailModelId",
                table: "SpiBomDetailModel",
                column: "MemoDetailModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpiBomDetailModel_MemoDetails_MemoDetailModelId",
                table: "SpiBomDetailModel",
                column: "MemoDetailModelId",
                principalTable: "MemoDetails",
                principalColumn: "Id");
        }
    }
}
