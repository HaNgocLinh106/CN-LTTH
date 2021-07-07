using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WS.Migrations
{
    public partial class Added_tasks2_EmployeeTask2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppTasks2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 65536, nullable: true),
                    State = table.Column<byte>(type: "tinyint", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTasks2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppEmployee_Task2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssignedEmployeeId = table.Column<int>(type: "int", nullable: true),
                    AssignedTask2Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppEmployee_Task2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppEmployee_Task2_AppEmployees_AssignedEmployeeId",
                        column: x => x.AssignedEmployeeId,
                        principalTable: "AppEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppEmployee_Task2_AppTasks2_AssignedTask2Id",
                        column: x => x.AssignedTask2Id,
                        principalTable: "AppTasks2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppEmployee_Task2_AssignedEmployeeId",
                table: "AppEmployee_Task2",
                column: "AssignedEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppEmployee_Task2_AssignedTask2Id",
                table: "AppEmployee_Task2",
                column: "AssignedTask2Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppEmployee_Task2");

            migrationBuilder.DropTable(
                name: "AppTasks2");
        }
    }
}
