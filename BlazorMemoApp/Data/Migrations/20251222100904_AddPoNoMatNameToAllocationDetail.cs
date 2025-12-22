using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorMemoApp.Migrations
{
    /// <inheritdoc />
    public partial class AddPoNoMatNameToAllocationDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MatName",
                table: "MemoAllocationDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PONo",
                table: "MemoAllocationDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatName",
                table: "MemoAllocationDetails");

            migrationBuilder.DropColumn(
                name: "PONo",
                table: "MemoAllocationDetails");
        }
    }
}
