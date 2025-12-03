using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorMemoApp.Migrations
{
    /// <inheritdoc />
    public partial class spibomdetailfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpiBomDetailModel_MemoDetails_MemoDetailId",
                table: "SpiBomDetailModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpiBomDetailModel",
                table: "SpiBomDetailModel");

            migrationBuilder.RenameTable(
                name: "SpiBomDetailModel",
                newName: "SpiBomDetails");

            migrationBuilder.RenameIndex(
                name: "IX_SpiBomDetailModel_MemoDetailId",
                table: "SpiBomDetails",
                newName: "IX_SpiBomDetails_MemoDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpiBomDetails",
                table: "SpiBomDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpiBomDetails_MemoDetails_MemoDetailId",
                table: "SpiBomDetails",
                column: "MemoDetailId",
                principalTable: "MemoDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpiBomDetails_MemoDetails_MemoDetailId",
                table: "SpiBomDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpiBomDetails",
                table: "SpiBomDetails");

            migrationBuilder.RenameTable(
                name: "SpiBomDetails",
                newName: "SpiBomDetailModel");

            migrationBuilder.RenameIndex(
                name: "IX_SpiBomDetails_MemoDetailId",
                table: "SpiBomDetailModel",
                newName: "IX_SpiBomDetailModel_MemoDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpiBomDetailModel",
                table: "SpiBomDetailModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpiBomDetailModel_MemoDetails_MemoDetailId",
                table: "SpiBomDetailModel",
                column: "MemoDetailId",
                principalTable: "MemoDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
