using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.Employees.DTO
{
    [AutoMapFrom(typeof(Employee))]

    public class EmployeeListDetailDto : EntityDto
    {
        public int? Id { get; set; }
        public DateTime BirthDate { get; set; }
        public string Name { get; set; }
        public int TaskPending { get; set; }
        public int TaskComplete { get; set; }

        public int Age { get; set; }

    }
    public class EmployeeListAssignOutput
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool Selected { get; set; }

    }
    [AutoMapFrom(typeof(Employee))]

    public class EmployeeListDto : EntityDto
    {
        private DateTime currentDate = DateTime.Now;

        public const int maxLengthName = 32;
        [StringLength(Employee.maxNameLenght)]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreationTime { get; set; }
    }

    [AutoMapTo(typeof(Employee))]
    public class CreateEmployeeInput
    {
        [StringLength(Employee.maxNameLenght)]
        [Required]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreationTime { get; set; }
    }
    [AutoMapTo(typeof(Employee))]
    public class UpdateEmployeeInput
    {
        public int Id { get; set; }
        [StringLength(Employee.maxNameLenght)]
        [Required]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

    }
    public class GetEmployeeInput
    {
        public int Id { get; set; }
    }
    public class DeleteEmployeeInput
    {
        public List<int> EmployeeIds { get; set; }
    }
}
