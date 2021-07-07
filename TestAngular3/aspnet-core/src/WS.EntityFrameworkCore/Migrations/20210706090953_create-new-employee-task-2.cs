using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WS.Migrations
{
    public partial class createnewemployeetask2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTasks_AppEmployees_AssignedEmployeeId",
                table: "AppTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppTasks",
                table: "AppTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppEmployeeTask_2",
                table: "AppEmployeeTask_2");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AppEmployeeTask_2");

            migrationBuilder.DropColumn(
                name: "test",
                table: "AppEmployeeTask_2");

            migrationBuilder.RenameTable(
                name: "AppTasks",
                newName: "Tasks");

            migrationBuilder.RenameTable(
                name: "AppEmployeeTask_2",
                newName: "EmployeeTask_2");

            migrationBuilder.RenameIndex(
                name: "IX_AppTasks_AssignedEmployeeId",
                table: "Tasks",
                newName: "IX_Tasks_AssignedEmployeeId");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "EmployeeTask_2",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TaskId2",
                table: "EmployeeTask_2",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "Task2Id",
            //    table: "EmployeeTask_2",
            //    type: "int",
            //    nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeTask_2",
                table: "EmployeeTask_2",
                columns: new[] { "EmployeeId", "TaskId2" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTask_2_Task2Id",
                table: "EmployeeTask_2",
                column: "Task2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTask_2_AppEmployees_EmployeeId",
                table: "EmployeeTask_2",
                column: "EmployeeId",
                principalTable: "AppEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTask_2_AppTasks2_Task2Id",
                table: "EmployeeTask_2",
                column: "Task2Id",
                principalTable: "AppTasks2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_EmployeeTask_2_AppEmployees_EmployeeId",
                table: "EmployeeTask_2");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTask_2_AppTasks2_Task2Id",
                table: "EmployeeTask_2");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AppEmployees_AssignedEmployeeId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeTask_2",
                table: "EmployeeTask_2");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTask_2_Task2Id",
                table: "EmployeeTask_2");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "EmployeeTask_2");

            migrationBuilder.DropColumn(
                name: "TaskId2",
                table: "EmployeeTask_2");

            migrationBuilder.DropColumn(
                name: "Task2Id",
                table: "EmployeeTask_2");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "AppTasks");

            migrationBuilder.RenameTable(
                name: "EmployeeTask_2",
                newName: "AppEmployeeTask_2");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_AssignedEmployeeId",
                table: "AppTasks",
                newName: "IX_AppTasks_AssignedEmployeeId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AppEmployeeTask_2",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            //migrationBuilder.AddColumn<int>(
            //    name: "test",
            //    table: "AppEmployeeTask_2",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppTasks",
                table: "AppTasks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppEmployeeTask_2",
                table: "AppEmployeeTask_2",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTasks_AppEmployees_AssignedEmployeeId",
                table: "AppTasks",
                column: "AssignedEmployeeId",
                principalTable: "AppEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
