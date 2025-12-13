using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorMemoApp.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailSettingsAndCreatorFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmail",
                table: "Memos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Memos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmailSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SmtpHost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmtpPort = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnableSsl = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSettings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Memos_CreatedByUserId",
                table: "Memos",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Memos_AspNetUsers_CreatedByUserId",
                table: "Memos",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memos_AspNetUsers_CreatedByUserId",
                table: "Memos");

            migrationBuilder.DropTable(
                name: "EmailSettings");

            migrationBuilder.DropIndex(
                name: "IX_Memos_CreatedByUserId",
                table: "Memos");

            migrationBuilder.DropColumn(
                name: "CreatedByEmail",
                table: "Memos");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Memos");
        }
    }
}
