using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Tasks;

namespace WS.Employees
{
    [Table("AppEmployees")]
    public class Employee : Entity, IHasCreationTime
    {
        public const int maxNameLenght = 32;
        [Required]
        [StringLength(maxNameLenght)]
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime BirthDate { get; set; }

        public List<WS.Tasks.Task> Tasks { get; set; }
        public List<WS.EmployeeTasks.EmployeeTask> EmployeeTasks { get; set; }
        //public int Task2Id { get; set; }
        //public Task2 Task2 { get; set; }
        public int Age => DateTime.Now.Year - BirthDate.Year;

        public Employee()
        {
            BirthDate = Clock.Now;
            Tasks = new List<WS.Tasks.Task>();
            EmployeeTasks = new List<WS.EmployeeTasks.EmployeeTask>();
        }
    }
}
