using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WS.Employees;
using WS.Tasks;

namespace WS.Employee_Task
{
    [Table("AppEmployeeTask2")]
    public class EmployeeTask:Entity
    {
      
        public EmployeeTask()
        {
        }
        [ForeignKey(nameof(AssignedEmployeeId))]
        public Employee AssignedEmployee { get; set; } // them tu Employee table
        public int? AssignedEmployeeId { get; set; }
        [ForeignKey(nameof(AssignedTask2Id))]
        public Task2 AssignedTask2 { get; set; } // them tu Employee table
        public int? AssignedTask2Id { get; set; }
        //public EmployeeTask(int tenantId, string title, string description = null, int? assignedEmployeeId = null)
        //    : this()
        //{
        //    Title = title;
        //    Description = description;
        //    AssignedEmployeeId = assignedEmployeeId;
        //}
    }
}
