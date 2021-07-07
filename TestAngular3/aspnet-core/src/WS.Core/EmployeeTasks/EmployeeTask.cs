using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WS.Employees;
using WS.Tasks;

namespace WS.EmployeeTasks
{

    //[Table("AppEmployeeTask2")]


    public class EmployeeTask
    {
        public int EmployeeId { get; set; }

        public int Task2Id { get; set; }
        //[ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        //[ForeignKey("TaskId2")]
        public Task2 Task2 { get; set; }
    }
}
