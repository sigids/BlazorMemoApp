using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorMemoApp.Migrations
{
    /// <inheritdoc />
    public partial class spibomdetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BOMQty",
                table: "MemoDetails");

            migrationBuilder.AddColumn<string>(
                name: "SpiNo",
                table: "MemoDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SpiBomDetailModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpiNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BomQty = table.Column<int>(type: "int", nullable: false),
                    MemoDetailModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpiBomDetailModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpiBomDetailModel_MemoDetails_MemoDetailModelId",
                        column: x => x.MemoDetailModelId,
                        principalTable: "MemoDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpiBomDetailModel_MemoDetailModelId",
                table: "SpiBomDetailModel",
                column: "MemoDetailModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpiBomDetailModel");

            migrationBuilder.DropColumn(
                name: "SpiNo",
                table: "MemoDetails");

            migrationBuilder.AddColumn<int>(
                name: "BOMQty",
                table: "MemoDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
