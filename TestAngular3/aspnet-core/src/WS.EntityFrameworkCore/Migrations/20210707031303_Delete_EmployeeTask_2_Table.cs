using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WS.Migrations
{
    public partial class Delete_EmployeeTask_2_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "EmployeeTask_2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
        }
    }
}
