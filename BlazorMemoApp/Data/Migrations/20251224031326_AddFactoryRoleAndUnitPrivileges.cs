using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorMemoApp.Migrations
{
    /// <inheritdoc />
    public partial class AddFactoryRoleAndUnitPrivileges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "QcApprovalDate",
                table: "MemoAllocations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "QcApprovalStatus",
                table: "MemoAllocations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "QcApprovalUserId",
                table: "MemoAllocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QcApprovalUserName",
                table: "MemoAllocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WhApprovalDate",
                table: "MemoAllocations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WhApprovalStatus",
                table: "MemoAllocations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "WhApprovalUserId",
                table: "MemoAllocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhApprovalUserName",
                table: "MemoAllocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserFactoryRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFactoryRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFactoryRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFactoryUnitPrivileges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFactoryUnitPrivileges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFactoryUnitPrivileges_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFactoryRoles_UserId",
                table: "UserFactoryRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFactoryUnitPrivileges_UserId",
                table: "UserFactoryUnitPrivileges",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFactoryRoles");

            migrationBuilder.DropTable(
                name: "UserFactoryUnitPrivileges");

            migrationBuilder.DropColumn(
                name: "QcApprovalDate",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "QcApprovalStatus",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "QcApprovalUserId",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "QcApprovalUserName",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "WhApprovalDate",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "WhApprovalStatus",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "WhApprovalUserId",
                table: "MemoAllocations");

            migrationBuilder.DropColumn(
                name: "WhApprovalUserName",
                table: "MemoAllocations");
        }
    }
}
