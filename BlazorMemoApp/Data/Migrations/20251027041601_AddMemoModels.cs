using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorMemoApp.Migrations
{
    /// <inheritdoc />
    public partial class AddMemoModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Memos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemoNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemoDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuyerId = table.Column<int>(type: "int", nullable: true),
                    StyleId = table.Column<int>(type: "int", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    PONumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GmtQty = table.Column<int>(type: "int", nullable: true),
                    GmtFobValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemoDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemoHeaderId = table.Column<int>(type: "int", nullable: false),
                    Article = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BOMQty = table.Column<int>(type: "int", nullable: false),
                    AvailStockQty = table.Column<int>(type: "int", nullable: false),
                    MCQQty = table.Column<int>(type: "int", nullable: false),
                    SurchargePaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockUsableQty = table.Column<int>(type: "int", nullable: false),
                    TotalExtraCollected = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemoDetails_Memos_MemoHeaderId",
                        column: x => x.MemoHeaderId,
                        principalTable: "Memos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemoDetails_MemoHeaderId",
                table: "MemoDetails",
                column: "MemoHeaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemoDetails");

            migrationBuilder.DropTable(
                name: "Memos");
        }
    }
}
