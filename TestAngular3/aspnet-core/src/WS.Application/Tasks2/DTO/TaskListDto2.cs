using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WS.Tasks;

namespace WS.Tasks2.DTO
{
    [AutoMapFrom(typeof(Task2))]
    public class TaskListDto2 : EntityDto, IHasCreationTime
    {
        public TaskListDto2()
        {

        }
        public int TenantId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public TaskState State { get; set; }
        public List<string> EmployeeNames { get; set; }
    }
  
    public class GetAllTaskInput
    {
        public TaskState? State { get; set; }
    }
    [AutoMapTo(typeof(Task2))]
    public class CreateTaskInput2
    {
        CreateTaskInput2()
        {
            this.TenantId = 1;
        }
        [MaxLength(WS.Tasks.Task2.MaxTitleLength)]
        public string Title { get; set; }
        [MaxLength(WS.Tasks.Task2.MaxDescriptionLength)]
        public string Description { get; set; }
        public int TenantId { get; set; } = 1;
        public TaskState State { get; set; }

    }

    public class GetTaskInput2
    {
        public int Id { get; set; }
    }
    [AutoMapTo(typeof(Task2))]
    public class UpdateTaskInput2
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        public TaskState State { get; set; }
        public List<int> EmployeeIds { get; set; }
    }
    public class AssignTaskInput2
    {
        public AssignTaskInput2()
        {
        }
        public int Id { get; set; }
        public List<int> EmployeeIds { get; set; }
    }
    public class DeleteTaskInput2
    {
        public int Id { get; set; }


    }
    public class EmployeeTaskList
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int Task2Id { get; set; }
    }
}
