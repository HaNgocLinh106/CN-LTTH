using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAngular.Employees.DTO
{
    [AutoMapFrom(typeof(Employee))]

    public class EmployeeListDto2 : EntityDto
    {
    
        public const int maxLengthName = 32;
        [StringLength(Employee.maxNameLenght)]
        public string name { get; set; }
        public int Age { get; set; }
        public int totalStateOpen { get; set; }
        public int totalStatecomponent { get; set; }



    }
    [AutoMapFrom(typeof(Employee))]
   
    public class EmployeeListDto : EntityDto
    {
        private DateTime currentDate = DateTime.Now;

        public const int maxLengthName = 32;
        [StringLength(Employee.maxNameLenght)]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        private int Age
        {
            get
            {
                int age;
                age = DateTime.Now.Year - BirthDate.Year;
                if (DateTime.Now.DayOfYear < BirthDate.DayOfYear)
                    age = age - 1;

                return age;
            }
           
        }


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
        public int Id { get; set; }
    }
}
