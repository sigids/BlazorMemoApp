using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorMemoApp.Migrations
{
    /// <inheritdoc />
    public partial class AddMemoMultiStyle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemoMultiStyles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemoHeaderId = table.Column<int>(type: "int", nullable: false),
                    StyleId = table.Column<int>(type: "int", nullable: true),
                    StyleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GmtQty = table.Column<int>(type: "int", nullable: false),
                    GmtFobRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoMultiStyles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemoMultiStyles_BuyerStyles_StyleId",
                        column: x => x.StyleId,
                        principalTable: "BuyerStyles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MemoMultiStyles_Memos_MemoHeaderId",
                        column: x => x.MemoHeaderId,
                        principalTable: "Memos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemoMultiStyles_MemoHeaderId",
                table: "MemoMultiStyles",
                column: "MemoHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_MemoMultiStyles_StyleId",
                table: "MemoMultiStyles",
                column: "StyleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemoMultiStyles");
        }
    }
}
