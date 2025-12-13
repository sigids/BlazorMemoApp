using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorMemoApp.Migrations
{
    /// <inheritdoc />
    public partial class BuyerStyleSpimodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuyerStyleOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerId = table.Column<int>(type: "int", nullable: false),
                    StyleId = table.Column<int>(type: "int", nullable: false),
                    OrderNo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerStyleOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyerStyleOrders_BuyerStyles_StyleId",
                        column: x => x.StyleId,
                        principalTable: "BuyerStyles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BuyerStyleOrders_MemoAddresses_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "MemoAddresses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuyerStyleOrders_BuyerId",
                table: "BuyerStyleOrders",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerStyleOrders_StyleId",
                table: "BuyerStyleOrders",
                column: "StyleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyerStyleOrders");
        }
    }
}
