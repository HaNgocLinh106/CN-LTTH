using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WS.Employees;
using WS.Tasks;

namespace WS.Employee_Task2
{

    //[Table("AppEmployeeTask2")]


    public class EmployeeTask2
    {
        public int EmployeeId { get; set; }
     
        public int Task2Id { get; set; }
        //[ForeignKey("EmployeeId")]
        public Employee Employee { get; set; } 
        //[ForeignKey("TaskId2")]
        public Task2 Task2 { get; set; } 
    }
}
