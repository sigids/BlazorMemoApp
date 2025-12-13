using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorMemoApp.Migrations
{
    /// <inheritdoc />
    public partial class addfieldgmtfobratemmheader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "GmtFobRate",
                table: "Memos",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GmtFobRate",
                table: "Memos");
        }
    }
}
