using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WS.Migrations
{
    public partial class updateEmployeetask2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_AppEmployee_Task2_AppEmployees_AssignedEmployeeId",
            //    table: "AppEmployee_Task2");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AppEmployee_Task2_AppTasks2_AssignedTask2Id",
            //    table: "AppEmployee_Task2");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_AppEmployee_Task2",
            //    table: "AppEmployee_Task2");

            //migrationBuilder.RenameTable(
            //    name: "AppEmployee_Task2",
            //    newName: "AppEmployeeTask2");

            //migrationBuilder.RenameIndex(
            //    name: "IX_AppEmployee_Task2_AssignedTask2Id",
            //    table: "AppEmployeeTask2",
            //    newName: "IX_AppEmployeeTask2_AssignedTask2Id");

            //migrationBuilder.RenameIndex(
            //    name: "IX_AppEmployee_Task2_AssignedEmployeeId",
            //    table: "AppEmployeeTask2",
            //    newName: "IX_AppEmployeeTask2_AssignedEmployeeId");

            //migrationBuilder.AddColumn<int>(
            //    name: "Task2Id",
            //    table: "AppEmployees",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_AppEmployeeTask2",
            //    table: "AppEmployeeTask2",
            //    column: "Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AppEmployees_Task2Id",
            //    table: "AppEmployees",
            //    column: "Task2Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AppEmployees_AppTasks2_Task2Id",
            //    table: "AppEmployees",
            //    column: "Task2Id",
            //    principalTable: "AppTasks2",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AppEmployeeTask2_AppEmployees_AssignedEmployeeId",
            //    table: "AppEmployeeTask2",
            //    column: "AssignedEmployeeId",
            //    principalTable: "AppEmployees",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AppEmployeeTask2_AppTasks2_AssignedTask2Id",
            //    table: "AppEmployeeTask2",
            //    column: "AssignedTask2Id",
            //    principalTable: "AppTasks2",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_AppEmployees_AppTasks2_Task2Id",
            //    table: "AppEmployees");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AppEmployeeTask2_AppEmployees_AssignedEmployeeId",
            //    table: "AppEmployeeTask2");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AppEmployeeTask2_AppTasks2_AssignedTask2Id",
            //    table: "AppEmployeeTask2");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_AppEmployeeTask2",
            //    table: "AppEmployeeTask2");

            //migrationBuilder.DropIndex(
            //    name: "IX_AppEmployees_Task2Id",
            //    table: "AppEmployees");

            //migrationBuilder.DropColumn(
            //    name: "Task2Id",
            //    table: "AppEmployees");

            //migrationBuilder.RenameTable(
            //    name: "AppEmployeeTask2",
            //    newName: "AppEmployee_Task2");

            //migrationBuilder.RenameIndex(
            //    name: "IX_AppEmployeeTask2_AssignedTask2Id",
            //    table: "AppEmployee_Task2",
            //    newName: "IX_AppEmployee_Task2_AssignedTask2Id");

            //migrationBuilder.RenameIndex(
            //    name: "IX_AppEmployeeTask2_AssignedEmployeeId",
            //    table: "AppEmployee_Task2",
            //    newName: "IX_AppEmployee_Task2_AssignedEmployeeId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_AppEmployee_Task2",
            //    table: "AppEmployee_Task2",
            //    column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AppEmployee_Task2_AppEmployees_AssignedEmployeeId",
            //    table: "AppEmployee_Task2",
            //    column: "AssignedEmployeeId",
            //    principalTable: "AppEmployees",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AppEmployee_Task2_AppTasks2_AssignedTask2Id",
            //    table: "AppEmployee_Task2",
            //    column: "AssignedTask2Id",
            //    principalTable: "AppTasks2",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
