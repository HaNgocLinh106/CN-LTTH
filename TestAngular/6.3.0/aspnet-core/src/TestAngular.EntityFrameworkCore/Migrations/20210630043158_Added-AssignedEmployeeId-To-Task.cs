using Microsoft.EntityFrameworkCore.Migrations;

namespace TestAngular.Migrations
{
    public partial class AddedAssignedEmployeeIdToTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedEmployeeId",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignedEmployeeId",
                table: "Tasks",
                column: "AssignedEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AppEmployees_AssignedEmployeeId",
                table: "Tasks",
                column: "AssignedEmployeeId",
                principalTable: "AppEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AppEmployees_AssignedEmployeeId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AssignedEmployeeId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "AssignedEmployeeId",
                table: "Tasks");
        }
    }
}
