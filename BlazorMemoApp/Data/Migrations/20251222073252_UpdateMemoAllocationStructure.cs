using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorMemoApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMemoAllocationStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemoAllocations_MemoAddresses_BuyerId",
                table: "MemoAllocations");

            migrationBuilder.DropForeignKey(
                name: "FK_MemoAllocationSpis_MemoAllocations_MemoAllocationHeaderId",
                table: "MemoAllocationSpis");

            migrationBuilder.DropIndex(
                name: "IX_MemoAllocations_BuyerId",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "Article",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "CurrencyPO",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "DateConfQc",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "DateConfWh",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "ItemCode",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "QtyActual",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "QtyPass",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "QtyStage",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "RatePO",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "Supplier",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "UnitPO",
                table: "MemoAllocations");

            migrationBuilder.RenameColumn(
                name: "MemoAllocationHeaderId",
                table: "MemoAllocationSpis",
                newName: "MemoAllocationDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_MemoAllocationSpis_MemoAllocationHeaderId",
                table: "MemoAllocationSpis",
                newName: "IX_MemoAllocationSpis_MemoAllocationDetailId");

            migrationBuilder.CreateTable(
                name: "MemoAllocationDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemoAllocationHeaderId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_MemoAllocationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemoAllocationDetails_MemoAddresses_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "MemoAddresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MemoAllocationDetails_MemoAllocations_MemoAllocationHeaderId",
                        column: x => x.MemoAllocationHeaderId,
                        principalTable: "MemoAllocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemoAllocationDetails_BuyerId",
                table: "MemoAllocationDetails",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_MemoAllocationDetails_MemoAllocationHeaderId",
                table: "MemoAllocationDetails",
                column: "MemoAllocationHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemoAllocationSpis_MemoAllocationDetails_MemoAllocationDetailId",
                table: "MemoAllocationSpis",
                column: "MemoAllocationDetailId",
                principalTable: "MemoAllocationDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemoAllocationSpis_MemoAllocationDetails_MemoAllocationDetailId",
                table: "MemoAllocationSpis");

            migrationBuilder.DropTable(
                name: "MemoAllocationDetails");

            migrationBuilder.RenameColumn(
                name: "MemoAllocationDetailId",
                table: "MemoAllocationSpis",
                newName: "MemoAllocationHeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_MemoAllocationSpis_MemoAllocationDetailId",
                table: "MemoAllocationSpis",
                newName: "IX_MemoAllocationSpis_MemoAllocationHeaderId");

            migrationBuilder.AddColumn<string>(
                name: "Article",
                table: "MemoAllocations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "MemoAllocations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "MemoAllocations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CurrencyPO",
                table: "MemoAllocations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateConfQc",
                table: "MemoAllocations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateConfWh",
                table: "MemoAllocations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemCode",
                table: "MemoAllocations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "QtyActual",
                table: "MemoAllocations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "QtyPass",
                table: "MemoAllocations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "QtyStage",
                table: "MemoAllocations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RatePO",
                table: "MemoAllocations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "MemoAllocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "MemoAllocations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Supplier",
                table: "MemoAllocations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UnitPO",
                table: "MemoAllocations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MemoAllocations_BuyerId",
                table: "MemoAllocations",
                column: "BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemoAllocations_MemoAddresses_BuyerId",
                table: "MemoAllocations",
                column: "BuyerId",
                principalTable: "MemoAddresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MemoAllocationSpis_MemoAllocations_MemoAllocationHeaderId",
                table: "MemoAllocationSpis",
                column: "MemoAllocationHeaderId",
                principalTable: "MemoAllocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
