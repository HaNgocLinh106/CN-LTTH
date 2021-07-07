using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WS.Migrations
{
    public partial class createemployeetask3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTask_AppTasks2_Task2Id",
                table: "EmployeeTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeTask",
                table: "EmployeeTask");

            migrationBuilder.DropColumn(
                name: "TaskId2",
                table: "EmployeeTask");

            migrationBuilder.AlterColumn<int>(
                name: "Task2Id",
                table: "EmployeeTask",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeTask",
                table: "EmployeeTask",
                columns: new[] { "EmployeeId", "Task2Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTask_AppTasks2_Task2Id",
                table: "EmployeeTask",
                column: "Task2Id",
                principalTable: "AppTasks2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTask_AppTasks2_Task2Id",
                table: "EmployeeTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeTask",
                table: "EmployeeTask");

            migrationBuilder.AlterColumn<int>(
                name: "Task2Id",
                table: "EmployeeTask",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TaskId2",
                table: "EmployeeTask",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeTask",
                table: "EmployeeTask",
                columns: new[] { "EmployeeId", "TaskId2" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTask_AppTasks2_Task2Id",
                table: "EmployeeTask",
                column: "Task2Id",
                principalTable: "AppTasks2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
