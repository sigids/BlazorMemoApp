using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorMemoApp.Migrations
{
    /// <inheritdoc />
    public partial class role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApproveDate",
                table: "Memos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApproveStatus",
                table: "Memos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApproveUserId",
                table: "Memos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Memos_ApproveUserId",
                table: "Memos",
                column: "ApproveUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Memos_AspNetUsers_ApproveUserId",
                table: "Memos",
                column: "ApproveUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memos_AspNetUsers_ApproveUserId",
                table: "Memos");

            migrationBuilder.DropIndex(
                name: "IX_Memos_ApproveUserId",
                table: "Memos");

            migrationBuilder.DropColumn(
                name: "ApproveDate",
                table: "Memos");

            migrationBuilder.DropColumn(
                name: "ApproveStatus",
                table: "Memos");

            migrationBuilder.DropColumn(
                name: "ApproveUserId",
                table: "Memos");
        }
    }
}
