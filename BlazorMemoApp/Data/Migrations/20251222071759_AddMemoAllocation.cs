using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorMemoApp.Migrations
{
    /// <inheritdoc />
    public partial class AddMemoAllocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemoAllocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemoNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemoDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PONo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Article = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RatePO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyPO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QtyStage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QtyActual = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateConfWh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QtyPass = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateConfQc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BuyerId = table.Column<int>(type: "int", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoAllocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemoAllocations_MemoAddresses_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "MemoAddresses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MemoAllocationSpis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemoAllocationHeaderId = table.Column<int>(type: "int", nullable: false),
                    SpiNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QtyAllocate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BuyerAllocatedId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoAllocationSpis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemoAllocationSpis_MemoAddresses_BuyerAllocatedId",
                        column: x => x.BuyerAllocatedId,
                        principalTable: "MemoAddresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MemoAllocationSpis_MemoAllocations_MemoAllocationHeaderId",
                        column: x => x.MemoAllocationHeaderId,
                        principalTable: "MemoAllocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemoAllocations_BuyerId",
                table: "MemoAllocations",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_MemoAllocationSpis_BuyerAllocatedId",
                table: "MemoAllocationSpis",
                column: "BuyerAllocatedId");

            migrationBuilder.CreateIndex(
                name: "IX_MemoAllocationSpis_MemoAllocationHeaderId",
                table: "MemoAllocationSpis",
                column: "MemoAllocationHeaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemoAllocationSpis");

            migrationBuilder.DropTable(
                name: "MemoAllocations");
        }
    }
}
