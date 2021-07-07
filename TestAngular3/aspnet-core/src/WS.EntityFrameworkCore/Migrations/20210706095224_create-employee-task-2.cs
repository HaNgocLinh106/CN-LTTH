using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WS.Migrations
{
    public partial class createemployeetask2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTask_2_AppTasks2_Task2Id",
                table: "EmployeeTask_2");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeTask_2",
                table: "EmployeeTask_2");

            migrationBuilder.DropColumn(
                name: "TaskId2",
                table: "EmployeeTask_2");

            migrationBuilder.AlterColumn<int>(
                name: "Task2Id",
                table: "EmployeeTask_2",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeTask_2",
                table: "EmployeeTask_2",
                columns: new[] { "EmployeeId", "Task2Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTask_2_AppTasks2_Task2Id",
                table: "EmployeeTask_2",
                column: "Task2Id",
                principalTable: "AppTasks2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTask_2_AppTasks2_Task2Id",
                table: "EmployeeTask_2");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeTask_2",
                table: "EmployeeTask_2");

            migrationBuilder.AlterColumn<int>(
                name: "Task2Id",
                table: "EmployeeTask_2",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TaskId2",
                table: "EmployeeTask_2",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeTask_2",
                table: "EmployeeTask_2",
                columns: new[] { "EmployeeId", "TaskId2" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTask_2_AppTasks2_Task2Id",
                table: "EmployeeTask_2",
                column: "Task2Id",
                principalTable: "AppTasks2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
